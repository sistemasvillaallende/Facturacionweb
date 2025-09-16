using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Facturacion.Secure
{
    public partial class PagoCedulon : System.Web.UI.Page
    {
        #region Propiedades
        public string NumeroCedulon { get; set; }
        public decimal MontoOriginal { get; set; }
        public decimal MontoIntereses { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool EstaVencido { get; set; }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtener número de cedulón del querystring
                NumeroCedulon = Request.QueryString["cedulon"];
                
                if (string.IsNullOrEmpty(NumeroCedulon))
                {
                    MostrarError("No se proporcionó un número de cedulón válido.");
                    return;
                }
                
                // Cargar datos del cedulón
                CargarDatosCedulon();
                
                // Cargar tarjetas disponibles
                CargarTarjetas();
            }
            else
            {
                // Verificar si se seleccionó una tarjeta
                if (Request.Form["__EVENTTARGET"] == hdnTarjetaSeleccionada.UniqueID)
                {
                    string tarjetaId = hdnTarjetaSeleccionada.Value;
                    if (!string.IsNullOrEmpty(tarjetaId))
                    {
                        CargarPlanesCuotas(Convert.ToInt32(tarjetaId));
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarPlanes", "mostrarPlanes();", true);
                    }
                }
            }
        }

        private void CargarDatosCedulon()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    string query = @"
                        SELECT 
                            c.numero_cedulon,
                            c.monto_original,
                            c.monto_intereses,
                            c.monto_total,
                            c.fecha_vencimiento,
                            c.descripcion,
                            d.cuit_cuil,
                            d.apellido,
                            d.nombre
                        FROM Cedulones c
                        INNER JOIN Deudores d ON c.id_deudor = d.id
                        WHERE c.numero_cedulon = @numeroCedulon
                        AND c.estado = 'Pendiente'";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@numeroCedulon", NumeroCedulon);
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Asignar valores a los controles
                                lblNroCedulon.Text = reader["numero_cedulon"].ToString();
                                lblCuit.Text = reader["cuit_cuil"].ToString();
                                lblContribuyente.Text = $"{reader["apellido"]} {reader["nombre"]}";
                                lblDescripcion.Text = reader["descripcion"].ToString();
                                
                                FechaVencimiento = Convert.ToDateTime(reader["fecha_vencimiento"]);
                                lblVencimiento.Text = FechaVencimiento.ToString("dd/MM/yyyy");
                                
                                MontoOriginal = Convert.ToDecimal(reader["monto_original"]);
                                MontoIntereses = Convert.ToDecimal(reader["monto_intereses"]);
                                MontoTotal = Convert.ToDecimal(reader["monto_total"]);
                                
                                lblMontoOriginal.Text = MontoOriginal.ToString("N2");
                                lblMontoTotal.Text = MontoTotal.ToString("N2");
                                
                                // Verificar si está vencido
                                EstaVencido = DateTime.Now > FechaVencimiento;
                                
                                if (MontoIntereses > 0)
                                {
                                    divIntereses.Visible = true;
                                    lblIntereses.Text = MontoIntereses.ToString("N2");
                                }
                                
                                if (EstaVencido)
                                {
                                    divOverdue.Visible = true;
                                }
                                
                                divPrincipal.Visible = true;
                            }
                            else
                            {
                                MostrarError("No se encontró un cedulón válido con el número proporcionado o ya se encuentra pagado.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error al cargar los datos del cedulón: {ex.Message}");
            }
        }

        private void CargarTarjetas()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    string query = @"
                        SELECT 
                            id,
                            nombre,
                            descripcion,
                            logo,
                            activa
                        FROM TarjetasCredito
                        WHERE activa = 1
                        ORDER BY orden, nombre";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            
                            rptTarjetas.DataSource = dt;
                            rptTarjetas.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error al cargar las tarjetas: {ex.Message}");
            }
        }

        private void CargarPlanesCuotas(int tarjetaId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    string query = @"
                        SELECT 
                            p.id,
                            p.cantidad_cuotas,
                            p.costo_financiero,
                            p.porcentaje_descuento,
                            p.aplica_descuento_vencido,
                            p.activo,
                            -- Calcular montos
                            CASE 
                                WHEN p.aplica_descuento_vencido = 1 AND @estaVencido = 1 
                                THEN (@montoTotal * (1 + p.costo_financiero)) * (1 - p.porcentaje_descuento/100)
                                ELSE @montoTotal * (1 + p.costo_financiero)
                            END as monto_total_plan,
                            CASE 
                                WHEN p.aplica_descuento_vencido = 1 AND @estaVencido = 1 
                                THEN ((@montoTotal * (1 + p.costo_financiero)) * (1 - p.porcentaje_descuento/100)) / p.cantidad_cuotas
                                ELSE (@montoTotal * (1 + p.costo_financiero)) / p.cantidad_cuotas
                            END as monto_cuota
                        FROM PlanesCuotas p
                        WHERE p.id_tarjeta = @tarjetaId
                        AND p.activo = 1
                        ORDER BY p.cantidad_cuotas";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@tarjetaId", tarjetaId);
                        cmd.Parameters.AddWithValue("@montoTotal", MontoTotal);
                        cmd.Parameters.AddWithValue("@estaVencido", EstaVencido ? 1 : 0);
                        
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            
                            // Procesar datos para mostrar descuentos aplicables
                            foreach (DataRow row in dt.Rows)
                            {
                                bool aplicaDescuento = Convert.ToBoolean(row["aplica_descuento_vencido"]) && EstaVencido;
                                decimal porcentajeDescuento = Convert.ToDecimal(row["porcentaje_descuento"]);
                                
                                // Agregar columna calculada para mostrar el descuento
                                if (!dt.Columns.Contains("MostrarDescuento"))
                                {
                                    dt.Columns.Add("MostrarDescuento", typeof(bool));
                                    dt.Columns.Add("DescuentoAplicable", typeof(decimal));
                                }
                                
                                row["MostrarDescuento"] = aplicaDescuento && porcentajeDescuento > 0;
                                row["DescuentoAplicable"] = aplicaDescuento ? porcentajeDescuento : 0;
                            }
                            
                            rptPlanes.DataSource = dt;
                            rptPlanes.DataBind();
                        }
                    }
                }
                
                upMain.Update();
            }
            catch (Exception ex)
            {
                MostrarError($"Error al cargar los planes de cuotas: {ex.Message}");
            }
        }

        protected void rptTarjetas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Este método se puede usar si necesitas manejar comandos específicos de las tarjetas
        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {
            try
            {
                string tarjetaSeleccionada = hdnTarjetaSeleccionada.Value;
                string planSeleccionado = hdnPlanSeleccionado.Value;
                
                if (string.IsNullOrEmpty(tarjetaSeleccionada))
                {
                    MostrarError("Debe seleccionar una tarjeta de crédito.");
                    return;
                }
                
                if (string.IsNullOrEmpty(planSeleccionado))
                {
                    MostrarError("Debe seleccionar un plan de cuotas.");
                    return;
                }
                
                // Guardar la transacción en la base de datos
                int transaccionId = GuardarTransaccion(
                    Convert.ToInt32(tarjetaSeleccionada), 
                    Convert.ToInt32(planSeleccionado)
                );
                
                // Redirigir al procesador de pagos (aquí puedes integrar con MercadoPago, etc.)
                string urlPago = GenerarUrlPago(transaccionId);
                Response.Redirect(urlPago);
            }
            catch (Exception ex)
            {
                MostrarError($"Error al procesar el pago: {ex.Message}");
            }
        }

        private int GuardarTransaccion(int tarjetaId, int planId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string query = @"
                            INSERT INTO TransaccionesPago 
                            (numero_cedulon, id_tarjeta, id_plan, monto_original, monto_intereses, 
                             monto_total, fecha_creacion, estado, ip_cliente)
                            VALUES 
                            (@numeroCedulon, @tarjetaId, @planId, @montoOriginal, @montoIntereses, 
                             @montoTotal, @fechaCreacion, @estado, @ipCliente);
                            SELECT SCOPE_IDENTITY();";
                        
                        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@numeroCedulon", NumeroCedulon);
                            cmd.Parameters.AddWithValue("@tarjetaId", tarjetaId);
                            cmd.Parameters.AddWithValue("@planId", planId);
                            cmd.Parameters.AddWithValue("@montoOriginal", MontoOriginal);
                            cmd.Parameters.AddWithValue("@montoIntereses", MontoIntereses);
                            cmd.Parameters.AddWithValue("@montoTotal", MontoTotal);
                            cmd.Parameters.AddWithValue("@fechaCreacion", DateTime.Now);
                            cmd.Parameters.AddWithValue("@estado", "Pendiente");
                            cmd.Parameters.AddWithValue("@ipCliente", Request.UserHostAddress);
                            
                            int transaccionId = Convert.ToInt32(cmd.ExecuteScalar());
                            
                            transaction.Commit();
                            return transaccionId;
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private string GenerarUrlPago(int transaccionId)
        {
            // Aquí integrarías con tu procesador de pagos preferido
            // Por ejemplo, MercadoPago, TodoPago, etc.
            
            // Por ahora, redirigimos a una página de confirmación
            return $"ConfirmarPago.aspx?transaccion={transaccionId}";
        }

        private void MostrarError(string mensaje)
        {
            divError.Visible = true;
            lblError.Text = mensaje;
            divPrincipal.Visible = false;
        }
    }
    
    #region Clases de Datos
    public class DatosCedulon
    {
        public string NumeroCedulon { get; set; }
        public decimal MontoOriginal { get; set; }
        public decimal MontoIntereses { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Descripcion { get; set; }
        public string CuitCuil { get; set; }
        public string NombreCompleto { get; set; }
        public bool EstaVencido { get; set; }
    }
    
    public class TarjetaCredito
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Logo { get; set; }
        public bool Activa { get; set; }
    }
    
    public class PlanCuotas
    {
        public int Id { get; set; }
        public int IdTarjeta { get; set; }
        public int CantidadCuotas { get; set; }
        public decimal CostoFinanciero { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        public bool AplicaDescuentoVencido { get; set; }
        public decimal MontoCuota { get; set; }
        public decimal MontoTotal { get; set; }
        public bool MostrarDescuento { get; set; }
    }
    #endregion
}