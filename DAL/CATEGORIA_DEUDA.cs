// Decompiled with JetBrains decompiler
// Type: DAL.CATEGORIA_DEUDA
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
    public class CATEGORIA_DEUDA : DALBase
    {
        public int cod_categoria { get; set; }

        public string des_categoria { get; set; }

        public CATEGORIA_DEUDA()
        {
            this.cod_categoria = 0;
            this.des_categoria = string.Empty;
        }

        public static List<CATEGORIA_DEUDA> getByOficina(int cod)
        {
            try
            {
                List<CATEGORIA_DEUDA> categoriaDeudaList = new List<CATEGORIA_DEUDA>();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("select c.cod_categoria, c.des_categoria");
                stringBuilder.AppendLine("from CATE_DEUDA_FACTU_x_OFICINA cf");
                stringBuilder.AppendLine("inner join CATE_DEUDA_FACTU C ON cf.cod_categoria = c.cod_categoria");
                stringBuilder.AppendLine("inner join OFICINAS o on cf.codigo_oficina = o.codigo_oficina");
                stringBuilder.AppendLine("WHERE cf.codigo_oficina = @cod");
                using (SqlConnection connection = DALBase.getConnection())
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = stringBuilder.ToString();
                    command.Parameters.AddWithValue("@cod", (object)cod);
                    command.Connection.Open();
                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                            categoriaDeudaList.Add(new CATEGORIA_DEUDA()
                            {
                                cod_categoria = sqlDataReader.GetInt32(0),
                                des_categoria = sqlDataReader.GetString(1)
                            });
                    }
                }
                if (categoriaDeudaList.Count > 0)
                    return categoriaDeudaList;
                return CATEGORIA_DEUDA.read();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<CATEGORIA_DEUDA> read()
        {
            try
            {
                List<CATEGORIA_DEUDA> categoriaDeudaList = new List<CATEGORIA_DEUDA>();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("select c.cod_categoria, c.des_categoria");
                stringBuilder.AppendLine("from CATE_DEUDA_FACTU C");
                using (SqlConnection connection = DALBase.getConnection())
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = stringBuilder.ToString();
                    command.Connection.Open();
                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                            categoriaDeudaList.Add(new CATEGORIA_DEUDA()
                            {
                                cod_categoria = sqlDataReader.GetInt32(0),
                                des_categoria = sqlDataReader.GetString(1)
                            });
                    }
                }
                return categoriaDeudaList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
