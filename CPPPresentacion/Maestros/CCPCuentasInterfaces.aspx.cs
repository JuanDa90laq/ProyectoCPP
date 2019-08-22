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
    using System.Linq;
    using System.Web.UI.WebControls;

    public partial class CCPCuentasInterfaces : System.Web.UI.Page
    {
        #region "definicion variables"

        readonly private BLTiposCuenta tiposCuentaBL = new BLTiposCuenta();
        readonly private BLTipoCesion tipoCesionBL = new BLTipoCesion();
        readonly private BLTipoCuenta tipoCuentaBL = new BLTipoCuenta();
        readonly private BLAplicaCuenta aplicaCuentaBL = new BLAplicaCuenta();
        readonly private BLInterfazContable interfazBL = new BLInterfazContable();
        readonly private BLInterfazCuenta interfazCuentaBL = new BLInterfazCuenta();
        readonly private BLCalificacion calificacionBL = new BLCalificacion();
        readonly private BLCodigosCuentaContable codigoCuentaContableBl = new BLCodigosCuentaContable();
        private SSOSesion usuarioCpp = new SSOSesion();
        private Validaciones validar = new Validaciones();
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
                this.CargarInfoParaCombos();

                if (!this.IsPostBack)
                {   
                    this.CargarInterfazCuentas(0);
                    this.validar.RegLog("Page_Load()", "CPP_SP_ConsultarInterfazCuenta", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("Page_Load()", "N/A", "N/A", "Error al ejecutar el proceso (Page_Load) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Page_Load) :: " + ex.Message);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                this.LimpiarObjetos();
                ASPxEdit.ClearEditorsInContainer(this.pcInterfazCuenta, "datos", true);
                this.pcInterfazCuenta.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnNuevo_Click()", "N/A", "N/A", "Error al ejecutar el proceso (Nuevo) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Nuevo) :: " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo que se llamada del lado del cliente para realizar la operacion de llenar las cuentas
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void Cuentas_Callback(object source, CallbackEventArgsBase e)
        {
            try
            {
                string[] parateros = e.Parameter.Split(',');
                this.CuentasCombo(parateros[0], parateros[1], parateros[2]);
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("Cuentas_Callback()", "N/A", "N/A", "Error al ejecutar el proceso (Cuentas_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Cuentas_Callback): " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.hdValores.Value))
                {
                    string accion = string.IsNullOrEmpty(this.hdIdInterfazCuenta.Value) ? TipoAccionLog.insercion.ToString() : TipoAccionLog.actualizacion.ToString();
                    this.SetInsertEdit(string.IsNullOrEmpty(this.hdIdInterfazCuenta.Value) ? 0 : 1);
                    this.validar.RegLog("btnGuardar_Click()", "CPP_SP_ConsultarInterfazCuenta", accion, Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                }
                else
                {
                    this.Cuentas_CallbackPriv();
                    this.ShowMensajes(2, "Seleccione almenos una cuenta.");
                }

            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnGuardar_Click()", "N/A", "N/A", "Error al ejecutar el proceso (btnGuardar_Click): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (btnGuardar_Click): " + ex.Message);
            }
        }

        protected void gvInterfacesCuentas_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
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

                        ASPxEdit.ClearEditorsInContainer(this.pcInterfazCuenta, "datos", true);
                        this.CargarInterfazCuentas(Convert.ToInt32(e.KeyValue.ToString().Split('|')[0]));
                        this.pcInterfazCuenta.ShowOnPageLoad = true;

                        break;

                    case "MostrarCuentas":

                        List<EntitiesActividadesAgropecuarias> lstActivi = new List<EntitiesActividadesAgropecuarias>();

                        string[] idActividades = e.KeyValue.ToString().Split('|')[1].Split(',');

                        List<EntitiesCodigosCuentaContable> lstCodigoCuentaContable = new List<EntitiesCodigosCuentaContable>();
                        List<EntitiesCodigosCuentaContable> lstCodigoCuentaContable2 = new List<EntitiesCodigosCuentaContable>();
                        lstCodigoCuentaContable = (List<EntitiesCodigosCuentaContable>)Session["CuentasContables"];

                        foreach (string id in idActividades)
                        {
                            lstCodigoCuentaContable2.Add(lstCodigoCuentaContable.Where(a => a.id.Equals(Convert.ToInt32(id))).FirstOrDefault());
                        }

                        this.CbCuetnasContablesAsoc.DataSource = lstCodigoCuentaContable2;
                        this.CbCuetnasContablesAsoc.DataBind();
                        this.ppCuentasAsociadas.ShowOnPageLoad = true;

                        break;

                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvBeneficiarios_RowCommand()", "N/A", "N/A", "Error al ejecutar el proceso (gvBeneficiarios_RowCommand): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvBeneficiarios_RowCommand): " + ex.Message);
            }
        }

        protected void gvInterfacesCuentas_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            try
            {
                this.CargarInterfazCuentas(0);
                this.validar.RegLog("gvInterfacesCuentas_BeforeColumnSortingGrouping()", "CPP_SP_ConsultarInterfazCuenta", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvInterfacesCuentas_BeforeColumnSortingGrouping()", "N/A", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (gvBeneficiosCapital_Load): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvInterfacesCuentas_BeforeColumnSortingGrouping) :: " + ex.Message);
            }
        }

        /// <summary>
        /// al cargar la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvInterfacesCuentas_Load(object sender, EventArgs e)
        {
            try
            {
                this.CargarInterfazCuentas(0);
                this.validar.RegLog("gvBeneficiarios_Load()", "CPP_SP_ConsultarInterfazCuenta", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvBeneficiarios_Load()", "N/A", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (gvBeneficiosCapital_Load): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvBeneficiarios_Load) :: " + ex.Message);
            }
        }

        /// <summary>
        /// paginacion de la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvInterfacesCuentas_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.CargarInterfazCuentas(0);
                this.validar.RegLog("gvInterfacesCuentas_PageIndexChanged()", "ConsultarBeneficiosCapital", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvInterfacesCuentas_PageIndexChanged()", "N/A", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (gvBeneficiosCapital_Load): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvInterfacesCuentas_PageIndexChanged) :: " + ex.Message);
            }
        }

        /// <summary>
        /// cambio del numero de registros en la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvInterfacesCuentas_PageSizeChanged(object sender, EventArgs e)
        {
            try
            {
                this.CargarInterfazCuentas(0);
                this.validar.RegLog("gvInterfacesCuentas_PageSizeChanged()", "ConsultarBeneficiosCapital", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvInterfacesCuentas_PageSizeChanged()", "N/A", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (gvBeneficiosCapital_Load): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvInterfacesCuentas_PageSizeChanged) :: " + ex.Message);
            }
        }

        #endregion

        #region "metodos privados"

        /// <summary>
        /// carga en variables de sesion la informacion que alimenta a los combos
        /// </summary>
        private void CargarInfoParaCombos()
        {
            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarTipoCesion",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            List<EntitiesTipoCesion> tipoCesion = this.tipoCesionBL.ConsultaTipoCesion(this.accesoDatos);
            Session["TipoCesion"] = tipoCesion.OrderBy(a => a.descripcion).ToList();
            this.cbTipoCesion.DataSource = tipoCesion.OrderBy(a => a.descripcion).ToList();
            this.cbTipoCesion.DataBind();

            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarTipoCuenta",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };
            List<EntitiesTipoCuenta> tipoCuenta = this.tipoCuentaBL.ConsultaTipoCuenta(this.accesoDatos);
            Session["TipoCuenta"] = tipoCuenta.OrderBy(a => a.descripcion).ToList();
            this.cbTipoCuenta.DataSource = tipoCuenta.OrderBy(a => a.descripcion).ToList();
            this.cbTipoCuenta.DataBind();


            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarCalificaciones",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            List<EntitiesCalificacion> calificacion = this.calificacionBL.ConsultaCalificacion(this.accesoDatos);
            Session["Calificacion"] = calificacion.OrderBy(a => a.descripcion).ToList();
            this.cbCalificacion.DataSource = calificacion.OrderBy(a => a.descripcion).ToList();
            this.cbCalificacion.DataBind();

            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarInterfazContable",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            List<EntitiesInterfaz> lsInterfaz = this.interfazBL.ConcultarIterfaz(this.accesoDatos);
            Session["Interfaz"] = lsInterfaz.OrderBy(a => a.cppIn_descripcion).ToList();
            this.cbInterfaz.DataSource = lsInterfaz.OrderBy(a => a.cppIn_descripcion).ToList();
            this.cbInterfaz.DataBind();

            this.accesoDatos = new DLAccesEntities
            {
                procedimiento = "CPP_SP_ConsultarCodigoCuentaContable",
                tipoejecucion = (int)TiposEjecucion.Procedimiento,
                parametros = new List<ParametrizacionSPQUERY>()
            };

            List<EntitiesCodigosCuentaContable> lstCodigoCuentaContable = this.codigoCuentaContableBl.ConsultarCuentasContables(this.accesoDatos);
            Session["CuentasContables"] = lstCodigoCuentaContable.OrderBy(a => a.cuenta).ToList();

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
        /// Metodo encargo de llenar el combo box de municio
        /// </summary>
        /// <param name="countryName"></param>
        private void CuentasCombo(string tipoCesion, string cuenta, string calificacion)
        {
            ASPxCallbackPanel panelContableCombo = this.CbCuetnasContablesP.FindControl("udPCargueCuentas") as ASPxCallbackPanel;
            ASPxListBox cuentaContableCombo = panelContableCombo.FindControl("CbCuetnasContables") as ASPxListBox;

            if (!string.IsNullOrEmpty(tipoCesion) || !string.IsNullOrEmpty(cuenta) || !string.IsNullOrEmpty(calificacion))
            {

                List<EntitiesCodigosCuentaContable> lstCodigoCuentaContable = new List<EntitiesCodigosCuentaContable>();
                lstCodigoCuentaContable = (List<EntitiesCodigosCuentaContable>)Session["CuentasContables"];

                if (!tipoCesion.Equals("null"))
                    lstCodigoCuentaContable = lstCodigoCuentaContable.Where(a => a.idTipoCesion.Equals(Convert.ToInt32(tipoCesion))).ToList();

                if (!cuenta.Equals("null"))
                    lstCodigoCuentaContable = lstCodigoCuentaContable.Where(a => a.idTipoCuenta.Equals(Convert.ToInt32(cuenta))).ToList();

                if (!calificacion.Equals("null"))
                    lstCodigoCuentaContable = lstCodigoCuentaContable.Where(a => a.idCalificacion.Equals(Convert.ToInt32(calificacion))).ToList();

                cuentaContableCombo.DataSource = lstCodigoCuentaContable;
                cuentaContableCombo.DataBind();
            }
            else
            {
                cuentaContableCombo.DataSource = null;
                cuentaContableCombo.DataBind();
            }
        }

        /// <summary>
        /// setea los datos que se van a almacenar en base de datos
        /// </summary>
        private void SetInsertEdit(int idReg)
        {
            string idCuentas = string.Empty;
            this.accesoDatos = new DLAccesEntities();
            this.parametros = new ParametrizacionSPQUERY();

            this.accesoDatos.procedimiento = "CPP_SP_EditarInsertarinterfazCuenta";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

            if (!idReg.Equals(0))
            {
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppCPt_id";
                this.parametros.parametroValor = Convert.ToInt32(this.hdIdInterfazCuenta.Value);
                this.accesoDatos.parametros.Add(this.parametros);
            }

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppIn_id";
            this.parametros.parametroValor = Convert.ToInt32(this.cbInterfaz.SelectedItem.Value.ToString().Trim());
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppTp_id";
            this.parametros.parametroValor = Convert.ToInt32(this.cbTipoCesion.SelectedItem.Value.ToString().Trim());
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppTc_id";
            this.parametros.parametroValor = Convert.ToInt32(this.cbTipoCuenta.SelectedItem.Value.ToString().Trim());
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppCa_id";
            this.parametros.parametroValor = Convert.ToInt32(this.cbCalificacion.SelectedItem.Value.ToString().Trim());
            this.accesoDatos.parametros.Add(this.parametros);

            List<EntitiesCodigosCuentaContable> lstCodigoCuentaContable = new List<EntitiesCodigosCuentaContable>();
            List<EntitiesCodigosCuentaContable> lstCodigoCuentaContable2 = new List<EntitiesCodigosCuentaContable>();
            lstCodigoCuentaContable = (List<EntitiesCodigosCuentaContable>)Session["CuentasContables"];
            string[] valores = this.hdValores.Value.Split(';');
            foreach (var item in valores)
            {
                lstCodigoCuentaContable2 = lstCodigoCuentaContable;
                lstCodigoCuentaContable2 = lstCodigoCuentaContable.Where(a => a.idTipoCesion.Equals(Convert.ToInt32(this.cbTipoCesion.SelectedItem.Value.ToString().Trim()))).ToList();
                lstCodigoCuentaContable2 = lstCodigoCuentaContable.Where(a => a.idTipoCuenta.Equals(Convert.ToInt32(this.cbTipoCuenta.SelectedItem.Value.ToString().Trim()))).ToList();
                lstCodigoCuentaContable2 = lstCodigoCuentaContable.Where(a => a.idCalificacion.Equals(Convert.ToInt32(this.cbCalificacion.SelectedItem.Value.ToString().Trim()))).ToList();
                lstCodigoCuentaContable2 = lstCodigoCuentaContable.Where(a => item.ToString().TrimStart().TrimEnd().Contains(a.codigoCuenta)).ToList();

                if (string.IsNullOrEmpty(idCuentas))
                    idCuentas = lstCodigoCuentaContable2.Select(a => a.id).FirstOrDefault().ToString();
                else
                    idCuentas = idCuentas + "," + lstCodigoCuentaContable2.Select(a => a.id).FirstOrDefault().ToString();
            }

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppCuentas";
            this.parametros.parametroValor = idCuentas;
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = string.IsNullOrEmpty(this.hdIdInterfazCuenta.Value) ? "@cppCPt_UsuarioCrea" : "@cppCPt_UsuarioModifica";
            this.parametros.parametroValor = Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString());
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@accion";
            this.parametros.parametroValor = string.IsNullOrEmpty(this.hdIdInterfazCuenta.Value) ? Convert.ToBoolean("true") : Convert.ToBoolean("false");
            this.accesoDatos.parametros.Add(this.parametros);

            if (this.OperacionInsertUpdate())
            {
                EntitiesAdminBeneficiosCapital dato = new EntitiesAdminBeneficiosCapital();
                this.CargarInterfazCuentas(0);
                this.pcInterfazCuenta.ShowOnPageLoad = false;
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
            this.resultadoOperacion = this.interfazCuentaBL.RegistrarEditarInterfazCuentas(this.accesoDatos);

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
        /// consulta los registros del formulario
        /// </summary>
        private void CargarInterfazCuentas(int id)
        {
            this.accesoDatos = new DLAccesEntities();
            this.parametros = new ParametrizacionSPQUERY();

            this.accesoDatos.procedimiento = "CPP_SP_ConsultarInterfazCuenta";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

            if (id != 0)
            {
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppCPt_id";
                this.parametros.parametroValor = id;
                this.accesoDatos.parametros.Add(this.parametros);
            }

            List<EntitiesInterfazCuenta> lstinterfazContable = this.interfazCuentaBL.ConcultarIterfaz(this.accesoDatos);

            if (id.Equals(0))
            {
                if (lstinterfazContable.Count > 0)
                {
                    this.gvInterfacesCuentas.DataSource = lstinterfazContable;
                    this.gvInterfacesCuentas.DataBind();
                }
            }
            else
                this.CargarObjetos(lstinterfazContable);
        }

        /// <summary>
        /// limpia los objetos del form
        /// </summary>
        private void LimpiarObjetos()
        {
            this.hdValores.Value = string.Empty;
            this.hdIdInterfazCuenta.Value = string.Empty;
            this.cbInterfaz.SelectedIndex = -1;
            this.cbTipoCesion.SelectedIndex = -1;
            this.cbTipoCuenta.SelectedIndex = -1;
            this.cbCalificacion.SelectedIndex = -1;
            ASPxCallbackPanel panelContableCombo = this.CbCuetnasContablesP.FindControl("udPCargueCuentas") as ASPxCallbackPanel;
            ASPxListBox cuentaContableCombo = panelContableCombo.FindControl("CbCuetnasContables") as ASPxListBox;
            cuentaContableCombo.DataSource = null;
            cuentaContableCombo.DataBind();
        }

        /// <summary>
        /// Metodo que se llamada del lado del cliente para realizar la operacion de llenar las cuentas
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void Cuentas_CallbackPriv()
        {
            this.CuentasCombo(this.cbTipoCesion.SelectedItem.Value.ToString().Trim(),
                                 this.cbTipoCuenta.SelectedItem.Value.ToString().Trim(),
                                 this.cbCalificacion.SelectedItem.Value.ToString().Trim());
        }

        /// <summary>
        ///carga los objetos para el detalle y la edicion
        /// </summary>
        /// <param name="data"></param>
        private void CargarObjetos(List<EntitiesInterfazCuenta> data)
        {
            this.hdIdInterfazCuenta.Value = data[0].cppCPt_id.ToString();
            this.cbInterfaz.SelectedIndex = this.cbInterfaz.Items.FindByText(data[0].interfaz).Index;
            this.cbTipoCesion.SelectedIndex = this.cbTipoCesion.Items.FindByText(data[0].cesion).Index;
            this.cbTipoCuenta.SelectedIndex = this.cbTipoCuenta.Items.FindByText(data[0].cuenta).Index;
            this.cbCalificacion.SelectedIndex = this.cbCalificacion.Items.FindByText(data[0].calificacion).Index;
            this.Cuentas_CallbackPriv();
            ASPxCallbackPanel panelContableCombo = this.CbCuetnasContablesP.FindControl("udPCargueCuentas") as ASPxCallbackPanel;
            ASPxListBox cuentaContableCombo = panelContableCombo.FindControl("CbCuetnasContables") as ASPxListBox;
            string[] cuentas = data[0].cppCuentas.Split(',');
            foreach (ListEditItem fila in cuentaContableCombo.Items)
            {
                foreach (string id in cuentas)
                {
                    if (fila.Value.Equals(Convert.ToInt32(id)))
                        fila.Selected = true;
                }
            }
        }

        #endregion
    }
}