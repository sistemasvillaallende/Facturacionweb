// Decompiled with JetBrains decompiler
// Type: DAL.MD5Encryption
// Assembly: DAL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71BBCA04-FA76-4CA8-B211-6F77771DC570
// Assembly location: H:\NET 2018\Facturacion\bin\DAL.dll

using System.Security.Cryptography;
using System.Text;

namespace DAL
{
  public class MD5Encryption
  {
    public static string EncryptMD5(string pass)
    {
      byte[] hash = MD5.Create().ComputeHash(Encoding.Default.GetBytes(pass));
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < hash.Length; ++index)
        stringBuilder.AppendFormat("{0:x2}", (object) hash[index]);
      return stringBuilder.ToString();
    }
  }
}
