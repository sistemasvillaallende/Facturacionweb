using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Facturacion.MP
{
    public partial class MP : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    if (this.Session["USER"] == null)
            //        this.Response.Redirect("LogIn.aspx");
            //    if (this.IsPostBack)
            //        return;
            //    Usuario usuario = (Usuario)this.Session["user"];
            //    this.spanOficina.InnerText = usuario.nombre_oficina;
            //    this.spanUsuario.InnerText = usuario.NOMBRE;
            //    this.spanUsuario2.InnerText = usuario.NOMBRE_COMPLETO;
            //}
            //catch (Exception ex)
            //{
            //}
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            //this.Session.Abandon();
            //this.Response.Redirect("../LogIn.aspx");
        }
    }
}