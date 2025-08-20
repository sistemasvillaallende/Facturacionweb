// Decompiled with JetBrains decompiler
// Type: Facturacion.WSAfip.persona
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
  public class persona : INotifyPropertyChanged
  {
    private Facturacion.WSAfip.actividad[] actividadField;
    private string apellidoField;
    private int cantidadSociosEmpresaMonoField;
    private bool cantidadSociosEmpresaMonoFieldSpecified;
    private Facturacion.WSAfip.categoria[] categoriaField;
    private long?[] claveInactivaAsociadaField;
    private dependencia dependenciaField;
    private Facturacion.WSAfip.domicilio[] domicilioField;
    private Facturacion.WSAfip.email[] emailField;
    private string estadoClaveField;
    private DateTime fechaContratoSocialField;
    private bool fechaContratoSocialFieldSpecified;
    private DateTime fechaFallecimientoField;
    private bool fechaFallecimientoFieldSpecified;
    private DateTime fechaInscripcionField;
    private bool fechaInscripcionFieldSpecified;
    private DateTime fechaJubiladoField;
    private bool fechaJubiladoFieldSpecified;
    private DateTime fechaNacimientoField;
    private bool fechaNacimientoFieldSpecified;
    private DateTime fechaVencimientoMigracionField;
    private bool fechaVencimientoMigracionFieldSpecified;
    private string formaJuridicaField;
    private long idPersonaField;
    private bool idPersonaFieldSpecified;
    private Facturacion.WSAfip.impuesto[] impuestoField;
    private int leyJubilacionField;
    private bool leyJubilacionFieldSpecified;
    private string localidadInscripcionField;
    private int mesCierreField;
    private bool mesCierreFieldSpecified;
    private string nombreField;
    private string numeroDocumentoField;
    private long numeroInscripcionField;
    private bool numeroInscripcionFieldSpecified;
    private string organismoInscripcionField;
    private string organismoOriginanteField;
    private double porcentajeCapitalNacionalField;
    private bool porcentajeCapitalNacionalFieldSpecified;
    private string provinciaInscripcionField;
    private string razonSocialField;
    private Facturacion.WSAfip.regimen[] regimenField;
    private Facturacion.WSAfip.relacion[] relacionField;
    private string sexoField;
    private Facturacion.WSAfip.telefono[] telefonoField;
    private string tipoClaveField;
    private string tipoDocumentoField;
    private string tipoOrganismoOriginanteField;
    private string tipoPersonaField;
    private string tipoResidenciaField;

    [XmlElement("actividad", Form = XmlSchemaForm.Unqualified, IsNullable = true, Order = 0)]
    public Facturacion.WSAfip.actividad[] actividad
    {
      get
      {
        return this.actividadField;
      }
      set
      {
        this.actividadField = value;
        this.RaisePropertyChanged(nameof (actividad));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
    public string apellido
    {
      get
      {
        return this.apellidoField;
      }
      set
      {
        this.apellidoField = value;
        this.RaisePropertyChanged(nameof (apellido));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 2)]
    public int cantidadSociosEmpresaMono
    {
      get
      {
        return this.cantidadSociosEmpresaMonoField;
      }
      set
      {
        this.cantidadSociosEmpresaMonoField = value;
        this.RaisePropertyChanged(nameof (cantidadSociosEmpresaMono));
      }
    }

    [XmlIgnore]
    public bool cantidadSociosEmpresaMonoSpecified
    {
      get
      {
        return this.cantidadSociosEmpresaMonoFieldSpecified;
      }
      set
      {
        this.cantidadSociosEmpresaMonoFieldSpecified = value;
        this.RaisePropertyChanged(nameof (cantidadSociosEmpresaMonoSpecified));
      }
    }

    [XmlElement("categoria", Form = XmlSchemaForm.Unqualified, IsNullable = true, Order = 3)]
    public Facturacion.WSAfip.categoria[] categoria
    {
      get
      {
        return this.categoriaField;
      }
      set
      {
        this.categoriaField = value;
        this.RaisePropertyChanged(nameof (categoria));
      }
    }

    [XmlElement("claveInactivaAsociada", Form = XmlSchemaForm.Unqualified, IsNullable = true, Order = 4)]
    public long?[] claveInactivaAsociada
    {
      get
      {
        return this.claveInactivaAsociadaField;
      }
      set
      {
        this.claveInactivaAsociadaField = value;
        this.RaisePropertyChanged(nameof (claveInactivaAsociada));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 5)]
    public dependencia dependencia
    {
      get
      {
        return this.dependenciaField;
      }
      set
      {
        this.dependenciaField = value;
        this.RaisePropertyChanged(nameof (dependencia));
      }
    }

    [XmlElement("domicilio", Form = XmlSchemaForm.Unqualified, IsNullable = true, Order = 6)]
    public Facturacion.WSAfip.domicilio[] domicilio
    {
      get
      {
        return this.domicilioField;
      }
      set
      {
        this.domicilioField = value;
        this.RaisePropertyChanged(nameof (domicilio));
      }
    }

    [XmlElement("email", Form = XmlSchemaForm.Unqualified, IsNullable = true, Order = 7)]
    public Facturacion.WSAfip.email[] email
    {
      get
      {
        return this.emailField;
      }
      set
      {
        this.emailField = value;
        this.RaisePropertyChanged(nameof (email));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 8)]
    public string estadoClave
    {
      get
      {
        return this.estadoClaveField;
      }
      set
      {
        this.estadoClaveField = value;
        this.RaisePropertyChanged(nameof (estadoClave));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 9)]
    public DateTime fechaContratoSocial
    {
      get
      {
        return this.fechaContratoSocialField;
      }
      set
      {
        this.fechaContratoSocialField = value;
        this.RaisePropertyChanged(nameof (fechaContratoSocial));
      }
    }

    [XmlIgnore]
    public bool fechaContratoSocialSpecified
    {
      get
      {
        return this.fechaContratoSocialFieldSpecified;
      }
      set
      {
        this.fechaContratoSocialFieldSpecified = value;
        this.RaisePropertyChanged(nameof (fechaContratoSocialSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 10)]
    public DateTime fechaFallecimiento
    {
      get
      {
        return this.fechaFallecimientoField;
      }
      set
      {
        this.fechaFallecimientoField = value;
        this.RaisePropertyChanged(nameof (fechaFallecimiento));
      }
    }

    [XmlIgnore]
    public bool fechaFallecimientoSpecified
    {
      get
      {
        return this.fechaFallecimientoFieldSpecified;
      }
      set
      {
        this.fechaFallecimientoFieldSpecified = value;
        this.RaisePropertyChanged(nameof (fechaFallecimientoSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 11)]
    public DateTime fechaInscripcion
    {
      get
      {
        return this.fechaInscripcionField;
      }
      set
      {
        this.fechaInscripcionField = value;
        this.RaisePropertyChanged(nameof (fechaInscripcion));
      }
    }

    [XmlIgnore]
    public bool fechaInscripcionSpecified
    {
      get
      {
        return this.fechaInscripcionFieldSpecified;
      }
      set
      {
        this.fechaInscripcionFieldSpecified = value;
        this.RaisePropertyChanged(nameof (fechaInscripcionSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 12)]
    public DateTime fechaJubilado
    {
      get
      {
        return this.fechaJubiladoField;
      }
      set
      {
        this.fechaJubiladoField = value;
        this.RaisePropertyChanged(nameof (fechaJubilado));
      }
    }

    [XmlIgnore]
    public bool fechaJubiladoSpecified
    {
      get
      {
        return this.fechaJubiladoFieldSpecified;
      }
      set
      {
        this.fechaJubiladoFieldSpecified = value;
        this.RaisePropertyChanged(nameof (fechaJubiladoSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 13)]
    public DateTime fechaNacimiento
    {
      get
      {
        return this.fechaNacimientoField;
      }
      set
      {
        this.fechaNacimientoField = value;
        this.RaisePropertyChanged(nameof (fechaNacimiento));
      }
    }

    [XmlIgnore]
    public bool fechaNacimientoSpecified
    {
      get
      {
        return this.fechaNacimientoFieldSpecified;
      }
      set
      {
        this.fechaNacimientoFieldSpecified = value;
        this.RaisePropertyChanged(nameof (fechaNacimientoSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 14)]
    public DateTime fechaVencimientoMigracion
    {
      get
      {
        return this.fechaVencimientoMigracionField;
      }
      set
      {
        this.fechaVencimientoMigracionField = value;
        this.RaisePropertyChanged(nameof (fechaVencimientoMigracion));
      }
    }

    [XmlIgnore]
    public bool fechaVencimientoMigracionSpecified
    {
      get
      {
        return this.fechaVencimientoMigracionFieldSpecified;
      }
      set
      {
        this.fechaVencimientoMigracionFieldSpecified = value;
        this.RaisePropertyChanged(nameof (fechaVencimientoMigracionSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 15)]
    public string formaJuridica
    {
      get
      {
        return this.formaJuridicaField;
      }
      set
      {
        this.formaJuridicaField = value;
        this.RaisePropertyChanged(nameof (formaJuridica));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 16)]
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

    [XmlElement("impuesto", Form = XmlSchemaForm.Unqualified, IsNullable = true, Order = 17)]
    public Facturacion.WSAfip.impuesto[] impuesto
    {
      get
      {
        return this.impuestoField;
      }
      set
      {
        this.impuestoField = value;
        this.RaisePropertyChanged(nameof (impuesto));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 18)]
    public int leyJubilacion
    {
      get
      {
        return this.leyJubilacionField;
      }
      set
      {
        this.leyJubilacionField = value;
        this.RaisePropertyChanged(nameof (leyJubilacion));
      }
    }

    [XmlIgnore]
    public bool leyJubilacionSpecified
    {
      get
      {
        return this.leyJubilacionFieldSpecified;
      }
      set
      {
        this.leyJubilacionFieldSpecified = value;
        this.RaisePropertyChanged(nameof (leyJubilacionSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 19)]
    public string localidadInscripcion
    {
      get
      {
        return this.localidadInscripcionField;
      }
      set
      {
        this.localidadInscripcionField = value;
        this.RaisePropertyChanged(nameof (localidadInscripcion));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 20)]
    public int mesCierre
    {
      get
      {
        return this.mesCierreField;
      }
      set
      {
        this.mesCierreField = value;
        this.RaisePropertyChanged(nameof (mesCierre));
      }
    }

    [XmlIgnore]
    public bool mesCierreSpecified
    {
      get
      {
        return this.mesCierreFieldSpecified;
      }
      set
      {
        this.mesCierreFieldSpecified = value;
        this.RaisePropertyChanged(nameof (mesCierreSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 21)]
    public string nombre
    {
      get
      {
        return this.nombreField;
      }
      set
      {
        this.nombreField = value;
        this.RaisePropertyChanged(nameof (nombre));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 22)]
    public string numeroDocumento
    {
      get
      {
        return this.numeroDocumentoField;
      }
      set
      {
        this.numeroDocumentoField = value;
        this.RaisePropertyChanged(nameof (numeroDocumento));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 23)]
    public long numeroInscripcion
    {
      get
      {
        return this.numeroInscripcionField;
      }
      set
      {
        this.numeroInscripcionField = value;
        this.RaisePropertyChanged(nameof (numeroInscripcion));
      }
    }

    [XmlIgnore]
    public bool numeroInscripcionSpecified
    {
      get
      {
        return this.numeroInscripcionFieldSpecified;
      }
      set
      {
        this.numeroInscripcionFieldSpecified = value;
        this.RaisePropertyChanged(nameof (numeroInscripcionSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 24)]
    public string organismoInscripcion
    {
      get
      {
        return this.organismoInscripcionField;
      }
      set
      {
        this.organismoInscripcionField = value;
        this.RaisePropertyChanged(nameof (organismoInscripcion));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 25)]
    public string organismoOriginante
    {
      get
      {
        return this.organismoOriginanteField;
      }
      set
      {
        this.organismoOriginanteField = value;
        this.RaisePropertyChanged(nameof (organismoOriginante));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 26)]
    public double porcentajeCapitalNacional
    {
      get
      {
        return this.porcentajeCapitalNacionalField;
      }
      set
      {
        this.porcentajeCapitalNacionalField = value;
        this.RaisePropertyChanged(nameof (porcentajeCapitalNacional));
      }
    }

    [XmlIgnore]
    public bool porcentajeCapitalNacionalSpecified
    {
      get
      {
        return this.porcentajeCapitalNacionalFieldSpecified;
      }
      set
      {
        this.porcentajeCapitalNacionalFieldSpecified = value;
        this.RaisePropertyChanged(nameof (porcentajeCapitalNacionalSpecified));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 27)]
    public string provinciaInscripcion
    {
      get
      {
        return this.provinciaInscripcionField;
      }
      set
      {
        this.provinciaInscripcionField = value;
        this.RaisePropertyChanged(nameof (provinciaInscripcion));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 28)]
    public string razonSocial
    {
      get
      {
        return this.razonSocialField;
      }
      set
      {
        this.razonSocialField = value;
        this.RaisePropertyChanged(nameof (razonSocial));
      }
    }

    [XmlElement("regimen", Form = XmlSchemaForm.Unqualified, IsNullable = true, Order = 29)]
    public Facturacion.WSAfip.regimen[] regimen
    {
      get
      {
        return this.regimenField;
      }
      set
      {
        this.regimenField = value;
        this.RaisePropertyChanged(nameof (regimen));
      }
    }

    [XmlElement("relacion", Form = XmlSchemaForm.Unqualified, IsNullable = true, Order = 30)]
    public Facturacion.WSAfip.relacion[] relacion
    {
      get
      {
        return this.relacionField;
      }
      set
      {
        this.relacionField = value;
        this.RaisePropertyChanged(nameof (relacion));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 31)]
    public string sexo
    {
      get
      {
        return this.sexoField;
      }
      set
      {
        this.sexoField = value;
        this.RaisePropertyChanged(nameof (sexo));
      }
    }

    [XmlElement("telefono", Form = XmlSchemaForm.Unqualified, IsNullable = true, Order = 32)]
    public Facturacion.WSAfip.telefono[] telefono
    {
      get
      {
        return this.telefonoField;
      }
      set
      {
        this.telefonoField = value;
        this.RaisePropertyChanged(nameof (telefono));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 33)]
    public string tipoClave
    {
      get
      {
        return this.tipoClaveField;
      }
      set
      {
        this.tipoClaveField = value;
        this.RaisePropertyChanged(nameof (tipoClave));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 34)]
    public string tipoDocumento
    {
      get
      {
        return this.tipoDocumentoField;
      }
      set
      {
        this.tipoDocumentoField = value;
        this.RaisePropertyChanged(nameof (tipoDocumento));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 35)]
    public string tipoOrganismoOriginante
    {
      get
      {
        return this.tipoOrganismoOriginanteField;
      }
      set
      {
        this.tipoOrganismoOriginanteField = value;
        this.RaisePropertyChanged(nameof (tipoOrganismoOriginante));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 36)]
    public string tipoPersona
    {
      get
      {
        return this.tipoPersonaField;
      }
      set
      {
        this.tipoPersonaField = value;
        this.RaisePropertyChanged(nameof (tipoPersona));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 37)]
    public string tipoResidencia
    {
      get
      {
        return this.tipoResidenciaField;
      }
      set
      {
        this.tipoResidenciaField = value;
        this.RaisePropertyChanged(nameof (tipoResidencia));
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
