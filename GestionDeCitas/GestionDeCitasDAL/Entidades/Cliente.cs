using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeCitasDAL.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Identificacion { get; set; } = "";
        public string Nombre { get; set; } = "";
        public int Edad { get; set; }
        public List<Vehiculo> Vehiculos { get; set; } = new();
    }
}
