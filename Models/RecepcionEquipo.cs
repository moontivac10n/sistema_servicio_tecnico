using System;
using System.Collections.Generic;

namespace sistema_servicio_tecnico.Models
{
    public partial class RecepcionEquipo
    {
        public int Id { get; set; }

        public string? Accesorio { get; set; }

        public string? CapacidadAlmacenamiento { get; set; }

        public int? CapacidadRam { get; set; }

        public int ClienteId { get; set; }

        public DateTime Fecha { get; set; }

        public string? Grafico { get; set; }

        public string? MarcaPc { get; set; }

        public string? ModeloPc { get; set; }

        public string? Nserie { get; set; }

        public int ServicioId { get; set; }

        public string? TipoAlmacenamiento { get; set; }

        public string? TipoGpu { get; set; }

        public string? TipoPc { get; set; }

        // Relaciones con Cliente y Servicio
        public virtual Cliente Cliente { get; set; } = null!;

        public virtual Servicio Servicio { get; set; } = null!;
    }
}
