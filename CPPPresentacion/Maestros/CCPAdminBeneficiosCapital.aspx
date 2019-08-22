<%@ Page Title="" Language="C#" MasterPageFile="~/SSOMaster.Master" AutoEventWireup="true" CodeBehind="CCPAdminBeneficiosCapital.aspx.cs" Inherits="CPPPresentacion.Maestros.CCPAdminBeneficiosCapital" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphCentro" runat="server" ClientIDMode="Static">

    <script src="../Scripts/Js/Maestros/JsCppBeneficiosCapital.js"></script>

    <div class="container">
        <div class="tituloPagina">
            <h1>Administrar beneficios de capital</h1>
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="udpBeneficios">
        <ContentTemplate>

            <!-- GRID DE LA INFORMACION-->
            <div class="table-responsive" id="tablaBeneficio">

                <dx:ASPxGridView ID="gvBeneficiosCapital" runat="server" AutoGenerateColumns="False" KeyFieldName="cppAb_Id;cppAb_IdActividadTotal" Width="100%" OnRowCommand="gvBeneficiosCapital_RowCommand" OnBeforeColumnSortingGrouping="gvBeneficiosCapital_BeforeColumnSortingGrouping" OnLoad="gvBeneficiosCapital_Load" OnPageIndexChanged="gvBeneficiosCapital_PageIndexChanged" OnPageSizeChanged="gvBeneficiosCapital_PageSizeChanged">
                    <Toolbars>
                        <dx:GridViewToolbar>
                            <Items>
                                <dx:GridViewToolbarItem>
                                    <Template>
                                        <dx:ASPxButton ID="BtnNuevo" runat="server" Text="Nuevo" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="False" OnClick="BtnNuevo_Click">
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

                        <dx:GridViewDataColumn Caption="Programa" FieldName="programa" Width="160" SortOrder="Ascending" UnboundType="String" SortIndex="1">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Condición" FieldName="condicion" Width="110" SortOrder="Ascending" UnboundType="String" SortIndex="2">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Valor" FieldName="valor" Width="80" SortOrder="Ascending" UnboundType="Decimal" SortIndex="3">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <%--<dx:GridViewDataColumn Caption="Actividad economica" FieldName="Actividad" Width="180" SortOrder="Ascending" UnboundType="String" SortIndex="4">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>--%>

                        <dx:GridViewDataTextColumn Caption="Ver Actividades" Width="140" CellStyle-HorizontalAlign="Center">
                            <DataItemTemplate>
                                <dx:ASPxButton ID="btnVerActividades" runat="server" ToolTip="Ver actividades" CommandName="MostrarActividades" CssClass="btnGrid" Image-IconID="find_find_16x16gray"></dx:ASPxButton>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos tablasDatosLink" VerticalAlign="Middle" />
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataColumn Caption="Departamento" FieldName="depto" Width="145" SortOrder="Ascending" UnboundType="String" SortIndex="5">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Municipio" FieldName="municipio" Width="138" SortOrder="Ascending" UnboundType="String" SortIndex="6">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Fecha inicio" FieldName="cppAb_FechaInicio" Width="127" SortOrder="Ascending" UnboundType="DateTime" SortIndex="7">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Fecha final" FieldName="cppAb_FechaFinal" Width="127" SortOrder="Ascending" UnboundType="DateTime" SortIndex="8">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

                        <dx:GridViewDataColumn Caption="Descripción" FieldName="cppAb_Descripcion" Width="150" SortOrder="Ascending" UnboundType="String" SortIndex="9">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult SizeFont" Font-Names="Verdana, Tahoma" />
                        </dx:GridViewDataColumn>

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

            <!-- Modal para la creacion y ediccion -->
            <dx:ASPxPopupControl ID="pcBeneficiosCapital" runat="server" Width="1000" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcBeneficiosCapital"
                HeaderText="Beneficios capital" AllowDragging="false" PopupAnimationType="Fade" AutoUpdatePosition="true">
                <SettingsAdaptivity Mode="Always" VerticalAlign="WindowCenter" MinHeight="30%" MinWidth="70%" />
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server">
                        <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btOK">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxPanel>

                        <asp:HiddenField ID="hdIdBeneficio" runat="server" Value="" />

                        <dx:ASPxCallbackPanel runat="server" ID="CallbackPanel" ClientInstanceName="CallbackPanel">
                            <ClientSideEvents EndCallback="OnEndCallback"></ClientSideEvents>
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    
                                    <div id="contentDiv">

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

                                                        <dx:LayoutItem Caption="Programa" VerticalAlign="Middle" RequiredMarkDisplayMode="Hidden" ColumnSpan="2">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxComboBox ID="CbPrograma" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="nombre" Width="100%" AutoPostBack="false">
                                                                        <ClearButton DisplayMode="Always"></ClearButton>
                                                                        <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="datos">
                                                                            <RequiredField IsRequired="True" ErrorText="Se requiere programa"></RequiredField>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxComboBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>

                                                        <dx:LayoutItem Caption="Condicion" VerticalAlign="Middle" RequiredMarkDisplayMode="Hidden" ColumnSpan="2">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxComboBox ID="CbCondicion" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="cppCd_Id" TextField="cppCd_descripcion" Width="100%" AutoPostBack="false">
                                                                        <ClearButton DisplayMode="Always"></ClearButton>
                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) { OnCondicionChanged(s); }" />
                                                                        <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="datos">
                                                                            <RequiredField IsRequired="True" ErrorText="Se requiere condición"></RequiredField>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxComboBox>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>

                                                        <dx:LayoutItem Caption="Valor" VerticalAlign="Middle" RequiredMarkDisplayMode="Hidden">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxButtonEdit ID="TxtValor" runat="server" Text="" MaxLength="5">
                                                                        <MaskSettings Mask="<0..99g>.<00..99>" IncludeLiterals="DecimalSymbol" />
                                                                        <ClearButton DisplayMode="Always"></ClearButton>
                                                                        <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="datos">
                                                                            <RequiredField IsRequired="True" ErrorText="Se requiere valor"></RequiredField>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxButtonEdit>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>

                                                        <dx:LayoutItem Caption="Nuevo Porcentaje" VerticalAlign="Middle" RequiredMarkDisplayMode="Hidden">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxButtonEdit ID="txtNuevoValor" runat="server" Text="" MaxLength="5">
                                                                        <MaskSettings Mask="<0..99g>.<00..99>" IncludeLiterals="DecimalSymbol" />
                                                                        <ClearButton DisplayMode="Always"></ClearButton>
                                                                        <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                            <RequiredField IsRequired="True" ErrorText="Se requiere valor"></RequiredField>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxButtonEdit>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>

                                                        <%-- <dx:LayoutItem Caption="Actividad economica" VerticalAlign="Middle" RequiredMarkDisplayMode="Hidden" ColumnSpan="2">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer>
                                                        <dx:ASPxComboBox ID="CbActividad" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="actividad" Width="100%" AutoPostBack="false">
                                                            <ClearButton DisplayMode="Always"></ClearButton>
                                                            <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="datos">
                                                                <RequiredField IsRequired="True" ErrorText="Se requiere actividad economica"></RequiredField>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>--%>

                                                        <dx:LayoutItem Caption="Actividad economica" VerticalAlign="Middle" RequiredMarkDisplayMode="Hidden">
                                                            <SpanRules>
                                                                <dx:SpanRule ColumnSpan="1" RowSpan="1" BreakpointName="S"></dx:SpanRule>
                                                            </SpanRules>
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxDropDownEdit ClientInstanceName="CbActividadP" ID="CbActividadP" runat="server"
                                                                        AutoPostBack="false">
                                                                        <DropDownApplyButton Visibility="Always"></DropDownApplyButton>
                                                                        <SettingsAdaptivity Mode="OnWindowInnerWidth" ModalDropDownCaption="Cuenta" />
                                                                        <DropDownWindowTemplate>
                                                                            <dx:ASPxListBox ID="CbActividad" runat="server"
                                                                                SelectionMode="CheckColumn" Caption=""
                                                                                RepeatColumns="1"
                                                                                TextField="actividad"
                                                                                ValueField="id"
                                                                                ValueType="System.Int32"
                                                                                Border-BorderStyle="None"
                                                                                Width="100%"
                                                                                Height="400"
                                                                                ClientInstanceName="CbActividad">
                                                                                <FilteringSettings ShowSearchUI="true" />
                                                                                <CaptionSettings Position="Top" />
                                                                                <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datos" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                                    <RequiredField IsRequired="True" ErrorText="Se requiere al menos una actividad economica"></RequiredField>
                                                                                </ValidationSettings>
                                                                                <ClientSideEvents SelectedIndexChanged="updateText" Init="updateText" />
                                                                            </dx:ASPxListBox>
                                                                        </DropDownWindowTemplate>
                                                                        <ClientSideEvents DropDownCommandButtonClick="OnDropDownCommandButtonClick" TextChanged="synchronizeListBoxValues" DropDown="synchronizeListBoxValues" />
                                                                    </dx:ASPxDropDownEdit>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>

                                                        <dx:LayoutItem VerticalAlign="Middle" RequiredMarkDisplayMode="Hidden" ShowCaption="False">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxButton ID="btnSeleccionarTodo" runat="server" Text="Seleccionar todas las actividades economicas" RenderMode="Secondary" CssClass="btnfeatures btnBordesRedondos tipoLetra" Width="100px" Font-Size="X-Small" OnClick="ckTodos_Click" />
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>

                                                        <dx:LayoutItem Caption="Departamento" VerticalAlign="Middle" RequiredMarkDisplayMode="Hidden">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxComboBox ID="cbDepartamento" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="depratamento" EnableSynchronization="False">
                                                                        <ClearButton DisplayMode="Always"></ClearButton>
                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) { OnDeptoChanged(s); }" />
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
                                                                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" ClientInstanceName="cbp" runat="server" Width="200px">
                                                                        <PanelCollection>
                                                                            <dx:PanelContent runat="server">
                                                                                <dx:ASPxComboBox ID="cbMunicipio" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="municipio" CssClass="tipoLetra" EnableSynchronization="False" OnCallback="cbMunicipio_Callback">
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

                                                        <dx:LayoutItem Caption="Fecha inicial vigencia" VerticalAlign="Middle" RequiredMarkDisplayMode="Hidden">
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

                                                        <dx:LayoutItem Caption="Fecha final vigencia" VerticalAlign="Middle" RequiredMarkDisplayMode="Hidden">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxDateEdit ID="TxtFechaFinal" runat="server" EditFormat="Custom" UseMaskBehavior="true" EditFormatString="dd/MM/yyyy">
                                                                        <ClearButton DisplayMode="Always"></ClearButton>
                                                                        <CalendarProperties>
                                                                            <FastNavProperties DisplayMode="Inline" />
                                                                        </CalendarProperties>
                                                                        <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" ValidationGroup="datos" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True">
                                                                            <RequiredField IsRequired="True" ErrorText="Se requiere final de la vigencia"></RequiredField>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxDateEdit>
                                                                </dx:LayoutItemNestedControlContainer>
                                                            </LayoutItemNestedControlCollection>
                                                        </dx:LayoutItem>

                                                        <dx:LayoutItem Caption="Descripción" VerticalAlign="Middle" RequiredMarkDisplayMode="Hidden" ColumnSpan="2">
                                                            <LayoutItemNestedControlCollection>
                                                                <dx:LayoutItemNestedControlContainer>
                                                                    <dx:ASPxButtonEdit ID="TxtDescripcion" runat="server" Text="" MaxLength="500">
                                                                        <ClearButton DisplayMode="Always"></ClearButton>
                                                                        <ValidationSettings RequiredField-IsRequired="true" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True" Display="Dynamic" ValidationGroup="datos">
                                                                            <RequiredField IsRequired="True" ErrorText="Se requiere valor"></RequiredField>
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

                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>

                        <div class="modal-footer">
                            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" CssClass="btnfeatures btnBordesRedondos tipoLetra" ValidationGroup="datos" OnClick="btnGuardar_Click" />
                            <dx:ASPxButton ID="bntCerrar" runat="server" Text="Cerrar" CssClass="btnfeatures btnBordesRedondos tipoLetra" AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e) { pcBeneficiosCapital.Hide(); }" />
                            </dx:ASPxButton>
                        </div>

                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ContentStyle>
                    <Paddings PaddingBottom="5px" />
                </ContentStyle>
            </dx:ASPxPopupControl>

            <!-- Modal para la consulta de actividades-->
            <dx:ASPxPopupControl ID="ppActividadesAsociadas" runat="server" Width="1000" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ppActividadesAsociadas"
                HeaderText="Actividades economicas" AllowDragging="false" PopupAnimationType="Fade" AutoUpdatePosition="true">
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
