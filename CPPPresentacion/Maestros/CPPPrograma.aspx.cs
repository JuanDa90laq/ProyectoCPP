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
    public partial class CPPPrograma : System.Web.UI.Page
    {
        #region "definicion variables"

        private NumberFormatInfo formato = new NumberFormatInfo();
        private SSOSesion usuarioCpp = new SSOSesion();
        private Validaciones validar = new Validaciones();
        private DLAccesEntities accesoDatos = null;
        private ParametrizacionSPQUERY parametros = null;
        private ResultConsulta resultadoOperacion = null;
        private BLPlanPagos planPagosBL = null;
        private BLProgama programaBL = null;        
        private List<EntitiesPrograma> lstProgramasBl = null;
        private List<EntitiesProgramaPlanPago> lstProgramaPlanPagoBl = null;
                
        #endregion

        #region "metodos protegidos"
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.usuarioCpp = (SSOSesion)Session["FINAGRO_SSO_SESION"];
                if (!this.IsPostBack)
                {
                    Session["programPlanPago"] = new List<EntitiesProgramaPlanPago>();
                    this.CargaCombos();
                    this.ConfigurarConsulta();
                    this.ConsultarProgramas();
                    this.validar.RegLog("Page_Load()", "CPP_SP_ConsultarCantidadPagares - CPP_SP_ConsultarProgramas", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());                   
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("Page_Load()", "N/A", "N/A", "Error al ejecutar el proceso (Nuevo) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Nuevo) :: " + ex.Message);
            }

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxEdit.ClearEditorsInContainer(this.pcPrograma, "datos", true);
                this.Limpiarobjetos();
                this.pcPrograma.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnNuevo_Click()", "N/A", "N/A", "Error al ejecutar el proceso (Nuevo) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Nuevo) :: " + ex.Message);
            }
        }

        protected void gvProgramas_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                switch (e.CommandArgs.CommandName)
                {
                    case "Editar":
                       
                        ASPxEdit.ClearEditorsInContainer(this.pcPrograma, "datos", true);

                        this.accesoDatos = new DLAccesEntities
                        {
                            procedimiento = "CPP_SP_ConsultarProgramas",
                            tipoejecucion = (int)TiposEjecucion.Procedimiento,
                            parametros = new List<ParametrizacionSPQUERY>()
                        };

                        this.parametros = new ParametrizacionSPQUERY
                        {
                            parametro = "@cppPr_Id",
                            parametroValor = Convert.ToInt32(e.KeyValue.ToString().Split('|')[0])
                        };
                        this.accesoDatos.parametros.Add(this.parametros);

                        this.ConsultarProgramasEdicion();
                        this.validar.RegLog("gvProgramas_RowCommand()", "CPP_SP_ConsultarProgramas", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                        this.pcPrograma.ShowOnPageLoad = true;
                        
                        break;
                    case "Mostrar":

                        this.programaBL = new BLProgama();

                        this.accesoDatos = new DLAccesEntities
                        {
                            procedimiento = "CPP_SP_ConsultarProgramaPlan",
                            tipoejecucion = (int)TiposEjecucion.Procedimiento,
                            parametros = new List<ParametrizacionSPQUERY>()
                        };

                        this.parametros = new ParametrizacionSPQUERY
                        {
                            parametro = "@cppPr_Id",
                            parametroValor = Convert.ToInt32(e.KeyValue.ToString().Split('|')[0])
                        };
                        this.accesoDatos.parametros.Add(this.parametros);

                        this.gvPlanPagosAsociados.DataSource = this.programaBL.ConsultarProgramaPlanPago(this.accesoDatos);
                        this.gvPlanPagosAsociados.DataBind();

                        this.validar.RegLog("gvProgramas_RowCommand()", "CPP_SP_ConsultarProgramaPlan", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                        this.pcPlanPagosVisualizacion.ShowOnPageLoad = true;

                        break;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvProgramas_RowCommand()", "N/A", "N/A", "Error al ejecutar el proceso (gvProgramas_RowCommand): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvProgramas_RowCommand): " + ex.Message);
            }
        }

        protected void gvProgramas_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarProgramas();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvProgramas_PageIndexChanged()", "N/A", "N/A", "Error al ejecutar el proceso (Paginación) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Paginación) :: " + ex.Message);
            }
        }

        protected void gvProgramas_PageSizeChanged(object sender, EventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarProgramas();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvProgramas_PageSizeChanged()", "N/A", "N/A", "Error al ejecutar el proceso (Paginación) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ajuste por pagina) :: " + ex.Message);
            }
        }

        protected void gvProgramas_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarProgramas();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvProgramas_BeforeColumnSortingGrouping()", "N/A", "N/A", "Error al ejecutar el proceso (Paginación) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ordenamiento) :: " + ex.Message);
            }
        }

        protected void Grid_Load(object sender, EventArgs e)
        {
            gvProgramas.SettingsBehavior.FilterRowMode = GridViewFilterRowMode.Auto;
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarProgramas();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("Grid_Load()", "N/A", "N/A", "Error al ejecutar el proceso (Paginación) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ajuste por pagina) :: " + ex.Message);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                EntitiesProgramaPlanPago programaPlanPago = new EntitiesProgramaPlanPago()
                {
                    idPlanPago = Convert.ToInt32(cbPlanPago.SelectedItem.Value.ToString()),
                    planPago = cbPlanPago.SelectedItem.Text,
                    convenio = txtConvenio.Text.ToString().Trim()
                };

                List<EntitiesProgramaPlanPago> listProgramaPlanPago = null;

                if (Session["programPlanPago"] == null)
                {
                    listProgramaPlanPago = new List<EntitiesProgramaPlanPago>();
                }
                else
                {
                    listProgramaPlanPago = (List<EntitiesProgramaPlanPago>)Session["programPlanPago"];
                }

                listProgramaPlanPago.Add(programaPlanPago);

                Session["programPlanPago"] = listProgramaPlanPago;

                gvPlanPagoPrograma.DataSource = Session["programPlanPago"];
                gvPlanPagoPrograma.DataBind();


                List<EntitiesPlanPagos> listPlanPagos = ((List<EntitiesPlanPagos>)Session["planesPago"]);

                EntitiesPlanPagos planPagos = listPlanPagos.SingleOrDefault(x => x.id == Convert.ToInt32(cbPlanPago.SelectedItem.Value.ToString()));
                listPlanPagos.Remove(planPagos);


                Session["planesPago"] = listPlanPagos.ToList();

                this.cbPlanPago.DataSource = Session["planesPago"];
                this.cbPlanPago.DataBind();

                this.cbPlanPago.SelectedIndex = -1;
                this.txtConvenio.Value = string.Empty;
            }
            catch(Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnAceptar_Click()", "N/A", "N/A", "Error al ejecutar el proceso (btnAceptar_Click) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (btnAceptar_Click) :: " + ex.Message);
            }
            
        }

        protected void gvPlanPagoPrograma_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                switch (e.CommandArgs.CommandName)
                {
                    case "Eliminar":

                        List<EntitiesProgramaPlanPago> listProgramaPlanPago = (List<EntitiesProgramaPlanPago>)Session["programPlanPago"];

                        EntitiesProgramaPlanPago programaPlanPagos = listProgramaPlanPago.SingleOrDefault(x => x.planPago == e.KeyValue.ToString());

                        listProgramaPlanPago.Remove(programaPlanPagos);

                        Session["programPlanPago"] = listProgramaPlanPago.ToList();

                        gvPlanPagoPrograma.DataSource = Session["programPlanPago"];
                        gvPlanPagoPrograma.DataBind();

                        EntitiesPlanPagos PlanPagos = ((List<EntitiesPlanPagos>)Session["planesPagoOriginal"]).SingleOrDefault(x => x.id == programaPlanPagos.idPlanPago);

                        List<EntitiesPlanPagos> listPlanPagos = ((List<EntitiesPlanPagos>)Session["planesPago"]);
                        listPlanPagos.Add(PlanPagos);

                        Session["planesPago"] = listPlanPagos.ToList().OrderBy(x => x.id).ToList();

                        this.cbPlanPago.DataSource = Session["planesPago"];
                        this.cbPlanPago.DataBind();

                        this.cbPlanPago.SelectedIndex = -1;
                        this.txtConvenio.Value = string.Empty;

                        break;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvPlanPagoPrograma_RowCommand()", "N/A", "N/A", "Error al ejecutar el proceso (gvPlanPagoPrograma_RowCommand) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvPlanPagoPrograma_RowCommand) :: " + ex.Message);
            }
            
        }

        protected void btnCerrarPlanes_Click(object sender, EventArgs e)
        {
            try
            {
                pcPlanPagos.ShowOnPageLoad = false;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnCerrarPlanes_Click()", "N/A", "N/A", "Error al ejecutar el proceso (btnCerrarPlanes_Click) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (btnCerrarPlanes_Click) :: " + ex.Message);
            }

            
        }

        protected void btnGuardaPlanes_Click(object sender, EventArgs e)
        {
            try
            {
                if (((List<EntitiesProgramaPlanPago>)Session["programPlanPago"]).Count == 0)
                {
                    this.ShowMensajes(0, "No hay planes de pagos asociados por favor valide");
                }
                else
                {
                    pcPlanPagos.ShowOnPageLoad = false;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnGuardaPlanes_Click()", "N/A", "N/A", "Error al ejecutar el proceso (btnGuardaPlanes_Click) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (btnGuardaPlanes_Click) :: " + ex.Message);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                btnGuardar.ClientEnabled = false;
                this.EjeciconSQLEditInser();
                string accion = string.IsNullOrEmpty(this.hdIdPrograma.Value) ? TipoAccionLog.insercion.ToString() : TipoAccionLog.actualizacion.ToString();
                this.validar.RegLog("btnGuardar_Click", "CPP_SP_EditarInsertarCodigoCuentaContable", accion, Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnGuardar_Click()", "N/A", "N/A", "Error al ejecutar el proceso (btnGuardar_Click) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (btnGuardar_Click) :: " + ex.Message);
            }
        }

        #endregion

        #region "metodos privados"
        /// <summary>
        /// carga los combos del formulario
        /// </summary>
        private void CargaCombos()
        {
            this.programaBL = new BLProgama();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarCantidadPagares",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.cbPagares.DataSource = this.programaBL.ConsultarCantidadPagares(this.accesoDatos);
            this.cbPagares.DataBind();

            this.programaBL = new BLProgama();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarCentroUtilidad",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.cbCentroUtilidad.DataSource = this.programaBL.ConsultarCentroUtilidad(this.accesoDatos);
            this.cbCentroUtilidad.DataBind();

            this.planPagosBL = new BLPlanPagos();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarPlanesPago",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            Session["planesPago"] = this.planPagosBL.ConsultarPlanesPago(this.accesoDatos);
            Session["planesPagoOriginal"] = this.planPagosBL.ConsultarPlanesPago(this.accesoDatos);
            this.cbPlanPago.DataSource = Session["planesPago"];
            this.cbPlanPago.DataBind();
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
                procedimiento = "CPP_SP_ConsultarProgramas",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };
        }

        /// <summary>
        /// consulta codigos cuentas contables
        /// </summary>
        private void ConsultarProgramas()
        {
            this.programaBL = new BLProgama();
            this.lstProgramasBl = new List<EntitiesPrograma>();
            this.lstProgramasBl = this.programaBL.ConsultarProgramas(this.accesoDatos);

            if (this.lstProgramasBl.Count > 0)
            {
                this.gvProgramas.DataSource = this.lstProgramasBl;
                this.gvProgramas.DataBind();
            }
        }

        /// <summary>
        /// limpia los objetos del form
        /// </summary>
        private void Limpiarobjetos()
        {
            this.CargaCombos();
            this.hdIdPrograma.Value = string.Empty;
            this.cbCentroUtilidad.SelectedIndex = -1;
            this.cbPagares.SelectedIndex = -1;
            this.txtNombrePrograma.Value = string.Empty;
            this.txtFechaInicial.Value = string.Empty;
            this.txtDescripcion.Value = string.Empty;
            Session["programPlanPago"] = new List<EntitiesProgramaPlanPago>();

            gvPlanPagoPrograma.DataSource = Session["ProgramPlanPago"];
            gvPlanPagoPrograma.DataBind();

        }

        /// <summary>
        /// se invoca para realizar peticiones a la base de datos (Edicion inserción)
        /// </summary>
        private void EjeciconSQLEditInser()
        {
            if (((List<EntitiesProgramaPlanPago>)Session["programPlanPago"]).Count == 0)
            {
                this.ShowMensajes(0, "No hay planes de pagos asociados por favor valide");
            }
            else
            {
                this.accesoDatos = new DLAccesEntities();
                this.programaBL = new BLProgama();
                this.parametros = new ParametrizacionSPQUERY();

                this.accesoDatos.procedimiento = "CPP_SP_EditarInsertarProgramas";
                this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
                this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

                if (!string.IsNullOrEmpty(this.hdIdPrograma.Value))
                {
                    this.parametros.parametro = "@cppPr_Id";
                    this.parametros.parametroValor = Convert.ToInt32(this.hdIdPrograma.Value.Trim());
                    this.accesoDatos.parametros.Add(this.parametros);
                }

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPr_Nombre",
                    parametroValor = this.txtNombrePrograma.Text.ToString().Trim()
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPr_FechaInicial",
                    parametroValor = Convert.ToDateTime(this.txtFechaInicial.Text.ToString())
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPr_IdCantidadPagares",
                    parametroValor = Convert.ToInt32(this.cbPagares.SelectedItem.Value.ToString().Trim())
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPr_IdCentroUtilidad",
                    parametroValor = Convert.ToInt32(this.cbCentroUtilidad.SelectedItem.Value.ToString().Trim())
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppPr_Descripcion",
                    parametroValor = this.txtDescripcion.Text.ToString().Trim()
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = string.IsNullOrEmpty(this.hdIdPrograma.Value) ? "@cppPr_UsuarioCrea" : "@cppPr_UsuarioMod",
                    parametroValor = Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString())
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@accion",
                    parametroValor = string.IsNullOrEmpty(this.hdIdPrograma.Value) ? Convert.ToBoolean("true") : Convert.ToBoolean("false")
                };
                this.accesoDatos.parametros.Add(this.parametros);

                if (this.OperacionInsertUpdate())
                {

                   /* this.accesoDatos.procedimiento = "CPP_SP_ConsultarProgramas";
                    this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
                    this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppPr_Nombre",
                        parametroValor = this.txtNombrePrograma.Text.ToString().Trim()
                    };
                    this.accesoDatos.parametros.Add(this.parametros);

                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppPr_FechaInicial",
                        parametroValor = Convert.ToDateTime(this.txtFechaInicial.Text.ToString())
                    };
                    this.accesoDatos.parametros.Add(this.parametros);

                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppPr_IdCantidadPagares",
                        parametroValor = Convert.ToInt32(this.cbPagares.SelectedItem.Value.ToString().Trim())
                    };
                    this.accesoDatos.parametros.Add(this.parametros);

                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppPr_IdCentroUtilidad",
                        parametroValor = Convert.ToInt32(this.cbCentroUtilidad.SelectedItem.Value.ToString().Trim())
                    };
                    this.accesoDatos.parametros.Add(this.parametros);

                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppPr_Descripcion",
                        parametroValor = this.txtDescripcion.Text.ToString().Trim()
                    };
                    this.accesoDatos.parametros.Add(this.parametros);*/

                    this.ConfigurarConsulta();

                    this.ConsultarProgramas();

                    this.InsertarRelacionProgramaPlan();

                    Session["programPlanPago"] = new List<EntitiesProgramaPlanPago>();

                    this.pcPrograma.ShowOnPageLoad = false;
                }
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
            this.programaBL = new BLProgama();
            this.resultadoOperacion = this.programaBL.RegistrarEditarProgramas(this.accesoDatos);

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
        private void InsertarRelacionProgramaPlan()
        {
            this.programaBL = new BLProgama();
            this.lstProgramasBl = new List<EntitiesPrograma>();
            this.lstProgramasBl = this.programaBL.ConsultarProgramas(this.accesoDatos);

            this.hdIdPrograma.Value = this.lstProgramasBl[0].id.ToString();

            this.accesoDatos.procedimiento = "CPP_SP_EliminarProgramaPlanPago";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();
            
            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPr_Id",
                parametroValor = Convert.ToInt32(this.hdIdPrograma.Value.Trim())
            };

            this.accesoDatos.parametros.Add(this.parametros);

            if (OperacionInsertUpdate())
            {
                var listaPlanesPago = (List<EntitiesProgramaPlanPago>)Session["programPlanPago"];

                foreach (var planesPago in listaPlanesPago)
                {
                    this.accesoDatos.procedimiento = "CPP_SP_EditarInsertarProgramaPlanCuenta";
                    this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
                    this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppPr_Id",
                        parametroValor = Convert.ToInt32(this.hdIdPrograma.Value.Trim())
                    };

                    this.accesoDatos.parametros.Add(this.parametros);

                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppPp_Id",
                        parametroValor = Convert.ToInt32(planesPago.idPlanPago)
                    };

                    this.accesoDatos.parametros.Add(this.parametros);

                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppPrPp_Convenio",
                        parametroValor = planesPago.convenio.ToString()
                    };

                    this.accesoDatos.parametros.Add(this.parametros);

                    OperacionInsertUpdate();
                }
            }
        }

        /// <summary>
        /// consulta Programa Edicion
        /// </summary>
        private void ConsultarProgramasEdicion()
        {
            this.programaBL = new BLProgama();
            this.lstProgramasBl = new List<EntitiesPrograma>();
            this.lstProgramasBl = this.programaBL.ConsultarProgramas(this.accesoDatos);

            this.Limpiarobjetos();
            this.hdIdPrograma.Value = this.lstProgramasBl[0].id.ToString();
            this.txtFechaInicial.Value = Convert.ToDateTime(this.lstProgramasBl[0].fechaInicial.ToString());
            this.txtNombrePrograma.Value = this.lstProgramasBl[0].nombre.ToString();
            this.txtDescripcion.Value = this.lstProgramasBl[0].descripcion.ToString();
            this.cbCentroUtilidad.SelectedIndex = this.cbCentroUtilidad.Items.FindByValue(this.lstProgramasBl[0].idCentroUtilidad.ToString()).Index;
            this.cbPagares.SelectedIndex = this.cbPagares.Items.FindByValue(this.lstProgramasBl[0].idPagares.ToString()).Index;

            
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarProgramaPlan",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppPr_Id",
                parametroValor = Convert.ToInt32(hdIdPrograma.Value)
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.ConsultarProgramaPlanEdicion();
        }

        /// <summary>
        /// consulta Programa plan edicion
        /// </summary>
        private void ConsultarProgramaPlanEdicion()
        {
            this.programaBL = new BLProgama();
            this.lstProgramaPlanPagoBl = new List<EntitiesProgramaPlanPago>();
            this.lstProgramaPlanPagoBl = this.programaBL.ConsultarProgramaPlanPago(this.accesoDatos);

            Session["programPlanPago"] = lstProgramaPlanPagoBl;

            gvPlanPagoPrograma.DataSource = Session["programPlanPago"];
            gvPlanPagoPrograma.DataBind();

            List<EntitiesPlanPagos> listPlanPagos = ((List<EntitiesPlanPagos>)Session["planesPago"]);

            foreach (EntitiesProgramaPlanPago programaPlanPago in lstProgramaPlanPagoBl)
            {
                EntitiesPlanPagos planPagos = listPlanPagos.SingleOrDefault(x => x.id == Convert.ToInt32(programaPlanPago.idPlanPago));
                listPlanPagos.Remove(planPagos);
            }

            Session["planesPago"] = listPlanPagos.ToList();

            this.cbPlanPago.DataSource = Session["planesPago"];
            this.cbPlanPago.DataBind();

            this.cbPlanPago.SelectedIndex = -1;
            this.txtConvenio.Value = string.Empty;
        }

        #endregion       
    }
}