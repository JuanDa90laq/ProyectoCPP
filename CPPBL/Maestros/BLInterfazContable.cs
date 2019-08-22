namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BLInterfazContable
    {
        #region "Definicion de variables privadas"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesInterfaz> lsinterfaz = null;
        private EntitiesInterfaz interfaz = null;

        #endregion

        /// <summary>
        /// consulta las interfaces contables 
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesInterfaz> ConcultarIterfaz(DLAccesEntities parametros)
        {
            try
            {
                this.lsinterfaz = new List<EntitiesInterfaz>();
                DataTable iterfazTb = this.acceso.Consultas(parametros);

                if (iterfazTb != null && iterfazTb.Rows.Count > 0)
                {
                    foreach (DataRow fila in iterfazTb.Rows)
                    {
                        this.interfaz = new EntitiesInterfaz();
                        this.interfaz.cppIn_id = Convert.ToInt32(fila["cppIn_id"].ToString());
                        this.interfaz.cppIn_descripcion = fila["cppIn_descripcion"].ToString();
                        this.interfaz.cppIn_estado = Convert.ToBoolean(fila["cppIn_estado"].ToString());
                        
                        this.lsinterfaz.Add(this.interfaz);
                    }
                }
                return lsinterfaz;
            }
            catch (Exception ex)
            {
                throw (new Exception("BLInterfazContable - ConcultarIterfaz :: " + ex.Message));
            }
        }
    }
}
