// Decompiled with JetBrains decompiler
// Type: Facturacion.WSAfip.domicilio
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
  public class domicilio : INotifyPropertyChanged
  {
    private string codPostalField;
    private string datoAdicionalField;
    private string descripcionProvinciaField;
    private string direccionField;
    private int idProvinciaField;
    private bool idProvinciaFieldSpecified;
    private string localidadField;
    private string tipoDatoAdicionalField;
    private string tipoDomicilioField;

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
    public string codPostal
    {
      get
      {
        return this.codPostalField;
      }
      set
      {
        this.codPostalField = value;
        this.RaisePropertyChanged(nameof (codPostal));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
    public string datoAdicional
    {
      get
      {
        return this.datoAdicionalField;
      }
      set
      {
        this.datoAdicionalField = value;
        this.RaisePropertyChanged(nameof (datoAdicional));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
    public string descripcionProvincia
    {
      get
      {
        return this.descripcionProvinciaField;
      }
      set
      {
        this.descripcionProvinciaField = value;
        this.RaisePropertyChanged(nameof (descripcionProvincia));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 3)]
    public string direccion
    {
      get
      {
        return this.direccionField;
      }
      set
      {
        this.direccionField = value;
        this.RaisePropertyChanged(nameof (direccion));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 4)]
    public int idProvincia
    {
      get
      {
        return this.idProvinciaField;
      }
      set
      {
        this.idProvinciaField = value;
        this.RaisePropertyChanged(nameof (idProvincia));
      }
    }

    [XmlIgnore]
    public bool idProvinciaSpecified
    {
      get
      {
        return this.idProvinciaFieldSpecified;
      }
      set
      {
        this.idProvinciaFieldSpecified = value;
        this.RaisePropertyChanged(nameof (idProvinciaSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
    public string localidad
    {
      get
      {
        return this.localidadField;
      }
      set
      {
        this.localidadField = value;
        this.RaisePropertyChanged(nameof (localidad));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 6)]
    public string tipoDatoAdicional
    {
      get
      {
        return this.tipoDatoAdicionalField;
      }
      set
      {
        this.tipoDatoAdicionalField = value;
        this.RaisePropertyChanged(nameof (tipoDatoAdicional));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 7)]
    public string tipoDomicilio
    {
      get
      {
        return this.tipoDomicilioField;
      }
      set
      {
        this.tipoDomicilioField = value;
        this.RaisePropertyChanged(nameof (tipoDomicilio));
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
