namespace CPPPresentacion.Maestros
{
    using CPPBL.Maestros;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using CPPENTL.Enumeraciones;
    using CPPPresentacion.Generica;
    using DevExpress.Web;
    using SSO.Finagro;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    public partial class CPPObligacion : System.Web.UI.Page
    {
        #region "definicion variables"

        private NumberFormatInfo formato = new NumberFormatInfo();
        private SSOSesion usuarioCpp = new SSOSesion();
        private Validaciones validar = new Validaciones();
        private DLAccesEntities accesoDatos = null;
        private ParametrizacionSPQUERY parametros = null;
        private ResultConsulta resultadoOperacion = null;
        private BLProgama programaBl = null;
        private BLBancosRecaudadoresAse bancoRecaudadorAseBL = null;
        private BLDatosbeneficiario datosbeneficiarioBL = null;
        private BLObligacion obligacionBl = null;
        private BLCodeudor codeudorBL = null;
        private BLPlanPagos planPagosBL = null;
        private List<EntitiesObligacion> lstObligacionBl = null;

        private string sUrlApi = ConfigurationManager.AppSettings["FINAGRO_SSO_URL_API"].ToString();
        #endregion

        #region "metodos protegidos"
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.usuarioCpp = (SSOSesion)Session["FINAGRO_SSO_SESION"];
                if (!this.IsPostBack)
                {
                    this.Inicializadores();
                    this.CargarDepartmentos();
                    Session["GarantiaIdonea"] = new List<EntitiesObligacionInmueble>();
                    this.validar.RegLog("Page_Load()", "CargarDepartmentos", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                    this.CargaCombos();
                    this.validar.RegLog("Page_Load()", "CPP_SP_ConsultarProgramas,CPP_SP_ConsultarBancoRecaudadorAseg,CPP_SP_ConsultarBeneficiario,CPP_SP_ConsultarSituacionJuridica,CPP_SP_ConsultarCodigoCIIU,CPP_SP_ConsultarTipoPlanPagos, CPP_SP_ConsultarModalidadCapital, CPP_SP_ConsultarPeriocidadIntereses",
                                        TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                    this.ConfigurarConsulta();
                    this.ConsultarObligaciones();
                    this.validar.RegLog("Page_Load()", "CPP_SP_ConsultarObligacion", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("Page_Load()", "CPP_SP_ConsultarObligacion", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (Nuevo) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Nuevo) :: " + ex.Message);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxEdit.ClearEditorsInContainer(this.pcObligacion, "datos", true);
                this.Limpiarobjetos();
                this.btnAjustarPlan.ClientEnabled = false;
                this.pcObligacion.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnNuevo_Click()", "N/A", "N/A", "Error al ejecutar el proceso (Nuevo) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Nuevo) :: " + ex.Message);
            }
        }

        protected void btnAjustarPlan_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnAjustarPlan.ClientEnabled = true;

                ASPxEdit.ClearEditorsInContainer(this.pcPlanPagos, "datosPlan", true);

                if (Session["PlanPago"] == null)
                {
                    this.ConfigurarConsultaPlan();

                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppPpa_Id",
                        parametroValor = string.IsNullOrEmpty(hdIdPlanPago.Value) ? Convert.ToInt32(this.cbPlanPago.SelectedItem.Value.ToString()) : Convert.ToInt32(hdIdPlanPago.Value)
                    };
                    this.accesoDatos.parametros.Add(this.parametros);

                    if (!string.IsNullOrEmpty(hdIdPlanPago.Value))
                    {
                        this.parametros = new ParametrizacionSPQUERY
                        {
                            parametro = "@unico",
                            parametroValor = true
                        };
                        this.accesoDatos.parametros.Add(this.parametros);
                    }

                    this.ConsultarPlanPagoEditar();
                }
                else
                {
                    EntitiesPlanPagos planPagos = new EntitiesPlanPagos();
                    planPagos = (EntitiesPlanPagos)Session["PlanPago"];

                    this.cbIntermediario.SelectedIndex = this.cbIntermediario.Items.FindByValue(planPagos.idIntermediario.ToString()).Index;
                    this.txPeridogracia.Value = planPagos.periodoGracia.ToString();
                    this.txtPalzoObligacion.Value = planPagos.plazoTotalObligacion.ToString();
                    this.txtPeriodoMuerto.Value = planPagos.periodoMuerto.ToString();
                    this.txtCuotasPlanPagos.Value = planPagos.numeroCuotasPlanPagos.ToString();
                    this.txtDescuentoAmortizacion.Value = planPagos.descuentoPorAmortizar.ToString();
                    this.txtImpuestoTimbre.Value = planPagos.impuestoTimbre.ToString();
                    this.txtDescuentoAmortizado.Value = planPagos.descuentoAmortizado.ToString();
                    this.txtPeriodicidadCapital.Value = planPagos.periocidadCapital.ToString();
                    this.cbTipoPlanPagos.SelectedIndex = this.cbTipoPlanPagos.Items.FindByValue(planPagos.idPlanPagos.ToString()).Index;
                    this.cbPeriocidadIntereses.SelectedIndex = this.cbPeriocidadIntereses.Items.FindByValue(planPagos.IdperiocidadInteresesCorrientes.ToString()).Index;
                    this.cbModalidadcapital.SelectedIndex = this.cbModalidadcapital.Items.FindByValue(planPagos.idModalidadCapital.ToString()).Index;
                    this.txtTasaInteresCorriente.Value = planPagos.tasaInteresesCorrientes.ToString();
                    this.txtFecha.Value = Convert.ToDateTime(planPagos.fechaPago.ToString());
                    this.txtPuntosContingentes.Value = planPagos.puntosContigentesInt.ToString();
                    this.txtTasaMoratoria.Value = planPagos.tasaInteresesMoratorios.ToString();
                    this.txtDescripcion.Value = planPagos.descripcion.ToString();
                }



                this.pcPlanPagos.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnAjustarPlan_Click()", "N/A", "N/A", "Error al ejecutar el proceso (Ajustar Plan) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ajustar Plan) :: " + ex.Message);
            }
        }

        protected void gvObligaciones_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                switch (e.CommandArgs.CommandName)
                {
                    case "Editar":

                        ASPxEdit.ClearEditorsInContainer(this.pcObligacion, "datos", true);

                        this.accesoDatos = new DLAccesEntities
                        {
                            procedimiento = "CPP_SP_ConsultarObligacion",
                            tipoejecucion = 1,
                            parametros = new List<ParametrizacionSPQUERY>()
                        };

                        this.parametros = new ParametrizacionSPQUERY
                        {
                            parametro = "@cppObl_Id",
                            parametroValor = Convert.ToInt32(e.KeyValue.ToString().Split('|')[0])
                        };
                        this.accesoDatos.parametros.Add(this.parametros);

                        this.ConsultarProgramaEditar();
                        this.validar.RegLog("gvObligaciones_RowCommand()", "CPP_SP_ConsultarObligacion", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                        this.btnAjustarPlan.ClientEnabled = true;
                        this.pcObligacion.ShowOnPageLoad = true;

                        break;
                    case "VerGarantias":
                        int tipoGarantia = Convert.ToInt32(e.KeyValue.ToString().Split('|')[1]);

                        if (tipoGarantia == 1)
                        {
                            this.accesoDatos = new DLAccesEntities
                            {
                                procedimiento = "CPP_SP_ConsultarObligacionInmueble",
                                tipoejecucion = 1,
                                parametros = new List<ParametrizacionSPQUERY>()
                            };

                            this.parametros = new ParametrizacionSPQUERY
                            {
                                parametro = "@cppObIn_IdObligacion",
                                parametroValor = Convert.ToInt32(e.KeyValue.ToString().Split('|')[0])
                            };
                            this.accesoDatos.parametros.Add(this.parametros);

                            this.ConsultarObligacionInmueble();

                            pcGarantiasIdoneasVisualizacion.ShowOnPageLoad = true;
                        }
                        else
                        {
                            this.accesoDatos = new DLAccesEntities
                            {
                                procedimiento = "CPP_SP_ConsultarObligacionCodeudor",
                                tipoejecucion = 1,
                                parametros = new List<ParametrizacionSPQUERY>()
                            };

                            this.parametros = new ParametrizacionSPQUERY
                            {
                                parametro = "@cppObl_Id",
                                parametroValor = Convert.ToInt32(e.KeyValue.ToString().Split('|')[0])
                            };
                            this.accesoDatos.parametros.Add(this.parametros);

                            this.ConsultarObligacionCodeudor();

                            pcGarantiasCodeudoresVisualizacion.ShowOnPageLoad = true;
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvObligaciones_RowCommand()", "CPP_SP_ConsultarObligacion", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (gvObligaciones_RowCommand): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvObligaciones_RowCommand): " + ex.Message);
            }
        }

        protected void gvObligaciones_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarObligaciones();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvObligaciones_PageIndexChanged()", "N/A", "N/A", "Error al ejecutar el proceso (Paginación) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Paginación) :: " + ex.Message);
            }
        }

        protected void gvObligaciones_PageSizeChanged(object sender, EventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarObligaciones();

            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvObligaciones_PageSizeChanged()", "N/A", "N/A", "Error al ejecutar el proceso (Paginación) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ajuste por pagina) :: " + ex.Message);
            }
        }

        protected void gvObligaciones_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarObligaciones();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvObligaciones_BeforeColumnSortingGrouping()", "N/A", "N/A", "Error al ejecutar el proceso (Paginación) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ordenamiento) :: " + ex.Message);
            }
        }

        protected void gvObligaciones_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                if (e.RowType.Equals(DevExpress.Web.GridViewRowType.Data))
                {
                    ASPxButton btnGarantias = this.gvObligaciones.FindRowCellTemplateControl(e.VisibleIndex, null, "btnGarantias") as ASPxButton;

                    if ((e.KeyValue.ToString().Split('|')[1].Equals("1") || e.KeyValue.ToString().Split('|')[1].Equals("2")) )
                        btnGarantias.Enabled = true;
                    else
                        btnGarantias.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvObligaciones_HtmlRowCreated()", "N/A", "N/A", "Error al ejecutar el proceso (gvObligaciones_HtmlRowCreated): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvObligaciones_HtmlRowCreated): " + ex.Message);
            }
        }

        protected void Grid_Load(object sender, EventArgs e)
        {
            gvObligaciones.SettingsBehavior.FilterRowMode = GridViewFilterRowMode.Auto;
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarObligaciones();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("Grid_Load()", "N/A", "N/A", "Error al ejecutar el proceso (Paginación) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ajuste por pagina) :: " + ex.Message);
            }
        }

        protected void cbPlanPago_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                this.CargaPlanPago(e.Parameter);
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbPlanPago_Callback()", "CPP_SP_ConsultarProgramaPlan", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (cbPlanPago_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbPlanPago_Callback): " + ex.Message);
            }
        }

        protected void cbIntermediario_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                this.CargarNitIntermediario(e.Parameter);
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbIntermediario_Callback()", "CPP_SP_ConsultarBancoRecaudadorAseg", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (cbIntermediario_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbIntermediario_Callback): " + ex.Message);
            }
        }

        protected void cbBeneficiario_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                this.CargarNombreBeneficiario(e.Parameter);
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbBeneficiario_Callback()", "CPP_SP_ConsultarBeneficiario", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (cbBeneficiario_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbBeneficiario_Callback): " + ex.Message);
            }
        }

        protected void txtConvenio_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                this.CargaConvenio(e.Parameter);
                this.btnAjustarPlan.ClientEnabled = true;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbPlanPago_Callback()", "CPP_SP_ConsultarBancoRecaudadorAseg", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (cbPlanPago_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbPlanPago_Callback): " + ex.Message);
            }
        }

        protected void cbMunciCompra_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                CargaMunicipioCompra(e.Parameter);
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbMunciCompra_Callback()", "N/A", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (cbMunciCompra_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (CmbCity_Callback): " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo encargo de llenar el combo box de municio compra
        /// </summary>
        /// <param name="countryName"></param>
        protected void CargaMunicipioCompra(string idDepartamento)
        {
            try
            {
                if (string.IsNullOrEmpty(idDepartamento)) return;

                this.cbMunciCompra.DataSource = ((List<Municipios>)Session["municipios"]).Where(a => a.idDepto.Equals(Convert.ToInt32(idDepartamento)));
                this.cbMunciCompra.DataBind();
                this.cbMunciCompra.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("CargaMunicipioCompra()", "N/A", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (Llenar Municipios): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Llenar Municipios): " + ex.Message);
            }
        }

        protected void cbMunciOrigen_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                CargaMunicipioOrigen(e.Parameter);
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbMunciOrigen_Callback()", "N/A", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (cbMunciOrigen_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbMunciOrigen_Callback): " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo encargo de llenar el combo box de municio compra
        /// </summary>
        /// <param name="countryName"></param>
        protected void CargaMunicipioOrigen(string idDepartamento)
        {
            try
            {
                if (string.IsNullOrEmpty(idDepartamento)) return;

                this.cbMunciOrigen.DataSource = ((List<Municipios>)Session["municipios"]).Where(a => a.idDepto.Equals(Convert.ToInt32(idDepartamento)));
                this.cbMunciOrigen.DataBind();
                this.cbMunciOrigen.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("CargaMunicipioOrigen()", "N/A", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (Llenar Municipios): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Llenar Municipios): " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo encargo de llenar el combo box de municio inversion
        /// </summary>
        /// <param name="countryName"></param>
        protected void cbMunciInv_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                CargaMunicipioInv(e.Parameter);
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbMunciInv_Callback()", "N/A", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (cbMunciInv_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbMunciInv_Callback): " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo encargo de llenar el combo box de municio inventario
        /// </summary>
        /// <param name="countryName"></param>
        protected void CargaMunicipioInv(string idDepartamento)
        {
            try
            {
                if (string.IsNullOrEmpty(idDepartamento)) return;

                this.cbMunciInv.DataSource = ((List<Municipios>)Session["municipios"]).Where(a => a.idDepto.Equals(Convert.ToInt32(idDepartamento)));
                this.cbMunciInv.DataBind();
                this.cbMunciInv.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("CargaMunicipioInv()", "N/A", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (Llenar Municipios): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Llenar Municipios): " + ex.Message);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                this.EjecucionSQLEditInsert();
                this.validar.RegLog("btnGuardar_Click()", "CPP_SP_EditarInsertarObligaciones", TipoAccionLog.insercion.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnGuardar_Click()", "CPP_SP_EditarInsertarObligaciones", TipoAccionLog.insercion.ToString(), "Error al ejecutar el proceso (btnGuardar_Click): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (btnAceptar_Click) :: " + ex.Message);
            }
        }

        protected void btnGuardarPlan_Click(object sender, EventArgs e)
        {
            try
            {
                EntitiesPlanPagos planPagos = new EntitiesPlanPagos()
                {
                    idIntermediario = this.cbIntermediario.SelectedIndex,
                    periodoGracia = Convert.ToInt32(this.txPeridogracia.Value),
                    plazoTotalObligacion = Convert.ToInt32(this.txtPalzoObligacion.Value),
                    periodoMuerto = Convert.ToInt32(this.txtPeriodoMuerto.Value),
                    numeroCuotasPlanPagos = Convert.ToInt32(this.txtCuotasPlanPagos.Value),
                    descuentoPorAmortizar = Convert.ToDouble(this.txtDescuentoAmortizacion.Value),
                    impuestoTimbre = Convert.ToDouble(this.txtImpuestoTimbre.Value),
                    descuentoAmortizado = Convert.ToDouble(this.txtDescuentoAmortizado.Value),
                    periocidadCapital = Convert.ToInt32(this.txtPeriodicidadCapital.Value),
                    idPlanPagos = Convert.ToInt32(this.cbTipoPlanPagos.SelectedItem.Value),
                    IdperiocidadInteresesCorrientes = Convert.ToInt32(this.cbPeriocidadIntereses.SelectedItem.Value),
                    idModalidadCapital = Convert.ToInt32(this.cbModalidadcapital.SelectedItem.Value),
                    tasaInteresesCorrientes = Convert.ToDouble(this.txtTasaInteresCorriente.Value),
                    fechaPago = Convert.ToDateTime(this.txtFecha.Value),
                    puntosContigentesInt = Convert.ToInt32(this.txtPuntosContingentes.Value),
                    tasaInteresesMoratorios = Convert.ToDouble(this.txtTasaMoratoria.Value),
                    descripcion = this.txtDescripcion.Value.ToString()
                };

                Session["PlanPago"] = planPagos;

                this.pcPlanPagos.ShowOnPageLoad = false;
                this.btnAjustarPlan.ClientEnabled = true;
                this.validar.RegLog("btnGuardarPlan_Click()", "CPP_SP_EditarInsertarPlanPagos", TipoAccionLog.insercion.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnGuardarPlan_Click()", "CPP_SP_EditarInsertarPlanPagos", TipoAccionLog.insercion.ToString(), "Error al ejecutar el proceso (btnGuardarPlan_Click): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (btnAceptar_Click) :: " + ex.Message);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                EntitiesObligacionInmueble entitiesObligacionInmueble = new EntitiesObligacionInmueble()
                {
                    idtipoInmueble = Convert.ToInt32(cbTipoInmueble.SelectedItem.Value.ToString()),
                    tipoInmueble = cbTipoInmueble.SelectedItem.Text.ToString(),
                    matriculaInmobiliaria = txtMatriculaMoviliaria.Value.ToString().Trim(),
                    direccion = txtDireccion.Value.ToString().Trim(),
                    valorInmueble = Convert.ToDouble(txtValorInmueble.Value.ToString().Trim())
                    
                };

                if (entitiesObligacionInmueble.valorInmueble != 0)
                {
                    List<EntitiesObligacionInmueble> listEntitiesObligacionInmueble = null;

                    if (Session["GarantiaIdonea"] == null)
                    {
                        listEntitiesObligacionInmueble = new List<EntitiesObligacionInmueble>();
                    }
                    else
                    {
                        listEntitiesObligacionInmueble = (List<EntitiesObligacionInmueble>)Session["GarantiaIdonea"];
                    }

                    listEntitiesObligacionInmueble.Add(entitiesObligacionInmueble);

                    Session["GarantiaIdonea"] = listEntitiesObligacionInmueble;

                    gvGarantiaIdonea.DataSource = Session["GarantiaIdonea"];
                    gvGarantiaIdonea.DataBind();

                    this.cbTipoInmueble.SelectedIndex = -1;
                    this.txtMatriculaMoviliaria.Value = string.Empty;
                    this.txtDireccion.Value = string.Empty;
                    this.txtValorInmueble.Value = string.Empty;
                }
                else
                {
                    this.ShowMensajes(0, "Error al agregar el inmueble valor del inmueble no puede ser cero ");
                }
                
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnAceptar_Click()", "N/A", TipoAccionLog.insercion.ToString(), "Error al ejecutar el proceso (btnAceptar_Click): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (btnAceptar_Click) :: " + ex.Message);
            }
        }

        protected void gvGarantiaIdonea_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                switch (e.CommandArgs.CommandName)
                {
                    case "Eliminar":

                        List<EntitiesObligacionInmueble> listObligacionInmueble = (List<EntitiesObligacionInmueble>)Session["GarantiaIdonea"];

                        EntitiesObligacionInmueble entitiesObligacionInmueble = listObligacionInmueble.FirstOrDefault(x => x.matriculaInmobiliaria == e.KeyValue.ToString());

                        listObligacionInmueble.Remove(entitiesObligacionInmueble);

                        Session["GarantiaIdonea"] = listObligacionInmueble.ToList();

                        gvGarantiaIdonea.DataSource = Session["GarantiaIdonea"];
                        gvGarantiaIdonea.DataBind();

                        this.cbTipoInmueble.SelectedIndex = -1;
                        this.txtMatriculaMoviliaria.Value = string.Empty;
                        this.txtDireccion.Value = string.Empty;

                        break;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvGarantiaIdonea_RowCommand()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (gvGarantiaIdonea_RowCommand): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvGarantiaIdonea_RowCommand) :: " + ex.Message);
            }
        }

        protected void btnGuardaIdonea_Click(object sender, EventArgs e)
        {
            try
            {
                var Garantia = ((List<EntitiesObligacionInmueble>)Session["GarantiaIdonea"]);

                if (Garantia == null)                    
                {
                    this.ShowMensajes(0, "No hay inmuebles por favor valide");
                }
                else
                {
                    pcIdonea.ShowOnPageLoad = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnGuardaIdonea_Click()", "N/A", "N/A", "Error al ejecutar el proceso (btnGuardaIdonea_Click) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (btnGuardaIdonea_Click) :: " + ex.Message);
            }
        }

        protected void btnCerrarIdonea_Click(object sender, EventArgs e)
        {
            try
            {
                pcIdonea.ShowOnPageLoad = false;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnCerrarIdonea_Click()", "N/A", "N/A", "Error al ejecutar el proceso (btnCerrarIdonea_Click) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (btnCerrarIdonea_Click) :: " + ex.Message);
            }
        }

        protected void btnAceptarCodeudor_Click(object sender, EventArgs e)
        {
            try
            {
                EntitiesCodeudor entitiesObligacionCodeudor = new EntitiesCodeudor()
                {
                    cppCo_Id = Convert.ToInt32(cbCodeudor.SelectedItem.Value.ToString()),
                    CedulaNombre = cbCodeudor.SelectedItem.Text.ToString()
                };

                List<EntitiesCodeudor> listObligacionesCodeudor = null;

                if (Session["GarantiaCodeudor"] == null)
                {
                    listObligacionesCodeudor = new List<EntitiesCodeudor>();
                }
                else
                {
                    listObligacionesCodeudor = (List<EntitiesCodeudor>)Session["GarantiaCodeudor"];
                }

                if(!listObligacionesCodeudor.Exists(x => x.cppCo_Id == entitiesObligacionCodeudor.cppCo_Id))
                {
                    listObligacionesCodeudor.Add(entitiesObligacionCodeudor);

                    Session["GarantiaCodeudor"] = listObligacionesCodeudor;

                    gvCodeudor.DataSource = Session["GarantiaCodeudor"];
                    gvCodeudor.DataBind();
                }

                this.cbCodeudor.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnAceptar_Click()", "N/A", TipoAccionLog.insercion.ToString(), "Error al ejecutar el proceso (btnAceptar_Click): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (btnAceptar_Click) :: " + ex.Message);
            }
        }

        protected void gvCodeudor_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                switch (e.CommandArgs.CommandName)
                {
                    case "Eliminar":

                        List<EntitiesCodeudor> listObligacionCodeudor = (List<EntitiesCodeudor>)Session["GarantiaCodeudor"];

                        EntitiesCodeudor obligacionCodeudor = listObligacionCodeudor.FirstOrDefault(x => x.cppCo_Id == Convert.ToInt32(e.KeyValue.ToString()));

                        listObligacionCodeudor.Remove(obligacionCodeudor);

                        Session["GarantiaCodeudor"] = listObligacionCodeudor.ToList();

                        gvCodeudor.DataSource = Session["GarantiaCodeudor"];
                        gvCodeudor.DataBind();

                        this.cbCodeudor.SelectedIndex = -1;

                        break;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvGarantiaIdonea_RowCommand()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (gvGarantiaIdonea_RowCommand): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvGarantiaIdonea_RowCommand) :: " + ex.Message);
            }
        }

        protected void btnGuardaCodeudor_Click(object sender, EventArgs e)
        {
            try
            {
                var Garantia = ((List<EntitiesCodeudor>)Session["GarantiaCodeudor"]);

                if (Garantia == null)
                {
                    this.ShowMensajes(0, "No hay codeudores por favor valide");
                }
                else
                {
                    pcCodeudor.ShowOnPageLoad = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnGuardaCodeudor_Click()", "N/A", "N/A", "Error al ejecutar el proceso (btnGuardaCodeudor_Click) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (btnGuabtnGuardaCodeudor_ClickrdaIdonea_Click) :: " + ex.Message);
            }
        }

        protected void btnCerrarCodeudor_Click(object sender, EventArgs e)
        {
            try
            {
                pcCodeudor.ShowOnPageLoad = false;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnCerrarCodeudor_Click()", "N/A", "N/A", "Error al ejecutar el proceso (btnCerrarCodeudor_Click) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (btnCerrarCodeudor_Click) :: " + ex.Message);
            }
        }

        #endregion

        #region "metodos privados"

        /// <summary>
        /// Formato a los campos
        /// </summary>
        private void Inicializadores()
        {
            this.txtIdObligacion.ClientEnabled = false;
            this.txtNitIntermediario.ClientEnabled = false;
            this.txtNombreDeudor.ClientEnabled = false;

            this.txtOperacionIntermediario.Attributes.Add("onKeyPress", "return soloNumeros(event)");
            this.txtOperacionIntermediario.Attributes.Add("onpaste", "return false");

            this.txPeridogracia.Attributes.Add("onKeyPress", "return soloNumeros(event)");
            this.txtPalzoObligacion.Attributes.Add("onKeyPress", "return soloNumeros(event)");
            this.txtCuotasPlanPagos.Attributes.Add("onKeyPress", "return soloNumeros(event)");
            this.txtPeriodoMuerto.Attributes.Add("onKeyPress", "return soloNumeros(event)");
            this.txtPeriodicidadCapital.Attributes.Add("onKeyPress", "return soloNumeros(event)");
            this.txtPuntosContingentes.Attributes.Add("onKeyPress", "return soloNumeros(event)");
        }

        /// <summary>
        /// carga los combos del formulario
        /// </summary>
        private void CargaCombos()
        {
            this.programaBl = new BLProgama();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarProgramas",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.cbPrograma.DataSource = this.programaBl.ConsultarProgramas(this.accesoDatos);
            this.cbPrograma.DataBind();

            this.bancoRecaudadorAseBL = new BLBancosRecaudadoresAse();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarBancoRecaudadorAseg",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.cbIntermediarioFinanciero.DataSource = this.bancoRecaudadorAseBL.ConsultarBancosRecaudadoresAse(this.accesoDatos);
            this.cbIntermediarioFinanciero.DataBind();

            this.cbIntermediario.DataSource = this.bancoRecaudadorAseBL.ConsultarBancosRecaudadoresAse(this.accesoDatos);
            this.cbIntermediario.DataBind();

            this.datosbeneficiarioBL = new BLDatosbeneficiario();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarBeneficiario",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.cbBeneficiario.DataSource = this.datosbeneficiarioBL.Consultarbeneficiarios(this.accesoDatos);
            this.cbBeneficiario.DataBind();

            this.obligacionBl = new BLObligacion();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarSituacionJuridica",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.cbSituacionJuridica.DataSource = this.obligacionBl.ConsultaSituacionJuridica(this.accesoDatos);
            this.cbSituacionJuridica.DataBind();

            this.obligacionBl = new BLObligacion();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarCodigoCIIU",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.cbCodigoCIIU.DataSource = this.obligacionBl.ConsultaCodigoCIIU(this.accesoDatos);
            this.cbCodigoCIIU.DataBind();

            this.obligacionBl = new BLObligacion();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarTipoGarantia",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.cbTipoGarantia.DataSource = this.obligacionBl.ConsultaTipoGarantia(this.accesoDatos);
            this.cbTipoGarantia.DataBind();

            this.codeudorBL = new BLCodeudor();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarCodeudor",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.cbCodeudor.DataSource = this.codeudorBL.ConsultarCodeudor(this.accesoDatos);
            this.cbCodeudor.DataBind();

            this.obligacionBl = new BLObligacion();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarTipoInmueble",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.cbTipoInmueble.DataSource = this.obligacionBl.ConsultaTipoInmueble(this.accesoDatos);
            this.cbTipoInmueble.DataBind();

            this.planPagosBL = new BLPlanPagos();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarTipoPlanPagos",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };
            this.cbTipoPlanPagos.DataSource = this.planPagosBL.ConsultarTipoPlanPagos(this.accesoDatos);
            this.cbTipoPlanPagos.DataBind();

            this.planPagosBL = new BLPlanPagos();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarModalidadCapital",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };
            this.cbModalidadcapital.DataSource = this.planPagosBL.ConsultarModalidadCapital(this.accesoDatos);
            this.cbModalidadcapital.DataBind();

            this.planPagosBL = new BLPlanPagos();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarPeriocidadIntereses",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };
            this.cbPeriocidadIntereses.DataSource = this.planPagosBL.ConsultarPeriocidadInteresesCorrientes(this.accesoDatos);
            this.cbPeriocidadIntereses.DataBind();

            Session["Actividades"] = this.validar.CargarActividades();
            cbActividadAgropecuaria.DataSource = Session["Actividades"];
            cbActividadAgropecuaria.DataBind();

            Session["Destinos"] = this.validar.DestinosCredito();
            cbDestino.DataSource = Session["Destinos"];
            cbDestino.DataBind();
        }

        /// <summary>
        /// muestra los mensajes al usuario
        /// </summary>
        /// <param name="tipo">tipod e operacion a realizar 0 -> procesos fallidos Mensaje a mostrar, 1 - > procesos Exitosos, 2 -> Informacion</param>
        /// <param name="mensaje">mensaje</param>
        private void ShowMensajes(int tipo, string mensaje)
        {
            this.validar.Mensajes(ref this.ppMensajesConfirmacion, ref this.lbMensaje, ref this.btCerrarSuperior, ref this.dvBtnAceptar, ref this.dvBtnCancelar, tipo, mensaje);
            this.ppMensajesConfirmacion.ShowOnPageLoad = true;

        }

        /// <summary>
        /// configura la cosulta
        /// </summary>
        private void ConfigurarConsulta()
        {
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarObligacion",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };
        }

        /// <summary>
        /// consulta codigos cuentas contables
        /// </summary>
        private void ConsultarObligaciones()
        {
            this.obligacionBl = new BLObligacion();
            this.lstObligacionBl = new List<EntitiesObligacion>();
            this.lstObligacionBl = this.obligacionBl.ConsultaObligacion(this.accesoDatos);

            if (this.lstObligacionBl.Count > 0)
            {
                foreach (EntitiesObligacion obligacion in this.lstObligacionBl)
                {
                    obligacion.departamentoCompra = ((List<Depatamentos>)Session["Depto"]).Where(a => a.id.Equals(Convert.ToInt32(obligacion.idDepartamentoCompra))).Select(x => x.depratamento).FirstOrDefault();
                    obligacion.municipioCompra = ((List<Municipios>)Session["municipios"]).Where(x => x.id.Equals(obligacion.idMunicipioCompra)).Select(x => x.municipio).FirstOrDefault();
                    obligacion.departamentoOrigen = ((List<Depatamentos>)Session["Depto"]).Where(x => x.id.Equals(obligacion.idDepartamentoOrigen)).Select(x => x.depratamento).FirstOrDefault();
                    obligacion.municipioOrigen = ((List<Municipios>)Session["municipios"]).Where(x => x.id.Equals(obligacion.idMunicipioOrigen)).Select(x => x.municipio).FirstOrDefault();
                    obligacion.departamentoInversion = ((List<Depatamentos>)Session["Depto"]).Where(x => x.id.Equals(obligacion.idDepartamentoInversion)).Select(x => x.depratamento).FirstOrDefault();
                    obligacion.municipioInversion = ((List<Municipios>)Session["municipios"]).Where(x => x.id.Equals(obligacion.idMunicipioInversion)).Select(x => x.municipio).FirstOrDefault();
                    obligacion.actividadAgropecuaria = ((List<EntitiesActividadesAgropecuarias>)Session["Actividades"]).Where(x => x.id.Equals(obligacion.idActividadAgropecuaria)).Select(x => x.actividad).FirstOrDefault();
                    obligacion.destino = ((List<EntitiesDestinos>)Session["Destinos"]).Where(x => x.id.Equals(obligacion.idDestino)).Select(x => x.actividad).FirstOrDefault();
                }

                this.gvObligaciones.DataSource = this.lstObligacionBl;
                this.gvObligaciones.DataBind();
            }
        }

        /// <summary>
        /// limpia los objetos del form
        /// </summary>
        private void Limpiarobjetos()
        {
            this.hdObligacion.Value = string.Empty;
            this.txtIdObligacion.Value = string.Empty;
            this.cbPrograma.SelectedIndex = -1;
            this.cbPlanPago.SelectedIndex = -1;
            this.txtConvenio.Value = string.Empty;
            this.cbIntermediarioFinanciero.SelectedIndex = -1;
            this.txtNitIntermediario.Value = string.Empty;
            this.cbBeneficiario.SelectedIndex = -1;
            this.txtNombreDeudor.Value = string.Empty;
            this.cbSituacionJuridica.SelectedIndex = -1;
            this.cbCodigoCIIU.SelectedIndex = -1;
            this.txtOperacionIntermediario.Value = string.Empty;
            this.cbDestino.SelectedIndex = -1;
            this.txtBaseCompra.Value = string.Empty;
            this.txtPorcentajeCompra.Value = string.Empty;
            this.txtValorPagagoFinagro.Value = string.Empty;
            this.txtFechaCompra.Value = string.Empty;
            this.txtAporteDinero.Value = string.Empty;
            this.txtAporteFinanciado.Value = string.Empty;
            this.txtCarteraInicial.Value = string.Empty;
            this.cbActividadAgropecuaria.SelectedIndex = -1;
            this.cbDeptoCompra.SelectedIndex = -1;
            this.cbMunciCompra.SelectedIndex = -1;
            this.cbDeptoOrigen.SelectedIndex = -1;
            this.cbMunciOrigen.SelectedIndex = -1;
            this.cbDeptoInv.SelectedIndex = -1;
            this.cbMunciInv.SelectedIndex = -1;
            this.cbTipoGarantia.SelectedIndex = -1;
            this.cbTipoInmueble.SelectedIndex = -1;
            this.txtMatriculaMoviliaria.Value = string.Empty;
            this.txtDireccion.Value = string.Empty;
            this.cbCodeudor.SelectedIndex = -1;

            //Limpa formulario de los planes unicos

            this.hdIdPlanPago.Value = string.Empty;
            this.cbIntermediario.SelectedIndex = -1;
            this.txPeridogracia.Value = string.Empty;
            this.txtPalzoObligacion.Value = string.Empty;
            this.txtPeriodoMuerto.Value = string.Empty;
            this.txtCuotasPlanPagos.Value = string.Empty;
            this.txtDescuentoAmortizacion.Value = string.Empty;
            this.txtImpuestoTimbre.Value = string.Empty;
            this.txtDescuentoAmortizado.Value = string.Empty;
            this.txtPeriodicidadCapital.Value = string.Empty;
            this.cbTipoPlanPagos.SelectedIndex = -1;
            this.cbPeriocidadIntereses.SelectedIndex = -1;
            this.cbModalidadcapital.SelectedIndex = -1;
            this.txtTasaInteresCorriente.Value = string.Empty;
            this.txtFecha.Value = string.Empty;
            this.txtPuntosContingentes.Value = string.Empty;
            this.txtTasaMoratoria.Value = string.Empty;
            this.txtDescripcion.Value = string.Empty;

            Session["PlanPago"] = null;
            Session["GarantiaIdonea"] = null;
            Session["GarantiaCodeudor"] = null;

            gvGarantiaIdonea.DataSource = Session["GarantiaIdonea"];
            gvGarantiaIdonea.DataBind();

            gvCodeudor.DataSource = Session["GarantiaCodeudor"];
            gvCodeudor.DataBind();
        }

        /// <summary>
        /// Metodo encargo de llenar el combo box de plan de pago
        /// </summary>
        /// <param name="countryName"></param>
        private void CargaPlanPago(string idPrograma)
        {
            try
            {
                this.txtConvenio.Value = string.Empty;
                this.txtConvenio.Text = string.Empty;

                this.programaBl = new BLProgama();
                if (string.IsNullOrEmpty(idPrograma)) return;

                this.cbPlanPago.Items.Clear();

                this.accesoDatos = new DLAccesEntities
                {
                    procedimiento = "CPP_SP_ConsultarProgramaPlan",
                    tipoejecucion = (int)TiposEjecucion.Procedimiento,
                    parametros = new List<ParametrizacionSPQUERY>()
                };

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPr_Id",
                    parametroValor = Convert.ToInt32(idPrograma)
                };
                this.accesoDatos.parametros.Add(this.parametros);


                this.cbPlanPago.DataSource = programaBl.ConsultarProgramaPlanPago(this.accesoDatos);
                this.cbPlanPago.DataBind();
                this.cbPlanPago.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("CargaPlanPago()", "N/A", "N/A", "Error al ejecutar el proceso (CargaPlanPago): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (CargaPlanPago): " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo encargo de llenar el textbox con el numero de convenio
        /// </summary>
        /// <param name="countryName"></param>
        private void CargaConvenio(string idPlanPago)
        {
            try
            {
                this.programaBl = new BLProgama();
                if (string.IsNullOrEmpty(idPlanPago)) return;

                this.cbPlanPago.Items.Clear();

                this.accesoDatos = new DLAccesEntities
                {
                    procedimiento = "CPP_SP_ConsultarProgramaPlan",
                    tipoejecucion = (int)TiposEjecucion.Procedimiento,
                    parametros = new List<ParametrizacionSPQUERY>()
                };

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPp_Id",
                    parametroValor = Convert.ToInt32(idPlanPago)
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPr_Id",
                    parametroValor = Convert.ToInt32(cbPrograma.SelectedItem.Value.ToString().Trim())
                };
                this.accesoDatos.parametros.Add(this.parametros);

                var listaConvenio = programaBl.ConsultarProgramaPlanPago(this.accesoDatos).FirstOrDefault();

                this.txtConvenio.Value = listaConvenio.convenio.ToString();
                this.txtConvenio.Validate();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("CargaConvenio()", "N/A", "N/A", "Error al ejecutar el proceso (CargaConvenio): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (CargaConvenio): " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo encargo de llenar el textbox con el nit del intermediario
        /// </summary>
        /// <param name="countryName"></param>
        private void CargarNitIntermediario(string idIntermediario)
        {
            try
            {
                this.txtNitIntermediario.Value = string.Empty;
                this.txtNitIntermediario.Text = string.Empty;

                this.bancoRecaudadorAseBL = new BLBancosRecaudadoresAse();
                if (string.IsNullOrEmpty(idIntermediario)) return;

                this.accesoDatos = new DLAccesEntities
                {
                    procedimiento = "CPP_SP_ConsultarBancoRecaudadorAseg",
                    tipoejecucion = (int)TiposEjecucion.Procedimiento,
                    parametros = new List<ParametrizacionSPQUERY>()
                };

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppBra_Id",
                    parametroValor = Convert.ToInt32(idIntermediario)
                };
                this.accesoDatos.parametros.Add(this.parametros);


                this.txtNitIntermediario.Value = bancoRecaudadorAseBL.ConsultarBancosRecaudadoresAse(this.accesoDatos).FirstOrDefault().nit;

            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("CargarNitIntermediario()", "N/A", "N/A", "Error al ejecutar el proceso (CargarNitIntermediario): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (CargarNitIntermediario): " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo encargo de llenar el textbox con el nombre del beneficiario
        /// </summary>
        /// <param name="countryName"></param>
        private void CargarNombreBeneficiario(string idBeneficiario)
        {
            try
            {
                this.txtNombreDeudor.Value = string.Empty;
                this.txtNombreDeudor.Text = string.Empty;

                this.datosbeneficiarioBL = new BLDatosbeneficiario();
                if (string.IsNullOrEmpty(idBeneficiario)) return;

                this.accesoDatos = new DLAccesEntities
                {
                    procedimiento = "CPP_SP_ConsultarBeneficiario",
                    tipoejecucion = (int)TiposEjecucion.Procedimiento,
                    parametros = new List<ParametrizacionSPQUERY>()
                };

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppBn_Id",
                    parametroValor = Convert.ToInt32(idBeneficiario)
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.txtNombreDeudor.Value = datosbeneficiarioBL.Consultarbeneficiarios(this.accesoDatos).FirstOrDefault().nombreCompleto;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("CargarNombreBeneficiario()", "N/A", "N/A", "Error al ejecutar el proceso (CargarNombreBeneficiario): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (CargarNombreBeneficiario): " + ex.Message);
            }
        }

        /// <summary>
        /// consulta los departamentos y carga los combos deptos
        /// </summary>
        private void CargarDepartmentos()
        {
            Session["Depto"] = this.validar.CargarDepartamentos();

            this.cbDeptoCompra.DataSource = Session["Depto"];
            this.cbDeptoCompra.DataBind();

            this.cbDeptoInv.DataSource = Session["Depto"];
            this.cbDeptoInv.DataBind();

            this.cbDeptoOrigen.DataSource = Session["Depto"];
            this.cbDeptoOrigen.DataBind();

            Session["municipios"] = this.validar.CargarMunicipios(0);
        }

        /// <summary>
        /// se invoca para realizar peticiones a la base de datos (Edicion inserción)
        /// </summary>
        private void EjecucionSQLInsertPlan()
        {

            EntitiesPlanPagos planPagos = (EntitiesPlanPagos)Session["PlanPago"];

            this.accesoDatos = new DLAccesEntities();
            this.planPagosBL = new BLPlanPagos();
            this.parametros = new ParametrizacionSPQUERY();

            this.accesoDatos.procedimiento = "CPP_SP_EditarInsertarPlanPagos";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

            if (!string.IsNullOrEmpty(this.hdIdPlanPago.Value))
            {
                this.parametros.parametro = "@cppPpa_Id";
                this.parametros.parametroValor = Convert.ToInt32(this.hdIdPlanPago.Value.Trim());
                this.accesoDatos.parametros.Add(this.parametros);
            }

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_IdIntermediario",
                parametroValor = Convert.ToInt32(planPagos.idIntermediario)
            };
            this.accesoDatos.parametros.Add(this.parametros);

            if (!string.IsNullOrEmpty(this.txtDescuentoAmortizacion.Text))
            {
                this.parametros = new ParametrizacionSPQUERY()
                {
                    parametro = "@cppPpa_DescuentoPorAmortizar",
                    parametroValor = Convert.ToDouble(planPagos.descuentoPorAmortizar)
                };
                this.accesoDatos.parametros.Add(this.parametros);
            }

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_DescuentoAmortizado",
                parametroValor = Convert.ToDouble(planPagos.descuentoAmortizado)
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_ImpuestoTimbre",
                parametroValor = Convert.ToDouble(planPagos.impuestoTimbre)
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_PeriocidadCapital",
                parametroValor = Convert.ToInt32(planPagos.periocidadCapital)
            };
            this.accesoDatos.parametros.Add(this.parametros);


            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_PeriodoGracia",
                parametroValor = Convert.ToInt32(planPagos.periodoGracia)
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_PeriodoMuerto",
                parametroValor = Convert.ToInt32(planPagos.periodoMuerto)
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_PlazoTotalObligacion",
                parametroValor = Convert.ToInt32(planPagos.plazoTotalObligacion)
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_NumeroCuotasPlanPagos",
                parametroValor = Convert.ToInt32(planPagos.numeroCuotasPlanPagos)
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_IdPlanPagos",
                parametroValor = Convert.ToInt32(planPagos.idPlanPagos)
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_IdModalidadCapital",
                parametroValor = Convert.ToInt32(planPagos.idModalidadCapital)
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_IdPeriocidadInteresesCorrientes",
                parametroValor = Convert.ToInt32(planPagos.IdperiocidadInteresesCorrientes)
            };
            this.accesoDatos.parametros.Add(this.parametros);

            if (this.txtTasaInteresCorriente.Text.Trim() != "0,00")
            {
                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPpa_TasaInteresesCorrientes",
                    parametroValor = Convert.ToDouble(planPagos.tasaInteresesCorrientes)
                };
                this.accesoDatos.parametros.Add(this.parametros);
            }
            else
            {
                throw (new Exception("BL - RegistrarEditarPlanesPago :: " + " tasa de  intereses corrientes no puede ser cero"));
            }

            if (!string.IsNullOrEmpty(this.txtPuntosContingentes.Text))
            {
                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPpa_PuntosContigentesInt",
                    parametroValor = Convert.ToInt32(planPagos.puntosContigentesInt)
                };

                this.accesoDatos.parametros.Add(this.parametros);
            }

            if (this.txtTasaMoratoria.Text.Trim() != "0,00")
            {
                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPpa_TasaInteresesMoratorios",
                    parametroValor = Convert.ToDouble(planPagos.tasaInteresesMoratorios)
                };

                this.accesoDatos.parametros.Add(this.parametros);
            }
            else
            {
                throw (new Exception("BL - RegistrarEditarPlanesPago :: " + " tasa de interes moratoria no puede ser cero"));
            }

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_FechaPago",
                parametroValor = Convert.ToDateTime(planPagos.fechaPago)
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_Descripcion",
                parametroValor = planPagos.descripcion
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = string.IsNullOrEmpty(this.hdIdPlanPago.Value) ? "@cppPpa_UsuarioCrea" : "@cppPpa_UsuarioModifica",
                parametroValor = Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@accion",
                parametroValor = string.IsNullOrEmpty(this.hdIdPlanPago.Value) ? Convert.ToBoolean("true") : Convert.ToBoolean("false")
            };
            this.accesoDatos.parametros.Add(this.parametros);

            if (this.OperacionInsertPlan())
            {
                this.ConfigurarConsultaPlan();

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPpa_Descripcion",
                    parametroValor = planPagos.descripcion
                };

                this.accesoDatos.parametros.Add(this.parametros);

                if (!string.IsNullOrEmpty(this.hdIdPlanPago.Value))
                {
                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@unico",
                        parametroValor = true
                    };

                    this.accesoDatos.parametros.Add(this.parametros);

                }

                this.ConsultarPlanPago();

            }
        }

        /// <summary>
        /// realiza la acciion sobre la base de datos
        /// </summary>
        /// <param name="parametros">objeto con la informacion a ejecutar</param>
        /// <param name="accion">true -> insercion o actualizacion, false -> consulta</param>
        /// <returns></returns>
        private bool OperacionInsertPlan()
        {
            this.planPagosBL = new BLPlanPagos();
            this.resultadoOperacion = this.planPagosBL.RegistrarEditarPlanesPago(this.accesoDatos);

            if (this.resultadoOperacion.estado.Equals(1))
            {
                this.ShowMensajes(1, "Operación Exitosa.");
                return true;
            }
            if (this.resultadoOperacion.estado.Equals(2))
            {
                this.ShowMensajes(2, "No se pudo efectuar la operación :: " + this.resultadoOperacion.mensaje);
                return false;
            }
            else
            {
                this.ShowMensajes(0, "Problemas en la operacion :: " + this.resultadoOperacion.mensaje);
                return false;
            }
        }

        /// <summary>
        /// configura la cosulta
        /// </summary>
        private void ConfigurarConsultaPlan()
        {
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarPlanesPago",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };
        }

        /// <summary>
        /// consulta planes de pagos
        /// </summary>
        private void ConsultarPlanPago()
        {
            this.planPagosBL = new BLPlanPagos();

            string idPlanPago = this.planPagosBL.ConsultarPlanesPago(this.accesoDatos).FirstOrDefault().id.ToString();

            if (string.IsNullOrEmpty(this.hdIdPlanPago.Value))
            {
                this.accesoDatos.procedimiento = "CPP_SP_EditarInsertarProgramaPlanCuenta";
                this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
                this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPr_Id",
                    parametroValor = Convert.ToInt32(this.cbPrograma.SelectedItem.Value.ToString())
                };

                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPp_Id",
                    parametroValor = Convert.ToInt32(idPlanPago)
                };

                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPrPp_Convenio",
                    parametroValor = txtConvenio.Value
                };

                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPrPp_Unico",
                    parametroValor = true
                };

                this.accesoDatos.parametros.Add(this.parametros);

                OperacionInsertUpdate();
            }

            this.hdIdPlanPago.Value = idPlanPago;
        }

        /// <summary>
        /// se invoca para realizar peticiones a la base de datos (Edicion inserción)
        /// </summary>
        private void EjecucionSQLEditInsert()
        {
            if (Session["PlanPago"] != null)
                this.EjecucionSQLInsertPlan();

            this.accesoDatos = new DLAccesEntities();
            this.obligacionBl = new BLObligacion();
            this.parametros = new ParametrizacionSPQUERY();

            this.accesoDatos.procedimiento = "CPP_SP_EditarInsertarObligaciones";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

            if (!string.IsNullOrEmpty(this.hdObligacion.Value))
            {
                this.parametros.parametro = "@cppObl_Id";
                this.parametros.parametroValor = Convert.ToInt32(this.hdObligacion.Value.Trim());
                this.accesoDatos.parametros.Add(this.parametros);
            }

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppObl_IdPrograma",
                parametroValor = Convert.ToInt32(this.cbPrograma.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppObl_IdPlanPago",
                parametroValor = Convert.ToInt32(this.cbPlanPago.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            if (!string.IsNullOrEmpty(this.hdIdPlanPago.Value))
            {
                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppObl_IdPlanPagoUnico",
                    parametroValor = Convert.ToInt32(this.hdIdPlanPago.Value)
                };
                this.accesoDatos.parametros.Add(this.parametros);
            }

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppObl_Convenio",
                parametroValor = this.txtConvenio.Value.ToString().Trim()
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppObl_IdBeneficiario",
                parametroValor = Convert.ToInt32(this.cbBeneficiario.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppObl_IdBancoRecaudador_Aseg",
                parametroValor = Convert.ToInt32(this.cbIntermediarioFinanciero.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppObl_IdSituacionJuridica",
                parametroValor = Convert.ToInt32(this.cbSituacionJuridica.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppObl_OperacionIntermediario",
                parametroValor = Convert.ToInt32(this.txtOperacionIntermediario.Text.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            if (this.txtBaseCompra.Text.Trim() != "0,00")
            {
                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppObl_BaseCompra",
                    parametroValor = Convert.ToDouble(this.txtBaseCompra.Text.Trim())
                };
                this.accesoDatos.parametros.Add(this.parametros);
            }
            else
            {
                throw (new Exception("BL - RegistrarEditarObligaciones :: " + " base compra no puede ser cero"));
            }

            if (this.txtPorcentajeCompra.Text.Trim() != "0,00")
            {
                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppObl_Porcentaje",
                    parametroValor = Convert.ToDouble(this.txtPorcentajeCompra.Text.Trim())
                };
                this.accesoDatos.parametros.Add(this.parametros);
            }
            else
            {
                throw (new Exception("BL - RegistrarEditarObligaciones :: " + " porcentaje compra no puede ser cero"));
            }

            if (this.txtValorPagagoFinagro.Text.Trim() != "0,00")
            {
                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppObl_ValorPagadoFinagro",
                    parametroValor = Convert.ToDouble(this.txtValorPagagoFinagro.Text.Trim())
                };
                this.accesoDatos.parametros.Add(this.parametros);
            }
            else
            {
                throw (new Exception("BL - RegistrarEditarObligaciones :: " + "Valor Pagado no puede ser cero"));
            }

            if (this.txtAporteDinero.Text.Trim() != "0,00")
            {
                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppObl_AporteDinero",
                    parametroValor = Convert.ToDouble(this.txtAporteDinero.Text.Trim())
                };
                this.accesoDatos.parametros.Add(this.parametros);
            }
            else
            {
                throw (new Exception("BL - RegistrarEditarObligaciones :: " + "Aporte dinero no puede ser cero"));
            }

            if (this.txtAporteFinanciado.Text.Trim() != "0,00")
            {
                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppObl_AporteFinanciado",
                    parametroValor = Convert.ToDouble(this.txtAporteFinanciado.Text.Trim())
                };
                this.accesoDatos.parametros.Add(this.parametros);
            }
            else
            {
                throw (new Exception("BL - RegistrarEditarObligaciones :: " + "Aporte financiado no puede ser cero"));
            }

            if (this.txtCarteraInicial.Text.Trim() != "0,00")
            {
                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppObl_ValorCarteraInicial",
                    parametroValor = Convert.ToDouble(this.txtCarteraInicial.Text.Trim())
                };
                this.accesoDatos.parametros.Add(this.parametros);
            }
            else
            {
                throw (new Exception("BL - RegistrarEditarObligaciones :: " + "Cartera inicial no puede ser cero"));
            }

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppObl_FechaCompra",
                parametroValor = Convert.ToDateTime(this.txtFechaCompra.Text.Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppObl_IdDestino",
                parametroValor = Convert.ToInt32(this.cbDestino.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppObl_IdActividadAgropecuaria",
                parametroValor = Convert.ToInt32(this.cbActividadAgropecuaria.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppObl_IdCodigoCIIU",
                parametroValor = Convert.ToInt32(this.cbCodigoCIIU.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppObl_IdDeptoCompra",
                parametroValor = Convert.ToInt32(this.cbDeptoCompra.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppObl_IdMunCompra",
                parametroValor = Convert.ToInt32(this.cbMunciCompra.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppObl_IdDeptoOrigen",
                parametroValor = Convert.ToInt32(this.cbDeptoOrigen.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppObl_IdMunOrigen",
                parametroValor = Convert.ToInt32(this.cbMunciOrigen.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppObl_IdDeptoInv",
                parametroValor = Convert.ToInt32(this.cbDeptoInv.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppObl_IdMunInv",
                parametroValor = Convert.ToInt32(this.cbMunciInv.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            if (!string.IsNullOrEmpty(this.cbTipoGarantia.SelectedItem.Value.ToString()))
            {
                if ((Session["GarantiaIdonea"] != null && this.cbTipoGarantia.SelectedItem.Value.ToString() == "1") || (Session["GarantiaCodeudor"] != null && this.cbTipoGarantia.SelectedItem.Value.ToString() == "2"))
                {
                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppObl_IdTipoGarantia",
                        parametroValor = Convert.ToInt32(this.cbTipoGarantia.SelectedItem.Value.ToString())
                    };
                    this.accesoDatos.parametros.Add(this.parametros);
                }
                else
                {
                    throw (new Exception("BL - RegistrarEditarObligaciones :: " + "La garantia no puede ser vacio"));
                }

            }

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = string.IsNullOrEmpty(this.hdObligacion.Value) ? "@cppObl_UsuarioCrea" : "@cppObl_UsuarioModifica",
                parametroValor = Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@accion",
                parametroValor = string.IsNullOrEmpty(this.hdObligacion.Value) ? Convert.ToBoolean("true") : Convert.ToBoolean("false")
            };
            this.accesoDatos.parametros.Add(this.parametros);

            if (this.OperacionEditInsert())
            {
                this.accesoDatos = new DLAccesEntities();
                this.obligacionBl = new BLObligacion();
                this.parametros = new ParametrizacionSPQUERY();

                this.accesoDatos.procedimiento = "CPP_SP_ConsultarObligacion";
                this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
                this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppObl_IdPrograma",
                    parametroValor = Convert.ToInt32(this.cbPrograma.SelectedItem.Value.ToString().Trim())
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppObl_IdPlanPago",
                    parametroValor = Convert.ToInt32(this.cbPlanPago.SelectedItem.Value.ToString().Trim())
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppObl_IdBeneficiario",
                    parametroValor = Convert.ToInt32(this.cbBeneficiario.SelectedItem.Value.ToString().Trim())
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppObl_IdBancoRecaudador_Aseg",
                    parametroValor = Convert.ToInt32(this.cbIntermediarioFinanciero.SelectedItem.Value.ToString().Trim())
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.ConsultarObligaciones();

                EntitiesObligacion obligacion = this.obligacionBl.ConsultaObligacion(this.accesoDatos).FirstOrDefault();
                if (cbTipoGarantia.SelectedItem.Value.ToString() == "1")
                {
                    this.InsertarRelacionObligacionInmueble(obligacion.id);
                }
                else if (cbTipoGarantia.SelectedItem.Value.ToString() == "2")
                {
                    this.InsertarRelacionObligacionCodeudor(obligacion.id);
                }

                Session["PlanPago"] = null;
                this.pcObligacion.ShowOnPageLoad = false;
            }
        }

        /// <summary>
        /// realiza la acciion sobre la base de datos
        /// </summary>
        /// <param name="parametros">objeto con la informacion a ejecutar</param>
        /// <param name="accion">true -> insercion o actualizacion, false -> consulta</param>
        /// <returns></returns>
        private bool OperacionEditInsert()
        {
            this.obligacionBl = new BLObligacion();
            this.resultadoOperacion = this.obligacionBl.RegistrarEditarObligaciones(this.accesoDatos);

            if (this.resultadoOperacion.estado.Equals(1))
            {
                this.ShowMensajes(1, "Operación Exitosa.");
                return true;
            }
            if (this.resultadoOperacion.estado.Equals(2))
            {
                this.ShowMensajes(2, "No se pudo efectuar la operación :: " + this.resultadoOperacion.mensaje);
                return false;
            }
            else
            {
                this.ShowMensajes(0, "Problemas en la operacion :: " + this.resultadoOperacion.mensaje);
                return false;
            }
        }

        /// <summary>
        /// Consulta para traer la informacion de la obligacion 
        /// </summary>
        private void ConsultarProgramaEditar()
        {
            this.obligacionBl = new BLObligacion();

            EntitiesObligacion obligacion = this.obligacionBl.ConsultaObligacion(this.accesoDatos).FirstOrDefault();

            this.Limpiarobjetos();

            this.hdObligacion.Value = obligacion.id.ToString();
            this.txtIdObligacion.Value = obligacion.id.ToString();
            this.cbPrograma.SelectedIndex = this.cbPrograma.Items.FindByValue(obligacion.idPrograma.ToString()).Index;
            this.CargaPlanPago(obligacion.idPrograma.ToString());
            this.cbPlanPago.SelectedIndex = obligacion.idPlanPago != null ? this.cbPlanPago.Items.FindByValue(obligacion.idPlanPago.ToString()).Index : -1;
            this.txtConvenio.Value = obligacion.convenio;
            this.cbIntermediarioFinanciero.SelectedIndex = this.cbIntermediarioFinanciero.Items.FindByValue(obligacion.idBancoRecaudador.ToString()).Index;
            this.txtNitIntermediario.Value = obligacion.bancoRecaudador.ToString();
            this.cbBeneficiario.SelectedIndex = this.cbBeneficiario.Items.FindByValue(obligacion.idBeneficiario.ToString()).Index;
            this.txtNombreDeudor.Value = obligacion.beneficiario;
            this.cbSituacionJuridica.SelectedIndex = this.cbSituacionJuridica.Items.FindByValue(obligacion.idSituacionJuridica.ToString()).Index;
            this.cbCodigoCIIU.SelectedIndex = this.cbCodigoCIIU.Items.FindByValue(obligacion.idCodigoCIIU.ToString()).Index;
            this.txtOperacionIntermediario.Value = obligacion.operacionIntermediario.ToString();
            this.cbDestino.SelectedIndex = this.cbDestino.Items.FindByValue(obligacion.idDestino.ToString()).Index;
            this.txtBaseCompra.Value = Convert.ToDouble(obligacion.baseCompra);
            this.txtPorcentajeCompra.Value = Convert.ToDouble(obligacion.porcentaje);
            this.txtValorPagagoFinagro.Value = Convert.ToDouble(obligacion.valorPagadoFinagro);
            this.txtFechaCompra.Value = Convert.ToDateTime(obligacion.fechaCompra);
            this.txtAporteDinero.Value = Convert.ToDouble(obligacion.aporteDinero);
            this.txtAporteFinanciado.Value = Convert.ToDouble(obligacion.aporteFinanciado);
            this.txtCarteraInicial.Value = Convert.ToDouble(obligacion.valorCarteraInicial);
            this.cbActividadAgropecuaria.SelectedIndex = cbActividadAgropecuaria.Items.FindByValue(obligacion.idActividadAgropecuaria.ToString()).Index;
            this.cbDeptoCompra.SelectedIndex = this.cbDeptoCompra.Items.FindByValue(obligacion.idDepartamentoCompra.ToString()).Index;
            this.CargaMunicipioCompra(obligacion.idDepartamentoCompra.ToString());
            this.cbMunciCompra.SelectedIndex = this.cbMunciCompra.Items.FindByValue(obligacion.idMunicipioCompra.ToString()).Index;
            this.cbDeptoOrigen.SelectedIndex = this.cbDeptoOrigen.Items.FindByValue(obligacion.idDepartamentoOrigen.ToString()).Index;
            this.CargaMunicipioOrigen(obligacion.idDepartamentoOrigen.ToString());
            this.cbMunciOrigen.SelectedIndex = this.cbMunciOrigen.Items.FindByValue(obligacion.idMunicipioOrigen.ToString()).Index;
            this.cbDeptoInv.SelectedIndex = this.cbDeptoInv.Items.FindByValue(obligacion.idDepartamentoInversion.ToString()).Index;
            this.CargaMunicipioInv(obligacion.idDepartamentoInversion.ToString());
            this.cbMunciInv.SelectedIndex = this.cbMunciInv.Items.FindByValue(obligacion.idMunicipioInversion.ToString()).Index;
            this.cbTipoGarantia.SelectedIndex = (obligacion.idTipoGarantia != null) ? this.cbTipoGarantia.Items.FindByValue(obligacion.idTipoGarantia.ToString()).Index : -1;
            this.hdIdPlanPago.Value = obligacion.idPlanPagoUnico.ToString();
            
            btnAsociarGarantia.ClientEnabled = false;
            cbTipoGarantia.ClientEnabled = false;

            this.planPagosBL = new BLPlanPagos();

            this.ConfigurarConsultaPlan();

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_Id",
                parametroValor = string.IsNullOrEmpty(hdIdPlanPago.Value) ? Convert.ToInt32(obligacion.idPlanPago) : Convert.ToInt32(this.hdIdPlanPago.Value)
            };
            this.accesoDatos.parametros.Add(this.parametros);

            if (!string.IsNullOrEmpty(hdIdPlanPago.Value))
            {
                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@unico",
                    parametroValor = true
                };
                this.accesoDatos.parametros.Add(this.parametros);
            }

            this.ConsultarPlanPagoEditar();
        }

        private void ConsultarPlanPagoEditar()
        {
            this.planPagosBL = new BLPlanPagos();

            EntitiesPlanPagos planPagos = this.planPagosBL.ConsultarPlanesPago(this.accesoDatos).FirstOrDefault();

            if (!string.IsNullOrEmpty(hdIdPlanPago.Value))
                Session["PlanPago"] = planPagos;

            this.cbIntermediario.SelectedIndex = this.cbIntermediario.Items.FindByValue(planPagos.idIntermediario.ToString()).Index;
            this.txPeridogracia.Value = planPagos.periodoGracia.ToString();
            this.txtPalzoObligacion.Value = planPagos.plazoTotalObligacion.ToString();
            this.txtPeriodoMuerto.Value = planPagos.periodoMuerto.ToString();
            this.txtCuotasPlanPagos.Value = planPagos.numeroCuotasPlanPagos.ToString();
            this.txtDescuentoAmortizacion.Value = planPagos.descuentoPorAmortizar.ToString();
            this.txtImpuestoTimbre.Value = planPagos.impuestoTimbre.ToString();
            this.txtDescuentoAmortizado.Value = planPagos.descuentoAmortizado.ToString();
            this.txtPeriodicidadCapital.Value = planPagos.periocidadCapital.ToString();
            this.cbTipoPlanPagos.SelectedIndex = this.cbTipoPlanPagos.Items.FindByValue(planPagos.idPlanPagos.ToString()).Index;
            this.cbPeriocidadIntereses.SelectedIndex = this.cbPeriocidadIntereses.Items.FindByValue(planPagos.IdperiocidadInteresesCorrientes.ToString()).Index;
            this.cbModalidadcapital.SelectedIndex = this.cbModalidadcapital.Items.FindByValue(planPagos.idModalidadCapital.ToString()).Index;
            this.txtTasaInteresCorriente.Value = planPagos.tasaInteresesCorrientes.ToString();
            this.txtFecha.Value = Convert.ToDateTime(planPagos.fechaPago.ToString());
            this.txtPuntosContingentes.Value = planPagos.puntosContigentesInt.ToString();
            this.txtTasaMoratoria.Value = planPagos.tasaInteresesMoratorios.ToString();
            this.txtDescripcion.Value = planPagos.descripcion.ToString();
        }

        /// <summary>
        /// realiza la acciion sobre la base de datos
        /// </summary>
        /// <param name="parametros">objeto con la informacion a ejecutar</param>
        /// <param name="accion">true -> insercion o actualizacion, false -> consulta</param>
        /// <returns></returns>
        private bool OperacionInsertUpdate()
        {
            this.programaBl = new BLProgama();
            this.resultadoOperacion = this.programaBl.RegistrarEditarProgramas(this.accesoDatos);

            if (this.resultadoOperacion.estado.Equals(1))
            {
                this.ShowMensajes(1, "Operación Exitosa.");
                return true;
            }
            if (this.resultadoOperacion.estado.Equals(2))
            {
                this.ShowMensajes(2, "No se pudo efectuar la operación :: " + this.resultadoOperacion.mensaje);
                return false;
            }
            else
            {
                this.ShowMensajes(0, "Problemas en la operacion :: " + this.resultadoOperacion.mensaje);
                return false;
            }
        }

        /// <summary>
        /// consulta programa y inserta la relacion con los planes de pago
        /// </summary>
        private void InsertarRelacionObligacionInmueble(int idObligacion)
        {
            this.accesoDatos.procedimiento = "CPP_SP_EliminarObligacionInmobiliaria";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppObIn_IdObligacion",
                parametroValor = Convert.ToInt32(idObligacion)
            };

            this.accesoDatos.parametros.Add(this.parametros);

            if (OperacionInsertUpdate())
            {
                var lstObligacionesInmuebles = (List<EntitiesObligacionInmueble>)Session["GarantiaIdonea"];

                foreach (var obligacionInmueble in lstObligacionesInmuebles)
                {
                    this.accesoDatos.procedimiento = "CPP_SP_EditarInsertarObligacionInmueble";
                    this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
                    this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppObIn_IdObligacion",
                        parametroValor = Convert.ToInt32(idObligacion)
                    };

                    this.accesoDatos.parametros.Add(this.parametros);

                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppObIn_IdTipoInmueble",
                        parametroValor = Convert.ToInt32(obligacionInmueble.idtipoInmueble)
                    };

                    this.accesoDatos.parametros.Add(this.parametros);

                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppObIn_MatriculaInmobiliaria",
                        parametroValor = obligacionInmueble.matriculaInmobiliaria
                    };

                    this.accesoDatos.parametros.Add(this.parametros);

                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppObIn_Direccion",
                        parametroValor = obligacionInmueble.direccion
                    };

                    this.accesoDatos.parametros.Add(this.parametros);

                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppObIn_UsuarioCrea",
                        parametroValor = Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString())
                    };

                    this.accesoDatos.parametros.Add(this.parametros);

                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppObIn_Valor",
                        parametroValor = Convert.ToDouble(obligacionInmueble.valorInmueble)
                    };

                    this.accesoDatos.parametros.Add(this.parametros);

                    OperacionInsertUpdate();
                }
            }

            Session["GarantiaIdonea"] = null;
        }

        /// <summary>
        /// consulta programa y inserta la relacion con los planes de pago
        /// </summary>
        private void InsertarRelacionObligacionCodeudor(int idObligacion)
        {
            this.accesoDatos.procedimiento = "CPP_SP_EliminarObligacionCodeudor";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppObl_Id",
                parametroValor = Convert.ToInt32(idObligacion)
            };

            this.accesoDatos.parametros.Add(this.parametros);

            if (OperacionInsertUpdate())
            {
                var lstObligacionesCodeudor = (List<EntitiesCodeudor>)Session["GarantiaCodeudor"];

                foreach (var obligacionCodeudor in lstObligacionesCodeudor)
                {
                    this.accesoDatos.procedimiento = "CPP_SP_EditarInsertarObligacionCodeudor";
                    this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
                    this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppObl_Id",
                        parametroValor = Convert.ToInt32(idObligacion)
                    };

                    this.accesoDatos.parametros.Add(this.parametros);

                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppBn_Id",
                        parametroValor = Convert.ToInt32(obligacionCodeudor.cppCo_Id)
                    };

                    this.accesoDatos.parametros.Add(this.parametros);

                    OperacionInsertUpdate();
                }
            }

            Session["GarantiaCodeudor"] = null;
        }

        /// <summary>
        /// Consulta para traer la informacion de la obligacion 
        /// </summary>
        private void ConsultarObligacionInmueble()
        {
            this.obligacionBl = new BLObligacion();

            List<EntitiesObligacionInmueble>  lstObligacion = this.obligacionBl.ConsultaObligacionInmueble(this.accesoDatos);

            gvGarantiasIdoneasAsociadas.DataSource = lstObligacion;
            gvGarantiasIdoneasAsociadas.DataBind();
        }

        /// <summary>
        /// Consulta para traer la informacion de la obligacion con el coudedor
        /// </summary>
        private void ConsultarObligacionCodeudor()
        {
            this.codeudorBL = new BLCodeudor();

            List<EntitiesCodeudor> lstCodeudor = this.codeudorBL.ConsultarCodeudor(this.accesoDatos);

            gvGarantiasCodeudorAsociadas.DataSource = lstCodeudor;
            gvGarantiasCodeudorAsociadas.DataBind();
        }

        #endregion


    }
}