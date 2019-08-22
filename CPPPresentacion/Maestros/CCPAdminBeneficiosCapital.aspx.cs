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
    using System.Data;
    using System.Linq;

    public partial class CCPAdminBeneficiosCapital : System.Web.UI.Page
    {
        readonly private Validaciones validar = new Validaciones();
        readonly private Validaciones validacionesServicio = new Validaciones();
        readonly private BLProgama programaBL = new BLProgama();
        readonly private BLCondiciones condicionesBL = new BLCondiciones();
        private BLBeneficioCapital beneficioCapitalBl = null;
        private SSOSesion usuarioCpp = new SSOSesion();        
        private ParametrizacionSPQUERY parametros = null;
        private DLAccesEntities accesoDatos = null;
        private ResultConsulta resultadoOperacion = null;

        #region "metodos protegidos"

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.usuarioCpp = (SSOSesion)Session["FINAGRO_SSO_SESION"];

                if (!this.IsPostBack)
                {
                    this.CargarProgramas();
                    this.validar.RegLog("Page_Load()", "CPP_SP_ConsultarProgramas", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                    this.CargarCondiciones();
                    this.validar.RegLog("Page_Load()", "CPP_SP_ConsultarCondiciones", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                    this.Cargaractividades();
                    this.CargarDepartamentos();
                    this.CargarMunicipios();
                    EntitiesAdminBeneficiosCapital dato = new EntitiesAdminBeneficiosCapital();
                    this.ConsultarBeneficiosCapital(dato, false);
                    this.validar.RegLog("Page_Load()", "ConsultarBeneficiosCapital", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());

                }
                else
                {
                    if (CallbackPanel.IsCallback)
                        this.ValidarValores();
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("Page_Load()", "N/A", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (Page_Load): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Page_Load) :: " + ex.Message);
            }
        }

        /// <summary>
        /// habilita el form para un nuevo registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Limpiar();
                ASPxEdit.ClearEditorsInContainer(this.pcBeneficiosCapital, "datos", true);
                this.pcBeneficiosCapital.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("BtnNuevo_Click()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (BtnNuevo_Click): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (BtnNuevo_Click) :: " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo que se llamada del lado del cliente para realizar la operacion de llenar el combo de municipio
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void cbMunicipio_Callback(object source, CallbackEventArgsBase e)
        {
            try
            {
                CargarCbMunicipio(e.Parameter);
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("cbMunicipio_Callback()", "N/A", "N/A", "Error al ejecutar el proceso (cbMunicipio_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (cbMunicipio_Callback): " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo encargo de llenar el combo box de municio
        /// </summary>
        /// <param name="countryName"></param>
        protected void CargarCbMunicipio(string idDepto)
        {
            try
            {
                if (string.IsNullOrEmpty(idDepto)) return;

                this.cbMunicipio.Items.Clear();
                List<Municipios> lstMunicipios = new List<Municipios>();
                lstMunicipios = (List<Municipios>)Session["municipios"];
                this.cbMunicipio.DataSource = lstMunicipios.Where(a => a.idDepto.Equals(Convert.ToInt32(idDepto)));
                this.cbMunicipio.DataBind();
                this.cbMunicipio.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("FillCityCombo()", "N/A", "N/A", "Error al ejecutar el proceso (Llenar Municipios): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Llenar Municipios): " + ex.Message);
            }
        }

        /// <summary>
        /// guarda el nuevo registro o la edicion de un registro seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool validarActiviades = true;
            bool validarFecha = true;
            bool validarValor = true;
            bool validarValorNuevo = true;
            string msjError = string.Empty;
            try
            {
                    if (!this.ValidarSeleccionActividades())
                        validarActiviades = false;

                    if (this.TxtFechaInicial.Date > this.TxtFechaFinal.Date)
                        validarFecha = false;

                    if (this.TxtValor.Text.Equals("0,00"))
                        validarValor = false;

                    if (!Convert.ToInt32(this.CbCondicion.SelectedItem.Value).Equals(6) && this.txtNuevoValor.Text.Equals("0,00"))
                        validarValorNuevo = false;

                    if (validarActiviades && validarFecha && validarValor && validarValorNuevo)
                    {
                        this.SetInsertEdit(string.IsNullOrEmpty(this.hdIdBeneficio.Value) ? 0 : 1);
                        string accion = string.IsNullOrEmpty(this.hdIdBeneficio.Value) ? TipoAccionLog.insercion.ToString() : TipoAccionLog.actualizacion.ToString();
                        this.validar.RegLog("btnGuardar_Click()", "CPP_SP_EditarInsertarConceptoAnual", accion, Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                    }
                    else
                    {

                        if (!validarActiviades)
                            msjError = "+Debe seleccionar al menos una actividad economica.";

                        if (!validarFecha)
                            msjError = string.IsNullOrEmpty(msjError) ? "+La fecha inicial debe ser menor a la fecha final." : msjError + "*+La fecha inicial debe ser menor a la fecha final.";

                        if (!validarValor)
                            msjError = string.IsNullOrEmpty(msjError) ? "+Digite un valor" : msjError + "*+Digite un valor";

                        if (!validarValorNuevo)
                            msjError = string.IsNullOrEmpty(msjError) ? "+Digite un nuevo porcentaje" : msjError + "*+Digite un nuevo porcentaje";

                        this.ShowMensajes(2, msjError);
                        this.ValidarValores();

                    }                    
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnGuardar_Click()", "N/A", TipoAccionLog.insercion.ToString(), "Error al ejecutar el proceso (btnGuardar_Click): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (btnGuardar_Click) :: " + ex.Message);
            }
        }

        protected void gvBeneficiosCapital_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                switch (e.CommandArgs.CommandName)
                {
                    case "Editar":


                        this.Limpiar();
                        ASPxEdit.ClearEditorsInContainer(this.pcBeneficiosCapital, "datos", true);
                        EntitiesAdminBeneficiosCapital dato = new EntitiesAdminBeneficiosCapital() { cppAb_Id = Convert.ToInt32(e.KeyValue.ToString().Split('|')[0]) };
                        this.ConsultarBeneficiosCapital(dato, true);
                        this.pcBeneficiosCapital.ShowOnPageLoad = true;                        

                        break;

                    case "MostrarActividades":

                        List<EntitiesActividadesAgropecuarias> lstActivi = new List<EntitiesActividadesAgropecuarias>();
                        string[] idActividades = e.KeyValue.ToString().Split('|')[1].Split(',');
                        foreach (string id in idActividades)
                        {
                            lstActivi.Add(((List<EntitiesActividadesAgropecuarias>)Session["Actividades"]).Where(a => a.id.Equals(Convert.ToInt32(id))).First());
                        }

                        this.ckConsulta.DataSource = lstActivi;
                        this.ckConsulta.DataBind();
                        this.ppActividadesAsociadas.ShowOnPageLoad = true;

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

        /// <summary>
        /// selecciona todos los registros referentes a las actividades economicas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckTodos_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxListBox cbActividades = CbActividadP.FindControl("CbActividad") as ASPxListBox;
                foreach (ListEditItem registro in cbActividades.Items)
                {
                    registro.Selected = true;
                }

                this.ValidarValores();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("ckConsultarTodos_CheckedChanged()", "N/A", "N/A", "Error al ejecutar el proceso (ckConsultarTodos_CheckedChanged): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (ckConsultarTodos_CheckedChanged): " + ex.Message);
            }
        }

        /// <summary>
        /// se ejecuta cuando se ordena
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBeneficiosCapital_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            try
            { 
                EntitiesAdminBeneficiosCapital dato = new EntitiesAdminBeneficiosCapital();
                this.ConsultarBeneficiosCapital(dato, false);
                this.validar.RegLog("gvBeneficiosCapital_BeforeColumnSortingGrouping()", "ConsultarBeneficiosCapital", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvBeneficiosCapital_BeforeColumnSortingGrouping()", "N/A", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (gvBeneficiosCapital_Load): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvBeneficiosCapital_BeforeColumnSortingGrouping) :: " + ex.Message);
            }
        }

        /// <summary>
        /// al cargar la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBeneficiosCapital_Load(object sender, EventArgs e)
        {
            try
            {
                EntitiesAdminBeneficiosCapital dato = new EntitiesAdminBeneficiosCapital();
                this.ConsultarBeneficiosCapital(dato, false);
                this.validar.RegLog("gvBeneficiosCapital_Load()", "ConsultarBeneficiosCapital", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvBeneficiosCapital_Load()", "N/A", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (gvBeneficiosCapital_Load): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvBeneficiosCapital_Load) :: " + ex.Message);
            }
        }

        /// <summary>
        /// paginacion de la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBeneficiosCapital_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                EntitiesAdminBeneficiosCapital dato = new EntitiesAdminBeneficiosCapital();
                this.ConsultarBeneficiosCapital(dato, false);
                this.validar.RegLog("gvBeneficiosCapital_PageIndexChanged()", "ConsultarBeneficiosCapital", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvBeneficiosCapital_PageIndexChanged()", "N/A", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (gvBeneficiosCapital_Load): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvBeneficiosCapital_PageIndexChanged) :: " + ex.Message);
            }
        }

        /// <summary>
        /// cambio del numero de registros en la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBeneficiosCapital_PageSizeChanged(object sender, EventArgs e)
        {
            try
            {
                EntitiesAdminBeneficiosCapital dato = new EntitiesAdminBeneficiosCapital();
                this.ConsultarBeneficiosCapital(dato, false);
                this.validar.RegLog("gvBeneficiosCapital_PageSizeChanged()", "ConsultarBeneficiosCapital", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvBeneficiosCapital_PageSizeChanged()", "N/A", TipoAccionLog.consulta.ToString(), "Error al ejecutar el proceso (gvBeneficiosCapital_Load): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvBeneficiosCapital_PageSizeChanged) :: " + ex.Message);
            }
        }

        #endregion

        #region "metodos privados"

        /// <summary>
        /// carga los departamentos
        /// </summary>
        private void CargarDepartamentos()
        {
            List<Depatamentos> lstDeptos = new List<Depatamentos>();
            lstDeptos = this.validar.CargarDepartamentos();

            Session["Depto"] = lstDeptos;
            this.cbDepartamento.DataSource = lstDeptos;
            this.cbDepartamento.DataBind();
        }

        /// <summary>
        /// consulta los departamentos y carga el combo deptos
        /// </summary>
        private void CargarMunicipios()
        {
            Session["municipios"] = this.validar.CargarMunicipios(0);
        }

        /// <summary>
        /// carga el combo de los programas
        /// </summary>
        private void CargarProgramas()
        {
            this.accesoDatos = new DLAccesEntities();
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();
            this.accesoDatos.procedimiento = "CPP_SP_ConsultarProgramas";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            List<EntitiesPrograma>  programas = this.programaBL.ConsultarProgramas(this.accesoDatos);
            Session["Programas"] = programas;
            this.CbPrograma.DataSource = programas;
            this.CbPrograma.DataBind();
        }

        /// <summary>
        /// carga el combo de las condiciones
        /// </summary>
        private void CargarCondiciones()
        {
            this.accesoDatos = new DLAccesEntities();
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();
            this.accesoDatos.procedimiento = "CPP_SP_ConsultarCondiciones";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;            
            List<EntitiesCondiciones> condiciones = this.condicionesBL.ConsultarCondiciones(this.accesoDatos);
            Session["Condiciones"] = condiciones;
            this.CbCondicion.DataSource = condiciones;
            this.CbCondicion.DataBind();
        }

        /// <summary>
        /// realiza el carge general de las actividades agropecuarias
        /// </summary>
        private void Cargaractividades()
        {
            List<EntitiesActividadesAgropecuarias> actividades = this.validar.CargarActividades();
            Session["Actividades"] = actividades;
            ASPxListBox cbActividades = CbActividadP.FindControl("CbActividad") as ASPxListBox;
            cbActividades.DataSource = actividades;
            cbActividades.DataBind();
        }

        /// <summary>
        /// setea los datos que se van a almacenar en base de datos
        /// </summary>
        private void SetInsertEdit(int idReg)
        {
            string idCuentas = string.Empty;
            int tipo = 0;
            this.accesoDatos = new DLAccesEntities();
            this.parametros = new ParametrizacionSPQUERY();

            this.accesoDatos.procedimiento = "CPP_SP_EditarInsertarBeneficioCapital";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

            if (!idReg.Equals(0))
            {
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppAb_Id";
                this.parametros.parametroValor = Convert.ToInt32(this.hdIdBeneficio.Value);
                this.accesoDatos.parametros.Add(this.parametros);
            }

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppAb_IdPr";
            this.parametros.parametroValor = Convert.ToInt32(this.CbPrograma.SelectedItem.Value.ToString().Trim());
            this.accesoDatos.parametros.Add(this.parametros);
            tipo = Convert.ToInt32(this.CbPrograma.SelectedItem.Value.ToString().Trim());

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppAb_IdCd";
            this.parametros.parametroValor = Convert.ToInt32(this.CbCondicion.SelectedItem.Value.ToString().Trim());
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppAb_IdDepto";
            this.parametros.parametroValor = Convert.ToInt32(this.cbDepartamento.SelectedItem.Value.ToString().Trim());
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppAb_IdMun";
            this.parametros.parametroValor = Convert.ToInt32(this.cbMunicipio.SelectedItem.Value.ToString().Trim());
            this.accesoDatos.parametros.Add(this.parametros);

            ASPxListBox cbActividades = CbActividadP.FindControl("CbActividad") as ASPxListBox;
            foreach (var item in cbActividades.SelectedValues)
                idCuentas = string.IsNullOrEmpty(idCuentas) ? item.ToString() : idCuentas + "," + item.ToString();

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppAb_IdActividad";
            this.parametros.parametroValor = idCuentas;
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppAb_Valor";
            this.parametros.parametroValor = Convert.ToDouble(this.TxtValor.Text.Trim());
            this.accesoDatos.parametros.Add(this.parametros);

            if (!Convert.ToInt32(this.CbCondicion.SelectedItem.Value).Equals(6))
            {
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppAb_SegundoValor";
                this.parametros.parametroValor = Convert.ToDouble(this.txtNuevoValor.Text);
                this.accesoDatos.parametros.Add(this.parametros);
            }

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppAb_FechaInicio";
            this.parametros.parametroValor = Convert.ToDateTime(this.TxtFechaInicial.Text.Trim());
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppAb_FechaFinal";
            this.parametros.parametroValor = Convert.ToDateTime(this.TxtFechaFinal.Text.Trim());
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppAb_Descripcion";
            this.parametros.parametroValor = this.TxtDescripcion.Text.Trim();
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = string.IsNullOrEmpty(this.hdIdBeneficio.Value) ? "@cppAb_UsuarioCrea" : "@cppAb_UsuarioMod";
            this.parametros.parametroValor = Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString());
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@accion";
            this.parametros.parametroValor = string.IsNullOrEmpty(this.hdIdBeneficio.Value) ? Convert.ToBoolean("true") : Convert.ToBoolean("false");
            this.accesoDatos.parametros.Add(this.parametros);

            if (this.OperacionInsertUpdate())
            {
                /*EntitiesAdminBeneficiosCapital dato = new EntitiesAdminBeneficiosCapital() {
                    cppAb_IdPr = Convert.ToInt32(this.CbPrograma.SelectedItem.Value.ToString().Trim()),
                    cppAb_IdActividadTotal = idCuentas,
                    cppAb_FechaInicio = Convert.ToDateTime(this.TxtFechaInicial.Text.Trim()),
                    cppAb_FechaFinal = Convert.ToDateTime(this.TxtFechaFinal.Text.Trim())
                 };*/

                EntitiesAdminBeneficiosCapital dato = new EntitiesAdminBeneficiosCapital();
                this.ConsultarBeneficiosCapital(dato,false);                
                this.pcBeneficiosCapital.ShowOnPageLoad = false;                
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
        /// realiza la acciion sobre la base de datos
        /// </summary>
        /// <param name="parametros">objeto con la informacion a ejecutar</param>
        /// <param name="accion">true -> insercion o actualizacion, false -> consulta</param>
        /// <returns></returns>
        private bool OperacionInsertUpdate()
        {
            this.beneficioCapitalBl = new BLBeneficioCapital();
            this.resultadoOperacion = this.beneficioCapitalBl.RegistrarEditarBeneficioCapital(this.accesoDatos);

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
        /// consulta el regsitro a buscar
        /// </summary>
        /// <param name="dato">objeto con los parametros de busqueda</param>        
        private void ConsultarBeneficiosCapital(EntitiesAdminBeneficiosCapital dato, bool actualizar)
        {
            this.beneficioCapitalBl = new BLBeneficioCapital();

            this.accesoDatos = new DLAccesEntities();
            this.accesoDatos.procedimiento = "CPP_SP_ConsultarBeneficioCapital";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

            if (!dato.cppAb_Id.Equals(0))
            {
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppAb_Id";
                this.parametros.parametroValor = dato.cppAb_Id;
                this.accesoDatos.parametros.Add(this.parametros);
            }


            if (!dato.cppAb_IdActividad.Equals(0))
            {
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppAb_IdActividadTotal";
                this.parametros.parametroValor = dato.cppAb_IdActividad;
                this.accesoDatos.parametros.Add(this.parametros);
            }

            if (!dato.cppAb_IdPr.Equals(0))
            {
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppAb_IdPr";
                this.parametros.parametroValor = dato.cppAb_IdPr;
                this.accesoDatos.parametros.Add(this.parametros);
            }

            if (!dato.cppAb_FechaInicio.Equals(null))
            {
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppAb_FechaInicio";
                this.parametros.parametroValor = Convert.ToDateTime(dato.cppAb_FechaInicio);
                this.accesoDatos.parametros.Add(this.parametros);
            }

            if (!dato.cppAb_FechaFinal.Equals(null))
            {
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppAb_FechaFinal";
                this.parametros.parametroValor = Convert.ToDateTime(dato.cppAb_FechaFinal);
                this.accesoDatos.parametros.Add(this.parametros);
            }

            List<EntitiesAdminBeneficiosCapital> lstConceptos = this.beneficioCapitalBl.ConcultarbeneficiosCapital(this.accesoDatos);
            foreach (EntitiesAdminBeneficiosCapital registro in lstConceptos)
            {
                registro.depto = ((List<Depatamentos>)Session["Depto"]).Where(a => a.id.Equals(registro.cppAb_IdDepto)).Select(b => b.depratamento).First();
                registro.programa =((List<EntitiesPrograma>)Session["Programas"]).Where(a => a.id.Equals(registro.cppAb_IdPr)).Select(b => b.nombre).First();
                ///registro.Actividad = ((List<EntitiesActividadesAgropecuarias>)Session["Actividades"]).Where(a => a.id.Equals(registro.cppAb_IdActividad)).Select(b => b.actividad).First();
                registro.condicion = ((List<EntitiesCondiciones>)Session["Condiciones"]).Where(a => a.cppCd_Id.Equals(registro.cppAb_IdCd)).Select(b => b.cppCd_descripcion).First();
                registro.municipio = ((List<Municipios>)Session["municipios"]).Where(a => a.id.Equals(registro.cppAb_IdMun)).Select(b => b.municipio).First();                
            }

            if (!actualizar)
            {
                this.gvBeneficiosCapital.DataSource = lstConceptos;
                this.gvBeneficiosCapital.DataBind();
            }
            else
                this.CargarForm(lstConceptos);
        }

        /// <summary>
        /// valida que existan actividades seleccionadas
        /// </summary>
        /// <returns></returns>
        private bool ValidarSeleccionActividades()
        {
            ASPxListBox cbActividades = CbActividadP.FindControl("CbActividad") as ASPxListBox;
            if (cbActividades.SelectedValues.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Carga la data en la interfaz para la edicion
        /// </summary>
        /// <param name="datos"></param>
        private void CargarForm(List<EntitiesAdminBeneficiosCapital> datos)
        {
            this.hdIdBeneficio.Value = datos[0].cppAb_Id.ToString();
            this.CbPrograma.SelectedIndex = this.CbPrograma.Items.FindByText(datos[0].programa).Index;
            this.CbCondicion.SelectedIndex = this.CbCondicion.Items.FindByText(datos[0].condicion).Index;
            this.cbDepartamento.SelectedIndex = this.cbDepartamento.Items.FindByValue(datos[0].cppAb_IdDepto.ToString()).Index;
            this.CargarCbMunicipio(datos[0].cppAb_IdDepto.ToString());
            this.cbMunicipio.SelectedIndex = this.cbMunicipio.Items.FindByValue(datos[0].cppAb_IdMun.ToString()).Index;
            this.TxtFechaInicial.Date = Convert.ToDateTime(datos[0].cppAb_FechaInicio);
            this.TxtFechaFinal.Date = Convert.ToDateTime(datos[0].cppAb_FechaFinal);
            this.TxtValor.Text = datos[0].valor.ToString();
            this.TxtDescripcion.Text = datos[0].cppAb_Descripcion;
            this.SeleccionarActividades(datos[0].cppAb_IdActividadTotal);
            this.txtNuevoValor.Visible = !datos[0].cppAb_IdCd.Equals(6) ? true : false;
            this.txtNuevoValor.Text = datos[0].segundoValor.ToString();
        }

        /// <summary>
        /// limpia los objetos de la interfaz
        /// </summary>
        private void Limpiar()
        {
            this.hdIdBeneficio.Value = string.Empty;
            this.CbPrograma.SelectedIndex = -1;
            this.CbCondicion.SelectedIndex = -1;            
            this.cbDepartamento.SelectedIndex = -1;
            this.cbMunicipio.SelectedIndex = -1;
            this.cbMunicipio.Items.Clear();
            this.TxtFechaInicial.Text = string.Empty;
            this.TxtFechaFinal.Text = string.Empty;
            this.TxtValor.Text = string.Empty;
            this.txtNuevoValor.Text = string.Empty;
            this.txtNuevoValor.Visible = false;

            ASPxListBox cbActividades = CbActividadP.FindControl("CbActividad") as ASPxListBox;

            foreach(ListEditItem registro in cbActividades.Items)
            {
                registro.Selected = false;
            }
        }

        /// <summary>
        /// carga actividades economicas seleccioandas
        /// </summary>
        private void SeleccionarActividades(string actividades)
        {
            ASPxListBox cbActividades = CbActividadP.FindControl("CbActividad") as ASPxListBox;

            foreach (string dato in actividades.Split(','))
            {
                cbActividades.Items.FindByValue(Convert.ToInt32(dato)).Selected = true;                
            }
        }

        /// <summary>
        /// segun la condicion seleccionada muestra el segundo valor
        /// </summary>
        private void ValidarValores()
        {
            if (!Convert.ToInt32(this.CbCondicion.SelectedItem.Value).Equals(6))
                this.txtNuevoValor.Visible = true;
            else
                this.txtNuevoValor.Visible = false;
        }

        #endregion

        
    }
}