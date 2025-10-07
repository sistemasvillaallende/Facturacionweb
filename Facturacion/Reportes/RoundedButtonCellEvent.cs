using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Facturacion.Helpers
{
    public class RoundedButtonCellEvent : IPdfPCellEvent
    {
        private readonly BaseColor _fill;
        private readonly float _radius;
        private readonly string _url;

        public RoundedButtonCellEvent(BaseColor fill, float radius, string url)
        {
            _fill = fill; _radius = radius; _url = url;
        }
        public void CellLayout(PdfPCell cell, Rectangle rect, PdfContentByte[] canvas)
        {
            // Fondo redondeado
            var bg = canvas[PdfPTable.BACKGROUNDCANVAS];
            bg.SaveState();
            bg.SetColorFill(_fill);
            bg.RoundRectangle(rect.Left, rect.Bottom, rect.Width, rect.Height, _radius);
            bg.Fill();
            bg.RestoreState();

            // Link en toda el área del botón
            var writer = canvas[PdfPTable.TEXTCANVAS].PdfWriter;
            var action = new PdfAction(_url);
            var link = PdfAnnotation.CreateLink(writer, rect, PdfAnnotation.HIGHLIGHT_INVERT, action);
            writer.AddAnnotation(link);
        }
        public static PdfPTable GetBotonAbonarEnLinea(int nroTransaccion)
        {
            string url = $"https://vecino.villaallende.gov.ar/PagosOnLine/CertificadosHabilitacion.aspx?nro_transaccion={nroTransaccion}";

            var container = new PdfPTable(1) { WidthPercentage = 100f, SpacingBefore = 8f, SpacingAfter = 8f };
            var buttonTable = new PdfPTable(1) { WidthPercentage = 70f, HorizontalAlignment = Element.ALIGN_CENTER };

            // Ruta absoluta a /FacturacionWeb/Facturacion/img/icono_pago.png
            string root = HttpRuntime.AppDomainAppPath; // e.g. C:\...\Facturacion\
            string rutaIcono = Path.Combine(root, "img", "icono_pago.png");

            Image icon = null;
            if (File.Exists(rutaIcono))
            {
                icon = Image.GetInstance(rutaIcono);
                icon.ScaleToFit(16f, 16f);
            }

            var fontBtn = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);

            var phrase = new Phrase();
            if (icon != null) phrase.Add(new Chunk(icon, 0, -2, true)); // ícono a la izquierda
            phrase.Add(new Chunk("  Click aquí para abonar en línea", fontBtn));

            var cellBtn = new PdfPCell(phrase)
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                PaddingTop = 12f,
                PaddingBottom = 12f,
                PaddingLeft = 18f,
                PaddingRight = 18f,
                CellEvent = new RoundedButtonCellEvent(new BaseColor(0, 150, 136), 8f, url) // fondo + link
            };

            buttonTable.AddCell(cellBtn);

            container.AddCell(new PdfPCell(buttonTable)
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER
            });

            return container;
        }

    }
}