namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLTipoCocepto
    {
        #region "Definicion de metodos privados"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesTipoConcepto> lstTipoConcepto = null;
        private EntitiesTipoConcepto tipoConcepto = null;

        #endregion

        /// <summary>
        /// consulta los tipos de cocepto
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesTipoConcepto> ConsultaTipoConcepto(DLAccesEntities parametros)
        {
            this.lstTipoConcepto = new List<EntitiesTipoConcepto>();
            DataTable tbTipoConceoto = this.acceso.Consultas(parametros);

            if (tbTipoConceoto != null && tbTipoConceoto.Rows.Count > 0)
            {
                foreach (DataRow fila in tbTipoConceoto.Rows)
                {
                    this.tipoConcepto = new EntitiesTipoConcepto
                    {
                        cppTc_Id = Convert.ToInt32(fila["identificador"].ToString()),
                        cppTc_Descripcion = fila["tipo concepto"].ToString(),
                        cppTc_Estado = Convert.ToBoolean(fila["estadoReg"].ToString())
                    };

                    this.lstTipoConcepto.Add(tipoConcepto);
                }
            }
            return lstTipoConcepto;
        }
    }
}
