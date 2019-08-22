namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLCalificacion
    {
        #region "Definicion de metodos privados"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesCalificacion> lstCalificacion = null;
        private EntitiesCalificacion calificacion = null;

        #endregion

        /// <summary>
        /// consulta las calificaciones del sistema
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesCalificacion> ConsultaCalificacion(DLAccesEntities parametros)
        {
            this.lstCalificacion = new List<EntitiesCalificacion>();
            DataTable calificacionb = this.acceso.Consultas(parametros);

            if (calificacionb != null && calificacionb.Rows.Count > 0)
            {
                foreach (DataRow fila in calificacionb.Rows)
                {
                    this.calificacion = new EntitiesCalificacion
                    {
                        id = Convert.ToInt32(fila["id"].ToString()),
                        descripcion = fila["descripcion"].ToString()
                    };

                    this.lstCalificacion.Add(calificacion);
                }
            }
            return lstCalificacion;
        }
    }
}
