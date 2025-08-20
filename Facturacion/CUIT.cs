// Decompiled with JetBrains decompiler
// Type: Facturacion.CUIT
// Assembly: Facturacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A9493007-1D65-4194-8547-961B9C83CD9E
// Assembly location: H:\NET 2018\Facturacion\bin\Facturacion.dll

using System.Collections.Generic;

namespace Facturacion
{
  public class CUIT
  {
    public bool success { get; set; }

    public string dominio { get; set; }

    public string dni { get; set; }

    public IList<long> data { get; set; }

    private string cuit { get; set; }
  }
}
