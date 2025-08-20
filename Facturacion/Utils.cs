// Decompiled with JetBrains decompiler
// Type: Facturacion.Utils
// Assembly: Facturacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A9493007-1D65-4194-8547-961B9C83CD9E
// Assembly location: H:\NET 2018\Facturacion\bin\Facturacion.dll

using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Facturacion
{
  public class Utils : DALBase
  {
    public static string getProvincia(int cod)
    {
      string str = string.Empty;
      switch (cod)
      {
        case 0:
          str = "Ciudad Autónoma de Buenos Aires";
          break;
        case 1:
          str = "Buenos Aires";
          break;
        case 2:
          str = "Catamara";
          break;
        case 3:
          str = "Córdoba";
          break;
        case 4:
          str = "Corrientes";
          break;
        case 5:
          str = "Entre Ríos";
          break;
        case 6:
          str = "Jujuy";
          break;
        case 7:
          str = "Mendoza";
          break;
        case 8:
          str = "La Rioja";
          break;
        case 9:
          str = "Salta";
          break;
        case 10:
          str = "San Juan";
          break;
        case 11:
          str = "San Luis";
          break;
        case 12:
          str = "Santa Fe";
          break;
        case 13:
          str = "Santiago del Estero";
          break;
        case 14:
          str = "Tucumán";
          break;
        case 16:
          str = "Chaco";
          break;
        case 17:
          str = "Chubut";
          break;
        case 18:
          str = "Formosa";
          break;
        case 19:
          str = "Misiones";
          break;
        case 20:
          str = "Neuquén";
          break;
        case 21:
          str = "La Pampa";
          break;
        case 22:
          str = "Río Negro";
          break;
        case 23:
          str = "Santa Cruz";
          break;
        case 24:
          str = "Tierra del Fuego";
          break;
      }
      return str;
    }

    public static List<CUIT> getDominio()
    {
      try
      {
        List<CUIT> cuitList = new List<CUIT>();
        new StringBuilder().AppendLine("select distinct top 1000 Nro_documento from EXPEDIENTE where Nombre is not null and Nombre <> '0'");
        using (SqlConnection connectionMesa = DALBase.getConnectionMesa())
        {
          SqlCommand command = connectionMesa.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = "select distinct top 1000 Nro_documento from EXPEDIENTE where Nombre is not null and Nombre <> '0' and Nro_documento <> 0 and cuit is null";
          command.Connection.Open();
          SqlDataReader sqlDataReader = command.ExecuteReader();
          if (sqlDataReader.HasRows)
          {
            while (sqlDataReader.Read())
              cuitList.Add(new CUIT()
              {
                dni = sqlDataReader.GetInt32(0).ToString()
              });
          }
        }
        return cuitList;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void update(string Nro_documento, string cuit)
    {
      try
      {
        List<CUIT> cuitList = new List<CUIT>();
        using (SqlConnection connectionMesa = DALBase.getConnectionMesa())
        {
          SqlCommand command = connectionMesa.CreateCommand();
          command.CommandType = CommandType.Text;
          command.CommandText = "update EXPEDIENTE SET CUIT = @CUIT WHERE Nro_documento = @Nro_documento";
          command.Parameters.AddWithValue("@CUIT", (object) cuit);
          command.Parameters.AddWithValue("@Nro_documento", (object) Nro_documento);
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
