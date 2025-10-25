using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeCitasBLL.Dtos
{
    public class VehiculoDto
    {
        public int Id { get; set; }
        public string Placa { get; set; } = "";
        public string Marca { get; set; } = "";
        public string Modelo { get; set; } = "";
        public int Anio { get; set; }
        public int ClienteId { get; set; }
    }
}
