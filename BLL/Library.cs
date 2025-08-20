// Decompiled with JetBrains decompiler
// Type: BLL.Library
// Assembly: BLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34EC4AF4-6E2A-4DA8-8439-6E4212AC4B99
// Assembly location: H:\NET 2018\Facturacion\bin\BLL.dll

using System;
using System.Text;

namespace BLL
{
  public class Library
  {
    public const string DEFAULT_DATE = "30/12/1899";
    public const string DEFAULT_REQUIRED_FIELD_ERROR = "Por favor rellene el campo $";
    public const string DEFAULT_CUSTOM_ERROR = "El formato del campo $ es ";

    public static string Left(string param, int length)
    {
      return param.Substring(0, length);
    }

    public static string Right(string param, int length)
    {
      return param.Substring(param.Length - length, length);
    }

    public static string Mid(string param, int startIndex, int length)
    {
      return param.Substring(startIndex, length);
    }

    public static string Mid(string param, int startIndex)
    {
      return param.Substring(startIndex);
    }

    public static int Asc(string s)
    {
      return (int) Encoding.ASCII.GetBytes(s)[0];
    }

    public static char Chr(int c)
    {
      return Convert.ToChar(c);
    }

    public static string ConvertToChar(string Input)
    {
      int num1 = (int) Library.Chr(0);
      int num2 = (int) Library.Chr((int) byte.MaxValue);
      string str1 = "";
      string str2 = Input;
      int length1 = str2.Length;
      double num3 = (double) (str2.Length % 2);
      int num4 = length1 / 2;
      if (num3 != 0.0)
        str2 = "0" + str2;
      int length2 = str2.Length;
      char ch;
      for (int index = 1; index <= length2 / 2; ++index)
      {
        string s = Library.Left(str2, 2);
        if (int.Parse(s) < 90)
        {
          string str3 = str1;
          ch = Library.Chr(int.Parse(s) + 33);
          string str4 = ch.ToString();
          str1 = str3 + str4;
        }
        else
        {
          string str3 = str1;
          ch = Library.Chr(int.Parse(s) + 71);
          string str4 = ch.ToString();
          str1 = str3 + str4;
        }
        str2 = Library.Right(str2, str2.Length - 2);
      }
      ch = Library.Chr(171);
      string str5 = ch.ToString();
      string str6 = str1;
      ch = Library.Chr(172);
      string str7 = ch.ToString();
      return str5 + str6 + str7;
    }

    public static string ArmoCBarra(
      string nro_cliente,
      string nro_comprobante,
      Decimal importe_1vto,
      string fecha_1vto,
      Decimal importe_2vto,
      string fecha_2vto,
      string tipo_cedulon)
    {
      char paddingChar = Convert.ToChar("0");
      Decimal num1 = new Decimal();
      int num2 = 0;
      string str1 = "0549";
      string str2 = nro_cliente.PadLeft(9, paddingChar);
      string str3 = nro_comprobante.PadLeft(12, paddingChar);
      string str4 = string.Format("{0:N}", (object) importe_1vto).Replace(".", "").Replace(",", "").PadLeft(8, paddingChar);
      string str5 = Convert.ToDateTime(fecha_1vto).ToShortDateString().Replace("/", "");
      if (importe_2vto > Decimal.Zero)
        num1 = importe_2vto - importe_1vto;
      string str6 = string.Format("{0:N}", (object) num1).Replace(".", "").Replace(",", "").PadLeft(4, paddingChar);
      if (fecha_2vto.Length > 1)
        num2 = Date.GetDaysBetween(fecha_2vto, fecha_1vto);
      string str7 = num2.ToString().PadLeft(2, paddingChar);
      string str8 = tipo_cedulon;
      string CodS = str1 + str2 + str3 + str4 + str5 + str6 + str7 + str8;
      return CodS + Library.Calcula_digito_verificador(CodS).ToString();
    }

    public static int Calcula_digito_verificador(string CodS)
    {
      string str = "1357935793579357935793579357935793579357935793579";
      int length = CodS.Length;
      int[] numArray = new int[length];
      int index1;
      for (index1 = 0; index1 < length; ++index1)
        numArray[index1] = 0;
      Console.Write(index1);
      for (int index2 = 0; index2 < length; ++index2)
        numArray[index2] = Convert.ToInt32(CodS[index2].ToString()) * Convert.ToInt32(str[index2].ToString());
      int num1 = 0;
      for (int index2 = 0; index2 < length; ++index2)
        num1 += numArray[index2];
      int num2 = num1 / 2;
      int startIndex = num2.ToString().Length - 1;
      return Convert.ToInt32(num2.ToString().Substring(startIndex, 1));
    }
  }
}
