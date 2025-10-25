using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeCitasDAL.Entidades
{
    public enum EstadoCita { Ingresada = 1, Cancelada = 2, Concluida = 3 }

    public class Cita
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int VehiculoId { get; set; }
        public DateTime Fecha { get; set; }
        public EstadoCita Estado { get; set; } = EstadoCita.Ingresada;
    }
}
