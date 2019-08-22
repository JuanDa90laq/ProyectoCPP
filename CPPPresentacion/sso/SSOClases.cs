using System;
using System.Data;
using System.Web.UI;
using System.Collections;
using System.Collections.Generic;

namespace SSO.Finagro
{




    public class cUsuarioSession
    {
        public int iAplicacion { get; set; }
        public int iUsuario { get; set; }
        public string Token { get; set; }
    }


    #region < Registro Usuarios >

    public class cProceso
    {
        public int Select { get; set; }
        public int Update { get; set; }
        public int Insert { get; set; }
    }
    
    public class cPreguntasSeguridad
    {
        public int idproceso { get; set; }
        public string email { get; set; }
        public int respuesta { get; set; }
        public string mensaje { get; set; }
        public int iPregunta1 { get; set; }
        public string vRespuesta1 { get; set; }
        public int iPregunta2 { get; set; }
        public string vRespuesta2 { get; set; }
        public int iPregunta3 { get; set; }
        public string vRespuesta3 { get; set; }
        public int iPregunta4 { get; set; }
        public string vRespuesta4 { get; set; }

        public List<CPreguntas_Respuestas> ListPreguntas { get; set; }

        
    }

    public class CPreguntas_Respuestas
    {
        public string email { get; set; }
        public int iPreguntacmb { get; set; }
        public string vPreguntaDecripcioncmb { get; set; }
        public int respuesta { get; set; }
        public string mensaje { get; set; }
        public int idusuario { get; set; }
        public string NombrePC { get; set; }
        public string ipPC { get; set; }
    }

    public class cRegistroUsuario
    {

        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string celular { get; set; }
        public string Email { get; set; }
        public int Tipodocumento { get; set; }
        public string Identificacion { get; set; }
        public int respuesta { get; set; }

    }

    public class cActivacionCuenta
    {
        public string Email { get; set; }
        public int respuesta { get; set; }
    }
    public class cBloqueoCuenta
    {
        public string Email { get; set; }

        public string FechaActual { get; set; }
        public int respuesta { get; set; }
        public string Mensajes { get; set; }

    }

    

    #endregion

    #region < Clase Menu >
    public class CMenu
    {
        public int Identificador { get; set; }
        public string Titulo { get; set; }
        public string URL { get; set; }
        public bool? Activo { get; set; }
        public int IdAplicativo { get; set; }
        public int IdPadre { get; set; }
        public int Orden { get; set; }
        public string Mensajes { get; set; }
        public int Resultado { get; set; }
    }
    #endregion

    #region < Clase Tipo Documento >
    public class CDocumento
    {
        public int tdoc_id_tipo_documento { get; set; }
        public string tdoc_vCodigo { get; set; }
        public int Identificador { get; set; }
        public string Mensajes { get; set; }
    }
    #endregion

    #region < Clase Aplicativo >
    public class CAplicativo
    {        
        public int Identificador { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string URL { get; set; }
        public string Imagen { get; set; }
        public string Version { get; set; }
        public bool? Activo { get; set; }
        public bool? Estado { get; set; }
        public string Mensajes { get; set; }
        public int Resultado { get; set; }
    }
    #endregion

    #region < Clase Perfil >
    public class CPerfil
    {     
        public int Identificador { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Activo { get; set; }
        public int IdAplicativo { get; set; }
        public List<CPermiso> ListaPermisos { get; set; }
        public List<CMenu> ListaMenus { get; set; }
        public string Mensajes { get; set; }
        public int Resultado { get; set; }
    }
    #endregion

    #region < Clase Permiso >
    public class CPermiso
    {
        public int Identificador { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public int IdAplicativo { get; set; }
        public string Mensajes { get; set; }
        public int Resultado { get; set; }
    }
    #endregion

    #region < Clase tipo documentos usuarios SSO y Externos >

    #endregion
    public class CTipoDocumentos
    {
        public int Identificador { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public int IdAplicativo { get; set; }
        public string Mensajes { get; set; }
        public int Resultado { get; set; }
    }

    #region < Clase Usuario >
    public class CUsuario
    {
        public long Identificador { get; set; }
        public string Correo { get; set; }
        public int? TipoDocumento { get; set; }        
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dependencia { get; set; }
        public string Cargo { get; set; }

