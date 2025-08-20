// Decompiled with JetBrains decompiler
// Type: Facturacion.WSAfip.telefono
// Assembly: Facturacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A9493007-1D65-4194-8547-961B9C83CD9E
// Assembly location: H:\NET 2018\Facturacion\bin\Facturacion.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Facturacion.WSAfip
{
  [GeneratedCode("System.Xml", "4.6.1067.0")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://a4.soap.ws.server.puc.sr/")]
  [Serializable]
  public class telefono : INotifyPropertyChanged
  {
    private long numeroField;
    private bool numeroFieldSpecified;
    private string tipoLineaField;
    private string tipoTelefonoField;

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
    public long numero
    {
      get
      {
        return this.numeroField;
      }
      set
      {
        this.numeroField = value;
        this.RaisePropertyChanged(nameof (numero));
      }
    }

    [XmlIgnore]
    public bool numeroSpecified
    {
      get
      {
        return this.numeroFieldSpecified;
      }
      set
      {
        this.numeroFieldSpecified = value;
        this.RaisePropertyChanged(nameof (numeroSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
    public string tipoLinea
    {
      get
      {
        return this.tipoLineaField;
      }
      set
      {
        this.tipoLineaField = value;
        this.RaisePropertyChanged(nameof (tipoLinea));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
    public string tipoTelefono
    {
      get
      {
        return this.tipoTelefonoField;
      }
      set
      {
        this.tipoTelefonoField = value;
        this.RaisePropertyChanged(nameof (tipoTelefono));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
