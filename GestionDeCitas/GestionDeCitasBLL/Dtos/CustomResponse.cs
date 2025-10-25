using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeCitasBLL.Dtos
{
    public class CustomResponse<T>
    {
        public bool EsError { get; set; } = false;
        public string Mensaje { get; set; } = "";
        public T? Data { get; set; }
    }
}
