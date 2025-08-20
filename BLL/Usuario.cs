// Decompiled with JetBrains decompiler
// Type: BLL.Usuario
// Assembly: BLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34EC4AF4-6E2A-4DA8-8439-6E4212AC4B99
// Assembly location: H:\NET 2018\Facturacion\bin\BLL.dll

using System;

namespace BLL
{
  public class Usuario
  {
    public static DAL.Usuario ValidUser(string user, string password)
    {
      try
      {
        return DAL.Usuario.ValidUser(user, password);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static DAL.Usuario getbyPk(int cod_usuario)
    {
      try
      {
        return DAL.Usuario.getByPk(cod_usuario);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
