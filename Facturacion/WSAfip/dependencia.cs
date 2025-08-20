// Decompiled with JetBrains decompiler
// Type: Facturacion.WSAfip.dependencia
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
  public class dependencia : INotifyPropertyChanged
  {
    private string descripcionDependenciaField;
    private int idDependenciaField;
    private bool idDependenciaFieldSpecified;

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 0)]
    public string descripcionDependencia
    {
      get
      {
        return this.descripcionDependenciaField;
      }
      set
      {
        this.descripcionDependenciaField = value;
        this.RaisePropertyChanged(nameof (descripcionDependencia));
      }
    }

    [XmlElement(Form = XmlSchemaForm.Unqualified, Order = 1)]
    public int idDependencia
    {
      get
      {
        return this.idDependenciaField;
      }
      set
      {
        this.idDependenciaField = value;
        this.RaisePropertyChanged(nameof (idDependencia));
      }
    }

    [XmlIgnore]
    public bool idDependenciaSpecified
    {
      get
      {
        return this.idDependenciaFieldSpecified;
      }
      set
      {
        this.idDependenciaFieldSpecified = value;
        this.RaisePropertyChanged(nameof (idDependenciaSpecified));
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
