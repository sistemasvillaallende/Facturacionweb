// Decompiled with JetBrains decompiler
// Type: DAL.Cedulones
// Assembly: DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71BBCA04-FA76-4CA8-B211-6F77771DC570
// Assembly location: H:\NET 2018\Facturacion\bin\DAL.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class Cedulones : DALBase
    {
        public List<DetalleDeuda> lstDeuda;

        public long nro_cedulon { get; set; }

        public string fecha_emision { get; set; }

        public int subsistema { get; set; }

        public int tipo_cedulon { get; set; }

        public bool no_pagado { get; set; }

        public bool activo { get; set; }

        public int nro_emision { get; set; }

        public int circunscripcion { get; set; }

        public int seccion { get; set; }

        public int manzana { get; set; }

        public int parcela { get; set; }

        public int p_h { get; set; }

        public string periodo { get; set; }

        public string vencimiento_1 { get; set; }

        public Decimal monto_1 { get; set; }

        public string vencimiento_2 { get; set; }

        public Decimal monto_2 { get; set; }

        public Decimal contado { get; set; }

        public Decimal cheques { get; set; }

        public Decimal monto_arreglo { get; set; }

        public string nro_decreto { get; set; }

        public string nro_dom_esp { get; set; }

        public string piso_dpto_esp { get; set; }

        public string local_esp { get; set; }

        public string nom_calle_dom_esp { get; set; }

        public string nom_barrio_dom_esp { get; set; }

        public string ciudad_dom_esp { get; set; }

        public string provincia_dom_esp { get; set; }

        public string pais_dom_esp { get; set; }

        public string codigo_postal_dom_esp { get; set; }

        public int nro_badec { get; set; }

        public int nro_contrib { get; set; }

        public string nom_badec { get; set; }

        public string nom_calle_pf { get; set; }

        public string nro_dom_pf { get; set; }

        public string dominio { get; set; }

        public int legajo { get; set; }

        public bool imprime { get; set; }

        public string tipo_cem { get; set; }

        public int manzana_cem { get; set; }

        public int lote_cem { get; set; }

        public int parcela_cem { get; set; }

        public int nivel_cem { get; set; }

        public bool mNewRecord { get; set; }

        public Cedulones()
        {
            this.nro_cedulon = 0L;
            this.fecha_emision = "";
            this.subsistema = 0;
            this.tipo_cedulon = 0;
            this.no_pagado = false;
            this.activo = false;
            this.nro_emision = 0;
            this.circunscripcion = 0;
            this.seccion = 0;
            this.manzana = 0;
            this.parcela = 0;
            this.p_h = 0;
            this.periodo = "";
            this.vencimiento_1 = "";
            this.monto_1 = Decimal.Zero;
            this.vencimiento_2 = "";
            this.monto_2 = Decimal.Zero;
            this.contado = Decimal.Zero;
            this.cheques = Decimal.Zero;
            this.monto_arreglo = Decimal.Zero;
            this.nro_decreto = "";
            this.nro_dom_esp = "";
            this.piso_dpto_esp = "";
            this.local_esp = "";
            this.nom_calle_dom_esp = "";
            this.nom_barrio_dom_esp = "";
            this.ciudad_dom_esp = "";
            this.provincia_dom_esp = "";
            this.pais_dom_esp = "";
            this.codigo_postal_dom_esp = "";
            this.nro_badec = 0;
            this.nro_contrib = 0;
            this.nom_badec = "";
            this.nom_calle_pf = "";
            this.nro_dom_pf = "";
            this.dominio = "";
            this.legajo = 0;
            this.imprime = false;
            this.tipo_cem = "";
            this.manzana_cem = 0;
            this.lote_cem = 0;
            this.parcela_cem = 0;
            this.nivel_cem = 0;
            this.mNewRecord = false;
            this.lstDeuda = new List<DetalleDeuda>();
        }

        public static long insertFacturacion(
          Cedulones oCedulon,
          int nro_pro,
          int nro_transaccion,
          int categoria_deuda)
        {
            SqlCommand sqlCommand = new SqlCommand();
            SqlConnection sqlConnection = (SqlConnection)null;
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                sqlConnection = DALBase.getConnection();
                sqlCommand.Connection = sqlConnection;
                oCedulon.nro_cedulon = DALBase.NewID("Cedulones2", "nro_cedulon");
                stringBuilder.AppendLine("INSERT INTO Cedulones2(");
                stringBuilder.AppendLine("nro_cedulon,");
                stringBuilder.AppendLine("fecha_emision,");
                stringBuilder.AppendLine("subsistema,");
                stringBuilder.AppendLine("tipo_cedulon,");
                stringBuilder.AppendLine("no_pagado,");
                stringBuilder.AppendLine("activo,");
                stringBuilder.AppendLine("nro_badec,");
                stringBuilder.AppendLine("circunscripcion,");
                stringBuilder.AppendLine("seccion,");
                stringBuilder.AppendLine("manzana,");
                stringBuilder.AppendLine("parcela,");
                stringBuilder.AppendLine("p_h,");
                stringBuilder.AppendLine("dominio,");
                stringBuilder.AppendLine("legajo,");
                stringBuilder.AppendLine("vencimiento_2,");
                stringBuilder.AppendLine("monto_2,");
                stringBuilder.AppendLine("contado,");
                stringBuilder.AppendLine("cheques,");
                stringBuilder.AppendLine("monto_arreglo)");
                stringBuilder.AppendLine("VALUES(");
                stringBuilder.AppendLine("@nro_cedulon,");
                stringBuilder.AppendLine("@fecha_emision,");
                stringBuilder.AppendLine("@subsistema,");
                stringBuilder.AppendLine("@tipo_cedulon,");
                stringBuilder.AppendLine("@no_pagado,");
                stringBuilder.AppendLine("@activo,");
                stringBuilder.AppendLine("@nro_badec,");
                stringBuilder.AppendLine("@circunscripcion,");
                stringBuilder.AppendLine("@seccion,");
                stringBuilder.AppendLine("@manzana,");
                stringBuilder.AppendLine("@parcela,");
                stringBuilder.AppendLine("@p_h,");
                stringBuilder.AppendLine("@dominio,");
                stringBuilder.AppendLine("@legajo,");
                stringBuilder.AppendLine("@vencimiento_2,");
                stringBuilder.AppendLine("@monto_2,");
                stringBuilder.AppendLine("@contado,");
                stringBuilder.AppendLine("@cheques,");
                stringBuilder.AppendLine("@monto_arreglo)");
                sqlCommand.Parameters.Add(new SqlParameter("@nro_cedulon", (object)oCedulon.nro_cedulon));
                sqlCommand.Parameters.Add(new SqlParameter("@fecha_emision", (object)oCedulon.fecha_emision));
                sqlCommand.Parameters.Add(new SqlParameter("@subsistema", (object)oCedulon.subsistema));
                sqlCommand.Parameters.Add(new SqlParameter("@tipo_cedulon", (object)oCedulon.tipo_cedulon));
                sqlCommand.Parameters.Add(new SqlParameter("@no_pagado", (object)oCedulon.no_pagado));
                sqlCommand.Parameters.Add(new SqlParameter("@activo", (object)oCedulon.activo));
                sqlCommand.Parameters.Add(new SqlParameter("@nro_badec", (object)oCedulon.nro_badec));
                sqlCommand.Parameters.Add(new SqlParameter("@circunscripcion", (object)oCedulon.circunscripcion));
                sqlCommand.Parameters.Add(new SqlParameter("@seccion", (object)oCedulon.seccion));
                sqlCommand.Parameters.Add(new SqlParameter("@manzana", (object)oCedulon.manzana));
                sqlCommand.Parameters.Add(new SqlParameter("@parcela", (object)oCedulon.parcela));
                sqlCommand.Parameters.Add(new SqlParameter("@p_h", (object)oCedulon.p_h));
                sqlCommand.Parameters.Add(new SqlParameter("@dominio", (object)oCedulon.dominio));
                sqlCommand.Parameters.Add(new SqlParameter("@legajo", (object)oCedulon.legajo));
                sqlCommand.Parameters.Add(new SqlParameter("@vencimiento_2", (object)oCedulon.vencimiento_2));
                sqlCommand.Parameters.Add(new SqlParameter("@monto_2", (object)oCedulon.monto_2));
                sqlCommand.Parameters.Add(new SqlParameter("@contado", (object)oCedulon.contado));
                sqlCommand.Parameters.Add(new SqlParameter("@cheques", (object)oCedulon.cheques));
                sqlCommand.Parameters.Add(new SqlParameter("@monto_arreglo", (object)oCedulon.monto_arreglo));
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = stringBuilder.ToString();
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Parameters.Clear();
                stringBuilder.Clear();
                stringBuilder.AppendLine("INSERT INTO Deudas_x_cedulon3(");
                stringBuilder.AppendLine("nro_cedulon,");
                stringBuilder.AppendLine("nro_transaccion,");
                stringBuilder.AppendLine("monto_pagado,");
                stringBuilder.AppendLine("descuento_anticipo,");
                stringBuilder.AppendLine("vencimiento_transaccion,");
                stringBuilder.AppendLine("pago_parcial,");
                stringBuilder.AppendLine("categoria_deuda)");
                stringBuilder.AppendLine("VALUES(");
                stringBuilder.AppendLine("@nro_cedulon,");
                stringBuilder.AppendLine("@nro_transaccion,");
                stringBuilder.AppendLine("@monto_pagado,");
                stringBuilder.AppendLine("@descuento_anticipo,");
                stringBuilder.AppendLine("@vencimiento_transaccion,");
                stringBuilder.AppendLine("@pago_parcial,");
                stringBuilder.AppendLine("@categoria_deuda)");
                sqlCommand.CommandText = stringBuilder.ToString();
                sqlCommand.Parameters.AddWithValue("nro_cedulon", (object)oCedulon.nro_cedulon);
                sqlCommand.Parameters.AddWithValue(nameof(nro_transaccion), (object)nro_transaccion);
                sqlCommand.Parameters.AddWithValue("monto_pagado", (object)oCedulon.monto_2);
                sqlCommand.Parameters.AddWithValue("descuento_anticipo", (object)0);
                sqlCommand.Parameters.AddWithValue("vencimiento_transaccion", (object)DateTime.Now);
                sqlCommand.Parameters.AddWithValue("pago_parcial", (object)false);
                sqlCommand.Parameters.AddWithValue(nameof(categoria_deuda), (object)categoria_deuda);
                sqlCommand.ExecuteNonQuery();
                return oCedulon.nro_cedulon;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in query!" + ex.ToString());
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
