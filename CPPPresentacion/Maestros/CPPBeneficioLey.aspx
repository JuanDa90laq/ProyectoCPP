<%@ Page Title="" Language="C#" MasterPageFile="~/SSOMaster.Master" AutoEventWireup="true" CodeBehind="CPPBeneficioLey.aspx.cs" Inherits="CPPPresentacion.Maestros.CCPBeneficioLey" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphCentro" runat="server" ClientIDMode="Static">
    <script src="../Scripts/Js/Maestros/JsCPPBeneficioLey.js"></script>
    <div class="container">
        <div class="tituloPagina">
            <h1>Beneficio de ley</h1>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="udp">
        <ContentTemplate>
            <!-- GRID DE LA INFORMACION-->
            <dx:ASPxGridView ID="gvBeneficiosLey" runat="server" AutoGenerateColumns="False" KeyFieldName="id" Width="100%" OnPageIndexChanged="gvBeneficiosLey_PageIndexChanged"
                OnBeforeColumnSortingGrouping="gvBeneficiosLey_BeforeColumnSortingGrouping" OnPageSizeChanged="gvBeneficiosLey_PageSizeChanged" OnRowCommand="gvBeneficiosLey_RowCommand"
                OnLoad="Grid_Load" EditFormLayoutProperties-SettingsAdaptivity-AdaptivityMode="SingleColumnWindowLimit" SettingsPager-EnableAdaptivity="true" SettingsPager-PageSize="5">
                <Toolbars>
                    <dx:GridViewToolbar EnableAdaptivity="true">
                        <Items>
                            <dx:GridViewToolbarItem>
                                <Template>
                                    <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="False" OnClick="btnNuevo_Click" />
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
                            <dx:ASPxButton ID="btEditar" Image-IconID="actions_edit_16x16devav" ToolTip="Editar" runat="server" CommandName="Editar" CssClass="btnGrid" />
                        </DataItemTemplate>
                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                        <CellStyle CssClass="tablasDatos" VerticalAlign="Middle" />
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataColumn Caption="Id beneficio" FieldName="id" Width="130" SortOrder="Ascending" UnboundType="Integer" CellStyle-HorizontalAlign="Center">
                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                        <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn Caption="Nombre beneficio" FieldName="nombreBeneficio" Width="166" SortOrder="Ascending" UnboundType="String">
                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                        <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn Caption="Programa" FieldName="programa" Width="150" SortOrder="Ascending" UnboundType="String">
                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                        <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn Caption="Departamento" FieldName="departamento" Width="150" SortOrder="Ascending" UnboundType="String">
                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                        <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn Caption="Municipio" FieldName="municipio" Width="150" SortOrder="Ascending" UnboundType="String">
                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                        <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn Caption="Fecha Inicial" FieldName="fechaInicial" Width="130" SortOrder="Ascending" UnboundType="DateTime">
                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                        <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn Caption="Fecha Final" FieldName="fechaFinal" Width="130" SortOrder="Ascending" UnboundType="DateTime">
                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                        <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn Caption="# Pagares" FieldName="cantidadPagares" Width="100" SortOrder="Ascending" UnboundType="String" CellStyle-HorizontalAlign="Center">
                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" VerticalAlign="Middle"/>
                        <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                    </dx:GridViewDataColumn>
                    
                    <dx:GridViewDataColumn Caption="Tope Maximo" FieldName="topeMaximo" Width="150" SortOrder="Ascending" UnboundType="String" CellStyle-HorizontalAlign="Center">
                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                        <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn Caption="Tipo Beneficiado" FieldName="tipoBeneficiado" Width="180" SortOrder="Ascending" UnboundType="String">
                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                        <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn Caption="Actividad Agropecuaria" FieldName="actividadAgropecuaria" Width="210" SortOrder="Ascending" UnboundType="String">
                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                        <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn Caption="Vigencia" FieldName="vigencia" Width="150" SortOrder="Ascending" UnboundType="String" CellStyle-HorizontalAlign="Center">
                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                        <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                    </dx:GridViewDataColumn>

                </Columns>
                <Settings VerticalScrollableHeight="400" />
                <Settings VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" />
                <Settings ShowFilterRow="true" />
                <SettingsBehavior AllowSelectByRowClick="true" />
                <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" />
                <SettingsPager>
                    <PageSizeItemSettings Visible="true" Items="5, 20, 50" ShowAllItem="true" />
                </SettingsPager>
            </dx:ASPxGridView>

            <!-- Modal para la creacion y ediccion del beneficio de ley-->
            <dx:ASPxPopupControl ID="pcBeneficio" runat="server" Width="1000" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcBeneficio"
                HeaderText="Beneficio de ley" AllowDragging="false" PopupAnimationType="Fade" AutoUpdatePosition="true">
                <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MinHeight="40%" MinWidth="90%" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <div id="contentDiv" class="clientContainer">
                            <asp:HiddenField ID="hdIdBeneficio" runat="server" Value="" />
                            <asp:HiddenField ID="hdIdPagare1" runat="server" Value="" />
                            <asp:HiddenField ID="hdIdPagare2" runat="server" Value="" />
                            <dx:ASPxCallbackPanel ID="ASPxCallbackPanelTab" ClientInstanceName="cbpBeneficio" runat="server" OnCallback="cbPagares_Callback" Style="width: 100%">
                                <PanelCollection>
                                    <dx:PanelContent runat="server">
                                        <dx:ASPxFormLayout runat="server" ID="formLayout" Width="100%" ClientInstanceName="FormLayout">
                                            <ClientSideEvents Init="onInit" />
                                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="500" />
                                            <Items>

                                                <dx:TabbedLayoutGroup ActiveTabIndex="0" ClientInstanceName="pageControl">
                                                    <Styles>
                                                        <TabStyle CssClass="myTab" />
                                                    </Styles>
                                                    <Items>
                                                        <dx:LayoutGroup Width="100%" ColumnCount="2" GroupBoxDecoration="HeadingLine" Caption="Cabecera" ShowCaption="False" Name="0_0">
                                                            <GroupBoxStyle>
                                                                <Caption Font-Bold="true" Font-Size="16" />
                                                            </GroupBoxStyle>
                                                            <GridSettings StretchLastItem="true" WrapCaptionAtWidth="660">
                                                                <Breakpoints>
                                                                    <dx:LayoutBreakpoint MaxWidth="800" ColumnCount="1" Name="S" />
                                                                    <dx:LayoutBreakpoint MaxWidth="1200" ColumnCount="2" Name="M" />
                                                                </Breakpoints>
                                                            </GridSettings>
                                                            <Items>
                                                                <dx:LayoutItem Caption="Codigo Beneficio" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxButtonEdit ID="txtCodigoBeneficio" MaxLength="255" runat="server" Text="" ClientEnabled="false" />
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Programa" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbPrograma" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClientSideEvents Validation="onValidation" />
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_0">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere el programa"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Nombre Beneficio" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxButtonEdit ID="txtNombreBeneficio" MaxLength="255" runat="server" Text="" OnCustomJSProperties="ASPxButtonEdit_CustomJSProperties">
                                                                                <ClientSideEvents Validation="onValidation" />
                                                                                <ClearButton DisplayMode="OnHover"></ClearButton>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_0">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere el nombre del beneficio de ley"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxButtonEdit>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Departamento" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbDepartamento" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="depratamento" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) { OnDeptoChanged(s); }" Validation="onValidation" />
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_0">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere el departamento"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Municipio" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxCallbackPanel ID="ASPxCallbackPanel" ClientInstanceName="cbp" runat="server">
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server">
                                                                                        <dx:ASPxComboBox ID="cbMunicipio" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="municipio" OnCallback="cbMunci_Callback" Style="width: 100%" ClientIDMode="Static" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                            <ClientSideEvents EndCallback="OnEndCallbackDepto" Validation="onValidation" />
                                                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_0">
                                                                                                <RequiredField IsRequired="True" ErrorText="Se requiere el municipio"></RequiredField>
                                                                                            </ValidationSettings>
                                                                                        </dx:ASPxComboBox>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Fecha Inicial" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxDateEdit ID="txtFechaInicial" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy" ClientInstanceName="txtFechaInicial" OnCustomJSProperties="ASPxDateEdit_CustomJSProperties">
                                                                                <ClientSideEvents Validation="onValidation" />
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <CalendarProperties>
                                                                                    <FastNavProperties DisplayMode="Inline" />
                                                                                </CalendarProperties>
                                                                                <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="0_0" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere fecha inicial"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxDateEdit>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Fecha Final" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxDateEdit ID="txtFechaFinal" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy" OnCustomJSProperties="ASPxDateEdit_CustomJSProperties">
                                                                                <ClientSideEvents Validation="onValidation" />
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <CalendarProperties>
                                                                                    <FastNavProperties DisplayMode="Inline" />
                                                                                </CalendarProperties>
                                                                                <DateRangeSettings StartDateEditID="txtFechaInicial" />
                                                                                <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="0_0" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere fecha final"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxDateEdit>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Cantidad Pagares" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbPagares" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) { OnPagareChange(s); }" Validation="onValidation" />
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_0">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere la cantidad de pagares"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Monto Maximo (SMVL)" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxButtonEdit ID="txtMontoMaximo" MaxLength="4" runat="server" Text="" OnCustomJSProperties="ASPxButtonEdit_CustomJSProperties">
                                                                                <ClientSideEvents Validation="onValidation" />
                                                                                <ClearButton DisplayMode="OnHover"></ClearButton>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_0">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere el monto maximo"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxButtonEdit>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Tipo de beneficio" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbTipoBeneficio" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClientSideEvents Validation="onValidation" />
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_0">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere el tipo de beneficio"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Actividad Agropecuaria" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbActividadAgropecuaria" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="actividad" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClientSideEvents Validation="onValidation" />
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_0">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere la actividad agropecuaria"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                            </Items>
                                                        </dx:LayoutGroup>
                                                        <dx:LayoutGroup Width="100%" ColumnCount="3" GroupBoxDecoration="HeadingLine" ShowCaption="False" Caption="Pagare 1" Name="0_1">
                                                            <GroupBoxStyle>
                                                                <Caption Font-Bold="true" Font-Size="16" />
                                                            </GroupBoxStyle>
                                                            <GridSettings StretchLastItem="true" WrapCaptionAtWidth="660">
                                                                <Breakpoints>
                                                                    <dx:LayoutBreakpoint MaxWidth="800" ColumnCount="1" Name="S" />
                                                                    <dx:LayoutBreakpoint MaxWidth="1200" ColumnCount="2" Name="M" />
                                                                </Breakpoints>
                                                            </GridSettings>
                                                            <Items>
                                                                <dx:LayoutItem Caption="Tasa de interes" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbTasaInteres" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ClientSideEvents Validation="onValidation" />
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_1">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere la tasa de mora"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Puntos adicionales" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxButtonEdit ID="txtPuntosIPC" MaxLength="6" runat="server" Text="" ValidationSettings-Display="None">
                                                                                <MaskSettings Mask="<0..100g>.<00..99>" IncludeLiterals="DecimalSymbol" />
                                                                                <ClearButton DisplayMode="OnHover"></ClearButton>
                                                                                <ValidationSettings Display="None">
                                                                                </ValidationSettings>
                                                                            </dx:ASPxButtonEdit>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Tasa de mora" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbTasaMora" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ClientSideEvents Validation="onValidation" />
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_1">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere la tasa de mora"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Plazo de la obligación" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbPlazoObligacion" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ClientSideEvents Validation="onValidation" />
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_1">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere el plazo de la obligación"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Cantidad años(Plazo)" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxButtonEdit ID="txtCantidadAnios" MaxLength="2" runat="server" Text="" OnCustomJSProperties="ASPxButtonEdit_CustomJSProperties">
                                                                                <ClearButton DisplayMode="OnHover"></ClearButton>
                                                                                <ClientSideEvents Validation="onValidation" />
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_1">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere la cantidad de años"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxButtonEdit>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="% Distribución base de compra P1" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxButtonEdit ID="txtPorcentajeDistribucionP1" MaxLength="6" runat="server" Text="" OnCustomJSProperties="ASPxButtonEdit_CustomJSProperties">
                                                                                <ClearButton DisplayMode="OnHover"></ClearButton>
                                                                                <ClientSideEvents Validation="onValidation" />
                                                                                <MaskSettings Mask="<0..100g>.<00..99>" IncludeLiterals="DecimalSymbol" />
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_1">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere el porcentaje de distribución"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxButtonEdit>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="% Distribución base de compra P2" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxButtonEdit ID="txtPorcentajeDistribucionP2" MaxLength="6" runat="server" Text="" ValidationSettings-Display="None">
                                                                                <MaskSettings Mask="<0..100g>.<00..99>" IncludeLiterals="DecimalSymbol" />
                                                                                <ClearButton DisplayMode="OnHover"></ClearButton>
                                                                                <ValidationSettings Display="None">
                                                                                </ValidationSettings>
                                                                            </dx:ASPxButtonEdit>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Periocidad de intereses" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbPeriocidadIntereses" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ClientSideEvents Validation="onValidation" />
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_1">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere la periocidad de intereses"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Periodo muerto" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbPeriodoMuerto" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClientSideEvents SelectedIndexChanged="OnPeriodoMuertoChange" />
                                                                                <ClientSideEvents Validation="onValidation" />
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_1">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere el periodo muerto"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Cantidad años" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxCallbackPanel ID="ASPxCallbackPanelCantidadPeriodoM" ClientInstanceName="cbpPeriodoMuerto" runat="server" OnCallback="ASPxCallbackPanelCantidadPeriodoM_Callback" Style="width: 100%">
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server">
                                                                                        <dx:ASPxButtonEdit ID="txtCantidadPeriodoMuerto" MaxLength="2" runat="server" Text="" Style="width: 100%">
                                                                                            <ClearButton DisplayMode="OnHover"></ClearButton>
                                                                                        </dx:ASPxButtonEdit>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Calificación" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbCalificacion" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ClientSideEvents Validation="onValidation" />
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_1">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere la calificacion"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Periodo de gracia" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbPeriodoGracia" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClientSideEvents SelectedIndexChanged="OnPeriodoGraciaChange" Validation="onValidation" />
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_1">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere el periodo de gracia"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Cantidad años" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" ClientInstanceName="cbpPeriodoGracia" runat="server" OnCallback="ASPxCallbackPanelCantidadPeriodoG_Callback" Style="width: 100%">
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server">
                                                                                        <dx:ASPxButtonEdit ID="txtCantidadPeriodoGracia" MaxLength="2" runat="server" Text="" Style="width: 100%">
                                                                                            <ClearButton DisplayMode="OnHover"></ClearButton>
                                                                                        </dx:ASPxButtonEdit>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:EmptyLayoutItem ColumnSpan="1" />
                                                                <dx:LayoutItem Caption="Beneficio Capital" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbBeneficioCapital" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="cppAb_Id" TextField="cppAb_Descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ClientSideEvents Validation="onValidation" />
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_1">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere el beneficio capital"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:EmptyLayoutItem ColumnSpan="2" />
                                                                <dx:LayoutItem Caption="Beneficio Seguro De Vida" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbBeneficioSeguroVida" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClientSideEvents SelectedIndexChanged="OnSeguroVidaChange" Validation="onValidation" />
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_1">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere el beneficio seguro de vida"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Fecha inicio" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxCallbackPanel ID="cbpFechaInicioSeguroV" ClientInstanceName="cbpFechaInicioSeguroV" runat="server" OnCallback="cbpFechaInicioSeguroV_Callback" Style="width: 100%" ClientSideEvents-EndCallback="OnEndCallbackSeguroVidaChange">
                                                                                <ClientSideEvents EndCallback="OnEndCallbackSeguroVidaChange" />
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server">
                                                                                        <dx:ASPxDateEdit ID="txtFechaInicioSeguroVida" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy" ClientInstanceName="txtFechaInicioSeguroVida" Style="width: 100%" >
                                                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                                                            <CalendarProperties>
                                                                                                <FastNavProperties DisplayMode="Inline" />
                                                                                            </CalendarProperties>
                                                                                        </dx:ASPxDateEdit>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Fecha fin" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxCallbackPanel ID="cbpFechaFinSeguroV" ClientInstanceName="cbpFechaFinSeguroV" runat="server" OnCallback="cbpFechaFinSeguroV_Callback" Style="width: 100%" >
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server">
                                                                                        <dx:ASPxDateEdit ID="txtFechaFinSeguroVida" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy" ClientInstanceName="txtFechaFinSeguroVida" Style="width: 100%">
                                                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                                                            <CalendarProperties>
                                                                                                <FastNavProperties DisplayMode="Inline" />
                                                                                            </CalendarProperties>
                                                                                            <DateRangeSettings StartDateEditID="txtFechaInicioSeguroVida" />
                                                                                        </dx:ASPxDateEdit>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>                                                                
                                                                <dx:LayoutItem Caption="Beneficio Interes" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbBeneficioInteres" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClientSideEvents SelectedIndexChanged="OnBeneficioInteresChange" Validation="onValidation" />
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_1">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere el beneficio seguro de vida"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Fecha inico" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxCallbackPanel ID="cbpFechaInicioBeneficioI" ClientInstanceName="cbpFechaInicioBeneficioI" runat="server" OnCallback="cbpFechaInicioBeneficioI_Callback" Style="width: 100%" ClientSideEvents-EndCallback="OnEndCallbackBeneficioInteresChange">
                                                                                <ClientSideEvents EndCallback="OnEndCallbackBeneficioInteresChange" />
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server">
                                                                                        <dx:ASPxDateEdit ID="txtFechaInicioBeneficioInteres" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy" ClientInstanceName="txtFechaInicioBeneficioInteres" Style="width: 100%">
                                                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                                                            <CalendarProperties>
                                                                                                <FastNavProperties DisplayMode="Inline" />
                                                                                            </CalendarProperties>
                                                                                        </dx:ASPxDateEdit>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Fecha fin" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxCallbackPanel ID="cbpFechaFinBeneficioI" ClientInstanceName="cbpFechaFinBeneficioI" runat="server" OnCallback="cbpFechaFinBeneficioI_Callback" Style="width: 100%">
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server">
                                                                                        <dx:ASPxDateEdit ID="txtFechaFinBeneficioInteres" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy" ClientInstanceName="txtFechaFinBeneficioInteres" Style="width: 100%">
                                                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                                                            <CalendarProperties>
                                                                                                <FastNavProperties DisplayMode="Inline" />
                                                                                            </CalendarProperties>
                                                                                            <DateRangeSettings StartDateEditID="txtFechaInicioBeneficioInteres" />
                                                                                        </dx:ASPxDateEdit>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Otros Conceptos Beneficio" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbOtrosBeneficios" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClientSideEvents SelectedIndexChanged="OnOtrosBeneficioChange" Validation="onValidation" />
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_1">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere otro beneficio"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Fecha inicio" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxCallbackPanel ID="cbpFechaInicioOtroB" ClientInstanceName="cbpFechaInicioOtroB" runat="server" OnCallback="cbpFechaInicioOtroB_Callback" Style="width: 100%" ClientSideEvents-EndCallback="OnEndCallbackOtroBeneficioChange">
                                                                                <ClientSideEvents EndCallback="OnEndCallbackOtroBeneficioChange" />
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server">
                                                                                        <dx:ASPxDateEdit ID="txtFechaInicioOtroBeneficio" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy" ClientInstanceName="txtFechaInicioOtroBeneficio" Style="width: 100%" >
                                                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                                                            <CalendarProperties>
                                                                                                <FastNavProperties DisplayMode="Inline" />
                                                                                            </CalendarProperties>
                                                                                        </dx:ASPxDateEdit>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Fecha fin" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxCallbackPanel ID="cbpFechaFinOtroBeneficio" ClientInstanceName="cbpFechaFinOtroBeneficio" runat="server" OnCallback="cbpFechaFinOtroBeneficio_Callback" Style="width: 100%" >
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server">
                                                                                        <dx:ASPxDateEdit ID="txtFechaFinOtroBeneficio" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy" ClientInstanceName="txtFechaFinOtroBeneficio" Style="width: 100%">
                                                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                                                            <CalendarProperties>
                                                                                                <FastNavProperties DisplayMode="Inline" />
                                                                                            </CalendarProperties>
                                                                                            <DateRangeSettings StartDateEditID="txtFechaInicioOtroBeneficio" />
                                                                                        </dx:ASPxDateEdit>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Capitalización de intereses" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbCapitalizacionIntereses" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ClientSideEvents Validation="onValidation" />
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_1">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere capitalizacion intereses"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:EmptyLayoutItem ColumnSpan="1"></dx:EmptyLayoutItem>
                                                            </Items>
                                                        </dx:LayoutGroup>
                                                        <dx:LayoutGroup Width="100%" ColumnCount="3" GroupBoxDecoration="HeadingLine" ShowCaption="False" Caption="Pagare 2" Name="0_2">
                                                            <GroupBoxStyle>
                                                                <Caption Font-Bold="true" Font-Size="16" />
                                                            </GroupBoxStyle>
                                                            <GridSettings StretchLastItem="true" WrapCaptionAtWidth="660">
                                                                <Breakpoints>
                                                                    <dx:LayoutBreakpoint MaxWidth="800" ColumnCount="1" Name="S" />
                                                                    <dx:LayoutBreakpoint MaxWidth="1200" ColumnCount="2" Name="M" />
                                                                </Breakpoints>
                                                            </GridSettings>
                                                            <Items>
                                                                <dx:LayoutItem Caption="Tasa de interes" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbTasaInteresP2" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ClientSideEvents Validation="onValidation"/>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_2">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere la tasa de mora"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Puntos adicionales" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxButtonEdit ID="txtPuntosIPCP2" MaxLength="6" runat="server" Text="" ValidationSettings-Display="None">
                                                                                <MaskSettings Mask="<0..100g>.<00..99>" IncludeLiterals="DecimalSymbol" />
                                                                                <ClearButton DisplayMode="OnHover"></ClearButton>
                                                                                <ValidationSettings Display="None">
                                                                                </ValidationSettings>
                                                                            </dx:ASPxButtonEdit>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Tasa de mora" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbTasaMoraP2" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ClientSideEvents Validation="onValidation"/>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_2">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere la tasa de mora"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Plazo de la obligación" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbPlazoObligacionP2" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ClientSideEvents Validation="onValidation"/>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_2">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere el plazo de la obligación"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Cantidad años(Plazo)" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxButtonEdit ID="txtCantidadAniosP2" MaxLength="2" runat="server" Text="" OnCustomJSProperties="ASPxButtonEdit_CustomJSProperties">
                                                                                <ClearButton DisplayMode="OnHover"></ClearButton>
                                                                                <ClientSideEvents Validation="onValidation"/>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_2">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere la cantidad de años"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxButtonEdit>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="% Distribución base de compra P1" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxButtonEdit ID="txtPorcentajeDistribucionP1P2" MaxLength="6" runat="server" Text="" OnCustomJSProperties="ASPxButtonEdit_CustomJSProperties">
                                                                                <MaskSettings Mask="<0..100g>.<00..99>" IncludeLiterals="DecimalSymbol" />
                                                                                <ClearButton DisplayMode="OnHover"></ClearButton>
                                                                                <ClientSideEvents Validation="onValidation"/>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_2">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere el porcentaje de distribución"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxButtonEdit>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="% Distribución base de compra P2" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxButtonEdit ID="txtPorcentajeDistribucionP2P2" MaxLength="6" runat="server" Text="" ValidationSettings-Display="None">
                                                                                <MaskSettings Mask="<0..100g>.<00..99>" IncludeLiterals="DecimalSymbol" />
                                                                                <ClearButton DisplayMode="OnHover"></ClearButton>
                                                                                <ValidationSettings Display="None">
                                                                                </ValidationSettings>
                                                                            </dx:ASPxButtonEdit>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Periocidad de intereses" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbPeriocidadInteresesP2" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ClientSideEvents Validation="onValidation"/>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_2">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere la periocidad de intereses"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Periodo muerto" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbPeriodoMuertoP2" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClientSideEvents SelectedIndexChanged="OnPeriodoMuertoP2Change" Validation="onValidation"/>
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_2">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere el periodo muerto"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Cantidad años" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxCallbackPanel ID="cbpPeriodoMuertoP2" ClientInstanceName="cbpPeriodoMuertoP2" runat="server" OnCallback="cbpPeriodoMuertoP2_Callback" Style="width: 100%">
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server">
                                                                                        <dx:ASPxButtonEdit ID="txtCantidadPeriodoMuertoP2" MaxLength="2" runat="server" Text="" Style="width: 100%">
                                                                                            <ClearButton DisplayMode="OnHover"></ClearButton>
                                                                                        </dx:ASPxButtonEdit>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Calificación" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbCalificacionP2" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ClientSideEvents Validation="onValidation" />
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_2">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere la calificacion"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Periodo de gracia" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbPeriodoGraciaP2" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClientSideEvents SelectedIndexChanged="OnPeriodoGraciaP2Change" Validation="onValidation"/>
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_2">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere el periodo de gracia"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Cantidad años" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxCallbackPanel ID="cbpPeriodoGraciaP2" ClientInstanceName="cbpPeriodoGraciaP2" runat="server" OnCallback="cbpPeriodoGraciaP2_Callback" Style="width: 100%">
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server">
                                                                                        <dx:ASPxButtonEdit ID="txtCantidadPeriodoGraciaP2" MaxLength="2" runat="server" Text="" Style="width: 100%">
                                                                                            <ClearButton DisplayMode="OnHover"></ClearButton>
                                                                                        </dx:ASPxButtonEdit>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:EmptyLayoutItem ColumnSpan="1" />
                                                                <dx:LayoutItem Caption="Beneficio Capital" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbBeneficioCapitalP2" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="cppAb_Id" TextField="cppAb_Descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ClientSideEvents Validation="onValidation" />
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_2">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere el beneficio capital"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:EmptyLayoutItem ColumnSpan="2" />
                                                                <dx:LayoutItem Caption="Beneficio Seguro De Vida" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbBeneficioSeguroVidaP2" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClientSideEvents SelectedIndexChanged="OnSeguroVidaP2Change" Validation="onValidation"/>
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_2">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere el beneficio seguro de vida"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Fecha inicio" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxCallbackPanel ID="cbpFechaInicioSeguroVP2" ClientInstanceName="cbpFechaInicioSeguroVP2" runat="server" OnCallback="cbpFechaInicioSeguroVP2_Callback" Style="width: 100%" ClientSideEvents-EndCallback="OnEndCallbackSeguroVidaP2Change">
                                                                                <ClientSideEvents EndCallback="OnEndCallbackSeguroVidaP2Change" />
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server">
                                                                                        <dx:ASPxDateEdit ID="txtFechaInicioSeguroVidaP2" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy" ClientInstanceName="txtFechaInicioSeguroVidaP2" Style="width: 100%">
                                                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                                                            <CalendarProperties>
                                                                                                <FastNavProperties DisplayMode="Inline" />
                                                                                            </CalendarProperties>
                                                                                        </dx:ASPxDateEdit>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Fecha fin" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxCallbackPanel ID="cbpFechaFinSeguroVP2" ClientInstanceName="cbpFechaFinSeguroVP2" runat="server" OnCallback="cbpFechaFinSeguroVP2_Callback" Style="width: 100%">
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server">
                                                                                        <dx:ASPxDateEdit ID="txtFechaFinSeguroVidaP2" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy" ClientInstanceName="txtFechaFinSeguroVidaP2" Style="width: 100%">
                                                                                            <DateRangeSettings StartDateEditID="txtFechaInicioSeguroVidaP2" />
                                                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                                                            <CalendarProperties>
                                                                                                <FastNavProperties DisplayMode="Inline" />
                                                                                            </CalendarProperties>
                                                                                        </dx:ASPxDateEdit>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Beneficio Interes" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbBeneficioInteresP2" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClientSideEvents SelectedIndexChanged="OnBeneficioInteresP2Change" Validation="onValidation"/>
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_2">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere el beneficio seguro de vida"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Fecha inico" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxCallbackPanel ID="cbpFechaInicioBeneficioIP2" ClientInstanceName="cbpFechaInicioBeneficioIP2" runat="server" OnCallback="cbpFechaInicioBeneficioIP2_Callback" Style="width: 100%" ClientSideEvents-EndCallback="OnEndCallbackBeneficioInteresP2Change">
                                                                                <ClientSideEvents EndCallback="OnEndCallbackBeneficioInteresP2Change" />
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server">
                                                                                        <dx:ASPxDateEdit ID="txtFechaInicioBeneficioInteresP2" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy" ClientInstanceName="txtFechaInicioBeneficioInteresP2" Style="width: 100%">
                                                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                                                            <CalendarProperties>
                                                                                                <FastNavProperties DisplayMode="Inline" />
                                                                                            </CalendarProperties>
                                                                                        </dx:ASPxDateEdit>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Fecha fin" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxCallbackPanel ID="cbpFechaFinBeneficioIP2" ClientInstanceName="cbpFechaFinBeneficioIP2" runat="server" OnCallback="cbpFechaFinBeneficioIP2_Callback" Style="width: 100%">
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server">
                                                                                        <dx:ASPxDateEdit ID="txtFechaFinBeneficioInteresP2" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy" ClientInstanceName="txtFechaFinBeneficioInteresP2" Style="width: 100%">
                                                                                            <DateRangeSettings StartDateEditID="txtFechaInicioBeneficioInteresP2" />
                                                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                                                            <CalendarProperties>
                                                                                                <FastNavProperties DisplayMode="Inline" />
                                                                                            </CalendarProperties>
                                                                                        </dx:ASPxDateEdit>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Concepto otros beneficios" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbOtrosBeneficiosP2" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClientSideEvents SelectedIndexChanged="OnOtrosBeneficioP2Change" Validation="onValidation"/>
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_2">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere otros beneficios"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Fecha inicio" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxCallbackPanel ID="cbpFechaInicioOtrosBeneficiosP2" ClientInstanceName="cbpFechaInicioOtrosBeneficiosP2" runat="server" OnCallback="cbpFechaInicioOtrosBeneficiosP2_Callback" Style="width: 100%" ClientSideEvents-EndCallback="OnEndCallbackOtrosBeneficiosP2Change">
                                                                                <ClientSideEvents EndCallback="OnEndCallbackOtrosBeneficiosP2Change" />
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server">
                                                                                        <dx:ASPxDateEdit ID="txtFechaInicioOtrosBeneficiosP2" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy" ClientInstanceName="txtFechaInicioOtrosBeneficiosP2" Style="width: 100%">
                                                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                                                            <CalendarProperties>
                                                                                                <FastNavProperties DisplayMode="Inline" />
                                                                                            </CalendarProperties>
                                                                                        </dx:ASPxDateEdit>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Fecha fin" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                                    <SpanRules>
                                                                        <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                                    </SpanRules>
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxCallbackPanel ID="cbpFechaFinOtrosBeneficiosP2" ClientInstanceName="cbpFechaFinOtrosBeneficiosP2" runat="server" OnCallback="cbpFechaFinOtrosBeneficiosP2_Callback" Style="width: 100%">
                                                                                <PanelCollection>
                                                                                    <dx:PanelContent runat="server">
                                                                                        <dx:ASPxDateEdit ID="txtFechaFinOtrosBeneficiosP2" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy" ClientInstanceName="txtFechaFinOtrosBeneficiosP2" Style="width: 100%">
                                                                                            <DateRangeSettings StartDateEditID="txtFechaInicioOtrosBeneficiosP2" />
                                                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                                                            <CalendarProperties>
                                                                                                <FastNavProperties DisplayMode="Inline" />
                                                                                            </CalendarProperties>
                                                                                        </dx:ASPxDateEdit>
                                                                                    </dx:PanelContent>
                                                                                </PanelCollection>
                                                                            </dx:ASPxCallbackPanel>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:LayoutItem Caption="Capitalización de intereses" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                                    <LayoutItemNestedControlCollection>
                                                                        <dx:LayoutItemNestedControlContainer>
                                                                            <dx:ASPxComboBox ID="cbCapitalizacionInteresesP2" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion" OnCustomJSProperties="ASPxComboBox_CustomJSProperties">
                                                                                <ClearButton DisplayMode="Always"></ClearButton>
                                                                                <ClientSideEvents Validation="onValidation"/>
                                                                                <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="0_2">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere capitalizacion intereses"></RequiredField>
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </dx:LayoutItemNestedControlContainer>
                                                                    </LayoutItemNestedControlCollection>
                                                                </dx:LayoutItem>
                                                                <dx:EmptyLayoutItem ColumnSpan="1"></dx:EmptyLayoutItem>
                                                            </Items>
                                                        </dx:LayoutGroup>
                                                    </Items>
                                                </dx:TabbedLayoutGroup>
                                            </Items>
                                        </dx:ASPxFormLayout>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>

                        </div>
                        <div class="modal-footer">
                            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" CssClass="btnfeatures btnBordesRedondos tipoLetra" CausesValidation="true" AutoPostBack="false" ValidateInvisibleEditors="true" OnClick="btnGuardar_Click">
                                <ClientSideEvents Click="onValidationClick" />
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="bntCerrar" runat="server" Text="Cerrar" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e) { pcBeneficio.Hide(); }" />
                            </dx:ASPxButton>
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