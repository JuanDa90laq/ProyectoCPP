namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLTiposCuenta
    {
        #region "Definicion de metodos privados"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesTiposCuenta> lstTiposCuenta = null;
        private EntitiesTiposCuenta tiposCuenta = null;

        #endregion

        /// <summary>
        /// consulta los tipos de cuenta
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesTiposCuenta> ConsultaTiposCuenta(DLAccesEntities parametros)
        {
            try
            {
                this.lstTiposCuenta = new List<EntitiesTiposCuenta>();
                DataTable tbTiposCuenta = this.acceso.Consultas(parametros);

                if (tbTiposCuenta != null && tbTiposCuenta.Rows.Count > 0)
                {
                    foreach (DataRow fila in tbTiposCuenta.Rows)
                    {
                        this.tiposCuenta = new EntitiesTiposCuenta
                        {
                            cppCt_Id = Convert.ToInt32(fila["cppCt_Id"].ToString()),
                            cppCt_Nombre = fila["cppCt_Nombre"].ToString(),
                            cppCt_Estado = Convert.ToBoolean(fila["cppCt_Estado"].ToString())                            
                        };

                        this.lstTiposCuenta.Add(tiposCuenta);
                    }
                }
                return lstTiposCuenta;
            }
            catch (Exception ex)
            {
                throw (new Exception("BLTiposCuenta - ConsultaTiposCuenta :: " + ex.Message));
            }
        }
    }
}
