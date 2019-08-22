<%@ Page Title="" Language="C#" MasterPageFile="~/SSOMaster.Master" AutoEventWireup="true" CodeBehind="CPPBancoRecaudadorAse.aspx.cs" Inherits="CPPPresentacion.Maestros.CPPBancoRecaudadorAse" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphCentro" runat="server" ClientIDMode="Static">
    <script src="../Scripts/Js/Maestros/JsCPPBancoRecaudador.js"></script>
    <div class="container">
        <div class="tituloPagina">
            <h1>Banco Recaudador</h1>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="udp">
        <ContentTemplate>
            <!-- GRID DE LA INFORMACION-->
            <div class="table-responsive">
                <div>
                    <dx:ASPxGridView ID="gvBancosRecaudadores" runat="server" AutoGenerateColumns="False" KeyFieldName="id" Width="100%" OnPageIndexChanged="gvBancosRecaudadores_PageIndexChanged"
                        OnBeforeColumnSortingGrouping="gvBancosRecaudadores_BeforeColumnSortingGrouping" OnPageSizeChanged="gvBancosRecaudadores_PageSizeChanged" OnRowCommand="gvBancosRecaudadores_RowCommand"
                        OnLoad="Grid_Load" EditFormLayoutProperties-SettingsAdaptivity-AdaptivityMode="SingleColumnWindowLimit" SettingsPager-EnableAdaptivity="true" SettingsPager-PageSize="5">
                        <Toolbars>
                            <dx:GridViewToolbar EnableAdaptivity="true">
                                <Items>
                                    <dx:GridViewToolbarItem>
                                        <Template>
                                            <dx:ASPxButton ID="bntNuevo" runat="server" Text="Nuevo" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="False" OnClick="bntNuevo_Click">
                                            </dx:ASPxButton>
                                        </Template>
                                    </dx:GridViewToolbarItem>
                                    <dx:GridViewToolbarItem Text="Exportar" Name="Exportar" Image-IconID="arrows_movedown_16x16" Alignment="Right" ToolTip="Exportar">
                                        <Items>
                                            <dx:GridViewToolbarItem Command="ExportToXls" Text="XLS" ToolTip="Exportar a XLS" Image-IconID="export_exporttoxls_16x16" />
                                            <dx:GridViewToolbarItem Command="ExportToXlsx" Text="XLSX" ToolTip="Exportar a XLSX" Image-IconID="export_exporttoxlsx_16x16" />
                                            <dx:GridViewToolbarItem Command="ExportToCsv" Text="CSV" ToolTip="Exportar a CSV" Image-IconID="export_exporttocsv_16x16" />
                                        </Items>
                                    </dx:GridViewToolbarItem>
                                </Items>
                            </dx:GridViewToolbar>
                        </Toolbars>
                        <Columns>
                            <dx:GridViewDataTextColumn Caption="Acción" Width="80">
                                <DataItemTemplate>
                                    <dx:ASPxButton ID="btEditar" runat="server" Image-IconID="actions_edit_16x16devav" ToolTip="Editar" CommandName="Editar" CssClass="btnGrid" />
                                </DataItemTemplate>
                                <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                <CellStyle CssClass="tablasDatos" VerticalAlign="Middle" />
                            </dx:GridViewDataTextColumn>

                            <dx:GridViewDataColumn Caption="Codigo Entidad" FieldName="codigoEntidad" Width="180" SortOrder="Ascending" UnboundType="String">
                                <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                            </dx:GridViewDataColumn>

                            <dx:GridViewDataColumn Caption="Nombre Entidad" FieldName="nombreEntidad" Width="250" SortOrder="Ascending" UnboundType="Integer">
                                <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                            </dx:GridViewDataColumn>

                            <dx:GridViewDataColumn Caption="Nit" FieldName="nit" Width="167" SortOrder="Ascending" UnboundType="Integer">
                                <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                            </dx:GridViewDataColumn>

                            <dx:GridViewDataTextColumn VisibleIndex="9" Caption="Ver Cuentas Bancarias" Width="190" CellStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <dx:ASPxButton ID="btnVerActividades" runat="server" ToolTip="Ver Cuentas Bancarias" CommandName="MostrarCuentasContables" CssClass="btnGrid" Image-IconID="find_find_16x16gray"></dx:ASPxButton>
                                </DataItemTemplate>
                                <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                <CellStyle CssClass="tablasDatos tablasDatosLink" VerticalAlign="Middle" />
                            </dx:GridViewDataTextColumn>

                        </Columns>
                        <Settings VerticalScrollableHeight="400" />
                        <Settings VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" />
                        <Settings ShowFilterRow="true" />
                        <SettingsBehavior AllowSelectByRowClick="true" />
                        <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" />
                        <SettingsPager>
                            <PageSizeItemSettings Visible="true" Items="5, 10, 50" ShowAllItem="true" />
                        </SettingsPager>
                    </dx:ASPxGridView>
                </div>
            </div>

            <!-- Modal para la creacion y ediccion del banco -->
            <dx:ASPxPopupControl ID="pcBancoRecaudador" runat="server" Width="100%" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcBancoRecaudador"
                HeaderText="Banco Recaudador" AllowDragging="false" PopupAnimationType="Fade" AutoUpdatePosition="true">
                <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MinHeight="35%" MinWidth="63%" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">

                        <div id="contentDiv">
                            <asp:HiddenField ID="hdIdBancoRecaudador" runat="server" Value="" />
                            <dx:ASPxFormLayout runat="server" ID="formLayout" Width="100%" ClientInstanceName="FormLayout">
                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="500" />
                                <Items>
                                    <dx:LayoutGroup Width="100%" ColumnCount="2" GroupBoxDecoration="HeadingLine" Caption="Banco Recaudador" ShowCaption="False">
                                        <GroupBoxStyle>
                                            <Caption Font-Bold="true" Font-Size="16" />
                                        </GroupBoxStyle>
                                        <GridSettings StretchLastItem="true" WrapCaptionAtWidth="60">
                                            <Breakpoints>
                                                <dx:LayoutBreakpoint MaxWidth="1000" ColumnCount="1" Name="S" />
                                                <dx:LayoutBreakpoint MaxWidth="1200" ColumnCount="2" Name="M" />
                                            </Breakpoints>
                                        </GridSettings>
                                        <Items>
                                            <dx:LayoutItem Caption="Código entidad" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtCodigoEntidad" MaxLength="1000" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Nombre de la entidad" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtNombreEntidad" MaxLength="1000" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datos" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere Nombre de la entidad"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Nit" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtNit" MaxLength="10" runat="server" Text="" AutoPostBack="false">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datos" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere Nit"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Cuenta" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxDropDownEdit ClientInstanceName="cbCuentasContablesC" ID="cbCuentasContablesC" runat="server"
                                                            AutoPostBack="false">
                                                            <DropDownApplyButton Visibility="Always"></DropDownApplyButton>
                                                            <SettingsAdaptivity Mode="OnWindowInnerWidth" ModalDropDownCaption="Cuenta" />
                                                            <DropDownWindowTemplate>
                                                                <dx:ASPxListBox ID="cbCuentaContable" runat="server"
                                                                    SelectionMode="CheckColumn" Caption=""
                                                                    RepeatColumns="1"
                                                                    TextField="nombreCuenta"
                                                                    ValueField="id"
                                                                    ValueType="System.Int32"
                                                                    Border-BorderStyle="None"
                                                                    Width="100%"
                                                                    Height="400"
                                                                    ClientInstanceName="cbCuentaContable">
                                                                    <FilteringSettings ShowSearchUI="true" EnableAnimation="true" EditorNullText="Buscar"/>
                                                                    <CaptionSettings Position="Top" />
                                                                    <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datos" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                        <RequiredField IsRequired="True" ErrorText="Se requiere al menos una cuenta contable"></RequiredField>
                                                                    </ValidationSettings>
                                                                    <ClientSideEvents SelectedIndexChanged="updateText" Init="updateText" />
                                                                </dx:ASPxListBox>
                                                            </DropDownWindowTemplate>
                                                            <ClientSideEvents DropDownCommandButtonClick="OnDropDownCommandButtonClick" TextChanged="synchronizeListBoxValues" DropDown="synchronizeListBoxValues" />
                                                        </dx:ASPxDropDownEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                            </dx:ASPxFormLayout>
                        </div>
                        <div class="modal-footer">
                            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" CssClass="btnfeatures btnBordesRedondos tipoLetra" ValidationGroup="datos" OnClick="btnAceptar_Click" />
                            <dx:ASPxButton ID="bntCerrar" runat="server" Text="Cerrar" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e) { pcBancoRecaudador.Hide(); }" />
                            </dx:ASPxButton>
                        </div>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ContentStyle>
                    <Paddings PaddingBottom="5px" />
                </ContentStyle>
            </dx:ASPxPopupControl>

            <!-- Modal para la consulta de las cuentas contables-->
            <dx:ASPxPopupControl ID="ppCuentasContablesAsociadas" runat="server" Width="1000" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ppCuentasContablesAsociadas"
                HeaderText="Cuentas Contables" AllowDragging="false" PopupAnimationType="Fade" AutoUpdatePosition="true">
                <ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('entryGroup'); }" />
                <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MinHeight="40%" MinWidth="30%" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <dx:ASPxPanel ID="ASPxPanel3" runat="server" DefaultButton="btOK">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>
                        <dx:ASPxListBox ID="ckConsulta" runat="server" ClientInstanceName="lbChoosen" EnableSynchronization="True"
                            Height="300px" SelectionMode="Single" Caption=""
                            Width="100%"
                            EnableViewState="false"
                            RepeatColumns="1"
                            TextField="nombreCuenta"
                            ValueField="id"
                            ValueType="System.Int32"
                            Border-BorderStyle="None">
                            <CaptionSettings Position="Top" />
                        </dx:ASPxListBox>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>

            <!-- Modal de los mensajes-->
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
