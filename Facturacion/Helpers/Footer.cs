 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Facturacion.Helpers
{
    public class Footer : PdfPageEventHelper
    {

        private readonly ComprobanteData _comprobanteData;
        private readonly int _subsistema;
        private readonly Paragraph _salto;
        private readonly Font _standardFont;
        private readonly Font _encabezado;

        public Footer(ComprobanteData d, int subsistema)
        {
            _comprobanteData = d;
            _subsistema = subsistema;
            _salto = new Paragraph() { SpacingAfter = 2 };
            _standardFont = new Font(Font.NORMAL, 8, Font.NORMAL, BaseColor.BLACK);
            _encabezado = new Font(Font.NORMAL, 12, Font.BOLD, BaseColor.BLACK);
        }

        public override void OnEndPage(PdfWriter writer, Document doc)
        {
            DibujarCupones(writer);
            DibujarSeccionLogosYDatos(writer);
            DibujarSeccionTitular(writer);
            DibujarSeccionCodigoBarras(writer);
        }

        private void DibujarCupones(PdfWriter writer)
        {
            PdfPTable table = new PdfPTable(2)
            {
                WidthPercentage = 100,
                SpacingBefore = 20,
                SpacingAfter = 10,
            };
            table.DefaultCell.UseAscender = true;

            // Cupón Municipalidad
            PdfPCell cellMunicipalidad = new PdfPCell() { BorderWidth = 0 };
            cellMunicipalidad.AddElement(new Phrase("CUPON MUNICIPALIDAD", _standardFont));
            table.AddCell(cellMunicipalidad);

            // Cupón Contribuyente
            PdfPCell cellContribuyente = new PdfPCell() { BorderWidth = 0 };
            cellContribuyente.AddElement(new Phrase("CUPON CONTRIBUYENTE", _standardFont));
            table.AddCell(cellContribuyente);

            table.TotalWidth = 550;
            table.WriteSelectedRows(0, -1, 20, 210, writer.DirectContent);
        }

        private void DibujarSeccionLogosYDatos(PdfWriter writer)
        {
            PdfPTable table = new PdfPTable(4) { WidthPercentage = 100 };
            table.SetWidths(new float[] { 15, 35, 15, 35 });

            // Logo izquierdo
            PdfPCell cellLogoIzq = CrearCeldaLogo(true);
            cellLogoIzq.PaddingLeft = 12f;
            table.AddCell(cellLogoIzq);

            // Datos izquierdo
            PdfPCell cellDatosIzq = CrearCeldaDatos(false);
            table.AddCell(cellDatosIzq);

            // Logo derecho (reutilizamos el mismo logo)
            PdfPCell cellLogoDer = CrearCeldaLogo(false);
            cellLogoDer.BorderWidthLeft = 1f;
            cellLogoDer.PaddingLeft = 12f;
            table.AddCell(cellLogoDer);

            // Datos derecho
            PdfPCell cellDatosDer = CrearCeldaDatos(true);
            table.AddCell(cellDatosDer);

            table.TotalWidth = 550;
            table.HorizontalAlignment = Element.ALIGN_CENTER;
            table.WriteSelectedRows(0, -1, 20, 190, writer.DirectContent);
        }

        private void DibujarSeccionTitular(PdfWriter writer)
        {
            PdfPTable table = new PdfPTable(4) { WidthPercentage = 100 };
            table.SetWidths(new float[] { 25, 25, 25, 25 });

            // Propietario izquierdo
            PdfPCell cellPropIzq = CrearCeldaPropietario(true, false);
            table.AddCell(cellPropIzq);

            // Bien izquierdo
            PdfPCell cellBienIzq = CrearCeldaBien(false);
            table.AddCell(cellBienIzq);

            // Propietario derecho
            PdfPCell cellPropDer = CrearCeldaPropietario(false, true);
            cellPropDer.BorderWidthLeft = 1f;  
            table.AddCell(cellPropDer);

            // Bien derecho
            PdfPCell cellBienDer = CrearCeldaBien(true);
            table.AddCell(cellBienDer);

            table.TotalWidth = 550;
            table.WriteSelectedRows(0, -1, 20, 160, writer.DirectContent);
        }

        private void DibujarSeccionCodigoBarras(PdfWriter writer)
        {
            PdfPTable table = new PdfPTable(2) { WidthPercentage = 100 };
            table.SetWidths(new float[] { 50, 50 });

            // Celda con código de barras
            PdfPCell cellCodigoBarras = new PdfPCell()
            {
                BorderWidth = 0,
                BorderWidthBottom = 1f,
                BorderWidthLeft = 1f,
                PaddingLeft = 50,
                PaddingRight = 50
            };

            cellCodigoBarras.AddElement(_salto);
            cellCodigoBarras.AddElement(CrearCodigoBarras(writer));
            table.AddCell(cellCodigoBarras);

            // Celda vacía
            PdfPCell cellVacia = new PdfPCell(new Phrase())
            {
                BorderWidth = 0,
                BorderWidthBottom = 1f,
                BorderWidthLeft = 1f,
                BorderWidthRight = 1f,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };
            table.AddCell(cellVacia);

            table.TotalWidth = 550;
            table.WriteSelectedRows(0, -1, 20, 80, writer.DirectContent);
        }

        private PdfPCell CrearCeldaLogo(bool esIzquierdo)
        {
            Image logo = ObtenerLogo();

            PdfPCell cell = new PdfPCell()
            {
                Image = logo,
                BorderWidth = 0,
                BorderWidthTop = 1f,
                BorderWidthBottom = 1f,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            if (esIzquierdo)
                cell.BorderWidthLeft = 1f;

            return cell;
        }

        private PdfPCell CrearCeldaDatos(bool esDerecho)
        {
            PdfPCell cell = new PdfPCell()
            {
                BorderWidth = 0,
                BorderWidthTop = 1f,
                BorderWidthBottom = 1f,
                Padding = 10
            };

            if (esDerecho)
                cell.BorderWidthRight = 1f;

            cell.AddElement(new Phrase("LIQUIDACIÓN DEUDA", _standardFont));
            return cell;
        }

        private PdfPCell CrearCeldaPropietario(bool esIzquierdo, bool incluyeNumeroComprobante)
        {
            PdfPCell cell = new PdfPCell()
            {
                BorderWidth = 0,
                Padding = 10
            };

            if (esIzquierdo)
                cell.BorderWidthLeft = 1f;

            // Nombre
            cell.AddElement(new Phrase(_comprobanteData.ContribuyenteNombre, _standardFont));
            cell.AddElement(_salto);

            // CUIT
            cell.AddElement(new Phrase(FormatearCuit(_comprobanteData.ContribuyenteCuit.ToString()), _standardFont));
            cell.AddElement(_salto);

            // Vencimiento
            cell.AddElement(new Phrase(string.Format("Vencimiento: {0}",
                _comprobanteData.Vencimiento.ToShortDateString()), _standardFont));

            // Número de comprobante (solo en el lado derecho)
            if (incluyeNumeroComprobante)
            {
                cell.AddElement(_salto);
                cell.AddElement(new Phrase(string.Format("Nro. Comprobante: {0}",
                    _comprobanteData.NroCedulon), _standardFont));
            }

            return cell;
        }

        private PdfPCell CrearCeldaBien(bool esDerecho)
        {
            PdfPCell cell = new PdfPCell()
            {
                BorderWidth = 0,
                PaddingBottom = 10,
                Padding = 10
            };

            if (esDerecho)
                cell.BorderWidthRight = 1f;

            string denominacionFormateada = FormatearDenominacionPorSubsistema(_subsistema, _comprobanteData.Concepto, esDerecho);
            cell.AddElement(new Phrase(denominacionFormateada, _standardFont));
            cell.AddElement(_salto);

            // Monto
            cell.AddElement(new Phrase(string.Format("Monto: {0:c}", _comprobanteData.Importe), _standardFont));

            return cell;
        }

        private Image ObtenerLogo()
        {
            try
            {
                return Image.GetInstance("https://vecino.villaallende.gov.ar/App_Themes/images/logo_horizontal.jpg");
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string FormatearCuit(string cuit)
        {
            if (cuit.Length > 5)
            {
                return string.Format("CUIT: {0}-{1}-{2}",
                    cuit.Substring(0, 2),
                    cuit.Substring(2, 8),
                    cuit.Substring(10, 1));
            }
            return cuit;
        }

        private string FormatearDenominacionPorSubsistema(int subsistema, string denominacion, bool esDerecho)
        {
            switch (subsistema)
            {
                case 1:
                    return esDerecho ? string.Format("Denominación: {0}", denominacion) : denominacion;
                case 2:
                case 5:
                case 6:
                    return denominacion;
                case 3:
                    return string.Format("Legajo: {0}", denominacion);
                case 4:
                    return string.Format("Dominio: {0}", denominacion);
                default:
                    return denominacion;
            }
        }

        private IElement CrearCodigoBarras(PdfWriter writer)
        {
            try
            {
                PdfContentByte cb = writer.DirectContent;
                Barcode39 code39 = new Barcode39();
                code39.GenerateChecksum = false;

                string nro =  _comprobanteData.NroCedulon.ToString().Trim();
                code39.Code = nro;

                return code39.CreateImageWithBarcode(cb, null, null);
            }
            catch (Exception)
            {
                return new Phrase("Error generando código de barras");
            }
        }
    }
}