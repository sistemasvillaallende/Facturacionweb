using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Facturacion
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnIngresar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                DAL.Usuario usuario = BLL.Usuario.ValidUser(this.txtUsuario.Value, this.txtPass.Value);
                if (usuario == null)
                    return;
                this.Session["USER"] = (object)usuario;
                this.Response.Redirect("Secure/Facturacion.aspx");
            }
            catch (Exception ex)
            {
                throw;
            }
            // }
            // }
        }
    }
}