// Decompiled with JetBrains decompiler
// Type: DAL.Usuario
// Assembly: DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71BBCA04-FA76-4CA8-B211-6F77771DC570
// Assembly location: H:\NET 2018\Facturacion\bin\DAL.dll

using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
  public class Usuario : DALBase
  {
    public int COD_USUARIO { get; set; }

    public string NOMBRE { get; set; }

    public int LEGAJO { get; set; }

    public bool ADMINISTRADOR { get; set; }

    public string NOMBRE_COMPLETO { get; set; }

    public string PASSWD { get; set; }

    public string EMAIL { get; set; }

    public bool BAJA { get; set; }

    public int cod_oficina { get; set; }

    public string nombre_oficina { get; set; }

    public static Usuario ValidUser(string user, string password)
    {
      Usuario usuario = (Usuario) null;
      SqlConnection sqlConnection = (SqlConnection) null;
      StringBuilder stringBuilder = new StringBuilder();
      MD5Encryption md5Encryption = new MD5Encryption();
      user = user.Replace("'", "").Replace(",", "").Replace("=", "");
      stringBuilder.AppendLine("SELECT U.*, O.nombre_oficina From USUARIOS_V2 U INNER JOIN OFICINAS O ON U.COD_OFICINA = O.codigo_oficina WHERE nombre = @user");
      SqlCommand sqlCommand = new SqlCommand();
      sqlCommand.Parameters.Add(new SqlParameter("@user", (object) user));
      try
      {
        sqlConnection = DALBase.getConnection();
        sqlCommand.Connection = sqlConnection;
        sqlCommand.CommandType = CommandType.Text;
        sqlCommand.CommandText = stringBuilder.ToString();
        sqlCommand.Connection.Open();
        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        int ordinal1 = sqlDataReader.GetOrdinal("COD_USUARIO");
        int ordinal2 = sqlDataReader.GetOrdinal("NOMBRE");
        int ordinal3 = sqlDataReader.GetOrdinal("LEGAJO");
        int ordinal4 = sqlDataReader.GetOrdinal("ADMINISTRADOR");
        int ordinal5 = sqlDataReader.GetOrdinal("NOMBRE_COMPLETO");
        int ordinal6 = sqlDataReader.GetOrdinal("PASSWD");
        int ordinal7 = sqlDataReader.GetOrdinal("EMAIL");
        int ordinal8 = sqlDataReader.GetOrdinal("BAJA");
        int ordinal9 = sqlDataReader.GetOrdinal("COD_OFICINA");
        int ordinal10 = sqlDataReader.GetOrdinal("nombre_oficina");
        while (sqlDataReader.Read())
        {
          if (sqlDataReader.GetString(sqlDataReader.GetOrdinal("passwd")) == MD5Encryption.EncryptMD5(password.Trim().ToUpper() + user.Trim().ToUpper()))
          {
            usuario = new Usuario();
            if (!sqlDataReader.IsDBNull(ordinal4))
              usuario.ADMINISTRADOR = sqlDataReader.GetBoolean(ordinal4);
            if (!sqlDataReader.IsDBNull(ordinal8))
              usuario.BAJA = sqlDataReader.GetBoolean(ordinal8);
            if (!sqlDataReader.IsDBNull(ordinal1))
              usuario.COD_USUARIO = sqlDataReader.GetInt32(ordinal1);
            if (!sqlDataReader.IsDBNull(ordinal7))
              usuario.EMAIL = sqlDataReader.GetString(ordinal7);
            if (!sqlDataReader.IsDBNull(ordinal3))
              usuario.LEGAJO = sqlDataReader.GetInt32(ordinal3);
            if (!sqlDataReader.IsDBNull(ordinal2))
              usuario.NOMBRE = sqlDataReader.GetString(ordinal2);
            if (!sqlDataReader.IsDBNull(ordinal5))
              usuario.NOMBRE_COMPLETO = sqlDataReader.GetString(ordinal5);
            if (!sqlDataReader.IsDBNull(ordinal6))
              usuario.PASSWD = sqlDataReader.GetString(ordinal6);
            if (!sqlDataReader.IsDBNull(ordinal9))
              usuario.cod_oficina = sqlDataReader.GetInt32(ordinal9);
            if (!sqlDataReader.IsDBNull(ordinal10))
              usuario.nombre_oficina = sqlDataReader.GetString(ordinal10);
          }
        }
        return usuario;
      }
      catch (Exception ex)
      {
         throw new Exception(ex.Message + " Error en la Autenticación!!!.");
      }
      finally
      {
        sqlConnection.Close();
      }
    }

    public static Usuario getByPk(int cod_usuario)
    {
      SqlConnection sqlConnection = (SqlConnection) null;
      StringBuilder stringBuilder = new StringBuilder();
      Usuario usuario = (Usuario) null;
      stringBuilder.AppendLine("SELECT U.*, O.nombre_oficina From USUARIOS_V2 U INNER JOIN OFICINAS O ON U.COD_OFICINA = O.codigo_oficina WHERE COD_USUARIO = @COD_USUARIO");
      SqlCommand sqlCommand = new SqlCommand();
      sqlCommand.Parameters.Add(new SqlParameter("@COD_USUARIO", (object) cod_usuario));
      try
      {
        sqlConnection = DALBase.getConnection();
        sqlCommand.Connection = sqlConnection;
        sqlCommand.CommandType = CommandType.Text;
        sqlCommand.CommandText = stringBuilder.ToString();
        sqlCommand.Connection.Open();
        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        int ordinal1 = sqlDataReader.GetOrdinal("COD_USUARIO");
        int ordinal2 = sqlDataReader.GetOrdinal("NOMBRE");
        int ordinal3 = sqlDataReader.GetOrdinal("LEGAJO");
        int ordinal4 = sqlDataReader.GetOrdinal("ADMINISTRADOR");
        int ordinal5 = sqlDataReader.GetOrdinal("NOMBRE_COMPLETO");
        int ordinal6 = sqlDataReader.GetOrdinal("PASSWD");
        int ordinal7 = sqlDataReader.GetOrdinal("EMAIL");
        int ordinal8 = sqlDataReader.GetOrdinal("BAJA");
        int ordinal9 = sqlDataReader.GetOrdinal("COD_OFICINA");
        int ordinal10 = sqlDataReader.GetOrdinal("nombre_oficina");
        while (sqlDataReader.Read())
        {
          usuario = new Usuario();
          if (!sqlDataReader.IsDBNull(ordinal4))
            usuario.ADMINISTRADOR = sqlDataReader.GetBoolean(ordinal4);
          if (!sqlDataReader.IsDBNull(ordinal8))
            usuario.BAJA = sqlDataReader.GetBoolean(ordinal8);
          if (!sqlDataReader.IsDBNull(ordinal1))
            usuario.COD_USUARIO = sqlDataReader.GetInt32(ordinal1);
          if (!sqlDataReader.IsDBNull(ordinal7))
            usuario.EMAIL = sqlDataReader.GetString(ordinal7);
          if (!sqlDataReader.IsDBNull(ordinal3))
            usuario.LEGAJO = sqlDataReader.GetInt32(ordinal3);
          if (!sqlDataReader.IsDBNull(ordinal2))
            usuario.NOMBRE = sqlDataReader.GetString(ordinal2);
          if (!sqlDataReader.IsDBNull(ordinal5))
            usuario.NOMBRE_COMPLETO = sqlDataReader.GetString(ordinal5);
          if (!sqlDataReader.IsDBNull(ordinal6))
            usuario.PASSWD = sqlDataReader.GetString(ordinal6);
          if (!sqlDataReader.IsDBNull(ordinal9))
            usuario.cod_oficina = sqlDataReader.GetInt32(ordinal9);
          if (!sqlDataReader.IsDBNull(ordinal10))
            usuario.nombre_oficina = sqlDataReader.GetString(ordinal10);
        }
        return usuario;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message + " Error en la Autenticación!!!.");
      }
      finally
      {
        sqlConnection.Close();
      }
    }
  }
}
