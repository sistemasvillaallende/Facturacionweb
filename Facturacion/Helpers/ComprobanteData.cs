using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facturacion.Helpers
{
    public class ComprobanteData
    {
        public string Municipio { get; set; }
        public string NroCedulon { get; set; }
        public string Concepto { get; set; }
        public string ContribuyenteNombre { get; set; }
        public long ContribuyenteCuit { get; set; }
        public string Domicilio { get; set; }
        public string Localidad { get; set; }
        public DateTime FechaImpresion { get; set; }
        public DateTime Vencimiento { get; set; }
        public decimal Importe { get; set; }
        public string CodigoBarraLargo { get; set; }
        public string CodigoBarraCorto { get; set; }
    }
}