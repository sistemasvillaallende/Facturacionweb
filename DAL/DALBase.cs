// Decompiled with JetBrains decompiler
// Type: DAL.DALBase
// Assembly: DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71BBCA04-FA76-4CA8-B211-6F77771DC570
// Assembly location: H:\NET 2018\Facturacion\bin\DAL.dll

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
  public class DALBase
  {
    public static SqlConnection getConnection()
    {
      try
      {
        return new SqlConnection(ConfigurationManager.ConnectionStrings["DBMainFactu"].ConnectionString);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static SqlConnection getConnectionMesa()
    {
      try
      {
        return new SqlConnection(ConfigurationManager.ConnectionStrings["DBMesaFactu"].ConnectionString);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static long NewID(string tableName, string campo)
    {
      long num = 0;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("SELECT MAX(nro_cedulon) As Mayor");
      stringBuilder.AppendLine("FROM " + tableName);
      SqlCommand sqlCommand = new SqlCommand();
      SqlConnection sqlConnection = (SqlConnection) null;
      sqlCommand.Parameters.Add(new SqlParameter("@campo", (object) campo));
      try
      {
        sqlConnection = DALBase.getConnection();
        sqlCommand.Connection = sqlConnection;
        sqlCommand.CommandType = CommandType.Text;
        sqlCommand.CommandText = stringBuilder.ToString();
        sqlCommand.Connection.Open();
        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        while (sqlDataReader.Read())
          num = (long) (sqlDataReader.GetInt32(0) + 1);
        return num;
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error, no se pudo obtener el prox. código, " + ex.Message);
        throw ex;
      }
      finally
      {
        sqlConnection.Close();
      }
    }
  }
}
