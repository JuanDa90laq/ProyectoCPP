namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLTipoProductor
    {

        #region "Definicion de metodos privados"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesTipoProductor> lstTipoProducto = null;
        private EntitiesTipoProductor tipoProducto = null;

        #endregion


        /// <summary>
        /// consulta los tipos de productores
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesTipoProductor> ConsultaProductores(DLAccesEntities parametros)
        {
            this.lstTipoProducto = new List<EntitiesTipoProductor>();            
            DataTable beneficiariosb = this.acceso.Consultas(parametros);

            if (beneficiariosb != null && beneficiariosb.Rows.Count > 0)
            {
                foreach (DataRow fila in beneficiariosb.Rows)
                {
                    this.tipoProducto = new EntitiesTipoProductor();
                    this.tipoProducto.id = Convert.ToInt32(fila["id"].ToString());
                    this.tipoProducto.productor = fila["productor"].ToString();
                    this.tipoProducto.estadoProd = Convert.ToBoolean(fila["estadoProd"].ToString());
                    
                    this.lstTipoProducto.Add(tipoProducto);
                }
            }
            return lstTipoProducto;
        }

    }
}
