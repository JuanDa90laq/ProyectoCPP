namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLCondiciones
    {
        readonly private DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesCondiciones> lstCondiciones = null;
        EntitiesCondiciones condiciones = null;        

        /// <summary>
        /// consulta las condiciones
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesCondiciones> ConsultarCondiciones(DLAccesEntities parametros)
        {
            try
            {
                this.lstCondiciones = new List<EntitiesCondiciones>();
                DataTable dtCondiciones = this.acceso.Consultas(parametros);

                if (dtCondiciones != null && dtCondiciones.Rows.Count > 0)
                {
                    foreach (DataRow fila in dtCondiciones.Rows)
                    {
                        this.condiciones = new EntitiesCondiciones();
                        this.condiciones.cppCd_Id = Convert.ToInt32(fila["cppCd_Id"].ToString());
                        this.condiciones.cppCd_descripcion = fila["cppCd_descripcion"].ToString();
                        this.condiciones.cppCd_Estado = Convert.ToBoolean(fila["cppCd_Estado"].ToString());

                        this.lstCondiciones.Add(this.condiciones);
                    }
                }
                return this.lstCondiciones;
            }
            catch (Exception ex)
            {
                throw (new Exception("BLCondiciones - Consultarbeneficiarios :: " + ex.Message));
            }
        }
    }
}
