//using Facturacion.WSAfip;
using Facturacion.WSAFIP2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Facturacion.Secure
{
    public partial class Facturacion : System.Web.UI.Page
    {

        WSAFIPSoapClient ws = new WSAFIPSoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (this.Session["USER"] == null)
                    this.Response.Redirect("../LogIn.aspx");
                if (this.IsPostBack)
                    return;
                DAL.Usuario usuario = (DAL.Usuario)this.Session["user"];

                this.DDLCatDeuda.DataValueField = "cod_categoria";
                this.DDLCatDeuda.DataTextField = "des_categoria";
                this.DDLCatDeuda.DataSource = (object)BLL.CATEGORIA_DEUDA.getByOficina(usuario.cod_oficina);
                this.DDLCatDeuda.DataBind();
                this.fillFacturas();
            }
            catch (Exception ex)
            {
                this.divError.Visible = true;
                this.msjError.InnerText = ex.Message;
            }
        }

        protected void fillFacturas()
        {
            try
            {
                this.gvFacturas.DataSource = (object)BLL.Facturacion.getByOficina(((DAL.Usuario)this.Session["user"]).cod_oficina);
                this.gvFacturas.DataBind();
            }
            catch (Exception ex)
            {
                this.divError.Visible = true;
                this.msjError.InnerText = ex.Message;
            }
        }

        protected void btnAddFactu_ServerClick(object sender, EventArgs e)
        {
            this.clear();
            this.divBuscar.Visible = false;
            this.divNuevo.Visible = true;
        }

        protected void btnAfip_ServerClick(object sender, EventArgs e)
        {
            fillPersona(Convert.ToInt64(this.txtCUIT.Text));
        }
        private void fillPersona(Int64 cuit)
        {
            try
            {
                this.clear();
                try
                {

                    //personaReturn person = new personaReturn();

                    personaReturn persona1 =  ws.getPersona(
                        "10.0.0.9:3128", "mvelez", "generallee", 
                        cuit);
                    this.txtApellido.Value = persona1.persona.apellido;
                    this.txtNombre.Value = persona1.persona.nombre;
                    if (persona1.persona.domicilio != null)
                    {
                        if (persona1.persona.domicilio[0].codPostal != null)
                            this.txtMModalCP.Text = persona1.persona.domicilio[0].codPostal;
                        if (persona1.persona.domicilio[0].direccion != null)
                            this.txtModalDir.Text = persona1.persona.domicilio[0].direccion;
                        if (persona1.persona.domicilio[0].localidad != null)
                            this.txtModalLocalidad.Text = persona1.persona.domicilio[0].localidad;
                        int idProvincia = persona1.persona.domicilio[0].idProvincia;
                        this.DDLModalProvincia.SelectedValue = persona1.persona.domicilio[0].idProvincia.ToString();
                    }
                    if (persona1.persona.tipoDocumento != null)
                        this.txtTipoDoc.Value = persona1.persona.tipoDocumento;
                    if (persona1.persona.numeroDocumento != null)
                        this.txtDoc.Value = persona1.persona.numeroDocumento;
                    DateTime fechaNacimiento = persona1.persona.fechaNacimiento;
                    this.txtFecNac.Value = persona1.persona.fechaNacimiento.ToShortDateString();
                    if (persona1.persona.sexo != null)
                        this.txtSexo.Value = persona1.persona.sexo;
                    Persona persona2 = Persona.getByPk(Convert.ToInt64(this.txtCUIT.Text));
                    if (persona2 != null)
                    {
                        this.txtApellido.Value = persona2.apellido;
                        this.txtDoc.Value = persona2.numeroDocumento;
                        this.txtFecNac.Value = persona2.Fecha_Nac.ToShortDateString();
                        this.txtNombre.Value = persona2.nombre;
                        this.txtTipoDoc.Value = persona2.tipoDocumento;
                        this.txtSexo.Value = persona2.sexo;
                        this.txtMail.Text = persona2.mail;
                        this.txtCel.Text = persona2.telefono;
                        this.txtMModalCP.Text = persona2.codPostal;
                        this.txtModalDir.Text = persona2.direccion;
                        this.txtModalLocalidad.Text = persona2.localidad;
                        this.DDLModalProvincia.SelectedValue = persona2.idProvincia.ToString();
                    }
                    else
                    {
                        persona2 = new Persona();
                        persona2.apellido = persona1.persona.apellido;
                        if (persona1.persona.domicilio != null)
                        {
                            if (persona1.persona.domicilio[0].codPostal != null)
                                this.txtMModalCP.Text = persona1.persona.domicilio[0].codPostal;
                            if (persona1.persona.domicilio[0].direccion != null)
                                this.txtModalDir.Text = persona1.persona.domicilio[0].direccion;
                            if (persona1.persona.domicilio[0].localidad != null)
                                this.txtModalLocalidad.Text = persona1.persona.domicilio[0].localidad;
                            int idProvincia = persona1.persona.domicilio[0].idProvincia;
                            this.DDLModalProvincia.SelectedValue = persona1.persona.domicilio[0].idProvincia.ToString();
                        }
                        persona2.apellido = persona1.persona.apellido;
                        persona2.nombre = persona1.persona.nombre;
                        persona2.numeroDocumento = persona1.persona.numeroDocumento;
                        persona2.sexo = persona1.persona.sexo;
                        persona2.tipoDocumento = persona1.persona.tipoDocumento;
                        persona2.tipoPersona = persona1.persona.tipoPersona;
                    }
                    this.Session["PERSONA"] = (object)persona2;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                this.divError.Visible = true;
                this.msjError.InnerText = ex.Message;
            }
        }
        private void clear()
        {
            this.txtCel.Text = string.Empty;
            this.txtDoc.Value = string.Empty;
            this.txtMail.Text = string.Empty;
            this.txtMonto.Text = string.Empty;
            this.txtNombre.Value = string.Empty;
            this.txtObs.Text = string.Empty;
            this.msjError.InnerText = string.Empty;
            this.divError.Visible = false;
            this.txtMModalCP.Text = string.Empty;
            this.txtModalDir.Text = string.Empty;
            this.txtModalLocalidad.Text = string.Empty;
            this.DDLModalProvincia.SelectedIndex = 0;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            this.clear();
            this.divBuscar.Visible = true;
            this.divNuevo.Visible = false;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                DAL.Usuario usuario = (DAL.Usuario)this.Session["user"];
                DAL.Facturacion facturacion = new DAL.Facturacion();
                Persona byPk = Persona.getByPk(Convert.ToInt64(this.txtCUIT.Text));
                
                if (byPk == null)
                {

                    //byPk = (Persona)this.Session["PERSONA"];
                    byPk = new Persona();
                    byPk.apellido = this.txtApellido.Value;
                    byPk.nombre = this.txtNombre.Value;
                    byPk.tipoDocumento = this.txtTipoDoc.Value;
                    byPk.numeroDocumento = this.txtDoc.Value;
                    byPk.CUIT = long.Parse(this.txtCUIT.Text);
                    byPk.telefono = this.txtCel.Text;
                    byPk.mail = this.txtMail.Text;
                    byPk.codPostal = this.txtMModalCP.Text;
                    byPk.direccion = this.txtModalDir.Text;
                    byPk.idProvincia = int.Parse(this.DDLModalProvincia.SelectedItem.Value);
                    byPk.localidad = this.txtModalLocalidad.Text;
                    byPk.Fecha_Nac = Convert.ToDateTime(this.txtFecNac.Value);
                    byPk.sexo = this.txtSexo.Value;
                    Persona.insert(byPk);
                }
                else
                    Persona.update(this.txtMail.Text, this.txtCel.Text, long.Parse(this.txtCUIT.Text), this.txtModalDir.Text, this.txtModalLocalidad.Text, this.txtMModalCP.Text, int.Parse(this.DDLModalProvincia.SelectedItem.Value));
                long num = BLL.Facturacion.insertFacturacion(Convert.ToDecimal(this.txtMonto.Text), Convert.ToInt32(this.DDLCatDeuda.SelectedItem.Value), this.txtNombre.Value, this.txtObs.Text, usuario.NOMBRE, usuario.COD_USUARIO, usuario.cod_oficina, byPk.CUIT);
                this.clear();
                this.divBuscar.Visible = true;
                this.divNuevo.Visible = false;
                this.fillFacturas();
                this.Response.Redirect(string.Format("../Reportes/Facturas.aspx?nroTran={0}", (object)num));
            }
            catch (Exception ex)
            {
                this.divError.Visible = true;
                this.msjError.InnerText = ex.Message;
            }
        }

        protected void gvFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            DAL.Facturacion dataItem = (DAL.Facturacion)e.Row.DataItem;
            Image control1 = (Image)e.Row.FindControl("imgPagado");
            ImageButton control2 = (ImageButton)e.Row.FindControl("imgImprimir");
            ImageButton control3 = (ImageButton)e.Row.FindControl("imgDelete");
            if (dataItem.pagado)
            {
                control1.ImageUrl = "../App_Themes/dist/img/Si.png";
                control3.Visible = false;
            }
            else
            {
                control1.ImageUrl = "../App_Themes/dist/img/No.png";
                control3.Visible = true;
            }
            if (Convert.ToDateTime(dataItem.vencimiento.ToShortDateString()) >= Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                control2.Visible = true;
            else
                control2.Visible = false;
            ((HtmlContainerControl)e.Row.FindControl("divObs")).InnerHtml = dataItem.observaciones;
        }

        protected void gvFacturas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToString() == "btnPrint")
                this.Response.Redirect(string.Format("../Reportes/Facturas.aspx?nroTran={0}", e.CommandArgument));
            if (!(e.CommandName.ToString() == "btnDelete"))
                return;
            BLL.Facturacion.delete(Convert.ToInt32(e.CommandArgument));
            this.fillFacturas();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (CUIT cuit1 in Utils.getDominio())
                {
                    WebClient webClient = new WebClient();
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://soa.afip.gob.ar/sr-padron/v2/personas/" + cuit1.dni.Trim());
                    httpWebRequest.Method = "GET";
                    httpWebRequest.ContentLength = 0L;
                    WebProxy webProxy = new WebProxy("10.0.0.9", 3128);
                    webClient.Proxy.Credentials = (ICredentials)new NetworkCredential("mvelez", "generallee");
                    httpWebRequest.Proxy = (IWebProxy)webProxy;
                    httpWebRequest.Credentials = (ICredentials)CredentialCache.DefaultNetworkCredentials;
                    string end = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.GetEncoding(0)).ReadToEnd();
                    if (!end.Contains("error"))
                    {
                        CUIT cuit2 = JsonConvert.DeserializeObject<CUIT>(end);
                        if (cuit2.data.Count == 1)
                        {
                            Utils.update(cuit1.dni, cuit2.data[0].ToString());
                        }
                        else
                        {
                            string empty = string.Empty;
                            foreach (long num in (IEnumerable<long>)cuit2.data)
                            {
                                empty += num.ToString();
                                empty += " - ";
                            }
                            empty.Trim();
                            empty.TrimEnd('-');
                            Utils.update(cuit1.dni, empty);
                        }
                    }
                    else
                    {
                        NoExiste noExiste = JsonConvert.DeserializeObject<NoExiste>(end);
                        Utils.update(cuit1.dni, noExiste.error.mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                this.divError.Visible = true;
                this.msjError.InnerText = ex.Message;
            }
        }

        protected void gvFacturas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvFacturas.PageIndex = e.NewPageIndex;
            this.fillFacturas();
        }
    }
}