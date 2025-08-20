// Decompiled with JetBrains decompiler
// Type: Facturacion.WSAfip.metadata
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
  public class metadata : INotifyPropertyChanged
  {
    private DateTime fechaHoraField;
    private bool fechaHoraFieldSpecified;
    private string servidorField;

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
    public DateTime fechaHora
    {
      get
      {
        return this.fechaHoraField;
      }
      set
      {
        this.fechaHoraField = value;
        this.RaisePropertyChanged(nameof (fechaHora));
      }
    }

    [XmlIgnore]
    public bool fechaHoraSpecified
    {
      get
      {
        return this.fechaHoraFieldSpecified;
      }
      set
      {
        this.fechaHoraFieldSpecified = value;
        this.RaisePropertyChanged(nameof (fechaHoraSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
    public string servidor
    {
      get
      {
        return this.servidorField;
      }
      set
      {
        this.servidorField = value;
        this.RaisePropertyChanged(nameof (servidor));
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
