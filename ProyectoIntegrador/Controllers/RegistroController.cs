using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using ProyectoIntegrador.Models;

namespace ProyectoIntegrador.Controllers
{
    public class RegistroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IConfiguration Configuration { get;}

        public  RegistroController (IConfiguration configuration) 
        { 
            Configuration = configuration;
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Usuario usuario)
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:conexion"]))
            {
                using (SqlCommand cmd = new ("InsertarUsuario", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Nombre_Usuario", System.Data.SqlDbType.NVarChar).Value = usuario.NombreUsuario;
                    cmd.Parameters.Add("@Apellido_Usuario", System.Data.SqlDbType.NVarChar).Value = usuario.ApellidoUsuario;
                    cmd.Parameters.Add("@Edad_Usuario", System.Data.SqlDbType.Int).Value = usuario.EdadUsuario;
                    cmd.Parameters.Add("@Direccion_Usuario", System.Data.SqlDbType.NVarChar).Value = usuario.DireccionUsuario;
                    cmd.Parameters.Add("@Correo_Usuario", System.Data.SqlDbType.NVarChar).Value = usuario.CorreoUsuario;
                    cmd.Parameters.Add("@Telefono_Usuario", System.Data.SqlDbType.NVarChar).Value = usuario.TelefonoUsuario;
                    cmd.Parameters.Add("@Contrasena_Usuario", System.Data.SqlDbType.NVarChar).Value = usuario.ContrasenaUsuario;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Redirect("Index");
        }
    }
}
