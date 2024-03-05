using System;
using System.Collections.Generic;

namespace ProyectoIntegrador.Models;

public partial class VistaRecursosActivo
{
    public string? NombreRecurso { get; set; }

    public string? DescripcionRecurso { get; set; }

    public int? CantidadRecurso { get; set; }

    public decimal? CostoRecurso { get; set; }

    public bool? DisponibilidadRecurso { get; set; }
}
