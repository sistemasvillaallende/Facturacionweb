using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using iTextSharp.text.html.simpleparser;

namespace Facturacion.Helpers
{
    public class ComprobantePdfGenerator
    {
        public static byte[] Generar(ComprobanteData d, int nroTransaccion)
        {
            // Cultura AR para formatos
            var ar = new CultureInfo("es-AR");

            using (var ms = new MemoryStream())
            {
                // Documento A4 con márgenes
                Document doc = null;
                try
                {
                     doc = new Document(PageSize.A4, 28f, 28f, 24f, 28f);
                    var writer = PdfWriter.GetInstance(doc, ms);
                    doc.Open();

                    // Fuentes
                    var f8 = FontFactory.GetFont(FontFactory.HELVETICA, 8);
                    var f9b = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9);
                    var f10 = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                    var f10b = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                    var f12b = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                    var f14b = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);

                    // Encabezado
                    var header = new PdfPTable(2) { WidthPercentage = 100 };
                    header.SetWidths(new float[] { 70, 30 });

                    header.AddCell(CellTxt(d.Municipio ?? "Municipio", f14b, Rectangle.NO_BORDER, Element.ALIGN_LEFT, 2f));
                    header.AddCell(CellTxt($"Nº Cedulón: {d.NroCedulon}", f10b, Rectangle.NO_BORDER, Element.ALIGN_RIGHT, 2f));

                    doc.Add(header);
                    doc.Add(new Paragraph(" "));


                    var conceptoPago = new PdfPTable(2) { WidthPercentage = 100 };
                    conceptoPago.SetWidths(new float[] { 70, 30 }); // izquierda 70%, derecha 30%

                    // Celda izquierda → concepto
                    conceptoPago.AddCell(CellTxt(d.Concepto?.Trim().ToUpper() ?? "CONCEPTO", f12b, Rectangle.NO_BORDER, Element.ALIGN_LEFT, 4f));

                    // Celda derecha → link de pago
                    //if (d.Concepto?.Trim().ToUpper() == "REQUIEM DE MOZART")  
                    // despues sacarlo con categoria_deuda
                    if (d.Concepto?.Trim().ToUpper() == "DERECHO DE INSCRIPCION E INSPECCION COMERCIAL")
                    {
                        string url = $"https://vecino.villaallende.gov.ar/PagosOnLine/CertificadosHabilitacion.aspx?nro_transaccion={nroTransaccion}";

                        // Crear font azul y subrayado
                        var linkFont = new Font(f12b.BaseFont, 12, Font.UNDERLINE, BaseColor.BLUE);

                        var anchor = new Anchor("💳 PAGÁ ACÁ ➜", linkFont) { Reference = url };
                        var phrase = new Phrase();
                        phrase.Add(anchor);

                        var cellLink = new PdfPCell(phrase)
                        {
                            Border = Rectangle.NO_BORDER,
                            HorizontalAlignment = Element.ALIGN_RIGHT,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        };
                        conceptoPago.AddCell(cellLink);
                    }
                    else
                    {
                        conceptoPago.AddCell(CellTxt("", f12b, Rectangle.NO_BORDER));
                    }

                    doc.Add(conceptoPago);
                    doc.Add(new Paragraph(" ", f10));

                    // Datos del contribuyente
                    var datos = new PdfPTable(2) { WidthPercentage = 100 };
                    datos.SetWidths(new float[] { 18, 82 });


                    datos.AddCell(CellTxt("CONTRIBUYENTE:", f9b));
                    datos.AddCell(CellTxt(d.ContribuyenteNombre, f10));
                    datos.AddCell(CellTxt("C.U.I.T.:", f9b));
                    datos.AddCell(CellTxt(d.ContribuyenteCuit.ToString(), f10));
                    datos.AddCell(CellTxt("DOMICILIO:", f9b));
                    datos.AddCell(CellTxt(d.Domicilio, f10));
                    datos.AddCell(CellTxt("LOCALIDAD:", f9b));
                    datos.AddCell(CellTxt(d.Localidad, f10));

                    doc.Add(datos);
                    doc.Add(new Paragraph(" ", f10));

                    // Fecha de impresión
                    var fechaImp = new Paragraph($"Fecha de Impresión: {d.FechaImpresion:dd/MM/yyyy}", f10);
                    doc.Add(fechaImp);
                    doc.Add(new Paragraph(" ", f10));

                    // Cuadro Vencimiento / Importe
                    var cuadro = new PdfPTable(2) { WidthPercentage = 60, HorizontalAlignment = Element.ALIGN_LEFT };
                    cuadro.SetWidths(new float[] { 50, 50 });
                    cuadro.AddCell(CellTxt("VENCIMIENTO", f10b, Rectangle.BOX, Element.ALIGN_CENTER, 6f, 16f));
                    cuadro.AddCell(CellTxt("IMPORTE", f10b, Rectangle.BOX, Element.ALIGN_CENTER, 6f, 16f));
                    cuadro.AddCell(CellTxt(d.Vencimiento.ToString("dd/MM/yyyy"), f12b, Rectangle.BOX, Element.ALIGN_CENTER, 10f, 20f));
                    cuadro.AddCell(CellTxt(d.Importe.ToString("$ #,##0.00", ar), f12b, Rectangle.BOX, Element.ALIGN_CENTER, 10f, 20f));
                    doc.Add(cuadro);

                    doc.Add(new Paragraph(" ", f10));

                    // Código de barras largo (Interleaved 2 of 5 si es par; si no, Code128)
                    if (!string.IsNullOrWhiteSpace(d.CodigoBarraLargo))
                    {
                        var imgBarraLarga = CrearBarcodeAuto(d.CodigoBarraLargo, writer, alto: 40f, anchoX: 0.9f, textoVisible: true);
                        if (imgBarraLarga != null)
                        {
                            imgBarraLarga.Alignment = Element.ALIGN_LEFT;
                            imgBarraLarga.ScalePercent(100f);
                            doc.Add(imgBarraLarga);
                        }
                    }

                    doc.Add(new Paragraph(" ", f10));

                    // Línea de corte
                    var cut = new Paragraph("------------------------------------------------  CUPÓN MUNICIPALIDAD  /  CUPÓN CONTRIBUYENTE  ------------------------------------------------", f8);
                    cut.Alignment = Element.ALIGN_CENTER;
                    doc.Add(cut);
                    doc.Add(new Paragraph(" ", f10));

                    // Duplicado: Municipalidad / Contribuyente
                    var cupones = new PdfPTable(2) { WidthPercentage = 100 };
                    cupones.SetWidths(new float[] { 50, 50 });

                    // Cupón 1
                    cupones.AddCell(BloqueCupon("CUPON MUNICIPALIDAD", d, writer, f10, f10b, ar));
                    // Cupón 2
                    cupones.AddCell(BloqueCupon("CUPON CONTRIBUYENTE", d, writer, f10, f10b, ar));

                    doc.Add(cupones);

                    if (!string.IsNullOrWhiteSpace(d.Observaciones))
                    {
                        doc.Add(new Paragraph(" ", f10)); // Espacio en blanco

                        // Título "Observaciones" en negrita
                        var tituloObs = new Paragraph("OBSERVACIONES:", f10b);
                        doc.Add(tituloObs);

                        try
                        {
                            // Renderizar HTML en el PDF usando HTMLWorker
                            using (var htmlReader = new StringReader(d.Observaciones))
                            {
                                var htmlElements = HTMLWorker.ParseToList(htmlReader, null);
                                foreach (var htmlElement in htmlElements)
                                {
                                    doc.Add(htmlElement);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            // Si falla el parsing de HTML, mostrar como texto plano
                            var contenidoObs = new Paragraph(d.Observaciones, f10);
                            contenidoObs.Alignment = Element.ALIGN_JUSTIFIED;
                            doc.Add(contenidoObs);
                        }
                    }

                    doc.Close();
                    return ms.ToArray();
                }
                catch (Exception ex)
                {
                    // Log o mensaje detallado
                    throw new Exception($"Error generando comprobante PDF: {ex.Message}", ex);
                }
                finally
                {
                    if (doc != null && doc.IsOpen())
                        doc.Close();
                }
            }
        }

