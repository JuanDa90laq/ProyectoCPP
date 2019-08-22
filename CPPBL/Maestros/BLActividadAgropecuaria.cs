namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLActividadAgropecuaria
    {
        #region "Definicion de metodos privados"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesActividadesAgropecuarias> lstActividadesAgropecuarias = null;
        private EntitiesActividadesAgropecuarias actividadAgropecuaria = null;

        #endregion

        /// <summary>
        /// consulta las Actividades agropecuarias
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesActividadesAgropecuarias> ConsultarActividadAgropecuaria(DLAccesEntities parametros)
        {
            this.lstActividadesAgropecuarias = new List<EntitiesActividadesAgropecuarias>();
            DataTable activiadadesTb = this.acceso.Consultas(parametros);

            if (activiadadesTb != null && activiadadesTb.Rows.Count > 0)
            {
                foreach (DataRow fila in activiadadesTb.Rows)
                {
                    this.actividadAgropecuaria = new EntitiesActividadesAgropecuarias();
                    actividadAgropecuaria.actividad = fila["ACTIVIDAD"].ToString();
                    actividadAgropecuaria.id = Convert.ToInt32(fila["iD"].ToString());
                    actividadAgropecuaria.estado = Convert.ToBoolean(fila["ESTADO"].ToString());
                    this.lstActividadesAgropecuarias.Add(actividadAgropecuaria);
                }
            }
            return this.lstActividadesAgropecuarias;
        }

        /// <summary>
        /// registra o edita una nueva actividad
        /// </summary>
        /// <param name="parametros">parametrizacion ejecucion consulta a base de datos</param>
        /// <returns>respuesta de la ejecucion </returns>
        public ResultConsulta RegistrarEditarActividad(DLAccesEntities parametros)
        {
            return this.acceso.Registros(parametros);
        }
    }
}
