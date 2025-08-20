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
    public partial class Factura1 : System.Web.UI.Page
    {
        private ReportDocument customerReport;
        Int32 nro_cedulon = 0;
        CrystalDecisions.Web.CrystalReportViewer crview;

        protected void Page_Load(object sender, EventArgs e)
        {
            crview = new CrystalDecisions.Web.CrystalReportViewer();
            customerReport = new ReportDocument();
            if(Request.QueryString["NROTRAN"] != null)
                ConfigureCrystalReports(Convert.ToInt32(Request.QueryString["NROTRAN"]));
        }

        private void ConfigureCrystalReports(int nroTran)
        {
            try
            {
                string reportPath = Server.MapPath("Factura.rpt");


                customerReport.PrintOptions.PaperSize = PaperSize.PaperA4;
                customerReport.Load(reportPath);

                int nroCedulon = BLL.Facturacion.getNroCedulon(nroTran);

                DAL.Facturacion objFactu = BLL.Facturacion.getByNroTran(nroTran);

                decimal monto_2 = objFactu.monto;
                string vencimiento_2 = objFactu.fecha_transaccion.ToShortDateString();

                string strCadena = Library.ArmoCBarra(
                    "0", nroCedulon.ToString(), monto_2, vencimiento_2, 0, "0", "10");

                Persona objPers = Persona.getByPk(Convert.ToInt64(objFactu.CUIT));


                //customerReport.SetParameterValue("strPeriodos", strPeriodos);

                customerReport.SetParameterValue("CodigoBarraRapiPago", Library.ConvertToChar(strCadena));
                customerReport.SetParameterValue("nroCodigoBarraRapiPago", strCadena);
                customerReport.SetParameterValue("FechaImpresion", DateTime.Now.ToShortDateString());
                customerReport.SetParameterValue("nroCedulon", string.Format("C0{0}", nroCedulon));
                customerReport.SetParameterValue("categoriaDeuda", objFactu.des_categoria);
                customerReport.SetParameterValue("CUIT", objFactu.CUIT);
                customerReport.SetParameterValue("contribuyente", objFactu.nombre);
                customerReport.SetParameterValue("direccion1", objPers.direccion);
                customerReport.SetParameterValue("direccion2", string.Format("({0}) {1} - {2}",
                    objPers.codPostal, objPers.localidad, 
                    Utils.getProvincia(objPers.idProvincia)));
                customerReport.SetParameterValue("obs", objFactu.observaciones.ToString());
                customerReport.SetParameterValue("vencimiento", objFactu.vencimiento.ToShortDateString());
                customerReport.SetParameterValue("importe", string.Format("{0:c}", objFactu.monto));
                customerReport.SetParameterValue("codigoBarraCaja", "*" + string.Format("C0{0}", nroCedulon) + "*");

                crview.ReportSource = customerReport;
                crview.RefreshReport();
                crview.DataBind();
                customerReport.ExportToHttpResponse
                    (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "");
            }

            catch (Exception ex)
            {
                System.Console.WriteLine("Error, no se pudo generar el Reporte " + ex.Message);
                Response.Write("Hubo problemas con el cedulon, no se pudo generar el Reporte...");
            }
        }

    }
}