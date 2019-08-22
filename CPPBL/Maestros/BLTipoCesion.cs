namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    public class BLTipoCesion
    {
        #region "Definicion de metodos privados"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesTipoCesion> lstTipoCesion = null;
        private EntitiesTipoCesion tipoCesion = null;

        #endregion

        /// <summary>
        /// consulta los tipos de cesion
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesTipoCesion> ConsultaTipoCesion(DLAccesEntities parametros)
        {
            this.lstTipoCesion = new List<EntitiesTipoCesion>();
            DataTable tipoCesionb = this.acceso.Consultas(parametros);

            if (tipoCesionb != null && tipoCesionb.Rows.Count > 0)
            {
                foreach (DataRow fila in tipoCesionb.Rows)
                {
                    this.tipoCesion = new EntitiesTipoCesion
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        descripcion = fila["descripcion"].ToString()
                    };

                    this.lstTipoCesion.Add(tipoCesion);
                }
            }
            return lstTipoCesion;
        }
    }
}
