<%@ Page Title="" Language="C#" MasterPageFile="~/SSOMaster.Master" AutoEventWireup="true" CodeBehind="SSODefault.aspx.cs" Inherits="SPF.UI.SSODefault" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="contenido" ContentPlaceHolderID="cphCentro" runat="server">
    <style>
        .divTable {
            display: table;
            width: 100%;
        }

        .divTableRow {
            display: table-row;
        }

        .divTableHeading {
            background-color: #EEE;
            display: table-header-group;
        }

        .divTableCell, .divTableHead {            
            display: table-cell;
            padding: 3px 10px;
        }

        .divTableHeading {
            background-color: #EEE;
            display: table-header-group;
            font-weight: bold;
        }

        .divTableFoot {
            background-color: #EEE;
            display: table-footer-group;
            font-weight: bold;
        }

        .divTableBody {
            display: table-row-group;
        }
    </style>
    <div class="divTable">
        <div class="divTableBody">
            <div class="divTableRow">
                <div class="divTableCell">
                    <dx:ASPxListBox ID="lSesion" runat="server" ValueType="System.String" Height="600px"></dx:ASPxListBox>
                </div>                
                <div class="divTableCell">
                    <dx:ASPxListBox ID="lAplicacion" runat="server" ValueType="System.String" Height="600px"></dx:ASPxListBox>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
