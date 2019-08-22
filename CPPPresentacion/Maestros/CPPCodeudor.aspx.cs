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

    public partial class CPPCodeudor : System.Web.UI.Page
    {
        #region "definicion variables privadas"

        private SSOSesion usuarioCpp = new SSOSesion();
        private DLAccesEntities accesoDatos = null;
        private ParametrizacionSPQUERY parametros = null;
        private ResultConsulta resultadoOperacion = null;
        readonly private Validaciones validar = new Validaciones();
        readonly private BLCodeudor codeudorBL = new BLCodeudor();

        #endregion

        #region "metodos protegidos"

        protected void Page_Load(object sender, EventArgs e)
        {
            this.usuarioCpp = (SSOSesion)Session["FINAGRO_SSO_SESION"];
            try
            {
                if (!this.IsPostBack)
                {
                    this.CargatipoDoc();
                    this.CargarDepartmentos();
                    this.CargarMunicipios();
                    this.txIdentificacion.Attributes.Add("onKeyPress", "return soloNumeros(event)");
                    this.CargarGrilla();
                    this.validar.RegLog("Page_Load()", "CPP_SP_ConsultarCodeudor", TipoAccionLog.consulta.ToString(), Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
                }
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("Page_Load()", "N/A", "N/A", "Error al ejecutar el proceso (Page_Load): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Page_Load): " + ex.Message);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxEdit.ClearEditorsInContainer(this.pcCodeudor, "datos", true);
                this.LimpiarObjetos();
                this.pcCodeudor.ShowOnPageLoad = true;
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnNuevo_Click()", "N/A", "N/A", "Error al ejecutar el proceso (Nuevo) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Nuevo) :: " + ex.Message);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string accion = string.IsNullOrEmpty(this.hdIdCodeudor.Value) ? TipoAccionLog.insercion.ToString() : TipoAccionLog.actualizacion.ToString();

            try
            {
                this.EjeciconSQLEditInser();
                this.validar.RegLog("btnAceptar_Click", "CPP_SP_EditarInsertarCodeudor", accion, Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()), this.Form.Page.ToString());
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("btnAceptar_Click()", "N/A", "N/A", "Error al ejecutar el proceso (btnAceptar_Click) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (btnAceptar_Click) :: " + ex.Message);
            }
        }

        /// <summary>
        /// Metodo que se llamada del lado del cliente para realizar la operacion de llenar el combo de municipio
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void CmbCity_Callback(object source, CallbackEventArgsBase e)
        {
            try
            {
                FillCityCombo(e.Parameter);
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("CmbCity_Callback()", "N/A", "N/A", "Error al ejecutar el proceso (CmbCity_Callback): " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (CmbCity_Callback): " + ex.Message);
            }
        }

        protected void gvCodeudor_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                ASPxButton btnEditar = new ASPxButton();
                ASPxButton btnEliminar = new ASPxButton();
                RequiredFieldValidator validador = new RequiredFieldValidator();

                switch (e.CommandArgs.CommandName)
                {
                    case "Editar":

                        ASPxEdit.ClearEditorsInContainer(this.pcCodeudor, "datos", true);
                        this.accesoDatos = new DLAccesEntities();
                        this.accesoDatos.procedimiento = "CPP_SP_ConsultarCodeudor";
                        this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
                        this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

                        this.parametros = new ParametrizacionSPQUERY();
                        this.parametros.parametro = "@cppCo_Id";
                        this.parametros.parametroValor = Convert.ToInt32(e.KeyValue.ToString());

                        this.accesoDatos.parametros.Add(this.parametros);
                        this.SetObjetAfterUpdate(this.ConsultarCodeudor());
                        this.pcCodeudor.ShowOnPageLoad = true;                        

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

        protected void gvCodeudor_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.CargarGrilla();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvCodeudor_PageIndexChanged()", "N/A", "N/A", "Error al ejecutar el proceso (Paginación) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Paginación) :: " + ex.Message);
            }
        }

        protected void gvCodeudor_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            try
            {
                this.CargarGrilla();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvCodeudor_BeforeColumnSortingGrouping()", "N/A", "N/A", "Error al ejecutar el proceso (Ordenamiento) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ordenamiento) :: " + ex.Message);
            }
        }

        protected void gvCodeudor_PageSizeChanged(object sender, EventArgs e)
        {
            try
            {
                this.CargarGrilla();
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("gvCodeudor_PageSizeChanged()", "N/A", "N/A", "Error al ejecutar el proceso (Ajuste por pagina) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ajuste por pagina) :: " + ex.Message);
            }
        }

        protected void Grid_Load(object sender, EventArgs e)
        {
            gvCodeudor.SettingsBehavior.FilterRowMode = GridViewFilterRowMode.Auto;
            try
            {
                this.CargarGrilla();                
            }
            catch (Exception ex)
            {
                int usuarioSesion = this.usuarioCpp != null ? Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString()) : 0;
                this.validar.RegLogError("Grid_Load()", "N/A", "N/A", "Error al ejecutar el proceso (Ajuste por pagina) :: " + ex.Message + " -- " + ex.StackTrace, usuarioSesion, this.Form.Page.ToString());
                this.ShowMensajes(0, "Error al ejecutar el proceso (Ajuste por pagina) :: " + ex.Message);
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
        /// consulta los departamentos y carga el combo deptos
        /// </summary>
        private void CargarDepartmentos()
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
        /// Metodo encargo de llenar el combo box de municio
        /// </summary>
        /// <param name="countryName"></param>
        protected void FillCityCombo(string countryName)
        {
            try
            {
                if (string.IsNullOrEmpty(countryName)) return;

                this.cbMunicipio.Items.Clear();
                List<Municipios> lstMunicipios = new List<Municipios>();
                lstMunicipios = (List<Municipios>)Session["municipios"];
                this.cbMunicipio.DataSource = lstMunicipios.Where(a => a.idDepto.Equals(Convert.ToInt32(countryName)));
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
        /// carga el combo que filtra las actividades
        /// </summary>
        private void CargatipoDoc()
        {
            CDocumento registro = new CDocumento { tdoc_id_tipo_documento = 1000, tdoc_vCodigo = string.Empty };
            List<CDocumento> lstDocumento = new List<CDocumento>();
            lstDocumento = this.usuarioCpp.ConsultarTipoDocumento(Convert.ToInt64(this.usuarioCpp.Usuario.iAplicacion), this.usuarioCpp.Usuario.Token);
            lstDocumento = lstDocumento.OrderBy(a => a.tdoc_vCodigo).ToList();
            this.cbTipoDoc.DataSource = lstDocumento;
            this.cbTipoDoc.DataBind();
            lstDocumento.Add(registro);
            lstDocumento = lstDocumento.OrderBy(a => a.tdoc_vCodigo).ToList();
            Session["TipoDoc"] = lstDocumento;
        }

        /// <summary>
        /// limpia los objetos de form
        /// </summary>
        private void LimpiarObjetos()
        {
            this.hdIdCodeudor.Value = string.Empty;
            this.cbTipoDoc.SelectedIndex = -1;
            this.txIdentificacion.Text = string.Empty;            
            this.txtNombre.Text = string.Empty;
            this.txtApellido.Text = string.Empty;
            this.txtTelefono.Text = string.Empty;
            this.txtCelular.Text = string.Empty;
            this.txtDireccion.Text = string.Empty;
            this.txtEmail.Text = string.Empty;
            this.cbDepartamento.SelectedIndex = -1;
            this.cbMunicipio.Items.Clear();
            this.cbMunicipio.Text = string.Empty;            
        }

        /// <summary>
        /// trae la informacion de los codeudores
        /// </summary>
        /// <returns></returns>
        private List<EntitiesCodeudor> ConsultarCodeudor()
        {
            List<EntitiesCodeudor> registros = this.codeudorBL.ConsultarCodeudor(this.accesoDatos);

            foreach (EntitiesCodeudor registro in registros)
            {
                registro.departamento = ((List<Depatamentos>)Session["Depto"]).Where(a => a.id.Equals(registro.cppCo_IdDepto)).Select(a => a.depratamento).FirstOrDefault();
                registro.municipio = ((List<Municipios>)Session["municipios"]).Where(a => a.id.Equals(registro.cppCo_Idmun) && a.idDepto.Equals(registro.cppCo_IdDepto)).Select(a => a.municipio).FirstOrDefault();
                registro.Tipoiden = ((List<CDocumento>)Session["TipoDoc"]).Where(a => a.tdoc_id_tipo_documento.Equals(registro.cppCo_IdTipoiden)).Select(a => a.tdoc_vCodigo).FirstOrDefault();                
            }

            return registros;
        }

        /// <summary>
        /// carga la informacion del codeudor seleccionado
        /// </summary>
        /// <param name="datos"></param>
        private void SetObjetAfterUpdate(List<EntitiesCodeudor> datos)
        {
            this.cbTipoDoc.SelectedIndex = this.cbTipoDoc.Items.FindByText(datos[0].Tipoiden).Index;
            this.cbDepartamento.SelectedIndex = this.cbDepartamento.Items.FindByText(datos[0].departamento).Index;
            this.CargarMunicipio();
            this.cbMunicipio.SelectedIndex = this.cbMunicipio.Items.FindByText(datos[0].municipio).Index;
            this.hdIdCodeudor.Value = datos[0].cppCo_Id.ToString();
            this.txIdentificacion.Text = datos[0].cppCo_Identificacion.ToString();
            this.txtNombre.Text = datos[0].cppCo_Nombre.ToString();
            this.txtApellido.Text = datos[0].cppCo_Apellido.ToString();
            this.txtTelefono.Text = datos[0].cppCo_Telefono.ToString();
            this.txtCelular.Text = datos[0].cppCo_Celular.ToString();
            this.txtDireccion.Text = datos[0].cppCo_Direccion.ToString();
            this.txtEmail.Text = datos[0].cppCo_email.ToString();
        }

        /// <summary>
        /// configura la consulta de los codeudores
        /// </summary>
        private void ConfiguracionConsultaGeneral()
        {
            this.accesoDatos = new DLAccesEntities();
            this.accesoDatos.procedimiento = "CPP_SP_ConsultarCodeudor";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();            
        }

        /// <summary>
        /// carga la grilla
        /// </summary>
        private void CargarGrilla()
        {
            this.ConfiguracionConsultaGeneral();
            this.gvCodeudor.DataSource = this.ConsultarCodeudor();
            this.gvCodeudor.DataBind();
        }

        /// <summary>
        /// se invoca para realizar peticiones a la base de datos (Edicion inserción)
        /// </summary>
        private void EjeciconSQLEditInser()
        {
            this.accesoDatos = new DLAccesEntities();            
            this.parametros = new ParametrizacionSPQUERY();

            this.accesoDatos.procedimiento = "CPP_SP_EditarInsertarCodeudor";
            this.accesoDatos.tipoejecucion = (int)TiposEjecucion.Procedimiento;
            this.accesoDatos.parametros = new List<ParametrizacionSPQUERY>();

            if (!string.IsNullOrEmpty(this.hdIdCodeudor.Value))
            {
                this.parametros.parametro = "@cppCo_Id";
                this.parametros.parametroValor = Convert.ToInt32(this.hdIdCodeudor.Value.Trim());
                this.accesoDatos.parametros.Add(this.parametros);
            }

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppCo_IdTipoiden";
            this.parametros.parametroValor = Convert.ToInt32(this.cbTipoDoc.SelectedItem.Value.ToString().Trim());
            this.accesoDatos.parametros.Add(this.parametros);            

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppCo_Identificacion";
            this.parametros.parametroValor = Convert.ToInt64(this.txIdentificacion.Text.Trim());
            this.accesoDatos.parametros.Add(this.parametros);            

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppCo_Nombre";
            this.parametros.parametroValor = this.txtNombre.Text.Trim();
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppCo_Apellido";
            this.parametros.parametroValor = this.txtApellido.Text.Trim();
            this.accesoDatos.parametros.Add(this.parametros);

            if (!string.IsNullOrEmpty(this.txtDireccion.Text.Trim()))
            {
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppCo_Direccion";
                this.parametros.parametroValor = this.txtDireccion.Text.Trim();
                this.accesoDatos.parametros.Add(this.parametros);
            }

            if (!string.IsNullOrEmpty(this.txtTelefono.Text.Trim()))
            {
                this.txtTelefono.Text = this.txtTelefono.Text.Replace(" ", "");
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppCo_Telefono";
                this.parametros.parametroValor = Convert.ToInt64(this.txtTelefono.Text);
                this.accesoDatos.parametros.Add(this.parametros);
            }

            if (!string.IsNullOrEmpty(this.txtCelular.Text.Trim()))
            {
                this.txtCelular.Text = this.txtCelular.Text.Replace(" ", "");
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppCo_Celular";
                this.parametros.parametroValor = Convert.ToInt64(this.txtCelular.Text);
                this.accesoDatos.parametros.Add(this.parametros);
            }

            if (!string.IsNullOrEmpty(this.txtEmail.Text.Trim()))
            {
                this.parametros = new ParametrizacionSPQUERY();
                this.parametros.parametro = "@cppCo_email";
                this.parametros.parametroValor = this.txtEmail.Text.Trim();
                this.accesoDatos.parametros.Add(this.parametros);
            }

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppCo_IdDepto";
            this.parametros.parametroValor = Convert.ToInt32(this.cbDepartamento.SelectedItem.Value.ToString().Trim());
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@cppCo_Idmun";
            this.parametros.parametroValor = Convert.ToInt32(this.cbMunicipio.SelectedItem.Value.ToString().Trim());
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = string.IsNullOrEmpty(this.hdIdCodeudor.Value) ? "@cppCo_UsuarioCrea" : "@cppCo_UsuarioMod";
            this.parametros.parametroValor = Convert.ToInt32(this.usuarioCpp.Usuario.Identificador.ToString());
            this.accesoDatos.parametros.Add(this.parametros);

            this.parametros = new ParametrizacionSPQUERY();
            this.parametros.parametro = "@accion";
            this.parametros.parametroValor = string.IsNullOrEmpty(this.hdIdCodeudor.Value) ? Convert.ToBoolean("true") : Convert.ToBoolean("false");
            this.accesoDatos.parametros.Add(this.parametros);

            if (this.OperacionInsertUpdate())
            {
                this.CargarGrilla();
                this.pcCodeudor.ShowOnPageLoad = false;
            }
        }

        /// <summary>
        /// realiza la acciion sobre la base de datos
        /// </summary>
        /// <returns></returns>
        private bool OperacionInsertUpdate()
        {
            this.resultadoOperacion = this.codeudorBL.RegistrarEditarCodeudor(this.accesoDatos);

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
        /// carga los municipios en el combo municipio
        /// </summary>
        private void CargarMunicipio()
        {
            this.cbMunicipio.Items.Clear();
            List<Municipios> lstMunicipios = new List<Municipios>();
            lstMunicipios = (List<Municipios>)Session["municipios"];
            this.cbMunicipio.DataSource = lstMunicipios.Where(a => a.idDepto.Equals(Convert.ToInt32(this.cbDepartamento.SelectedItem.Value.ToString())));
            this.cbMunicipio.DataBind();
            this.cbMunicipio.SelectedIndex = -1;
        }
        #endregion
    }
}