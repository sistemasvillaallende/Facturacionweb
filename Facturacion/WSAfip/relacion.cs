// Decompiled with JetBrains decompiler
// Type: Facturacion.WSAfip.relacion
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
  public class relacion : INotifyPropertyChanged
  {
    private DateTime ffRelacionField;
    private bool ffRelacionFieldSpecified;
    private DateTime ffVencimientoField;
    private bool ffVencimientoFieldSpecified;
    private long idPersonaField;
    private bool idPersonaFieldSpecified;
    private long idPersonaAsociadaField;
    private bool idPersonaAsociadaFieldSpecified;
    private string subtipoRelacionField;
    private string tipoRelacionField;

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
    public DateTime ffRelacion
    {
      get
      {
        return this.ffRelacionField;
      }
      set
      {
        this.ffRelacionField = value;
        this.RaisePropertyChanged(nameof (ffRelacion));
      }
    }

    [XmlIgnore]
    public bool ffRelacionSpecified
    {
      get
      {
        return this.ffRelacionFieldSpecified;
      }
      set
      {
        this.ffRelacionFieldSpecified = value;
        this.RaisePropertyChanged(nameof (ffRelacionSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
    public DateTime ffVencimiento
    {
      get
      {
        return this.ffVencimientoField;
      }
      set
      {
        this.ffVencimientoField = value;
        this.RaisePropertyChanged(nameof (ffVencimiento));
      }
    }

    [XmlIgnore]
    public bool ffVencimientoSpecified
    {
      get
      {
        return this.ffVencimientoFieldSpecified;
      }
      set
      {
        this.ffVencimientoFieldSpecified = value;
        this.RaisePropertyChanged(nameof (ffVencimientoSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
    public long idPersona
    {
      get
      {
        return this.idPersonaField;
      }
      set
      {
        this.idPersonaField = value;
        this.RaisePropertyChanged(nameof (idPersona));
      }
    }

    [XmlIgnore]
    public bool idPersonaSpecified
    {
      get
      {
        return this.idPersonaFieldSpecified;
      }
      set
      {
        this.idPersonaFieldSpecified = value;
        this.RaisePropertyChanged(nameof (idPersonaSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
    public long idPersonaAsociada
    {
      get
      {
        return this.idPersonaAsociadaField;
      }
      set
      {
        this.idPersonaAsociadaField = value;
        this.RaisePropertyChanged(nameof (idPersonaAsociada));
      }
    }

    [XmlIgnore]
    public bool idPersonaAsociadaSpecified
    {
      get
      {
        return this.idPersonaAsociadaFieldSpecified;
      }
      set
      {
        this.idPersonaAsociadaFieldSpecified = value;
        this.RaisePropertyChanged(nameof (idPersonaAsociadaSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
    public string subtipoRelacion
    {
      get
      {
        return this.subtipoRelacionField;
      }
      set
      {
        this.subtipoRelacionField = value;
        this.RaisePropertyChanged(nameof (subtipoRelacion));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
    public string tipoRelacion
    {
      get
      {
        return this.tipoRelacionField;
      }
      set
      {
        this.tipoRelacionField = value;
        this.RaisePropertyChanged(nameof (tipoRelacion));
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
