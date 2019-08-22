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

    public class BLBeneficioCapital
    {
        #region "Definicion de variables privadas"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesAdminBeneficiosCapital> lsBeneficiosCapital = null;
        private EntitiesAdminBeneficiosCapital beneficiosCapital = null;

        #endregion

        /// <summary>
        /// consulta los beneficios de capital
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesAdminBeneficiosCapital> ConcultarbeneficiosCapital(DLAccesEntities parametros)
        {
            try
            {
                this.lsBeneficiosCapital = new List<EntitiesAdminBeneficiosCapital>();
                DataTable beneficiosTb = this.acceso.Consultas(parametros);

                if (beneficiosTb != null && beneficiosTb.Rows.Count > 0)
                {
                    foreach (DataRow fila in beneficiosTb.Rows)
                    {
                        this.beneficiosCapital = new EntitiesAdminBeneficiosCapital();
                        this.beneficiosCapital.cppAb_Id = Convert.ToInt32(fila["cppAb_Id"].ToString());
                        this.beneficiosCapital.cppAb_IdPr = Convert.ToInt32(fila["cppAb_IdPr"].ToString());
                        this.beneficiosCapital.cppAb_IdCd = Convert.ToInt32(fila["cppAb_IdCd"].ToString());
                        this.beneficiosCapital.cppAb_IdDepto = Convert.ToInt32(fila["cppAb_IdDepto"].ToString());
                        this.beneficiosCapital.cppAb_IdMun = Convert.ToInt32(fila["cppAb_IdMun"].ToString());
                        this.beneficiosCapital.cppAb_IdActividad = Convert.ToInt32(fila["cppAb_IdActividad"].ToString());
                        this.beneficiosCapital.valor = Convert.ToDecimal(fila["cppAb_Valor"].ToString());
                        this.beneficiosCapital.cppAb_FechaInicio = Convert.ToDateTime(fila["cppAb_FechaInicio"].ToString());
                        this.beneficiosCapital.cppAb_FechaFinal = Convert.ToDateTime(fila["cppAb_FechaFinal"].ToString());
                        this.beneficiosCapital.cppAb_Descripcion = fila["cppAb_Descripcion"].ToString();
                        this.beneficiosCapital.cppAb_IdActividadTotal = fila["cppAb_Actividades"].ToString();
                        this.beneficiosCapital.segundoValor = string.IsNullOrEmpty(fila["cppAb_SegundoValor"].ToString())  ? (decimal?)null : Convert.ToDecimal(fila["cppAb_SegundoValor"].ToString());
                        this.lsBeneficiosCapital.Add(this.beneficiosCapital);
                    }
                }
                return lsBeneficiosCapital;
            }
            catch (Exception ex)
            {
                throw (new Exception("BLBeneficioCapital - ConcultarbeneficiosCapital :: " + ex.Message));
            }
        }

        /// <summary>
        /// registra o edita un nuevo beneficio de capital
        /// </summary>
        /// <param name="parametros">parametrizacion ejecucion consulta a base de datos</param>
        /// <returns>respuesta de la ejecucion </returns>
        public ResultConsulta RegistrarEditarBeneficioCapital(DLAccesEntities parametros)
        {
            try
            {
                return this.acceso.Registros(parametros);
            }
            catch (Exception ex)
            {
                throw (new Exception("BLBeneficioCapital - RegistrarEditarBeneficiario :: " + ex.Message));
            }
        }
    }
}
