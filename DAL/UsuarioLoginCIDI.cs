using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UsuarioLoginCIDI : DALBase
    {
        public int cod_usuario { get; set; }
        public string nombre_usuario { get; set; }
        public int legajo { get; set; }
        public string sessionHash { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string nombre_completo { get; set; }
        public string cuit { get; set; }
        public string cuit_formateado { get; set; }
        public bool administrador { get; set; }
        public int cod_oficina { get; set; }
        public string nombre_oficina { get; set; }
        //public List<> lstProcesosPrincipales { get; set; }

        public UsuarioLoginCIDI()
        {
            cod_usuario = 0;
            nombre_usuario = string.Empty;
            legajo = 0;
            sessionHash = string.Empty;
            nombre = string.Empty;
            apellido = string.Empty;
            nombre_completo = string.Empty;
            cuit = string.Empty;
            cuit_formateado = string.Empty;
            administrador = false;
            cod_oficina = 0;
            nombre_oficina = string.Empty;
            //   lstProcesosPrincipales = new List<Procesos>();
        }

        public static UsuarioLoginCIDI getByCuit(string cuit)
        {
            try
            {
                UsuarioLoginCIDI usuario = null;
                using (SqlConnection con = DALBase.getConnection())
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"
                    SELECT 
	                    A.COD_USUARIO,
	                    A.NOMBRE,
	                    A.COD_OFICINA,
	                    A.ADMINISTRADOR,
	                    B.nombre_oficina, 
                        C.legajo
                    FROM USUARIOS_V2 A
                    INNER JOIN OFICINAS B ON A.COD_OFICINA=B.codigo_oficina
                    FULL JOIN EMPLEADOS C ON C.CUIL=@cuit
                    WHERE CUIT=@cuit";

                    cmd.Parameters.AddWithValue("@cuit", cuit);
                    cmd.Connection.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        int COD_USUARIO = sqlDataReader.GetOrdinal("COD_USUARIO");
                        int NOMBRE = sqlDataReader.GetOrdinal("NOMBRE");
                        int COD_OFICINA = sqlDataReader.GetOrdinal("COD_OFICINA");
                        int ADMINISTRADOR = sqlDataReader.GetOrdinal("ADMINISTRADOR");
                        int nombre_oficina = sqlDataReader.GetOrdinal("nombre_oficina");
                        int legajo = sqlDataReader.GetOrdinal("legajo");
                        while (sqlDataReader.Read())
                        {
                            usuario = new UsuarioLoginCIDI();
                            if (!sqlDataReader.IsDBNull(COD_USUARIO))
                                usuario.cod_usuario = sqlDataReader.GetInt32(COD_USUARIO);
                            if (!sqlDataReader.IsDBNull(NOMBRE))
                                usuario.nombre = sqlDataReader.GetString(NOMBRE);
                            if (!sqlDataReader.IsDBNull(COD_OFICINA))
                                usuario.cod_oficina = sqlDataReader.GetInt32(COD_OFICINA);
                            if (!sqlDataReader.IsDBNull(ADMINISTRADOR))
                                usuario.administrador = sqlDataReader.GetBoolean(ADMINISTRADOR);
                            if (!sqlDataReader.IsDBNull(nombre_oficina))
                                usuario.nombre_oficina = sqlDataReader.GetString(nombre_oficina);
                            if (!sqlDataReader.IsDBNull(legajo))
                                usuario.legajo = sqlDataReader.GetInt32(legajo);
                            //usuario.lstProcesosPrincipales = Procesos.read(usuario.cod_usuario);
                        }
                    }
                    return usuario;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
