// Decompiled with JetBrains decompiler
// Type: Facturacion.WSAfip.WSAFIPSoap
// Assembly: Facturacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A9493007-1D65-4194-8547-961B9C83CD9E
// Assembly location: H:\NET 2018\Facturacion\bin\Facturacion.dll

using System.CodeDom.Compiler;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Facturacion.WSAfip
{
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  [ServiceContract(ConfigurationName = "WSAfip.WSAFIPSoap")]
  public interface WSAFIPSoap
  {
    [OperationContract(Action = "http://tempuri.org/getPersona", ReplyAction = "*")]
    [XmlSerializerFormat(SupportFaults = true)]
    personaReturn getPersona(
      string dirProxy,
      string proxyUser,
      string proxyPassword,
      long CUIT);

    [OperationContract(Action = "http://tempuri.org/getPersona", ReplyAction = "*")]
    Task<personaReturn> getPersonaAsync(
      string dirProxy,
      string proxyUser,
      string proxyPassword,
      long CUIT);
  }
}
