using System;
using System.Collections.Generic;

namespace ProyectoIntegrador.Models;

public partial class TipoPago
{
    public int IdTipoPago { get; set; }

    public string? TipoPago1 { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
