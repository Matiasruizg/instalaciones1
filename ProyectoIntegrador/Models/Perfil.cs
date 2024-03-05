using System;
using System.Collections.Generic;

namespace ProyectoIntegrador.Models;

public partial class Perfil
{
    public int IdPerfil { get; set; }

    public string? TipoPerfil { get; set; }

    public virtual ICollection<UsuarioPerfil> UsuarioPerfils { get; set; } = new List<UsuarioPerfil>();
}
