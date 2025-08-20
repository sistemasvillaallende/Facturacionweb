<%@ Page Title="" Language="C#" MasterPageFile="~/MP/MP.Master" AutoEventWireup="true" CodeBehind="Facturas.aspx.cs" Inherits="Facturacion.Reportes.Facturas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="margin-top: 60px;">
            <div class="row">
                <div class="col-xs-12" style="text-align: center;">
                    <h3>FACTURACION</h3>
                </div>
            </div>
        <div class="row">
            <div class="col-md-12" style="text-align: center; margin-top:20px; margin-bottom:20px;">
                <a href="../Secure/Facturacion.aspx" class="btn btn-warning">Volver</a>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <iframe src="" id="frame" runat="server" height="1000" width="100%" style="overflow-y:hidden;"></iframe>
            </div>
        </div>
    </div>
</asp:Content>
