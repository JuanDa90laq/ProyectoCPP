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
    using System.Web.UI.WebControls;

    public partial class CPPConceptosAnuales : System.Web.UI.Page
    {
        #region "variables privadas"

        readonly private Validaciones validar = new Validaciones();
        readonly private Validaciones validacionesServicio = new Validaciones();
        readonly private BLTipoCocepto tipoConceptosLB = new BLTipoCocepto();
        readonly private BLConceptoAnual conceptoBL = new BLConceptoAnual();
        private DLAccesEntities accesoDatos = null;
        private SSOSesion usuarioCpp = new SSOSesion();
        private List<EntitiesTipoConcepto> lstTipoConcepto = null;
        private ParametrizacionSPQUERY parametros = null;
        private ResultConsulta resultadoOperacion = null;

        #endregion

        #region "metodos privados"

        /// <summary>
        /// ealiza el cargue inicial
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.usuarioCpp = (SSOSesion)Session["FINAGRO_SSO_SESION"];

                if (!this.IsPostBack)
                {
                    this.Inicializar();
                    this.CargarTipoConcepto();
                    this.validar.RegLog("Page_Load()", "CPP_SP_ConsultarTipoConcepto", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                    this.CargarDTFIBRTotal();
                    this.CargarConceptos();
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
                this.LimpiarCajas();
                this.Inicializar();
                ASPxEdit.ClearEditorsInContainer(this.pcConceptosAnuales, "datos", true);
                this.pcConceptosAnuales.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("BtnNuevo_Click()", "N/A", TipoAccionLog.otro.ToString(), "Error al ejecutar el proceso (BtnNuevo_Click): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (BtnNuevo_Click) :: " + ex.Message);
            }
        }

        /// <summary>
        /// realiza el cambio del tipo de concepto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CbTipoConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ValidarEdicionObjetos(((ASPxComboBox)sender));
        }

        /// <summary>
        /// guarda el nuevo registro o la edicion de un registro seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool insertEditar = true;
                if (!string.IsNullOrEmpty(this.TxtFechaFinal.Text.Trim()))
                {
                    if (this.TxtFechaFinal.Date < this.TxtFechaInicial.Date)
                    {
                        insertEditar = false;
                        this.ShowMensajes(2, "La fecha inicial debe ser menor a la fecha final.");
                    }
                }

                if (insertEditar)
                {
                    this.SetInsertEdit(string.IsNullOrEmpty(this.hdIdConcepto.Value) ? 0 : 1);
                    string accion = string.IsNullOrEmpty(this.hdIdConcepto.Value) ? TipoAccionLog.insercion.ToString() : TipoAccionLog.actualizacion.ToString();
                    this.validar.RegLog("btnGuardar_Click()", "CPP_SP_EditarInsertarConceptoAnual", accion, Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnGuardar_Click()", "N/A", TipoAccionLog.insercion.ToString(), "Error al ejecutar el proceso (btnGuardar_Click): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (btnGuardar_Click) :: " + ex.Message);
            }
        }

        /// <summary>
        /// ordena la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvConceptos_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            try
            {
                //this.CargarGrilla(this.ckConsultarTodos.Checked.Equals(true) ? 1 : 2);
                this.CargarGrilla(2);
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvConceptos_BeforeColumnSortingGrouping()", "N/A", "N/A", "Error al ejecutar el proceso (Ordenamiento) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ordenamiento) :: " + ex.Message);
            }
        }

        /// <summary>
        /// filtro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvConceptos_Load(object sender, EventArgs e)
        {
            gvConceptos.SettingsBehavior.FilterRowMode = GridViewFilterRowMode.Auto;
            try
            {
                //this.CargarGrilla(this.ckConsultarTodos.Checked.Equals(true) ? 1 : 2);
                this.CargarGrilla(2);
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvConceptos_Load()", "N/A", "N/A", "Error al ejecutar el proceso (Ajuste por pagina) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvConceptos_Load) :: " + ex.Message);
            }
        }

        /// <summary>
        /// paginacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvConceptos_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //this.CargarGrilla(this.ckConsultarTodos.Checked.Equals(true) ? 1 : 2);
                this.CargarGrilla(2);
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvConceptos_PageIndexChanged()", "N/A", "N/A", "Error al ejecutar el proceso (Paginación) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvConceptos_PageIndexChanged) :: " + ex.Message);
            }
        }

        /// <summary>
        /// tamaño de la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvConceptos_PageSizeChanged(object sender, EventArgs e)
        {
            try
            {
                ///this.CargarGrilla(this.ckConsultarTodos.Checked.Equals(true) ? 1 : 2);
                this.CargarGrilla(2);
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvConceptos_PageSizeChanged()", "N/A", "N/A", "Error al ejecutar el proceso (Ajuste por pagina) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvConceptos_PageSizeChanged) :: " + ex.Message);
            }
        }

        /// <summary>
        /// eventos del interior de la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvConceptos_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
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

                        ASPxEdit.ClearEditorsInContainer(this.pcConceptosAnuales, "datos", true);
                        this.EdicionReg(Convert.ToInt32(e.KeyValue.ToString().Split('|')[3]));
                        this.pcConceptosAnuales.ShowOnPageLoad = true;

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
        /// Inhabilita el boton para conceptos tipo DTF, IBR o Historicos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvConceptos_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                if (e.RowType.Equals(DevExpress.Web.GridViewRowType.Data))
                {
                    ASPxButton btnEditar = this.gvConceptos.FindRowCellTemplateControl(e.VisibleIndex, null, "btEditar") as ASPxButton;

                    if ((e.KeyValue.ToString().Split('|')[0].Equals("6") || e.KeyValue.ToString().Split('|')[0].Equals("7")) || (e.KeyValue.ToString().Split('|')[2].Equals("H")))
                        btnEditar.Enabled = false;
                    else
                        btnEditar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvConceptos_HtmlRowCreated()", "N/A", "N/A", "Error al ejecutar el proceso (gvConceptos_HtmlRowCreated): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (gvConceptos_HtmlRowCreated): " + ex.Message);
            }
        }

        /// <summary>
        /// true consulta todo - false solo registros actuales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckConsultarTodos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ///this.CargarGrilla(this.ckConsultarTodos.Checked.Equals(true) ? 1 : 2);
                this.CargarGrilla(2);
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("ckConsultarTodos_CheckedChanged()", "N/A", "N/A", "Error al ejecutar el proceso (ckConsultarTodos_CheckedChanged): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (ckConsultarTodos_CheckedChanged): " + ex.Message);
            }
        }

        #endregion

        #region "metodos privados"

        /// <summary>
        /// carga el combo CbTipoConcepto
        /// </summary>
        private void CargarTipoConcepto()
        {
            this.accesoDatos = new DLAccesEntities();
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.procedimiento = "CPP_SP_ConsultarTipoConcepto";
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();
            this.lstTipoConcepto = new List<EntitiesTipoConcepto>();
            this.lstTipoConcepto = this.tipoConceptosLB.ConsultaTipoConcepto(this.accesoDatos).Where(b => b.cppTc_Id != 6 && b.cppTc_Id != 7 && b.cppTc_Id != 10 && b.cppTc_Id != 11 && b.cppTc_Id != 12).OrderBy(a => a.cppTc_Descripcion).ToList();
            Session["TipoConcepto"] = this.lstTipoConcepto;
            this.CbTipoConcepto.DataSource = this.lstTipoConcepto;
            this.CbTipoConcepto.DataBind();
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
        /// inicializa componentes
        /// </summary>
        private void Inicializar()
        {
            LayoutItem layout = ((LayoutItem)formLayout.FindItemOrGroupByName("fechaInicial"));
            layout.Visible = false;
            LayoutItem layoutFinal = ((LayoutItem)formLayout.FindItemOrGroupByName("fechafinal"));
            layoutFinal.Visible = false;
            LayoutItem layoutValor = ((LayoutItem)formLayout.FindItemOrGroupByName("valor"));
            layoutValor.ColumnSpan = 1;
            layoutValor.Visible = false;

            this.TxtValor.MaskSettings.Mask = "";
            this.TxtValor.MaskSettings.IncludeLiterals = MaskIncludeLiteralsMode.None;
            this.TxtValor.ReadOnly = true;

            this.CbTipoConcepto.SelectedIndex = 0;
            this.CbTipoConcepto.Enabled = true;
        }

        /// <summary>
        /// limpia los objetos una vez se realice el cambio de valor en el tipo de concepto
        /// </summary>
        private void LimpiarCajas()
        {
            this.TxtValor.Text = string.Empty;
            this.TxtFechaInicial.Text = string.Empty;
            this.TxtFechaFinal.Text = string.Empty;
            this.TxtValor.ReadOnly = false;
            this.hdIdConcepto.Value = string.Empty;
        }

        /// <summary>
        /// carga la grilla
        /// </summary>
        private void CargarGrilla(int accion)
        {
            if (accion.Equals(2))
            {
                List<EntitiesConceptosAnuales> newList = new List<EntitiesConceptosAnuales>();
                newList = ((List<EntitiesConceptosAnuales>)Session["IBRDTFActual"]).ToList();
                newList = newList.Concat(((List<EntitiesConceptosAnuales>)Session["Conceptos"]).Where(a => a.historico.Equals("A")).ToList()).ToList();
                newList = newList.OrderBy(a => a.historico).ThenBy(a => a.cppCp_Descripcion).ThenBy(a => a.cppCp_FechaVigenciaDesde).ToList();
                this.gvConceptos.DataSource = newList;
            }
            else
            {
                List<EntitiesConceptosAnuales> newList = new List<EntitiesConceptosAnuales>();
                newList = ((List<EntitiesConceptosAnuales>)Session["IBRDTFTotal"]).ToList();
                newList = newList.Concat(((List<EntitiesConceptosAnuales>)Session["Conceptos"])).ToList();
                newList = newList.OrderBy(a => a.historico).ThenBy(a=>a.cppCp_Descripcion).ThenBy(a => a.cppCp_FechaVigenciaDesde).ToList();
                this.gvConceptos.DataSource = newList;
            }

            this.gvConceptos.DataBind();
        }

        /// <summary>
        /// Carga toda la data DTF e IBR
        /// </summary>
        /// <param name="accion">1 todos los registros, 2 unicamente los actuales</param>
        private List<EntitiesDTFIBR> CargarHistoricosDTFIBR(int accion)
        {
            return this.validacionesServicio.CargarDTFIBR(null, null, null, null, accion);
        }

        private List<EntitiesConceptosAnuales> ConvertirIBRDTF_Cenceptos(List<EntitiesDTFIBR> lstDtfIbr)
        {
            List<EntitiesConceptosAnuales> lsdConceptos = new List<EntitiesConceptosAnuales>();
            EntitiesConceptosAnuales concepto = null;

            foreach (EntitiesDTFIBR reg in lstDtfIbr)
            {
                concepto = new EntitiesConceptosAnuales();
                concepto.cppCp_Id = reg.idtasa;
                concepto.cppCp_IdTipoCon = reg.simbolo.Contains("IBR") ? 7 : 6;
                concepto.cppCp_Descripcion = reg.simbolo;
                concepto.cppCp_Valor = reg.valor;
                concepto.cppCp_FechaVigenciaDesde = reg.fechaVigenciaDesde;
                concepto.cppCp_FechaVigenciaHasta = reg.fechaVigenciaHasta;
                concepto.historico = reg.historico;
                lsdConceptos.Add(concepto);
            }

            return lsdConceptos.OrderBy(a => a.cppCp_Descripcion).ToList();
        }

        /// <summary>
        /// carga las dos variables de sesion que contiene los datos de la DTF y el IBR
        /// </summary>
        private void CargarDTFIBRTotal()
        {
            List<EntitiesConceptosAnuales> lsdConcesptos = new List<EntitiesConceptosAnuales>();
            lsdConcesptos = this.ConvertirIBRDTF_Cenceptos(this.CargarHistoricosDTFIBR(1));
            Session["IBRDTFTotal"] = lsdConcesptos.OrderByDescending(b => b.cppCp_Descripcion).ThenBy(a => a.cppCp_FechaVigenciaDesde).ToList();

            lsdConcesptos = new List<EntitiesConceptosAnuales>();
            lsdConcesptos = this.ConvertirIBRDTF_Cenceptos(this.CargarHistoricosDTFIBR(2));
            Session["IBRDTFActual"] = lsdConcesptos.OrderByDescending(b => b.cppCp_Descripcion).ThenBy(a => a.cppCp_FechaVigenciaDesde).ToList();
        }

        /// <summary>
        /// carga las dos variables de sesion que contiene los datos de la DTF y el IBR
        /// </summary>
        private void CargarConceptos()
        {
            this.accesoDatos = new DLAccesEntities();
            this.accesoDatos.procedimiento = "CPP_SP_ConsultarConceptoAnual";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@accion";
            this.parametros.parametroValor = 0;
            this.accesoDatos.parametros.Add(this.parametros);

            List<EntitiesConceptosAnuales> lstConceptos = this.conceptoBL.ConsultaConcepto(this.accesoDatos);
            foreach (EntitiesConceptosAnuales registro in lstConceptos)
            {
                registro.cppCp_Descripcion = ((List<EntitiesTipoConcepto>)Session["TipoConcepto"]).Where(a => a.cppTc_Id.Equals(registro.cppCp_IdTipoCon)).Select(b => b.cppTc_Descripcion).First();
            }

            Session["Conceptos"] = lstConceptos.OrderBy(b => b.historico).ThenBy(a=>a.cppCp_Descripcion).ThenBy(a => a.cppCp_FechaVigenciaDesde).ToList();
        }

        /// <summary>
        /// setea los datos que se van a almacenar en base de datos
        /// </summary>
        private void SetInsertEdit(int idReg)
        {
            int tipo = 0;
            this.accesoDatos = new DLAccesEntities();
            this.parametros = new ParametrizacionSPQUERY();

            this.accesoDatos.procedimiento = "CPP_SP_EditarInsertarConceptoAnual";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

            if (!idReg.Equals(0))
            {
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppCp_Id";
                this.parametros.parametroValor = Convert.ToInt32(this.hdIdConcepto.Value);
                this.accesoDatos.parametros.Add(this.parametros);
            }

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppCp_IdTipoCon";
            this.parametros.parametroValor = Convert.ToInt32(this.CbTipoConcepto.SelectedItem.Value.ToString().Trim());
            this.accesoDatos.parametros.Add(this.parametros);
            tipo = Convert.ToInt32(this.CbTipoConcepto.SelectedItem.Value.ToString().Trim());

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppCp_Valor";
            this.parametros.parametroValor = this.TxtValor.Text.Trim();
            this.accesoDatos.parametros.Add(this.parametros);

            if (!string.IsNullOrEmpty(this.TxtFechaInicial.Text.Trim()))
            {
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppCp_FechaVigenciaDesde";
                this.parametros.parametroValor = Convert.ToDateTime(this.TxtFechaInicial.Text.Trim());
                this.accesoDatos.parametros.Add(this.parametros);
            }

            if (!string.IsNullOrEmpty(this.TxtFechaFinal.Text.Trim()))
            {
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppCp_FechaVigenciaHasta";
                this.parametros.parametroValor = Convert.ToDateTime(this.TxtFechaFinal.Text.Trim());
                this.accesoDatos.parametros.Add(this.parametros);
            }

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = string.IsNullOrEmpty(this.hdIdConcepto.Value) ? "@cppCp_UsuarioCrea" : "@cppCp_UsuarioModifica";
            this.parametros.parametroValor = Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString());
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@accion";
            this.parametros.parametroValor = string.IsNullOrEmpty(this.hdIdConcepto.Value) ? Convert.ToBoolean("true") : Convert.ToBoolean("false");
            this.accesoDatos.parametros.Add(this.parametros);

            if (this.OperacionInsertUpdate())
            {
                this.ConsultaIndividual(true, tipo, string.IsNullOrEmpty(this.hdIdConcepto.Value) ? true : false);
                this.pcConceptosAnuales.ShowOnPageLoad = false;
                this.CargarConceptos();
            }
        }

        /// <summary>
        /// ejecuta un insert o una edicion
        /// </summary>
        /// <returns></returns>
        private bool OperacionInsertUpdate()
        {
            this.resultadoOperacion = this.conceptoBL.RegistrarConceptos(this.accesoDatos);

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
        /// consulta un unico registro se invoca despues de registrar o editar
        /// </summary>
        /// <param name="accion">true solo consulta un registro, false todo</param>
        /// <param name="tipo">parametro a filtrar</param>
        /// <param name="operacion">true inserta, false edita</param>
        private void ConsultaIndividual(bool accion, int tipo, bool operacion)
        {
            this.accesoDatos = new DLAccesEntities();
            this.accesoDatos.procedimiento = "CPP_SP_ConsultarConceptoAnual";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@accion";
            this.parametros.parametroValor = accion;
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppCp_IdTipoCon";
            this.parametros.parametroValor = tipo;
            this.accesoDatos.parametros.Add(this.parametros);

            List<EntitiesConceptosAnuales> lstConceptos = this.conceptoBL.ConsultaConcepto(this.accesoDatos);
            foreach (EntitiesConceptosAnuales registro in lstConceptos)
            {
                registro.cppCp_Descripcion = ((List<EntitiesTipoConcepto>)Session["TipoConcepto"]).Where(a => a.cppTc_Id.Equals(registro.cppCp_IdTipoCon)).Select(b => b.cppTc_Descripcion).First();
            }

            if (operacion)
            {
                List<EntitiesConceptosAnuales> lstConceptosTem = ((List<EntitiesConceptosAnuales>)Session["Conceptos"]).Concat(lstConceptos).ToList();
                Session["Conceptos"] = lstConceptosTem;
            }
            else
            {
                ((List<EntitiesConceptosAnuales>)Session["Conceptos"]).Where(a => a.cppCp_Id.Equals(lstConceptos[0].cppCp_Id)).First().cppCp_IdTipoCon = lstConceptos[0].cppCp_IdTipoCon;
                ((List<EntitiesConceptosAnuales>)Session["Conceptos"]).Where(a => a.cppCp_Id.Equals(lstConceptos[0].cppCp_Id)).First().cppCp_Valor = lstConceptos[0].cppCp_Valor;
                ((List<EntitiesConceptosAnuales>)Session["Conceptos"]).Where(a => a.cppCp_Id.Equals(lstConceptos[0].cppCp_Id)).First().cppCp_FechaVigenciaDesde = lstConceptos[0].cppCp_FechaVigenciaDesde;
                ((List<EntitiesConceptosAnuales>)Session["Conceptos"]).Where(a => a.cppCp_Id.Equals(lstConceptos[0].cppCp_Id)).First().cppCp_FechaVigenciaHasta = lstConceptos[0].cppCp_FechaVigenciaHasta;
            }

            this.gvConceptos.DataSource = lstConceptos;
            this.gvConceptos.DataBind();
        }

        /// <summary>
        /// se invoca cuando se va a modificar un registro
        /// </summary>
        private void EdicionReg(int idReg)
        {
            this.accesoDatos = new DLAccesEntities();
            this.accesoDatos.procedimiento = "CPP_SP_ConsultarConceptoAnual";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@accion";
            this.parametros.parametroValor = 1;
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppCp_Id";
            this.parametros.parametroValor = idReg;
            this.accesoDatos.parametros.Add(this.parametros);

            List<EntitiesConceptosAnuales> lstConceptos = this.conceptoBL.ConsultaConcepto(this.accesoDatos);
            foreach (EntitiesConceptosAnuales registro in lstConceptos)
            {
                registro.cppCp_Descripcion = ((List<EntitiesTipoConcepto>)Session["TipoConcepto"]).Where(a => a.cppTc_Id.Equals(registro.cppCp_IdTipoCon)).Select(b => b.cppTc_Descripcion).First();
            }

            this.CbTipoConcepto.SelectedIndex = this.CbTipoConcepto.Items.FindByText(lstConceptos[0].cppCp_Descripcion).Index;
            this.CbTipoConcepto.Enabled = false;
            this.ValidarEdicionObjetos(this.CbTipoConcepto);
            this.TxtValor.Text = lstConceptos[0].cppCp_Valor;
            this.hdIdConcepto.Value = lstConceptos[0].cppCp_Id.ToString();

            if (lstConceptos[0].cppCp_FechaVigenciaDesde != null)
            {
                this.TxtFechaInicial.Date = Convert.ToDateTime(lstConceptos[0].cppCp_FechaVigenciaDesde.ToString());
                this.TxtFechaFinal.Date = Convert.ToDateTime(lstConceptos[0].cppCp_FechaVigenciaHasta.ToString());
            }

        }

        /// <summary>
        /// configura la visualizacion de la pantalla de acuerdo al tipo de concepto seleccionado
        /// </summary>
        /// <param name="objeto"></param>
        private void ValidarEdicionObjetos(object objeto)
        {
            if (!Convert.ToInt32(((ASPxComboBox)objeto).SelectedIndex).Equals(-1))
            {
                int valor = Convert.ToInt32(((ASPxComboBox)objeto).SelectedItem.Value);
                this.LimpiarCajas();

                if (valor.Equals(1) || valor.Equals(2) || valor.Equals(3) ||
                    valor.Equals(4) || valor.Equals(5) || valor.Equals(8) ||
                    valor.Equals(9) || valor.Equals(13))
                {
                    LayoutItem layout = ((LayoutItem)formLayout.FindItemOrGroupByName("fechaInicial"));
                    layout.Visible = true;
                    layout = ((LayoutItem)formLayout.FindItemOrGroupByName("fechafinal"));
                    layout.Visible = true;
                    layout = ((LayoutItem)formLayout.FindItemOrGroupByName("concepto"));
                    layout.ColumnSpan = 2;
                    layout = ((LayoutItem)formLayout.FindItemOrGroupByName("valor"));
                    layout.ColumnSpan = 2;
                    layout.Visible = true;
                }
                else
                {
                    LayoutItem layout = ((LayoutItem)formLayout.FindItemOrGroupByName("fechaInicial"));
                    layout.Visible = false;
                    layout = ((LayoutItem)formLayout.FindItemOrGroupByName("fechafinal"));
                    layout.Visible = false;
                    layout = ((LayoutItem)formLayout.FindItemOrGroupByName("concepto"));
                    layout.ColumnSpan = 1;
                    layout = ((LayoutItem)formLayout.FindItemOrGroupByName("valor"));
                    layout.ColumnSpan = 1;
                    layout.Visible = true;
                }

                if (valor.Equals(14))
                {
                    this.TxtValor.MaskSettings.Mask = "<9999>";
                    this.TxtValor.MaskSettings.IncludeLiterals = MaskIncludeLiteralsMode.None;
                    this.TxtValor.MaxLength = 4;
                    this.TxtValor.Enabled = true;
                    this.btnGuardar.Visible = true;
                }

                if (valor.Equals(1) || valor.Equals(2) || valor.Equals(3) || valor.Equals(4) || valor.Equals(5) || valor.Equals(6) ||
                    valor.Equals(7) || valor.Equals(8) || valor.Equals(9) || valor.Equals(13))
                {
                    this.TxtValor.MaskSettings.Mask = "<0..9999999999999999g>.<00..99>";
                    this.TxtValor.MaskSettings.IncludeLiterals = MaskIncludeLiteralsMode.DecimalSymbol;
                    this.TxtValor.MaxLength = 15;
                    this.TxtValor.Enabled = true;
                    this.btnGuardar.Visible = true;
                }

                if (valor.Equals(10) || valor.Equals(11) || valor.Equals(12))
                {
                    this.TxtValor.MaskSettings.Mask = "";
                    this.TxtValor.MaskSettings.IncludeLiterals = MaskIncludeLiteralsMode.None;
                    this.TxtValor.Enabled = false;
                    this.btnGuardar.Visible = false;
                }
            }
            else
                this.LimpiarCajas();
        }

        #endregion

    }
}