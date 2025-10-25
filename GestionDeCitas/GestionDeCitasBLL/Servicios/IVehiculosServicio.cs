using GestionDeCitasBLL.Dtos;


namespace GestionDeCitasBLL.Servicios
{
    public interface IVehiculosServicio
    {
        Task<CustomResponse<List<VehiculoDto>>> ObtenerAsync();
        Task<CustomResponse<VehiculoDto>> AgregarAsync(VehiculoDto dto);
        Task<CustomResponse<VehiculoDto>> ActualizarAsync(VehiculoDto dto);
        Task<CustomResponse<VehiculoDto>> EliminarAsync(int id);
    }
}
