namespace CPPBL.Maestros
{
    using CPPDL;
    using CPPENL.Maestros;
    using CPPENL.Transversal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class BLDatosbeneficiario
    {
        #region "Definicion de metodos privados"

        private readonly DLDataAcces acceso = new DLDataAcces();
        private List<EntitiesBeneficiarios> lstBeneficiarios = null;
        private EntitiesBeneficiarios beneficiarios = null;

        #endregion


        /// <summary>
        /// consulta los beneficiarios
        /// </summary>
        /// <param name="parametros">objeto con la parametrizacion apra ejecutar la sentencia de busqueda</param>
        /// <returns>objeto con la informacion resultado de la consulta</returns>
        public List<EntitiesBeneficiarios> Consultarbeneficiarios(DLAccesEntities parametros)
        {
            try
            {
                this.lstBeneficiarios = new List<EntitiesBeneficiarios>();                
                DataTable beneficiariosb = this.acceso.Consultas(parametros);

                if (beneficiariosb != null && beneficiariosb.Rows.Count > 0)
                {
                    foreach (DataRow fila in beneficiariosb.Rows)
                    {
                        this.beneficiarios = new EntitiesBeneficiarios();
                        this.beneficiarios.identificador = Convert.ToInt32(fila["identificador"].ToString());
                        this.beneficiarios.tipo_Documento = Convert.ToInt32(fila["tipo de documento"].ToString());
                        this.beneficiarios.identificacion = Convert.ToInt64(fila["identificacion"].ToString());

                        if (string.IsNullOrEmpty(fila["fecha de expedicion"].ToString()))
                            this.beneficiarios.fecha_Expedicion = null;
                        else
                            this.beneficiarios.fecha_Expedicion = Convert.ToDateTime(fila["fecha de expedicion"].ToString());

                        this.beneficiarios.nombre = fila["nombre"].ToString();
                        this.beneficiarios.apellido = fila["apellido"].ToString();
                        this.beneficiarios.nombreCompleto = fila["nombre"].ToString() + " " + fila["apellido"].ToString();
                        this.beneficiarios.direccion = fila["direccion"].ToString();

                        if (string.IsNullOrEmpty(fila["telefono"].ToString()))
                            this.beneficiarios.telefono = null;
                        else
                            this.beneficiarios.telefono = Convert.ToInt64(fila["telefono"].ToString());

                        if (string.IsNullOrEmpty(fila["celular"].ToString()))
                            this.beneficiarios.celular = null;
                        else
                            this.beneficiarios.celular = Convert.ToInt64(fila["celular"].ToString());

                        this.beneficiarios.correo = fila["correo"].ToString();

                        if (string.IsNullOrEmpty(fila["montos activos"].ToString()))
                            this.beneficiarios.montos_Activos = null;
                        else
                            this.beneficiarios.montos_Activos = Convert.ToDecimal(fila["montos activos"].ToString());

                        if (string.IsNullOrEmpty(fila["fecha corte activos"].ToString()))
                            this.beneficiarios.fecha_Corte_Activos = null;
                        else
                            this.beneficiarios.fecha_Corte_Activos = Convert.ToDateTime(fila["fecha corte activos"].ToString());

                        this.beneficiarios.tipo_Productor = Convert.ToInt32(fila["tipo productor"].ToString());
                        this.beneficiarios.productor = fila["productor"].ToString();
                        this.beneficiarios.actividad = fila["actividad agropecuaria"].ToString();
                        this.beneficiarios.idDepartamento = Convert.ToInt32(fila["departamento"].ToString());
                        this.beneficiarios.idMunicipio = Convert.ToInt32(fila["municipio"].ToString());

                        this.lstBeneficiarios.Add(beneficiarios);
                    }
                }
                return lstBeneficiarios;
            }
            catch (Exception ex)
            {
                throw (new Exception("BLDatosbeneficiario - Consultarbeneficiarios :: " + ex.Message));
            }
        }

        /// <summary>
        /// registra o edita un nuevo beneficiario
        /// </summary>
        /// <param name="parametros">parametrizacion ejecucion consulta a base de datos</param>
        /// <returns>respuesta de la ejecucion </returns>
        public ResultConsulta RegistrarEditarBeneficiario(DLAccesEntities parametros)
        {
            try
            {
                return this.acceso.Registros(parametros);
            }
            catch (Exception ex)
            {
                throw (new Exception("BLDatosbeneficiario - RegistrarEditarBeneficiario :: " + ex.Message));
            }
        }
    }
}
