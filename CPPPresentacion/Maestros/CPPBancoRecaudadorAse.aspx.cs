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
    using System.Globalization;
    using System.Web.UI.WebControls;

    public partial class CPPBancoRecaudadorAse : System.Web.UI.Page
    {

        #region "definicion variables"

        private NumberFormatInfo formato = new NumberFormatInfo();
        private SSOSesion usuarioCpp = new SSOSesion();
        private Validaciones validar = new Validaciones();
        private DLAccesEntities accesoDatos = null;
        private ParametrizacionSPQUERY parametros = null;
        private ResultConsulta resultadoOperacion = null;
        private BLCodigosCuentaContable CodigoCuentaContableBL = null;
        private BLBancosRecaudadoresAse bancosRecaudadoresAseBl = null;
        private List<EntitiesBancoRecaudadorAse> lstBancoRecaudadoresAseBl = null;
        private BLBancoCuenta bancoCuentaBl = null;
        private List<EntitiesBancoCuenta> lstBancoCuentaBl = null;
        
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
                    this.ConfigurarConsulta();
                    this.ConsultarBancosRecaudadores();
                    this.txtCodigoEntidad.Attributes.Add("onKeyPress", "return soloNumeros(event)");
                    this.txtCodigoEntidad.Attributes.Add("onpaste", "return false");
                    this.txtNit.Attributes.Add("onKeyPress", "return soloNumeros(event)");
                    this.txtNit.Attributes.Add("onpaste", "return false");
                    this.validar.RegLog("Page_Load()", "CPP_SP_ConsultarBancoRecaudadorAseg", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("Page_Load()", "CPP_SP_ConsultarBancoRecaudadorAseg", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (Page_Load): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Page_Load) :: " + ex.Message);
            }
        }

        protected void bntNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxEdit.ClearEditorsInContainer(this.pcBancoRecaudador, "datos", true);
                this.Limpiarobjetos();
                this.CargaCombos();
                this.pcBancoRecaudador.ShowOnPageLoad = true;
                this.validar.RegLog("bntNuevo_Click()", "CPP_SP_ConsultarCodigoCuentaContable", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("bntNuevo_Click()", "N/A", "N/A", "Error al ejecutar el proceso (Page_Load): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Nuevo) :: " + ex.Message);
            }
        }

        protected void gvBancosRecaudadores_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                switch (e.CommandArgs.CommandName)
                {
                    case "Editar":

                        ASPxEdit.ClearEditorsInContainer(this.pcBancoRecaudador, "datos", true);

                        this.accesoDatos = new DLAccesEntities
                        {
                            procedimiento = "CPP_SP_ConsultarBancoRecaudadorAseg",
                            tipoejecucion = 1,
                            parametros = new List<ParametrizacionSPQUERY>()
                        };

                        this.parametros = new ParametrizacionSPQUERY
                        {
                            parametro = "@cppBra_Id",
                            parametroValor = Convert.ToInt32(e.KeyValue.ToString().Split('|')[0])
                        };
                        this.accesoDatos.parametros.Add(this.parametros);

                        this.ConsultarBancoRecaudadoresEdicion();
                        this.validar.RegLog("gvBancosRecaudadores_RowCommand()", "CPP_SP_ConsultarBancoCuenta", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                        this.pcBancoRecaudador.ShowOnPageLoad = true;

                        break;
                    case "MostrarCuentasContables":

                        this.CodigoCuentaContableBL = new BLCodigosCuentaContable();

                        this.accesoDatos = new DLAccesEntities
                        {
                            procedimiento = "CPP_SP_ConsultarBancoCuentaContable",
                            tipoejecucion = 1,
                            parametros = new List<ParametrizacionSPQUERY>()
                        };

                        this.parametros = new ParametrizacionSPQUERY
                        {
                            parametro = "@cppBra_Id",
                            parametroValor = Convert.ToInt32(e.KeyValue.ToString().Split('|')[0])
                        };
                        this.accesoDatos.parametros.Add(this.parametros);

                        this.ckConsulta.DataSource = this.CodigoCuentaContableBL.ConsultarCuentasContables(this.accesoDatos);
                        this.ckConsulta.DataBind();

                        this.validar.RegLog("gvBancosRecaudadores_RowCommand()", "CPP_SP_ConsultarBancoCuentaContable", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                        this.ppCuentasContablesAsociadas.ShowOnPageLoad = true;

                        break;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvBancosRecaudadores_RowCommand()", "N/A", "N/A", "Error al ejecutar el proceso (gvBancosRecaudadores_RowCommand): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvBancosRecaudadores_RowCommand): " + ex.Message);
            }
        }

        protected void gvBancosRecaudadores_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarBancosRecaudadores();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvBancosRecaudadores_PageIndexChanged()", "N/A", "N/A", "Error al ejecutar el proceso (gvBancosRecaudadores_PageIndexChanged): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Paginación) :: " + ex.Message);
            }
        }

        protected void gvBancosRecaudadores_PageSizeChanged(object sender, EventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarBancosRecaudadores();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvBancosRecaudadores_PageSizeChanged()", "N/A", "N/A", "Error al ejecutar el proceso (gvBancosRecaudadores_PageSizeChanged): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ajuste por pagina) :: " + ex.Message);
            }
        }

        protected void gvBancosRecaudadores_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarBancosRecaudadores();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvBancosRecaudadores_BeforeColumnSortingGrouping()", "N/A", "N/A", "Error al ejecutar el proceso (gvBancosRecaudadores_BeforeColumnSortingGrouping): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ordenamiento) :: " + ex.Message);
            }
        }

        protected void Grid_Load(object sender, EventArgs e)
        {
            gvBancosRecaudadores.SettingsBehavior.FilterRowMode = GridViewFilterRowMode.Auto;
            try
            {
                this.ConfigurarConsulta();
                this.ConsultarBancosRecaudadores();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("Grid_Load()", "N/A", "N/A", "Error al ejecutar el proceso (Grid_Load): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ajuste por pagina) :: " + ex.Message);
            }
        }

        /// <summary>
        /// se dispara cuandop se da clic al boton aceptar al registrar un banco recaudador nuevo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                this.EjecucionSQLEditInsert();
                string accion = string.IsNullOrEmpty(this.hdIdBancoRecaudador.Value) ? TipoAccionLog.insercion.ToString() : TipoAccionLog.actualizacion.ToString();
                this.validar.RegLog("btnAceptar_Click()", "CPP_SP_EditarInsertarBancoRecaudadorAse", accion, Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnAceptar_Click()", "N/A", "N/A", "Error al ejecutar el proceso (btnAceptar_Click): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (btnAceptar_Click) :: " + ex.Message);
            }
        }

        #endregion

        #region "metodos privados"

        /// <summary>
        /// carga el combo de las cuentas contables
        /// </summary>
        private void CargaCombos()
        {
            this.CodigoCuentaContableBL = new BLCodigosCuentaContable();
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarCodigoCuentaContable",
                tipoejecucion = 1,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppCCC_IdAplicaCuenta",
                parametroValor = Convert.ToInt32(1)
            };
            this.accesoDatos.parametros.Add(this.parametros);

            ASPxListBox cuentaContableCombo = cbCuentasContablesC.FindControl("cbCuentaContable") as ASPxListBox;

            cuentaContableCombo.DataSource = this.CodigoCuentaContableBL.ConsultarCuentasContables(this.accesoDatos);
            cuentaContableCombo.DataBind();
        }

        /// <summary>
        /// configura la cosulta
        /// </summary>
        private void ConfigurarConsulta()
        {
            this.accesoDatos = new DLAccesEntities();
            this.accesoDatos.procedimiento = "CPP_SP_ConsultarBancoRecaudadorAseg";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();
        }

        /// <summary>
        /// consulta Bancos recaudadores del sistema
        /// </summary>
        private void ConsultarBancosRecaudadores()
        {
            this.bancosRecaudadoresAseBl = new BLBancosRecaudadoresAse();
            this.lstBancoRecaudadoresAseBl = new List<EntitiesBancoRecaudadorAse>();
            this.lstBancoRecaudadoresAseBl = this.bancosRecaudadoresAseBl.ConsultarBancosRecaudadoresAse(this.accesoDatos);

            if (this.lstBancoRecaudadoresAseBl.Count > 0)
            {
                this.gvBancosRecaudadores.DataSource = this.lstBancoRecaudadoresAseBl;
                this.gvBancosRecaudadores.DataBind();
            }
        }

        /// <summary>
        /// consulta Banco Recaudadores Edición
        /// </summary>
        private void ConsultarBancoRecaudadoresEdicion()
        {
            this.bancosRecaudadoresAseBl = new BLBancosRecaudadoresAse();
            this.lstBancoRecaudadoresAseBl = new List<EntitiesBancoRecaudadorAse>();
            this.lstBancoRecaudadoresAseBl = this.bancosRecaudadoresAseBl.ConsultarBancosRecaudadoresAse(this.accesoDatos);

            this.Limpiarobjetos();
            this.hdIdBancoRecaudador.Value = this.lstBancoRecaudadoresAseBl[0].id.ToString();
            this.txtCodigoEntidad.Value = this.lstBancoRecaudadoresAseBl[0].codigoEntidad.ToString();
            this.txtNombreEntidad.Value = this.lstBancoRecaudadoresAseBl[0].nombreEntidad.ToString();
            this.txtNit.Value = this.lstBancoRecaudadoresAseBl[0].nit.ToString();
            this.CargaCombos();

            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarBancoCuenta",
                tipoejecucion = 1,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            this.parametros = new ParametrizacionSPQUERY
            {
                parametro = "@cppBra_Id",
                parametroValor = Convert.ToInt32(hdIdBancoRecaudador.Value)
            };
            this.accesoDatos.parametros.Add(this.parametros);

            this.ConsultarBancoCuentaEdicion();
        }

        /// <summary>
        /// consulta Banco Recaudadores Edición
        /// </summary>
        private void InsertarRelacionBancoCuenta()
        {
            this.bancosRecaudadoresAseBl = new BLBancosRecaudadoresAse();
            this.lstBancoRecaudadoresAseBl = new List<EntitiesBancoRecaudadorAse>();
            this.lstBancoRecaudadoresAseBl = this.bancosRecaudadoresAseBl.ConsultarBancosRecaudadoresAse(this.accesoDatos);


            this.hdIdBancoRecaudador.Value = this.lstBancoRecaudadoresAseBl[0].id.ToString();

            string idCuentas = string.Empty;


            ASPxListBox cuentacontableCombo = cbCuentasContablesC.FindControl("cbCuentaContable") as ASPxListBox;

            foreach (var item in cuentacontableCombo.SelectedValues)
            {
                idCuentas = string.IsNullOrEmpty(idCuentas) ? item.ToString() : idCuentas + "," + item.ToString();
            }

            this.accesoDatos = new DLAccesEntities();
            this.bancoCuentaBl = new BLBancoCuenta();
            this.parametros = new ParametrizacionSPQUERY();

            this.accesoDatos.procedimiento = "CPP_SP_EditarInsertarBancoCuenta";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

            if (!string.IsNullOrEmpty(this.hdIdBancoRecaudador.Value))
            {
                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppBra_Id",
                    parametroValor = Convert.ToInt32(this.hdIdBancoRecaudador.Value.Trim())
                };

                this.accesoDatos.parametros.Add(this.parametros);
            }

            if (!string.IsNullOrEmpty(idCuentas))
            {
                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@idCuentas",
                    parametroValor = idCuentas.Trim()
                };  
                
                this.accesoDatos.parametros.Add(this.parametros);
            }

            OperacionInsertUpdateBancosCuentas();
        }

        /// <summary>
        /// limpia los objetos del form
        /// </summary>
        private void Limpiarobjetos()
        {
            this.hdIdBancoRecaudador.Value = string.Empty;
            this.txtCodigoEntidad.Value = string.Empty;
            this.txtNombreEntidad.Value = string.Empty;
            this.txtNit.Value = string.Empty;

            ASPxListBox CuentaContableCombo = cbCuentasContablesC.FindControl("cbCuentaContable") as ASPxListBox;

            CuentaContableCombo.Items.Clear();
        }

        /// <summary>
        /// se invoca para realizar peticiones a la base de datos (Edicion inserción)
        /// </summary>
        private void EjecucionSQLEditInsert()
        {

            ASPxListBox cuentacontableCombo = cbCuentasContablesC.FindControl("cbCuentaContable") as ASPxListBox;

            if (cuentacontableCombo.SelectedValues.Count> 0)
            {
                this.accesoDatos = new DLAccesEntities();
                this.bancosRecaudadoresAseBl = new BLBancosRecaudadoresAse();
                this.parametros = new ParametrizacionSPQUERY();

                this.accesoDatos.procedimiento = "CPP_SP_EditarInsertarBancoRecaudadorAse";
                this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
                this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

                if (!string.IsNullOrEmpty(this.hdIdBancoRecaudador.Value))
                {
                    this.parametros.parametro = "@cppBra_Id";
                    this.parametros.parametroValor = Convert.ToInt32(this.hdIdBancoRecaudador.Value.Trim());
                    this.accesoDatos.parametros.Add(this.parametros);
                }

                if (!string.IsNullOrEmpty(this.txtCodigoEntidad.Text))
                {
                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppBra_CodigoEntidad",
                        parametroValor = Convert.ToInt32(this.txtCodigoEntidad.Text.Trim())
                    };
                    this.accesoDatos.parametros.Add(this.parametros);
                }

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppBra_NombreEntidad",
                    parametroValor = this.txtNombreEntidad.Text.Trim()
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@cppBra_Nit",
                    parametroValor = Convert.ToInt32(this.txtNit.Text.Trim())
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = string.IsNullOrEmpty(this.hdIdBancoRecaudador.Value) ? "@cppBra_UsuarioCrea" : "@cppBra_UsuarioMod",
                    parametroValor = Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString())
                };
                this.accesoDatos.parametros.Add(this.parametros);

                this.parametros = new ParametrizacionSPQUERY
                {
                    parametro = "@accion",
                    parametroValor = string.IsNullOrEmpty(this.hdIdBancoRecaudador.Value) ? Convert.ToBoolean("true") : Convert.ToBoolean("false")
                };
                this.accesoDatos.parametros.Add(this.parametros);

                if (this.OperacionInsertUpdate())
                {
                    /*this.accesoDatos.procedimiento = "CPP_SP_ConsultarBancoRecaudadorAseg";
                    this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
                    this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

                    if (!string.IsNullOrEmpty(this.txtCodigoEntidad.Text))
                    {
                        this.parametros = new ParametrizacionSPQUERY
                        {
                            parametro = "@cppBra_CodigoEntidad",
                            parametroValor = Convert.ToInt32(this.txtCodigoEntidad.Text.Trim())
                        };
                        this.accesoDatos.parametros.Add(this.parametros);
                    }


                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppBra_NombreEntidad",
                        parametroValor = this.txtNombreEntidad.Text.ToString()
                    };
                    this.accesoDatos.parametros.Add(this.parametros);

                    this.parametros = new ParametrizacionSPQUERY
                    {
                        parametro = "@cppBra_Nit",
                        parametroValor = Convert.ToInt32(this.txtNit.Text.ToString())
                    };
                    this.accesoDatos.parametros.Add(this.parametros);*/

                    this.ConfigurarConsulta();

                    this.ConsultarBancosRecaudadores();

                    this.InsertarRelacionBancoCuenta();

                    this.pcBancoRecaudador.ShowOnPageLoad = false;
                }
            }
            else
            {
                throw (new Exception("BancoRecaudador - RegistrarInsertarBancoRecaudador :: " + " Debe de seleccionar una cuenta"));
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
            this.bancosRecaudadoresAseBl = new BLBancosRecaudadoresAse();
            this.resultadoOperacion = this.bancosRecaudadoresAseBl.RegistrarEditarBancosRecaudadoresAse(this.accesoDatos);

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
        /// realiza la acciion sobre la base de datos
        /// </summary>
        /// <param name="parametros">objeto con la informacion a ejecutar</param>
        /// <param name="accion">true -> insercion o actualizacion, false -> consulta</param>
        /// <returns></returns>
        private bool OperacionInsertUpdateBancosCuentas()
        {
            this.bancoCuentaBl = new BLBancoCuenta();
            this.resultadoOperacion = this.bancoCuentaBl.RegistrarEditarCuentasBancos(this.accesoDatos);

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
        /// consulta Banco Cuenta Relacion
        /// </summary>
        private void ConsultarBancoCuentaEdicion()
        {
            this.bancoCuentaBl = new BLBancoCuenta();
            this.lstBancoCuentaBl = new List<EntitiesBancoCuenta>();
            this.lstBancoCuentaBl = this.bancoCuentaBl.ConsultarBancoCuenta(this.accesoDatos);

            ASPxListBox cuentaContableCombo = cbCuentasContablesC.FindControl("cbCuentaContable") as ASPxListBox;


            foreach (EntitiesBancoCuenta bancoCuenta in lstBancoCuentaBl)
            {
                var list = cuentaContableCombo.Items.FindByValue(bancoCuenta.idCuenta);
                if (list != null)
                {
                    list.Selected = true;
                }
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
        
        #endregion
    }
}