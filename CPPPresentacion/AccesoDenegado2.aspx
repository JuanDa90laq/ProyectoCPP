<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccesoDenegado2.aspx.cs" Inherits="SPF.UI.AccesoDenegado2" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<link href="Css/CPPCss.css" rel="stylesheet" type="text/css" />
<link href="Css/JqueryCss.css" rel="stylesheet" type="text/css" />
<link href="sso/css/finagro.css" rel="stylesheet" type="text/css" />
<link href="sso/css/breadcrumb.css" rel="stylesheet" type="text/css" />
<link rel="shortcut icon" href="https://www.finagro.com.co/sites/default/files/image002.jpg" type="image/jpeg" />
<link href="Content/bootstrap.min.css" rel="stylesheet" />

<br /><br /><br /><br />
<form id="frmAccesoDeg" runat="server">
    <div id="header">
        <dx:ASPxCallbackPanel ID="cbpEncabezado" runat="server" FixedPosition="WindowTop" CssClass="Encabezado" Collapsible="true">
            <SettingsAdaptivity CollapseAtWindowInnerWidth="800" />
            <PanelCollection>
                <dx:PanelContent>
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align: left">
                                <dx:ASPxImage runat="server" ID="ASPxImage1" ImageUrl="Imagenes/finagro_blanco.png" height="65px"></dx:ASPxImage>                                
                            </td>
                            <td style="width: 5px; height: 10px;"></td>
                            <td style="width: 50px; height: 40px;">
                                <dx:ASPxImage runat="server" ID="ImagenUsuario" ImageUrl="Imagenes/Cpp.png" Width="100px"></dx:ASPxImage>
                            </td>
                        </tr>
                    </table>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
    </div>

    <table style="width: 50%" align="center">
        <tr>
            <td>
                <dx:ASPxButton ID="btnMensaje" runat="server" AllowFocus="False" AutoPostBack="False" CausesValidation="False"
                    Cursor="auto" UseSubmitBehavior="False" RenderMode="Link" ClientInstanceName="btnMensaje">
                    <Image IconID="actions_close_32x32office2013"></Image>
                    <BackgroundImage Repeat="NoRepeat" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxLabel runat="server" Text="<b>Acceso Denegado</b>" EncodeHtml="False" Font-Size="XX-Large">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <dx:ASPxLabel ID="lblMensaje" runat="server" Text="ASPxLabel" EncodeHtml="False"
                    ClientInstanceName="lblMensaje">
                </dx:ASPxLabel>
            </td>
        </tr>
    </table>

    <div id="footer" style="text-align: center">
        <dx:ASPxCallbackPanel ID="BottomPanel" runat="server" FixedPosition="WindowBottom" CssClass="PieDePagina" Collapsible="true">
            <SettingsAdaptivity CollapseAtWindowInnerWidth="800" />
            <PanelCollection>
                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                    <div class="PieDePaginaTexto1">
                        Al usar este sitio, acepta nuestro 
                        <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="Aviso Legal" CssClass="PieDePaginaTexto2">
                            <clientsideevents click="function(s, e){pcAviso.Show();}" />
                        </dx:ASPxHyperLink>
                        y nuestra
                            <dx:ASPxHyperLink ID="hlPrivacidad" runat="server" Text="Politica de Privacidad." CssClass="PieDePaginaTexto2">
                                <clientsideevents click="function(s, e){pcPrivacidad.Show();}" />
                            </dx:ASPxHyperLink>
                        No olvide verificar nuestras
                                <dx:ASPxHyperLink ID="hlRecomendaciones" runat="server" Text="Recomendaciones de Seguridad."
                                    CssClass="PieDePaginaTexto2">
                                    <clientsideevents click="function(s, e){pcRecomendaciones.Show();}" />
                                </dx:ASPxHyperLink>
                    </div>
                    <div class="PieDePaginaTexto3">
                        Copyright © 2013 - 2018
                            <dx:ASPxHyperLink ID="hlFinagro" runat="server" Text="FINAGRO" CssClass="PieDePaginaTexto4"
                                NavigateUrl="http://www.finagro.com.co/" ToolTip="Visita nuestra página Web">
                            </dx:ASPxHyperLink>
                        - Carrera 13 No. 28-17 - Línea de Atención: 320 3377 - Bogotá, D.C. - Colombia
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>
    </div>
</form>