        public string Celular { get; set; }

        public bool Activo { get; set; }
        public List<CPerfil> ListaPerfiles { get; set; }
        public List<CMenu> ListaMenus { get; set; }
        public List<CAplicativo> ListaAplicativos { get; set; }
        public List<CPermiso> ListaPermisos { get; set; }

        public List<CDocumento> ListaTipoDocumentos { get; set; }
                
        public string Mensajes { get; set; }
        public int Resultado { get; set; }
        public string Token { get; set; }
        public int Preguntas { get; set; }
        public bool ControlPreguntas { get; set; }

        public bool ControlIP { get; set; }

        public int ControlPreguntasbd { get; set; }
        public string ipPcConfianza { get; set; }
        public string PcConfianzaNombre { get; set; }

        public List<CequiposConfianza> ListaEquiposComfianza { get; set; }

        public int validacionBloqPass { get; set; }

        public int iAplicacion { get; set; }
    }

    public class CequiposConfianza
    {
        public int IdentificadorPC{ get; set; }
        public string ipPcConfianza { get; set; }
        public string PcConfianzaNombre { get; set; }
        public int Identificador { get; set; }
        public string Mensajes { get; set; }
    }

    public class CUsuarioToken
    {
        long iIdentificador;
        string sCorreo;
        string sDocumento;
        string sNombre;
        string sApellido;
        string sArea;
        string sCargo;
        string sToken;
        string sMensajes;
        string sDependencia;
        int iRespuesta;
        int iTipoDocumento;

        public long Identificador
        {
            get { return iIdentificador; }
            set { iIdentificador = value; }
        }

        public string Correo
        {
            get { return sCorreo; }
            set { sCorreo = value; }
        }

        public int TipoDocumento
        {
            get { return iTipoDocumento; }
            set { iTipoDocumento = value; }
        }

        public string Documento
        {
            get { return sDocumento; }
            set { sDocumento = value; }
        }

        public string Nombre
        {
            get { return sNombre; }
            set { sNombre = value; }
        }

        public string Apellido
        {
            get { return sApellido; }
            set { sApellido = value; }
        }

        public string Mensajes
        {
            get { return sMensajes; }
            set { sMensajes = value; }
        }

        public string Token
        {
            get { return sToken; }
            set { sToken = value; }
        }

        public string Area
        {
            get { return sArea; }
            set { sArea = value; }
        }

        public string Cargo
        {
            get { return sCargo; }
            set { sCargo = value; }
        }

        public string Dependencia
        {
            get { return sDependencia; }
            set { sDependencia = value; }
        }

        public int Respuesta
        {
            get { return iRespuesta; }
            set { iRespuesta = value; }
        }
    }
    #endregion

    #region < Clase LoginRequest >
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    #endregion

    #region < Clase Registro de sesion >

    public class SessionRegister
    {
        public string cookie { get; set; }
        public string ip { get; set; }
        public string tok { get; set; }
        public string usuario { get; set; }
        public string fecha { get; set; }

        /// <summary>
        /// 0 = inserta, 1 = actualiza
        /// </summary>
        public string accion { get; set; }
    }

    #endregion

    #region < Clase HierarchicalDataSet >

    /// <summary>
    /// A class that translates a DataSet into IHierarchicalDataSource that can be used to bind Hierarchical data to a TreeView
    /// </summary>
    public class HierarchicalDataSet : IHierarchicalDataSource
    {
        DataSet dataSet;
        readonly string idColumnName;
        string parentIdColumnName;

        /// <summary>
        /// The constructor of the class
        /// </summary>
        /// <param name="dataSet">The dataset that contains the data</param>
        /// <param name="idColumnName">The Primary key column name</param>
        /// <param name="parentidColumnName">The Parent Primary key column name that identifies the Parent-Child relationship</param>
        public HierarchicalDataSet(DataSet dataSet, string idColumnName, string parentIdColumnName)
        {
            this.dataSet = dataSet;
            this.idColumnName = idColumnName;
            this.parentIdColumnName = parentIdColumnName;
        }

        public event EventHandler DataSourceChanged; // never used here

        public HierarchicalDataSourceView GetHierarchicalView(string viewPath) => new DataSourceView(this, viewPath);

