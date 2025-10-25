using GestionDeCitasBLL.Dtos;


namespace GestionDeCitasBLL.Servicios
{
    public interface ICitasServicio
    {
        Task<CustomResponse<List<CitaDto>>> ObtenerAsync();
        Task<CustomResponse<CitaDto>> AgregarAsync(CitaDto dto);
        Task<CustomResponse<CitaDto>> ActualizarAsync(CitaDto dto);
        Task<CustomResponse<CitaDto>> CambiarEstadoAsync(int id, int nuevoEstado);
        Task<CustomResponse<CitaDto>> EliminarAsync(int id);
    }
}
