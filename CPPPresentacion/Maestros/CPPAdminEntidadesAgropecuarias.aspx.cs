namespace CPPPresentacion.Maestros
{
    using CPPPresentacion.Generica;
    using CPPBL.Maestros;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using DevExpress.Web;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;
    using System.Linq;
    using SSO.Finagro;

    public partial class CPPAdminEntidadesAgropecuarias : System.Web.UI.Page
    {
        #region "Definicion de variables privadas"

        private BLActividadAgropecuaria actividadesAgroBL = null;
        private DLAccesEntities accesoDatos = null;
        private ParametrizacionSPQUERY parametros = null;
        private List<EntitiesActividadesAgropecuarias> lstActividadesAgro = null;
        private ResultConsulta resultadoOperacion = null;
        private Validaciones validar = new Validaciones();
        private SSOSesion usuarioCpp = new SSOSesion();

        #endregion

        #region "Metodos Protegidos"

        /// <summary>
        /// metodo principal de la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.usuarioCpp = (SSOSesion)Session["FINAGRO_SSO_SESION"];

                if (!this.IsPostBack)
                    this.CargaCombo();                

            }
            catch (Exception ex)
            {
                this.ShowMensajes(0, "Error al ejecutar el proceso :: " + ex.Message);
            }
        }

        /// <summary>
        /// cuando se da clic en el boton consultar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                this.ConsultarActividad();
            }
            catch (Exception ex)
            {
                this.ShowMensajes(0, "Error al ejecutar el proceso :: " + ex.Message);
            }
        }   

        /// <summary>
        /// se ejecuta cuando un eveto en la grilla se dsipara
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvActividadesAgropeacuarias_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
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

                        this.gvActividadesAgropeacuarias.Columns[0].Width = 230;
                        btnEditar = this.gvActividadesAgropeacuarias.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)this.gvActividadesAgropeacuarias.Columns[0], "btEditar") as ASPxButton;
                        btnEliminar = this.gvActividadesAgropeacuarias.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)this.gvActividadesAgropeacuarias.Columns[0], "btElimnar") as ASPxButton;
                        lbId = this.gvActividadesAgropeacuarias.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)this.gvActividadesAgropeacuarias.Columns[1], "lbIdentificadorAgro") as ASPxLabel;
                        lbActividad = this.gvActividadesAgropeacuarias.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)this.gvActividadesAgropeacuarias.Columns[2], "lbActividadAgro") as ASPxLabel;
                        txtActividad = this.gvActividadesAgropeacuarias.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)this.gvActividadesAgropeacuarias.Columns[2], "txtActividad") as ASPxTextBox;
                        validador = this.gvActividadesAgropeacuarias.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)this.gvActividadesAgropeacuarias.Columns[2], "vldActividad_" + lbId.Text) as RequiredFieldValidator;
                        ckEstadoIni = this.gvActividadesAgropeacuarias.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)this.gvActividadesAgropeacuarias.Columns[3], "ckEstado") as ASPxCheckBox;
                        btnEditar.CommandName = "Aceptar";
                        btnEditar.Text = "Aceptar";
                        btnEliminar.CommandName = "Cancelar";
                        btnEliminar.Text = "Cancelar";
                        btnEditar.ValidationGroup = "ActividadEdicion" + lbId.Text;
                        validador.ValidationGroup = "ActividadEdicion" + lbId.Text;                        
                        btnEditar.ClientSideEvents.Click = "function(s, e) { activarValidador('" + validador.ClientID  + "', true); }";
                        btnEliminar.ClientSideEvents.Click = "function(s, e) { activarValidador('" + validador.ClientID + "', false); }";
                        btnEliminar.Visible = true;
                        lbActividad.Visible = false;
                        txtActividad.Text = lbActividad.Text;
                        txtActividad.Text = txtActividad.Text.TrimEnd().TrimStart();
                        txtActividad.Visible = true;
                        ckEstadoIni.Enabled = true;
                        this.hdControlEditar.Value = (Convert.ToInt32(this.hdControlEditar.Value) + 1).ToString();

                        break;

                    case "Cancelar":

                        btnEditar = this.gvActividadesAgropeacuarias.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)this.gvActividadesAgropeacuarias.Columns[0], "btEditar") as ASPxButton;
                        btnEliminar = this.gvActividadesAgropeacuarias.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)this.gvActividadesAgropeacuarias.Columns[0], "btElimnar") as ASPxButton;
                        lbActividad = this.gvActividadesAgropeacuarias.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)this.gvActividadesAgropeacuarias.Columns[2], "lbActividadAgro") as ASPxLabel;
                        txtActividad = this.gvActividadesAgropeacuarias.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)this.gvActividadesAgropeacuarias.Columns[2], "txtActividad") as ASPxTextBox;
                        ckEstadoIni = this.gvActividadesAgropeacuarias.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)this.gvActividadesAgropeacuarias.Columns[3], "ckEstado") as ASPxCheckBox;
                        btnEditar.CommandName = "Editar";
                        btnEditar.Text = "Editar";
                        btnEliminar.CommandName = "Eliminar";
                        btnEliminar.Text = "Eliminar";
                        btnEditar.ClientSideEvents.Click = string.Empty;
                        btnEliminar.ClientSideEvents.Click = string.Empty;
                        btnEliminar.Visible = false;
                        lbActividad.Visible = true;
                        txtActividad.Visible = false;
                        ckEstadoIni.Enabled = false;
                        this.hdControlEditar.Value = (Convert.ToInt32(this.hdControlEditar.Value) - 1).ToString();
                        this.gvActividadesAgropeacuarias.Columns[0].Width = Convert.ToInt32(this.hdControlEditar.Value).Equals(0) ? 125 : 230;

                        break;

                    case "Aceptar":

                        this.gvActividadesAgropeacuarias.Columns[0].Width = 230;
                        btnEditar = this.gvActividadesAgropeacuarias.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)this.gvActividadesAgropeacuarias.Columns[0], "btEditar") as ASPxButton;
                        btnEliminar = this.gvActividadesAgropeacuarias.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)this.gvActividadesAgropeacuarias.Columns[0], "btElimnar") as ASPxButton;
                        lbIdentificadorAgro = this.gvActividadesAgropeacuarias.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)this.gvActividadesAgropeacuarias.Columns[1], "lbIdentificadorAgro") as ASPxLabel;
                        lbActividad = this.gvActividadesAgropeacuarias.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)this.gvActividadesAgropeacuarias.Columns[2], "lbActividadAgro") as ASPxLabel;
                        txtActividad = this.gvActividadesAgropeacuarias.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)this.gvActividadesAgropeacuarias.Columns[2], "txtActividad") as ASPxTextBox;
                        validador = this.gvActividadesAgropeacuarias.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)this.gvActividadesAgropeacuarias.Columns[2], "vldActividad_" + lbId.Text) as RequiredFieldValidator;
                        ckEstadoIni = this.gvActividadesAgropeacuarias.FindRowCellTemplateControl(e.VisibleIndex, (GridViewDataColumn)this.gvActividadesAgropeacuarias.Columns[3], "ckEstado") as ASPxCheckBox;
                        btnEditar.CommandName = "Editar";
                        btnEditar.Text = "Editar";
                        btnEditar.ClientSideEvents.Click = string.Empty;
                        btnEliminar.ClientSideEvents.Click = string.Empty;
                        btnEliminar.CommandName = "Eliminar";
                        btnEliminar.Text = "Eliminar";
                        btnEliminar.Visible = false;                        
                        txtActividad.Text = txtActividad.Text.TrimEnd().TrimStart();
                        lbActividad.Text = txtActividad.Text;
                        txtActividad.Visible = false;
                        lbActividad.Visible = true;
                        ckEstadoIni.Enabled = false;
                        this.hdControlEditar.Value = (Convert.ToInt32(this.hdControlEditar.Value) - 1).ToString();
                        this.gvActividadesAgropeacuarias.Columns[0].Width = Convert.ToInt32(this.hdControlEditar.Value).Equals(0) ? 125 : 230;
                        this.accesoDatos = new DLAccesEntities();
                        this.accesoDatos.procedimiento = "CPP_SP_EdicionNuevaActividad";
                        this.accesoDatos.tipoejecucion = 1;
                        this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();
                        this.parametros = new ParametrizacionSPQUERY();
                        this.parametros.parametro = "@cppAg_Id";
                        this.parametros.parametroValor = Convert.ToInt32(lbIdentificadorAgro.Text.Trim());
                        this.accesoDatos.parametros.Add(this.parametros);
                        this.parametros = new ParametrizacionSPQUERY();
                        this.parametros.parametro = "@cppAg_Actividad";
                        this.parametros.parametroValor = txtActividad.Text.Trim();
                        this.accesoDatos.parametros.Add(this.parametros);
                        this.parametros = new ParametrizacionSPQUERY();
                        this.parametros.parametro = "@cppAg_Estado";
                        this.parametros.parametroValor = Convert.ToBoolean(ckEstadoIni.Checked.ToString());
                        this.accesoDatos.parametros.Add(this.parametros);
                        this.parametros = new ParametrizacionSPQUERY();
                        this.parametros.parametro = "@cppAg_UsuarioCrea";
                        this.parametros.parametroValor = 0;
                        this.accesoDatos.parametros.Add(this.parametros);
                        this.parametros = new ParametrizacionSPQUERY();
                        this.parametros.parametro = "@cppAg_UsuarioMod";
                        this.parametros.parametroValor = Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString());
                        this.accesoDatos.parametros.Add(this.parametros);
                        this.parametros = new ParametrizacionSPQUERY();
                        this.parametros.parametro = "@accion";
                        this.parametros.parametroValor = 1;
                        this.accesoDatos.parametros.Add(this.parametros);

                        if (this.NuevoActualizar(this.accesoDatos))
                        {
                            this.ConsultarActividad();
                            this.CargaCombo();
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                this.ShowMensajes(0, "Error al ejecutar el proceso :: " + ex.Message);                
            }
        }

        /// <summary>
        /// cuando se realiza un short
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvActividadesAgropeacuarias_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            this.ConsultarActividad();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            this.txtActividadNueva.Text = string.Empty;
            this.ckEstadoNuevo.Checked = true;
            this.ppNuevActividad.ShowOnPageLoad = true;
        }

        /// <summary>
        /// cuando se quiere crear un nuevo registro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAceptarNuevo_Click(object sender, EventArgs e)
        {
            this.NuevoRegistro();
        }

        /// <summary>
        /// Paginado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvActividadesAgropeacuarias_PageIndexChanged(object sender, EventArgs e)
        {
            this.ConsultarActividad();
        }

        /// <summary>
        /// cuando se cambia el tamaño de la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvActividadesAgropeacuarias_PageSizeChanged(object sender, EventArgs e)
        {
            this.ConsultarActividad();
        }

        /// <summary>
        /// se ejecuta cuando se estam creando las filas de la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvActividadesAgropeacuarias_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType.Equals(DevExpress.Web.GridViewRowType.Data))
            {                
                RequiredFieldValidator validador = this.gvActividadesAgropeacuarias.FindRowCellTemplateControl(e.VisibleIndex, null, "vldActividad") as RequiredFieldValidator;
                validador.ID = validador.ID + "_" + e.GetValue("id").ToString();
            }
        }

        #endregion

        #region "Metodos privados"

        /// <summary>
        /// consulta las actividades agropecuarias registradas en la base de datos
        /// </summary>
        private void ConsultarActividad()
        {
            this.actividadesAgroBL = new BLActividadAgropecuaria();
            this.lstActividadesAgro = new List<EntitiesActividadesAgropecuarias>();
            string valor = this.cbActividades.SelectedItem != null ? string.IsNullOrEmpty(this.cbActividades.SelectedItem.Value.ToString()) ? "0" : this.cbActividades.SelectedItem.Value.ToString().Equals("0") ? "0" : this.cbActividades.SelectedItem.Value.ToString() : "0";
            this.accesoDatos = new DLAccesEntities();
            this.parametros = new ParametrizacionSPQUERY() { parametro = "@id_Actividad", parametroValor = Convert.ToInt32(valor) };
            this.accesoDatos.procedimiento = "CPP_SP_ConsultarActividadesAgropecuarias";
            this.accesoDatos.tipoejecucion = 1;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();
            this.accesoDatos.parametros.Add(this.parametros);
            this.lstActividadesAgro = this.actividadesAgroBL.ConsultarActividadAgropecuaria(this.accesoDatos);
            this.gvActividadesAgropeacuarias.DataSource = this.lstActividadesAgro;
            this.gvActividadesAgropeacuarias.DataBind();

            if (this.lstActividadesAgro.Count > 0)
                this.divConsulta.Visible = true;
            else
            {
                this.divConsulta.Visible = false;
                this.ShowMensajes(2, "No hay datos para mostrar");
            }
        }

        /// <summary>
        /// Inserta un nuevo registro y edita uno existente
        /// </summary>
        private bool NuevoActualizar(DLAccesEntities parametros)
        {
            this.actividadesAgroBL = new BLActividadAgropecuaria();
            this.resultadoOperacion = this.actividadesAgroBL.RegistrarEditarActividad(parametros);
            if (this.resultadoOperacion.estado.Equals(1))
            {
                this.ShowMensajes(1, "Operación Exitosa.");
                return true;
            }
            else
            {
                this.ShowMensajes(3, "Problemas en la operacion :: " + this.resultadoOperacion.mensaje);
                return false;
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
        /// carga el combo que filtra las actividades
        /// </summary>
        private void CargaCombo()
        {
            EntitiesActividadesAgropecuarias entidad = new EntitiesActividadesAgropecuarias() { id = 0, actividad = string.Empty, estado = true };
            this.actividadesAgroBL = new BLActividadAgropecuaria();
            this.lstActividadesAgro = new List<EntitiesActividadesAgropecuarias>();
            this.accesoDatos = new DLAccesEntities();
            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@id_Actividad";
            this.parametros.parametroValor = Convert.ToInt32("0");
            this.accesoDatos.procedimiento = "CPP_SP_ConsultarActividadesAgropecuarias";
            this.accesoDatos.tipoejecucion = 1;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();
            this.accesoDatos.parametros.Add(this.parametros);
            this.lstActividadesAgro = this.actividadesAgroBL.ConsultarActividadAgropecuaria(this.accesoDatos);
            this.lstActividadesAgro.Add(entidad);
            this.lstActividadesAgro = this.lstActividadesAgro.OrderBy(a => a.id).ThenBy(a=>a.actividad).ToList();
            this.cbActividades.DataSource = this.lstActividadesAgro;
            this.cbActividades.DataBind();
        }

        /// <summary>
        /// registra nuevas Actividades Agropecuarias
        /// </summary>
        private void NuevoRegistro()
        {
            this.accesoDatos = new DLAccesEntities();
            this.accesoDatos.procedimiento = "CPP_SP_EdicionNuevaActividad";
            this.accesoDatos.tipoejecucion = 1;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();
            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppAg_Id";
            this.parametros.parametroValor = 0;
            this.accesoDatos.parametros.Add(this.parametros);
            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppAg_Actividad";
            this.parametros.parametroValor = this.txtActividadNueva.Text.Trim();
            this.accesoDatos.parametros.Add(this.parametros);
            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppAg_Estado";
            this.parametros.parametroValor = Convert.ToBoolean(this.ckEstadoNuevo.Checked.ToString());
            this.accesoDatos.parametros.Add(this.parametros);
            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppAg_UsuarioCrea";
            this.parametros.parametroValor = Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString());
            this.accesoDatos.parametros.Add(this.parametros);
            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppAg_UsuarioMod";
            this.parametros.parametroValor = 0;
            this.accesoDatos.parametros.Add(this.parametros);
            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@accion";
            this.parametros.parametroValor = 0;
            this.accesoDatos.parametros.Add(this.parametros);

            if (this.NuevoActualizar(this.accesoDatos))
            {
                this.ppNuevActividad.ShowOnPageLoad = false;
                this.ConsultarActividad();
                this.CargaCombo();
            }
        }


        #endregion        
    }
}