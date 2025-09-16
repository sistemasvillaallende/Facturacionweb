<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MP.Master" AutoEventWireup="True"
    CodeBehind="PagoCedulon.aspx.cs" Inherits="Facturacion.Secure.PagoCedulon"
    ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Estilos modernos para la landing de pago */
        .payment-container {
            max-width: 1200px;
            margin: 0 auto;
            padding: 20px;
            background: #f8f9fa;
            min-height: 100vh;
        }
        
        .payment-header {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 3rem 0;
            margin: -15px -15px 40px -15px;
            border-radius: 0 0 20px 20px;
            box-shadow: 0 8px 32px rgba(0, 0, 0, 0.1);
            text-align: center;
        }
        
        .payment-title {
            font-size: 2.8rem;
            font-weight: 300;
            margin: 0 0 10px 0;
            text-shadow: 0 2px 4px rgba(0,0,0,0.3);
        }
        
        .payment-subtitle {
            font-size: 1.2rem;
            opacity: 0.9;
            margin: 0;
        }
        
        .card-modern {
            background: white;
            border-radius: 20px;
            box-shadow: 0 8px 32px rgba(0, 0, 0, 0.1);
            padding: 30px;
            margin-bottom: 30px;
            border: none;
            transition: all 0.3s ease;
        }
        
        .card-modern:hover {
            transform: translateY(-2px);
            box-shadow: 0 12px 40px rgba(0, 0, 0, 0.15);
        }
        
        .debt-info-card {
            background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%);
            color: white;
        }
        
        .debt-info-card .card-title {
            color: white;
            font-size: 1.5rem;
            margin-bottom: 20px;
        }
        
        .debt-amount {
            font-size: 3rem;
            font-weight: bold;
            text-align: center;
            margin: 20px 0;
            text-shadow: 0 2px 4px rgba(0,0,0,0.3);
        }
        
        .debt-detail {
            display: flex;
            justify-content: space-between;
            margin: 10px 0;
            padding: 10px 0;
            border-bottom: 1px solid rgba(255,255,255,0.2);
        }
        
        .debt-detail:last-child {
            border-bottom: none;
        }
        
        .debt-detail-label {
            font-weight: 500;
            opacity: 0.9;
        }
        
        .debt-detail-value {
            font-weight: bold;
        }
        
        .card-selector {
            background: linear-gradient(135deg, #56ab2f 0%, #a8e6cf 100%);
            color: white;
        }
        
        .card-selector .card-title {
            color: white;
            font-size: 1.5rem;
            margin-bottom: 20px;
        }
        
        .card-option {
            background: rgba(255,255,255,0.1);
            border: 2px solid rgba(255,255,255,0.3);
            border-radius: 15px;
            padding: 20px;
            margin: 15px 0;
            cursor: pointer;
            transition: all 0.3s ease;
            display: flex;
            align-items: center;
        }
        
        .card-option:hover {
            background: rgba(255,255,255,0.2);
            border-color: rgba(255,255,255,0.6);
            transform: scale(1.02);
        }
        
        .card-option.selected {
            background: rgba(255,255,255,0.3);
            border-color: white;
            transform: scale(1.05);
        }
        
        .card-logo {
            width: 60px;
            height: 40px;
            background: white;
            border-radius: 8px;
            margin-right: 15px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-weight: bold;
            color: #333;
        }
        
        .card-name {
            font-size: 1.2rem;
            font-weight: 500;
        }
        
        .installment-plans {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            display: none;
        }
        
        .installment-plans .card-title {
            color: white;
            font-size: 1.5rem;
            margin-bottom: 20px;
        }
        
        .plan-option {
            background: rgba(255,255,255,0.1);
            border: 2px solid rgba(255,255,255,0.3);
            border-radius: 15px;
            padding: 20px;
            margin: 15px 0;
            cursor: pointer;
            transition: all 0.3s ease;
        }
        
        .plan-option:hover {
            background: rgba(255,255,255,0.2);
            border-color: rgba(255,255,255,0.6);
            transform: scale(1.02);
        }
        
        .plan-option.selected {
            background: rgba(255,255,255,0.3);
            border-color: white;
            transform: scale(1.05);
        }
        
        .plan-installments {
            font-size: 1.3rem;
            font-weight: bold;
            margin-bottom: 10px;
        }
        
        .plan-amount {
            font-size: 1.1rem;
            margin-bottom: 5px;
        }
        
        .plan-cost {
            font-size: 0.95rem;
            opacity: 0.8;
        }
        
        .discount-badge {
            background: #28a745;
            color: white;
            padding: 5px 15px;
            border-radius: 20px;
            font-size: 0.85rem;
            font-weight: bold;
            display: inline-block;
            margin-top: 10px;
        }
        
        .btn-pay {
            background: linear-gradient(45deg, #28a745, #20c997);
            color: white;
            border: none;
            border-radius: 50px;
            padding: 15px 40px;
            font-size: 1.2rem;
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            transition: all 0.3s ease;
            box-shadow: 0 4px 15px rgba(40, 167, 69, 0.3);
            width: 100%;
        }
        
        .btn-pay:hover {
            transform: translateY(-3px);
            box-shadow: 0 8px 25px rgba(40, 167, 69, 0.4);
            background: linear-gradient(45deg, #218838, #1dc2a3);
        }
        
        .btn-pay:disabled {
            background: #6c757d;
            cursor: not-allowed;
            transform: none;
            box-shadow: none;
        }
        
        .loading-spinner {
            display: none;
            text-align: center;
            padding: 50px;
        }
        
        .spinner {
            border: 4px solid #f3f3f3;
            border-top: 4px solid #667eea;
            border-radius: 50%;
            width: 50px;
            height: 50px;
            animation: spin 1s linear infinite;
            margin: 0 auto 20px;
        }
        
        @keyframes spin {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
        }
        
        .error-message {
            background: #dc3545;
            color: white;
            padding: 20px;
            border-radius: 15px;
            margin: 20px 0;
            text-align: center;
        }
        
        .fade-in {
            animation: fadeIn 0.6s ease-in;
        }
        
        @keyframes fadeIn {
            from { opacity: 0; transform: translateY(30px); }
            to { opacity: 1; transform: translateY(0); }
        }
        
        .overdue-warning {
            background: #ffc107;
            color: #212529;
            padding: 15px;
            border-radius: 10px;
            margin: 15px 0;
            font-weight: 500;
            text-align: center;
        }
        
        @media (max-width: 768px) {
            .payment-title {
                font-size: 2rem;
            }
            
            .debt-amount {
                font-size: 2.5rem;
            }
            
            .card-modern {
                padding: 20px;
                margin-bottom: 20px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="payment-container fade-in">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
        <!-- Header de Pago -->
        <div class="payment-header">
            <h1 class="payment-title">💳 Portal de Pagos</h1>
            <p class="payment-subtitle">Paga tu cedulón de forma rápida y segura</p>
        </div>
        
        <!-- Mensaje de Error -->
        <div id="divError" runat="server" visible="false" class="error-message">
            <h4>❌ Error</h4>
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        
        <!-- Loading Spinner -->
        <div id="loadingSpinner" class="loading-spinner">
            <div class="spinner"></div>
            <p>Cargando información del cedulón...</p>
        </div>
        
        <!-- Contenido Principal -->
        <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="divPrincipal" runat="server" visible="false">
                    <div class="row">
                        <!-- Información de la Deuda -->
                        <div class="col-md-6">
                            <div class="card-modern debt-info-card">
                                <h3 class="card-title">📄 Información del Cedulón</h3>
                                
                                <!-- Número de Cedulón -->
                                <div class="debt-detail">
                                    <span class="debt-detail-label">Número de Cedulón:</span>
                                    <span class="debt-detail-value">
                                        <asp:Label ID="lblNroCedulon" runat="server"></asp:Label>
                                    </span>
                                </div>
                                
                                <!-- Datos del Deudor -->
                                <div class="debt-detail">
                                    <span class="debt-detail-label">CUIT/CUIL:</span>
                                    <span class="debt-detail-value">
                                        <asp:Label ID="lblCuit" runat="server"></asp:Label>
                                    </span>
                                </div>
                                
                                <div class="debt-detail">
                                    <span class="debt-detail-label">Contribuyente:</span>
                                    <span class="debt-detail-value">
                                        <asp:Label ID="lblContribuyente" runat="server"></asp:Label>
                                    </span>
                                </div>
                                
                                <!-- Descripción -->
                                <div class="debt-detail">
                                    <span class="debt-detail-label">Concepto:</span>
                                    <span class="debt-detail-value">
                                        <asp:Label ID="lblDescripcion" runat="server"></asp:Label>
                                    </span>
                                </div>
                                
                                <!-- Fecha de Vencimiento -->
                                <div class="debt-detail">
                                    <span class="debt-detail-label">Fecha de Vencimiento:</span>
                                    <span class="debt-detail-value">
                                        <asp:Label ID="lblVencimiento" runat="server"></asp:Label>
                                    </span>
                                </div>
                                
                                <!-- Advertencia de Vencimiento -->
                                <div id="divOverdue" runat="server" visible="false" class="overdue-warning">
                                    ⚠️ Este cedulón se encuentra vencido. Se aplicarán intereses.
                                </div>
                                
                                <!-- Monto Original -->
                                <div class="debt-detail">
                                    <span class="debt-detail-label">Monto Original:</span>
                                    <span class="debt-detail-value">
                                        $<asp:Label ID="lblMontoOriginal" runat="server"></asp:Label>
                                    </span>
                                </div>
                                
                                <!-- Intereses -->
                                <div id="divIntereses" runat="server" visible="false">
                                    <div class="debt-detail">
                                        <span class="debt-detail-label">Intereses:</span>
                                        <span class="debt-detail-value">
                                            $<asp:Label ID="lblIntereses" runat="server"></asp:Label>
                                        </span>
                                    </div>
                                </div>
                                
                                <!-- Monto Total -->
                                <div class="debt-amount">
                                    $<asp:Label ID="lblMontoTotal" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        
                        <!-- Selector de Tarjeta y Planes -->
                        <div class="col-md-6">
                            <!-- Selector de Tarjeta -->
                            <div class="card-modern card-selector">
                                <h3 class="card-title">💳 Seleccionar Tarjeta</h3>
                                
                                <asp:Repeater ID="rptTarjetas" runat="server" OnItemCommand="rptTarjetas_ItemCommand">
                                    <ItemTemplate>
                                        <div class="card-option" data-tarjeta-id='<%# Eval("Id") %>'>
                                            <div class="card-logo">
                                                <%# Eval("Logo") %>
                                            </div>
                                            <div>
                                                <div class="card-name"><%# Eval("Nombre") %></div>
                                                <small><%# Eval("Descripcion") %></small>
                                            </div>
                                            <asp:HiddenField ID="hdnTarjetaId" runat="server" Value='<%# Eval("Id") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                
                                <asp:HiddenField ID="hdnTarjetaSeleccionada" runat="server" />
                            </div>
                            
                            <!-- Planes de Cuotas -->
                            <div id="divPlanes" class="card-modern installment-plans">
                                <h3 class="card-title">📊 Planes de Cuotas</h3>
                                
                                <asp:Repeater ID="rptPlanes" runat="server">
                                    <ItemTemplate>
                                        <div class="plan-option" data-plan-id='<%# Eval("Id") %>'>
                                            <div class="plan-installments">
                                                <%# Eval("CantidadCuotas") %> Cuotas
                                            </div>
                                            <div class="plan-amount">
                                                $<%# Eval("MontoCuota", "{0:N2}") %> por mes
                                            </div>
                                            <div class="plan-cost">
                                                Costo financiero: <%# Eval("CostoFinanciero", "{0:P2}") %>
                                            </div>
                                            <div class="plan-cost">
                                                Total: $<%# Eval("MontoTotal", "{0:N2}") %>
                                            </div>
                                            
                                            <div id="divDescuento" runat="server" visible='<%# Convert.ToDecimal(Eval("PorcentajeDescuento")) > 0 %>'>
                                                <span class="discount-badge">
                                                    <%# Eval("PorcentajeDescuento", "{0:P0}") %> OFF sobre intereses
                                                </span>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                
                                <asp:HiddenField ID="hdnPlanSeleccionado" runat="server" />
                            </div>
                            
                            <!-- Botón de Pago -->
                            <div class="card-modern" id="divPagar" style="display: none;">
                                <asp:Button ID="btnPagar" runat="server" Text="💰 Proceder al Pago" 
                                    CssClass="btn-pay" OnClick="btnPagar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <!-- Scripts -->
    <script src="../App_Themes/plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <script src="../App_Themes/bootstrap/js/bootstrap.min.js"></script>
    
    <script type="text/javascript">
        $(document).ready(function() {
            // Ocultar spinner cuando la página esté cargada
            setTimeout(function() {
                $('#loadingSpinner').fadeOut();
                $('#<%= divPrincipal.ClientID %>').fadeIn();
            }, 1000);
            
            // Manejo de selección de tarjeta
            $('.card-option').click(function() {
                $('.card-option').removeClass('selected');
                $(this).addClass('selected');
                
                var tarjetaId = $(this).data('tarjeta-id');
                $('#<%= hdnTarjetaSeleccionada.ClientID %>').val(tarjetaId);
                
                // Llamar al servidor para cargar planes
                __doPostBack('<%= hdnTarjetaSeleccionada.UniqueID %>', '');
            });
            
            // Manejo de selección de plan
            $(document).on('click', '.plan-option', function() {
                $('.plan-option').removeClass('selected');
                $(this).addClass('selected');
                
                var planId = $(this).data('plan-id');
                $('#<%= hdnPlanSeleccionado.ClientID %>').val(planId);
                
                // Mostrar botón de pago
                $('#divPagar').fadeIn();
            });
        });
        
        // Función para mostrar planes
        function mostrarPlanes() {
            $('#divPlanes').fadeIn();
        }
    </script>
</asp:Content>