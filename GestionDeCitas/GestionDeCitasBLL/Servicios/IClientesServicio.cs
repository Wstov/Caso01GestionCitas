using GestionDeCitasBLL.Dtos;


namespace GestionDeCitasBLL.Servicios
{
    public interface IClientesServicio
    {
        Task<CustomResponse<List<ClienteDto>>> ObtenerAsync();
        Task<CustomResponse<ClienteDto>> AgregarAsync(ClienteDto dto);
        Task<CustomResponse<ClienteDto>> ActualizarAsync(ClienteDto dto);
        Task<CustomResponse<ClienteDto>> EliminarAsync(int id);
    }
}