        #region supporting methods
        DataRowView GetParentRow(DataRowView row)
        {
            dataSet.Tables[0].DefaultView.RowFilter = string.Format("{0} = {1}", idColumnName, row[parentIdColumnName].ToString());
            DataRowView parentRow = dataSet.Tables[0].DefaultView[0];
            dataSet.Tables[0].DefaultView.RowFilter = "";
            return parentRow;
        }

        string GetChildrenViewPath(string viewPath, DataRowView row) => viewPath + "\\" + row[idColumnName].ToString();

        bool HasChildren(DataRowView row)
        {
            dataSet.Tables[0].DefaultView.RowFilter = string.Format("{0} = {1}", parentIdColumnName, row[idColumnName]);
            bool hasChildren = dataSet.Tables[0].DefaultView.Count > 0;
            dataSet.Tables[0].DefaultView.RowFilter = "";
            return hasChildren;
        }

        string GetParentViewPath(string viewPath)
        {
            return viewPath.Substring(0, viewPath.LastIndexOf("\\"));
        }
        #endregion

        #region private classes that implement further interfaces
        class DataSourceView : HierarchicalDataSourceView
        {
            readonly HierarchicalDataSet hDataSet;
            readonly string viewPath;

            public DataSourceView(HierarchicalDataSet hDataSet, string viewPath)
            {
                this.hDataSet = hDataSet;
                this.viewPath = viewPath;
            }

            public override IHierarchicalEnumerable Select()
            {
                return new HierarchicalEnumerable(hDataSet, viewPath);
            }
        }

        class HierarchicalEnumerable : IHierarchicalEnumerable
        {
            HierarchicalDataSet hDataSet;
            string viewPath;

            public HierarchicalEnumerable(HierarchicalDataSet hDataSet, string viewPath)
            {
                this.hDataSet = hDataSet;
                this.viewPath = viewPath;
            }

            public IHierarchyData GetHierarchyData(object enumeratedItem)
            {
                DataRowView row = (DataRowView)enumeratedItem;
                return new HierarchyData(hDataSet, viewPath, row);
            }

            public IEnumerator GetEnumerator()
            {
                if (viewPath == "")
                    hDataSet.dataSet.Tables[0].DefaultView.RowFilter = string.Format("{0} is null", hDataSet.parentIdColumnName);
                else
                {
                    string lastID = viewPath.Substring(viewPath.LastIndexOf("\\") + 1);
                    hDataSet.dataSet.Tables[0].DefaultView.RowFilter = string.Format("{0} = {1}", hDataSet.parentIdColumnName, lastID);
                }

                IEnumerator i = hDataSet.dataSet.Tables[0].DefaultView.GetEnumerator();
                hDataSet.dataSet.Tables[0].DefaultView.RowFilter = "";
                return i;
            }
        }

        class HierarchyData : IHierarchyData
        {
            HierarchicalDataSet hDataSet;
            DataRowView row;
            readonly string viewPath;

            public HierarchyData(HierarchicalDataSet hDataSet, string viewPath, DataRowView row)
            {
                this.hDataSet = hDataSet;
                this.viewPath = viewPath;
                this.row = row;
            }

            public IHierarchicalEnumerable GetChildren()
            {
                return new HierarchicalEnumerable(hDataSet, hDataSet.GetChildrenViewPath(viewPath, row));
            }

            public IHierarchyData GetParent()
            {
                return new HierarchyData(hDataSet, hDataSet.GetParentViewPath(viewPath), hDataSet.GetParentRow(row));
            }

            public bool HasChildren
            {
                get
                {
                    return hDataSet.HasChildren(row);
                }
            }

            public object Item
            {
                get
                {
                    return row;
                }
            }

            public string Path
            {
                get
                {
                    return viewPath;
                }
            }

            public string Type
            {
                get
                {
                    return typeof(DataRowView).ToString();
                }
            }
        }
        #endregion
    }
    #endregion

    #region ActualizarPWD

    public class PWDActualizar
    {
        public string email { get; set; }
        public string PWD { get; set; }        
        
    }

    public class PWDActualizarResult
    {
        public string mensaje { get; set; }
        public int operacion { get; set; }

    }

    #endregion
}