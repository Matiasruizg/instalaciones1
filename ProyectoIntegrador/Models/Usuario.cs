using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoIntegrador.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? NombreUsuario { get; set; }

    public string? ApellidoUsuario { get; set; }

    public int? EdadUsuario { get; set; }

    public string? DireccionUsuario { get; set; }

    public string? CorreoUsuario { get; set; }

    public string? TelefonoUsuario { get; set; }

    public string? ContrasenaUsuario { get; set; }

    public virtual ICollection<UsuarioPerfil> UsuarioPerfils { get; set; } = new List<UsuarioPerfil>();

    [NotMapped]
    public bool MantenerActivo {  get; set; }
}
