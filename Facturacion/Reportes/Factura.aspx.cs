using BLL;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
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


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["NROTRAN"] != null)
            {
                int nroTran = Convert.ToInt32(Request.QueryString["NROTRAN"]);
                GenerarPDFFactura(nroTran);
            }
            else
            {
                Response.Write("Parámetro NROTRAN requerido");
            }
        }

        private void GenerarPDFFactura(int nroTran)
        {
            try
            {
                DAL.Facturacion objFactu = BLL.Facturacion.getByNroTran(nroTran);
                int nroCedulon = BLL.Facturacion.getNroCedulon(nroTran);
                Persona objPers = Persona.getByPk(Convert.ToInt64(objFactu.CUIT));

                // Preparar datos para el generador PDF
                var datosComprobante = new Helpers.ComprobanteData
                {
                    Municipio = "MUNICIPALIDAD DE VILLA ALLENDE ", 
                    NroCedulon = string.Format("C0{0}", nroCedulon),
                    Concepto = objFactu.des_categoria ?? "FACTURACIÓN",
                    ContribuyenteNombre = objFactu.nombre,
                    ContribuyenteCuit = objFactu.CUIT,
                    Domicilio = objPers.direccion,
                    Localidad = string.Format("({0}) {1} - {2}",
                        objPers.codPostal,
                        objPers.localidad,
                        Utils.getProvincia(objPers.idProvincia)),
                    FechaImpresion = DateTime.Now,
                    Vencimiento = objFactu.vencimiento,
                    Importe = objFactu.monto,
                    CodigoBarraLargo = GenerarCodigoBarraLargo(nroCedulon, objFactu.monto, objFactu.vencimiento),
                    CodigoBarraCorto = "*" + string.Format("C0{0}", nroCedulon) + "*"
                };

                // Generar PDF
                byte[] pdfBytes = Helpers.ComprobantePdfGenerator.Generar(datosComprobante,nroTran);

                // Enviar PDF al navegador
                EnviarPDFAlNavegador(pdfBytes, $"Factura_{nroCedulon}.pdf");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error al generar el PDF: " + ex.Message);
                Response.Write("Hubo problemas al generar la factura. Error: " + ex.Message);
            }
        }

        private string GenerarCodigoBarraLargo(int nroCedulon, decimal monto, DateTime vencimiento)
        {
            // Usar la misma lógica que tenías con Crystal Reports
            string vencimiento_str = vencimiento.ToShortDateString();
            return Library.ArmoCBarra("0", nroCedulon.ToString(), monto, vencimiento_str, 0, "0", "10");
        }

        private void EnviarPDFAlNavegador(byte[] pdfBytes, string nombreArchivo)
        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", $"inline; filename=\"{nombreArchivo}\"");
            Response.AddHeader("Content-Length", pdfBytes.Length.ToString());
            Response.BinaryWrite(pdfBytes);
            Response.End();
        }
    }
}

