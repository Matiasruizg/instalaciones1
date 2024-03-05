using System;
using System.Collections.Generic;

namespace ProyectoIntegrador.Models;

public partial class VistaReserva
{
    public string? NombreCliente { get; set; }

    public string? CorreoUsuario { get; set; }

    public string? TelefonoUsuario { get; set; }

    public string? DireccionUsuario { get; set; }

    public int? EdadUsuario { get; set; }

    public DateTime? FechaReserva { get; set; }

    public TimeSpan? HoraReserva { get; set; }

    public string? NombreInstalacion { get; set; }

    public string? TipoUsuario { get; set; }

    public string? RecursosReserva { get; set; }
}
