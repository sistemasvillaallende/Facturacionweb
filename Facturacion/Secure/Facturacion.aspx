<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MP.Master" AutoEventWireup="True"
    CodeBehind="Facturacion.aspx.cs" Inherits="Facturacion.Secure.Facturacion"
    ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        a{
            padding:5px;
        }
       td > span{
                background-color: #00BCD4;
    color: black;
    padding: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="margin-top: 60px;">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-xs-12" style="text-align: center;">
                <h3>FACTURACION</h3>
            </div>
        </div>
        <div class="row" id="divError" runat="server" visible="false">
            <div class="col-md-8 col-md-offset-2">
                <div class="alert alert-dismissable alert-warning">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                        ×
                    </button>
                    <h4>Error!
                    </h4>
                    <strong id="msjError" runat="server"></strong>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12" style="text-align: center;">
                <hr />
            </div>
        </div>

        <div id="divBuscar" runat="server">
            <div class="row">
                <div class="col-md-10 col-md-offset-1">
                    <button type="button" id="btnAddFactu" class="btn btn-primary" runat="server"
                        onserverclick="btnAddFactu_ServerClick">
                        <span class="glyphicon glyphicon-plus-sign" aria-hidden="true">&nbsp;Nueva Factura</span>
                    </button>
                </div>
            </div>
            <div class="row" style="margin-top: 20px;">
                <div class="col-md-10 col-md-offset-1">
                    <div class="input-group">
                        <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
                        <span class="input-group-btn">
                            <label class="btn btn-default">Buscar</label>
                        </span>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 25px;">
                <div class="col-md-10 col-md-offset-1">
                    <asp:GridView ID="gvFacturas" CssClass="table"
                        EmptyDataText="No se encontraron facturas asociadas a su oficina"
                        OnRowDataBound="gvFacturas_RowDataBound" OnRowCommand="gvFacturas_RowCommand"
                        AllowPaging="true"
                        OnPageIndexChanging="gvFacturas_PageIndexChanging"
                        runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
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
                                    <asp:Image ID="imgPagado" Height="23" runat="server" />
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
                                        ImageUrl="~/App_Themes/dist/img/Delete.png"
                                        CommandArgument='<%# Eval("nro_transaccion") %>'
                                        runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <EditRowStyle BackColor="#999999"></EditRowStyle>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                        <PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                        <PagerStyle Width="100%" />
                    </asp:GridView>
                </div>
            </div>
        </div>

        <div id="divNuevo" runat="server" visible="false">
            <div class="row" style="margin-top: 20px;">
                <div class="col-md-10 col-md-offset-2">
                    <label>Ingrese CUIT/CUIL</label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 col-md-offset-2">
                    <div class="input-group">
                        <asp:TextBox ID="txtCUIT" TextMode="Number" CssClass="form-control"
                            placeholder="Ingrese CUIT/CUIL (sin puntos ni guiones)" runat="server"></asp:TextBox>
                        <span class="input-group-btn">
                            <button class="btn btn-primary" type="button" id="btnAfip" runat="server"
                                onserverclick="btnAfip_ServerClick">
                                Buscar</button>
                        </span>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 col-md-offset-2">
                    <div class="form-group has-feedback " style="margin-top: 20px;">
                        <label>Apellido<span style="color: red;">*</span></label>
                        <input type="text" runat="server" id="txtApellido" class="form-control" placeholder="Nombre" />
                        <span class="glyphicon glyphicon-user form-control-feedback"></span>
                    </div>
                    <asp:RequiredFieldValidator ID="rv10" runat="server" ControlToValidate="txtApellido"
                        ErrorMessage="Ingrese Apellido" ForeColor="Red" ValidationGroup="factu"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <div class="form-group has-feedback " style="margin-top: 20px;">
                        <label>Nombre<span style="color: red;">*</span></label>
                        <input type="text" runat="server" id="txtNombre" class="form-control" placeholder="Nombre" />
                        <span class="glyphicon glyphicon-user form-control-feedback"></span>
                    </div>
                    <asp:RequiredFieldValidator ID="rv11" runat="server" ControlToValidate="txtNombre"
                        ErrorMessage="Ingrese Nombre" ForeColor="Red" ValidationGroup="factu"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 col-md-offset-2">
                    <div class="form-group has-feedback " style="margin-top: 20px;">
                        <label>Tipo Documento<span style="color: red;">*</span></label>
                        <input type="text" runat="server" id="txtTipoDoc" class="form-control" placeholder="Nombre" />
                        <span class="glyphicon glyphicon-hdd form-control-feedback"></span>
                    </div>
                    <asp:RequiredFieldValidator ID="rv12" runat="server" ControlToValidate="txtTipoDoc"
                        ErrorMessage="Ingrese Tipo Documento" ForeColor="Red" ValidationGroup="factu"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <div class="form-group has-feedback " style="margin-top: 20px;">
                        <label>Documento<span style="color: red;">*</span></label>
                        <input type="text" runat="server" id="txtDoc" class="form-control" placeholder="Nombre" />
                        <span class="glyphicon glyphicon-hdd form-control-feedback"></span>
                    </div>
                    <asp:RequiredFieldValidator ID="rv13" runat="server" ControlToValidate="txtDoc"
                        ErrorMessage="Ingrese Nro. Documento" ForeColor="Red" ValidationGroup="factu"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4 col-md-offset-2">
                    <div class="form-group has-feedback " style="margin-top: 20px;">
                        <label>Fecha de Naciemiento<span style="color: red;">*</span></label>
                        <input type="text" runat="server" id="txtFecNac" class="form-control" placeholder="Nombre" />
                        <span class="glyphicon glyphicon-calendar form-control-feedback"></span>
                    </div>
                    <asp:RequiredFieldValidator ID="rv14" runat="server" ControlToValidate="txtFecNac"
                        ErrorMessage="Ingrese Fecha de Nacimiento" ForeColor="Red" ValidationGroup="factu"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <div class="form-group has-feedback " style="margin-top: 20px;">
                        <label>Sexo<span style="color: red;">*</span></label>
                        <input type="text" runat="server" id="txtSexo" class="form-control" placeholder="Nombre" />
                        <span class="glyphicon glyphicon-user form-control-feedback"></span>
                    </div>
                    <asp:RequiredFieldValidator ID="rv15" runat="server" ControlToValidate="txtSexo"
                        ErrorMessage="Ingrese Sexo" ForeColor="Red" ValidationGroup="factu"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 col-md-offset-2">
                    <div class="form-group has-feedback " style="margin-top: 20px;">
                        <label>Direccion<span style="color: red;">*</span></label>
                        <asp:TextBox ID="txtModalDir" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rvm1" runat="server" ValidationGroup="factu"
                            ControlToValidate="txtModalDir"
                            ErrorMessage="Ingrese la direccion" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group has-feedback " style="margin-top: 20px;">
                        <label>Localidad<span style="color: red;">*</span></label>
                        <asp:TextBox ID="txtModalLocalidad" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rvm2" runat="server" ValidationGroup="factu"
                            ControlToValidate="txtModalLocalidad"
                            ErrorMessage="Ingrese la localidad" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 col-md-offset-2">
                    <div class="form-group has-feedback " style="margin-top: 20px;">
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
                <div class="col-md-4">
                    <div class="form-group has-feedback " style="margin-top: 20px;">
                        <label>Codigo Postal<span style="color: red;">*</span></label>
                        <asp:TextBox ID="txtMModalCP" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rvm3" runat="server" ValidationGroup="factu"
                            ControlToValidate="txtMModalCP"
                            ErrorMessage="Ingrese el CP" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 col-md-offset-2">
                    <div class="form-group has-feedback " style="margin-top: 20px;">
                        <label>Telefono <span style="color: red;">*</span></label>
                        <asp:TextBox ID="txtCel" CssClass="form-control" placeholder="Ingrese telefono" runat="server"></asp:TextBox>
                        <span class="glyphicon glyphicon-earphone form-control-feedback"></span>
                        <asp:RequiredFieldValidator ID="rv1" runat="server" ControlToValidate="txtCel" ForeColor="Red"
                            ErrorMessage="Ingrese el Telefono" ValidationGroup="factu"></asp:RequiredFieldValidator>
                    </div>

                </div>
                <div class="col-md-4">
                    <div class="form-group has-feedback " style="margin-top: 20px;">
                        <label>MAIL <span style="color: red;">*</span></label>
                        <asp:TextBox ID="txtMail" CssClass="form-control" TextMode="Email" placeholder="Ingrese e-mail" runat="server"></asp:TextBox>
                        <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                        <asp:RequiredFieldValidator ID="rv2" runat="server" ControlToValidate="txtMail" ForeColor="Red"
                            ErrorMessage="Ingrese el e-mail" ValidationGroup="factu"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-md-offset-2">
                    <div class="form-group has-feedback " style="margin-top: 20px;">
                        <label>Categoria Deuda</label>
                        <asp:DropDownList CssClass="form-control" ID="DDLCatDeuda" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group has-feedback" style="margin-top: 20px;">
                        <label>Monto <span style="color: red;">*</span></label>
                        <span class="glyphicon glyphicon-usd form-control-feedback"></span>
                        <asp:TextBox ID="txtMonto" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="rv3" runat="server" ControlToValidate="txtMonto" ForeColor="Red"
                        ErrorMessage="Ingrese el Monto" ValidationGroup="factu"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8 col-md-offset-2">
                    <div class="form-group has-feedback " style="margin-top: 20px;">
                        <label>Observaciones</label>
                        <asp:TextBox ID="txtObs" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8 col-md-offset-2" style="text-align: right;">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-warning"
                        OnClick="btnCancelar_Click" />
                    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary"
                        ValidationGroup="factu" OnClick="btnAceptar_Click" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-8 col-md-offset-2" style="margin-bottom: 30px;">
                </div>
            </div>
        </div>

        <asp:Button ID="Button1" Visible="false" runat="server" OnClick="Button1_Click" Text="Button" />

    </div>



    <!-- jQuery 2.1.4 -->
    <script src="../App_Themes/plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <script src="../App_Themes/bootstrap/js/quicksearch.js"></script>

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
    </script>


</asp:Content>
