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

    public partial class CPPPlanDePagos : System.Web.UI.Page
    {
        #region "definicion variables"
        private SSOSesion usuarioCpp = new SSOSesion();
        readonly private Validaciones validar = new Validaciones();
        private BLPlanPagos planPagosBL = null;
        private List<EntitiesPlanPagos> lstPlanPagosBl = null;
        private BLBancosRecaudadoresAse intermediarioBL = null;
        private DLAccesEntities accesoDatos = null;
        private ParametrizacionSPQUERY parametros = null;
        private ResultConsulta resultadoOperacion = null;
        
        #endregion

        #region "metodos protegidos"

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.usuarioCpp = (SSOSesion)Session["FINAGRO_SSO_SESION"];
                if (!this.IsPostBack)
                {
                    this.Inicializar();
                    this.CargarIntermediario();
                    this.validar.RegLog("Page_Load()", "CPP_SP_ConsultarBancoRecaudadorAseg", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                    this.CargarTipoPlanPagos();
                    this.validar.RegLog("Page_Load()", "CPP_SP_ConsultarTipoPlanPagos", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                    this.CargarModalidad();
                    this.validar.RegLog("Page_Load()", "CPP_SP_ConsultarModalidadCapital", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                    this.CargarPeriocidadIntereses();
                    this.validar.RegLog("Page_Load()", "CPP_SP_ConsultarPeriocidadIntereses", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("Page_Load()", "CPP_SP_ConsultarBancoRecaudadorAseg - CPP_SP_ConsultarTipoPlanPagos - CPP_SP_ConsultarModalidadCapital - CPP_SP_ConsultarPeriocidadIntereses", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (Page_Load): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Page_Load) :: " + ex.Message);
            }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxEdit.ClearEditorsInContainer(this.pcPlanPagos, "datos", true);
                this.Limpiar();
                this.pcPlanPagos.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("BtnNuevo_Click()", "N/A", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (BtnNuevo_Click): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (BtnNuevo_Click) :: " + ex.Message);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string accion = string.IsNullOrEmpty(this.hdIdPlanPago.Value) ? TipoAccionLog.insercion.ToString() : TipoAccionLog.actualizacion.ToString();
            try
            {
                this.EjeciconSQLEditInser();
                this.validar.RegLog("btnGuardar_Click()", "CPP_SP_EditarInsertarPlanPagos", accion, Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnGuardar_Click()", "CPP_SP_ConsultarBancoRecaudadorAseg - CPP_SP_ConsultarTipoPlanPagos - CPP_SP_ConsultarModalidadCapital - CPP_SP_ConsultarPeriocidadIntereses", accion, "Error al ejecutar el proceso (Page_Load): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (btnAceptar_Click) :: " + ex.Message);
            }
        }

        protected void gvPlanes_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarPlanPago();
                this.validar.RegLog("gvPlanes_PageIndexChanged()", "CPP_SP_ConsultarPlanesPago", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvPlanes_PageIndexChanged()", "CPP_SP_ConsultarPlanesPago", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (Paginación): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Paginación) :: " + ex.Message);
            }
        }

        protected void gvPlanes_PageSizeChanged(object sender, EventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarPlanPago();
                this.validar.RegLog("gvPlanes_PageSizeChanged()", "CPP_SP_ConsultarPlanesPago", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvPlanes_PageSizeChanged()", "CPP_SP_ConsultarPlanesPago", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (Ajuste por pagina): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ajuste por pagina) :: " + ex.Message);
            }
        }

        protected void gvPlanes_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarPlanPago();
                this.validar.RegLog("gvPlanes_BeforeColumnSortingGrouping()", "CPP_SP_ConsultarPlanesPago", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvPlanes_BeforeColumnSortingGrouping()", "CPP_SP_ConsultarPlanesPago", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (Ordenamiento): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ordenamiento) :: " + ex.Message);
            }
        }

        protected void gvBancosRecaudadores_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                switch (e.CommandArgs.CommandName)
                {
                    case "Editar":

                        ASPxEdit.ClearEditorsInContainer(this.pcPlanPagos, "datos", true);

                        this.accesoDatos = new DLAccesEntities
                        {
                            procedimiento = "CPP_SP_ConsultarPlanesPago",
                            tipoejecucion = (int)TiposEjecucion.Procedimiento,
                            parametros = new List<ParametrizacionSPQUERY>()
                        };

                        this.parametros = new ParametrizacionSPQUERY
                        {
                            parametro = "@cppPpa_Id",
                            parametroValor = Convert.ToInt32(e.KeyValue.ToString().Split('|')[0])
                        };
                        this.accesoDatos.parametros.Add(this.parametros);

                        this.ConsultarPlanPagoEditar();
                        this.validar.RegLog("gvBancosRecaudadores_RowCommand()", "CPP_SP_ConsultarPlanesPago", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                        this.pcPlanPagos.ShowOnPageLoad = true;

                        break;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvBancosRecaudadores_RowCommand()", "CPP_SP_ConsultarPlanesPago", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (gvBancosRecaudadores_RowCommand): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvBancosRecaudadores_RowCommand" +"): " + ex.Message);
            }
        }

        protected void Grid_Load(object sender, EventArgs e)
        {
            gvPlanes.SettingsBehavior.FilterRowMode = GridViewFilterRowMode.Auto;
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarPlanPago();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("Grid_Load()", "CPP_SP_ConsultarPlanesPago", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (Grid_Load): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Grid_Load) :: " + ex.Message);
            }
        }

        #endregion

        #region "metodos privados"
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
        /// carga los planes de pagos en el combo
        /// </summary>
        private void CargarTipoPlanPagos()
        {
            this.planPagosBL = new BLPlanPagos();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarTipoPlanPagos",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };
            this.cbTipoPlanPagos.DataSource = this.planPagosBL.ConsultarTipoPlanPagos(this.accesoDatos);
            this.cbTipoPlanPagos.DataBind();
        }

        /// <summary>
        /// carga la modalidad en el combo
        /// </summary>
        private void CargarModalidad()
        {
            this.planPagosBL = new BLPlanPagos();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarModalidadCapital",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };
            this.cbModalidadcapital.DataSource = this.planPagosBL.ConsultarModalidadCapital(this.accesoDatos);
            this.cbModalidadcapital.DataBind();
        }

        /// <summary>
        /// carga el combo de intermediarios
        /// </summary>
        private void CargarIntermediario()
        {
            this.intermediarioBL = new BLBancosRecaudadoresAse();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarBancoRecaudadorAseg",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };
            this.cbIntermediario.DataSource = this.intermediarioBL.ConsultarBancosRecaudadoresAse(this.accesoDatos);
            this.cbIntermediario.DataBind();
        }

        /// <summary>
        /// carga el combo de periocidad Intereses
        /// </summary>
        private void CargarPeriocidadIntereses()
        {
            this.planPagosBL = new BLPlanPagos();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarPeriocidadIntereses",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };
            this.cbPeriocidadIntereses.DataSource = this.planPagosBL.ConsultarPeriocidadInteresesCorrientes(this.accesoDatos);
            this.cbPeriocidadIntereses.DataBind();
        }

        /// <summary>
        /// limpia los objetos de la interfaz
        /// </summary>
        private void Limpiar()
        {
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
        }

        /// <summary>
        /// inicializa los objetos y variables del form
        /// </summary>
        private void Inicializar()
        {
            this.txPeridogracia.Attributes.Add("onKeyPress", "return soloNumeros(event)");
            this.txtPalzoObligacion.Attributes.Add("onKeyPress", "return soloNumeros(event)");
            this.txtCuotasPlanPagos.Attributes.Add("onKeyPress", "return soloNumeros(event)");
            this.txtPeriodoMuerto.Attributes.Add("onKeyPress", "return soloNumeros(event)");
            this.txtPeriodicidadCapital.Attributes.Add("onKeyPress", "return soloNumeros(event)");
            this.txtPuntosContingentes.Attributes.Add("onKeyPress", "return soloNumeros(event)");
        }

        /// <summary>
        /// se invoca para realizar peticiones a la base de datos (Edicion inserción)
        /// </summary>
        private void EjeciconSQLEditInser()
        {

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
                parametroValor = Convert.ToInt32(this.cbIntermediario.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            if (!string.IsNullOrEmpty(this.txtDescuentoAmortizacion.Text))
            {
                this.parametros = new ParametrizacionSPQUERY()
                {
                    parametro = "@cppPpa_DescuentoPorAmortizar",
                    parametroValor = Convert.ToDouble(this.txtDescuentoAmortizacion.Text.Trim())
                };
                this.accesoDatos.parametros.Add(this.parametros);
            }

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_DescuentoAmortizado",
                parametroValor = Convert.ToDouble(this.txtDescuentoAmortizado.Text.Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_ImpuestoTimbre",
                parametroValor = Convert.ToDouble(this.txtImpuestoTimbre.Text.Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_PeriocidadCapital",
                parametroValor = Convert.ToInt32(this.txtPeriodicidadCapital.Text.Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);


            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_PeriodoGracia",
                parametroValor = Convert.ToInt32(this.txPeridogracia.Text.Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_PeriodoMuerto",
                parametroValor = Convert.ToInt32(this.txtPeriodoMuerto.Text.Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_PlazoTotalObligacion",
                parametroValor = Convert.ToInt32(this.txtPalzoObligacion.Text.Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_NumeroCuotasPlanPagos",
                parametroValor = Convert.ToInt32(this.txtCuotasPlanPagos.Text.Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_IdPlanPagos",
                parametroValor = Convert.ToInt32(this.cbTipoPlanPagos.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_IdModalidadCapital",
                parametroValor = Convert.ToInt32(this.cbModalidadcapital.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_IdPeriocidadInteresesCorrientes",
                parametroValor = Convert.ToInt32(this.cbPeriocidadIntereses.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            if (this.txtTasaInteresCorriente.Text.Trim() != "0,00")
            {
                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPpa_TasaInteresesCorrientes",
                    parametroValor = Convert.ToDouble(this.txtTasaInteresCorriente.Text.Trim())
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
                    parametroValor = Convert.ToInt32(this.txtPuntosContingentes.Text.Trim())
                };

                this.accesoDatos.parametros.Add(this.parametros);
            }

            if (this.txtTasaMoratoria.Text.Trim() != "0,00")
            {
                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPpa_TasaInteresesMoratorios",
                    parametroValor = Convert.ToDouble(this.txtTasaMoratoria.Text.Trim())
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
                parametroValor = Convert.ToDateTime(this.txtFecha.Text.Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPpa_Descripcion",
                parametroValor = this.txtDescripcion.Text.Trim()
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

            if (this.OperacionInsertUpdate())
            {
                this.ConfigurarConsulta();
                this.ConsultarPlanPago();
                this.pcPlanPagos.ShowOnPageLoad = false;
            }
        }

        /// <summary>
        /// realiza la acciion sobre la base de datos
        /// </summary>
        /// <param name="parametros">objeto con la informacion a ejecutar</param>
        /// <param name="accion">true -> insercion o actualizacion, false -> consulta</param>
        /// <returns></returns>
        private bool OperacionInsertUpdate()
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
        private void ConfigurarConsulta()
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
            this.lstPlanPagosBl = new List<EntitiesPlanPagos>();
            this.lstPlanPagosBl = this.planPagosBL.ConsultarPlanesPago(this.accesoDatos);

            if (this.lstPlanPagosBl.Count > 0)
            {
                this.gvPlanes.DataSource = this.lstPlanPagosBl;
                this.gvPlanes.DataBind();
            }
        }

        private void ConsultarPlanPagoEditar()
        {
            this.planPagosBL = new BLPlanPagos();
            this.lstPlanPagosBl = new List<EntitiesPlanPagos>();
            this.lstPlanPagosBl = this.planPagosBL.ConsultarPlanesPago(this.accesoDatos);

            this.Limpiar();
            this.hdIdPlanPago.Value = this.lstPlanPagosBl[0].id.ToString();
            this.cbIntermediario.SelectedIndex = this.cbIntermediario.Items.FindByValue(this.lstPlanPagosBl[0].idIntermediario.ToString()).Index;
            this.txPeridogracia.Value = this.lstPlanPagosBl[0].periodoGracia.ToString();
            this.txtPalzoObligacion.Value = this.lstPlanPagosBl[0].plazoTotalObligacion.ToString();
            this.txtPeriodoMuerto.Value = this.lstPlanPagosBl[0].periodoMuerto.ToString();
            this.txtCuotasPlanPagos.Value = this.lstPlanPagosBl[0].numeroCuotasPlanPagos.ToString();
            this.txtDescuentoAmortizacion.Value = this.lstPlanPagosBl[0].descuentoPorAmortizar.ToString();
            this.txtImpuestoTimbre.Value = this.lstPlanPagosBl[0].impuestoTimbre.ToString();
            this.txtDescuentoAmortizado.Value = this.lstPlanPagosBl[0].descuentoAmortizado.ToString();
            this.txtPeriodicidadCapital.Value = this.lstPlanPagosBl[0].periocidadCapital.ToString();
            this.cbTipoPlanPagos.SelectedIndex = this.cbTipoPlanPagos.Items.FindByValue(this.lstPlanPagosBl[0].idPlanPagos.ToString()).Index;
            this.cbPeriocidadIntereses.SelectedIndex = this.cbPeriocidadIntereses.Items.FindByValue(this.lstPlanPagosBl[0].IdperiocidadInteresesCorrientes.ToString()).Index;
            this.cbModalidadcapital.SelectedIndex = this.cbModalidadcapital.Items.FindByValue(this.lstPlanPagosBl[0].idModalidadCapital.ToString()).Index;
            this.txtTasaInteresCorriente.Value = this.lstPlanPagosBl[0].tasaInteresesCorrientes.ToString();
            this.txtFecha.Value = Convert.ToDateTime(this.lstPlanPagosBl[0].fechaPago.ToString());
            this.txtPuntosContingentes.Value = this.lstPlanPagosBl[0].puntosContigentesInt.ToString();
            this.txtTasaMoratoria.Value = this.lstPlanPagosBl[0].tasaInteresesMoratorios.ToString();
            this.txtDescripcion.Value = this.lstPlanPagosBl[0].descripcion.ToString();
        }

        #endregion
    }
}