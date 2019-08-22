<%@ Page Title="" Language="C#" MasterPageFile="~/SSOMaster.Master" AutoEventWireup="true" CodeBehind="CPPDatosBeneficiarios.aspx.cs" Inherits="CPPPresentacion.Maestros.CPPDatosBeneficiarios" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphCentro" runat="server" ClientIDMode="Static">

    <script src="../Scripts/Js/Maestros/JsCPPDatosBeneficiario.js"></script>
    <div class="container">
        <div class="tituloPagina">
            <h1>Beneficiarios</h1>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="udp">
        <ContentTemplate>
            <!-- GRID DE LA INFORMACION-->
            <div class="table-responsive">
                <dx:ASPxGridView ID="gvBeneficiarios" runat="server" AutoGenerateColumns="False" KeyFieldName="identificador;actividad" Width="100%" OnPageIndexChanged="gvBeneficiarios_PageIndexChanged"
                    OnBeforeColumnSortingGrouping="gvBeneficiarios_BeforeColumnSortingGrouping" OnPageSizeChanged="gvBeneficiarios_PageSizeChanged" OnRowCommand="gvBeneficiarios_RowCommand"
                    OnLoad="Grid_Load" EditFormLayoutProperties-SettingsAdaptivity-AdaptivityMode="SingleColumnWindowLimit" SettingsPager-EnableAdaptivity="true" SettingsPager-PageSize="5" >
                    <Toolbars>
                        <dx:GridViewToolbar EnableAdaptivity="true">
                            <Items>
                                <dx:GridViewToolbarItem>
                                    <Template>
                                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Nuevo" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="False" OnClick="btnNuevo_Click">
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
                                <dx:ASPxButton ID="btEditar" Image-IconID="actions_edit_16x16devav" ToolTip="Editar" runat="server" CommandName="Editar" CssClass="btnGrid"/>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos" VerticalAlign="Middle" />
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataColumn Caption="Tipo Identificación" FieldName="Documento" Width="170" SortOrder="Ascending" UnboundType="String">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="N° Identificación" FieldName="identificacion" Width="160" SortOrder="Ascending" UnboundType="Integer">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Nombre y Apellido" FieldName="nombreCompleto" Width="210" SortOrder="Ascending" UnboundType="String">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Dirección" FieldName="direccion" Width="190" SortOrder="Ascending" UnboundType="String">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Correo Electronico" FieldName="correo" Width="170" SortOrder="Ascending" UnboundType="String">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataTextColumn VisibleIndex="9" Caption="Ver Actividades" Width="140" CellStyle-HorizontalAlign="Center">
                            <DataItemTemplate>
                                <dx:ASPxButton ID="btnVerActividades" runat="server" ToolTip="Ver actividades" CommandName="MostrarActividades" CssClass="btnGrid" Image-IconID="find_find_16x16gray"></dx:ASPxButton>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos tablasDatosLink" VerticalAlign="Middle" />
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataColumn Caption="Departamento" FieldName="departamento" Width="150" SortOrder="Ascending" UnboundType="String" VisibleIndex="13">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Municipio" FieldName="municipio" Width="130" SortOrder="Ascending" UnboundType="String" VisibleIndex="14">
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

            <!-- Modal para la creacion y ediccion del beneficiario -->
            <dx:ASPxPopupControl ID="pcBeneficiario" runat="server" Width="1000" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcBeneficiario"
                HeaderText="Beneficiario" AllowDragging="false" PopupAnimationType="Fade" AutoUpdatePosition="true">
                <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MinHeight="50%" MinWidth="70%" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btOK">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>
                        <div id="contentDiv">
                            <asp:HiddenField ID="hdIdBeneficiario" runat="server" Value="" />
                            <dx:ASPxFormLayout runat="server" ID="formLayout" Width="100%" ClientInstanceName="FormLayout">
                                <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="500" />
                                <Items>
                                    <dx:LayoutGroup Width="100%" ColumnCount="2" GroupBoxDecoration="HeadingLine" Caption="Beneficiario" ShowCaption="False">
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

                                            <dx:LayoutItem Caption="Tipo de documento" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbTipoDoc" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="tdoc_id_tipo_documento" TextField="tdoc_vCodigo">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere tipo de documento"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>

                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="N° de documento" RequiredMarkDisplayMode="Hidden">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txIdentificacion" MaxLength="15" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere N° de documento"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Fecha de Expedición">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxDateEdit ID="txtFechaExped" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <CalendarProperties>
                                                                <FastNavProperties DisplayMode="Inline" />
                                                            </CalendarProperties>
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Nombre" RequiredMarkDisplayMode="Hidden">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtNombre" MaxLength="255" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datos" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere el nombre"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Apellido" RequiredMarkDisplayMode="Hidden">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtApellido" MaxLength="255" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datos" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere el apellido"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Telefono">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtTelefono" MaxLength="8" runat="server" Text="" ValidationSettings-Display="None">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <MaskSettings Mask="9 999 9999" IncludeLiterals="None" />
                                                            <ValidationSettings Display="None">
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Celular">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtCelular" MaxLength="12" runat="server" Text="" ValidationSettings-Display="None">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <MaskSettings Mask="999 999 9999" IncludeLiterals="None" />
                                                            <ValidationSettings Display="None">
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Dirección" VerticalAlign="Middle" ColumnSpan="2">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtDireccion" MaxLength="500" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Email" VerticalAlign="Middle" ColumnSpan="2" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txtEmail" MaxLength="1000" runat="server" Text="">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings ValidationGroup="datos" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" >
                                                                <RegularExpression ErrorText="Ingrese un Email valido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Tipo productor" VerticalAlign="Middle" RequiredMarkDisplayMode="Hidden">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbProductor" runat="server" ValueType="System.String" ValueField="id" TextField="productor">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datos" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere el tipo de productor"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem ShowCaption="False" VerticalAlign="Middle">
                                                <SpanRules>
                                                    <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                </SpanRules>
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButton ID="btnAsociar" runat="server" Text="Asociar actividad economica" RenderMode="Link" Theme="Material" AutoPostBack="false" OnClick="btnAsociar_Click">
                                                        </dx:ASPxButton>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Monto de activos" VerticalAlign="Middle">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxButtonEdit ID="txMontoActivos" MaxLength="13" runat="server" Text="" ValidationSettings-Display="None">
                                                            <MaskSettings Mask="<0..9999999999g>.<00..99>" IncludeLiterals="DecimalSymbol" />
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings Display="None">
                                                            </ValidationSettings>
                                                        </dx:ASPxButtonEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Fecha corte">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxDateEdit ID="txFechaCorte" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <CalendarProperties>
                                                                <FastNavProperties DisplayMode="Inline" />
                                                            </CalendarProperties>
                                                        </dx:ASPxDateEdit>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Departamento" RequiredMarkDisplayMode="Hidden">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="cbDepartamento" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="depratamento" CssClass="tipoLetra" EnableSynchronization="False">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) { OnCountryChanged(s); }" />
                                                            <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datos" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere el departamento"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>
                                            <dx:LayoutItem Caption="Municipio" RequiredMarkDisplayMode="Hidden">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" ClientInstanceName="cbp" runat="server">
                                                            <PanelCollection>
                                                                <dx:PanelContent runat="server">
                                                                    <dx:ASPxComboBox ID="cbMunicipio" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="municipio" CssClass="tipoLetra" EnableSynchronization="False" OnCallback="CmbCity_Callback" Width="100%">
                                                                        <ClearButton DisplayMode="Always"></ClearButton>
                                                                        <ClientSideEvents EndCallback=" OnEndCallback" />
                                                                        <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datos" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                            <RequiredField IsRequired="True" ErrorText="Se requiere el municipio"></RequiredField>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxComboBox>
                                                                </dx:PanelContent>
                                                            </PanelCollection>
                                                        </dx:ASPxCallbackPanel>
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
                                <ClientSideEvents Click="function(s, e) { pcBeneficiario.Hide(); }" />
                            </dx:ASPxButton>
                        </div>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ContentStyle>
                    <Paddings PaddingBottom="5px" />
                </ContentStyle>
            </dx:ASPxPopupControl>

            <!-- Modal de las actividades-->
            <dx:ASPxPopupControl ID="pcActividades" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                HeaderText="Actividades" AllowDragging="false" PopupAnimationType="Fade">
                <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MinHeight="50%" MinWidth="75%" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout1" Width="100%" ClientInstanceName="FormLayout">
                            <SettingsAdaptivity AdaptivityMode="SingleColumnWindowLimit" SwitchToSingleColumnAtWindowInnerWidth="500" />
                            <Items>
                                <dx:LayoutGroup Width="100%" ColumnCount="3" GroupBoxDecoration="HeadingLine" Caption="Actividad económica" ShowCaption="False">
                                    <GroupBoxStyle>
                                        <Caption Font-Bold="true" Font-Size="16" />
                                    </GroupBoxStyle>
                                    <GridSettings StretchLastItem="true" WrapCaptionAtWidth="660">
                                        <Breakpoints>
                                            <dx:LayoutBreakpoint MaxWidth="800" ColumnCount="1" Name="S" />
                                        </Breakpoints>
                                    </GridSettings>
                                    <Items>
                                        <dx:LayoutItem Caption="Digite filtro" VerticalAlign="Middle">
                                            <SpanRules>
                                                <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                            </SpanRules>
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxButtonEdit ID="txtActividadFiltro" runat="server" Text="" ToolTip="Para consultar varios valores separe por ','" AutoPostBack="false">
                                                        <ClearButton DisplayMode="Always"></ClearButton>
                                                    </dx:ASPxButtonEdit>
                                                    <asp:HiddenField ID="hditemSelec" runat="server" Value="" />
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" VerticalAlign="Middle">
                                            <SpanRules>
                                                <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                            </SpanRules>
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxButton ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="false" OnClick="btnFiltrar_Click">
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem ShowCaption="False" VerticalAlign="Middle">
                                            <SpanRules>
                                                <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                            </SpanRules>
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxButton ID="btnAceptarFiltro" ClientInstanceName="btnAceptarFiltro" runat="server" Text="Asociar Actividad" CssClass="btnfeatures btnBordesRedondos tipoLetra" OnClick="btnAceptarFiltro_Click">
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>
                        <div>
                            <div class="container1 horizontal-center-aligned">
                                <div class="contentEditors">
                                    <dx:ASPxListBox ID="ckListActividades" runat="server"
                                        Height="300px" SelectionMode="CheckColumn" Caption=""
                                        Width="400px"
                                        RepeatColumns="1"
                                        TextField="actividad"
                                        ValueField="id"
                                        ValueType="System.Int32"
                                        Border-BorderStyle="None">
                                        <CaptionSettings Position="Top" />
                                        <Border BorderStyle="None"></Border>
                                        <FilteringSettings ShowSearchUI="true" />
                                    </dx:ASPxListBox>
                                </div>
                                <div class="contentButtons">
                                    <div>
                                        <dx:ASPxButton ID="btnAdd"
                                            runat="server"
                                            CssClass="btnfeatures btnBordesRedondos tipoLetra"
                                            AutoPostBack="False"
                                            Text=">"
                                            ToolTip="Add selected items"
                                            OnClick="btnAdd_Click">
                                        </dx:ASPxButton>
                                    </div>
                                    <div class="TopPadding">
                                        <dx:ASPxButton ID="btnTodosAdd"
                                            runat="server"
                                            CssClass="btnfeatures btnBordesRedondos tipoLetra"
                                            AutoPostBack="False"
                                            Text=">>"
                                            ToolTip="Add all items"
                                            OnClick="btnTodosAdd_Click">
                                        </dx:ASPxButton>
                                    </div>
                                    <div style="height: 32px">
                                    </div>
                                    <div>
                                        <dx:ASPxButton ID="btnless"
                                            runat="server"
                                            CssClass="btnfeatures btnBordesRedondos tipoLetra"
                                            AutoPostBack="False"
                                            Text="<"
                                            ToolTip="Remove selected items"
                                            OnClick="btnless_Click">
                                        </dx:ASPxButton>
                                    </div>
                                    <div class="TopPadding">
                                        <dx:ASPxButton ID="btnTodosLess"
                                            runat="server"
                                            CssClass="btnfeatures btnBordesRedondos tipoLetra"
                                            AutoPostBack="False"
                                            Text="<<"
                                            ToolTip="Remove all items"
                                            OnClick="btnTodosLess_Click">
                                        </dx:ASPxButton>
                                    </div>
                                </div>
                                <div class="contentEditors">
                                    <dx:ASPxListBox ID="ckListActividades2" runat="server"
                                        Height="300px" SelectionMode="CheckColumn" Caption=""
                                        Width="350px"
                                        RepeatColumns="1"
                                        TextField="actividad"
                                        ValueField="id"
                                        ValueType="System.Int32"
                                        Border-BorderStyle="None">
                                        <CaptionSettings Position="Top" />
                                        <Border BorderStyle="None"></Border>
                                    </dx:ASPxListBox>
                                </div>
                            </div>
                        </div>

                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>

            <!-- Modal para la consulta de actividades-->
            <dx:ASPxPopupControl ID="ppActividadesAsociadas" runat="server" Width="1000" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ppActividadesAsociadas"
                HeaderText="Beneficiario" AllowDragging="false" PopupAnimationType="Fade" AutoUpdatePosition="true">
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
                        <dx:ASPxListBox ID="ckConsulta" runat="server" ClientInstanceName="lbChoosen" EnableSynchronization="True"
                            Height="300px" SelectionMode="Single" Caption=""
                            Width="100%"
                            EnableViewState="false"
                            RepeatColumns="1"
                            TextField="actividad"
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
