<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccesoDenegado.aspx.cs" MasterPageFile="SSOMaster.Master" Inherits="SPF.UI.AccesoDenegado" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="contenido" ContentPlaceHolderID="cphCentro" runat="server">
    <br/>
    <br/>
    <br/>
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
            <td colspan="2"><br/>
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
</asp:Content>
