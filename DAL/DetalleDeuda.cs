// Decompiled with JetBrains decompiler
// Type: DAL.DetalleDeuda
// Assembly: DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71BBCA04-FA76-4CA8-B211-6F77771DC570
// Assembly location: H:\NET 2018\Facturacion\bin\DAL.dll

using System;

namespace DAL
{
  public class DetalleDeuda
  {
    public string periodo { get; set; }

    public Decimal debe { get; set; }

    public int nro_transacion { get; set; }

    public DateTime vencimiento { get; set; }

    public int categoria_deuda { get; set; }

    public Decimal monto_original { get; set; }

    public Decimal recargo { get; set; }
  }
}
