<%@ Page Title="" Language="C#" MasterPageFile="~/SSOMaster.Master" AutoEventWireup="true" CodeBehind="CPPAdminEntidadesAgropecuarias.aspx.cs" Inherits="CPPPresentacion.Maestros.CPPAdminEntidadesAgropecuarias" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphCentro" runat="server" ClientIDMode="Static">

    <script src="../Scripts/Js/Maestros/JsActividadAgropecuaria.js" type="text/javascript"></script>

    <div class="divAgrupacionesbordesRedondos divAgrupacionesPrincipalInternas" style="width: 405px;">
        <dx:ASPxLabel ID="lBTitulo" runat="server" Text="Actividades Agropecuarias - Consultas." CssClass="LbTitulosbordesSombras LbTitulosbordesRedondos LbTitulosFeatures tipoLetra" />
        <div class="divAgrupacionesSecunInternas">
            <table style="width: 400px;">
                <tr>
                    <td>
                        <dx:ASPxLabel ID="lbConsultar" runat="server" Text="Actividades Agropecuarias" CssClass="LbFeatures tipoLetra" />
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="cbActividades" runat="server" ValueType="System.String" ValueField="id" TextField="actividad" CssClass="tipoLetra"></dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
            <div class="divBotones">
                <dx:ASPxButton ID="btnConsultar" runat="server" Text="Consultar" CssClass="btnfeatures btnBordesRedondos tipoLetra" OnClick="btnConsultar_Click"></dx:ASPxButton>
                &nbsp;
                <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btnfeatures btnBordesRedondos tipoLetra" OnClick="btnNuevo_Click"></dx:ASPxButton>
            </div>
        </div>
    </div>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:HiddenField ID="hdControlEditar" runat="server" Value="0" />

            <div id="divConsulta" class="divAgrupacionesbordesRedondos divAgrupacionesPrincipalInternas divAgrupacionesinferiores" runat="server" visible="false" style="width: 885px">
                <div class="divNoAgrupacionesResultados" style="width: auto">
                    <dx:ASPxGridView ID="gvActividadesAgropeacuarias" runat="server" AutoGenerateColumns="False" KeyFieldName="id" OnRowCommand="gvActividadesAgropeacuarias_RowCommand" Width="100%"
                        OnBeforeColumnSortingGrouping="gvActividadesAgropeacuarias_BeforeColumnSortingGrouping"
                        OnPageIndexChanged="gvActividadesAgropeacuarias_PageIndexChanged"
                        OnPageSizeChanged="gvActividadesAgropeacuarias_PageSizeChanged" OnHtmlRowCreated="gvActividadesAgropeacuarias_HtmlRowCreated">
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Acción" Width="125">
                                <DataItemTemplate>
                                    <dx:ASPxButton ID="btEditar" runat="server" Text="Editar" CssClass="btnfeatures btnBordesRedondos tipoLetra" CommandName="Editar" ValidationGroup="ActividadEdicion" />
                                    <dx:ASPxButton ID="btElimnar" runat="server" Text="Eliminar" CssClass="btnfeatures btnBordesRedondos tipoLetra" Visible="false" CommandName="Eliminar" />
                                </DataItemTemplate>
                                <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                <CellStyle CssClass="tablasDatos" VerticalAlign="Middle" />
                            </dx:GridViewDataTextColumn>

                            <dx:GridViewDataColumn Caption="Número Interno" FieldName="id" Width="150" SortOrder="Ascending" UnboundType="Integer">
                                <DataItemTemplate>
                                    <dx:ASPxLabel ID="lbIdentificadorAgro" runat="server" Text='<%#Eval("id")%>' Visible="true" CssClass="LbResultadosTablas tipoLetraresult" />
                                </DataItemTemplate>
                                <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                <CellStyle CssClass="tablasDatos" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                            </dx:GridViewDataColumn>

                            <dx:GridViewDataColumn Caption="Actividad Agropecuaria" FieldName="actividad" Width="500" SortOrder="Ascending" UnboundType="String">
                                <DataItemTemplate>
                                    <dx:ASPxLabel ID="lbActividadAgro" runat="server" Text='<%#Eval("actividad")%>' Visible="true" CssClass="LbResultadosTablas tipoLetraresult" />
                                    <dx:ASPxTextBox ID="txtActividad" ValidationSettings-CausesValidation="true" MaxLength="500" runat="server" Text='<%#Eval("actividad")%>' Width="463px" CssClass="txtEdicionTablas tipoLetraresult" Visible="false" />
                                    <asp:RequiredFieldValidator ID="vldActividad" Enabled="false" runat="server" CssClass="tipoLetravalidadoresTablas validadoresTablas" ErrorMessage="!Digite la Actividad."
                                        ValidationGroup="ActividadEdicion" ControlToValidate="txtActividad"></asp:RequiredFieldValidator>​
                                </DataItemTemplate>
                                <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                <CellStyle CssClass="tablasDatos validadoresTablas" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                            </dx:GridViewDataColumn>

                            <dx:GridViewDataColumn Caption="Estado" Width="100">
                                <DataItemTemplate>
                                    <dx:ASPxCheckBox ID="ckEstado" Checked='<%#Eval("estado")%>' runat="server" Enabled="false" CssClass="checkTablas" />
                                </DataItemTemplate>
                                <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                <CellStyle CssClass="tablasDatos" />
                            </dx:GridViewDataColumn>
                        </Columns>
                        <Settings VerticalScrollableHeight="360" />
                        <Settings VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" />
                        <SettingsPager>
                            <PageSizeItemSettings Visible="true" Items="10, 20, 50" />
                        </SettingsPager>
                    </dx:ASPxGridView>
                </div>
            </div>

            <dx:ASPxPopupControl ID="ppNuevActividad" runat="server" Modal="true" HeaderText="Actividad Agropecuaria" PopupVerticalAlign="WindowCenter"
                PopupHorizontalAlign="WindowCenter" AllowResize="false" CssClass="LbFeatures tipoLetra" CloseAction="CloseButton" ClientInstanceName="ppNuevActividad" ScrollBars="None"
                Width="500px">
                <ContentCollection>
                    <dx:PopupControlContentControl>
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lbActividadNueva" runat="server" Text="Actividad Agropecuaria:" EncodeHtml="False" CssClass="LbFeatures tipoLetra" />
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="txtActividadNueva" MaxLength="500" ValidationSettings-CausesValidation="true" runat="server" Text="" Width="200px" CssClass="txtEdicionTablas tipoLetraresult" />
                                                <asp:RequiredFieldValidator ID="vldActividadNuevo" Name="ValidadorNuevo" ControlToValidate="txtActividadNueva" ValidationGroup="Nuevo"
                                                    runat="server" CssClass="tipoLetravalidadoresTablas validadoresTablas" ErrorMessage="!Digite la Actividad." />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="lbEstadoNueva" runat="server" Text="Estado:" EncodeHtml="False" CssClass="LbFeatures tipoLetra" />
                                            </td>
                                            <td>
                                                <dx:ASPxCheckBox ID="ckEstadoNuevo" runat="server" Checked="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table>
                                        <tr>
                                            <td style="width: 250PX"></td>
                                            <td>

                                                <dx:ASPxButton ID="btnAceptarNuevo" ValidationGroup="Nuevo" runat="server" Text="Aceptar" CssClass="btnfeatures btnBordesRedondos tipoLetra" OnClick="btnAceptarNuevo_Click" />
                                                <dx:ASPxButton ID="btnCancelarNuevo" runat="server" Text="Cancelar" CssClass="btnfeatures btnBordesRedondos tipoLetra">
                                                    <ClientSideEvents Click="function(s, e) {   ppNuevActividad.Hide(); }" />
                                                </dx:ASPxButton>

                                            </td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                        </table>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
            
            <dx:ASPxPopupControl ID="ppMensajesConfirmacion" runat="server" Modal="true" HeaderText="Resultado Operación" PopupVerticalAlign="WindowCenter"
                PopupHorizontalAlign="WindowCenter" AllowResize="false" CloseAction="CloseButton" ClientInstanceName="ppMensajesConfirmacion" ScrollBars="None"
                Width="700px">
                <ContentCollection>
                    <dx:PopupControlContentControl>
                        <table>
                            <tr>
                                <td style="vertical-align: top">
                                    <dx:ASPxButton ID="btCerrarSuperior" runat="server" AllowFocus="False" AutoPostBack="False" CausesValidation="False"
                                        Cursor="auto" UseSubmitBehavior="False" RenderMode="Link" ClientInstanceName="btCerrarSuperior">
                                        <Image IconID="status_warning_32x32"></Image>
                                        <BackgroundImage Repeat="NoRepeat" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="lbMensaje" runat="server" Text="" EncodeHtml="False" ClientInstanceName="lbMensaje">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="align-items: center">

                                    <dx:ASPxButton ID="dvBtnAceptar" runat="server" Text="Aceptar">
                                        <ClientSideEvents Click="function(s, e) {  ppMensajesConfirmacion.Hide(); }" />
                                    </dx:ASPxButton>
                                    <dx:ASPxButton ID="dvBtnCancelar" runat="server" Text="Cancelar">
                                        <ClientSideEvents Click="function(s, e) {  ppMensajesConfirmacion.Hide(); }" />
                                    </dx:ASPxButton>

                                </td>
                            </tr>
                        </table>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
