using System;
using System.Collections.Generic;

namespace ProyectoIntegrador.Models;

public partial class UsuarioPerfil
{
    public int IdUperfil { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdPerfil { get; set; }

    public virtual Perfil? IdPerfilNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
