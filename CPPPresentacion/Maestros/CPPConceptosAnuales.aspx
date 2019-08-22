<%@ Page Title="" Language="C#" MasterPageFile="~/SSOMaster.Master" AutoEventWireup="true" CodeBehind="CPPConceptosAnuales.aspx.cs" Inherits="CPPPresentacion.Maestros.CPPConceptosAnuales" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphCentro" runat="server" ClientIDMode="Static">
    <link href="../Css/ConceptosAnuales.css" rel="stylesheet" />

    <div class="container">
        <div class="tituloPagina">
            <h1>Conceptos Anuales</h1>
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="udpConceptos">
        <ContentTemplate>
            <!-- GRID DE LA INFORMACION-->
            <div class="table-responsive">
                <dx:ASPxGridView ID="gvConceptos" runat="server" AutoGenerateColumns="False" KeyFieldName="cppCp_IdTipoCon;historico;historico;cppCp_Id" Width="100%"
                    OnBeforeColumnSortingGrouping="gvConceptos_BeforeColumnSortingGrouping" OnLoad="gvConceptos_Load" OnPageIndexChanged="gvConceptos_PageIndexChanged"
                    OnPageSizeChanged="gvConceptos_PageSizeChanged" OnRowCommand="gvConceptos_RowCommand" OnHtmlRowCreated="gvConceptos_HtmlRowCreated"
                    EditFormLayoutProperties-SettingsAdaptivity-AdaptivityMode="SingleColumnWindowLimit" SettingsPager-EnableAdaptivity="true" SettingsPager-PageSize="5">
                    <Toolbars>
                        <dx:GridViewToolbar EnableAdaptivity="true">
                            <Items>
                                <dx:GridViewToolbarItem>
                                    <Template>
                                        <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="False" OnClick="BtnNuevo_Click" />
                                    </Template>
                                </dx:GridViewToolbarItem>
                                <dx:GridViewToolbarItem>
                                    <Template>
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

                        <dx:GridViewDataColumn Caption="Concepto" FieldName="cppCp_Descripcion" Width="180" SortOrder="Ascending" UnboundType="String" SortIndex="1" CellStyle-HorizontalAlign="Center">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Valor" FieldName="cppCp_Valor" Width="166" SortOrder="Ascending" UnboundType="String" SortIndex="2" CellStyle-HorizontalAlign="Center">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Fecha inicio vigencia" FieldName="cppCp_FechaVigenciaDesde" Width="230" SortOrder="Ascending" UnboundType="DateTime" SortIndex="3" CellStyle-HorizontalAlign="Center">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Fecha final vigencia" FieldName="cppCp_FechaVigenciaHasta" Width="230" SortOrder="Ascending" UnboundType="DateTime" SortIndex="4" CellStyle-HorizontalAlign="Center">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Estado" FieldName="historico" Width="166" SortOrder="Ascending" UnboundType="String" SortIndex="0" CellStyle-HorizontalAlign="Center">
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
                        <PageSizeItemSettings Visible="true" Items="5, 10, 50" ShowAllItem="true" />
                    </SettingsPager>
                </dx:ASPxGridView>

            </div>

            <!-- Modal para la creacion y ediccion -->
            <dx:ASPxPopupControl ID="pcConceptosAnuales" runat="server" Width="1000" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcConceptosAnuales"
                HeaderText="Conceptos Anuales" AllowDragging="false" PopupAnimationType="Fade" AutoUpdatePosition="true">
                <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MinHeight="30%" MinWidth="70%" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btOK">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>
                        <div id="contentDiv">
                            <asp:HiddenField ID="hdIdConcepto" runat="server" Value="" />
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

                                            <dx:LayoutItem Caption="Tipo concepto" VerticalAlign="Middle" RequiredMarkDisplayMode="Hidden" Name="concepto" ColumnSpan="2">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="CbTipoConcepto" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="cppTc_Id" TextField="cppTc_Descripcion" AutoPostBack="true" OnSelectedIndexChanged="CbTipoConcepto_SelectedIndexChanged">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere tipo concepto"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Valor" RequiredMarkDisplayMode="Hidden" Name="valor">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="TxtValor" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere valor"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Fecha inicial vigencia" RequiredMarkDisplayMode="Hidden" Name="fechaInicial">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxDateEdit ID="TxtFechaInicial" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <CalendarProperties>
                                                                <FastNavProperties DisplayMode="Inline" />
                                                            </CalendarProperties>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datos" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere fecha inicial de la vigencia"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                            <dx:LayoutItem Caption="Fecha final vigencia" RequiredMarkDisplayMode="Hidden" Name="fechafinal">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxDateEdit ID="TxtFechaFinal" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <CalendarProperties>
                                                                <FastNavProperties DisplayMode="Inline" />
                                                            </CalendarProperties>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datos" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere fecha final de la vigencia"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>

                                        </Items>
                                    </dx:LayoutGroup>
                                </Items>
                            </dx:ASPxFormLayout>
                        </div>
                        <div class="modal-footer">
                            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" CssClass="btnfeatures btnBordesRedondos tipoLetra" ValidationGroup="datos" OnClick="btnGuardar_Click" />
                            <dx:ASPxButton ID="bntCerrar" runat="server" Text="Cerrar" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e) { pcConceptosAnuales.Hide(); }" />
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