        private static PdfPCell BloqueCupon(string titulo, ComprobanteData d, PdfWriter w, Font f10, Font f10b, CultureInfo ar)
        {
            var panel = new PdfPTable(1) { WidthPercentage = 100 };

            panel.AddCell(CellTxt(titulo, f10b, Rectangle.BOX, Element.ALIGN_CENTER, 6f, 14f));
            panel.AddCell(CellTxt(d.ContribuyenteNombre, f10, Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, Element.ALIGN_LEFT, 6f));
            panel.AddCell(CellTxt($"CUIT: {d.ContribuyenteCuit}", f10, Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, Element.ALIGN_LEFT, 6f));
            panel.AddCell(CellTxt(d.Domicilio, f10, Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, Element.ALIGN_LEFT, 6f));
            panel.AddCell(CellTxt(d.Localidad, f10, Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, Element.ALIGN_LEFT, 6f));

            var t = new PdfPTable(2) { WidthPercentage = 100 };
            t.SetWidths(new float[] { 50, 50 });
            t.AddCell(CellTxt("Vencimiento", f10b, Rectangle.BOX, Element.ALIGN_CENTER, 4f));
            t.AddCell(CellTxt("Importe", f10b, Rectangle.BOX, Element.ALIGN_CENTER, 4f));
            t.AddCell(CellTxt(d.Vencimiento.ToString("dd/MM/yyyy"), f10b, Rectangle.BOX, Element.ALIGN_CENTER, 8f, 16f));
            t.AddCell(CellTxt(d.Importe.ToString("$ #,##0.00", ar), f10b, Rectangle.BOX, Element.ALIGN_CENTER, 8f, 16f));
            panel.AddCell(new PdfPCell(t) { Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER });

            // Código corto (generalmente "*Cxxxx*" impreso como texto + Code39/128)
            if (!string.IsNullOrWhiteSpace(d.CodigoBarraCorto))
            {
                var imgBarraCorta = CrearBarcodeCode39Or128(d.CodigoBarraCorto, w, alto: 36f, anchoX: 0.9f, textoVisible: true);
                if (imgBarraCorta != null)
                {
                    var cellImg = new PdfPCell(imgBarraCorta) { Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, Padding = 6f, HorizontalAlignment = Element.ALIGN_CENTER };
                    panel.AddCell(cellImg);
                }
            }

            // Nro de Cedulón en grande abajo
            panel.AddCell(CellTxt(d.NroCedulon, f10b, Rectangle.BOX, Element.ALIGN_CENTER, 6f, 16f));

            // Contenedor final
            return new PdfPCell(panel) { Border = Rectangle.NO_BORDER, Padding = 4f };
        }

