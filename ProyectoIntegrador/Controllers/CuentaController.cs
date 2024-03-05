using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProyectoIntegrador.Data;
using ProyectoIntegrador.Models;
using System.Security.Claims;

namespace ProyectoIntegrador.Controllers
{
    public class CuentaController : Controller
    {

        private readonly Contexto _contexto;

        public CuentaController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Login()
        {
            ClaimsPrincipal c = HttpContext.User;
            if (c.Identity != null)
            {
                if (c.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario u)
        {
            try
            {
                using (SqlConnection con = new(_contexto.Conexion))
                {
                    using (SqlCommand cmd = new("ValidarUsuario", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Correo_Usuario", System.Data.SqlDbType.NVarChar).Value = u.CorreoUsuario;
                        cmd.Parameters.Add("@Contrasena_Usuario", System.Data.SqlDbType.NVarChar).Value = u.ContrasenaUsuario;
                        con.Open();
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            if (dr["CorreoUsuario"] != null && u.CorreoUsuario != null)
                            {
                                List<Claim> c = new List<Claim>()
                                {
                                    new Claim(ClaimTypes.NameIdentifier, u.CorreoUsuario)
                                };
                                ClaimsIdentity ci = new(c, CookieAuthenticationDefaults.AuthenticationScheme);
                                AuthenticationProperties p = new();

                                p.AllowRefresh = true;
                                p.IsPersistent = u.MantenerActivo;

                                if (!u.MantenerActivo)
                                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(1);
                                else
                                    p.ExpiresUtc = DateTimeOffset.MaxValue;

                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ci), p);

                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ViewBag.Error = "Crendenciales icnorreta o cuenta no registrada.";
                            }
                        }
                        con.Close();
                    }
                    return View();
                }
            }
            catch (SystemException e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}
