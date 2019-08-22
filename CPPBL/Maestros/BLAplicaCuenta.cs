namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    public class BLAplicaCuenta
    {
        #region "Definicion de metodos privados"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesAplicaCuenta> lstAplicaCuenta = null;
        private EntitiesAplicaCuenta aplicaCuenta = null;

        #endregion

        /// <summary>
        /// consulta los tipos de aplica cuenta
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesAplicaCuenta> ConsultaAplicaCuenta(DLAccesEntities parametros)
        {
            this.lstAplicaCuenta = new List<EntitiesAplicaCuenta>();
            DataTable aplicaCuentab = this.acceso.Consultas(parametros);

            if (aplicaCuentab != null && aplicaCuentab.Rows.Count > 0)
            {
                foreach (DataRow fila in aplicaCuentab.Rows)
                {
                    this.aplicaCuenta = new EntitiesAplicaCuenta
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        descripcion = fila["descripcion"].ToString()
                    };

                    this.lstAplicaCuenta.Add(aplicaCuenta);
                }
            }
            return lstAplicaCuenta;
        }
    }
}
