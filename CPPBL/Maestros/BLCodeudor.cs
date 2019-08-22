using CPPDL;
using CPPENL.Maestros;
using CPPENL.Transversal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPPBL.Maestros
{
    public class BLCodeudor
    {
        #region "Definicion de metodos privados"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesCodeudor> lstCodeudor = null;
        private EntitiesCodeudor codeudor = null;

        #endregion

        /// <summary>
        /// consulta los codeudores
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesCodeudor> ConsultarCodeudor(DLAccesEntities parametros)
        {
            try
            {
                this.lstCodeudor = new List<EntitiesCodeudor>();
                DataTable beneficiariosb = this.acceso.Consultas(parametros);

                if (beneficiariosb != null && beneficiariosb.Rows.Count > 0)
                {
                    foreach (DataRow fila in beneficiariosb.Rows)
                    {
                        this.codeudor = new EntitiesCodeudor();
                        this.codeudor.cppCo_Id = Convert.ToInt32(fila["identificador"].ToString());
                        this.codeudor.cppCo_IdTipoiden = Convert.ToInt32(fila["tipo de documento"].ToString());
                        this.codeudor.cppCo_Identificacion = Convert.ToInt64(fila["identificacion"].ToString());

                        this.codeudor.cppCo_Nombre = fila["nombre"].ToString();
                        this.codeudor.cppCo_Apellido = fila["apellido"].ToString();
                        this.codeudor.nombreCompleto = fila["nombre"].ToString() + " " + fila["apellido"].ToString();
                        this.codeudor.cppCo_Direccion = fila["direccion"].ToString();

                        if (string.IsNullOrEmpty(fila["telefono"].ToString()))
                            this.codeudor.cppCo_Telefono = null;
                        else
                            this.codeudor.cppCo_Telefono = Convert.ToInt64(fila["telefono"].ToString());

                        if (string.IsNullOrEmpty(fila["celular"].ToString()))
                            this.codeudor.cppCo_Celular = null;
                        else
                            this.codeudor.cppCo_Celular = Convert.ToInt64(fila["celular"].ToString());

                        this.codeudor.cppCo_email = fila["correo"].ToString();
                        this.codeudor.cppCo_IdDepto = Convert.ToInt32(fila["departamento"].ToString());
                        this.codeudor.cppCo_Idmun = Convert.ToInt32(fila["municipio"].ToString());

                        this.codeudor.CedulaNombre = Convert.ToInt64(fila["identificacion"].ToString()) + " - " + fila["nombre"].ToString() + " " + fila["apellido"].ToString();

                        this.lstCodeudor.Add(codeudor);
                    }
                }
                return lstCodeudor;
            }
            catch (Exception ex)
            {
                throw (new Exception("BLCodeudor - ConsultarCodeudor :: " + ex.Message));
            }
        }

        /// <summary>
        /// registra o edita un nuevo codeudor
        /// </summary>
        /// <param name="parametros">parametrizacion ejecucion consulta a base de datos</param>
        /// <returns>respuesta de la ejecucion </returns>
        public ResultConsulta RegistrarEditarCodeudor(DLAccesEntities parametros)
        {
            try
            {
                return this.acceso.Registros(parametros);
            }
            catch (Exception ex)
            {
                throw (new Exception("BLCodeudor - RegistrarEditarCodeudor :: " + ex.Message));
            }
        }
    }
}
