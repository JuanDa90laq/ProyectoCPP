<%@ Page Title="" Language="C#" MasterPageFile="~/SSOMaster.Master" AutoEventWireup="true" CodeBehind="CCPCuentasInterfaces.aspx.cs" Inherits="CPPPresentacion.Maestros.CCPCuentasInterfaces" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphCentro" runat="server" ClientIDMode="Static">

    <script src="../Scripts/Js/Maestros/JsCPPInterfaz.js"></script>

    <div class="container">
        <div class="tituloPagina">
            <h1>Asociar cuentas contables a procesos contables</h1>
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="udpCuentasInterfaces">
        <ContentTemplate>

            <!-- GRID DE LA INFORMACION-->
            <div class="table-responsive" id="tablaBeneficio">

                <dx:ASPxGridView ID="gvInterfacesCuentas" runat="server" AutoGenerateColumns="False" KeyFieldName="cppCPt_id;cppCuentas" Width="100%" 
                    OnRowCommand="gvInterfacesCuentas_RowCommand" OnBeforeColumnSortingGrouping="gvInterfacesCuentas_BeforeColumnSortingGrouping" OnLoad="gvInterfacesCuentas_Load"
                    OnPageIndexChanged="gvInterfacesCuentas_PageIndexChanged" OnPageSizeChanged="gvInterfacesCuentas_PageSizeChanged">
                    <Toolbars>
                        <dx:GridViewToolbar>
                            <Items>
                                <dx:GridViewToolbarItem>
                                    <Template>
                                        <dx:ASPxButton ID="BtnNuevo" runat="server" Text="Nuevo" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="False" OnClick="btnNuevo_Click">
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

                        <dx:GridViewDataTextColumn Caption="Acción" Width="75">
                            <DataItemTemplate>
                                <dx:ASPxButton ID="btEditar" runat="server" Image-IconID="actions_edit_16x16devav" ToolTip="Editar" CommandName="Editar" CssClass="btnGrid">
                                </dx:ASPxButton>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos" VerticalAlign="Middle" />
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataColumn Caption="Interfaz contable" FieldName="interfaz" Width="300" SortOrder="Ascending" UnboundType="String" SortIndex="1">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Tipo cesión" FieldName="cesion" Width="250" SortOrder="Ascending" UnboundType="String" SortIndex="2">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Tipo cuenta" FieldName="cuenta" Width="150" SortOrder="Ascending" UnboundType="Decimal" SortIndex="3">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Calificación" FieldName="calificacion" Width="130" SortOrder="Ascending" UnboundType="String" SortIndex="5">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataTextColumn VisibleIndex="9" Caption="Ver cuentas" Width="130" CellStyle-HorizontalAlign="Center">
                            <DataItemTemplate>
                                <dx:ASPxButton ID="btnCuentas" runat="server" ToolTip="Ver cuentas" CommandName="MostrarCuentas" CssClass="btnGrid" Image-IconID="find_find_16x16gray"></dx:ASPxButton>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos tablasDatosLink" VerticalAlign="Middle" />
                        </dx:GridViewDataTextColumn>

                    </Columns>
                    <Settings VerticalScrollableHeight="360" />
                    <Settings VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" />
                    <Settings ShowFilterRow="true" />
                    <SettingsBehavior AllowSelectByRowClick="true" />
                    <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" />
                    <SettingsPager>
                        <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                    </SettingsPager>
                </dx:ASPxGridView>

            </div>

            <!-- Modal para la creacion y ediccion del beneficiario -->
            <dx:ASPxPopupControl ID="pcInterfazCuenta" runat="server" Width="1000" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcInterfazCuenta"
                HeaderText="Interfaz cuenta contable" AllowDragging="false" PopupAnimationType="Fade" AutoUpdatePosition="true">
                <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MinHeight="55%" MinWidth="50%" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btOK">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>
                        <div id="contentDiv" class="clientContainer">
                            <asp:HiddenField ID="hdValores" runat="server" Value="" />
                            <asp:HiddenField ID="hdIdInterfazCuenta" runat="server" Value="" />
                            <dx:ASPxFormLayout runat="server" ID="formLayout" Width="100%" ClientInstanceName="FormLayout">
                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="500" />
                                <Items>
                                    <dx:LayoutGroup Width="100%" ColumnCount="2" GroupBoxDecoration="HeadingLine" Caption="Cuenta Contable" ShowCaption="False">
                                        <GroupBoxStyle>
                                            <Caption Font-Bold="true" Font-Size="16" />
                                        </GroupBoxStyle>
                                        <GridSettings StretchLastItem="true" WrapCaptionAtWidth="660">
                                            <Breakpoints>
                                                <dx:LayoutBreakpoint MaxWidth="800" ColumnCount="1" Name="S" />
                                                <dx:LayoutBreakpoint MaxWidth="1000" ColumnCount="2" Name="M" />
                                            </Breakpoints>
                                        </GridSettings>
                                        <Items>
                                            <dx:LayoutItem Caption="Interfaz contable" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="M"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbInterfaz" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="cppIn_id" TextField="cppIn_descripcion" EnableSynchronization="False">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere tipo de cesión"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Tipo de cesión" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="M"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbTipoCesion" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) { OnTipoCesionChanged(s); }" />
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere tipo de cesión"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Tipo de cuenta" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="M"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbTipoCuenta" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) { OnTipoCuentaChanged(s); }" />
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere tipo de cuenta"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Calificación" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="M"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbCalificacion" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) { OnTipoCalificacionChanged(s); }" />
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere calificación"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Cuentas contables" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>

                                                        <dx:ASPxDropDownEdit ClientInstanceName="CbCuetnasContablesP" ID="CbCuetnasContablesP" runat="server"
                                                            AutoPostBack="false">
                                                            <DropDownApplyButton Visibility="Always"></DropDownApplyButton>
                                                            <SettingsAdaptivity Mode="OnWindowInnerWidth" ModalDropDownCaption="Cuenta" />
                                                            <DropDownWindowTemplate>

                                                                <dx:ASPxCallbackPanel ID="udPCargueCuentas" ClientInstanceName="udPCargueCuentas" runat="server" OnCallback="Cuentas_Callback" ClientSideEvents-EndCallback="OnEndCallback" Style="width: 100%">
                                                                    <PanelCollection>
                                                                        <dx:PanelContent runat="server">

                                                                            <dx:ASPxListBox ID="CbCuetnasContables" runat="server"
                                                                                SelectionMode="CheckColumn" Caption=""
                                                                                RepeatColumns="1"
                                                                                TextField="cuentaNombreCuenta"
                                                                                ValueField="id"
                                                                                ValueType="System.Int32"
                                                                                Border-BorderStyle="None"
                                                                                Width="100%"
                                                                                Height="400"
                                                                                ClientInstanceName="CbCuetnasContables">
                                                                                <FilteringSettings ShowSearchUI="true" EnableAnimation="true" EditorNullText="Buscar" />
                                                                                <CaptionSettings Position="Top" />
                                                                                <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datos" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere al menos una actividad economica"></RequiredField>
                                                                                </ValidationSettings>
                                                                                <ClientSideEvents SelectedIndexChanged="updateText" Init="updateText" />

                                                                            </dx:ASPxListBox>
                                                                        </dx:PanelContent>
                                                                    </PanelCollection>

                                                                </dx:ASPxCallbackPanel>
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
                            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" CssClass="btnfeatures btnBordesRedondos tipoLetra" ValidationGroup="datos" OnClick="btnGuardar_Click" AutoPostBack="false" />
                            <dx:ASPxButton ID="bntCerrar" runat="server" Text="Cerrar" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e) { pcInterfazCuenta.Hide(); }" />
                            </dx:ASPxButton>
                        </div>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ContentStyle>
                    <Paddings PaddingBottom="5px" />
                </ContentStyle>
            </dx:ASPxPopupControl>

            <!-- Modal para la consulta de actividades-->
            <dx:ASPxPopupControl ID="ppCuentasAsociadas" runat="server" Width="1000" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ppActividadesAsociadas"
                HeaderText="Cuentas" AllowDragging="false" PopupAnimationType="Fade" AutoUpdatePosition="true">
                <ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('entryGroup'); }" />
                <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MinHeight="50%" MinWidth="50%" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <dx:ASPxPanel ID="ASPxPanel3" runat="server" DefaultButton="btOK">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>

                        <dx:ASPxListBox ID="CbCuetnasContablesAsoc" runat="server"
                            SelectionMode="CheckColumn" Caption=""
                            RepeatColumns="1"
                            TextField="cuentaNombreCuenta"
                            ValueField="id"
                            ValueType="System.Int32"
                            Border-BorderStyle="None"
                            Width="100%"
                            Height="400"
                            ReadOnly ="true">
                            <FilteringSettings ShowSearchUI="true" EnableAnimation="true" EditorNullText="Buscar" />                            

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
