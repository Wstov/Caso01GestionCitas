using AutoMapper;
using GestionDeCitasBLL.Dtos;
using GestionDeCitasDAL.Entidades;
using GestionDeCitasDAL.Repositorios;

namespace GestionDeCitasBLL.Servicios
{
    public class CitasServicio : ICitasServicio
    {
        private readonly ICitasRepositorio _repoCitas;
        private readonly IClientesRepositorio _repoCli;
        private readonly IVehiculosRepositorio _repoVeh;
        private readonly IMapper _mapper;

        public CitasServicio(ICitasRepositorio rc, IClientesRepositorio rcli, IVehiculosRepositorio rv, IMapper m)
        { _repoCitas = rc; _repoCli = rcli; _repoVeh = rv; _mapper = m; }

        public async Task<CustomResponse<List<CitaDto>>> ObtenerAsync()
        {
            var list = await _repoCitas.ObtenerAsync();
            return new() { Data = _mapper.Map<List<CitaDto>>(list) };
        }

        public async Task<CustomResponse<CitaDto>> AgregarAsync(CitaDto dto)
        {
            // Cliente y vehículo deben existir y el vehículo debe pertenecer al cliente
            var cliente = await _repoCli.ObtenerPorIdAsync(dto.ClienteId);
            if (cliente is null)
                return new() { EsError = true, Mensaje = "Debe seleccionar un cliente válido" };

            var veh = await _repoVeh.ObtenerPorIdAsync(dto.VehiculoId);
            if (veh is null)
                return new() { EsError = true, Mensaje = "Debe seleccionar un vehículo válido" };

            if (veh.ClienteId != dto.ClienteId)
                return new() { EsError = true, Mensaje = "El vehículo seleccionado no pertenece al cliente indicado" };

            if (dto.Fecha < DateTime.Today)
                return new() { EsError = true, Mensaje = "La fecha de la cita no puede ser en el pasado" };

            var ok = await _repoCitas.AgregarAsync(_mapper.Map<Cita>(dto));
            return ok ? new() : new() { EsError = true, Mensaje = "No se pudo registrar la cita" };
        }

        public async Task<CustomResponse<CitaDto>> ActualizarAsync(CitaDto dto)
        {
            // Reutiliza las mismas validaciones que en agregar
            return await AgregarOActualizar(dto, actualizar: true);
        }

        private async Task<CustomResponse<CitaDto>> AgregarOActualizar(CitaDto dto, bool actualizar)
        {
            var cliente = await _repoCli.ObtenerPorIdAsync(dto.ClienteId);
            var veh = await _repoVeh.ObtenerPorIdAsync(dto.VehiculoId);
            if (cliente is null) return new() { EsError = true, Mensaje = "Cliente inválido" };
            if (veh is null) return new() { EsError = true, Mensaje = "Vehículo inválido" };
            if (veh.ClienteId != dto.ClienteId)
                return new() { EsError = true, Mensaje = "El vehículo no pertenece al cliente" };
            if (dto.Fecha < DateTime.Today)
                return new() { EsError = true, Mensaje = "La fecha no puede ser en el pasado" };

            var cita = _mapper.Map<Cita>(dto);
            var ok = actualizar ? await _repoCitas.ActualizarAsync(cita)
                                : await _repoCitas.AgregarAsync(cita);
            return ok ? new() : new() { EsError = true, Mensaje = "No se pudo registrar/actualizar la cita" };
        }

        public async Task<CustomResponse<CitaDto>> CambiarEstadoAsync(int id, int nuevoEstado)
        {
            var cita = await _repoCitas.ObtenerPorIdAsync(id);
            if (cita is null) return new() { EsError = true, Mensaje = "Cita no existe" };

            cita.Estado = (EstadoCita)nuevoEstado;
            var ok = await _repoCitas.ActualizarAsync(cita);
            return ok ? new() : new() { EsError = true, Mensaje = "No se pudo cambiar el estado" };
        }

        public async Task<CustomResponse<CitaDto>> EliminarAsync(int id)
        {
            var ok = await _repoCitas.EliminarAsync(id);
            return ok ? new() : new() { EsError = true, Mensaje = "No se pudo eliminar la cita" };
        }
    }
}
