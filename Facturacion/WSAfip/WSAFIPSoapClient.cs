// Decompiled with JetBrains decompiler
// Type: Facturacion.WSAfip.WSAFIPSoapClient
// Assembly: Facturacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A9493007-1D65-4194-8547-961B9C83CD9E
// Assembly location: H:\NET 2018\Facturacion\bin\Facturacion.dll

using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Facturacion.WSAfip
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  public class WSAFIPSoapClient : ClientBase<WSAFIPSoap>, WSAFIPSoap
  {
    public WSAFIPSoapClient()
    {
    }

    public WSAFIPSoapClient(string endpointConfigurationName)
      : base(endpointConfigurationName)
    {
    }

    public WSAFIPSoapClient(string endpointConfigurationName, string remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public WSAFIPSoapClient(string endpointConfigurationName, EndpointAddress remoteAddress)
      : base(endpointConfigurationName, remoteAddress)
    {
    }

    public WSAFIPSoapClient(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    public personaReturn getPersona(
      string dirProxy,
      string proxyUser,
      string proxyPassword,
      long CUIT)
    {
      return this.Channel.getPersona(dirProxy, proxyUser, proxyPassword, CUIT);
    }

    public Task<personaReturn> getPersonaAsync(
      string dirProxy,
      string proxyUser,
      string proxyPassword,
      long CUIT)
    {
      return this.Channel.getPersonaAsync(dirProxy, proxyUser, proxyPassword, CUIT);
    }
  }
}
