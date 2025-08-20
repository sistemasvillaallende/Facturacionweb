// Decompiled with JetBrains decompiler
// Type: DAL.Facturacion
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
    public class Facturacion : DALBase
    {
        public int nro_transaccion { get; set; }

        public DateTime fecha_transaccion { get; set; }

        public string periodo { get; set; }

        public int nro_bad { get; set; }

        public string nombre { get; set; }

        public int circunscripcion { get; set; }

        public int seccion { get; set; }

        public int manzana { get; set; }

        public int parcela { get; set; }

        public int p_h { get; set; }

        public int legajo { get; set; }

        public string dominio { get; set; }

        public int categoria_deuda { get; set; }

        public string observaciones { get; set; }

        public Decimal monto { get; set; }

        public bool pagado { get; set; }

        public bool exigible { get; set; }

        public int nro_expediente { get; set; }

        public string usuario { get; set; }

        public DateTime fecha_alta { get; set; }

        public int nro_procuracion { get; set; }

        public int cod_oficina { get; set; }

        public int cod_usuario { get; set; }

        public long CUIT { get; set; }

        public string des_categoria { get; set; }

        public DateTime vencimiento { get; set; }

        public Facturacion()
        {
            this.nro_transaccion = 0;
            this.fecha_transaccion = DateTime.Now;
            this.periodo = string.Empty;
            this.nro_bad = 0;
            this.nombre = string.Empty;
            this.circunscripcion = 0;
            this.seccion = 0;
            this.manzana = 0;
            this.parcela = 0;
            this.p_h = 0;
            this.legajo = 0;
            this.dominio = string.Empty;
            this.categoria_deuda = 0;
            this.observaciones = string.Empty;
            this.monto = Decimal.Zero;
            this.pagado = false;
            this.exigible = false;
            this.nro_expediente = 0;
            this.usuario = string.Empty;
            this.fecha_alta = DateTime.Now;
            this.nro_procuracion = 0;
        }

        public static void insert(Facturacion obj)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("INSERT INTO FACTURACION");
                stringBuilder.AppendLine("(nro_transaccion,fecha_transaccion,periodo,nro_bad,nombre,circunscripcion");
                stringBuilder.AppendLine(",seccion,manzana,parcela,p_h,legajo,dominio,categoria_deuda,observaciones");
                stringBuilder.AppendLine(",monto,pagado,exigible,nro_expediente,usuario,fecha_alta, nro_procuracion");
                stringBuilder.AppendLine(",cod_oficina, cod_usuario, CUIT)");
                stringBuilder.AppendLine("VALUES");
                stringBuilder.AppendLine("(@nro_transaccion,@fecha_transaccion,@periodo,@nro_bad,@nombre,@circunscripcion,");
                stringBuilder.AppendLine("@seccion,@manzana,@parcela,@p_h,@legajo,@dominio,@categoria_deuda,@observaciones,");
                stringBuilder.AppendLine("@monto,@pagado,@exigible,@nro_expediente,@usuario,@fecha_alta,@nro_procuracion,");
                stringBuilder.AppendLine("@cod_oficina, @cod_usuario, @CUIT)");
                using (SqlConnection connection = DALBase.getConnection())
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = 9000;
                    command.CommandText = stringBuilder.ToString();
                    command.Parameters.AddWithValue("@nro_transaccion", (object)obj.nro_transaccion);
                    command.Parameters.AddWithValue("@fecha_transaccion", (object)obj.fecha_transaccion);
                    command.Parameters.AddWithValue("@periodo", (object)obj.periodo);
                    command.Parameters.AddWithValue("@nro_bad", (object)obj.nro_bad);
                    command.Parameters.AddWithValue("@nombre", (object)obj.nombre);
                    command.Parameters.AddWithValue("@circunscripcion", (object)obj.circunscripcion);
                    command.Parameters.AddWithValue("@seccion", (object)obj.seccion);
                    command.Parameters.AddWithValue("@manzana", (object)obj.manzana);
                    command.Parameters.AddWithValue("@parcela", (object)obj.parcela);
                    command.Parameters.AddWithValue("@p_h", (object)obj.p_h);
                    command.Parameters.AddWithValue("@legajo", (object)obj.legajo);
                    command.Parameters.AddWithValue("@dominio", (object)obj.dominio);
                    command.Parameters.AddWithValue("@categoria_deuda", (object)obj.categoria_deuda);
                    command.Parameters.AddWithValue("@observaciones", (object)obj.observaciones);
                    command.Parameters.AddWithValue("@monto", (object)obj.monto);
                    command.Parameters.AddWithValue("@pagado", (object)obj.pagado);
                    command.Parameters.AddWithValue("@exigible", (object)obj.exigible);
                    command.Parameters.AddWithValue("@nro_expediente", (object)obj.nro_expediente);
                    command.Parameters.AddWithValue("@usuario", (object)obj.usuario);
                    command.Parameters.AddWithValue("@fecha_alta", (object)obj.fecha_alta);
                    command.Parameters.AddWithValue("@nro_procuracion", (object)obj.nro_procuracion);
                    command.Parameters.AddWithValue("@cod_oficina", (object)obj.cod_oficina);
                    command.Parameters.AddWithValue("@cod_usuario", (object)obj.cod_usuario);
                    command.Parameters.AddWithValue("@CUIT", (object)obj.CUIT);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void deleta(int nro_tran)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("DELETE FACTURACION");
                stringBuilder.AppendLine("WHERE  nro_transaccion = @nro_tran");
                using (SqlConnection connection = DALBase.getConnection())
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = 9000;
                    command.CommandText = stringBuilder.ToString();
                    command.Parameters.AddWithValue("@nro_tran", (object)nro_tran);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int getNroCedulon(int nroTran)
        {
            try
            {
                int num = 0;
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("SELECT c2.nro_cedulon ");
                stringBuilder.AppendLine("FROM CEDULONES2 c2");
                stringBuilder.AppendLine("INNER JOIN DEUDAS_X_CEDULON3 dc3 ON c2.nro_cedulon = dc3.nro_cedulon");
                stringBuilder.AppendLine("INNER JOIN FACTURACION f ON f.nro_transaccion = dc3.nro_transaccion");
                stringBuilder.AppendLine("WHERE f.nro_transaccion = @nroTran");
                using (SqlConnection connection = DALBase.getConnection())
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = stringBuilder.ToString();
                    command.Connection.Open();
                    command.Parameters.AddWithValue("@nroTran", (object)nroTran);
                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            if (!sqlDataReader.IsDBNull(0))
                                num = sqlDataReader.GetInt32(0);
                        }
                    }
                }
                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int getNroTran()
        {
            try
            {
                int num = 0;
                using (SqlConnection connection = DALBase.getConnection())
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT NRO_TRAN_FACTU FROM NUMEROS_CLAVES";
                    command.Connection.Open();
                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            if (!sqlDataReader.IsDBNull(0))
                                num = sqlDataReader.GetInt32(0);
                        }
                    }
                }
                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void setNroTran(int nroTran)
        {
            try
            {
                using (SqlConnection connection = DALBase.getConnection())
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE NUMEROS_CLAVES SET NRO_TRAN_FACTU = @NRO_TRAN_FACTU";
                    command.Parameters.AddWithValue("@NRO_TRAN_FACTU", (object)nroTran);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Facturacion> getByOficina(int cod)
        {
            try
            {
                List<Facturacion> facturacionList = new List<Facturacion>();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("SELECT  f.fecha_transaccion, pf.apellido + ', ' + pf.nombre nombre, cf.des_categoria, f.monto, f.observaciones,");
                stringBuilder.AppendLine("f.pagado, f.nro_transaccion, cta.vencimiento");
                stringBuilder.AppendLine("FROM FACTURACION f");
                stringBuilder.AppendLine("INNER JOIN CATE_DEUDA_FACTU cf ON f.categoria_deuda = cf.cod_categoria");
                stringBuilder.AppendLine("INNER JOIN CTASCTES_FACTU cta ON f.nro_transaccion = cta.nro_transaccion AND tipo_transaccion = 1");
                stringBuilder.AppendLine("INNER JOIN PERSONAS_FACTU pf ON f.cuit = pf.CUIT");
                stringBuilder.AppendLine("WHERE f.cod_oficina = @cod");
                stringBuilder.AppendLine("ORDER BY f.fecha_transaccion DESC");
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
                        int ordinal1 = sqlDataReader.GetOrdinal("fecha_transaccion");
                        int ordinal2 = sqlDataReader.GetOrdinal("nombre");
                        int ordinal3 = sqlDataReader.GetOrdinal("des_categoria");
                        int ordinal4 = sqlDataReader.GetOrdinal("monto");
                        int ordinal5 = sqlDataReader.GetOrdinal("observaciones");
                        int ordinal6 = sqlDataReader.GetOrdinal("pagado");
                        int ordinal7 = sqlDataReader.GetOrdinal("nro_transaccion");
                        int ordinal8 = sqlDataReader.GetOrdinal("vencimiento");
                        while (sqlDataReader.Read())
                        {
                            Facturacion facturacion = new Facturacion();
                            if (!sqlDataReader.IsDBNull(ordinal1))
                                facturacion.fecha_transaccion = sqlDataReader.GetDateTime(ordinal1);
                            if (!sqlDataReader.IsDBNull(ordinal2))
                                facturacion.nombre = sqlDataReader.GetString(ordinal2);
                            if (!sqlDataReader.IsDBNull(ordinal3))
                                facturacion.des_categoria = sqlDataReader.GetString(ordinal3);
                            if (!sqlDataReader.IsDBNull(ordinal4))
                                facturacion.monto = sqlDataReader.GetDecimal(ordinal4);
                            if (!sqlDataReader.IsDBNull(ordinal5))
                                facturacion.observaciones = sqlDataReader.GetString(ordinal5);
                            if (!sqlDataReader.IsDBNull(ordinal6))
                                facturacion.pagado = sqlDataReader.GetBoolean(ordinal6);
                            if (!sqlDataReader.IsDBNull(ordinal7))
                                facturacion.nro_transaccion = sqlDataReader.GetInt32(ordinal7);
                            if (!sqlDataReader.IsDBNull(ordinal8))
                                facturacion.vencimiento = sqlDataReader.GetDateTime(ordinal8);
                            facturacionList.Add(facturacion);
                        }
                    }
                }
                return facturacionList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Facturacion> getByCUIT(string cuit)
        {
            try
            {
                List<Facturacion> facturacionList = new List<Facturacion>();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("SELECT  f.fecha_transaccion, pf.apellido + ', ' + pf.nombre nombre, cf.des_categoria, f.monto, f.observaciones,");
                stringBuilder.AppendLine("f.pagado, f.nro_transaccion, cta.vencimiento");
                stringBuilder.AppendLine("FROM FACTURACION f");
                stringBuilder.AppendLine("INNER JOIN CATE_DEUDA_FACTU cf ON f.categoria_deuda = cf.cod_categoria");
                stringBuilder.AppendLine("INNER JOIN CTASCTES_FACTU cta ON f.nro_transaccion = cta.nro_transaccion AND tipo_transaccion = 1");
                stringBuilder.AppendLine("INNER JOIN PERSONAS_FACTU pf ON f.cuit = pf.CUIT");
                stringBuilder.AppendLine("WHERE f.CUIT = @cuit");
                stringBuilder.AppendLine("ORDER BY f.fecha_transaccion DESC");
                using (SqlConnection connection = DALBase.getConnection())
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = stringBuilder.ToString();
                    command.Parameters.AddWithValue("@cuit", (object)cuit);
                    command.Connection.Open();
                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        int ordinal1 = sqlDataReader.GetOrdinal("fecha_transaccion");
                        int ordinal2 = sqlDataReader.GetOrdinal("nombre");
                        int ordinal3 = sqlDataReader.GetOrdinal("des_categoria");
                        int ordinal4 = sqlDataReader.GetOrdinal("monto");
                        int ordinal5 = sqlDataReader.GetOrdinal("observaciones");
                        int ordinal6 = sqlDataReader.GetOrdinal("pagado");
                        int ordinal7 = sqlDataReader.GetOrdinal("nro_transaccion");
                        int ordinal8 = sqlDataReader.GetOrdinal("vencimiento");
                        while (sqlDataReader.Read())
                        {
                            Facturacion facturacion = new Facturacion();
                            if (!sqlDataReader.IsDBNull(ordinal1))
                                facturacion.fecha_transaccion = sqlDataReader.GetDateTime(ordinal1);
                            if (!sqlDataReader.IsDBNull(ordinal2))
                                facturacion.nombre = sqlDataReader.GetString(ordinal2);
                            if (!sqlDataReader.IsDBNull(ordinal3))
                                facturacion.des_categoria = sqlDataReader.GetString(ordinal3);
                            if (!sqlDataReader.IsDBNull(ordinal4))
                                facturacion.monto = sqlDataReader.GetDecimal(ordinal4);
                            if (!sqlDataReader.IsDBNull(ordinal5))
                                facturacion.observaciones = sqlDataReader.GetString(ordinal5);
                            if (!sqlDataReader.IsDBNull(ordinal6))
                                facturacion.pagado = sqlDataReader.GetBoolean(ordinal6);
                            if (!sqlDataReader.IsDBNull(ordinal7))
                                facturacion.nro_transaccion = sqlDataReader.GetInt32(ordinal7);
                            if (!sqlDataReader.IsDBNull(ordinal8))
                                facturacion.vencimiento = sqlDataReader.GetDateTime(ordinal8);
                            facturacionList.Add(facturacion);
                        }
                    }
                }
                return facturacionList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Facturacion getByNorTran(int nroTran)
        {
            try
            {
                Facturacion facturacion = (Facturacion)null;
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("SELECT f.fecha_transaccion, pf.apellido + ', ' + pf.nombre nombre, cf.des_categoria, f.monto,");
                stringBuilder.AppendLine("f.observaciones, f.pagado, f.CUIT, cta.vencimiento");
                stringBuilder.AppendLine("FROM FACTURACION f");
                stringBuilder.AppendLine("INNER JOIN CATE_DEUDA_FACTU cf ON f.categoria_deuda = cf.cod_categoria");
                stringBuilder.AppendLine("INNER JOIN CTASCTES_FACTU cta ON f.nro_transaccion = cta.nro_transaccion");
                stringBuilder.AppendLine("INNER JOIN PERSONAS_FACTU pf ON f.cuit = pf.CUIT");
                stringBuilder.AppendLine("WHERE f.nro_transaccion = @nroTran");
                stringBuilder.AppendLine("ORDER BY f.fecha_transaccion DESC");
                using (SqlConnection connection = DALBase.getConnection())
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = stringBuilder.ToString();
                    command.Parameters.AddWithValue("@nroTran", (object)nroTran);
                    command.Connection.Open();
                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        int ordinal1 = sqlDataReader.GetOrdinal("fecha_transaccion");
                        int ordinal2 = sqlDataReader.GetOrdinal("nombre");
                        int ordinal3 = sqlDataReader.GetOrdinal("des_categoria");
                        int ordinal4 = sqlDataReader.GetOrdinal("monto");
                        int ordinal5 = sqlDataReader.GetOrdinal("observaciones");
                        int ordinal6 = sqlDataReader.GetOrdinal("pagado");
                        int ordinal7 = sqlDataReader.GetOrdinal("CUIT");
                        int ordinal8 = sqlDataReader.GetOrdinal("vencimiento");
                        while (sqlDataReader.Read())
                        {
                            facturacion = new Facturacion();
                            if (!sqlDataReader.IsDBNull(ordinal1))
                                facturacion.fecha_transaccion = sqlDataReader.GetDateTime(ordinal1);
                            if (!sqlDataReader.IsDBNull(ordinal2))
                                facturacion.nombre = sqlDataReader.GetString(ordinal2);
                            if (!sqlDataReader.IsDBNull(ordinal3))
                                facturacion.des_categoria = sqlDataReader.GetString(ordinal3);
                            if (!sqlDataReader.IsDBNull(ordinal4))
                                facturacion.monto = sqlDataReader.GetDecimal(ordinal4);
                            if (!sqlDataReader.IsDBNull(ordinal5))
                                facturacion.observaciones = sqlDataReader.GetString(ordinal5);
                            if (!sqlDataReader.IsDBNull(ordinal6))
                                facturacion.pagado = sqlDataReader.GetBoolean(ordinal6);
                            if (!sqlDataReader.IsDBNull(ordinal7))
                                facturacion.CUIT = sqlDataReader.GetInt64(ordinal7);
                            if (!sqlDataReader.IsDBNull(ordinal8))
                                facturacion.vencimiento = sqlDataReader.GetDateTime(ordinal8);
                        }
                    }
                }
                return facturacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
