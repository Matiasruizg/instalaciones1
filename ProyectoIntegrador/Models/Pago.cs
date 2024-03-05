using System;
using System.Collections.Generic;

namespace ProyectoIntegrador.Models;

public partial class Pago
{
    public int IdPago { get; set; }

    public decimal? MontoPago { get; set; }

    public DateTime? FechaPago { get; set; }

    public TimeSpan? HoraPago { get; set; }

    public decimal? TotalPago { get; set; }

    public int? IdReserva { get; set; }

    public int? IdTipoPago { get; set; }

    public virtual Reserva? IdReservaNavigation { get; set; }

    public virtual TipoPago? IdTipoPagoNavigation { get; set; }
}
