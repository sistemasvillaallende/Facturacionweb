using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using iTextSharp.text.pdf.codec.wmf;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Crmf;
using RestSharp;

namespace Facturacion
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["VABack.CIDI"] == null)
                Response.Redirect("http://10.0.0.24/siimva/login.aspx");
            else
            {
                UsuarioLoginCIDI usu = null;
                string baseApi = System.Configuration.ConfigurationManager.AppSettings["BaseApi"];
                var options = new RestClientOptions(baseApi);
                var client = new RestClient(options);
                var request = new RestRequest(string.Format(
                    "/CiDiLogin/Usuario/GetUsuarioLogueado?hash={0}",
                    Request.Cookies["VABack.CIDI"]["SesionHash"], Method.Get));

                RestResponse response = client.Execute(request);
                usu = JsonConvert.DeserializeObject<UsuarioLoginCIDI>(response.Content);
                DAL.Usuario usuario = new DAL.Usuario();    
                usuario.COD_USUARIO=usu.cod_usuario;
                usuario.NOMBRE_COMPLETO=usu.nombre_completo;
                usuario.NOMBRE=usu.nombre;
                usuario.ADMINISTRADOR = usu.administrador;  
                usuario.LEGAJO = usu.legajo;
                usuario.cod_oficina = usu.cod_oficina;  
                usuario.nombre_oficina = usu.nombre_oficina;
                //this.Session["USER"] = (object)usuario;
                this.Response.Redirect("Secure/Facturacion.aspx");
            }
        }

        protected void btnIngresar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                DAL.Usuario usuario = BLL.Usuario.ValidUser(this.txtUsuario.Value, this.txtPass.Value);
                if (usuario == null)
                    return;
                //this.Session["USER"] = (object)usuario;
                this.Response.Redirect("Secure/Facturacion.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // }
            // }
        }
    }
}