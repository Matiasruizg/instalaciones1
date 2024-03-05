using System;
using System.Collections.Generic;

namespace ProyectoIntegrador.Models;

public partial class Recurso
{
    public int IdRecurso { get; set; }

    public string? NombreRecurso { get; set; }

    public string? DescripcionRecurso { get; set; }

    public int? CantidadRecurso { get; set; }

    public decimal? CostoRecurso { get; set; }

    public bool? DisponibilidadRecurso { get; set; }

    public virtual ICollection<Reserva> IdReservas { get; set; } = new List<Reserva>();
}
