namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class BLPlanPagos
    {
        #region "Definicion de metodos privados"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiescppTipoPlanPagos> lstTipoPlanPagos = null;
        private EntitiescppTipoPlanPagos tipoPlanPagos = null;
        private List<EntitiescppModalidadCapital> lstModalidadCapital = null;
        private EntitiescppModalidadCapital modalidadCapital = null;

        #endregion

        /// <summary>
        /// consulta los tipos de planes de pagos
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiescppTipoPlanPagos> ConsultarTipoPlanPagos(DLAccesEntities parametros)
        {
            try
            {
                this.lstTipoPlanPagos = new List<EntitiescppTipoPlanPagos>();
                DataTable dtTipoPlanPagos = this.acceso.Consultas(parametros);

                if (dtTipoPlanPagos != null && dtTipoPlanPagos.Rows.Count > 0)
                {
                    foreach (DataRow fila in dtTipoPlanPagos.Rows)
                    {
                        this.tipoPlanPagos = new EntitiescppTipoPlanPagos();
                        this.tipoPlanPagos.id = Convert.ToInt32(fila["identificador"].ToString());
                        this.tipoPlanPagos.tipoPlanPagos = fila["plan de pagos"].ToString();
                        this.tipoPlanPagos.estado = Convert.ToBoolean(fila["estadoReg"].ToString());
                        this.lstTipoPlanPagos.Add(tipoPlanPagos);
                    }
                }

                lstTipoPlanPagos = lstTipoPlanPagos.OrderBy(a => a.tipoPlanPagos).ToList();

                return lstTipoPlanPagos;
            }
            catch (Exception ex)
            {
                throw (new Exception("BL - BLPlanPagos - ConsultarTipoPlanPagos :: " + ex.Message));
            }
        }

        /// <summary>
        /// consulta la modalidad de capital
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiescppModalidadCapital> ConsultarModalidadCapital(DLAccesEntities parametros)
        {
            try
            {
                this.lstModalidadCapital = new List<EntitiescppModalidadCapital>();
                DataTable dtModalidadCapital = this.acceso.Consultas(parametros);

                if (dtModalidadCapital != null && dtModalidadCapital.Rows.Count > 0)
                {
                    foreach (DataRow fila in dtModalidadCapital.Rows)
                    {
                        this.modalidadCapital = new EntitiescppModalidadCapital();
                        this.modalidadCapital.id = Convert.ToInt32(fila["identificador"].ToString());
                        this.modalidadCapital.modalidad = fila["modalidad"].ToString();
                        this.modalidadCapital.estado = Convert.ToBoolean(fila["estadoReg"].ToString());
                        this.lstModalidadCapital.Add(modalidadCapital);
                    }
                }

                lstModalidadCapital = lstModalidadCapital.OrderBy(a => a.modalidad).ToList();

                return lstModalidadCapital;
            }
            catch (Exception ex)
            {
                throw (new Exception("BL - BLPlanPagos - ConsultarModalidadCapital :: " + ex.Message));
            }
        }
    }
}
