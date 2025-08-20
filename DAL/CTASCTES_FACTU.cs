// Decompiled with JetBrains decompiler
// Type: DAL.CTASCTES_FACTU
// Assembly: DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71BBCA04-FA76-4CA8-B211-6F77771DC570
// Assembly location: H:\NET 2018\Facturacion\bin\DAL.dll

using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
  public class CTASCTES_FACTU : DALBase
  {
    public int tipo_transaccion { get; set; }

    public int nro_transaccion { get; set; }

    public int nro_pago_parcial { get; set; }

    public DateTime fecha_transaccion { get; set; }

    public string periodo { get; set; }

    public Decimal monto_original { get; set; }

    public int nro_plan { get; set; }

    public bool pagado { get; set; }

    public Decimal debe { get; set; }

    public Decimal haber { get; set; }

    public bool pago_parcial { get; set; }

    public int categoria_deuda { get; set; }

    public int nro_procuracion { get; set; }

    public DateTime vencimiento { get; set; }

    public int nro_cedulon { get; set; }

    public bool exigible { get; set; }

    public Decimal monto_pagado { get; set; }

    public CTASCTES_FACTU()
    {
      this.tipo_transaccion = 0;
      this.nro_transaccion = 0;
      this.nro_pago_parcial = 0;
      this.fecha_transaccion = DateTime.Now;
      this.periodo = string.Empty;
      this.monto_original = Decimal.Zero;
      this.nro_plan = 0;
      this.pagado = false;
      this.debe = Decimal.Zero;
      this.haber = Decimal.Zero;
      this.pago_parcial = false;
      this.categoria_deuda = 0;
      this.nro_procuracion = 0;
      this.vencimiento = DateTime.Now;
      this.nro_cedulon = 0;
      this.exigible = false;
      this.monto_pagado = Decimal.Zero;
    }

    public static void insert(CTASCTES_FACTU obj)
    {
      try
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("INSERT INTO CTASCTES_FACTU");
        stringBuilder.AppendLine("(tipo_transaccion,nro_transaccion,fecha_transaccion,periodo,");
        stringBuilder.AppendLine("monto_original,pagado,debe,haber,pago_parcial,categoria_deuda,");
        stringBuilder.AppendLine("vencimiento,nro_cedulon,exigible,monto_pagado,nro_pago_parcial)");
        stringBuilder.AppendLine("VALUES");
        stringBuilder.AppendLine("(@tipo_transaccion,@nro_transaccion,@fecha_transaccion,@periodo, ");
        stringBuilder.AppendLine("@monto_original,@pagado,@debe,@haber,@pago_parcial,@categoria_deuda,");
        stringBuilder.AppendLine("@vencimiento, @nro_cedulon, @exigible, @monto_pagado,0)");
        using (SqlConnection connection = DALBase.getConnection())
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = stringBuilder.ToString();
          command.Parameters.AddWithValue("@tipo_transaccion", (object) obj.tipo_transaccion);
          command.Parameters.AddWithValue("@nro_transaccion", (object) obj.nro_transaccion);
          command.Parameters.AddWithValue("@fecha_transaccion", (object) obj.fecha_transaccion);
          command.Parameters.AddWithValue("@periodo", (object) obj.periodo);
          command.Parameters.AddWithValue("@monto_original", (object) obj.monto_original);
          command.Parameters.AddWithValue("@pagado", (object) obj.pagado);
          command.Parameters.AddWithValue("@debe", (object) obj.debe);
          command.Parameters.AddWithValue("@haber", (object) obj.haber);
          command.Parameters.AddWithValue("@pago_parcial", (object) obj.pago_parcial);
          command.Parameters.AddWithValue("@categoria_deuda", (object) obj.categoria_deuda);
          command.Parameters.AddWithValue("@vencimiento", (object) obj.vencimiento);
          command.Parameters.AddWithValue("@nro_cedulon", (object) obj.nro_cedulon);
          command.Parameters.AddWithValue("@exigible", (object) obj.exigible);
          command.Parameters.AddWithValue("@monto_pagado", (object) obj.monto_pagado);
          command.Connection.Open();
          command.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void delete(int nro_tran)
    {
      try
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("DELETE CTASCTES_FACTU");
        stringBuilder.AppendLine("WHERE");
        stringBuilder.AppendLine("nro_transaccion = @nro_tran AND tipo_transaccion = 1 AND nro_pago_parcial = 0");
        using (SqlConnection connection = DALBase.getConnection())
        {
          SqlCommand command = connection.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = stringBuilder.ToString();
          command.Parameters.AddWithValue("@nro_tran", (object) nro_tran);
          command.Connection.Open();
          command.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
