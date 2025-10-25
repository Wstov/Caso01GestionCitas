using GestionDeCitasDAL.Entidades;


namespace GestionDeCitasBLL.Dtos
{
    public class CitaDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int VehiculoId { get; set; }
        public DateTime Fecha { get; set; }
        public EstadoCita Estado { get; set; } = EstadoCita.Ingresada;
    }
}
