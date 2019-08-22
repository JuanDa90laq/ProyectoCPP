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
    using System.Globalization;
    using System.Linq;
    using System.Web.UI.WebControls;
    public partial class CCPBeneficioLey : System.Web.UI.Page
    {
        #region "definicion variables"

        private NumberFormatInfo formato = new NumberFormatInfo();
        private SSOSesion usuarioCpp = new SSOSesion();
        private Validaciones validar = new Validaciones();
        private DLAccesEntities accesoDatos = null;
        private ParametrizacionSPQUERY parametros = null;
        private ResultConsulta resultadoOperacion = null;
        private BLBeneficioLey beneficioBL = null;
        private List<EntitiesCabeceraBeneficioLey> lstBeneficioBl = null;
        private BLProgama programaBL = null;
        private BLPlanPagos planPagosBL = null;
        private BLCalificacion calificacionBL = null;
        private BLBeneficioCapital beneficioCapitalBL = null;

        #endregion

        #region "metodos protegidos"

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.usuarioCpp = (SSOSesion)Session["FINAGRO_SSO_SESION"];
                if (!IsPostBack)
                {
                    this.Inicializadores();
                    this.CargarDepartmentos();
                    this.CargaCombos();
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("Page_Load()", "CPP_SP_ConsultarObligacion", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (Page_Load()) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Page_Load()) :: " + ex.Message);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtCantidadPeriodoMuerto.ClientEnabled = false;
                this.txtCantidadPeriodoGracia.ClientEnabled = false;
                this.txtFechaInicioSeguroVida.ClientEnabled = false;
                this.txtFechaFinSeguroVida.ClientEnabled = false;
                this.txtFechaInicioBeneficioInteres.ClientEnabled = false;
                this.txtFechaFinBeneficioInteres.ClientEnabled = false;
                this.txtCantidadPeriodoMuertoP2.ClientEnabled = false;
                this.txtCantidadPeriodoGraciaP2.ClientEnabled = false;
                this.txtFechaInicioSeguroVidaP2.ClientEnabled = false;
                this.txtFechaFinSeguroVidaP2.ClientEnabled = false;
                this.txtFechaInicioBeneficioInteresP2.ClientEnabled = false;
                this.txtFechaFinBeneficioInteresP2.ClientEnabled = false;
                this.txtFechaInicioOtroBeneficio.ClientEnabled = false;
                this.txtFechaFinOtroBeneficio.ClientEnabled = false;
                this.txtFechaInicioOtrosBeneficiosP2.ClientEnabled = false;
                this.txtFechaFinOtrosBeneficiosP2.ClientEnabled = false;

                TabbedLayoutGroup group = (TabbedLayoutGroup)formLayout.Items[0];
                group.PageControl.ActiveTabIndex = 0;
                group.PageControl.TabPages[2].Enabled = false;
                ASPxEdit.ClearEditorsInContainer(this.pcBeneficio, "0_0", true);
                ASPxEdit.ClearEditorsInContainer(this.pcBeneficio, "0_1", true);
                ASPxEdit.ClearEditorsInContainer(this.pcBeneficio, "0_2", true);
                this.Limpiarobjetos();
                this.pcBeneficio.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnNuevo_Click()", "N/A", "N/A", "Error al ejecutar el proceso (Nuevo) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Nuevo) :: " + ex.Message);
            }
        }

        protected void gvBeneficiosLey_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarBeneficios();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvBeneficiosLey_PageIndexChanged()", "N/A", "N/A", "Error al ejecutar el proceso (Paginación) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Paginación) :: " + ex.Message);
            }
        }

        protected void gvBeneficiosLey_PageSizeChanged(object sender, EventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarBeneficios();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvBeneficiosLey_PageSizeChanged()", "N/A", "N/A", "Error al ejecutar el proceso (Paginación) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ajuste por pagina) :: " + ex.Message);
            }
        }

        protected void gvBeneficiosLey_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarBeneficios();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvBeneficiosLey_BeforeColumnSortingGrouping()", "N/A", "N/A", "Error al ejecutar el proceso (Paginación) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ordenamiento) :: " + ex.Message);
            }
        }

        protected void gvBeneficiosLey_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                switch (e.CommandArgs.CommandName)
                {
                    case "Editar":

                        ASPxEdit.ClearEditorsInContainer(this.pcBeneficio, "0_0", true);
                        ASPxEdit.ClearEditorsInContainer(this.pcBeneficio, "0_1", true);
                        ASPxEdit.ClearEditorsInContainer(this.pcBeneficio, "0_2", true);

                        this.accesoDatos = new DLAccesEntities
                        {
                            procedimiento = "CPP_SP_ConsultarCabeceraBeneficioLey",
                            tipoejecucion = (int)TiposEjecucion.Procedimiento,
                            parametros = new List<ParametrizacionSPQUERY>()
                        };

                        this.parametros = new ParametrizacionSPQUERY
                        {
                            parametro = "@cppCbl_Id",
                            parametroValor = Convert.ToInt32(e.KeyValue.ToString().Split('|')[0])
                        };
                        this.accesoDatos.parametros.Add(this.parametros);

                        this.ConsultarBeneficioEditar();
                        this.validar.RegLog("gvBancosRecaudadores_RowCommand()", "CPP_SP_ConsultarCabeceraBeneficioLey", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                        this.pcBeneficio.ShowOnPageLoad = true;

                        break;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvBeneficiosLey_RowCommand()", "CPP_SP_ConsultarCabeceraBeneficioLey", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (gvBeneficiosLey_RowCommand): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvBeneficiosLey_RowCommand" + "): " + ex.Message);
            }
        }

        protected void Grid_Load(object sender, EventArgs e)
        {
            gvBeneficiosLey.SettingsBehavior.FilterRowMode = GridViewFilterRowMode.Auto;
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarBeneficios();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("Grid_Load()", "N/A", "N/A", "Error al ejecutar el proceso (Paginación) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ajuste por pagina) :: " + ex.Message);
            }
        }

        protected void cbMunci_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                CargaMunicipio(e.Parameter);
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbMunci_Callback()", "N/A", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (cbMunci_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbMunci_Callback): " + ex.Message);
            }
        }

        protected void CargaMunicipio(string idDepartamento)
        {
            try
            {
                if (string.IsNullOrEmpty(idDepartamento)) return;

                this.cbMunicipio.DataSource = ((List<Municipios>)Session["municipios"]).Where(a => a.idDepto.Equals(Convert.ToInt32(idDepartamento)));
                this.cbMunicipio.DataBind();
                this.cbMunicipio.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("CargaMunicipio()", "N/A", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (Llenar Municipios): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Llenar Municipios): " + ex.Message);
            }
        }

        protected void cbPagares_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                TabbedLayoutGroup group = (TabbedLayoutGroup)formLayout.Items[0];

                if (e.Parameter == "2")
                {
                    group.PageControl.TabPages[2].Enabled = true;
                }
                else
                {
                    group.PageControl.TabPages[2].Enabled = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbPagares_Callback()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (cbPagares_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbPagares_Callback): " + ex.Message);
            }
        }

        protected void ASPxCallbackPanelCantidadPeriodoM_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                if (e.Parameter == "2")
                {
                    this.txtCantidadPeriodoMuerto.ClientEnabled = true;
                    this.txtCantidadPeriodoMuerto.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtCantidadPeriodoMuerto.ValidationSettings.ValidationGroup = "0_1";
                    this.txtCantidadPeriodoMuerto.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtCantidadPeriodoMuerto.ValidationSettings.CausesValidation = true;
                    this.txtCantidadPeriodoMuerto.ValidationSettings.ErrorText = "Se requiere la cantidad de los años";
                    this.txtCantidadPeriodoMuerto.ValidationSettings.Display = Display.Dynamic;
                    this.txtCantidadPeriodoMuerto.ClientSideEvents.Validation = "onValidation";

                    this.txtCantidadPeriodoMuerto.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtCantidadPeriodoMuerto.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtCantidadPeriodoMuerto.ClientEnabled = false;
                    this.txtCantidadPeriodoMuerto.ValidationSettings.RequiredField.IsRequired = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("ASPxCallbackPanelCantidadPeriodoM_Callback()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (ASPxCallbackPanelCantidadPeriodoM_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (ASPxCallbackPanelCantidadPeriodoM_Callback): " + ex.Message);
            }
        }

        protected void ASPxCallbackPanelCantidadPeriodoG_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                if (e.Parameter == "2")
                {
                    this.txtCantidadPeriodoGracia.ClientEnabled = true;
                    this.txtCantidadPeriodoGracia.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtCantidadPeriodoGracia.ValidationSettings.ValidationGroup = "0_1";
                    this.txtCantidadPeriodoGracia.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtCantidadPeriodoGracia.ValidationSettings.CausesValidation = true;
                    this.txtCantidadPeriodoGracia.ValidationSettings.ErrorText = "Se requiere la cantidad de los años";
                    this.txtCantidadPeriodoGracia.ValidationSettings.Display = Display.Dynamic;
                    this.txtCantidadPeriodoGracia.ClientSideEvents.Validation = "onValidation";

                    this.txtCantidadPeriodoGracia.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtCantidadPeriodoGracia.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtCantidadPeriodoGracia.ClientEnabled = false;
                    this.txtCantidadPeriodoGracia.ValidationSettings.RequiredField.IsRequired = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("ASPxCallbackPanelCantidadPeriodoG_Callback()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (ASPxCallbackPanelCantidadPeriodoG_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (ASPxCallbackPanelCantidadPeriodoG_Callback): " + ex.Message);
            }

        }

        protected void cbpFechaInicioSeguroV_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                if (e.Parameter == "2")
                {
                    this.txtFechaInicioSeguroVida.ClientEnabled = true;
                    this.txtFechaInicioSeguroVida.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaInicioSeguroVida.ValidationSettings.ValidationGroup = "0_1";
                    this.txtFechaInicioSeguroVida.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaInicioSeguroVida.ValidationSettings.CausesValidation = true;
                    this.txtFechaInicioSeguroVida.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaInicioSeguroVida.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaInicioSeguroVida.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaInicioSeguroVida.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaInicioSeguroVida.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtFechaInicioSeguroVida.ClientEnabled = false;
                    this.txtFechaInicioSeguroVida.ValidationSettings.RequiredField.IsRequired = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbpFechaInicioSeguroV_Callback()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (cbpFechaInicioSeguroV_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbpFechaInicioSeguroV_Callback): " + ex.Message);
            }
        }

        protected void cbpFechaFinSeguroV_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                if (e.Parameter == "2")
                {
                    this.txtFechaFinSeguroVida.ClientEnabled = true;
                    this.txtFechaFinSeguroVida.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaFinSeguroVida.ValidationSettings.ValidationGroup = "0_1";
                    this.txtFechaFinSeguroVida.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaFinSeguroVida.ValidationSettings.CausesValidation = true;
                    this.txtFechaFinSeguroVida.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaFinSeguroVida.ValidationSettings.Display = Display.Dynamic;

                }
                else
                {
                    this.txtFechaFinSeguroVida.ClientEnabled = false;
                    this.txtFechaFinSeguroVida.ValidationSettings.RequiredField.IsRequired = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbpFechaFinSeguroV_Callback()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (cbpFechaFinSeguroV_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbpFechaFinSeguroV_Callback): " + ex.Message);
            }
        }

        protected void cbpFechaInicioBeneficioI_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                if (e.Parameter == "3" || e.Parameter == "4")
                {
                    this.txtFechaInicioBeneficioInteres.ClientEnabled = true;
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.ValidationGroup = "0_1";
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.CausesValidation = true;
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaInicioBeneficioInteres.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaInicioBeneficioInteres.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaInicioBeneficioInteres.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtFechaInicioBeneficioInteres.ClientEnabled = false;
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.RequiredField.IsRequired = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbpFechaInicioBeneficioI_Callback()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (cbpFechaInicioBeneficioI_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbpFechaInicioBeneficioI_Callback): " + ex.Message);
            }
        }

        protected void cbpFechaFinBeneficioI_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                if (e.Parameter == "3" || e.Parameter == "4")
                {
                    this.txtFechaFinBeneficioInteres.ClientEnabled = true;
                    this.txtFechaFinBeneficioInteres.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaFinBeneficioInteres.ValidationSettings.ValidationGroup = "0_1";
                    this.txtFechaFinBeneficioInteres.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaFinBeneficioInteres.ValidationSettings.CausesValidation = true;
                    this.txtFechaFinBeneficioInteres.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaFinBeneficioInteres.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaFinBeneficioInteres.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaFinBeneficioInteres.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaFinBeneficioInteres.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtFechaFinBeneficioInteres.ClientEnabled = false;
                    this.txtFechaFinBeneficioInteres.ValidationSettings.RequiredField.IsRequired = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbpFechaFinBeneficioI_Callback()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (cbpFechaFinBeneficioI_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbpFechaFinBeneficioI_Callback): " + ex.Message);
            }
        }

        protected void cbpPeriodoMuertoP2_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                if (e.Parameter == "2")
                {
                    this.txtCantidadPeriodoMuertoP2.ClientEnabled = true;
                    this.txtCantidadPeriodoMuertoP2.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtCantidadPeriodoMuertoP2.ValidationSettings.ValidationGroup = "0_2";
                    this.txtCantidadPeriodoMuertoP2.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtCantidadPeriodoMuertoP2.ValidationSettings.CausesValidation = true;
                    this.txtCantidadPeriodoMuertoP2.ValidationSettings.ErrorText = "Se requiere la cantidad de los años";
                    this.txtCantidadPeriodoMuertoP2.ValidationSettings.Display = Display.Dynamic;
                    this.txtCantidadPeriodoMuertoP2.ClientSideEvents.Validation = "onValidation";

                    this.txtCantidadPeriodoMuertoP2.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtCantidadPeriodoMuertoP2.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtCantidadPeriodoMuertoP2.ClientEnabled = false;
                    this.txtCantidadPeriodoMuertoP2.ValidationSettings.RequiredField.IsRequired = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbpPeriodoMuertoP2_Callback()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (cbpPeriodoMuertoP2_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbpPeriodoMuertoP2_Callback): " + ex.Message);
            }
        }

        protected void cbpPeriodoGraciaP2_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                if (e.Parameter == "2")
                {
                    this.txtCantidadPeriodoGraciaP2.ClientEnabled = true;
                    this.txtCantidadPeriodoGraciaP2.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtCantidadPeriodoGraciaP2.ValidationSettings.ValidationGroup = "0_2";
                    this.txtCantidadPeriodoGraciaP2.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtCantidadPeriodoGraciaP2.ValidationSettings.CausesValidation = true;
                    this.txtCantidadPeriodoGraciaP2.ValidationSettings.ErrorText = "Se requiere la cantidad de los años";
                    this.txtCantidadPeriodoGraciaP2.ValidationSettings.Display = Display.Dynamic;
                    this.txtCantidadPeriodoGraciaP2.ClientSideEvents.Validation = "onValidation";

                    this.txtCantidadPeriodoGraciaP2.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtCantidadPeriodoGraciaP2.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtCantidadPeriodoGraciaP2.ClientEnabled = false;
                    this.txtCantidadPeriodoGraciaP2.ValidationSettings.RequiredField.IsRequired = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbpPeriodoGraciaP2_Callback()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (cbpPeriodoGraciaP2_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbpPeriodoGraciaP2_Callback): " + ex.Message);
            }
        }

        protected void cbpFechaInicioSeguroVP2_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                if (e.Parameter == "2")
                {
                    this.txtFechaInicioSeguroVidaP2.ClientEnabled = true;
                    this.txtFechaInicioSeguroVidaP2.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaInicioSeguroVidaP2.ValidationSettings.ValidationGroup = "0_2";
                    this.txtFechaInicioSeguroVidaP2.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaInicioSeguroVidaP2.ValidationSettings.CausesValidation = true;
                    this.txtFechaInicioSeguroVidaP2.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaInicioSeguroVidaP2.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaInicioSeguroVidaP2.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaInicioSeguroVidaP2.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaInicioSeguroVidaP2.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtFechaInicioSeguroVidaP2.ClientEnabled = false;
                    this.txtFechaInicioSeguroVidaP2.ValidationSettings.RequiredField.IsRequired = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbpFechaInicioSeguroVP2_Callback()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (cbpFechaInicioSeguroVP2_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbpFechaInicioSeguroVP2_Callback): " + ex.Message);
            }
        }

        protected void cbpFechaFinSeguroVP2_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                if (e.Parameter == "2")
                {
                    this.txtFechaFinSeguroVidaP2.ClientEnabled = true;
                    this.txtFechaFinSeguroVidaP2.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaFinSeguroVidaP2.ValidationSettings.ValidationGroup = "0_2";
                    this.txtFechaFinSeguroVidaP2.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaFinSeguroVidaP2.ValidationSettings.CausesValidation = true;
                    this.txtFechaFinSeguroVidaP2.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaFinSeguroVidaP2.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaFinSeguroVidaP2.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaFinSeguroVidaP2.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaFinSeguroVidaP2.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtFechaFinSeguroVidaP2.ClientEnabled = false;
                    this.txtFechaFinSeguroVidaP2.ValidationSettings.RequiredField.IsRequired = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbpFechaFinSeguroVP2_Callback()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (cbpFechaFinSeguroVP2_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbpFechaFinSeguroVP2_Callback): " + ex.Message);
            }
        }

        protected void cbpFechaInicioBeneficioIP2_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                if (e.Parameter == "3" || e.Parameter == "4")
                {
                    this.txtFechaInicioBeneficioInteresP2.ClientEnabled = true;
                    this.txtFechaInicioBeneficioInteresP2.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaInicioBeneficioInteresP2.ValidationSettings.ValidationGroup = "0_2";
                    this.txtFechaInicioBeneficioInteresP2.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaInicioBeneficioInteresP2.ValidationSettings.CausesValidation = true;
                    this.txtFechaInicioBeneficioInteresP2.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaInicioBeneficioInteresP2.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaInicioBeneficioInteresP2.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaInicioBeneficioInteresP2.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaInicioBeneficioInteresP2.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtFechaInicioBeneficioInteresP2.ClientEnabled = false;
                    this.txtFechaInicioBeneficioInteresP2.ValidationSettings.RequiredField.IsRequired = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbpFechaInicioBeneficioIP2_Callback()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (cbpFechaInicioBeneficioIP2_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbpFechaInicioBeneficioIP2_Callback): " + ex.Message);
            }
        }

        protected void cbpFechaFinBeneficioIP2_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                if (e.Parameter == "3" || e.Parameter == "4")
                {
                    this.txtFechaFinBeneficioInteresP2.ClientEnabled = true;
                    this.txtFechaFinBeneficioInteresP2.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaFinBeneficioInteresP2.ValidationSettings.ValidationGroup = "0_2";
                    this.txtFechaFinBeneficioInteresP2.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaFinBeneficioInteresP2.ValidationSettings.CausesValidation = true;
                    this.txtFechaFinBeneficioInteresP2.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaFinBeneficioInteresP2.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaFinBeneficioInteresP2.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaFinBeneficioInteresP2.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaFinBeneficioInteresP2.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtFechaFinBeneficioInteresP2.ClientEnabled = false;
                    this.txtFechaFinBeneficioInteresP2.ValidationSettings.RequiredField.IsRequired = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbpFechaFinBeneficioIP2_Callback()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (cbpFechaFinBeneficioIP2_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbpFechaFinBeneficioIP2_Callback): " + ex.Message);
            }
        }

        protected void cbpFechaInicioOtroB_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                if (e.Parameter == "2")
                {
                    this.txtFechaInicioOtroBeneficio.ClientEnabled = true;
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.ValidationGroup = "0_1";
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.CausesValidation = true;
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaInicioOtroBeneficio.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaInicioOtroBeneficio.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaInicioOtroBeneficio.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtFechaInicioOtroBeneficio.ClientEnabled = false;
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.RequiredField.IsRequired = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbpFechaInicioOtroB_Callback()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (cbpFechaInicioOtroB_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbpFechaInicioOtroB_Callback): " + ex.Message);
            }
        }

        protected void cbpFechaFinOtroBeneficio_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                if (e.Parameter == "2")
                {
                    this.txtFechaFinOtroBeneficio.ClientEnabled = true;
                    this.txtFechaFinOtroBeneficio.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaFinOtroBeneficio.ValidationSettings.ValidationGroup = "0_1";
                    this.txtFechaFinOtroBeneficio.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaFinOtroBeneficio.ValidationSettings.CausesValidation = true;
                    this.txtFechaFinOtroBeneficio.ValidationSettings.ErrorText = "Se requiere la fecha final";
                    this.txtFechaFinOtroBeneficio.ValidationSettings.Display = Display.Dynamic;

                    this.txtFechaFinOtroBeneficio.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaFinOtroBeneficio.ValidationSettings.ValidationGroup;
                    };

                }
                else
                {
                    this.txtFechaFinOtroBeneficio.ClientEnabled = false;
                    this.txtFechaFinOtroBeneficio.ValidationSettings.RequiredField.IsRequired = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbpFechaFinOtroBeneficio_Callback()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (cbpFechaFinOtroBeneficio_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbpFechaFinOtroBeneficio_Callback): " + ex.Message);
            }
        }

        protected void cbpFechaInicioOtrosBeneficiosP2_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                if (e.Parameter == "2")
                {
                    this.txtFechaInicioOtrosBeneficiosP2.ClientEnabled = true;
                    this.txtFechaInicioOtrosBeneficiosP2.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaInicioOtrosBeneficiosP2.ValidationSettings.ValidationGroup = "0_2";
                    this.txtFechaInicioOtrosBeneficiosP2.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaInicioOtrosBeneficiosP2.ValidationSettings.CausesValidation = true;
                    this.txtFechaInicioOtrosBeneficiosP2.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaInicioOtrosBeneficiosP2.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaInicioOtrosBeneficiosP2.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaInicioOtrosBeneficiosP2.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaInicioOtrosBeneficiosP2.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtFechaInicioOtrosBeneficiosP2.ClientEnabled = false;
                    this.txtFechaInicioOtrosBeneficiosP2.ValidationSettings.RequiredField.IsRequired = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbpFechaInicioOtrosBeneficiosP2_Callback()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (cbpFechaInicioOtrosBeneficiosP2_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbpFechaInicioOtrosBeneficiosP2_Callback): " + ex.Message);
            }
        }

        protected void cbpFechaFinOtrosBeneficiosP2_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                if (e.Parameter == "2")
                {
                    this.txtFechaFinOtrosBeneficiosP2.ClientEnabled = true;
                    this.txtFechaFinOtrosBeneficiosP2.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaFinOtrosBeneficiosP2.ValidationSettings.ValidationGroup = "0_2";
                    this.txtFechaFinOtrosBeneficiosP2.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaFinOtrosBeneficiosP2.ValidationSettings.CausesValidation = true;
                    this.txtFechaFinOtrosBeneficiosP2.ValidationSettings.ErrorText = "Se requiere la fecha final";
                    this.txtFechaFinOtrosBeneficiosP2.ValidationSettings.Display = Display.Dynamic;

                    this.txtFechaFinOtrosBeneficiosP2.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaFinOtrosBeneficiosP2.ValidationSettings.ValidationGroup;
                    };

                }
                else
                {
                    this.txtFechaFinOtrosBeneficiosP2.ClientEnabled = false;
                    this.txtFechaFinOtrosBeneficiosP2.ValidationSettings.RequiredField.IsRequired = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbpFechaFinOtrosBeneficiosP2_Callback()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (cbpFechaFinOtrosBeneficiosP2_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbpFechaFinOtrosBeneficiosP2_Callback): " + ex.Message);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var pagares = this.ValidarPagares();
                if (pagares.Count >= 1)
                {
                    this.EjecucionSQLEditInsert(pagares);
                    this.validar.RegLog("btnGuardar_Click()", "CPP_SP_EditarInsertarObligaciones", TipoAccionLog.insercion.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                }
                
            }
            catch (Exception ex)
            {
                if (cbPagares.SelectedItem.Value.ToString() == "1")
                {
                    TabbedLayoutGroup group = (TabbedLayoutGroup)formLayout.Items[0];
                    group.PageControl.TabPages[2].Enabled = false;
                }

                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnGuardar_Click()", "CPP_SP_EditarInsertarObligaciones", TipoAccionLog.insercion.ToString(), "Error al ejecutar el proceso (btnGuardar_Click): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (btnAceptar_Click) :: " + ex.Message);
            }
        }

        protected void ASPxComboBox_CustomJSProperties(object sender, DevExpress.Web.CustomJSPropertiesEventArgs e)
        {
            ASPxComboBox tb = sender as ASPxComboBox;
            e.Properties["cpTab"] = tb.ValidationSettings.ValidationGroup;
        }

        protected void ASPxButtonEdit_CustomJSProperties(object sender, DevExpress.Web.CustomJSPropertiesEventArgs e)
        {
            ASPxButtonEdit tb = sender as ASPxButtonEdit;
            e.Properties["cpTab"] = tb.ValidationSettings.ValidationGroup;
        }

        protected void ASPxDateEdit_CustomJSProperties(object sender, DevExpress.Web.CustomJSPropertiesEventArgs e)
        {
            ASPxDateEdit tb = sender as ASPxDateEdit;
            e.Properties["cpTab"] = tb.ValidationSettings.ValidationGroup;
        }

        #endregion

        #region "metodos privados"

        /// <summary>
        /// configura la cosulta
        /// </summary>
        private void ConfigurarConsulta()
        {
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarCabeceraBeneficioLey",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };
        }

        /// <summary>
        /// consulta codigos cuentas contables
        /// </summary>
        private void ConsultarBeneficios()
        {
            this.beneficioBL = new BLBeneficioLey();
            this.lstBeneficioBl = new List<EntitiesCabeceraBeneficioLey>();
            this.lstBeneficioBl = this.beneficioBL.ConsultaCabeceraBeneficioLey(this.accesoDatos);

            if (this.lstBeneficioBl.Count > 0)
            {
                foreach (EntitiesCabeceraBeneficioLey beneficio in this.lstBeneficioBl)
                {
                    beneficio.departamento = ((List<Depatamentos>)Session["Depto"]).Where(a => a.id.Equals(beneficio.idDepartamento)).Select(a => a.depratamento).FirstOrDefault();
                    beneficio.municipio = ((List<Municipios>)Session["municipios"]).Where(a => a.id.Equals(beneficio.idMunicipio) && a.idDepto.Equals(beneficio.idDepartamento)).Select(a => a.municipio).FirstOrDefault();
                    beneficio.actividadAgropecuaria = ((List<EntitiesActividadesAgropecuarias>)Session["Actividades"]).Where(a => a.id.Equals(beneficio.idActividadAgropecuaria)).Select(a => a.actividad).FirstOrDefault();
                }
                this.gvBeneficiosLey.DataSource = this.lstBeneficioBl;
                this.gvBeneficiosLey.DataBind();
            }
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
        /// carga los combos del formulario
        /// </summary>
        private void CargaCombos()
        {
            this.programaBL = new BLProgama();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarProgramas",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.cbPrograma.DataSource = this.programaBL.ConsultarProgramas(this.accesoDatos);
            this.cbPrograma.DataBind();

            this.programaBL = new BLProgama();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarCantidadPagares",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.cbPagares.DataSource = this.programaBL.ConsultarCantidadPagares(this.accesoDatos);
            this.cbPagares.DataBind();

            this.beneficioBL = new BLBeneficioLey();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarTipoBeneficiado",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.cbTipoBeneficio.DataSource = this.beneficioBL.ConsultaTipoBeneficiado(this.accesoDatos);
            this.cbTipoBeneficio.DataBind();

            this.beneficioBL = new BLBeneficioLey();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarTasaMora",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            var tasaMora = this.beneficioBL.ConsultaTasaMora(this.accesoDatos);

            this.cbTasaMora.DataSource = tasaMora;
            this.cbTasaMora.DataBind();

            this.cbTasaMoraP2.DataSource = tasaMora;
            this.cbTasaMoraP2.DataBind();

            this.beneficioBL = new BLBeneficioLey();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarPlazoObligacion",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            var plazoObligacion = this.beneficioBL.ConsultaPlazoObligacion(this.accesoDatos);

            this.cbPlazoObligacion.DataSource = plazoObligacion;
            this.cbPlazoObligacion.DataBind();

            this.cbPlazoObligacionP2.DataSource = plazoObligacion;
            this.cbPlazoObligacionP2.DataBind();

            this.planPagosBL = new BLPlanPagos();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarPeriocidadIntereses",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            var periocidadIntereses = this.planPagosBL.ConsultarPeriocidadInteresesCorrientes(this.accesoDatos);
            this.cbPeriocidadIntereses.DataSource = periocidadIntereses;
            this.cbPeriocidadIntereses.DataBind();

            this.cbPeriocidadInteresesP2.DataSource = periocidadIntereses;
            this.cbPeriocidadInteresesP2.DataBind();

            this.beneficioBL = new BLBeneficioLey();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarPeriodoMuerto",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            var periodoMuertos = this.beneficioBL.ConsultaPeriodoMuerto(this.accesoDatos);

            this.cbPeriodoMuerto.DataSource = periodoMuertos;
            this.cbPeriodoMuerto.DataBind();

            this.cbPeriodoMuertoP2.DataSource = periodoMuertos;
            this.cbPeriodoMuertoP2.DataBind();

            this.calificacionBL = new BLCalificacion();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarCalificaciones",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            var calificacion = this.calificacionBL.ConsultaCalificacion(this.accesoDatos);

            this.cbCalificacion.DataSource = calificacion;
            this.cbCalificacion.DataBind();

            this.cbCalificacionP2.DataSource = calificacion;
            this.cbCalificacionP2.DataBind();

            this.beneficioBL = new BLBeneficioLey();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarPeriodoGracia",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            var periodoGracia = this.beneficioBL.ConsultaPeriodoGracia(this.accesoDatos);

            this.cbPeriodoGracia.DataSource = periodoGracia;
            this.cbPeriodoGracia.DataBind();

            this.cbPeriodoGraciaP2.DataSource = periodoGracia;
            this.cbPeriodoGraciaP2.DataBind();

            this.beneficioCapitalBL = new BLBeneficioCapital();

            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarBeneficioCapital",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            var Conceptos = this.beneficioCapitalBL.ConcultarbeneficiosCapital(this.accesoDatos);

            this.cbBeneficioCapital.DataSource = Conceptos;
            this.cbBeneficioCapital.DataBind();

            this.cbBeneficioCapitalP2.DataSource = Conceptos;
            this.cbBeneficioCapitalP2.DataBind();

            this.beneficioBL = new BLBeneficioLey();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarBeneficiosSeguroVida",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            var seguroVida = this.beneficioBL.ConsultaBeneficiosSeguroVida(this.accesoDatos);

            this.cbBeneficioSeguroVida.DataSource = seguroVida;
            this.cbBeneficioSeguroVida.DataBind();

            this.cbBeneficioSeguroVidaP2.DataSource = seguroVida;
            this.cbBeneficioSeguroVidaP2.DataBind();

            this.beneficioBL = new BLBeneficioLey();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarBeneficiosInteres",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            var interes = this.beneficioBL.ConsultaBeneficiosInteres(this.accesoDatos);

            this.cbBeneficioInteres.DataSource = interes;
            this.cbBeneficioInteres.DataBind();

            this.cbBeneficioInteresP2.DataSource = interes;
            this.cbBeneficioInteresP2.DataBind();

            this.beneficioBL = new BLBeneficioLey();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarCapitalizacionIntereses",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            var capitalizacionIntereses = this.beneficioBL.ConsultaCapitalizacionIntereses(this.accesoDatos);

            this.cbCapitalizacionIntereses.DataSource = capitalizacionIntereses;
            this.cbCapitalizacionIntereses.DataBind();

            this.cbCapitalizacionInteresesP2.DataSource = capitalizacionIntereses;
            this.cbCapitalizacionInteresesP2.DataBind();

            this.beneficioBL = new BLBeneficioLey();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarTasaInteres",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            var tasaIntereses = this.beneficioBL.ConsultaTasaInteres(this.accesoDatos);

            tasaIntereses.AddRange(new List<EntitiesTasaIntereses> {
                new EntitiesTasaIntereses() { id = 96, descripcion ="IPC + Puntos IPC"},
                new EntitiesTasaIntereses() { id = 97, descripcion ="DTF + Puntos IPC"},
                new EntitiesTasaIntereses() { id = 98, descripcion ="IBR (Overnight, mensual, trimestral, semestral) + Puntos IPC"},
                new EntitiesTasaIntereses() { id = 99, descripcion = "N/A" }
            });

            tasaIntereses.OrderBy(x => x.id).ToList();

            this.cbTasaInteres.DataSource = tasaIntereses;
            this.cbTasaInteres.DataBind();

            this.cbTasaInteresP2.DataSource = tasaIntereses;
            this.cbTasaInteresP2.DataBind();

            this.beneficioBL = new BLBeneficioLey();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarOtrosBeneficios",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            var otrosBeneficios = this.beneficioBL.ConsultaOtrosBeneficios(this.accesoDatos);

            this.cbOtrosBeneficios.DataSource = otrosBeneficios;
            this.cbOtrosBeneficios.DataBind();

            this.cbOtrosBeneficiosP2.DataSource = otrosBeneficios;
            this.cbOtrosBeneficiosP2.DataBind();

            Session["Actividades"] = this.validar.CargarActividades();
            
            this.cbActividadAgropecuaria.DataSource = Session["Actividades"];
            this.cbActividadAgropecuaria.DataBind();
        }

        /// <summary>
        /// consulta los departamentos y carga los combos deptos
        /// </summary>
        private void CargarDepartmentos()
        {
            Session["Depto"] = this.validar.CargarDepartamentos();

            this.cbDepartamento.DataSource = Session["Depto"];
            this.cbDepartamento.DataBind();

            Session["municipios"] = this.validar.CargarMunicipios(0);
        }

        /// <summary>
        /// Formato a los campos
        /// </summary>
        private void Inicializadores()
        {
            this.txtMontoMaximo.Attributes.Add("onKeyPress", "return soloNumeros(event)");
            this.txtMontoMaximo.Attributes.Add("onpaste", "return false");

            this.txtCantidadAnios.Attributes.Add("onKeyPress", "return soloNumeros(event)");
            this.txtCantidadAnios.Attributes.Add("onpaste", "return false");
            this.txtCantidadAniosP2.Attributes.Add("onKeyPress", "return soloNumeros(event)");
            this.txtCantidadAniosP2.Attributes.Add("onpaste", "return false");

            this.txtCantidadPeriodoMuerto.Attributes.Add("onKeyPress", "return soloNumeros(event)");
            this.txtCantidadPeriodoMuerto.Attributes.Add("onpaste", "return false");
            this.txtCantidadPeriodoMuertoP2.Attributes.Add("onKeyPress", "return soloNumeros(event)");
            this.txtCantidadPeriodoMuertoP2.Attributes.Add("onpaste", "return false");

            this.txtCantidadPeriodoGracia.Attributes.Add("onKeyPress", "return soloNumeros(event)");
            this.txtCantidadPeriodoGracia.Attributes.Add("onpaste", "return false");
            this.txtCantidadPeriodoGraciaP2.Attributes.Add("onKeyPress", "return soloNumeros(event)");
            this.txtCantidadPeriodoGraciaP2.Attributes.Add("onpaste", "return false");

        }

        private void Limpiarobjetos()
        {
            //Cabecera del pagare
            this.hdIdBeneficio.Value = string.Empty;
            this.txtCodigoBeneficio.Value = string.Empty;
            this.cbPrograma.SelectedIndex = -1;
            this.txtNombreBeneficio.Value = string.Empty;
            this.cbDepartamento.SelectedIndex = -1;
            this.cbMunicipio.SelectedIndex = -1;
            this.txtFechaInicial.Value = string.Empty;
            this.txtFechaFinal.Value = string.Empty;
            this.cbPagares.SelectedIndex = -1;
            this.txtMontoMaximo.Value = string.Empty;
            this.cbTipoBeneficio.SelectedIndex = -1;
            this.cbActividadAgropecuaria.SelectedIndex = -1;

            //Pagare #1
            this.hdIdPagare1.Value = string.Empty;
            this.cbTasaInteres.SelectedIndex = -1;
            this.txtPuntosIPC.Value = string.Empty;
            this.cbTasaMora.SelectedIndex = -1;
            this.cbPlazoObligacion.SelectedIndex = -1;
            this.txtCantidadAnios.Value = string.Empty;
            this.txtPorcentajeDistribucionP1.Value = string.Empty;
            this.txtPorcentajeDistribucionP1P2.Value = string.Empty;
            this.cbPeriocidadIntereses.SelectedIndex = -1;
            this.cbPeriodoMuerto.SelectedIndex = -1;
            this.txtCantidadPeriodoMuerto.Value = string.Empty;
            this.cbCalificacion.SelectedIndex = -1;
            this.cbPeriodoGracia.SelectedIndex = -1;
            this.txtCantidadPeriodoGracia.Value = string.Empty;
            this.cbBeneficioCapital.SelectedIndex = -1;
            this.cbBeneficioSeguroVida.SelectedIndex = -1;
            this.txtFechaInicioSeguroVida.Value = string.Empty;
            this.txtFechaFinSeguroVida.Value = string.Empty;
            this.cbBeneficioInteres.SelectedIndex = -1;
            this.txtFechaInicioBeneficioInteres.Value = string.Empty;
            this.txtFechaFinBeneficioInteres.Value = string.Empty;
            this.cbCapitalizacionIntereses.SelectedIndex = -1;

            //Pagare #2
            this.hdIdPagare2.Value = string.Empty;
            this.cbTasaInteresP2.SelectedIndex = -1;
            this.txtPuntosIPCP2.Value = string.Empty;
            this.cbTasaMoraP2.SelectedIndex = -1;
            this.cbPlazoObligacionP2.SelectedIndex = -1;
            this.txtCantidadAniosP2.Value = string.Empty;
            this.txtPorcentajeDistribucionP2.Value = string.Empty;
            this.txtPorcentajeDistribucionP2P2.Value = string.Empty;
            this.cbPeriocidadInteresesP2.SelectedIndex = -1;
            this.cbPeriodoMuertoP2.SelectedIndex = -1;
            this.txtCantidadPeriodoMuertoP2.Value = string.Empty;
            this.cbCalificacionP2.SelectedIndex = -1;
            this.cbPeriodoGraciaP2.SelectedIndex = -1;
            this.txtCantidadPeriodoGraciaP2.Value = string.Empty;
            this.cbBeneficioCapitalP2.SelectedIndex = -1;
            this.cbBeneficioSeguroVidaP2.SelectedIndex = -1;
            this.txtFechaInicioSeguroVidaP2.Value = string.Empty;
            this.txtFechaFinSeguroVidaP2.Value = string.Empty;
            this.cbBeneficioInteresP2.SelectedIndex = -1;
            this.txtFechaInicioBeneficioInteresP2.Value = string.Empty;
            this.txtFechaFinBeneficioInteresP2.Value = string.Empty;
            this.cbCapitalizacionInteresesP2.SelectedIndex = -1;
        }

        /// <summary>
        /// se invoca para realizar peticiones a la base de datos (Edicion inserción)
        /// </summary>
        private void EjecucionSQLEditInsert(List<EntitiesPagareBeneficioLey> listPagares)
        {
            this.accesoDatos = new DLAccesEntities();
            this.beneficioBL = new BLBeneficioLey();
            this.parametros = new ParametrizacionSPQUERY();

            this.accesoDatos.procedimiento = "CPP_SP_EditarInsertarCabeceraBeneficioLey";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

            if (!string.IsNullOrEmpty(this.hdIdBeneficio.Value))
            {
                this.parametros.parametro = "@cppCbl_Id";
                this.parametros.parametroValor = Convert.ToInt32(this.hdIdBeneficio.Value.Trim());
                this.accesoDatos.parametros.Add(this.parametros);
            }

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppCbl_Nombre",
                parametroValor = Convert.ToString(this.txtNombreBeneficio.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppCbl_IdPrograma",
                parametroValor = Convert.ToInt32(this.cbPrograma.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppCbl_IdDepartamento",
                parametroValor = Convert.ToInt32(this.cbDepartamento.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppCbl_IdMunicipio",
                parametroValor = this.cbMunicipio.SelectedItem == null ? this.cbMunicipio.ClientValue : Convert.ToInt32(this.cbMunicipio.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppCbl_FechaInicial",
                parametroValor = Convert.ToDateTime(this.txtFechaInicial.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppCbl_FechaFinal",
                parametroValor = Convert.ToDateTime(this.txtFechaFinal.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppCbl_IdCantidadPagares",
                parametroValor = Convert.ToInt32(this.cbPagares.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppCbl_TopeMaximo",
                parametroValor = Convert.ToInt32(this.txtMontoMaximo.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppCbl_IdTipoBeneficiado",
                parametroValor = Convert.ToInt32(this.cbTipoBeneficio.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppCbl_IdActividadAgropecuaria",
                parametroValor = Convert.ToInt32(this.cbActividadAgropecuaria.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = string.IsNullOrEmpty(this.hdIdBeneficio.Value) ? "@cppCbl_UsuarioCrea" : "@cppCbl_UsuarioModifica",
                parametroValor = Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@accion",
                parametroValor = string.IsNullOrEmpty(this.hdIdBeneficio.Value) ? Convert.ToBoolean("true") : Convert.ToBoolean("false")
            };
            this.accesoDatos.parametros.Add(this.parametros);

            if (this.OperacionEditInsert())
            {
                this.ConfigurarConsulta();

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppCbl_Nombre",
                    parametroValor = Convert.ToString(this.txtNombreBeneficio.Value.ToString().Trim())
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.beneficioBL = new BLBeneficioLey();
                this.lstBeneficioBl = new List<EntitiesCabeceraBeneficioLey>();
                this.hdIdBeneficio.Value = string.IsNullOrEmpty(hdIdBeneficio.Value) ? this.beneficioBL.ConsultaCabeceraBeneficioLey(this.accesoDatos).FirstOrDefault().id.ToString() : hdIdBeneficio.Value;

                foreach (var pagare in listPagares)
                {
                    pagare.idCabecera = Convert.ToInt32(this.hdIdBeneficio.Value);
                }

                EjecucionSQLEditInsertPagares(listPagares);

                this.ConfigurarConsulta();

                this.ConsultarBeneficios();

                this.pcBeneficio.ShowOnPageLoad = false;
            }
            else
            {
                TabbedLayoutGroup group = (TabbedLayoutGroup)formLayout.Items[0];

                if (cbPagares.SelectedItem.Value.ToString() == "2")
                {
                    group.PageControl.TabPages[2].Enabled = true;
                }
                else
                {
                    group.PageControl.TabPages[2].Enabled = false;
                }
            }
        }

        private void EjecucionSQLEditInsertPagares(List<EntitiesPagareBeneficioLey> listPagares)
        {
            foreach (var pagare in listPagares)
            {
                this.accesoDatos = new DLAccesEntities();
                this.beneficioBL = new BLBeneficioLey();
                this.parametros = new ParametrizacionSPQUERY();

                this.accesoDatos.procedimiento = "CPP_SP_EditarInsertarPagareBeneficioLey";
                this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
                this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();


                if (pagare.id != 0)
                {
                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppPbl_Id",
                        parametroValor = Convert.ToInt32(pagare.id)
                    };
                    this.accesoDatos.parametros.Add(this.parametros);
                }

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPbl_IdCabeceraBeneficioLey",
                    parametroValor = Convert.ToInt32(pagare.idCabecera)
                };  
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPbl_IdTasaInteres",
                    parametroValor = Convert.ToInt32(pagare.idTasaInteresCorriente)
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPbl_PuntosAdicionales",
                    parametroValor = Convert.ToDouble(pagare.puntosAdicionales)
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPbl_IdTasaMora",
                    parametroValor = Convert.ToInt32(pagare.idTasaMora)
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPbl_IdPlazoObligacion",
                    parametroValor = Convert.ToInt32(pagare.idPlazoObligacion)
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPbl_CantidadAniosPlazo",
                    parametroValor = Convert.ToInt32(pagare.cantidadAnios)
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPbl_PorcentajeP1",
                    parametroValor = Convert.ToDouble(pagare.PorcentajeP1)
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPbl_PorcentajeP2",
                    parametroValor = Convert.ToDouble(pagare.PorcentajeP2)
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPbl_IdPeriocidadIntereses",
                    parametroValor = Convert.ToInt32(pagare.idPeriocidadIntereses)
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPbl_IdPeriodoMuerto",
                    parametroValor = Convert.ToInt32(pagare.idPeriodoMuerto)
                };
                this.accesoDatos.parametros.Add(this.parametros);

                if (pagare.cantidadAñosPeriodoMuerto.HasValue)
                {
                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppPbl_CantidadAniosPeriodoMuerto",
                        parametroValor = Convert.ToInt32(pagare.cantidadAñosPeriodoMuerto)
                    };
                    this.accesoDatos.parametros.Add(this.parametros);
                }

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPbl_IdPeriodoGracia",
                    parametroValor = Convert.ToInt32(pagare.idPeriodogracia)
                };
                this.accesoDatos.parametros.Add(this.parametros);

                if (pagare.cantidadAñosPeriodoGracia.HasValue)
                {
                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppPbl_CantidadAniosPeriodoGracia",
                        parametroValor = Convert.ToInt32(pagare.cantidadAñosPeriodoGracia)
                    };
                    this.accesoDatos.parametros.Add(this.parametros);
                }

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPbl_IdBeneficioCapital",
                    parametroValor = Convert.ToInt32(pagare.idBeneficioCapital)
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPbl_IdBeneficiosInteres",
                    parametroValor = Convert.ToInt32(pagare.idBeneficioInteres)
                };
                this.accesoDatos.parametros.Add(this.parametros);

                if (pagare.fechaInicioInteres.HasValue)
                {
                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppPbl_FechaInicioBeneficioInteres",
                        parametroValor = Convert.ToDateTime(pagare.fechaInicioInteres)
                    };
                    this.accesoDatos.parametros.Add(this.parametros);
                }

                if (pagare.fechaFinInteres.HasValue)
                {
                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppPbl_FechaFinBeneficioInteres",
                        parametroValor = Convert.ToDateTime(pagare.fechaFinInteres)
                    };
                    this.accesoDatos.parametros.Add(this.parametros);
                }

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPbl_IdBeneficioSeguroVida",
                    parametroValor = Convert.ToInt32(pagare.idBeneficioSeguroVida)
                };
                this.accesoDatos.parametros.Add(this.parametros);

                if (pagare.fechaInicioSeguroVida.HasValue)
                {
                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppPbl_FechaInicioSeguroVida",
                        parametroValor = Convert.ToDateTime(pagare.fechaInicioSeguroVida)
                    };
                    this.accesoDatos.parametros.Add(this.parametros);
                }

                if (pagare.fechaFinSeguroVida.HasValue)
                {
                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppPbl_FechaFinSeguroVida",
                        parametroValor = Convert.ToDateTime(pagare.fechaFinSeguroVida)
                    };
                    this.accesoDatos.parametros.Add(this.parametros);
                }

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPbl_IdCalificacionCartera",
                    parametroValor = Convert.ToInt32(pagare.idCalificacionCartera)
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPbl_IdCapitalizacionIntereses",
                    parametroValor = Convert.ToInt32(pagare.idCapitalizacionIntereses)
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPbl_IdOtrosBeneficios",
                    parametroValor = Convert.ToInt32(pagare.idOtrosBeneficios)
                };
                this.accesoDatos.parametros.Add(this.parametros);

                if (pagare.fechaInicioOtrosBeneficios.HasValue)
                {
                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppPbl_FechaInicioOtrosBeneficios",
                        parametroValor = Convert.ToDateTime(pagare.fechaInicioOtrosBeneficios)
                    };
                    this.accesoDatos.parametros.Add(this.parametros);
                }

                if (pagare.fechaFinOtrosBeneficios.HasValue)
                {
                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppPbl_FechaFinOtrosBeneficios",
                        parametroValor = Convert.ToDateTime(pagare.fechaFinOtrosBeneficios)
                    };
                    this.accesoDatos.parametros.Add(this.parametros);
                }

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = pagare.id == 0 ? "@cppPbl_UsuarioCrea" : "@cppPbl_UsuarioModifica",
                    parametroValor = Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString())
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@accion",
                    parametroValor = pagare.id == 0 ?  Convert.ToBoolean("true") : Convert.ToBoolean("false")
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.OperacionEditInsert();
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
            this.beneficioBL = new BLBeneficioLey();
            this.resultadoOperacion = this.beneficioBL.RegistrarEditarPagares(this.accesoDatos);

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
        /// Se invoca para validar que los campos sean correctos en los pagares
        /// </summary>
        private List<EntitiesPagareBeneficioLey> ValidarPagares()
        {
            var Pagares = new List<EntitiesPagareBeneficioLey>();

            if (this.cbPagares.SelectedItem.Value.ToString().Trim() == "2")
            {
                var pagare1 = new EntitiesPagareBeneficioLey();

                if (!string.IsNullOrEmpty(hdIdPagare1.Value))
                {
                    pagare1.id = Convert.ToInt32(hdIdPagare1.Value);
                }

                if (!string.IsNullOrEmpty(hdIdBeneficio.Value))
                {
                    pagare1.idCabecera = Convert.ToInt32(hdIdBeneficio.Value);
                }
                
                pagare1.idTasaInteresCorriente = Convert.ToInt32(cbTasaInteres.SelectedItem.Value.ToString().Trim());

                if (cbTasaInteres.SelectedItem.Value.ToString().Trim() == "96" || cbTasaInteres.SelectedItem.Value.ToString().Trim() == "97"
                    || cbTasaInteres.SelectedItem.Value.ToString().Trim() == "98" || cbTasaInteres.SelectedItem.Value.ToString().Trim() == "99")
                {
                    if (txtPuntosIPC.Value.ToString().Trim() != "0,00")
                    {
                        pagare1.puntosAdicionales = Convert.ToDouble(txtPuntosIPC.Value.ToString().Trim());
                    }
                    else
                    {
                        throw (new Exception("BL - RegistrarEditarBeneficio :: " + " puntos IPC no puede ser cero"));
                    }
                }
                else
                {
                    pagare1.puntosAdicionales = Convert.ToDouble(txtPuntosIPC.Value.ToString().Trim());
                }

                pagare1.idTasaMora = Convert.ToInt32(cbTasaMora.SelectedItem.Value.ToString().Trim());
                pagare1.idPlazoObligacion = Convert.ToInt32(cbPlazoObligacion.SelectedItem.Value.ToString().Trim());
                pagare1.cantidadAnios = Convert.ToInt32(txtCantidadAnios.Value.ToString().Trim());

                if (txtPorcentajeDistribucionP1.Value.ToString().Trim() != "0,00")
                {
                    pagare1.PorcentajeP1 = Convert.ToDouble(txtPorcentajeDistribucionP1.Value.ToString().Trim());
                }
                else
                {
                    throw (new Exception("BL - RegistrarEditarBeneficio :: " + " Porcentaje de distribucion P1 no puede ser cero"));
                }

                if (txtPorcentajeDistribucionP2.Value.ToString().Trim() != "0,00")
                {
                    pagare1.PorcentajeP2 = Convert.ToDouble(txtPorcentajeDistribucionP2.Value.ToString().Trim());
                }
                else
                {
                    throw (new Exception("BL - RegistrarEditarBeneficio :: " + " Porcentaje de distribucion P2 no puede ser cero"));
                }

                pagare1.idPeriocidadIntereses = Convert.ToInt32(cbPeriocidadIntereses.SelectedItem.Value.ToString().Trim());

                pagare1.idPeriodoMuerto = Convert.ToInt32(cbPeriodoMuerto.SelectedItem.Value.ToString().Trim());

                if (cbPeriodoMuerto.SelectedItem.Value.ToString().Trim() == "2")
                {
                    if (Convert.ToInt32(txtCantidadPeriodoMuerto.Value.ToString().Trim()) != 0)
                    {
                        pagare1.cantidadAñosPeriodoMuerto = Convert.ToInt32(txtCantidadPeriodoMuerto.Value.ToString().Trim());
                    }
                    else
                    {
                        throw (new Exception("BL - RegistrarEditarBeneficio :: " + " Cantidad años periodo muerto no puede ser cero"));
                    }
                }
                else
                {
                    pagare1.cantidadAñosPeriodoMuerto = (int?)null;
                }

                pagare1.idCalificacionCartera = Convert.ToInt32(cbCalificacion.SelectedItem.Value.ToString().Trim());

                pagare1.idPeriodogracia = Convert.ToInt32(cbPeriodoGracia.SelectedItem.Value.ToString().Trim());

                if (cbPeriodoGracia.SelectedItem.Value.ToString().Trim() == "2")
                {
                    if (Convert.ToInt32(txtCantidadPeriodoGracia.Value.ToString().Trim()) != 0)
                    {
                        pagare1.cantidadAñosPeriodoGracia = Convert.ToInt32(txtCantidadPeriodoGracia.Value.ToString().Trim());
                    }
                    else
                    {
                        throw (new Exception("BL - RegistrarEditarBeneficio :: " + " Cantidad años periodo gracia no puede ser cero"));
                    }
                }
                else
                {
                    pagare1.cantidadAñosPeriodoGracia = (int?)null;
                }

                pagare1.idBeneficioCapital = Convert.ToInt32(cbBeneficioCapital.SelectedItem.Value.ToString().Trim());

                pagare1.idBeneficioSeguroVida = Convert.ToInt32(cbBeneficioSeguroVida.SelectedItem.Value.ToString().Trim());

                if (cbBeneficioSeguroVida.SelectedItem.Value.ToString().Trim() == "2")
                {
                    pagare1.fechaInicioSeguroVida = Convert.ToDateTime(txtFechaInicioSeguroVida.Value.ToString().Trim());
                    pagare1.fechaFinSeguroVida = Convert.ToDateTime(txtFechaFinSeguroVida.Value.ToString().Trim());
                }
                else
                {
                    pagare1.fechaInicioSeguroVida = (DateTime?)null;
                    pagare1.fechaFinSeguroVida = (DateTime?)null;
                }

                pagare1.idBeneficioInteres = Convert.ToInt32(cbBeneficioInteres.SelectedItem.Value.ToString().Trim());

                if (cbBeneficioInteres.SelectedItem.Value.ToString().Trim() == "3" || cbBeneficioInteres.SelectedItem.Value.ToString().Trim() == "4")
                {
                    pagare1.fechaInicioInteres = Convert.ToDateTime(txtFechaInicioBeneficioInteres.Value.ToString().Trim());
                    pagare1.fechaFinInteres = Convert.ToDateTime(txtFechaFinBeneficioInteres.Value.ToString().Trim());
                }
                else
                {
                    pagare1.fechaInicioInteres = (DateTime?)null;
                    pagare1.fechaFinInteres = (DateTime?)null;
                }

                pagare1.idCapitalizacionIntereses = Convert.ToInt32(cbCapitalizacionIntereses.SelectedItem.Value.ToString().Trim());

                pagare1.idOtrosBeneficios = Convert.ToInt32(cbOtrosBeneficios.SelectedItem.Value.ToString().Trim());

                if (cbOtrosBeneficios.SelectedItem.Value.ToString().Trim() == "2")
                {
                    pagare1.fechaInicioOtrosBeneficios = Convert.ToDateTime(txtFechaInicioOtroBeneficio.Value.ToString().Trim());
                    pagare1.fechaFinOtrosBeneficios = Convert.ToDateTime(txtFechaFinOtroBeneficio.Value.ToString().Trim());
                }
                else
                {
                    pagare1.fechaInicioOtrosBeneficios = (DateTime?)null;
                    pagare1.fechaFinOtrosBeneficios = (DateTime?)null;
                }

                Pagares.Add(pagare1);


                //Validacion del pagare 2
                var pagare2 = new EntitiesPagareBeneficioLey();

                if (!string.IsNullOrEmpty(hdIdPagare2.Value))
                {
                    pagare2.id = Convert.ToInt32(hdIdPagare2.Value);
                }

                if (!string.IsNullOrEmpty(hdIdBeneficio.Value))
                {
                    pagare2.idCabecera = Convert.ToInt32(hdIdBeneficio.Value);
                }
                
                pagare2.idTasaInteresCorriente = Convert.ToInt32(cbTasaInteres.SelectedItem.Value.ToString().Trim());

                if (cbTasaInteresP2.SelectedItem.Value.ToString().Trim() == "96" || cbTasaInteresP2.SelectedItem.Value.ToString().Trim() == "97"
                    || cbTasaInteresP2.SelectedItem.Value.ToString().Trim() == "98" || cbTasaInteresP2.SelectedItem.Value.ToString().Trim() == "99")
                {
                    if (txtPuntosIPCP2.Value.ToString().Trim() != "0,00")
                    {
                        pagare2.puntosAdicionales = Convert.ToDouble(txtPuntosIPCP2.Value.ToString().Trim());
                    }
                    else
                    {
                        throw (new Exception("BL - RegistrarEditarBeneficio :: " + " puntos IPC no puede ser cero"));
                    }
                }
                else
                {
                    pagare2.puntosAdicionales = Convert.ToDouble(txtPuntosIPCP2.Value.ToString().Trim());
                }

                pagare2.idTasaMora = Convert.ToInt32(cbTasaMoraP2.SelectedItem.Value.ToString().Trim());
                pagare2.idPlazoObligacion = Convert.ToInt32(cbPlazoObligacionP2.SelectedItem.Value.ToString().Trim());
                pagare2.cantidadAnios = Convert.ToInt32(txtCantidadAniosP2.Value.ToString().Trim());

                if (txtPorcentajeDistribucionP1P2.Value.ToString().Trim() != "0,00")
                {
                    pagare2.PorcentajeP1 = Convert.ToDouble(txtPorcentajeDistribucionP1P2.Value.ToString().Trim());
                }
                else
                {
                    throw (new Exception("BL - RegistrarEditarBeneficio :: " + " Porcentaje de distribucion P1 no puede ser cero"));
                }

                if (txtPorcentajeDistribucionP2P2.Value.ToString().Trim() != "0,00")
                {
                    pagare2.PorcentajeP2 = Convert.ToDouble(txtPorcentajeDistribucionP2P2.Value.ToString().Trim());
                }
                else
                {
                    throw (new Exception("BL - RegistrarEditarBeneficio :: " + " Porcentaje de distribucion P2 no puede ser cero"));
                }

                pagare2.idPeriocidadIntereses = Convert.ToInt32(cbPeriocidadInteresesP2.SelectedItem.Value.ToString().Trim());

                pagare2.idPeriodoMuerto = Convert.ToInt32(cbPeriodoMuertoP2.SelectedItem.Value.ToString().Trim());

                if (cbPeriodoMuertoP2.SelectedItem.Value.ToString().Trim() == "2")
                {
                    if (Convert.ToInt32(txtCantidadPeriodoMuertoP2.Value.ToString().Trim()) != 0)
                    {
                        pagare2.cantidadAñosPeriodoMuerto = Convert.ToInt32(txtCantidadPeriodoMuertoP2.Value.ToString().Trim());
                    }
                    else
                    {
                        throw (new Exception("BL - RegistrarEditarBeneficio :: " + " Cantidad años periodo muerto no puede ser cero"));
                    }
                }
                else
                {
                    pagare2.cantidadAñosPeriodoMuerto = (int?)null;
                }

                pagare2.idCalificacionCartera = Convert.ToInt32(cbCalificacionP2.SelectedItem.Value.ToString().Trim());

                pagare2.idPeriodogracia = Convert.ToInt32(cbPeriodoGraciaP2.SelectedItem.Value.ToString().Trim());

                if (cbPeriodoGraciaP2.SelectedItem.Value.ToString().Trim() == "2")
                {
                    if (Convert.ToInt32(txtCantidadPeriodoGraciaP2.Value.ToString().Trim()) != 0)
                    {
                        pagare2.cantidadAñosPeriodoGracia = Convert.ToInt32(txtCantidadPeriodoGraciaP2.Value.ToString().Trim());
                    }
                    else
                    {
                        throw (new Exception("BL - RegistrarEditarBeneficio :: " + " Cantidad años periodo gracia no puede ser cero"));
                    }
                }
                else
                {
                    pagare2.cantidadAñosPeriodoGracia = (int?)null;
                }

                pagare2.idBeneficioCapital = Convert.ToInt32(cbBeneficioCapitalP2.SelectedItem.Value.ToString().Trim());

                pagare2.idBeneficioSeguroVida = Convert.ToInt32(cbBeneficioSeguroVidaP2.SelectedItem.Value.ToString().Trim());

                if (cbBeneficioSeguroVidaP2.SelectedItem.Value.ToString().Trim() == "2")
                {
                    pagare2.fechaInicioSeguroVida = Convert.ToDateTime(txtFechaInicioSeguroVidaP2.Value.ToString().Trim());
                    pagare2.fechaFinSeguroVida = Convert.ToDateTime(txtFechaFinSeguroVidaP2.Value.ToString().Trim());
                }
                else
                {
                    pagare2.fechaInicioSeguroVida = (DateTime?)null;
                    pagare2.fechaFinSeguroVida = (DateTime?)null;
                }

                pagare2.idBeneficioInteres = Convert.ToInt32(cbBeneficioInteresP2.SelectedItem.Value.ToString().Trim());

                if (cbBeneficioInteresP2.SelectedItem.Value.ToString().Trim() == "3" || cbBeneficioInteresP2.SelectedItem.Value.ToString().Trim() == "4")
                {
                    pagare2.fechaInicioInteres = Convert.ToDateTime(txtFechaInicioBeneficioInteresP2.Value.ToString().Trim());
                    pagare2.fechaFinInteres = Convert.ToDateTime(txtFechaFinBeneficioInteresP2.Value.ToString().Trim());
                }
                else
                {
                    pagare2.fechaInicioInteres = (DateTime?)null;
                    pagare2.fechaFinInteres = (DateTime?)null;
                }

                pagare2.idCapitalizacionIntereses = Convert.ToInt32(cbCapitalizacionInteresesP2.SelectedItem.Value.ToString().Trim());

                pagare2.idOtrosBeneficios = Convert.ToInt32(cbOtrosBeneficiosP2.SelectedItem.Value.ToString().Trim());

                if (cbOtrosBeneficiosP2.SelectedItem.Value.ToString().Trim() == "2")
                {
                    pagare2.fechaInicioOtrosBeneficios = Convert.ToDateTime(txtFechaInicioOtrosBeneficiosP2.Value.ToString().Trim());
                    pagare2.fechaFinOtrosBeneficios = Convert.ToDateTime(txtFechaFinOtrosBeneficiosP2.Value.ToString().Trim());
                }
                else
                {
                    pagare2.fechaInicioOtrosBeneficios = (DateTime?)null;
                    pagare2.fechaFinOtrosBeneficios = (DateTime?)null;
                }

                Pagares.Add(pagare2);

            }
            else
            {
                var pagare = new EntitiesPagareBeneficioLey();

                if (!string.IsNullOrEmpty(hdIdPagare1.Value))
                {
                    pagare.id = Convert.ToInt32(hdIdPagare1.Value);
                }

                if (!string.IsNullOrEmpty(hdIdBeneficio.Value))
                {
                    pagare.idCabecera = Convert.ToInt32(hdIdBeneficio.Value);
                }
                
                pagare.idTasaInteresCorriente = Convert.ToInt32(cbTasaInteres.SelectedItem.Value.ToString().Trim());                

                if (cbTasaInteres.SelectedItem.Value.ToString().Trim() == "96" || cbTasaInteres.SelectedItem.Value.ToString().Trim() == "97"
                    || cbTasaInteres.SelectedItem.Value.ToString().Trim() == "98" || cbTasaInteres.SelectedItem.Value.ToString().Trim() == "99")
                {
                    if (txtPuntosIPC.Value.ToString().Trim() != "0,00")
                    {
                        pagare.puntosAdicionales = Convert.ToDouble(txtPuntosIPC.Value.ToString().Trim());
                    }
                    else
                    {
                        throw (new Exception("BL - RegistrarEditarBeneficio :: " + " puntos IPC no puede ser cero"));
                    }
                }
                else
                {
                    pagare.puntosAdicionales = Convert.ToDouble(txtPuntosIPC.Value.ToString().Trim());
                }

                pagare.idTasaMora = Convert.ToInt32(cbTasaMora.SelectedItem.Value.ToString().Trim());
                pagare.idPlazoObligacion = Convert.ToInt32(cbPlazoObligacion.SelectedItem.Value.ToString().Trim());
                pagare.cantidadAnios = Convert.ToInt32(txtCantidadAnios.Value.ToString().Trim());

                if (txtPorcentajeDistribucionP1.Value.ToString().Trim() != "0,00")
                {
                    pagare.PorcentajeP1 = Convert.ToDouble(txtPorcentajeDistribucionP1.Value.ToString().Trim());
                }
                else
                {
                    throw (new Exception("BL - RegistrarEditarBeneficio :: " + " Porcentaje de distribucion P1 no puede ser cero"));
                }

                if (txtPorcentajeDistribucionP2.Value.ToString().Trim() != "0,00")
                {
                    pagare.PorcentajeP2 = Convert.ToDouble(txtPorcentajeDistribucionP2.Value.ToString().Trim());
                }
                else
                {
                    throw (new Exception("BL - RegistrarEditarBeneficio :: " + " Porcentaje de distribucion P2 no puede ser cero"));
                }

                pagare.idPeriocidadIntereses = Convert.ToInt32(cbPeriocidadIntereses.SelectedItem.Value.ToString().Trim());

                pagare.idPeriodoMuerto = Convert.ToInt32(cbPeriodoMuerto.SelectedItem.Value.ToString().Trim());

                if (cbPeriodoMuerto.SelectedItem.Value.ToString().Trim() == "2")
                {
                    if (Convert.ToInt32(txtCantidadPeriodoMuerto.Value.ToString().Trim()) != 0)
                    {
                        pagare.cantidadAñosPeriodoMuerto = Convert.ToInt32(txtCantidadPeriodoMuerto.Value.ToString().Trim());
                    }
                    else
                    {
                        throw (new Exception("BL - RegistrarEditarBeneficio :: " + " Cantidad años periodo muerto no puede ser cero"));
                    }
                }
                else
                {
                    pagare.cantidadAñosPeriodoMuerto = (int?)null;
                }

                pagare.idCalificacionCartera = Convert.ToInt32(cbCalificacion.SelectedItem.Value.ToString().Trim());

                pagare.idPeriodogracia = Convert.ToInt32(cbPeriodoGracia.SelectedItem.Value.ToString().Trim());

                if (cbPeriodoGracia.SelectedItem.Value.ToString().Trim() == "2")
                {
                    if (Convert.ToInt32(txtCantidadPeriodoGracia.Value.ToString().Trim()) != 0)
                    {
                        pagare.cantidadAñosPeriodoGracia = Convert.ToInt32(txtCantidadPeriodoGracia.Value.ToString().Trim());
                    }
                    else
                    {
                        throw (new Exception("BL - RegistrarEditarBeneficio :: " + " Cantidad años periodo gracia no puede ser cero"));
                    }
                }
                else
                {
                    pagare.cantidadAñosPeriodoGracia = (int?)null;
                }

                pagare.idBeneficioCapital = Convert.ToInt32(cbBeneficioCapital.SelectedItem.Value.ToString().Trim());

                pagare.idBeneficioSeguroVida = Convert.ToInt32(cbBeneficioSeguroVida.SelectedItem.Value.ToString().Trim());

                if (cbBeneficioSeguroVida.SelectedItem.Value.ToString().Trim() == "2")
                {
                    pagare.fechaInicioSeguroVida = Convert.ToDateTime(txtFechaInicioSeguroVida.Value.ToString().Trim());
                    pagare.fechaFinSeguroVida = Convert.ToDateTime(txtFechaFinSeguroVida.Value.ToString().Trim());
                }
                else
                {
                    pagare.fechaInicioSeguroVida = (DateTime?)null;
                    pagare.fechaFinSeguroVida = (DateTime?)null;
                }

                pagare.idBeneficioInteres = Convert.ToInt32(cbBeneficioInteres.SelectedItem.Value.ToString().Trim());

                if (cbBeneficioInteres.SelectedItem.Value.ToString().Trim() == "3" || cbBeneficioInteres.SelectedItem.Value.ToString().Trim() == "4")
                {
                    pagare.fechaInicioInteres = Convert.ToDateTime(txtFechaInicioBeneficioInteres.Value.ToString().Trim());
                    pagare.fechaFinInteres = Convert.ToDateTime(txtFechaFinBeneficioInteres.Value.ToString().Trim());
                }
                else
                {
                    pagare.fechaInicioInteres = (DateTime?)null;
                    pagare.fechaFinInteres = (DateTime?)null;
                }

                pagare.idCapitalizacionIntereses = Convert.ToInt32(cbCapitalizacionIntereses.SelectedItem.Value.ToString().Trim());

                pagare.idOtrosBeneficios = Convert.ToInt32(cbOtrosBeneficios.SelectedItem.Value.ToString().Trim());

                if (cbOtrosBeneficios.SelectedItem.Value.ToString().Trim() == "2")
                {
                    pagare.fechaInicioOtrosBeneficios = Convert.ToDateTime(txtFechaInicioOtroBeneficio.Value.ToString().Trim());
                    pagare.fechaFinOtrosBeneficios = Convert.ToDateTime(txtFechaFinOtroBeneficio.Value.ToString().Trim());
                }
                else
                {
                    pagare.fechaInicioOtrosBeneficios = (DateTime?)null;
                    pagare.fechaFinOtrosBeneficios = (DateTime?)null;
                }

                Pagares.Add(pagare);
            }

            return Pagares;
        }

        /// <summary>
        /// Consulta los planes de pago para su respectiva ediccion o consulta
        /// </summary>
        private void ConsultarBeneficioEditar()
        {
            this.beneficioBL = new BLBeneficioLey();
            this.lstBeneficioBl = new List<EntitiesCabeceraBeneficioLey>();
            var beneficio = this.beneficioBL.ConsultaCabeceraBeneficioLey(this.accesoDatos).FirstOrDefault();

            this.Limpiarobjetos();
            this.hdIdBeneficio.Value = beneficio.id.ToString();
            this.txtCodigoBeneficio.Value = beneficio.id.ToString();
            this.cbPrograma.SelectedIndex = this.cbPrograma.Items.FindByValue(beneficio.idPrograma.ToString()).Index;
            this.txtNombreBeneficio.Value = beneficio.nombreBeneficio.ToString();
            this.cbDepartamento.SelectedIndex = this.cbDepartamento.Items.FindByValue(beneficio.idDepartamento.ToString()).Index;
            this.CargaMunicipio(beneficio.idDepartamento.ToString());
            this.cbMunicipio.SelectedIndex = this.cbMunicipio.Items.FindByValue(beneficio.idMunicipio.ToString()).Index;
            this.txtFechaInicial.Value = Convert.ToDateTime(beneficio.fechaInicial.ToString());
            this.txtFechaFinal.Value = Convert.ToDateTime(beneficio.fechaFinal.ToString());
            this.cbPagares.SelectedIndex = this.cbPagares.Items.FindByValue(beneficio.idCantidadPagares.ToString()).Index;
            this.txtMontoMaximo.Value = beneficio.topeMaximo.ToString();
            this.cbTipoBeneficio.SelectedIndex = this.cbTipoBeneficio.Items.FindByValue(beneficio.idTipoBeneficiado.ToString()).Index;
            this.cbActividadAgropecuaria.SelectedIndex = this.cbActividadAgropecuaria.Items.FindByValue(beneficio.idActividadAgropecuaria.ToString()).Index; ;

            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarPagaresBeneficioLey",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppCbl_Id",
                parametroValor = Convert.ToInt32(this.hdIdBeneficio.Value)
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.ConsultarPagaresBeneficiosEditar();
        }

        /// <summary>
        /// Consulta los planes de pago para su respectiva ediccion o consulta
        /// </summary>
        private void ConsultarPagaresBeneficiosEditar()
        {
            this.beneficioBL = new BLBeneficioLey();
            this.lstBeneficioBl = new List<EntitiesCabeceraBeneficioLey>();
            var beneficio = this.beneficioBL.ConsultaPagareBeneficioLey(this.accesoDatos);

            TabbedLayoutGroup group = (TabbedLayoutGroup)formLayout.Items[0];
            group.PageControl.ActiveTabIndex = 0;

            if (beneficio.Count == 2)
            {
                group.PageControl.TabPages[2].Enabled = true;

                //Pagare 1

                this.hdIdPagare1.Value = beneficio[0].id.ToString();
                this.cbTasaInteres.SelectedIndex = this.cbTasaInteres.Items.FindByValue(beneficio[0].idTasaInteresCorriente.ToString()).Index;
                this.txtPuntosIPC.Value = beneficio[0].puntosAdicionales.ToString();
                this.cbTasaMora.SelectedIndex = this.cbTasaMora.Items.FindByValue(beneficio[0].idTasaMora.ToString()).Index;
                this.cbPlazoObligacion.SelectedIndex = this.cbPlazoObligacion.Items.FindByValue(beneficio[0].idPlazoObligacion.ToString()).Index;
                this.txtCantidadAnios.Value = beneficio[0].cantidadAnios.ToString();
                this.txtPorcentajeDistribucionP1.Value = Convert.ToDouble(beneficio[0].PorcentajeP1.ToString());
                this.txtPorcentajeDistribucionP2.Value = Convert.ToDouble(beneficio[0].PorcentajeP2.ToString());
                this.cbPeriocidadIntereses.SelectedIndex = this.cbPeriocidadIntereses.Items.FindByValue(beneficio[0].idPeriocidadIntereses.ToString()).Index;
                this.cbPeriodoMuerto.SelectedIndex = this.cbPeriodoMuerto.Items.FindByValue(beneficio[0].idPeriodoMuerto.ToString()).Index;
                this.txtCantidadPeriodoMuerto.Value = beneficio[0].cantidadAñosPeriodoMuerto.ToString();
                this.cbCalificacion.SelectedIndex = this.cbCalificacion.Items.FindByValue(beneficio[0].idCalificacionCartera.ToString()).Index;
                this.cbPeriodoGracia.SelectedIndex = this.cbPeriodoGracia.Items.FindByValue(beneficio[0].idPeriodogracia.ToString()).Index;
                this.txtCantidadPeriodoGracia.Value = beneficio[0].cantidadAñosPeriodoGracia.ToString();
                this.cbBeneficioCapital.SelectedIndex = this.cbBeneficioCapital.Items.FindByValue(beneficio[0].idBeneficioCapital.ToString()).Index;
                this.cbBeneficioSeguroVida.SelectedIndex = this.cbBeneficioSeguroVida.Items.FindByValue(beneficio[0].idBeneficioSeguroVida.ToString()).Index;
                this.txtFechaInicioSeguroVida.Value = string.IsNullOrEmpty(beneficio[0].fechaInicioSeguroVida.ToString()) ? (DateTime?)null : Convert.ToDateTime(beneficio[0].fechaInicioSeguroVida.ToString());
                this.txtFechaFinSeguroVida.Value = string.IsNullOrEmpty(beneficio[0].fechaFinSeguroVida.ToString()) ? (DateTime?)null : Convert.ToDateTime(beneficio[0].fechaFinSeguroVida.ToString());
                this.cbBeneficioInteres.SelectedIndex = this.cbBeneficioInteres.Items.FindByValue(beneficio[0].idBeneficioInteres.ToString()).Index;
                this.txtFechaInicioBeneficioInteres.Value = string.IsNullOrEmpty(beneficio[0].fechaInicioInteres.ToString()) ? (DateTime?)null : Convert.ToDateTime(beneficio[0].fechaInicioInteres.ToString());
                this.txtFechaFinBeneficioInteres.Value = string.IsNullOrEmpty(beneficio[0].fechaFinInteres.ToString()) ? (DateTime?)null : Convert.ToDateTime(beneficio[0].fechaFinInteres.ToString());
                this.cbCapitalizacionIntereses.SelectedIndex = this.cbCapitalizacionIntereses.Items.FindByValue(beneficio[0].idCapitalizacionIntereses.ToString()).Index;
                this.cbOtrosBeneficios.SelectedIndex = this.cbOtrosBeneficios.Items.FindByValue(beneficio[0].idOtrosBeneficios.ToString()).Index;
                this.txtFechaInicioOtroBeneficio.Value = string.IsNullOrEmpty(beneficio[0].fechaInicioOtrosBeneficios.ToString()) ? (DateTime?)null : Convert.ToDateTime(beneficio[0].fechaInicioOtrosBeneficios.ToString());
                this.txtFechaFinOtroBeneficio.Value = string.IsNullOrEmpty(beneficio[0].fechaFinOtrosBeneficios.ToString()) ? (DateTime?)null : Convert.ToDateTime(beneficio[0].fechaFinOtrosBeneficios.ToString());

                if (beneficio[0].idPeriodoMuerto == 2)
                {
                    this.txtCantidadPeriodoMuerto.ClientEnabled = true;
                    this.txtCantidadPeriodoMuerto.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtCantidadPeriodoMuerto.ValidationSettings.ValidationGroup = "0_1";
                    this.txtCantidadPeriodoMuerto.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtCantidadPeriodoMuerto.ValidationSettings.CausesValidation = true;
                    this.txtCantidadPeriodoMuerto.ValidationSettings.ErrorText = "Se requiere la cantidad de los años";
                    this.txtCantidadPeriodoMuerto.ValidationSettings.Display = Display.Dynamic;
                    this.txtCantidadPeriodoMuerto.ClientSideEvents.Validation = "onValidation";

                    this.txtCantidadPeriodoMuerto.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtCantidadPeriodoMuerto.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtCantidadPeriodoMuerto.ClientEnabled = false;
                    this.txtCantidadPeriodoMuerto.ValidationSettings.RequiredField.IsRequired = false;
                }

                if (beneficio[0].idPeriodogracia == 2)
                {
                    this.txtCantidadPeriodoGracia.ClientEnabled = true;
                    this.txtCantidadPeriodoGracia.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtCantidadPeriodoGracia.ValidationSettings.ValidationGroup = "0_1";
                    this.txtCantidadPeriodoGracia.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtCantidadPeriodoGracia.ValidationSettings.CausesValidation = true;
                    this.txtCantidadPeriodoGracia.ValidationSettings.ErrorText = "Se requiere la cantidad de los años";
                    this.txtCantidadPeriodoGracia.ValidationSettings.Display = Display.Dynamic;
                    this.txtCantidadPeriodoGracia.ClientSideEvents.Validation = "onValidation";

                    this.txtCantidadPeriodoGracia.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtCantidadPeriodoGracia.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtCantidadPeriodoGracia.ClientEnabled = false;
                    this.txtCantidadPeriodoGracia.ValidationSettings.RequiredField.IsRequired = false;
                }

                if (beneficio[0].idBeneficioSeguroVida == 2)
                {
                    this.txtFechaInicioSeguroVida.ClientEnabled = true;
                    this.txtFechaInicioSeguroVida.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaInicioSeguroVida.ValidationSettings.ValidationGroup = "0_1";
                    this.txtFechaInicioSeguroVida.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaInicioSeguroVida.ValidationSettings.CausesValidation = true;
                    this.txtFechaInicioSeguroVida.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaInicioSeguroVida.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaInicioSeguroVida.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaInicioSeguroVida.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaInicioSeguroVida.ValidationSettings.ValidationGroup;
                    };

                    this.txtFechaFinSeguroVida.ClientEnabled = true;
                    this.txtFechaFinSeguroVida.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaFinSeguroVida.ValidationSettings.ValidationGroup = "0_1";
                    this.txtFechaFinSeguroVida.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaFinSeguroVida.ValidationSettings.CausesValidation = true;
                    this.txtFechaFinSeguroVida.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaFinSeguroVida.ValidationSettings.Display = Display.Dynamic;
                }
                else
                {
                    this.txtFechaInicioSeguroVida.ClientEnabled = false;
                    this.txtFechaInicioSeguroVida.ValidationSettings.RequiredField.IsRequired = false;

                    this.txtFechaFinSeguroVida.ClientEnabled = false;
                    this.txtFechaFinSeguroVida.ValidationSettings.RequiredField.IsRequired = false;
                }

                if (beneficio[0].idBeneficioInteres == 3 || beneficio[0].idBeneficioInteres == 4)
                {
                    this.txtFechaInicioBeneficioInteres.ClientEnabled = true;
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.ValidationGroup = "0_1";
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.CausesValidation = true;
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaInicioBeneficioInteres.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaInicioBeneficioInteres.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaInicioBeneficioInteres.ValidationSettings.ValidationGroup;
                    };

                    this.txtFechaFinBeneficioInteres.ClientEnabled = true;
                    this.txtFechaFinBeneficioInteres.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaFinBeneficioInteres.ValidationSettings.ValidationGroup = "0_1";
                    this.txtFechaFinBeneficioInteres.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaFinBeneficioInteres.ValidationSettings.CausesValidation = true;
                    this.txtFechaFinBeneficioInteres.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaFinBeneficioInteres.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaFinBeneficioInteres.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaFinBeneficioInteres.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaFinBeneficioInteres.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtFechaInicioBeneficioInteres.ClientEnabled = false;
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.RequiredField.IsRequired = false;

                    this.txtFechaFinBeneficioInteres.ClientEnabled = false;
                    this.txtFechaFinBeneficioInteres.ValidationSettings.RequiredField.IsRequired = false;
                }

                if (beneficio[0].idOtrosBeneficios == 2)
                {
                    this.txtFechaInicioOtroBeneficio.ClientEnabled = true;
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.ValidationGroup = "0_1";
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.CausesValidation = true;
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaInicioOtroBeneficio.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaInicioOtroBeneficio.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaInicioOtroBeneficio.ValidationSettings.ValidationGroup;
                    };

                    this.txtFechaFinOtroBeneficio.ClientEnabled = true;
                    this.txtFechaFinOtroBeneficio.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaFinOtroBeneficio.ValidationSettings.ValidationGroup = "0_1";
                    this.txtFechaFinOtroBeneficio.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaFinOtroBeneficio.ValidationSettings.CausesValidation = true;
                    this.txtFechaFinOtroBeneficio.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaFinOtroBeneficio.ValidationSettings.Display = Display.Dynamic;

                    this.txtFechaFinOtroBeneficio.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaFinOtroBeneficio.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaFinOtroBeneficio.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtFechaInicioOtroBeneficio.ClientEnabled = false;
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.RequiredField.IsRequired = false;

                    this.txtFechaFinOtroBeneficio.ClientEnabled = false;
                    this.txtFechaFinOtroBeneficio.ValidationSettings.RequiredField.IsRequired = false;
                }

                //Pagare 2

                this.hdIdPagare2.Value = beneficio[1].id.ToString();
                this.cbTasaInteresP2.SelectedIndex = this.cbTasaInteres.Items.FindByValue(beneficio[1].idTasaInteresCorriente.ToString()).Index;
                this.txtPuntosIPCP2.Value = beneficio[1].puntosAdicionales.ToString();
                this.cbTasaMoraP2.SelectedIndex = this.cbTasaMora.Items.FindByValue(beneficio[1].idTasaMora.ToString()).Index;
                this.cbPlazoObligacionP2.SelectedIndex = this.cbPlazoObligacion.Items.FindByValue(beneficio[1].idPlazoObligacion.ToString()).Index;
                this.txtCantidadAniosP2.Value = beneficio[1].cantidadAnios.ToString();
                this.txtPorcentajeDistribucionP1P2.Value = Convert.ToDouble(beneficio[1].PorcentajeP1.ToString());
                this.txtPorcentajeDistribucionP2P2.Value = Convert.ToDouble(beneficio[1].PorcentajeP2.ToString());
                this.cbPeriocidadInteresesP2.SelectedIndex = this.cbPeriocidadIntereses.Items.FindByValue(beneficio[1].idPeriocidadIntereses.ToString()).Index;
                this.cbPeriodoMuertoP2.SelectedIndex = this.cbPeriodoMuerto.Items.FindByValue(beneficio[1].idPeriodoMuerto.ToString()).Index;
                this.txtCantidadPeriodoMuertoP2.Value = beneficio[1].cantidadAñosPeriodoMuerto.ToString();
                this.cbCalificacionP2.SelectedIndex = this.cbCalificacion.Items.FindByValue(beneficio[1].idCalificacionCartera.ToString()).Index;
                this.cbPeriodoGraciaP2.SelectedIndex = this.cbPeriodoGracia.Items.FindByValue(beneficio[1].idPeriodogracia.ToString()).Index;
                this.txtCantidadPeriodoGraciaP2.Value = beneficio[1].cantidadAñosPeriodoGracia.ToString();
                this.cbBeneficioCapitalP2.SelectedIndex = this.cbBeneficioCapital.Items.FindByValue(beneficio[1].idBeneficioCapital.ToString()).Index;
                this.cbBeneficioSeguroVidaP2.SelectedIndex = this.cbBeneficioSeguroVida.Items.FindByValue(beneficio[1].idBeneficioSeguroVida.ToString()).Index;
                this.txtFechaInicioSeguroVidaP2.Value = string.IsNullOrEmpty(beneficio[1].fechaInicioSeguroVida.ToString()) ? (DateTime?)null : Convert.ToDateTime(beneficio[1].fechaInicioSeguroVida.ToString());
                this.txtFechaFinSeguroVidaP2.Value = string.IsNullOrEmpty(beneficio[1].fechaFinSeguroVida.ToString()) ? (DateTime?)null : Convert.ToDateTime(beneficio[1].fechaFinSeguroVida.ToString());
                this.cbBeneficioInteresP2.SelectedIndex = this.cbBeneficioInteres.Items.FindByValue(beneficio[1].idBeneficioInteres.ToString()).Index;
                this.txtFechaInicioBeneficioInteresP2.Value = string.IsNullOrEmpty(beneficio[1].fechaInicioInteres.ToString()) ? (DateTime?)null : Convert.ToDateTime(beneficio[1].fechaInicioInteres.ToString());
                this.txtFechaFinBeneficioInteresP2.Value = string.IsNullOrEmpty(beneficio[1].fechaFinInteres.ToString()) ? (DateTime?)null : Convert.ToDateTime(beneficio[1].fechaFinInteres.ToString());
                this.cbCapitalizacionInteresesP2.SelectedIndex = this.cbCapitalizacionInteresesP2.Items.FindByValue(beneficio[1].idCapitalizacionIntereses.ToString()).Index;
                this.cbOtrosBeneficiosP2.SelectedIndex = this.cbOtrosBeneficiosP2.Items.FindByValue(beneficio[1].idOtrosBeneficios.ToString()).Index;
                this.txtFechaInicioOtrosBeneficiosP2.Value = string.IsNullOrEmpty(beneficio[1].fechaInicioOtrosBeneficios.ToString()) ? (DateTime?)null : Convert.ToDateTime(beneficio[1].fechaInicioOtrosBeneficios.ToString());
                this.txtFechaFinOtrosBeneficiosP2.Value = string.IsNullOrEmpty(beneficio[1].fechaFinOtrosBeneficios.ToString()) ? (DateTime?)null : Convert.ToDateTime(beneficio[1].fechaFinOtrosBeneficios.ToString());

                if (beneficio[1].idPeriodoMuerto == 2)
                {
                    this.txtCantidadPeriodoMuertoP2.ClientEnabled = true;
                    this.txtCantidadPeriodoMuertoP2.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtCantidadPeriodoMuertoP2.ValidationSettings.ValidationGroup = "0_2";
                    this.txtCantidadPeriodoMuertoP2.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtCantidadPeriodoMuertoP2.ValidationSettings.CausesValidation = true;
                    this.txtCantidadPeriodoMuertoP2.ValidationSettings.ErrorText = "Se requiere la cantidad de los años";
                    this.txtCantidadPeriodoMuertoP2.ValidationSettings.Display = Display.Dynamic;
                    this.txtCantidadPeriodoMuertoP2.ClientSideEvents.Validation = "onValidation";

                    this.txtCantidadPeriodoMuertoP2.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtCantidadPeriodoMuertoP2.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtCantidadPeriodoMuertoP2.ClientEnabled = false;
                    this.txtCantidadPeriodoMuertoP2.ValidationSettings.RequiredField.IsRequired = false;
                }

                if (beneficio[1].idPeriodogracia == 2)
                {
                    this.txtCantidadPeriodoGraciaP2.ClientEnabled = true;
                    this.txtCantidadPeriodoGraciaP2.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtCantidadPeriodoGraciaP2.ValidationSettings.ValidationGroup = "0_2";
                    this.txtCantidadPeriodoGraciaP2.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtCantidadPeriodoGraciaP2.ValidationSettings.CausesValidation = true;
                    this.txtCantidadPeriodoGraciaP2.ValidationSettings.ErrorText = "Se requiere la cantidad de los años";
                    this.txtCantidadPeriodoGraciaP2.ValidationSettings.Display = Display.Dynamic;
                    this.txtCantidadPeriodoGraciaP2.ClientSideEvents.Validation = "onValidation";

                    this.txtCantidadPeriodoGraciaP2.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtCantidadPeriodoGraciaP2.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtCantidadPeriodoGraciaP2.ClientEnabled = false;
                    this.txtCantidadPeriodoGraciaP2.ValidationSettings.RequiredField.IsRequired = false;
                }

                if (beneficio[1].idBeneficioSeguroVida == 2)
                {
                    this.txtFechaInicioSeguroVidaP2.ClientEnabled = true;
                    this.txtFechaInicioSeguroVidaP2.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaInicioSeguroVidaP2.ValidationSettings.ValidationGroup = "0_2";
                    this.txtFechaInicioSeguroVidaP2.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaInicioSeguroVidaP2.ValidationSettings.CausesValidation = true;
                    this.txtFechaInicioSeguroVidaP2.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaInicioSeguroVidaP2.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaInicioSeguroVidaP2.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaInicioSeguroVidaP2.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaInicioSeguroVidaP2.ValidationSettings.ValidationGroup;
                    };

                    this.txtFechaFinSeguroVidaP2.ClientEnabled = true;
                    this.txtFechaFinSeguroVidaP2.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaFinSeguroVidaP2.ValidationSettings.ValidationGroup = "0_2";
                    this.txtFechaFinSeguroVidaP2.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaFinSeguroVidaP2.ValidationSettings.CausesValidation = true;
                    this.txtFechaFinSeguroVidaP2.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaFinSeguroVidaP2.ValidationSettings.Display = Display.Dynamic;
                }
                else
                {
                    this.txtFechaInicioSeguroVidaP2.ClientEnabled = false;
                    this.txtFechaInicioSeguroVidaP2.ValidationSettings.RequiredField.IsRequired = false;

                    this.txtFechaFinSeguroVidaP2.ClientEnabled = false;
                    this.txtFechaFinSeguroVidaP2.ValidationSettings.RequiredField.IsRequired = false;
                }

                if (beneficio[1].idBeneficioInteres == 3 || beneficio[1].idBeneficioInteres == 4)
                {
                    this.txtFechaInicioBeneficioInteresP2.ClientEnabled = true;
                    this.txtFechaInicioBeneficioInteresP2.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaInicioBeneficioInteresP2.ValidationSettings.ValidationGroup = "0_2";
                    this.txtFechaInicioBeneficioInteresP2.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaInicioBeneficioInteresP2.ValidationSettings.CausesValidation = true;
                    this.txtFechaInicioBeneficioInteresP2.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaInicioBeneficioInteresP2.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaInicioBeneficioInteresP2.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaInicioBeneficioInteresP2.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaInicioBeneficioInteresP2.ValidationSettings.ValidationGroup;
                    };

                    this.txtFechaFinBeneficioInteresP2.ClientEnabled = true;
                    this.txtFechaFinBeneficioInteresP2.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaFinBeneficioInteresP2.ValidationSettings.ValidationGroup = "0_2";
                    this.txtFechaFinBeneficioInteresP2.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaFinBeneficioInteresP2.ValidationSettings.CausesValidation = true;
                    this.txtFechaFinBeneficioInteresP2.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaFinBeneficioInteresP2.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaFinBeneficioInteresP2.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaFinBeneficioInteresP2.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaFinBeneficioInteresP2.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtFechaInicioBeneficioInteresP2.ClientEnabled = false;
                    this.txtFechaInicioBeneficioInteresP2.ValidationSettings.RequiredField.IsRequired = false;

                    this.txtFechaFinBeneficioInteresP2.ClientEnabled = false;
                    this.txtFechaFinBeneficioInteresP2.ValidationSettings.RequiredField.IsRequired = false;
                }

                if (beneficio[1].idOtrosBeneficios == 2)
                {
                    this.txtFechaInicioOtrosBeneficiosP2.ClientEnabled = true;
                    this.txtFechaInicioOtrosBeneficiosP2.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaInicioOtrosBeneficiosP2.ValidationSettings.ValidationGroup = "0_2";
                    this.txtFechaInicioOtrosBeneficiosP2.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaInicioOtrosBeneficiosP2.ValidationSettings.CausesValidation = true;
                    this.txtFechaInicioOtrosBeneficiosP2.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaInicioOtrosBeneficiosP2.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaInicioOtrosBeneficiosP2.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaInicioOtrosBeneficiosP2.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaInicioOtrosBeneficiosP2.ValidationSettings.ValidationGroup;
                    };

                    this.txtFechaFinOtrosBeneficiosP2.ClientEnabled = true;
                    this.txtFechaFinOtrosBeneficiosP2.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaFinOtrosBeneficiosP2.ValidationSettings.ValidationGroup = "0_2";
                    this.txtFechaFinOtrosBeneficiosP2.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaFinOtrosBeneficiosP2.ValidationSettings.CausesValidation = true;
                    this.txtFechaFinOtrosBeneficiosP2.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaFinOtrosBeneficiosP2.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaFinOtrosBeneficiosP2.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaFinOtrosBeneficiosP2.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaFinOtrosBeneficiosP2.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtFechaInicioOtrosBeneficiosP2.ClientEnabled = false;
                    this.txtFechaInicioOtrosBeneficiosP2.ValidationSettings.RequiredField.IsRequired = false;

                    this.txtFechaFinOtrosBeneficiosP2.ClientEnabled = false;
                    this.txtFechaFinOtrosBeneficiosP2.ValidationSettings.RequiredField.IsRequired = false;
                }
            }
            else
            {
                var beneficio1 = beneficio.FirstOrDefault();

                group.PageControl.TabPages[2].Enabled = false;

                this.hdIdPagare1.Value = beneficio1.id.ToString();
                this.cbTasaInteres.SelectedIndex = this.cbTasaInteres.Items.FindByValue(beneficio1.idTasaInteresCorriente.ToString()).Index;
                this.txtPuntosIPC.Value = beneficio1.puntosAdicionales.ToString();
                this.cbTasaMora.SelectedIndex = this.cbTasaMora.Items.FindByValue(beneficio1.idTasaMora.ToString()).Index;
                this.cbPlazoObligacion.SelectedIndex = this.cbPlazoObligacion.Items.FindByValue(beneficio1.idPlazoObligacion.ToString()).Index;
                this.txtCantidadAnios.Value = beneficio1.cantidadAnios.ToString();
                this.txtPorcentajeDistribucionP1.Value = Convert.ToDouble(beneficio1.PorcentajeP1.ToString());
                this.txtPorcentajeDistribucionP2.Value = Convert.ToDouble(beneficio1.PorcentajeP2.ToString());
                this.cbPeriocidadIntereses.SelectedIndex = this.cbPeriocidadIntereses.Items.FindByValue(beneficio1.idPeriocidadIntereses.ToString()).Index;
                this.cbPeriodoMuerto.SelectedIndex = this.cbPeriodoMuerto.Items.FindByValue(beneficio1.idPeriodoMuerto.ToString()).Index;
                this.txtCantidadPeriodoMuerto.Value = beneficio1.cantidadAñosPeriodoMuerto.ToString();
                this.cbCalificacion.SelectedIndex = this.cbCalificacion.Items.FindByValue(beneficio1.idCalificacionCartera.ToString()).Index;
                this.cbPeriodoGracia.SelectedIndex = this.cbPeriodoGracia.Items.FindByValue(beneficio1.idPeriodogracia.ToString()).Index;
                this.txtCantidadPeriodoGracia.Value = beneficio1.cantidadAñosPeriodoGracia.ToString();
                this.cbBeneficioCapital.SelectedIndex = this.cbBeneficioCapital.Items.FindByValue(beneficio1.idBeneficioCapital.ToString()).Index;
                this.cbBeneficioSeguroVida.SelectedIndex = this.cbBeneficioSeguroVida.Items.FindByValue(beneficio1.idBeneficioSeguroVida.ToString()).Index;
                this.txtFechaInicioSeguroVida.Value = string.IsNullOrEmpty(beneficio1.fechaInicioSeguroVida.ToString()) ? (DateTime?)null : Convert.ToDateTime(beneficio1.fechaInicioSeguroVida.ToString());
                this.txtFechaFinSeguroVida.Value = string.IsNullOrEmpty(beneficio1.fechaFinSeguroVida.ToString()) ? (DateTime?)null : Convert.ToDateTime(beneficio1.fechaFinSeguroVida.ToString());
                this.cbBeneficioInteres.SelectedIndex = this.cbBeneficioInteres.Items.FindByValue(beneficio1.idBeneficioInteres.ToString()).Index;
                this.txtFechaInicioBeneficioInteres.Value = string.IsNullOrEmpty(beneficio1.fechaInicioInteres.ToString()) ? (DateTime?)null : Convert.ToDateTime(beneficio1.fechaInicioInteres.ToString());
                this.txtFechaFinBeneficioInteres.Value = string.IsNullOrEmpty(beneficio1.fechaFinInteres.ToString()) ? (DateTime?)null : Convert.ToDateTime(beneficio1.fechaFinInteres.ToString());
                this.cbCapitalizacionIntereses.SelectedIndex = this.cbCapitalizacionIntereses.Items.FindByValue(beneficio1.idCapitalizacionIntereses.ToString()).Index;
                this.cbOtrosBeneficios.SelectedIndex = this.cbOtrosBeneficios.Items.FindByValue(beneficio1.idOtrosBeneficios.ToString()).Index;
                this.txtFechaInicioOtroBeneficio.Value = string.IsNullOrEmpty(beneficio1.fechaInicioOtrosBeneficios.ToString()) ? (DateTime?)null : Convert.ToDateTime(beneficio1.fechaInicioOtrosBeneficios.ToString());
                this.txtFechaFinOtroBeneficio.Value = string.IsNullOrEmpty(beneficio1.fechaFinOtrosBeneficios.ToString()) ? (DateTime?)null : Convert.ToDateTime(beneficio1.fechaFinOtrosBeneficios.ToString());

                if (beneficio1.idPeriodoMuerto == 2)
                {
                    this.txtCantidadPeriodoMuerto.ClientEnabled = true;
                    this.txtCantidadPeriodoMuerto.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtCantidadPeriodoMuerto.ValidationSettings.ValidationGroup = "0_1";
                    this.txtCantidadPeriodoMuerto.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtCantidadPeriodoMuerto.ValidationSettings.CausesValidation = true;
                    this.txtCantidadPeriodoMuerto.ValidationSettings.ErrorText = "Se requiere la cantidad de los años";
                    this.txtCantidadPeriodoMuerto.ValidationSettings.Display = Display.Dynamic;
                    this.txtCantidadPeriodoMuerto.ClientSideEvents.Validation = "onValidation";

                    this.txtCantidadPeriodoMuerto.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtCantidadPeriodoMuerto.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtCantidadPeriodoMuerto.ClientEnabled = false;
                    this.txtCantidadPeriodoMuerto.ValidationSettings.RequiredField.IsRequired = false;
                }

                if (beneficio1.idPeriodogracia == 2)
                {
                    this.txtCantidadPeriodoGracia.ClientEnabled = true;
                    this.txtCantidadPeriodoGracia.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtCantidadPeriodoGracia.ValidationSettings.ValidationGroup = "0_1";
                    this.txtCantidadPeriodoGracia.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtCantidadPeriodoGracia.ValidationSettings.CausesValidation = true;
                    this.txtCantidadPeriodoGracia.ValidationSettings.ErrorText = "Se requiere la cantidad de los años";
                    this.txtCantidadPeriodoGracia.ValidationSettings.Display = Display.Dynamic;
                    this.txtCantidadPeriodoGracia.ClientSideEvents.Validation = "onValidation";

                    this.txtCantidadPeriodoGracia.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtCantidadPeriodoGracia.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtCantidadPeriodoGracia.ClientEnabled = false;
                    this.txtCantidadPeriodoGracia.ValidationSettings.RequiredField.IsRequired = false;
                }

                if (beneficio1.idBeneficioSeguroVida == 2)
                {
                    this.txtFechaInicioSeguroVida.ClientEnabled = true;
                    this.txtFechaInicioSeguroVida.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaInicioSeguroVida.ValidationSettings.ValidationGroup = "0_1";
                    this.txtFechaInicioSeguroVida.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaInicioSeguroVida.ValidationSettings.CausesValidation = true;
                    this.txtFechaInicioSeguroVida.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaInicioSeguroVida.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaInicioSeguroVida.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaInicioSeguroVida.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaInicioSeguroVida.ValidationSettings.ValidationGroup;
                    };

                    this.txtFechaFinSeguroVida.ClientEnabled = true;
                    this.txtFechaFinSeguroVida.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaFinSeguroVida.ValidationSettings.ValidationGroup = "0_1";
                    this.txtFechaFinSeguroVida.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaFinSeguroVida.ValidationSettings.CausesValidation = true;
                    this.txtFechaFinSeguroVida.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaFinSeguroVida.ValidationSettings.Display = Display.Dynamic;
                }
                else
                {
                    this.txtFechaInicioSeguroVida.ClientEnabled = false;
                    this.txtFechaInicioSeguroVida.ValidationSettings.RequiredField.IsRequired = false;

                    this.txtFechaFinSeguroVida.ClientEnabled = false;
                    this.txtFechaFinSeguroVida.ValidationSettings.RequiredField.IsRequired = false;
                }

                if (beneficio1.idBeneficioInteres == 3 || beneficio1.idBeneficioInteres == 4)
                {
                    this.txtFechaInicioBeneficioInteres.ClientEnabled = true;
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.ValidationGroup = "0_1";
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.CausesValidation = true;
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaInicioBeneficioInteres.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaInicioBeneficioInteres.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaInicioBeneficioInteres.ValidationSettings.ValidationGroup;
                    };

                    this.txtFechaFinBeneficioInteres.ClientEnabled = true;
                    this.txtFechaFinBeneficioInteres.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaFinBeneficioInteres.ValidationSettings.ValidationGroup = "0_1";
                    this.txtFechaFinBeneficioInteres.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaFinBeneficioInteres.ValidationSettings.CausesValidation = true;
                    this.txtFechaFinBeneficioInteres.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaFinBeneficioInteres.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaFinBeneficioInteres.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaFinBeneficioInteres.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaFinBeneficioInteres.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtFechaInicioBeneficioInteres.ClientEnabled = false;
                    this.txtFechaInicioBeneficioInteres.ValidationSettings.RequiredField.IsRequired = false;

                    this.txtFechaFinBeneficioInteres.ClientEnabled = false;
                    this.txtFechaFinBeneficioInteres.ValidationSettings.RequiredField.IsRequired = false;
                }

                if (beneficio1.idOtrosBeneficios == 2)
                {
                    this.txtFechaInicioOtroBeneficio.ClientEnabled = true;
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.ValidationGroup = "0_1";
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.CausesValidation = true;
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaInicioOtroBeneficio.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaInicioOtroBeneficio.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaInicioOtroBeneficio.ValidationSettings.ValidationGroup;
                    };

                    this.txtFechaFinOtroBeneficio.ClientEnabled = true;
                    this.txtFechaFinOtroBeneficio.ValidationSettings.RequiredField.IsRequired = true;
                    this.txtFechaFinOtroBeneficio.ValidationSettings.ValidationGroup = "0_1";
                    this.txtFechaFinOtroBeneficio.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
                    this.txtFechaFinOtroBeneficio.ValidationSettings.CausesValidation = true;
                    this.txtFechaFinOtroBeneficio.ValidationSettings.ErrorText = "Se requiere la fecha inicial";
                    this.txtFechaFinOtroBeneficio.ValidationSettings.Display = Display.Dynamic;
                    this.txtFechaFinOtroBeneficio.ClientSideEvents.Validation = "onValidation";

                    this.txtFechaFinOtroBeneficio.CustomJSProperties += (s, es) =>
                    {
                        es.Properties["cpTab"] = txtFechaFinOtroBeneficio.ValidationSettings.ValidationGroup;
                    };
                }
                else
                {
                    this.txtFechaInicioOtroBeneficio.ClientEnabled = false;
                    this.txtFechaInicioOtroBeneficio.ValidationSettings.RequiredField.IsRequired = false;

                    this.txtFechaFinOtroBeneficio.ClientEnabled = false;
                    this.txtFechaFinOtroBeneficio.ValidationSettings.RequiredField.IsRequired = false;
                }
            }

        }

        #endregion
    }
}