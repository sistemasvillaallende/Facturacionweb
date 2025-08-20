// Decompiled with JetBrains decompiler
// Type: Facturacion.WSAfip.actividad
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
  public class actividad : INotifyPropertyChanged
  {
    private string descripcionActividadField;
    private long idActividadField;
    private bool idActividadFieldSpecified;
    private int nomencladorField;
    private bool nomencladorFieldSpecified;
    private int ordenField;
    private bool ordenFieldSpecified;
    private int periodoField;
    private bool periodoFieldSpecified;

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
    public string descripcionActividad
    {
      get
      {
        return this.descripcionActividadField;
      }
      set
      {
        this.descripcionActividadField = value;
        this.RaisePropertyChanged(nameof (descripcionActividad));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
    public long idActividad
    {
      get
      {
        return this.idActividadField;
      }
      set
      {
        this.idActividadField = value;
        this.RaisePropertyChanged(nameof (idActividad));
      }
    }

    [XmlIgnore]
    public bool idActividadSpecified
    {
      get
      {
        return this.idActividadFieldSpecified;
      }
      set
      {
        this.idActividadFieldSpecified = value;
        this.RaisePropertyChanged(nameof (idActividadSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
    public int nomenclador
    {
      get
      {
        return this.nomencladorField;
      }
      set
      {
        this.nomencladorField = value;
        this.RaisePropertyChanged(nameof (nomenclador));
      }
    }

    [XmlIgnore]
    public bool nomencladorSpecified
    {
      get
      {
        return this.nomencladorFieldSpecified;
      }
      set
      {
        this.nomencladorFieldSpecified = value;
        this.RaisePropertyChanged(nameof (nomencladorSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
    public int orden
    {
      get
      {
        return this.ordenField;
      }
      set
      {
        this.ordenField = value;
        this.RaisePropertyChanged(nameof (orden));
      }
    }

    [XmlIgnore]
    public bool ordenSpecified
    {
      get
      {
        return this.ordenFieldSpecified;
      }
      set
      {
        this.ordenFieldSpecified = value;
        this.RaisePropertyChanged(nameof (ordenSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
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
