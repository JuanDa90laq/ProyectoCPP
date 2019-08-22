<%@ Page Title="" Language="C#" MasterPageFile="~/SSOMaster.Master" AutoEventWireup="true" CodeBehind="CPPDatosBeneficiariosOriginal.aspx.cs" Inherits="CPPPresentacion.Maestros.CPPDatosBeneficiariosOriginal" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCentro" runat="server" ClientIDMode="Static">    
    
    <script src="../Scripts/Js/Maestros/JsDatosbeneficiario.js"></script>
    <div class="divAgrupacionesbordesRedondos divAgrupacionesPrincipalInternas" style="width: 640px;">
        <div class="LbTitulosbordesSombras LbTitulosbordesRedondos">
            <dx:aspxlabel ID="lBTitulo" runat="server" Text="Beneficiarios - Consultas." CssClass="LbTitulosFeatures tipoLetra"/>
        </div>
        <div class="divAgrupacionesSecunInternas">
            <table style="width: auto;">
                <tr>
                    <td>
                        <dx:aspxlabel ID="lbConsultar" runat="server" Text="Tipo de documento" CssClass="LbFeatures tipoLetra"/>
                    </td>
                    <td>
                        <dx:aspxcombobox ID="cbTipoDocumento" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="tdoc_id_tipo_documento" TextField="tdoc_vCodigo" CssClass="tipoLetra"></dx:aspxcombobox>
                    </td>
                    <td>
                        <dx:aspxlabel ID="lbNumIdentificacion" runat="server" Text="N° de documento" CssClass="LbFeatures tipoLetra"/>
                    </td>
                    <td>
                        <dx:aspxtextbox ID="txtNumDocumento" MaxLength="15" runat="server" Text="" Width="200px" CssClass="txtEdicionTablas tipoLetraresult"/>
                    </td>
                </tr>
            </table>
            <div class="divBotones" style="align-content:center";>
                <dx:aspxbutton ID="btnConsultar" runat="server" Text="Consultar" CssClass="btnfeatures btnBordesRedondos tipoLetra" OnClick="btnConsultar_Click"></dx:aspxbutton>
                &nbsp;
                <dx:aspxbutton ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btnfeatures btnBordesRedondos tipoLetra" OnClick="btnNuevo_Click"></dx:aspxbutton>
            </div>
        </div>
    </div>  

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div id="divConsulta" class="divAgrupacionesbordesRedondos divAgrupacionesPrincipalInternas divAgrupacionesinferiores" runat="server" visible="false" style="width: 1205px">
                <div class="divNoAgrupacionesResultados" style="width:auto">
                    <dx:ASPxGridView ID="gvBeneficiarios" runat="server" AutoGenerateColumns="False" KeyFieldName="identificador;actividad"  Width="100%" OnPageIndexChanged="gvBeneficiarios_PageIndexChanged" OnBeforeColumnSortingGrouping="gvBeneficiarios_BeforeColumnSortingGrouping" OnPageSizeChanged="gvBeneficiarios_PageSizeChanged" OnRowCommand="gvBeneficiarios_RowCommand" OnHtmlRowCreated="gvBeneficiarios_HtmlRowCreated">
                    <Columns>

                        <dx:GridViewDataTextColumn Caption="Acción" Width="125">
                            <DataItemTemplate>
                                <dx:ASPxButton ID="btEditar" runat="server" Text="Detalle" CssClass="btnfeatures btnBordesRedondos tipoLetra" CommandName="Editar" />
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos" VerticalAlign="Middle" />
                        </dx:GridViewDataTextColumn>

                         <dx:GridViewDataColumn Caption="Tipo Identificación" FieldName="Documento" Width="180" SortOrder="Ascending" UnboundType="String">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                         </dx:GridViewDataColumn>

                         <dx:GridViewDataColumn Caption="N° Identificación" FieldName="identificacion" Width="166" SortOrder="Ascending" UnboundType="Integer">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                         </dx:GridViewDataColumn>

                         <dx:GridViewDataColumn Caption="Fecha Expedición" FieldName="fecha_Expedicion" Width="167" SortOrder="Ascending" UnboundType="DateTime">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                         </dx:GridViewDataColumn>

                         <dx:GridViewDataColumn Caption="Nombre y Apellido" FieldName="nombreCompleto" Width="310" SortOrder="Ascending" UnboundType="String">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                         </dx:GridViewDataColumn>

                         <dx:GridViewDataColumn Caption="Dirección" FieldName="direccion" Width="310" SortOrder="Ascending" UnboundType="String">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                         </dx:GridViewDataColumn>

                         <dx:GridViewDataColumn Caption="Telefono Fijo" FieldName="telefono" Width="140" SortOrder="Ascending" UnboundType="Integer">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                         </dx:GridViewDataColumn>

                         <dx:GridViewDataColumn Caption="Celular" FieldName="celular" Width="140" SortOrder="Ascending" UnboundType="Integer">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                         </dx:GridViewDataColumn>

                         <dx:GridViewDataColumn Caption="Correo Electronico" FieldName="correo" Width="175" SortOrder="Ascending" UnboundType="String">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                         </dx:GridViewDataColumn>

                         <dx:GridViewDataTextColumn VisibleIndex="9" Caption="Actividades" Width="130" >
                            <DataItemTemplate>
                                <dx:ASPxButton ID="btnVerActividades" runat="server" Text="Ver actividades" RenderMode="Link"  CommandName="MostrarActividades"></dx:ASPxButton>
                            </DataItemTemplate>
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos tablasDatosLink" VerticalAlign="Middle"/>
                        </dx:GridViewDataTextColumn>                        

                         <dx:GridViewDataColumn Caption="Tipo Productor" FieldName="productor" Width="145" SortOrder="Ascending" UnboundType="String" VisibleIndex="10">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                         </dx:GridViewDataColumn>

                         <dx:GridViewDataColumn Caption="Monto Activos" FieldName="montos_Activos" Width="145" SortOrder="Ascending" UnboundType="String" VisibleIndex="11">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                             <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                         </dx:GridViewDataColumn>

                         <dx:GridViewDataColumn Caption="Fecha Corte Activos" FieldName="fecha_Corte_Activos" Width="185" SortOrder="Ascending" UnboundType="DateTime" VisibleIndex="12">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                         </dx:GridViewDataColumn>

                         <dx:GridViewDataColumn Caption="Departamento" FieldName="departamento" Width="145" SortOrder="Ascending" UnboundType="String" VisibleIndex="13">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                         </dx:GridViewDataColumn>

                         <dx:GridViewDataColumn Caption="Municipio" FieldName="municipio" Width="140" SortOrder="Ascending" UnboundType="String" VisibleIndex="14">
                            <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                            <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                         </dx:GridViewDataColumn>                        

                    </Columns>
                    <Settings VerticalScrollableHeight="360" />
                    <Settings VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" />
                    <SettingsPager>
                        <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true"/>
                    </SettingsPager>
                    </dx:ASPxGridView>
                </div>
             </div>

            <%--<div id="dvActividades" title="Actividades Asociadas">--%>
            <dx:ASPxPopupControl ID="ppActividadesAsociadas" runat="server" Modal="true" HeaderText="Actividades asociadas" PopupVerticalAlign="TopSides"
                AllowResize="false" CssClass="ppActividades LbFeatures tipoLetra" CloseAction="CloseButton" ClientInstanceName="ppActividadesAsociadas" ScrollBars="None">
                <ContentCollection>
                    <dx:PopupControlContentControl>
                        <%--<dx:ASPxGridView ID="gvActividades" runat="server" AutoGenerateColumns="False" KeyFieldName="id" OnBeforeColumnSortingGrouping="gvActividades_BeforeColumnSortingGrouping" OnPageIndexChanged="gvActividades_PageIndexChanged" OnPageSizeChanged="gvActividades_PageSizeChanged"  Visible=" false">
                            <Columns>
                                <dx:GridViewDataColumn Caption="Actividad" FieldName="actividad" Width="145" SortOrder="Ascending" UnboundType="String">
                                <HeaderStyle Font-Bold="true" ForeColor="#4c6671" Font-Size="Smaller" Font-Names="Verdana, Tahoma" HorizontalAlign="Center" />
                                <CellStyle CssClass="tablasDatos letraResult" Font-Size="Smaller" Font-Names="Verdana, Tahoma" />
                                </dx:GridViewDataColumn>
                            </Columns>
                            <Settings VerticalScrollableHeight="200" />
                            <Settings VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" />
                            <SettingsPager>
                                <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true"/>
                            </SettingsPager>
                        </dx:ASPxGridView>    --%>                     

                        <asp:Panel ID="Panel1" runat="server" CssClass="scrollingControlContainer scrollingCheckBoxList">
                            <asp:CheckBoxList ID="ckConsulta" runat="server" CssClass="tipoLetraCheck" DataValueField="id" DataTextField="actividad" RepeatColumns="1" Width="600"></asp:CheckBoxList>
                        </asp:Panel>

                    </dx:PopupControlContentControl>
                 </ContentCollection>
            </dx:ASPxPopupControl>
            <%--</div>--%>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            
            <asp:HiddenField ID="hdIdBeneficiario" runat="server" Value=""/>
            <dx:ASPxPopupControl ID="ppBeneficiario" runat="server" Modal="true" HeaderText="Beneficiario" PopupVerticalAlign="TopSides"
                AllowResize="false" CssClass="ppBeneficiario LbFeatures tipoLetra" CloseAction="CloseButton" ClientInstanceName="ppBeneficiario" ScrollBars="None">
                <ContentCollection>
                    <dx:PopupControlContentControl>
                        <table style="width: 766px;">
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lbTipoDoc" runat="server" Text="Tipo de documento" CssClass="LbFeatures tipoLetra" />
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="cbTipoDoc" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="tdoc_id_tipo_documento" TextField="tdoc_vCodigo" CssClass="tipoLetra"></dx:ASPxComboBox>                                    
                                    
                                </td>           
                                <td colspan="3">
                                    <asp:RequiredFieldValidator ID="vldTipoDoc" runat="server" CssClass="tipoLetravalidadoresTablas validadoresTablas" ErrorMessage="*"
                                        ValidationGroup="validar" ControlToValidate="cbTipoDoc"></asp:RequiredFieldValidator>​
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lbIdentificacion" runat="server" Text="N° de documento" CssClass="LbFeatures tipoLetra" />
                                 </td>
                                 <td>
                                    <dx:ASPxTextBox ID="txIdentificacion" MaxLength="15" runat="server" Text="" Width="200px" CssClass="txtEdicionTablas tipoLetraresult"/>
                                 </td>
                                 <td>
                                     <asp:RequiredFieldValidator ID="vldidentificacion" runat="server" CssClass="tipoLetravalidadoresTablas validadoresTablas" ErrorMessage="*"
                                        ValidationGroup="validar" ControlToValidate="txIdentificacion"></asp:RequiredFieldValidator>​
                                 </td>
                                 <td >
                                    <dx:ASPxLabel ID="lbFechaExpedición" runat="server" Text="Fecha de Expedición" CssClass="LbFeatures tipoLetra" />
                                 </td>
                                 <td>
                                    <asp:TextBox ID="txtFechaExped" runat="server" Text="" Width="200px" CssClass="cajasPicker txtEdicionTablas tipoLetraresult"/>
                                 </td>
                                 <td>
                                     <%--<asp:RequiredFieldValidator ID="vlFecha" runat="server" CssClass="tipoLetravalidadoresTablas validadoresTablas" ErrorMessage="*"
                                        ValidationGroup="validar" ControlToValidate="txtFechaExped"></asp:RequiredFieldValidator>​--%>
                                     <asp:CompareValidator id="vlFecha2" runat="server" ValidationGroup="validar" CssClass="tipoLetravalidadoresTablas validadoresTablas" ToolTip="Fecha Invalida."  Type="Date" Operator="DataTypeCheck" ControlToValidate="txtFechaExped" ErrorMessage="*">
                                     </asp:CompareValidator>
                                  </td>                                  
                             </tr>
                             <tr>
                                <td>
                                    <dx:ASPxLabel ID="lbNombre" runat="server" Text="Nombre" CssClass="LbFeatures tipoLetra" />
                                 </td>
                                 <td>
                                    <dx:ASPxTextBox ID="txtNombre" MaxLength="255" runat="server" Text="" Width="200px" CssClass="txtEdicionTablas tipoLetraresult"/>
                                 </td>
                                 <td>
                                     <asp:RequiredFieldValidator ID="vlNombre" runat="server" CssClass="tipoLetravalidadoresTablas validadoresTablas" ErrorMessage="*"
                                        ValidationGroup="validar" ControlToValidate="txtNombre"></asp:RequiredFieldValidator>​
                                 </td>
                                 <td >
                                    <dx:ASPxLabel ID="lbApellido" runat="server" Text="Apellido" CssClass="LbFeatures tipoLetra" />
                                 </td>
                                 <td>
                                    <dx:ASPxTextBox ID="txtApellido" MaxLength="255" runat="server" Text="" Width="200px" CssClass="txtEdicionTablas tipoLetraresult"/>
                                 </td>
                                 <td>
                                     <asp:RequiredFieldValidator ID="vlApellido" runat="server" CssClass="tipoLetravalidadoresTablas validadoresTablas" ErrorMessage="*"
                                        ValidationGroup="validar" ControlToValidate="txtApellido"></asp:RequiredFieldValidator>​
                                  </td>                                  
                             </tr>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lbTelefono" runat="server" Text="Telefono" CssClass="LbFeatures tipoLetra" />
                                 </td>
                                 <td colspan="2">
                                     <asp:TextBox ID="txtTelefono" MaxLength="8" runat="server" Text="" Width="200px" CssClass="cajasTxtAspx tipoLetraresult"/>
                                 </td>
                                 <td >
                                    <dx:ASPxLabel ID="tbCelular" runat="server" Text="Celular" CssClass="LbFeatures tipoLetra" />
                                 </td>
                                 <td colspan="2">
                                    <asp:TextBox ID="txtCelular" MaxLength="12" runat="server" Text="" Width="200px" CssClass="cajasTxtAspx tipoLetraresult"/>
                                  </td>                                  
                             </tr>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lbDireccion" runat="server" Text="Dirección" CssClass="LbFeatures tipoLetra" />
                                 </td>
                                 <td colspan="5">
                                    <dx:ASPxTextBox ID="txtDireccion" MaxLength="500" runat="server" Text="" Width="563px" CssClass="txtEdicionTablas tipoLetraresult"/>
                                 </td>                                  
                             </tr>
                             <tr>
                                <td>
                                    <dx:ASPxLabel ID="lbEmail" runat="server" Text="Email" CssClass="LbFeatures tipoLetra" />
                                 </td>
                                 <td>
                                    <dx:ASPxTextBox ID="txtEmail" MaxLength="100" runat="server" Text="" Width="200px" CssClass="txtEdicionTablas tipoLetraresult"/>                                     
                                 <td colspan="4">
                                    <asp:RegularExpressionValidator runat="server" ValidationGroup="validar" CssClass="tipoLetravalidadoresTablas validadoresTablas" ValidationExpression="^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$" ErrorMessage="!Correo invalido" ControlToValidate="txtEmail"/>
                                 </td>                                                                   
                             </tr>
                            <tr>
                                <td >
                                   <dx:ASPxLabel ID="lbTipopro" runat="server" Text="Tipo productor" CssClass="LbFeatures tipoLetra" />
                                </td>
                                <td>
                                   <dx:ASPxComboBox ID="cbProductor" runat="server" ValueType="System.String" ValueField="id" TextField="productor" CssClass="tipoLetra"></dx:ASPxComboBox>                                    
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="vlproductor" runat="server" CssClass="tipoLetravalidadoresTablas validadoresTablas" ErrorMessage="*"
                                       ValidationGroup="validar" ControlToValidate="cbProductor"></asp:RequiredFieldValidator>​
                                </td>                          
                                <td>
                                </td>
                                <td style="text-align:left" colspan="2">
                                     <dx:ASPxButton ID="btnAsociar" runat="server" Text="Asociar actividad economica" RenderMode="Link" Theme="Material" OnClick="btnAsociar_Click"></dx:ASPxButton>                                    
                                 </td>                                 
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lbMontoActi" runat="server" Text="Monto de activos" CssClass="LbFeatures tipoLetra" />
                                 </td>
                                 <td colspan="2">                                    
                                     <asp:TextBox ID="txMontoActivos" MaxLength="15" runat="server" Text="" Width="200px" CssClass="cajasTxtAspx tipoLetraresult"/>
                                 </td>
                                 <td >
                                    <dx:ASPxLabel ID="lbFechaCorteActivos" runat="server" Text="Fecha corte" CssClass="LbFeatures tipoLetra" />
                                 </td>
                                 <td>
                                    <asp:TextBox ID="txFechaCorte" runat="server" Text="" Width="200px" CssClass="cajasPicker txtEdicionTablas tipoLetraresult"/>
                                 <td>
                                     <asp:CompareValidator id="cpFechaCorte" runat="server" ValidationGroup="validar" CssClass="tipoLetravalidadoresTablas validadoresTablas" ToolTip="Fecha Invalida."  Type="Date" Operator="DataTypeCheck" ControlToValidate="txFechaCorte" ErrorMessage="*">
                                     </asp:CompareValidator>
                                 </td>                                  
                            </tr>

                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lbDepartamento" runat="server" Text="Departamento" CssClass="LbFeatures tipoLetra" />
                                 </td>
                                 <td>                                   
                                     <dx:ASPxComboBox ID="cbDepartamento" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="depratamento" CssClass="tipoLetra" AutoPostBack="True" OnSelectedIndexChanged="cbDepartamento_SelectedIndexChanged" ViewStateMode="Enabled"></dx:ASPxComboBox>                                                                       
                                 </td>
                                 <td>
                                     <asp:RequiredFieldValidator ID="vlDeprtamento" runat="server" CssClass="tipoLetravalidadoresTablas validadoresTablas" ErrorMessage="*"
                                      ValidationGroup="validar" ControlToValidate="cbDepartamento"></asp:RequiredFieldValidator>​
                                 </td>
                                 <td >
                                    <dx:ASPxLabel ID="lbMunicipio" runat="server" Text="Municipio" CssClass="LbFeatures tipoLetra" />
                                 </td>
                                 <td>
                                    <dx:ASPxComboBox ID="cbMunicipio" DropDownStyle="DropDownList" runat="server" ValueType="System.String" ValueField="id" TextField="municipio" CssClass="tipoLetra"></dx:ASPxComboBox>                                    
                                 </td>
                                 <td>
                                     <asp:RequiredFieldValidator ID="vlMunicipio" runat="server" CssClass="tipoLetravalidadoresTablas validadoresTablas" ErrorMessage="*"
                                      ValidationGroup="validar" ControlToValidate="cbMunicipio"></asp:RequiredFieldValidator>​
                                  </td>                                  
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>                                
                                <td colspan="2" style="text-align:left">
                                    <dx:ASPxButton ID="btnAceptar" runat="server" ValidationGroup="validar" Text="Aceptar" CssClass="btnfeatures btnBordesRedondos tipoLetra" OnClick="btnAceptar_Click"></dx:ASPxButton>
                                    <dx:ASPxButton ID="btCancelar" runat="server" Text="Cancelar" CssClass="btnfeatures btnBordesRedondos tipoLetra">
                                        <ClientSideEvents Click="function(s, e) {  ppBeneficiario.Hide(); }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                         </table>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>


            <dx:ASPxPopupControl ID="ppAsociarActividad" runat="server" Modal="true" HeaderText="Actividad económica" PopupVerticalAlign="WindowCenter"
                                 PopupHorizontalAlign="WindowCenter" AllowResize="false" CloseAction="CloseButton" ClientInstanceName="ppAsociarActividad" ScrollBars="None"
                                 Width="800px">
            <ContentCollection>
            <dx:PopupControlContentControl>
                <div style="width: 800px;">
                    <table style="width: auto;">
                    <tr>
                        <td style="width: auto;">
                            <dx:aspxlabel ID="lbActividad" runat="server" Text="Digite filtro" CssClass="LbFeatures tipoLetra"/>
                        </td>
                        <td style="padding-left:5px; width: auto;">
                            <dx:aspxtextbox ID="txtActividadFiltro" MaxLength="500" runat="server" Text="" Width="200px" CssClass="txtEdicionTablas tipoLetraresult" ToolTip="Para consultar varios valores separe por ','"/>
                            <asp:HiddenField ID="hditemSelec" runat="server" Value=""/>
                        </td>
                        <td style="padding-left:10px; width: auto;">
                            <dx:aspxbutton ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btnfeatures btnBordesRedondos tipoLetra" OnClick="btnFiltrar_Click"></dx:aspxbutton>
                        </td>
                        <td style="padding-left:10px; width:auto">
                            <dx:aspxbutton ID="btnAceptarFiltro" runat="server" Text="Asociar Actividad" CssClass="btnfeatures btnBordesRedondos tipoLetra" Height="22px" Width="200px" OnClick="btnAceptarFiltro_Click" ></dx:aspxbutton>
                        </td>                        
                   </tr>                   
                   </table>                                            
                </div>
                <br /><br />
                <div class="LbTitulosbordesSombras LbTitulosbordesRedondos" style="width: 800px;" runat="server" id="actividadesEco" visible="false">
                    <table style="width: 800px;">
                        <tr>
                            <td style="width: 400px;">
                                <asp:Panel ID="checkBoxPanel" runat="server" CssClass="scrollingControlContainer scrollingCheckBoxList">
                                    <asp:CheckBoxList ID="ckListActividades" runat="server" CssClass="tipoLetraCheck CheckBoxList" DataValueField="id" DataTextField="actividad" RepeatColumns="1"></asp:CheckBoxList>
                                </asp:Panel>
                            </tds>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <dx:aspxbutton ID="btnAdd" runat="server" Text=">" CssClass="btnfeatures btnBordesRedondos tipoLetra" Width="55px" OnClick="btnAdd_Click"></dx:aspxbutton>                                            
                                        </td>                                        
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:aspxbutton ID="btnless" runat="server" Text="<" CssClass="btnfeatures btnBordesRedondos tipoLetra" Width="55px" OnClick="btnless_Click"></dx:aspxbutton>
                                        </td>                                        
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:aspxbutton ID="btnTodosAdd" runat="server" Text=">>" CssClass="btnfeatures btnBordesRedondos tipoLetra" Width="55px" OnClick="btnTodosAdd_Click"></dx:aspxbutton>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:aspxbutton ID="btnTodosLess" runat="server" Text="<<" CssClass="btnfeatures btnBordesRedondos tipoLetra" Width="55px" OnClick="btnTodosLess_Click"></dx:aspxbutton>
                                        </td>
                                    </tr>
                                </table>                                
                                <br />
                                
                            </td>
                            <td style="width: 400px;">
                                <asp:Panel ID="checkBoxPanel2" runat="server" CssClass="scrollingControlContainer scrollingCheckBoxList">
                                    <asp:CheckBoxList ID="ckListActividades2" runat="server" CssClass="tipoLetraCheck CheckBoxList" DataValueField="id" DataTextField="actividad" RepeatColumns="1"></asp:CheckBoxList>
                                </asp:Panel>
                            </td>
                        </tr>
                    
                    </table>
                </div>
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
