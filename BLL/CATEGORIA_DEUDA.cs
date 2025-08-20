// Decompiled with JetBrains decompiler
// Type: BLL.CATEGORIA_DEUDA
// Assembly: BLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34EC4AF4-6E2A-4DA8-8439-6E4212AC4B99
// Assembly location: H:\NET 2018\Facturacion\bin\BLL.dll

using System;
using System.Collections.Generic;

namespace BLL
{
  public class CATEGORIA_DEUDA
  {
    public static List<DAL.CATEGORIA_DEUDA> getByOficina(int cod)
    {
      try
      {
        return DAL.CATEGORIA_DEUDA.getByOficina(cod);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
