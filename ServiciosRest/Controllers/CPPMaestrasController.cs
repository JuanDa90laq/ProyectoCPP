using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CPPBL;
using CPPEN;
namespace ServiciosRest.Controllers
{
    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("apiCPP/Maestras")]
    public class CPPMaestrasController : ApiController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Activacion"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ConsultarUsuario")]
        public HttpResponseMessage Usuarios(DLAccesEntities parametros)
        {

            EntitiesUsuarios objusuario = new EntitiesUsuarios();
            
            var dtResultado = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["SSO_DB"].ToString();
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SSO_SP_U_ACTIVACION_USUARIO", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usrc_vEmail", Activacion.Email);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dtResultado.Load(dr);
                        cRegistro.respuesta = Convert.ToInt32(dtResultado.Rows[0]["RESULTADO"]);
                    }
                }
            }

            if (cRegistro.respuesta == 1)
                return Request.CreateResponse(HttpStatusCode.OK, cRegistro);
            else
                return Request.CreateResponse(HttpStatusCode.Unauthorized, cRegistro);
        }

    }
}
