namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    public class BLTipoCuenta
    {
        #region "Definicion de metodos privados"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesTipoCuenta> lstTipoCuenta = null;
        private EntitiesTipoCuenta tipoCuenta = null;

        #endregion

        /// <summary>
        /// consulta los tipos de cesion
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesTipoCuenta> ConsultaTipoCuenta(DLAccesEntities parametros)
        {
            this.lstTipoCuenta = new List<EntitiesTipoCuenta>();
            DataTable tipoCuentab = this.acceso.Consultas(parametros);

            if (tipoCuentab != null && tipoCuentab.Rows.Count > 0)
            {
                foreach (DataRow fila in tipoCuentab.Rows)
                {
                    this.tipoCuenta = new EntitiesTipoCuenta
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        descripcion = fila["descripcion"].ToString(),
                        abonoCancelacion = fila["abonoCancelacion"].ToString()
                    };

                    this.lstTipoCuenta.Add(tipoCuenta);
                }
            }
            return lstTipoCuenta;
        }
    }
}
