using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using iTextSharp.text.html.simpleparser;
using DAL;
using System.Configuration;


namespace Facturacion.Helpers
{
    public class ComprobantePdfGenerator
    {

        private static Font _standardFont = new Font(Font.NORMAL, 8, Font.NORMAL, BaseColor.BLACK);
        private static Font _encabezado = new Font(Font.NORMAL, 12, Font.BOLD, BaseColor.BLACK);
        private static Font _titulo = new Font(Font.NORMAL, 14, Font.BOLD, BaseColor.BLACK);
        private static Font _subtitulo = new Font(Font.NORMAL, 10, Font.BOLD, BaseColor.BLACK);

        // Párrafo con espacio
        private static Paragraph salto = new Paragraph("") { SpacingAfter = 2 };
        private static Paragraph salto2 = new Paragraph(" ")  
        {
            SpacingAfter = 15,
        };

        public static byte[] Generar(ComprobanteData d, int nroTransaccion, int subsistema)
        {
            // Cultura AR para formatos
            var ar = new CultureInfo("es-AR");

            Document doc = new Document(PageSize.A4, 20, 20, 20, 20);

            using (MemoryStream output = new MemoryStream())
            {
                try
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, output);
                    doc.Open();

                    doc.Add(GetEncabezado(d, writer));

                    doc.Add(GetTitular(d,subsistema));
                   
                    doc.Add(GetDetalle(d,nroTransaccion));
                    doc.Add(salto2);

                    if (!string.IsNullOrWhiteSpace(d.Observaciones))
                    {
                        doc.Add(GetTituloObservaciones());
                        doc.Add(salto);
                        doc.Add(GetContenidoObservaciones(d.Observaciones));
                        doc.Add(salto);
                    }

                    writer.PageEvent = new Footer(d, 2);
                    doc.Close();
                    return output.ToArray();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error generando comprobante PDF: {ex.Message}", ex);
                }
                finally
                {
                    if (doc != null && doc.IsOpen())
                        doc.Close();
                }
            }
        }


        private static PdfPTable GetEncabezado(ComprobanteData d, PdfWriter writer)
        {
            PdfPTable table = new PdfPTable(2) { WidthPercentage = 100 };
            table.SetWidths(new float[] { 30, 70 }); 

            PdfPCell cellLogo = new PdfPCell()
            {
                BorderWidth = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                PaddingBottom = 10,
                PaddingRight = 10
            };
            cellLogo.AddElement(GetLogo(writer));
            table.AddCell(cellLogo);

            PdfPCell cellDatos = new PdfPCell()
            {
                BorderWidth = 0,
                Padding = 5,
                PaddingRight = 50,
                PaddingLeft = 50,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            cellDatos.AddElement(new Phrase(
                string.Format("NRO. COMPROBANTE: {0}  -  FECHA EMISIÓN: {1}",
                d.NroCedulon , DateTime.Now.ToShortDateString()), _standardFont));

            cellDatos.AddElement(GetCodigoBarrasCedulon(d, writer));

            table.AddCell(cellDatos);

            return table;
        }

        private static IElement GetCodigoBarrasCedulon(ComprobanteData d, PdfWriter writer)
        {
            try
            {
                string strCadena = string.Format("0{0}{1}{2}0010",
                    d.NroCedulon.ToString().PadLeft(10, '0'),
                    d.Importe.ToString("F2").Replace(".", "").Replace(",", ""),
                    d.Vencimiento.ToString());

                PdfContentByte cb = writer.DirectContent;
                Barcode128 code25 = new Barcode128();
                code25.GenerateChecksum = true;
                code25.Code = strCadena;

                return code25.CreateImageWithBarcode(cb, null, null);
            }
            catch (Exception ex)
            {
                return new Phrase("Error generando código de barras");
            }
        }


        private static IElement GetLogo(PdfWriter writer)
        {
            try
            {
                Uri url = new Uri("https://vecino.villaallende.gov.ar/App_Themes/images/logo_horizontal.jpg");
                Image png = Image.GetInstance(url);
                return png;
            }
            catch (Exception ex)
            {
                return new Phrase("Logo no disponible");
            }
        }


        private static PdfPTable GetTitular(ComprobanteData d, int subsistema)
        {
            PdfPTable table = new PdfPTable(4)
            {
                SpacingBefore = 0,
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 5, 55, 20, 20 });

            PdfPCell cellLogoTitular = new PdfPCell()
            {
                BorderWidth = 0,
                PaddingBottom = 5,
                PaddingTop = 5
            };
            cellLogoTitular.AddElement(GetLogoTitular());
            table.AddCell(cellLogoTitular);

            PdfPCell cellPropietario = new PdfPCell()
            {
                BorderWidth = 0,
                PaddingBottom = 5,
                PaddingTop = 5
            };
            cellPropietario.AddElement(GetNombreTitular(d.ContribuyenteNombre));
            cellPropietario.AddElement(salto);
            cellPropietario.AddElement(GetCuitFormateado(d.ContribuyenteCuit.ToString()));
            table.AddCell(cellPropietario);

            PdfPCell cellEtiquetas = new PdfPCell()
            {
                BorderWidth = 0,
                PaddingBottom = 5,
                PaddingTop = 5,
                HorizontalAlignment = PdfPCell.ALIGN_LEFT
            };
            cellEtiquetas.AddElement(GetEtiquetaVencimiento());
            cellEtiquetas.AddElement(salto);
            cellEtiquetas.AddElement(GetEtiquetaTotal());
            table.AddCell(cellEtiquetas);

            PdfPCell cellValores = new PdfPCell()
            {
                BorderWidth = 0,
                PaddingBottom = 5,
                PaddingTop = 5,
                HorizontalAlignment = PdfPCell.ALIGN_RIGHT
            };
            cellValores.AddElement(GetFechaVencimiento(d.Vencimiento));
            cellValores.AddElement(salto);
            cellValores.AddElement(GetMontoTotal(d.Importe));
            table.AddCell(cellValores);

            return table;
        }

        private static IElement GetLogoTitular()
        {
            try
            {
                Image pngTitular = Image.GetInstance("https://vecino.villaallende.gov.ar/img/usuario.png");
                return pngTitular;
            }
            catch (Exception)
            {
                return new Phrase(""); 
            }
        }

        private static IElement GetNombreTitular(string nombre)
        {
            Paragraph paragraph = new Paragraph();
            paragraph.Font = new Font(FontFactory.GetFont("Arial", 10, Font.BOLD));
            paragraph.Add(nombre);
            return paragraph;
        }

        // Método auxiliar para formatear el CUIT
        private static IElement GetCuitFormateado(string cuit)
        {
            try
            {
                if (cuit.Length > 5)
                {
                    string cuitFormateado = string.Format("CUIT: {0}-{1}-{2}",
                        cuit.Substring(0, 2),
                        cuit.Substring(2, 8),
                        cuit.Substring(10, 1));
                    return new Phrase(cuitFormateado, _standardFont);
                }
                else
                {
                    return new Phrase(cuit, _standardFont);
                }
            }
            catch (Exception)
            {
                return new Phrase(cuit, _standardFont);
            }
        }

        // Método auxiliar para la etiqueta de vencimiento
        private static IElement GetEtiquetaVencimiento()
        {
            Paragraph paragraph = new Paragraph();
            paragraph.Font = _standardFont;
            paragraph.Alignment = Element.ALIGN_LEFT;
            paragraph.Add("VENCIMIENTO:");
            return paragraph;
        }

        // Método auxiliar para la etiqueta de total
        private static IElement GetEtiquetaTotal()
        {
            Paragraph paragraph = new Paragraph();
            paragraph.Font = _standardFont;
            paragraph.Alignment = Element.ALIGN_LEFT;
            paragraph.Add("TOTAL A PAGAR:");
            return paragraph;
        }

        // Método auxiliar para la fecha de vencimiento
        private static IElement GetFechaVencimiento(DateTime vencimiento)
        {
            Paragraph paragraph = new Paragraph();
            paragraph.Font = _standardFont;
            paragraph.Alignment = Element.ALIGN_RIGHT;
            paragraph.Add(string.Format("{0:d}", vencimiento));
            return paragraph;
        }


        private static PdfPTable GetBien(ComprobanteData d, int subsistema)
        {
            PdfPTable table = new PdfPTable(2)
            {
                SpacingBefore = 0,
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 5, 95 });

            // Logo del bien según subsistema
            PdfPCell cellLogoBien = new PdfPCell()
            {
                BorderWidth = 0,
                PaddingBottom = 5,
                PaddingTop = 5,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = BaseColor.LIGHT_GRAY
            };
            table.AddCell(cellLogoBien);

            // Información del bien (solo nombre completo)
            PdfPCell cellBien = new PdfPCell()
            {
                BorderWidth = 0,
                PaddingBottom = 5,
                PaddingTop = 0,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                BackgroundColor = BaseColor.LIGHT_GRAY
            };
            cellBien.AddElement(GetInformacionBien(d));
            table.AddCell(cellBien);

            return table;
        }

        private static PdfPTable GetDetalleTitulo()
        {
            PdfPTable table = new PdfPTable(1)
            {
                SpacingBefore = 10,
                SpacingAfter = 10,
                WidthPercentage = 100
            };

            PdfPCell cellTitulo = new PdfPCell()
            {
                BorderWidth = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            cellTitulo.AddElement(GetTituloDetalle());
            table.AddCell(cellTitulo);

            return table;
        }

        private static PdfPTable GetDetalleSimple(ComprobanteData d)
        {
            PdfPTable table = new PdfPTable(2)
            {
                SpacingBefore = 5,
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 70, 30 });

            // Concepto
            PdfPCell cellConcepto = new PdfPCell()
            {
                BorderWidth = 0,
                BorderWidthTop = 1f,
                BorderWidthBottom = 1f,
                BorderWidthLeft = 1f,
                Padding = 8,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };
            cellConcepto.AddElement(GetConcepto(d));
            table.AddCell(cellConcepto);

            // Monto
            PdfPCell cellMonto = new PdfPCell()
            {
                BorderWidth = 0,
                BorderWidthTop = 1f,
                BorderWidthBottom = 1f,
                BorderWidthRight = 1f,
                Padding = 8,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };
            cellMonto.AddElement(GetMontoTotal(d.Importe));
            table.AddCell(cellMonto);

            return table;
        }

        private static PdfPTable GetObservaciones(string observaciones)
        {
            if (string.IsNullOrWhiteSpace(observaciones))
                return new PdfPTable(1) { WidthPercentage = 100 }; // Tabla vacía si no hay observaciones

            PdfPTable table = new PdfPTable(1)
            {
                SpacingBefore = 10,
                WidthPercentage = 100
            };

            // Título de observaciones
            PdfPCell cellTituloObs = new PdfPCell()
            {
                BorderWidth = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                PaddingBottom = 5
            };
            cellTituloObs.AddElement(GetTituloObservaciones());
            table.AddCell(cellTituloObs);

            // Contenido de observaciones
            PdfPCell cellObservaciones = new PdfPCell()
            {
                BorderWidth = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_TOP,
                Padding = 5
            };
            cellObservaciones.AddElement(GetContenidoObservaciones(observaciones));
            table.AddCell(cellObservaciones);

            return table;
        }

        private static PdfPTable GetNota(bool anual, bool tieneDescuentoDecreto)
        {
            PdfPTable table = new PdfPTable(1)
            {
                SpacingBefore = 5,
                WidthPercentage = 100
            };

            PdfPCell cellNota = new PdfPCell()
            {
                BorderWidth = 0,
                PaddingBottom = 5,
                PaddingTop = 15,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };

            cellNota.AddElement(GetMensajeNota(anual, tieneDescuentoDecreto));
            table.AddCell(cellNota);

            return table;
        }      

        private static IElement GetInformacionBien(ComprobanteData d)
        {
            // Solo mostramos el nombre completo (nombre + apellido)
            Paragraph paragraph = new Paragraph();
            paragraph.Font = new Font(FontFactory.GetFont("Arial", 10, Font.NORMAL));

            string informacion = d.ContribuyenteNombre.Trim();

            paragraph.Add(informacion);
            return paragraph;
        }

        private static IElement GetTituloDetalle()
        {
            Paragraph paragraph = new Paragraph();
            paragraph.Font = new Font(FontFactory.GetFont("Arial", 10, Font.BOLD));
            paragraph.Alignment = Element.ALIGN_LEFT;
            paragraph.Add("DETALLE DEUDA");
            return paragraph;
        }

        private static IElement GetConcepto(ComprobanteData d)
        {
            Paragraph paragraph = new Paragraph();
            paragraph.Font = GetStandardFont();
            paragraph.Add(d.Concepto); // El concepto va en denominacion
            return paragraph;
        }

        private static IElement GetMontoTotal(decimal monto)
        {
            Paragraph paragraph = new Paragraph();
            paragraph.Font = GetStandardFont();
            paragraph.Alignment = Element.ALIGN_RIGHT;
            paragraph.Add(string.Format("{0:c}", monto));
            return paragraph;
        }

        private static IElement GetTituloObservaciones()
        {
            Paragraph paragraph = new Paragraph();
            paragraph.Font = new Font(FontFactory.GetFont("Arial", 9, Font.BOLD));
            paragraph.Alignment = Element.ALIGN_LEFT;
            paragraph.Add("OBSERVACIONES:");
            return paragraph;
        }

        private static IElement GetContenidoObservaciones(string observaciones)
        {
            Paragraph contenedor = new Paragraph();
            contenedor.SpacingBefore = 10f;

            if (!string.IsNullOrWhiteSpace(observaciones))
            {
                using (var htmlReader = new StringReader(observaciones))
                {
                    var styles = new StyleSheet();
                    styles.LoadTagStyle("body", "size", "8pt");  
                    styles.LoadTagStyle("p", "size", "8pt");
                    styles.LoadTagStyle("span", "size", "8pt");
                    var elements =HTMLWorker.ParseToList(htmlReader, styles);
                    foreach (var element in elements)
                    {
                        contenedor.Add(element);
                    }
                }
            }

            return contenedor;
        }

        private static IElement GetMensajeNota(bool anual, bool tieneDescuentoDecreto)
        {
            string mensaje = ObtenerMensajeSegunTipo(anual, tieneDescuentoDecreto);

            Paragraph paragraph = new Paragraph();
            paragraph.Font = new Font(FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.GRAY));
            paragraph.Alignment = Element.ALIGN_LEFT;
            paragraph.Add(mensaje);
            return paragraph;
        }

        private static string ObtenerMensajeSegunTipo(bool anual, bool tieneDescuentoDecreto)
        {
            try
            {
                if (anual)
                {
                    return "DE HABER OPTADO POR EL PAGO ANUAL, EN CASO DE CAMBIO DE RADICACION O BAJA , NO CORRESPONDERA  REINTEGRO.";
                }
                else
                {
                    string msjDecreto = tieneDescuentoDecreto
                        ? ObtenerConfiguracion("mensajeDecretoMulta")
                        : ObtenerConfiguracion("mensajeDecreto");

                    return msjDecreto;
                }
            }
            catch (Exception)
            {
                return "Mensaje no disponible";
            }
        }

        private static string ObtenerConfiguracion(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key]?.ToString() ?? "Configuración no encontrada";
            }
            catch (Exception)
            {
                return "Error al obtener configuración";
            }
        }

        private static Font GetStandardFont()
        {
            return FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
        }



        private static PdfPTable GetDatosConcepto(ComprobanteData d, int nroTransaccion)
        {
            PdfPTable table = new PdfPTable(2) { WidthPercentage = 100 };
            table.SetWidths(new float[] { 70, 30 });

            // Concepto
            PdfPCell cellConcepto = new PdfPCell(new Paragraph(d.Concepto?.Trim().ToUpper() ?? "CONCEPTO", _encabezado))
            {
                BorderWidth = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                PaddingBottom = 10,
                PaddingTop = 10
            };
            table.AddCell(cellConcepto);

            // Link de pago (si corresponde)
            if (d.Concepto?.Trim().ToUpper() == "DERECHO DE INSCRIPCION E INSPECCION COMERCIAL")
            {
                string url = $"https://vecino.villaallende.gov.ar/PagosOnLine/CertificadosHabilitacion.aspx?nro_transaccion={nroTransaccion}";
                var linkFont = new Font(_encabezado.BaseFont, 12, Font.UNDERLINE, BaseColor.BLUE);
                var anchor = new Anchor("💳 PAGÁ ACÁ ➜", linkFont) { Reference = url };
                var phrase = new Phrase();
                phrase.Add(anchor);

                PdfPCell cellLink = new PdfPCell(phrase)
                {
                    BorderWidth = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    PaddingBottom = 10,
                    PaddingTop = 10
                };
                table.AddCell(cellLink);
            }
            else
            {
                table.AddCell(new PdfPCell(new Paragraph("", _standardFont)) { BorderWidth = 0 });
            }

            // Fecha de impresión
            table.AddCell(new PdfPCell(new Paragraph($"Fecha de Impresión: {d.FechaImpresion:dd/MM/yyyy}", _standardFont))
            {
                BorderWidth = 0,
                Colspan = 2,
                PaddingTop = 10,
                PaddingBottom = 5
            });

            return table;
        }

        private static PdfPTable GetDetalle(ComprobanteData d, CultureInfo ar)
        {
            PdfPTable table = new PdfPTable(2) { WidthPercentage = 60, HorizontalAlignment = Element.ALIGN_LEFT };
            table.SetWidths(new float[] { 50, 50 });

            // Encabezados
            PdfPCell cellVencimiento = new PdfPCell(new Paragraph("VENCIMIENTO", _subtitulo))
            {
                BorderWidth = 1,
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 8,
                PaddingTop = 8,
                BackgroundColor = BaseColor.LIGHT_GRAY
            };
            table.AddCell(cellVencimiento);

            PdfPCell cellImporte = new PdfPCell(new Paragraph("IMPORTE", _subtitulo))
            {
                BorderWidth = 1,
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 8,
                PaddingTop = 8,
                BackgroundColor = BaseColor.LIGHT_GRAY
            };
            table.AddCell(cellImporte);

            // Valores
            PdfPCell cellVencimientoValor = new PdfPCell(new Paragraph(d.Vencimiento.ToString("dd/MM/yyyy"), _encabezado))
            {
                BorderWidth = 1,
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 12,
                PaddingTop = 12
            };
            table.AddCell(cellVencimientoValor);

            PdfPCell cellImporteValor = new PdfPCell(new Paragraph(d.Importe.ToString("$ #,##0.00", ar), _encabezado))
            {
                BorderWidth = 1,
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 12,
                PaddingTop = 12
            };
            table.AddCell(cellImporteValor);

            return table;
        }

        private static IElement GetCodigoBarras(ComprobanteData d, PdfWriter writer)
        {
            // Crear un contenedor para el número de cedulón y el código de barras
            var contenedor = new Paragraph();
            contenedor.Alignment = Element.ALIGN_RIGHT;

            // Agregar número de cedulón arriba con el mismo tamaño que el número del código de barras
            if (!string.IsNullOrWhiteSpace(d.NroCedulon))
            {
                var parrafoCedulon = new Paragraph($"Nº Cedulón: {d.NroCedulon}", _standardFont);
                parrafoCedulon.Alignment = Element.ALIGN_RIGHT;
                contenedor.Add(parrafoCedulon);
                contenedor.Add(new Chunk("\n", _standardFont));
            }

            // Agregar código de barras si existe
            if (!string.IsNullOrWhiteSpace(d.CodigoBarraLargo))
            {
                try
                {
                    var imgBarraLarga = CrearBarcodeAuto(d.CodigoBarraLargo, writer, 40f, 0.9f, true);
                    if (imgBarraLarga != null)
                    {
                        imgBarraLarga.Alignment = Element.ALIGN_RIGHT;
                        imgBarraLarga.ScalePercent(70f); // Reducido para que no se salga de la página
                        contenedor.Add(imgBarraLarga);
                    }
                }
                catch (Exception)
                {
                    // Si falla, devolver texto plano
                    var parrafoError = new Paragraph($"Código: {d.CodigoBarraLargo}", _standardFont);
                    parrafoError.Alignment = Element.ALIGN_RIGHT;
                    contenedor.Add(parrafoError);
                }
            }

            if (contenedor.Count == 0)
            {
                return new Paragraph("", _standardFont);
            }

            return contenedor;
        }

        private static IElement GetObservaciones(ComprobanteData d)
        {
            var container = new PdfPTable(1) { WidthPercentage = 100 };

            // Título
            PdfPCell titleCell = new PdfPCell(new Paragraph("OBSERVACIONES:", _subtitulo))
            {
                BorderWidth = 0,
                PaddingBottom = 5,
                PaddingTop = 10
            };
            container.AddCell(titleCell);

            try
            {
                // Intentar procesar HTML
                using (var htmlReader = new StringReader(d.Observaciones))
                {
                    var htmlElements = HTMLWorker.ParseToList(htmlReader, null);
                    foreach (var htmlElement in htmlElements)
                    {
                        PdfPCell contentCell = new PdfPCell()
                        {
                            BorderWidth = 0,
                            PaddingBottom = 5,
                            PaddingTop = 2
                        };
                        contentCell.AddElement(htmlElement);
                        container.AddCell(contentCell);
                    }
                }
            }
            catch (Exception)
            {
                // Si falla el HTML, mostrar como texto plano
                PdfPCell contentCell = new PdfPCell(new Paragraph(d.Observaciones, _standardFont))
                {
                    BorderWidth = 0,
                    PaddingBottom = 5,
                    PaddingTop = 2
                };
                container.AddCell(contentCell);
            }

            return container;
        }

        private static Paragraph GetLineaDivisoria()
        {
            var linea = new Paragraph("─────────────────────  CUPÓN MUNICIPALIDAD  /  CUPÓN CONTRIBUYENTE  ─────────────────────", _standardFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingBefore = 15,
                SpacingAfter = 10
            };
            return linea;
        }

        //private static PdfPTable GetCupones(ComprobanteData d, PdfWriter writer, CultureInfo ar)
        //{
        //    PdfPTable cupones = new PdfPTable(2) { WidthPercentage = 100 };
        //    cupones.SetWidths(new float[] { 50, 50 });

        //    // Cupón Municipalidad
        //    cupones.AddCell(CrearCupon("CUPÓN MUNICIPALIDAD", d, writer, ar));

        //    // Cupón Contribuyente  
        //    cupones.AddCell(CrearCupon("CUPÓN CONTRIBUYENTE", d, writer, ar));

        //    return cupones;
        //}

        //private static PdfPCell CrearCupon(string titulo, ComprobanteData d, PdfWriter writer, CultureInfo ar)
        //{
        //    PdfPTable cupon = new PdfPTable(1) { WidthPercentage = 100 };

        //    // Título del cupón
        //    PdfPCell titleCell = new PdfPCell(new Paragraph(titulo, _subtitulo))
        //    {
        //        BorderWidth = 1,
        //        HorizontalAlignment = Element.ALIGN_CENTER,
        //        PaddingBottom = 8,
        //        PaddingTop = 8,
        //        BackgroundColor = BaseColor.LIGHT_GRAY
        //    };
        //    cupon.AddCell(titleCell);

        //    // Datos del contribuyente
        //    cupon.AddCell(new PdfPCell(new Paragraph(d.ContribuyenteNombre, _standardFont))
        //    {
        //        BorderWidth = 1,
        //        BorderWidthTop = 0,
        //        PaddingLeft = 5,
        //        PaddingBottom = 3,
        //        PaddingTop = 3
        //    });

        //    cupon.AddCell(new PdfPCell(new Paragraph($"CUIT: {d.ContribuyenteCuit}", _standardFont))
        //    {
        //        BorderWidth = 1,
        //        BorderWidthTop = 0,
        //        PaddingLeft = 5,
        //        PaddingBottom = 3,
        //        PaddingTop = 3
        //    });

        //    cupon.AddCell(new PdfPCell(new Paragraph(d.Domicilio, _standardFont))
        //    {
        //        BorderWidth = 1,
        //        BorderWidthTop = 0,
        //        PaddingLeft = 5,
        //        PaddingBottom = 3,
        //        PaddingTop = 3
        //    });

        //    cupon.AddCell(new PdfPCell(new Paragraph(d.Localidad, _standardFont))
        //    {
        //        BorderWidth = 1,
        //        BorderWidthTop = 0,
        //        PaddingLeft = 5,
        //        PaddingBottom = 8,
        //        PaddingTop = 3
        //    });

        //    // Tabla vencimiento/importe del cupón
        //    PdfPTable tablaVencImporte = new PdfPTable(2);
        //    tablaVencImporte.SetWidths(new float[] { 50, 50 });

        //    tablaVencImporte.AddCell(new PdfPCell(new Paragraph("Vencimiento", _subtitulo))
        //    {
        //        BorderWidth = 1,
        //        HorizontalAlignment = Element.ALIGN_CENTER,
        //        PaddingBottom = 5,
        //        PaddingTop = 5
        //    });

        //    tablaVencImporte.AddCell(new PdfPCell(new Paragraph("Importe", _subtitulo))
        //    {
        //        BorderWidth = 1,
        //        HorizontalAlignment = Element.ALIGN_CENTER,
        //        PaddingBottom = 5,
        //        PaddingTop = 5
        //    });

        //    tablaVencImporte.AddCell(new PdfPCell(new Paragraph(d.Vencimiento.ToString("dd/MM/yyyy"), _encabezado))
        //    {
        //        BorderWidth = 1,
        //        HorizontalAlignment = Element.ALIGN_CENTER,
        //        PaddingBottom = 10,
        //        PaddingTop = 10
        //    });

        //    tablaVencImporte.AddCell(new PdfPCell(new Paragraph(d.Importe.ToString("$ #,##0.00", ar), _encabezado))
        //    {
        //        BorderWidth = 1,
        //        HorizontalAlignment = Element.ALIGN_CENTER,
        //        PaddingBottom = 10,
        //        PaddingTop = 10
        //    });

        //    cupon.AddCell(new PdfPCell(tablaVencImporte)
        //    {
        //        BorderWidth = 1,
        //        BorderWidthTop = 0,
        //        Padding = 0
        //    });

        //    // Código de barras corto si existe
        //    if (!string.IsNullOrWhiteSpace(d.CodigoBarraCorto))
        //    {
        //        try
        //        {
        //            var imgBarraCorta = CrearBarcodeCode39Or128(d.CodigoBarraCorto, writer, 36f, 0.9f, true);
        //            if (imgBarraCorta != null)
        //            {
        //                PdfPCell cellImg = new PdfPCell(imgBarraCorta)
        //                {
        //                    BorderWidth = 1,
        //                    BorderWidthTop = 0,
        //                    HorizontalAlignment = Element.ALIGN_CENTER,
        //                    PaddingBottom = 5,
        //                    PaddingTop = 5
        //                };
        //                cupon.AddCell(cellImg);
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            // Si falla el código de barras, agregar texto
        //            cupon.AddCell(new PdfPCell(new Paragraph(d.CodigoBarraCorto, _standardFont))
        //            {
        //                BorderWidth = 1,
        //                BorderWidthTop = 0,
        //                HorizontalAlignment = Element.ALIGN_CENTER,
        //                PaddingBottom = 5,
        //                PaddingTop = 5
        //            });
        //        }
        //    }

        //    // Número de cedulón
        //    cupon.AddCell(new PdfPCell(new Paragraph(d.NroCedulon, _encabezado))
        //    {
        //        BorderWidth = 1,
        //        BorderWidthTop = 0,
        //        HorizontalAlignment = Element.ALIGN_CENTER,
        //        PaddingBottom = 8,
        //        PaddingTop = 8
        //    });

        //    return new PdfPCell(cupon)
        //    {
        //        BorderWidth = 0,
        //        Padding = 5
        //    };
        //}

        // Métodos helper para códigos de barra (copiados del código original)
        private static Image CrearBarcodeAuto(string data, PdfWriter writer, float alto, float anchoX, bool textoVisible)
        {
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

        private static bool EsNumerico(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            foreach (var ch in s)
                if (ch < '0' || ch > '9') return false;
            return true;
        }



        private static PdfPTable GetDetalle(ComprobanteData d, int nroTransaccion)
        {
            PdfPTable table = new PdfPTable(3) { WidthPercentage = 100 };
            table.SetWidths(new float[] { 65, 20, 15 });

            AgregarEncabezadosDetalle(table);
            AgregarFilasDetalle(table, d,nroTransaccion);
            AgregarFilaTotales(table, d);

            return table;
        }

        private static void AgregarEncabezadosDetalle(PdfPTable table)
        {
            string[] encabezados = {  "Concepto", "Monto Original", "Total" };

            foreach (string encabezado in encabezados)
            {
                PdfPCell cell = new PdfPCell(new Paragraph(encabezado, _standardFont))
                {
                    BorderWidth = 0,
                    BorderWidthBottom = 1f,
                    PaddingBottom = 10,
                    PaddingTop = 10,
                    PaddingLeft = 5
                };
                table.AddCell(cell);
            }
        }


        private static void AgregarFilasDetalle(PdfPTable table, ComprobanteData d, int nroTransaccion)
        {

            if (d.Concepto?.Trim().ToUpper() == "DERECHO DE INSCRIPCION E INSPECCION COMERCIAL")
            {
                string url = $"https://vecino.villaallende.gov.ar/PagosOnLine/CertificadosHabilitacion.aspx?nro_transaccion={nroTransaccion}";

                // Fuente normal para el concepto
                Font conceptoFont = new Font(_standardFont.BaseFont, 9, Font.NORMAL);
                Phrase phrase = new Phrase();
                phrase.Add(new Chunk(d.Concepto + "  ", conceptoFont)); // 🔹 Texto del concepto

                // Fuente azul y subrayado para el link
                Font linkFont = new Font(_standardFont.BaseFont, 9, Font.UNDERLINE, BaseColor.BLUE);
                Anchor anchor = new Anchor("💳 PAGÁ ACÁ ➜", linkFont)
                {
                    Reference = url
                };
                phrase.Add(anchor);

                PdfPCell cellConcepto = new PdfPCell(phrase)
                {
                    BorderWidth = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT
                };
                table.AddCell(cellConcepto);
            }
            else
            {
                // Si no cumple condición, celda normal
                table.AddCell(CrearCeldaDetalle(d.Concepto));
            }

            //table.AddCell(CrearCeldaDetalle(d.Concepto));

            // Monto Original
            table.AddCell(CrearCeldaDetalle(string.Format("{0:c}", d.Importe)));

                // Total
                table.AddCell(CrearCeldaDetalle(string.Format("{0:c}", d.Importe)));
            
        }

        private static void AgregarFilaTotales(PdfPTable table, ComprobanteData d )
        {
            // Periodo - "TOTAL"
            PdfPCell cellTotalLabel = new PdfPCell()
            {
                BorderWidth = 0,
                BorderWidthTop = 1f,
                PaddingBottom = 0,
                PaddingTop = 0,
                PaddingLeft = 5
            };
            cellTotalLabel.AddElement(GetTextoNegrita("TOTAL"));
            table.AddCell(cellTotalLabel);

            // Concepto - Vacío
            table.AddCell(CrearCeldaTotal(" "));

            table.AddCell(CrearCeldaTotalConValor(string.Format("{0:c}", d.Importe)));

            table.AddCell(CrearCeldaTotalConValor(string.Format("{0:c}", d.Importe)));
        }

        private static PdfPCell CrearCeldaDetalle(string texto)
        {
            return new PdfPCell(new Paragraph(texto, _standardFont))
            {
                BorderWidth = 0
            };
        }

        private static PdfPCell CrearCeldaTotal(string texto)
        {
            return new PdfPCell(new Paragraph(texto, _standardFont))
            {
                BorderWidth = 0,
                BorderWidthTop = 1f,
                PaddingBottom = 0,
                PaddingTop = 0
            };
        }

        private static PdfPCell CrearCeldaTotalConValor(string valor)
        {
            PdfPCell cell = new PdfPCell()
            {
                BorderWidth = 0,
                BorderWidthTop = 1f,
                PaddingLeft = 5,
                PaddingBottom = 0,
                PaddingTop = 0
            };

            cell.AddElement(GetTextoNegrita(valor));
            return cell;
        }

        private static IElement GetTextoNegrita(string texto)
        {
            Paragraph paragraph = new Paragraph();
            paragraph.Font = new Font(FontFactory.GetFont("Arial", 10, Font.BOLD));
            paragraph.Add(texto);
            return paragraph;
        }

     

    }
}