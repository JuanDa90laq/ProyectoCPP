namespace CPPPresentacion.Maestros
{
    using CPPBL.Maestros;
    using CPPBL.Transversal;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using CPPENTL.Enumeraciones;
    using CPPPresentacion.Generica;
    using DevExpress.Web;
    using SSO.Finagro;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Web.UI.WebControls;

    public partial class CCPCodigoCuentaContable : System.Web.UI.Page
    {

        #region "definicion variables"
        readonly private BLTiposCuenta tiposCuentaBL = new BLTiposCuenta();
        private NumberFormatInfo formato = new NumberFormatInfo();
        private SSOSesion usuarioCpp = new SSOSesion();
        private Validaciones validar = new Validaciones();
        private DLAccesEntities accesoDatos = null;
        private ParametrizacionSPQUERY parametros = null;
        private ResultConsulta resultadoOperacion = null;
        private BLTipoCesion tipoCesionBL = null;
        private BLTipoCuenta tipoCuentaBL = null;
        private BLAplicaCuenta aplicaCuentaBL = null;
        private BLCalificacion calificacionBL = null;
        private BLCodigosCuentaContable codigoCuentaContableBl = null;
        private List<EntitiesCodigosCuentaContable> lstCodigoCuentaContableBl = null;
        
        #endregion

        #region "metodos protegidos"

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                this.formato.NumberDecimalSeparator = ".";
                this.formato.NumberGroupSeparator = ",";
                this.usuarioCpp = (SSOSesion)Session["FINAGRO_SSO_SESION"];
                if (!this.IsPostBack)
                {
                    this.CargaCombos();
                    this.ConfigurarConsulta();
                    this.ConsultarCodigoCuentaContable();
                    this.validar.RegLog("Page_Load()", "CPP_SP_ConsultarCodigoCuentaContable", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                    this.CargarTiposCuenta();
                    this.validar.RegLog("Page_Load()", "CPP_SP_ConsultarTiposCuenta", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                    this.txtCodigoCuenta.Attributes.Add("onKeyPress", "return soloNumeros(event)");
                    this.txtCodigoCuenta.Attributes.Add("onpaste", "return false");                    
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
                ASPxEdit.ClearEditorsInContainer(this.pcCuentaContable, "datos", true);
                this.Limpiarobjetos();
                this.pcCuentaContable.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnNuevo_Click()", "N/A", "N/A", "Error al ejecutar el proceso (Nuevo) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());                 
                this.ShowMensajes(0, "Error al ejecutar el proceso (Nuevo) :: " + ex.Message);                
            }
        }

        protected void gvCuentasContables_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                ASPxButton btnEditar = new ASPxButton();
                ASPxButton btnEliminar = new ASPxButton();
                ASPxLabel lbIdentificadorAgro = new ASPxLabel();
                ASPxLabel lbActividad = new ASPxLabel();
                ASPxLabel lbId = new ASPxLabel();
                ASPxTextBox txtActividad = new ASPxTextBox();
                ASPxCheckBox ckEstadoIni = new ASPxCheckBox();
                RequiredFieldValidator validador = new RequiredFieldValidator();

                switch (e.CommandArgs.CommandName)
                {
                    case "Editar":

                        ASPxEdit.ClearEditorsInContainer(this.pcCuentaContable, "datos", true);

                        this.accesoDatos = new DLAccesEntities
                        {
                            procedimiento = "CPP_SP_ConsultarCodigoCuentaContable",
                            tipoejecucion = 1,
                            parametros = new List<ParametrizacionSPQUERY>()
                        };

                        this.parametros = new ParametrizacionSPQUERY
                        {
                            parametro = "@cppCCC_Id",
                            parametroValor = Convert.ToInt32(e.KeyValue.ToString().Split('|')[0])
                        };
                        this.accesoDatos.parametros.Add(this.parametros);

                        this.ConsultarCodigoCuentaEdicion();
                        this.pcCuentaContable.ShowOnPageLoad = true;
                        
                        break;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvCuentasContables_RowCommand()", "N/A", "N/A", "Error al ejecutar el proceso (gvCuentasContables_RowCommand): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());                
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvCuentasContables_RowCommand): " + ex.Message);
            }
        }

        protected void gvCuentasContables_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarCodigoCuentaContable();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvCuentasContables_PageIndexChanged()", "N/A", "N/A", "Error al ejecutar el proceso (Paginación) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Paginación) :: " + ex.Message);
            }
        }

        protected void gvCuentasContables_PageSizeChanged(object sender, EventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarCodigoCuentaContable();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvCuentasContables_PageSizeChanged()", "N/A", "N/A", "Error al ejecutar el proceso (Paginación) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ajuste por pagina) :: " + ex.Message);
            }
        }

        protected void gvCuentasContables_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarCodigoCuentaContable();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvCuentasContables_BeforeColumnSortingGrouping()", "N/A", "N/A", "Error al ejecutar el proceso (Paginación) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ordenamiento) :: " + ex.Message);
            }
        }

        protected void Grid_Load(object sender, EventArgs e)
        {
            gvCuentasContables.SettingsBehavior.FilterRowMode = GridViewFilterRowMode.Auto;
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarCodigoCuentaContable();
                ////this.RegLog("Grid_Load()", "CPP_SP_ConsultarCodigoCuentaContable", TipoAccionLog.consulta.ToString());
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("Grid_Load()", "N/A", "N/A", "Error al ejecutar el proceso (Paginación) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ajuste por pagina) :: " + ex.Message);
            }
        }

        /// <summary>
        /// se dispara cuandop se da clic al boton aceptar al registrar un codigo cuenta contable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                this.EjeciconSQLEditInser();
                string accion = string.IsNullOrEmpty(this.hdIdCuentaContable.Value) ? TipoAccionLog.insercion.ToString() : TipoAccionLog.actualizacion.ToString();
                this.validar.RegLog("btnAceptar_Click", "CPP_SP_EditarInsertarCodigoCuentaContable", accion, Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnAceptar_Click()", "N/A", "N/A", "Error al ejecutar el proceso (btnAceptar_Click) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (btnAceptar_Click) :: " + ex.Message);
            }
        }

        #endregion

        #region "metodos privados"

        /// <summary>
        /// carga el combo que filtra las actividades
        /// </summary>
        private void CargaCombos()
        {
            this.tipoCesionBL = new BLTipoCesion();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarTipoCesion",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.cbTipoCesion.DataSource = this.tipoCesionBL.ConsultaTipoCesion(this.accesoDatos);
            this.cbTipoCesion.DataBind();

            this.tipoCuentaBL = new BLTipoCuenta();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarTipoCuenta",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.cbTipoCuenta.DataSource = this.tipoCuentaBL.ConsultaTipoCuenta(this.accesoDatos);
            this.cbTipoCuenta.DataBind();


            this.calificacionBL = new BLCalificacion();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarCalificaciones",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.cbCalificacion.DataSource = this.calificacionBL.ConsultaCalificacion(this.accesoDatos);
            this.cbCalificacion.DataBind();

            this.aplicaCuentaBL = new BLAplicaCuenta();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarAplicaCuenta",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.cbAplica.DataSource = this.aplicaCuentaBL.ConsultaAplicaCuenta(this.accesoDatos);
            this.cbAplica.DataBind();

        }

        /// <summary>
        /// consulta cuentas contables Edición
        /// </summary>
        private void ConsultarCodigoCuentaEdicion()
        {
            this.codigoCuentaContableBl = new BLCodigosCuentaContable();
            this.lstCodigoCuentaContableBl = new List<EntitiesCodigosCuentaContable>();
            this.lstCodigoCuentaContableBl = this.codigoCuentaContableBl.ConsultarCuentasContables(this.accesoDatos);

            this.Limpiarobjetos();
            this.hdIdCuentaContable.Value = this.lstCodigoCuentaContableBl[0].id.ToString();
            this.cbTipoCesion.SelectedIndex = this.cbTipoCesion.Items.FindByText(this.lstCodigoCuentaContableBl[0].cesion.ToString()).Index;
            this.cbTipoCuenta.SelectedIndex = this.cbTipoCuenta.Items.FindByText(this.lstCodigoCuentaContableBl[0].cuenta.ToString()).Index;
            this.rdEfectuaMovimiento.Value = this.lstCodigoCuentaContableBl[0].efectuaMovimiento;
            this.cbCalificacion.SelectedIndex = this.cbCalificacion.Items.FindByText(this.lstCodigoCuentaContableBl[0].calificacion.ToString()).Index;
            this.txtCodigoCuenta.Value = this.lstCodigoCuentaContableBl[0].codigoCuenta.ToString();
            ///this.txtNombreCuenta.Value = this.lstCodigoCuentaContableBl[0].nombreCuenta.ToString();
            this.cbCuenta.SelectedIndex = this.cbCuenta.Items.FindByValue(this.lstCodigoCuentaContableBl[0].cppCCC_IdCuenta.ToString()).Index;
            this.cbAplica.SelectedIndex = this.cbAplica.Items.FindByText(this.lstCodigoCuentaContableBl[0].aplicaCuenta.ToString()).Index;
        }

        /// <summary>
        /// li´mpia los objetos del form
        /// </summary>
        private void Limpiarobjetos()
        {
            this.hdIdCuentaContable.Value = string.Empty;
            this.cbTipoCesion.SelectedIndex = -1;
            this.cbTipoCuenta.SelectedIndex = -1;
            this.txtCodigoCuenta.Value = string.Empty;
            //this.txtNombreCuenta.Value = string.Empty;
            this.rdEfectuaMovimiento.Value = false;
            this.cbCalificacion.SelectedIndex = -1;
            this.cbAplica.SelectedIndex = -1;
            this.cbCuenta.SelectedIndex = -1;
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
            this.accesoDatos = new DLAccesEntities();
            this.accesoDatos.procedimiento = "CPP_SP_ConsultarCodigoCuentaContable";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();
        }

        /// <summary>
        /// consulta codigos cuentas contables
        /// </summary>
        private void ConsultarCodigoCuentaContable()
        {
            this.codigoCuentaContableBl = new BLCodigosCuentaContable();
            this.lstCodigoCuentaContableBl = new List<EntitiesCodigosCuentaContable>();
            this.lstCodigoCuentaContableBl = this.codigoCuentaContableBl.ConsultarCuentasContables(this.accesoDatos);

            if (this.lstCodigoCuentaContableBl.Count > 0)
            {
                this.gvCuentasContables.DataSource = this.lstCodigoCuentaContableBl;
                this.gvCuentasContables.DataBind();

            }
        }

        /// <summary>
        /// se invoca para realizar peticiones a la base de datos (Edicion inserción)
        /// </summary>
        private void EjeciconSQLEditInser()
        {
            this.accesoDatos = new DLAccesEntities();
            this.codigoCuentaContableBl = new BLCodigosCuentaContable();
            this.parametros = new ParametrizacionSPQUERY();

            this.accesoDatos.procedimiento = "CPP_SP_EditarInsertarCodigoCuentaContable";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

            if (!string.IsNullOrEmpty(this.hdIdCuentaContable.Value))
            {
                this.parametros.parametro = "@cppCCC_Id";
                this.parametros.parametroValor = Convert.ToInt32(this.hdIdCuentaContable.Value.Trim());
                this.accesoDatos.parametros.Add(this.parametros);
            }

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppCCC_IdTipoCesion",
                parametroValor = Convert.ToInt32(this.cbTipoCesion.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppCCC_IdTipoCuenta",
                parametroValor = Convert.ToInt32(this.cbTipoCuenta.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppCCC_CodigoCuenta",
                parametroValor = this.txtCodigoCuenta.Text.Trim()
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppCCC_NombreCuenta",
                parametroValor = this.cbCuenta.SelectedItem.Text
                ///parametroValor = this.txtNombreCuenta.Text.Trim()
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppCCC_EfectuaMov",
                parametroValor = this.rdEfectuaMovimiento.Value
            };
            this.accesoDatos.parametros.Add(this.parametros);


            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppCCC_IdCalificacion",
                parametroValor = Convert.ToInt32(this.cbCalificacion.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = string.IsNullOrEmpty(this.hdIdCuentaContable.Value) ? "@cppCCC_UsuarioCrea" : "@cppCCC_UsuarioMod",
                parametroValor = Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppCCC_IdAplicaCuenta",
                parametroValor = Convert.ToInt32(this.cbAplica.SelectedItem.Value.ToString().Trim())
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@accion",
                parametroValor = string.IsNullOrEmpty(this.hdIdCuentaContable.Value) ? Convert.ToBoolean("true") : Convert.ToBoolean("false")
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppCCC_IdCuenta",
                parametroValor = Convert.ToInt32(this.cbCuenta.SelectedItem.Value)                
            };
            this.accesoDatos.parametros.Add(this.parametros);

            if (this.OperacionInsertUpdate())
            {
                this.ConfigurarConsulta();
                this.ConsultarCodigoCuentaContable();
                this.pcCuentaContable.ShowOnPageLoad = false;
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
            this.codigoCuentaContableBl = new BLCodigosCuentaContable();
            this.resultadoOperacion = this.codigoCuentaContableBl.RegistrarEditarCodigosCuentasContables(this.accesoDatos);

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
        /// carga el combo del tipo de las cuentas 
        /// </summary>
        private void CargarTiposCuenta()
        {
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarTiposCuenta",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.cbCuenta.DataSource = this.tiposCuentaBL.ConsultaTiposCuenta(this.accesoDatos);
            this.cbCuenta.DataBind();
        }

        #endregion

    }
}