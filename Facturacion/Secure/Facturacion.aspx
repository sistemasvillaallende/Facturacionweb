<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MP.Master" AutoEventWireup="True"
    CodeBehind="Facturacion.aspx.cs" Inherits="Facturacion.Secure.Facturacion"
    ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Estilos modernos para la página de facturación */
        .page-header {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 2rem 0;
            margin: -15px -15px 30px -15px;
            border-radius: 0 0 10px 10px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }

        .page-title {
            font-size: 2.5rem;
            font-weight: 300;
            margin: 0;
            text-shadow: 0 2px 4px rgba(0,0,0,0.3);
        }

        .card {
            background: white;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
            padding: 20px;
            margin-bottom: 20px;
            border: none;
        }

        .btn-modern {
            border-radius: 50px;
            padding: 12px 24px;
            font-weight: 500;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            transition: all 0.3s ease;
            border: none;
        }

            .btn-modern:hover {
                transform: translateY(-2px);
                box-shadow: 0 6px 20px rgba(0, 0, 0, 0.15);
            }

        .btn-primary-modern {
            background: linear-gradient(45deg, #667eea, #764ba2);
            color: white;
        }

        .btn-success-modern {
            background: linear-gradient(45deg, #56ab2f, #a8e6cf);
            color: white;
        }

        .btn-warning-modern {
            background: linear-gradient(45deg, #f093fb, #f5576c);
            color: white;
        }

        .form-control-modern {
            border: 2px solid #e9ecef;
            border-radius: 10px;
            padding: 12px 15px;
            font-size: 14px;
        }

            .form-control-modern:focus {
                border-color: #667eea;
                box-shadow: 0 0 0 0.2rem rgba(102, 126, 234, 0.25);
            }

        .search-container {
            position: relative;
            margin-bottom: 30px;
        }

        .search-input {
            border-radius: 50px;
            padding-left: 50px;
            height: 50px;
            border: 2px solid #e9ecef;
            transition: all 0.3s ease;
        }

            .search-input:focus {
                border-color: #667eea;
                box-shadow: 0 0 20px rgba(102, 126, 234, 0.1);
            }

        .search-icon {
            position: absolute;
            left: 18px;
            top: 50%;
            transform: translateY(-50%);
            color: #6c757d;
            font-size: 18px;
        }

        .table-modern {
            border-radius: 10px;
            overflow: hidden;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
        }

            .table-modern thead {
                background: linear-gradient(45deg, #667eea, #764ba2);
                color: white;
            }


        .form-section {
            background: white;
            border-radius: 15px;
            padding: 30px;
            margin-bottom: 30px;
            box-shadow: 0 4px 12px rgb(0 0 0 / 47%) !important;
        }

        .section-title {
            color: #333;
            font-size: 24px;
            font-weight: 500;
            margin-bottom: 25px;
            padding-bottom: 10px;
            border-bottom: 2px solid #e9ecef;
        }

        .form-group {
            margin-bottom: 25px;
        }

            .form-group label {
                font-weight: 500;
                color: #555;
                margin-bottom: 8px;
                display: block;
            }

        .required-field {
            color: #e74c3c;
        }

        .input-group-modern {
            border-radius: 50px;
            overflow: hidden;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
        }

        .action-buttons {
            text-align: center;
            padding: 20px 0;
        }

        /*        .fade-in {
            animation: fadeIn 0.5s ease-in;
        }
s
        
        .container-modern {
            max-width: 1200px;
            margin: 0 auto;
            padding: 20px;
        }
        
        a{
            padding:5px;
        }
        
        td > span{
            background-color: #00BCD4;
            color: black;
            padding: 10px;
        }
    */
        header {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            background-color: white;
            text-align: center;
            z-index: 1000;
        }

        .select2-container--default .select2-selection--single {
        height: 34px !important;
        border: 1px solid #ccc !important;
        border-radius: 2px !important;
        background-color: #fff;
    }
    
    .select2-container--default .select2-selection--single .select2-selection__rendered {
        color: #555;
        line-height: 32px;
        padding-left: 12px;
        padding-right: 30px;
        font-size: 14px;
    }
    
    .select2-container--default .select2-selection--single .select2-selection__arrow {
        height: 32px;
        right: 1px;
        width: 30px;
    }
    
    .select2-container--default .select2-selection--single .select2-selection__arrow b {
        border-color: #888 transparent transparent transparent;
        border-width: 5px 4px 0 4px;
    }
    
    /* Focus igual que form-control de Bootstrap */
    .select2-container--default.select2-container--focus .select2-selection--single {
        border-color: #66afe9;
        outline: 0;
        box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102,175,233,.6);
    }
    
    /* Dropdown con altura estándar */
    .select2-container--default .select2-results > .select2-results__options {
        max-height: 200px;
    }
    
    /* Opciones del dropdown */
    .select2-container--default .select2-results__option {
        padding: 6px 12px;
        font-size: 14px;
    }
    
    /* Sin placeholder visible para mantener diseño original */
    .select2-container--default .select2-selection--single .select2-selection__placeholder {
        color: #999;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="padding-left: 4%; padding-right: 4%">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <va-header-general base-url="http://10.0.0.24/header_back"></va-header-general>
        <link href="http://10.0.0.24/header_back/header.css" rel="stylesheet" />
        <!-- Alertas de Error -->
        <asp:HiddenField ID="hCodOficina" runat="server" />
        <asp:HiddenField ID="hCodUsuario" runat="server" />
        <asp:HiddenField ID="hNombre" runat="server" />
        <div class="row" id="divError" runat="server" visible="false">
            <div class="col-md-12">
                <div class="alert alert-danger" style="border-radius: 10px; box-shadow: 0 4px 12px rgba(220, 53, 69, 0.15);">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                        ×
                    </button>
                    <h4>⚠️ Error!</h4>
                    <strong id="msjError" runat="server"></strong>
                </div>
            </div>
        </div>

        <div id="divBuscar" runat="server" style="margin-top: 120px;">
            <div class="row">
                <div class="col-md-9">
                    <div class="search-container">
                        <i class="glyphicon glyphicon-search search-icon"></i>
                        <asp:TextBox ID="TextBox1" CssClass="form-control search-input"
                            placeholder="Buscar facturas por contribuyente, concepto o monto..." runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3" style="text-align: right;">
                    <div class="action-buttons" style="text-align: right; padding: 10px 0; padding-top: 5px;">
                        <button type="button" id="btnAddFactu" class="btn btn-primary-modern btn-modern" runat="server"
                            onserverclick="btnAddFactu_ServerClick">
                            <i class="glyphicon glyphicon-plus-sign" aria-hidden="true"></i>&nbsp;Nueva Factura
                        </button>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="gvFacturas" CssClass="table table-striped table-modern"
                        EmptyDataText="📄 No se encontraron facturas asociadas a su oficina"
                        OnRowDataBound="gvFacturas_RowDataBound" OnRowCommand="gvFacturas_RowCommand"
                        AllowPaging="true"
                        OnPageIndexChanging="gvFacturas_PageIndexChanging"
                        runat="server" CellPadding="15" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
                        <AlternatingRowStyle BackColor="#f8f9fa" ForeColor="#333333"></AlternatingRowStyle>
                        <Columns>
                            <asp:BoundField DataField="fecha_transaccion" DataFormatString="{0:d}" HeaderText="Fecha"></asp:BoundField>
                            <asp:BoundField DataField="nombre" HeaderText="Contribuyente"></asp:BoundField>
                            <asp:BoundField DataField="des_categoria" HeaderText="Concepto"></asp:BoundField>
                            <asp:BoundField DataField="monto" DataFormatString="{0:c}" HeaderText="Monto"></asp:BoundField>
                            <asp:BoundField DataField="vencimiento" DataFormatString="{0:d}" HeaderText="Vencimiento"></asp:BoundField>
                            <asp:TemplateField HeaderText="Observaciones">
                                <ItemTemplate>
                                    <div id="divObs" runat="server"></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pagado" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Image ID="imgNoPagado" Visible="false"
                                        ImageUrl="~/App_Themes/dist/img/NoPagado.png"
                                        Height="23" runat="server" />
                                    <asp:Image ID="imgPagado" Visible="false"
                                        ImageUrl="~/App_Themes/dist/img/Pagado.png"
                                        Height="23" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Imprimir" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgImprimir" Height="23"
                                        CommandName="btnPrint"
                                        ToolTip="Imprimir"
                                        ImageUrl="~/App_Themes/dist/img/print.png"
                                        CommandArgument='<%# Eval("nro_transaccion") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDelete" Height="23"
                                        CommandName="btnDelete"
                                        ToolTip="Eliminar"
                                        OnClientClick="return confirm('¿Esta seguro de borrar la factura?');"
                                        ImageUrl="~/App_Themes/dist/img/trash.png"
                                        CommandArgument='<%# Eval("nro_transaccion") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle Width="100%" />
                    </asp:GridView>
                </div>
            </div>
        </div>

        <div id="divNuevo" runat="server" visible="false" class="form-section" style="margin-top: 120px;">
            <h2 class="section-title">📝 Nueva Factura</h2>

            <!-- Sección CUIT/CUIL -->
            <div class="form-group">
                <label class="form-label">Ingrese CUIT/CUIL</label>
                <div class="row">
                    <div class="col-md-8">
                        <div class="input-group">
                            <asp:TextBox ID="txtCUIT" TextMode="Number" CssClass="form-control"
                                Style="height: 44px;"
                                placeholder="Ingrese CUIT/CUIL (sin puntos ni guiones)" runat="server"></asp:TextBox>
                            <span class="input-group-btn">
                                <button class="btn btn-primary-modern btn-modern" type="button" id="btnAfip" runat="server"
                                    onserverclick="btnAfip_ServerClick">
                                    🔍 Buscar en AFIP</button>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Sección Datos Personales -->
            <div class="form-group">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Apellido <span class="required-field">*</span></label>
                            <input type="text" runat="server" id="txtApellido" class="form-control" placeholder="Ingrese apellido" />
                            <asp:RequiredFieldValidator ID="rv10" runat="server" ControlToValidate="txtApellido"
                                ErrorMessage="Ingrese Apellido" ForeColor="Red" ValidationGroup="factu"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Nombre <span class="required-field">*</span></label>
                            <input type="text" runat="server" id="txtNombre" class="form-control" placeholder="Ingrese nombre" />
                            <asp:RequiredFieldValidator ID="rv11" runat="server" ControlToValidate="txtNombre"
                                ErrorMessage="Ingrese Nombre" ForeColor="Red" ValidationGroup="factu"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Sección Documentación -->
            <div class="form-group">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Tipo Documento <span class="required-field">*</span></label>
                            <input type="text" runat="server" id="txtTipoDoc" class="form-control" placeholder="Ej: DNI, Pasaporte" />
                            <asp:RequiredFieldValidator ID="rv12" runat="server" ControlToValidate="txtTipoDoc"
                                ErrorMessage="Ingrese Tipo Documento" ForeColor="Red" ValidationGroup="factu"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Número de Documento <span class="required-field">*</span></label>
                            <input type="text" runat="server" id="txtDoc" class="form-control" placeholder="Ingrese número de documento" />
                            <asp:RequiredFieldValidator ID="rv13" runat="server" ControlToValidate="txtDoc"
                                ErrorMessage="Ingrese Nro. Documento" ForeColor="Red" ValidationGroup="factu"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Sección Datos Adicionales -->
            <div class="form-group">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Fecha de Nacimiento <span class="required-field">*</span></label>
                            <input type="date" runat="server" id="txtFecNac" class="form-control" />
                            <asp:RequiredFieldValidator ID="rv14" runat="server" ControlToValidate="txtFecNac"
                                ErrorMessage="Ingrese Fecha de Nacimiento" ForeColor="Red" ValidationGroup="factu"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Sexo <span class="required-field">*</span></label>
                            <select runat="server" id="txtSexo" class="form-control">
                                <option value="">Seleccione...</option>
                                <option value="M">Masculino</option>
                                <option value="F">Femenino</option>
                                <option value="X">No binario</option>
                            </select>
                            <asp:RequiredFieldValidator ID="rv15" runat="server" ControlToValidate="txtSexo"
                                ErrorMessage="Seleccione Sexo" ForeColor="Red" ValidationGroup="factu"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Sección Dirección -->
            <div class="form-group">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Dirección <span class="required-field">*</span></label>
                            <asp:TextBox ID="txtModalDir" CssClass="form-control"
                                placeholder="Calle, número, piso, depto" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rvm1" runat="server" ValidationGroup="factu"
                                ControlToValidate="txtModalDir"
                                ErrorMessage="Ingrese la dirección" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Localidad <span class="required-field">*</span></label>
                            <asp:TextBox ID="txtModalLocalidad" CssClass="form-control"
                                placeholder="Ingrese localidad" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rvm2" runat="server" ValidationGroup="factu"
                                ControlToValidate="txtModalLocalidad"
                                ErrorMessage="Ingrese la localidad" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Provincia</label>
                            <asp:DropDownList ID="DDLModalProvincia" CssClass="form-control" runat="server">
                                <asp:ListItem Text="Ciudad Autónoma de Buenos Aires" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Buenos Aires" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Catamara" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Córdoba" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Corrientes" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Entre Ríos" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Jujuy" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Mendoza" Value="7"></asp:ListItem>
                                <asp:ListItem Text="La Rioja" Value="8"></asp:ListItem>
                                <asp:ListItem Text="Salta" Value="9"></asp:ListItem>
                                <asp:ListItem Text="San Juan" Value="10"></asp:ListItem>
                                <asp:ListItem Text="San Luis" Value="11"></asp:ListItem>
                                <asp:ListItem Text="Santa Fe" Value="12"></asp:ListItem>
                                <asp:ListItem Text="Santiago del Estero" Value="13"></asp:ListItem>
                                <asp:ListItem Text="Tucumán" Value="14"></asp:ListItem>
                                <asp:ListItem Text="Chaco" Value="16"></asp:ListItem>
                                <asp:ListItem Text="Chubut" Value="17"></asp:ListItem>
                                <asp:ListItem Text="Formosa" Value="18"></asp:ListItem>
                                <asp:ListItem Text="Misiones" Value="19"></asp:ListItem>
                                <asp:ListItem Text="Neuquén" Value="20"></asp:ListItem>
                                <asp:ListItem Text="La Pampa" Value="21"></asp:ListItem>
                                <asp:ListItem Text="Río Negro" Value="22"></asp:ListItem>
                                <asp:ListItem Text="Santa Cruz" Value="23"></asp:ListItem>
                                <asp:ListItem Text="Tierra del Fuego" Value="24"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Código Postal <span class="required-field">*</span></label>
                            <asp:TextBox ID="txtMModalCP" CssClass="form-control"
                                placeholder="Ej: 1234" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rvm3" runat="server" ValidationGroup="factu"
                                ControlToValidate="txtMModalCP"
                                ErrorMessage="Ingrese el CP" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Sección Contacto -->
            <div class="form-group">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Teléfono <span class="required-field">*</span></label>
                            <asp:TextBox ID="txtCel" CssClass="form-control"
                                placeholder="Ej: +54 11 1234-5678" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rv1" runat="server" ControlToValidate="txtCel" ForeColor="Red"
                                ErrorMessage="Ingrese el Teléfono" ValidationGroup="factu"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Correo Electrónico <span class="required-field">*</span></label>
                            <asp:TextBox ID="txtMail" CssClass="form-control" TextMode="Email"
                                placeholder="ejemplo@correo.com" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rv2" runat="server" ControlToValidate="txtMail" ForeColor="Red"
                                ErrorMessage="Ingrese el e-mail" ValidationGroup="factu"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Sección Facturación -->
            <div class="form-group">
                <div class="row">
                    <div class="col-md-8">
                        <div class="form-group">
                            <label>Categoría de Deuda</label>
                            <asp:DropDownList CssClass="form-control" ID="DDLCatDeuda" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Monto <span class="required-field">*</span></label>
                            <div class="input-group">
                                <span class="input-group-addon">$</span>
                                <asp:TextBox ID="txtMonto" CssClass="form-control"
                                    TextMode="Number" step="0.01" placeholder="0.00" runat="server"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator ID="rv3" runat="server" ControlToValidate="txtMonto" ForeColor="Red"
                                ErrorMessage="Ingrese el Monto" ValidationGroup="factu"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Sección Observaciones -->
            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label>Observaciones</label>
                            <asp:TextBox ID="txtObs" TextMode="MultiLine" CssClass="form-control"
                                Rows="4" placeholder="Ingrese observaciones adicionales..." runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Botones de Acción -->
            <div class="action-buttons" style="text-align: center; padding: 30px 0;">
                <asp:Button ID="btnCancelar" runat="server" Text="❌ Cancelar"
                    CssClass="btn btn-warning-modern btn-modern" Style="margin-right: 15px;"
                    OnClick="btnCancelar_Click" />
                <asp:Button ID="btnAceptar" runat="server" Text="✓ Crear Factura"
                    CssClass="btn btn-success-modern btn-modern"
                    ValidationGroup="factu" OnClick="btnAceptar_Click" />
            </div>
        </div>

        <asp:Button ID="Button1" Visible="false" runat="server" OnClick="Button1_Click" Text="Button" />
    </div>



    <!-- jQuery 2.1.4 -->
    <script src="../App_Themes/plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <script src="../App_Themes/bootstrap/js/quicksearch.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

    <a href="http://10.0.0.24/siimva/" id="enlaceHeader">

        <script>


            $(document).ready(function () {
                var txtFiltro = '#' + '<%=TextBox1.ClientID %>';
                var grillaJedis = '#' + '<%=gvFacturas.ClientID %>';
                $(txtFiltro).quicksearch(grillaJedis + ' tbody tr');
            });
        </script>

        <!-- Bootstrap 3.3.2 JS -->
        <script src="../App_Themes/bootstrap/js/bootstrap.min.js"></script>
        <!-- CK Editor -->
        <script src="https://cdn.ckeditor.com/4.4.3/standard/ckeditor.js"></script>
        <script type="text/javascript">
            $(function () {
                // Replace the <textarea id="editor1"> with a CKEditor
                // instance, using default configuration.
                CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtObs');
            });

            $(document).ready(function () {
                // Inicializar Select2
                $('#<%= DDLCatDeuda.ClientID %>').select2({
        placeholder: "Seleccionar o buscar categoría...",
        allowClear: true,
        width: '100%',
        dropdownAutoWidth: true,
        language: {
            noResults: function () {
                return "No se encontraron resultados";
            },
            searching: function () {
                return "Buscando...";
            },
            inputTooShort: function () {
                return "Escriba para buscar";
            }
        }
    });
});
        </script>
</asp:Content>
