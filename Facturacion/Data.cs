// Decompiled with JetBrains decompiler
// Type: Facturacion.Data
// Assembly: Facturacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A9493007-1D65-4194-8547-961B9C83CD9E
// Assembly location: H:\NET 2018\Facturacion\bin\Facturacion.dll

using System;
using System.Collections.Generic;

namespace Facturacion
{
  public class Data
  {
    public long CUIT { get; set; }

    public long idPersona { get; set; }

    public string tipoPersona { get; set; }

    public string tipoClave { get; set; }

    public string estadoClave { get; set; }

    public string nombre { get; set; }

    public string tipoDocumento { get; set; }

    public string numeroDocumento { get; set; }

    public DomicilioFiscal domicilioFiscal { get; set; }

    public int idDependencia { get; set; }

    public int mesCierre { get; set; }

    public string fechaInscripcion { get; set; }

    public IList<int> actividades { get; set; }

    public string telefono { get; set; }

    public string mail { get; set; }

    public DateTime Fecha_Nac { get; set; }

    public string sexo { get; set; }
  }
}
