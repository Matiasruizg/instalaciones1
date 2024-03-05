using System;
using System.Collections.Generic;

namespace ProyectoIntegrador.Models;

public partial class InstalacionesDeportiva
{
    public int IdInstalacionDeportiva { get; set; }

    public string? NombreInstalacion { get; set; }

    public string? TipoCanchaInstalacion { get; set; }

    public string? UbicacionInstalacion { get; set; }

    public bool? DisponibilidadInstalacion { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
