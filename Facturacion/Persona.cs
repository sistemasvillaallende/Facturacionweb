// Decompiled with JetBrains decompiler
// Type: Facturacion.Persona
// Assembly: Facturacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A9493007-1D65-4194-8547-961B9C83CD9E
// Assembly location: H:\NET 2018\Facturacion\bin\Facturacion.dll

using DAL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Facturacion
{
    public class Persona : DALBase
    {
        public string apellido { get; set; }

        public long CUIT { get; set; }

        public string tipoPersona { get; set; }

        public string nombre { get; set; }

        public string tipoDocumento { get; set; }

        public string numeroDocumento { get; set; }

        public string direccion { get; set; }

        public string localidad { get; set; }

        public string codPostal { get; set; }

        public int idProvincia { get; set; }

        public string telefono { get; set; }

        public string mail { get; set; }

        public DateTime Fecha_Nac { get; set; }

        public string sexo { get; set; }

        public static void insert(Persona obj)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("INSERT INTO PERSONAS_FACTU");
                stringBuilder.AppendLine("(tipoPersona, nombre, tipoDocumento, numeroDocumento, direccion,");
                stringBuilder.AppendLine("localidad, codPostal, idProvincia, CUIT, ");
                stringBuilder.AppendLine("telefono, mail, Fecha_Nac, Sexo, apellido)");
                stringBuilder.AppendLine("VALUES");
                stringBuilder.AppendLine("(@tipoPersona, @nombre, @tipoDocumento, @numeroDocumento, @direccion, ");
                stringBuilder.AppendLine("@localidad, @codPostal, @idProvincia, @CUIT,");
                stringBuilder.AppendLine("@telefono, @mail, @Fecha_Nac, @Sexo, @apellido)");
                using (SqlConnection connection = DALBase.getConnection())
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = stringBuilder.ToString();
                    command.Parameters.AddWithValue("@tipoPersona", (object)obj.tipoPersona);
                    command.Parameters.AddWithValue("@nombre", (object)obj.nombre);
                    command.Parameters.AddWithValue("@tipoDocumento", (object)obj.tipoDocumento);
                    command.Parameters.AddWithValue("@numeroDocumento", (object)obj.numeroDocumento);
                    command.Parameters.AddWithValue("@direccion", (object)obj.direccion);
                    command.Parameters.AddWithValue("@localidad", (object)obj.localidad);
                    command.Parameters.AddWithValue("@codPostal", (object)obj.codPostal);
                    command.Parameters.AddWithValue("@idProvincia", (object)obj.idProvincia);
                    command.Parameters.AddWithValue("@CUIT", (object)obj.CUIT);
                    command.Parameters.AddWithValue("@telefono", (object)obj.telefono);
                    command.Parameters.AddWithValue("@mail", (object)obj.mail);
                    command.Parameters.AddWithValue("@Fecha_Nac", (object)obj.Fecha_Nac);
                    command.Parameters.AddWithValue("@Sexo", (object)obj.sexo);
                    command.Parameters.AddWithValue("@apellido", (object)obj.apellido);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void update(
          string mail,
          string tel,
          long CUIT,
          string direccion,
          string localidad,
          string codPostal,
          int idProvincia)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("UPDATE PERSONAS_FACTU");
                stringBuilder.AppendLine("SET telefono = @telefono, mail = @mail, direccion = @direccion,");
                stringBuilder.AppendLine("localidad = @localidad, codPostal = @codPostal, idProvincia = @idProvincia");
                stringBuilder.AppendLine("WHERE CUIT = @CUIT");
                using (SqlConnection connection = DALBase.getConnection())
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = stringBuilder.ToString();
                    command.Parameters.AddWithValue("@CUIT", (object)CUIT);
                    command.Parameters.AddWithValue("@telefono", (object)tel);
                    command.Parameters.AddWithValue("@mail", (object)mail);
                    command.Parameters.AddWithValue("@direccion", (object)direccion);
                    command.Parameters.AddWithValue("@localidad", (object)localidad);
                    command.Parameters.AddWithValue("@codPostal", (object)codPostal);
                    command.Parameters.AddWithValue("@idProvincia", (object)idProvincia);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Persona getByPk(long CUIT)
        {
            try
            {
                Persona persona = (Persona)null;
                using (SqlConnection connection = DALBase.getConnection())
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT *FROM PERSONAS_FACTU WHERE CUIT = @CUIT";
                    command.Parameters.AddWithValue("@CUIT", (object)CUIT);
                    command.Connection.Open();
                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        int ordinal1 = sqlDataReader.GetOrdinal("tipoPersona");
                        int ordinal2 = sqlDataReader.GetOrdinal("nombre");
                        int ordinal3 = sqlDataReader.GetOrdinal("tipoDocumento");
                        int ordinal4 = sqlDataReader.GetOrdinal("numeroDocumento");
                        int ordinal5 = sqlDataReader.GetOrdinal("direccion");
                        int ordinal6 = sqlDataReader.GetOrdinal("localidad");
                        int ordinal7 = sqlDataReader.GetOrdinal("codPostal");
                        int ordinal8 = sqlDataReader.GetOrdinal("idProvincia");
                        int ordinal9 = sqlDataReader.GetOrdinal(nameof(CUIT));
                        int ordinal10 = sqlDataReader.GetOrdinal("telefono");
                        int ordinal11 = sqlDataReader.GetOrdinal("mail");
                        int ordinal12 = sqlDataReader.GetOrdinal("Fecha_Nac");
                        int ordinal13 = sqlDataReader.GetOrdinal("sexo");
                        int ordinal14 = sqlDataReader.GetOrdinal("apellido");
                        while (sqlDataReader.Read())
                        {
                            persona = new Persona();
                            if (!sqlDataReader.IsDBNull(ordinal1))
                                persona.tipoPersona = sqlDataReader.GetString(ordinal1);
                            if (!sqlDataReader.IsDBNull(ordinal2))
                                persona.nombre = sqlDataReader.GetString(ordinal2);
                            if (!sqlDataReader.IsDBNull(ordinal3))
                                persona.tipoDocumento = sqlDataReader.GetString(ordinal3);
                            if (!sqlDataReader.IsDBNull(ordinal4))
                                persona.numeroDocumento = sqlDataReader.GetString(ordinal4);
                            if (!sqlDataReader.IsDBNull(ordinal5))
                                persona.direccion = sqlDataReader.GetString(ordinal5);
                            if (!sqlDataReader.IsDBNull(ordinal6))
                                persona.localidad = sqlDataReader.GetString(ordinal6);
                            if (!sqlDataReader.IsDBNull(ordinal7))
                                persona.codPostal = sqlDataReader.GetString(ordinal7);
                            if (!sqlDataReader.IsDBNull(ordinal8))
                                persona.idProvincia = sqlDataReader.GetInt32(ordinal8);
                            if (!sqlDataReader.IsDBNull(ordinal9))
                                persona.CUIT = sqlDataReader.GetInt64(ordinal9);
                            if (!sqlDataReader.IsDBNull(ordinal10))
                                persona.telefono = sqlDataReader.GetString(ordinal10);
                            if (!sqlDataReader.IsDBNull(ordinal11))
                                persona.mail = sqlDataReader.GetString(ordinal11);
                            if (!sqlDataReader.IsDBNull(ordinal12))
                                persona.Fecha_Nac = sqlDataReader.GetDateTime(ordinal12);
                            if (!sqlDataReader.IsDBNull(ordinal13))
                                persona.sexo = sqlDataReader.GetString(ordinal13);
                            if (!sqlDataReader.IsDBNull(ordinal14))
                                persona.apellido = sqlDataReader.GetString(ordinal14);
                        }
                    }
                }
                return persona;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