        private static Image CrearBarcodeAuto(string data, PdfWriter writer, float alto, float anchoX, bool textoVisible)
        {
            // Si la longitud es par y numérica → Interleaved 2 of 5 (más común en recaudación).
            if (EsNumerico(data) && data.Length % 2 == 0)
            {
                var bc = new BarcodeInter25
                {
                    Code = data,
                    Extended = false,
                    ChecksumText = false,
                    GenerateChecksum = false,
                    BarHeight = alto,
                    X = anchoX,
                    Font = textoVisible ? BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false) : null
                };
                return bc.CreateImageWithBarcode(writer.DirectContent, null, null);
            }
            else
            {
                // Fallback a Code128
                var bc = new Barcode128
                {
                    Code = data,
                    BarHeight = alto,
                    X = anchoX,
                    Font = textoVisible ? BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false) : null
                };
                return bc.CreateImageWithBarcode(writer.DirectContent, null, null);
            }
        }

        private static Image CrearBarcodeCode39Or128(string data, PdfWriter writer, float alto, float anchoX, bool textoVisible)
        {
            // Si el dato está rodeado por *...* suele usarse Code39; si no, Code128.
            if (data.StartsWith("*") && data.EndsWith("*"))
            {
                var bc39 = new Barcode39
                {
                    Code = data.Trim('*'),
                    StartStopText = true,
                    Extended = false,
                    BarHeight = alto,
                    X = anchoX,
                    Font = textoVisible ? BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false) : null
                };
                return bc39.CreateImageWithBarcode(writer.DirectContent, null, null);
            }
            else
            {
                var bc128 = new Barcode128
                {
                    Code = data,
                    BarHeight = alto,
                    X = anchoX,
                    Font = textoVisible ? BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false) : null
                };
                return bc128.CreateImageWithBarcode(writer.DirectContent, null, null);
            }
        }

        private static PdfPCell CellTxt(string txt, Font f, int border = Rectangle.NO_BORDER, int align = Element.ALIGN_LEFT, float padding = 4f, float minH = 0f)
        {
            var c = new PdfPCell(new Phrase(txt ?? string.Empty, f)) { Border = border, Padding = padding, HorizontalAlignment = align, MinimumHeight = minH };
            return c;
        }

        private static bool EsNumerico(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            foreach (var ch in s)
                if (ch < '0' || ch > '9') return false;
            return true;
        }
    }
}