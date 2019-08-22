<%@ Page Title="" Language="C#" MasterPageFile="~/SSOMaster.Master" AutoEventWireup="true" CodeBehind="CPPPrograma.aspx.cs" Inherits="CPPPresentacion.Maestros.CPPPrograma" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphCentro" runat="server">
    <div class="container">
        <div class="tituloPagina">
            <h1>Programas</h1>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="udp">
        <ContentTemplate>
            <!-- GRID DE LA INFORMACION-->
            <div class="table-responsive">
                <dx:ASPxGridView ID="gvProgramas" runat="server" AutoGenerateColumns="False" KeyFieldName="id" Width="100%" OnPageIndexChanged="gvProgramas_PageIndexChanged"
                    OnBeforeColumnSortingGrouping="gvProgramas_BeforeColumnSortingGrouping" OnPageSizeChanged="gvProgramas_PageSizeChanged" OnRowCommand="gvProgramas_RowCommand"
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

                        <dx:GridViewDataColumn Caption="Id del programa" FieldName="id" Width="180" SortOrder="Ascending" UnboundType="Integer">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Nombre" FieldName="nombre" Width="166" SortOrder="Ascending" UnboundType="String">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataTextColumn Caption="Ver Plan Pagos" Width="180" CellStyle-HorizontalAlign="Center">
                            <DataItemTemplate>
                                <dx:ASPxButton ID="bnPlanPagos" runat="server" ToolTip="Ver Plan Pagos" CssClass="btnGrid" CommandName="Mostrar" Image-IconID="find_find_16x16gray"></dx:ASPxButton>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos" VerticalAlign="Middle" />
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataColumn Caption="Fecha Inicial" FieldName="fechaInicial" Width="150" SortOrder="Ascending" UnboundType="DateTime">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Pagares" FieldName="pagares" Width="150" SortOrder="Ascending" UnboundType="String">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Centro Utilidad" FieldName="centroUtilidad" Width="200" SortOrder="Ascending" UnboundType="String">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Descripcion" FieldName="descripcion" Width="200" SortOrder="Ascending" UnboundType="String">
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
            </div>

            <!-- Modal para la creacion y ediccion del programa-->
            <dx:ASPxPopupControl ID="pcPrograma" runat="server" Width="1000" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcPrograma"
                HeaderText="Programa" AllowDragging="false" PopupAnimationType="Fade" AutoUpdatePosition="true">
                <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MinHeight="50%" MinWidth="50%" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btOK">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>
                        <div id="contentDiv" class="clientContainer">
                            <asp:HiddenField ID="hdIdPrograma" runat="server" Value="" />
                            <dx:ASPxFormLayout runat="server" ID="formLayout" Width="100%" ClientInstanceName="FormLayout">
                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="500" />
                                <Items>
                                    <dx:LayoutGroup Width="100%" ColumnCount="2" GroupBoxDecoration="HeadingLine" ShowCaption="False">
                                        <GroupBoxStyle>
                                            <Caption Font-Bold="true" Font-Size="16" />
                                        </GroupBoxStyle>
                                        <GridSettings StretchLastItem="true" WrapCaptionAtWidth="660">
                                            <Breakpoints>
                                                <dx:LayoutBreakpoint MaxWidth="300" ColumnCount="1" Name="S" />
                                                <dx:LayoutBreakpoint MaxWidth="800" ColumnCount="2" Name="M" />
                                            </Breakpoints>
                                        </GridSettings>
                                        <Items>
                                            <dx:LayoutItem Caption="Nombre del programa" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtNombrePrograma" MaxLength="255" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datos" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere nombre del programa"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Plan de pagos" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButton ID="btnPlanPagos" runat="server" Text="Plan Pagos" AutoPostBack="false" CssClass="btnfeatures btnBordesRedondos tipoLetra">
                                                            <ClientSideEvents Click="function(s, e) { pcPlanPagos.Show(); }" />
                                                        </dx:ASPxButton>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Fecha Inicial" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxDateEdit ID="txtFechaInicial" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <CalendarProperties>
                                                                <FastNavProperties DisplayMode="Inline" />
                                                            </CalendarProperties>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datos" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere fecha inicial"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Cantidad de pagarés" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbPagares" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere la cantidad de pagares"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Centro de utilidad" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbCentroUtilidad" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="codigodescripcion">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere el centro de utilidad"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Descripcion" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxMemo ID="txtDescripcion" MaxLength="255" runat="server" Text="" Height="150px">
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datos" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere una descripcion"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxMemo>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                            </dx:ASPxFormLayout>
                        </div>
                        <div class="modal-footer">
                            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" CssClass="btnfeatures btnBordesRedondos tipoLetra" ValidationGroup="datos" OnClick="btnGuardar_Click" ClientInstanceName="btnGuardar">
                                <ClientSideEvents Click="function (s, e) {  
                                        s.SetEnabled(false);  
                                }" />
                            </dx:ASPxButton>
                            <dx:ASPxButton ID="bntCerrar" runat="server" Text="Cerrar" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e) { pcPrograma.Hide(); }" />
                            </dx:ASPxButton>
                        </div>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ContentStyle>
                    <Paddings PaddingBottom="5px" />
                </ContentStyle>
            </dx:ASPxPopupControl>

            <!-- Modal para la asociacion y ediccion del plan de pagos-->
            <dx:ASPxPopupControl ID="pcPlanPagos" runat="server" Width="1000" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcPlanPagos"
                HeaderText="Plan de pagos" AllowDragging="false" PopupAnimationType="Fade" AutoUpdatePosition="true">
                <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MinHeight="30%" MinWidth="50%" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <div class="clientContainer">
                            <asp:HiddenField ID="hdIdPlanPagos" runat="server" Value="" />
                            <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout1" Width="100%" ClientInstanceName="FormLayout">
                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="500" />
                                <Items>
                                    <dx:LayoutGroup Width="100%" ColumnCount="3" GroupBoxDecoration="HeadingLine" ShowCaption="False">
                                        <GroupBoxStyle>
                                            <Caption Font-Bold="true" Font-Size="16" />
                                        </GroupBoxStyle>
                                        <GridSettings StretchLastItem="true" WrapCaptionAtWidth="660">
                                            <Breakpoints>
                                                <dx:LayoutBreakpoint MaxWidth="300" ColumnCount="1" Name="S" />
                                                <dx:LayoutBreakpoint MaxWidth="500" ColumnCount="2" Name="M" />
                                                <dx:LayoutBreakpoint MaxWidth="800" ColumnCount="3" Name="X" />
                                            </Breakpoints>
                                        </GridSettings>
                                        <Items>
                                            <dx:LayoutItem Caption="Plan de Pago" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbPlanPago" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="descripcion">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="planPago">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere un plan de pagos"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Convenio" VerticalAlign="Middle" ColumnSpan="1" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtConvenio" MaxLength="255" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="planPago" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere un convenio"></RequiredField>
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
                                                        <dx:ASPxButton ID="btnAceptar" runat="server" Text="Aceptar" ValidationGroup="planPago" AutoPostBack="false" OnClick="btnAceptar_Click" CssClass="btnfeatures btnBordesRedondos tipoLetra"></dx:ASPxButton>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                            </dx:ASPxFormLayout>
                            <dx:ASPxGridView ID="gvPlanPagoPrograma" runat="server" AutoGenerateColumns="False" KeyFieldName="planPago" Width="100%" OnRowCommand="gvPlanPagoPrograma_RowCommand">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Acción" Width="125">
                                        <DataItemTemplate>
                                            <dx:ASPxButton ID="btEliminar" runat="server" Text="Eliminar" CssClass="btnfeatures btnBordesRedondos tipoLetra" CommandName="Eliminar"></dx:ASPxButton>
                                        </DataItemTemplate>
                                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                        <CellStyle CssClass="tablasDatos" VerticalAlign="Middle" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataColumn Caption="Plan de Pago" FieldName="planPago" Width="180" SortOrder="Ascending" UnboundType="Integer">
                                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                        <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Convenio" FieldName="convenio" Width="166" SortOrder="Ascending" UnboundType="String">
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
                            <dx:ASPxButton ID="btnGuardaPlanes" runat="server" Text="Guardar" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="false" OnClick="btnGuardaPlanes_Click" />
                            <dx:ASPxButton ID="btnCerrarPlanes" runat="server" Text="Cerrar" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="false" OnClick="btnCerrarPlanes_Click" />
                        </div>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ContentStyle>
                    <Paddings PaddingBottom="5px" />
                </ContentStyle>
            </dx:ASPxPopupControl>

            <!-- Modal para la visualizacion de los planes de pagos-->
            <dx:ASPxPopupControl ID="pcPlanPagosVisualizacion" runat="server" Width="1000" Modal="True" CloseOnEscape="true"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcPlanPagosVisualizacion"
                HeaderText="Plan de pagos" AllowDragging="false" PopupAnimationType="Fade" AutoUpdatePosition="true">
                <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MinHeight="30%" MinWidth="50%" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <div class="clientContainer">
                            <dx:ASPxGridView ID="gvPlanPagosAsociados" runat="server" AutoGenerateColumns="False" KeyFieldName="planPago" Width="100%">
                                <Columns>
                                    <dx:GridViewDataColumn Caption="Plan pago" FieldName="planPago" Width="360" UnboundType="Integer">
                                        <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                        <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Convenio" FieldName="convenio" Width="150" UnboundType="String">
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
