// Decompiled with JetBrains decompiler
// Type: Facturacion.WSAfip.impuesto
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
  public class impuesto : INotifyPropertyChanged
  {
    private string descripcionImpuestoField;
    private int diaPeriodoField;
    private bool diaPeriodoFieldSpecified;
    private string estadoField;
    private DateTime ffInscripcionField;
    private bool ffInscripcionFieldSpecified;
    private int idImpuestoField;
    private bool idImpuestoFieldSpecified;
    private int periodoField;
    private bool periodoFieldSpecified;

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
    public string descripcionImpuesto
    {
      get
      {
        return this.descripcionImpuestoField;
      }
      set
      {
        this.descripcionImpuestoField = value;
        this.RaisePropertyChanged(nameof (descripcionImpuesto));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
    public int diaPeriodo
    {
      get
      {
        return this.diaPeriodoField;
      }
      set
      {
        this.diaPeriodoField = value;
        this.RaisePropertyChanged(nameof (diaPeriodo));
      }
    }

    [XmlIgnore]
    public bool diaPeriodoSpecified
    {
      get
      {
        return this.diaPeriodoFieldSpecified;
      }
      set
      {
        this.diaPeriodoFieldSpecified = value;
        this.RaisePropertyChanged(nameof (diaPeriodoSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
    public string estado
    {
      get
      {
        return this.estadoField;
      }
      set
      {
        this.estadoField = value;
        this.RaisePropertyChanged(nameof (estado));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
    public DateTime ffInscripcion
    {
      get
      {
        return this.ffInscripcionField;
      }
      set
      {
        this.ffInscripcionField = value;
        this.RaisePropertyChanged(nameof (ffInscripcion));
      }
    }

    [XmlIgnore]
    public bool ffInscripcionSpecified
    {
      get
      {
        return this.ffInscripcionFieldSpecified;
      }
      set
      {
        this.ffInscripcionFieldSpecified = value;
        this.RaisePropertyChanged(nameof (ffInscripcionSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
    public int idImpuesto
    {
      get
      {
        return this.idImpuestoField;
      }
      set
      {
        this.idImpuestoField = value;
        this.RaisePropertyChanged(nameof (idImpuesto));
      }
    }

    [XmlIgnore]
    public bool idImpuestoSpecified
    {
      get
      {
        return this.idImpuestoFieldSpecified;
      }
      set
      {
        this.idImpuestoFieldSpecified = value;
        this.RaisePropertyChanged(nameof (idImpuestoSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
    public int periodo
    {
      get
      {
        return this.periodoField;
      }
      set
      {
        this.periodoField = value;
        this.RaisePropertyChanged(nameof (periodo));
      }
    }

    [XmlIgnore]
    public bool periodoSpecified
    {
      get
      {
        return this.periodoFieldSpecified;
      }
      set
      {
        this.periodoFieldSpecified = value;
        this.RaisePropertyChanged(nameof (periodoSpecified));
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
