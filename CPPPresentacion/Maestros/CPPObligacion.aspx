<%@ Page Title="" Language="C#" MasterPageFile="~/SSOMaster.Master" AutoEventWireup="true" CodeBehind="CPPObligacion.aspx.cs" Inherits="CPPPresentacion.Maestros.CPPObligacion" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphCentro" runat="server" ClientIDMode="Static">
    <script src="../Scripts/Js/Maestros/JsCPPObligacion.js"></script>
    <div class="container">
        <div class="tituloPagina">
            <h1>Obligaciones</h1>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="udp">
        <ContentTemplate>
            <!-- GRID DE LA INFORMACION-->
            <div class="table-responsive">
                <dx:ASPxGridView ID="gvObligaciones" runat="server" AutoGenerateColumns="False" KeyFieldName="id;idTipoGarantia" Width="100%" OnPageIndexChanged="gvObligaciones_PageIndexChanged"
                    OnBeforeColumnSortingGrouping="gvObligaciones_BeforeColumnSortingGrouping" OnPageSizeChanged="gvObligaciones_PageSizeChanged" OnRowCommand="gvObligaciones_RowCommand"
                    OnLoad="Grid_Load" EditFormLayoutProperties-SettingsAdaptivity-AdaptivityMode="SingleColumnWindowLimit" SettingsPager-EnableAdaptivity="true" SettingsPager-PageSize="5"
                    OnHtmlRowCreated="gvObligaciones_HtmlRowCreated">
                    <Toolbars>
                        <dx:GridViewToolbar EnableAdaptivity="true">
                            <Items>
                                <dx:GridViewToolbarItem>
                                    <Template>
                                        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="False" OnClick="btnNuevo_Click">
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
                                <dx:ASPxButton ID="btEditar" Image-IconID="actions_edit_16x16devav" ToolTip="Editar" runat="server" CommandName="Editar" CssClass="btnGrid">
                                </dx:ASPxButton>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos" VerticalAlign="Middle" />
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataColumn Caption="Programa" FieldName="programa" Width="130" SortOrder="Ascending" UnboundType="String">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Plan Pago" FieldName="planPago" Width="130" SortOrder="Ascending" UnboundType="String">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Plan Pago Unico" FieldName="planPagoUnico" Width="160" SortOrder="Ascending" UnboundType="String">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Convenio" FieldName="convenio" Width="120" SortOrder="Ascending" UnboundType="String">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Beneficiario" FieldName="beneficiario" Width="280" SortOrder="Ascending" UnboundType="String">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Banco Recaudador" FieldName="bancoRecaudador" Width="180" SortOrder="Ascending" UnboundType="String">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Situacion Juridica" FieldName="situacionJuridica" Width="180" SortOrder="Ascending" UnboundType="String">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Tipo garantia" FieldName="tipoGarantia" Width="180" SortOrder="Ascending" UnboundType="String">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataTextColumn Caption="Garantia" Width="80">
                            <DataItemTemplate>
                                <dx:ASPxButton ID="btnGarantias" Image-IconID="find_find_16x16gray" ToolTip="Garantias" runat="server" CommandName="VerGarantias" CssClass="btnGrid"/>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos" VerticalAlign="Middle" />
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

            <!-- Modal para la creacion y ediccion de la obligacion-->
            <dx:ASPxPopupControl ID="pcObligacion" runat="server" Width="1000" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcObligacion"
                HeaderText="Obligación" AllowDragging="false" PopupAnimationType="Fade" AutoUpdatePosition="true">
                <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MinHeight="50%" MinWidth="80%" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btOK">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>
                        <div id="contentDiv">
                            <asp:HiddenField ID="hdObligacion" runat="server" Value="" />
                            <dx:ASPxFormLayout runat="server" ID="formLayout" Width="100%" ClientInstanceName="FormLayout">
                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="500" />
                                <Items>
                                    <dx:LayoutGroup Width="100%" ColumnCount="2" GroupBoxDecoration="HeadingLine" ShowCaption="False">
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
                                            <dx:LayoutItem Caption="Identificador de la obligacion" VerticalAlign="Middle" ColumnSpan="2">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="2" RowSpan="1" BreakpointName="M"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtIdObligacion" MaxLength="255" runat="server" Text="" />
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Programa" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbPrograma" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="nombre" EnableSynchronization="False">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) { OnProgramaChange(s); }" />
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere el programa"></RequiredField>
                                                            </ValidationSettings>
                                                            <SettingsAdaptivity Mode="OnWindowInnerWidth" ModalDropDownCaption="Programa" />
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Plan pago" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" ClientInstanceName="cbp" runat="server">
                                                            <PanelCollection>
                                                                <dx:PanelContent runat="server">
                                                                    <dx:ASPxComboBox ID="cbPlanPago" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="idPlanPago" TextField="planPago" OnCallback="cbPlanPago_Callback" Style="width: 100%" ClientInstanceName="cbPlanPago">
                                                                        <ClearButton DisplayMode="Always"></ClearButton>
                                                                        <ClientSideEvents EndCallback="OnEndCallback" SelectedIndexChanged="function(s, e) { OnPlanChange(s); }" />
                                                                        <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                            <RequiredField IsRequired="True" ErrorText="Se requiere el plan de pagos"></RequiredField>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxComboBox>
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxCallbackPanel>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Convenio" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxCallbackPanel ID="ASPxCallbackPanel2" ClientInstanceName="cbpC" runat="server" OnCallback="txtConvenio_Callback" ClientSideEvents-EndCallback="OnEndCallbackPlan" Style="width: 100%">
                                                            <PanelCollection>
                                                                <dx:PanelContent runat="server">
                                                                    <dx:ASPxButtonEdit ID="txtConvenio" MaxLength="255" runat="server" Text="" Style="width: 100%" ClientInstanceName="txtConvenio" ClientEnabled="false">
                                                                        <ClearButton DisplayMode="Always"></ClearButton>
                                                                        <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                            <RequiredField IsRequired="True" ErrorText="Se requiere convenio"></RequiredField>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxButtonEdit>
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxCallbackPanel>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Intermediario Financiero" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbIntermediarioFinanciero" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="nombreEntidad">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ClientSideEvents EndCallback="OnEndCallbackIntermediario" SelectedIndexChanged="function(s, e) { OnChangeIntermediario(s); }" />
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere intermediario financiero"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Nit intermediario" VerticalAlign="Middle" ColumnSpan="1">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxCallbackPanel ID="ASPxCallbackPanel3" ClientInstanceName="cbpIntermediario" runat="server" OnCallback="cbIntermediario_Callback" ClientSideEvents-EndCallback="OnEndCallbackIntermediario" Style="width: 100%">
                                                            <PanelCollection>
                                                                <dx:PanelContent runat="server">
                                                                    <dx:ASPxButtonEdit ID="txtNitIntermediario" MaxLength="255" runat="server" Text="" Enabled="false" Style="width: 100%" />
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxCallbackPanel>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Nit Deudor" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbBeneficiario" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="identificador" TextField="identificacion">
                                                            <ClientSideEvents EndCallback="OnEndCallbackBeneficiario" SelectedIndexChanged="function(s, e) { OnChangeBeneficiario(s); }" />
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere nit del deudor"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Nombre deudor" VerticalAlign="Middle" ColumnSpan="1">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxCallbackPanel ID="ASPxCallbackPanel4" ClientInstanceName="cbpBeneficiario" runat="server" OnCallback="cbBeneficiario_Callback" ClientSideEvents-EndCallback="OnEndCallbackBeneficiario" Style="width: 100%">
                                                            <PanelCollection>
                                                                <dx:PanelContent runat="server">
                                                                    <dx:ASPxButtonEdit ID="txtNombreDeudor" MaxLength="255" runat="server" Text="" Enabled="false" Style="width: 100%" />
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxCallbackPanel>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Situacion juridica" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbSituacionJuridica" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere situacion juridica"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Codigo CIIU" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbCodigoCIIU" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere situacion juridica"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="No. Operación intermediario" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtOperacionIntermediario" MaxLength="8" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere No. Operación intermediario"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Destino" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbDestino" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="actividad">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere Destino"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Base de la compra" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtBaseCompra" MaxLength="13" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <MaskSettings Mask="<0..9999999999g>.<00..99>" IncludeLiterals="DecimalSymbol" />
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere la base de compra"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="% Compra" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtPorcentajeCompra" MaxLength="6" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <MaskSettings Mask="<0..100g>.<00..99>" IncludeLiterals="DecimalSymbol" />
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere el porcentaje de la compra"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Valor pagado por finagro" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtValorPagagoFinagro" MaxLength="13" runat="server" Text="">
                                                            <MaskSettings Mask="<0..9999999999g>.<00..99>" IncludeLiterals="DecimalSymbol" />
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere el valor pagago por Finagro"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Fecha de compra" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxDateEdit ID="txtFechaCompra" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <CalendarProperties>
                                                                <FastNavProperties DisplayMode="Inline" />
                                                            </CalendarProperties>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datos" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere fecha de compra"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Aporte en dinero" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtAporteDinero" MaxLength="13" runat="server" Text="">
                                                            <MaskSettings Mask="<0..9999999999g>.<00..99>" IncludeLiterals="DecimalSymbol" />
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere el aporte en dinero"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Aporte financiado" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtAporteFinanciado" MaxLength="13" runat="server" Text="">
                                                            <MaskSettings Mask="<0..9999999999g>.<00..99>" IncludeLiterals="DecimalSymbol" />
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere el aporte financiado"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Valor cartera inicial" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtCarteraInicial" MaxLength="13" runat="server" Text="">
                                                            <MaskSettings Mask="<0..9999999999g>.<00..99>" IncludeLiterals="DecimalSymbol" />
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere el valor de cartera inicial"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Actividad Agropecuaria" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbActividadAgropecuaria" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="actividad">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere la actividad Agropecuaria"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Depto Compra" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbDeptoCompra" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="depratamento">
                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) { OnDeptoCompraChanged(s); }" />
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere departamento de compra"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Municipio Compra" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxCallbackPanel ID="ASPxCallbackPanel5" ClientInstanceName="cbp" runat="server">
                                                            <PanelCollection>
                                                                <dx:PanelContent runat="server">
                                                                    <dx:ASPxComboBox ID="cbMunciCompra" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="municipio" OnCallback="cbMunciCompra_Callback" Style="width: 100%">
                                                                        <ClientSideEvents EndCallback="OnEndCallbackDeptoCompra" />
                                                                        <ClearButton DisplayMode="Always"></ClearButton>
                                                                        <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                            <RequiredField IsRequired="True" ErrorText="Se requiere municipio de compra"></RequiredField>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxComboBox>
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxCallbackPanel>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Depto Origen" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbDeptoOrigen" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="depratamento">
                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) { OnDeptoOrigenChanged(s); }" />
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere departamento de origen"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Municipio Origen" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxCallbackPanel ID="ASPxCallbackPanel6" ClientInstanceName="cbp" runat="server">
                                                            <PanelCollection>
                                                                <dx:PanelContent runat="server">
                                                                    <dx:ASPxComboBox ID="cbMunciOrigen" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="municipio" OnCallback="cbMunciOrigen_Callback" Style="width: 100%">
                                                                        <ClientSideEvents EndCallback="OnEndCallbackDeptoOrigen" />
                                                                        <ClearButton DisplayMode="Always"></ClearButton>
                                                                        <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                            <RequiredField IsRequired="True" ErrorText="Se requiere municipio de origen"></RequiredField>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxComboBox>
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxCallbackPanel>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Depto inversión" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbDeptoInv" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="depratamento">
                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) { OnDeptoInvChanged(s); }" />
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere departamento de inversion"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Municipio inversión" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxCallbackPanel ID="ASPxCallbackPanel7" ClientInstanceName="cbp" runat="server">
                                                            <PanelCollection>
                                                                <dx:PanelContent runat="server">
                                                                    <dx:ASPxComboBox ID="cbMunciInv" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="municipio" OnCallback="cbMunciInv_Callback" Style="width: 100%">
                                                                        <ClientSideEvents EndCallback="OnEndCallbackDeptoInv" />
                                                                        <ClearButton DisplayMode="Always"></ClearButton>
                                                                        <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                            <RequiredField IsRequired="True" ErrorText="Se requiere municipio de inversion"></RequiredField>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxComboBox>
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxCallbackPanel>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Tipo Garantia" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbTipoGarantia" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" ClientInstanceName="cbTipoGarantia">
                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {tipoGarantiaChange(s);}"/>
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem ShowCaption="False">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S" />
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButton ID="btnAsociarGarantia" ClientInstanceName="btnAsociarGarantia" runat="server" Text="Asociar Garantia" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="false" CausesValidation="false" ClientEnabled="false">
                                                            <ClientSideEvents Click="onClickGarantia"/>
                                                        </dx:ASPxButton>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                            </dx:ASPxFormLayout>
                            <dx:ASPxButton ID="btnAjustarPlan" runat="server" Text="Ajustar plan de pagos unico" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="false" CausesValidation="false" OnClick="btnAjustarPlan_Click" ClientInstanceName="btnAjustarPlan" ClientEnabled="false" />
                        </div>
                        <div class="modal-footer">
                            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" CssClass="btnfeatures btnBordesRedondos tipoLetra" ValidationGroup="datos" OnClick="btnGuardar_Click" />
                            <dx:ASPxButton ID="bntCerrar" runat="server" Text="Cerrar" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e) { pcObligacion.Hide(); }" />
                            </dx:ASPxButton>
                        </div>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ContentStyle>
                    <Paddings PaddingBottom="5px" />
                </ContentStyle>
            </dx:ASPxPopupControl>

            <!-- Modal para la creacion y ediccion del plan de pago -->
            <dx:ASPxPopupControl ID="pcPlanPagos" runat="server" Width="1000" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcPlanPagos"
                HeaderText="Plan de Pago" AllowDragging="false" PopupAnimationType="Fade" AutoUpdatePosition="true">
                <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MinHeight="50%" MinWidth="70%" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <dx:ASPxPanel ID="ASPxPanel2" runat="server" DefaultButton="btOK">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>
                        <div id="contentDiv2">
                            <asp:HiddenField ID="hdIdPlanPago" runat="server" Value="" />
                            <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout1" Width="100%" ClientInstanceName="FormLayout">
                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="500" />
                                <Items>
                                    <dx:LayoutGroup Width="100%" ColumnCount="2" GroupBoxDecoration="HeadingLine" ShowCaption="False">
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

                                            <dx:LayoutItem Caption="Intermediario financiero" VerticalAlign="Middle" RequiredMarkDisplayMode="Hidden" ColumnSpan="2">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbIntermediario" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="nombreEntidadExtendido">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="datosPlan">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere intermediario"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Periodo de gracia" RequiredMarkDisplayMode="Hidden">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txPeridogracia" MaxLength="3" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datosPlan">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere periodo de gracia"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Plazo total obligación" RequiredMarkDisplayMode="Hidden">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtPalzoObligacion" MaxLength="3" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datosPlan" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere plazo total obligacion"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Periodo muerto" RequiredMarkDisplayMode="Hidden">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtPeriodoMuerto" MaxLength="2" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datosPlan" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere periodo muerto"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="N° cuotas plan de pagos" RequiredMarkDisplayMode="Hidden">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtCuotasPlanPagos" MaxLength="4" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datosPlan" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere cuotas plan de pagos"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Descuento por amortizar" RequiredMarkDisplayMode="Hidden">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtDescuentoAmortizacion" runat="server" Text="" MaxLength="13" ValidationSettings-Display="None">
                                                            <MaskSettings Mask="<0..9999999999g>.<00..99>" IncludeLiterals="DecimalSymbol" />
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Impuesto de timbre" RequiredMarkDisplayMode="Hidden">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtImpuestoTimbre" MaxLength="5" runat="server" Text="" ValidationSettings-Display="None">
                                                            <MaskSettings Mask="<0..99g>.<00..99>" IncludeLiterals="DecimalSymbol" />
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Descuento amortizado" RequiredMarkDisplayMode="Hidden">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtDescuentoAmortizado" MaxLength="13" runat="server" Text="" ValidationSettings-Display="None">
                                                            <MaskSettings Mask="<0..9999999999g>.<00..99>" IncludeLiterals="DecimalSymbol" />
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Periodicidad capital" RequiredMarkDisplayMode="Hidden">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtPeriodicidadCapital" MaxLength="4" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datosPlan" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere periodicidad capital"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Tipo plan de pagos" RequiredMarkDisplayMode="Hidden">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbTipoPlanPagos" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="tipoPlanPagos" CssClass="tipoLetra">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datosPlan" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere tipo plan de pagos"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Periodicidad intereses corrientes" RequiredMarkDisplayMode="Hidden">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbPeriocidadIntereses" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" CssClass="tipoLetra">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datosPlan" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere tipo plan de pagos"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Modalidad capital" RequiredMarkDisplayMode="Hidden">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbModalidadcapital" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="modalidad" CssClass="tipoLetra">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datosPlan" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere modalidad capital"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Tasa intereses corrientes" RequiredMarkDisplayMode="Hidden">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtTasaInteresCorriente" MaxLength="5" runat="server" Text="" ValidationSettings-Display="None">
                                                            <MaskSettings Mask="<0..99>.<0..99>" IncludeLiterals="DecimalSymbol" />
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datosPlan" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere tasa de intereses corrientes"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Fecha de pago" RequiredMarkDisplayMode="Hidden">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxDateEdit ID="txtFecha" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <CalendarProperties>
                                                                <FastNavProperties DisplayMode="Inline" />
                                                            </CalendarProperties>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datosPlan" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere fecha de pago"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Puntos contigentes intereses corrientes" RequiredMarkDisplayMode="Hidden">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtPuntosContingentes" MaxLength="4" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Tasa de interes moratorio" RequiredMarkDisplayMode="Hidden">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtTasaMoratoria" MaxLength="5" runat="server" Text="" ValidationSettings-Display="None">
                                                            <MaskSettings Mask="<0..99g>.<00..99>" IncludeLiterals="DecimalSymbol" />
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datosPlan" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere tasa de interes moratoria"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Descripcion" RequiredMarkDisplayMode="Hidden" ColumnSpan="2">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtDescripcion" MaxLength="255" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datosPlan" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere descripción"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                            </dx:ASPxFormLayout>
                        </div>
                        <div class="modal-footer">
                            <dx:ASPxButton ID="btnGuardarPlan" runat="server" Text="Guardar" CssClass="btnfeatures btnBordesRedondos tipoLetra" ValidationGroup="datosPlan" OnClick="btnGuardarPlan_Click" ValidateRequestMode="Enabled" />
                            <dx:ASPxButton ID="btnCancelar" runat="server" Text="Cerrar" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="false" CausesValidation="false">
                                <ClientSideEvents Click="function(s, e) { pcPlanPagos.Hide(); }" />
                            </dx:ASPxButton>
                        </div>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ContentStyle>
                    <Paddings PaddingBottom="5px" />
                </ContentStyle>
            </dx:ASPxPopupControl>

            <!-- Modal para la asociacion y ediccion del tipo de garantia Idonea-->
            <dx:ASPxPopupControl ID="pcIdonea" runat="server" Width="1000" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcIdonea"
                HeaderText="Idonea" AllowDragging="false" PopupAnimationType="Fade" AutoUpdatePosition="true">
                <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MinHeight="30%" MinWidth="70%" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <div class="clientContainer">
                            <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout2" Width="100%" ClientInstanceName="FormLayout">
                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="500" />
                                <Items>
                                    <dx:LayoutGroup Width="100%" ColumnCount="1" GroupBoxDecoration="HeadingLine" ShowCaption="False">
                                        <GroupBoxStyle>
                                            <Caption Font-Bold="true" Font-Size="16" />
                                        </GroupBoxStyle>
                                        <GridSettings StretchLastItem="true" WrapCaptionAtWidth="660">
                                            <Breakpoints>
                                                <dx:LayoutBreakpoint MaxWidth="300" ColumnCount="1" Name="S" />
                                                <dx:LayoutBreakpoint MaxWidth="800" ColumnCount="2" Name="X" />
                                            </Breakpoints>
                                        </GridSettings>
                                        <Items>
                                            <dx:LayoutItem Caption="Tipo inmueble" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbTipoInmueble" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="idonea">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere un tipo de inmueble"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Matricula moviliaria" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtMatriculaMoviliaria" MaxLength="255" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="idonea" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere la matricula"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Dirección" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtDireccion" MaxLength="255" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="idonea" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere la direccion del inmueble"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Valor inmueble" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtValorInmueble" MaxLength="15" runat="server" Text="">
                                                            <MaskSettings Mask="<0..999999999999g>.<00..99>" IncludeLiterals="DecimalSymbol" />
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="idonea" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere el valor del inmueble"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden" ShowCaption="False">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButton ID="btnAceptar" runat="server" Text="Aceptar" ValidationGroup="idonea" AutoPostBack="false" CssClass="btnfeatures btnBordesRedondos tipoLetra" OnClick="btnAceptar_Click"></dx:ASPxButton>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                            </dx:ASPxFormLayout>
                            <dx:ASPxGridView ID="gvGarantiaIdonea" runat="server" AutoGenerateColumns="False" KeyFieldName="matriculaInmobiliaria" Width="100%" OnRowCommand="gvGarantiaIdonea_RowCommand">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Acción" Width="125">
                                        <DataItemTemplate>
                                            <dx:ASPxButton ID="btEliminar" runat="server" Text="Eliminar" CssClass="btnfeatures btnBordesRedondos tipoLetra" CommandName="Eliminar"></dx:ASPxButton>
                                        </DataItemTemplate>
                                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                        <CellStyle CssClass="tablasDatos" VerticalAlign="Middle" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataColumn Caption="Tipo inmueble" FieldName="tipoInmueble" Width="180" SortOrder="Ascending" UnboundType="Integer">                                    
                                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                        <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Matricula" FieldName="matriculaInmobiliaria" Width="200" SortOrder="Ascending" UnboundType="String">
                                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                        <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Direccion" FieldName="direccion" Width="300" SortOrder="Ascending" UnboundType="String">
                                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                        <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Valor" FieldName="valorInmueble" Width="300" SortOrder="Ascending" UnboundType="Decimal">
                                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                        <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                                    </dx:GridViewDataColumn>
                                </Columns>
                                <Settings VerticalScrollableHeight="360" />
                                <Settings VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" />
                                <Settings ShowFilterRow="true" />
                                <SettingsBehavior AllowSelectByRowClick="true" />
                            </dx:ASPxGridView>
                        </div>
                        <div class="modal-footer">
                            <dx:ASPxButton ID="btnGuardaIdonea" runat="server" Text="Guardar" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="false" OnClick="btnGuardaIdonea_Click" />
                            <dx:ASPxButton ID="btnCerrarIdonea" runat="server" Text="Cerrar" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="false" OnClick="btnCerrarIdonea_Click"/>
                        </div>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ContentStyle>
                    <Paddings PaddingBottom="5px" />
                </ContentStyle>
            </dx:ASPxPopupControl>

            <!-- Modal para la asociacion y ediccion del tipo de garantia Codeudor-->
            <dx:ASPxPopupControl ID="pcCodeudor" runat="server" Width="1000" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcCodeudor"
                HeaderText="Codeudor" AllowDragging="false" PopupAnimationType="Fade" AutoUpdatePosition="true">
                <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MinHeight="30%" MinWidth="50%" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <div class="clientContainer">
                            <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout3" Width="100%" ClientInstanceName="FormLayout">
                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="500" />
                                <Items>
                                    <dx:LayoutGroup Width="100%" ColumnCount="1" GroupBoxDecoration="HeadingLine" ShowCaption="False">
                                        <GroupBoxStyle>
                                            <Caption Font-Bold="true" Font-Size="16" />
                                        </GroupBoxStyle>
                                        <GridSettings StretchLastItem="true" WrapCaptionAtWidth="660">
                                            <Breakpoints>
                                                <dx:LayoutBreakpoint MaxWidth="300" ColumnCount="1" Name="S" />
                                                <dx:LayoutBreakpoint MaxWidth="800" ColumnCount="2" Name="X" />
                                            </Breakpoints>
                                        </GridSettings>
                                        <Items>
                                            <dx:LayoutItem Caption="Codeudor" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbCodeudor" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="cppCo_Id" TextField="CedulaNombre">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="idonea">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere un tipo de inmueble"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden" ShowCaption="False">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButton ID="btnAceptarCodeudor" runat="server" Text="Aceptar" ValidationGroup="idonea" AutoPostBack="false" CssClass="btnfeatures btnBordesRedondos tipoLetra" OnClick="btnAceptarCodeudor_Click"></dx:ASPxButton>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                            </dx:ASPxFormLayout>
                            <dx:ASPxGridView ID="gvCodeudor" runat="server" AutoGenerateColumns="False" KeyFieldName="cppCo_Id" Width="100%" OnRowCommand="gvCodeudor_RowCommand">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Acción" Width="125">
                                        <DataItemTemplate>
                                            <dx:ASPxButton ID="btEliminar" runat="server" Text="Eliminar" CssClass="btnfeatures btnBordesRedondos tipoLetra" CommandName="Eliminar"></dx:ASPxButton>
                                        </DataItemTemplate>
                                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                        <CellStyle CssClass="tablasDatos" VerticalAlign="Middle" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataColumn Caption="Codeudor" FieldName="CedulaNombre" Width="180" SortOrder="Ascending" UnboundType="Integer">                                    
                                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                        <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                                    </dx:GridViewDataColumn>
                                    
                                </Columns>
                                <Settings VerticalScrollableHeight="360" />
                                <Settings VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" />
                                <Settings ShowFilterRow="true" />
                                <SettingsBehavior AllowSelectByRowClick="true" />
                            </dx:ASPxGridView>
                        </div>
                        <div class="modal-footer">
                            <dx:ASPxButton ID="btnGuardaCodeudor" runat="server" Text="Guardar" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="false" OnClick="btnGuardaCodeudor_Click" />
                            <dx:ASPxButton ID="btnCerrarCodeudor" runat="server" Text="Cerrar" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="false" OnClick="btnCerrarCodeudor_Click"/>
                        </div>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ContentStyle>
                    <Paddings PaddingBottom="5px" />
                </ContentStyle>
            </dx:ASPxPopupControl>

            <!-- Modal para la visualizacion de las garantias idoneas-->
            <dx:ASPxPopupControl ID="pcGarantiasIdoneasVisualizacion" runat="server" Width="1000" Modal="True" CloseOnEscape="true"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcGarantiasIdoneasVisualizacion"
                HeaderText="Idoneas" AllowDragging="false" PopupAnimationType="Fade" AutoUpdatePosition="true">
                <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MinHeight="30%" MinWidth="70%" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <div class="clientContainer">
                            <dx:ASPxGridView ID="gvGarantiasIdoneasAsociadas" runat="server" AutoGenerateColumns="False" KeyFieldName="matriculaInmobiliaria" Width="100%">
                                <Columns>
                                    <dx:GridViewDataColumn Caption="Tipo inmueble" FieldName="tipoInmueble" Width="180" SortOrder="Ascending" UnboundType="Integer">                                    
                                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                        <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Matricula" FieldName="matriculaInmobiliaria" Width="200" SortOrder="Ascending" UnboundType="String">
                                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                        <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Direccion" FieldName="direccion" Width="300" SortOrder="Ascending" UnboundType="String">
                                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                        <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                                    </dx:GridViewDataColumn>

                                    <dx:GridViewDataColumn Caption="Valor Inmueble" FieldName="valorInmueble" Width="300" SortOrder="Ascending" UnboundType="String">
                                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                        <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                                    </dx:GridViewDataColumn>
                                </Columns>
                                <Settings VerticalScrollableHeight="360" />
                                <Settings VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" />
                                <SettingsBehavior AllowSelectByRowClick="true" />
                            </dx:ASPxGridView>
                        </div>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ContentStyle>
                    <Paddings PaddingBottom="5px" />
                </ContentStyle>
            </dx:ASPxPopupControl>

            <!-- Modal para la visualizacion de las garantias codeudores-->
            <dx:ASPxPopupControl ID="pcGarantiasCodeudoresVisualizacion" runat="server" Width="1000" Modal="True" CloseOnEscape="true"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcGarantiasCodeudoresVisualizacion"
                HeaderText="Codeudores" AllowDragging="false" PopupAnimationType="Fade" AutoUpdatePosition="true">
                <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MinHeight="30%" MinWidth="50%" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <div class="clientContainer">
                            <dx:ASPxGridView ID="gvGarantiasCodeudorAsociadas" runat="server" AutoGenerateColumns="False" KeyFieldName="cppCo_Id" Width="100%">
                               <Columns>
                                    <dx:GridViewDataColumn Caption="Codeudor" FieldName="CedulaNombre" Width="400" SortOrder="Ascending" UnboundType="Integer">                                    
                                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                        <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                                    </dx:GridViewDataColumn>                                    
                                </Columns>
                                <Settings VerticalScrollableHeight="360" />
                                <Settings VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" />
                                <SettingsBehavior AllowSelectByRowClick="true" />
                            </dx:ASPxGridView>
                        </div>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ContentStyle>
                    <Paddings PaddingBottom="5px" />
                </ContentStyle>
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
