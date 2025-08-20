using BLL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Facturacion.Reportes
{
    public partial class Facturas : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["nroTran"] != null)
                    frame.Attributes.Add("src", "factura.aspx?nroTran=" +
                        Request.QueryString["nroTran"]);
                else
                    Response.Redirect("../Secure/Facturacion.aspx");
            }
        }

    }
}