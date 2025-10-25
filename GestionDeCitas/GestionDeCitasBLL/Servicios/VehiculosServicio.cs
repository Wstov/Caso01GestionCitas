using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeCitasBLL.Servicios
{
    // VehiculosServicio.cs
    using AutoMapper;
    using GestionDeCitasDAL.Entidades;
    using GestionDeCitasDAL.Repositorios;
    using global::GestionDeCitasBLL.Dtos;

    namespace GestionDeCitasBLL.Servicios
    {
        public class VehiculosServicio : IVehiculosServicio
        {
            private readonly IVehiculosRepositorio _repoVeh;
            private readonly IClientesRepositorio _repoCli;
            private readonly IMapper _mapper;

            public VehiculosServicio(IVehiculosRepositorio rv, IClientesRepositorio rc, IMapper m)
            { _repoVeh = rv; _repoCli = rc; _mapper = m; }

            public async Task<CustomResponse<List<VehiculoDto>>> ObtenerAsync()
            {
                var list = await _repoVeh.ObtenerAsync();
                return new() { Data = _mapper.Map<List<VehiculoDto>>(list) };
            }

            public async Task<CustomResponse<VehiculoDto>> AgregarAsync(VehiculoDto dto)
            {
                // Validar que el cliente exista
                var cliente = await _repoCli.ObtenerPorIdAsync(dto.ClienteId);
                if (cliente is null)
                    return new() { EsError = true, Mensaje = "Cliente no existe" };

                // Validar placa única
                var placa = await _repoVeh.ObtenerPorPlacaAsync(dto.Placa);
                if (placa is not null)
                {
                    // Si la placa ya existe y pertenece a otro cliente, violación
                    if (placa.ClienteId != dto.ClienteId)
                        return new() { EsError = true, Mensaje = "Un vehículo no puede tener 2 clientes (placa ya asignada)" };
                    else
                        return new() { EsError = true, Mensaje = "Ya existe un vehículo con la misma placa" };
                }

                var ok = await _repoVeh.AgregarAsync(_mapper.Map<Vehiculo>(dto));
                return ok ? new() : new() { EsError = true, Mensaje = "No se pudo registrar el vehículo" };
            }

            public async Task<CustomResponse<VehiculoDto>> ActualizarAsync(VehiculoDto dto)
            {
                var cliente = await _repoCli.ObtenerPorIdAsync(dto.ClienteId);
                if (cliente is null)
                    return new() { EsError = true, Mensaje = "Cliente no existe" };

                var placa = await _repoVeh.ObtenerPorPlacaAsync(dto.Placa);
                if (placa is not null && placa.Id != dto.Id && placa.ClienteId != dto.ClienteId)
                    return new() { EsError = true, Mensaje = "Un vehículo no puede tener 2 clientes (placa ya asignada)" };

                var ok = await _repoVeh.ActualizarAsync(_mapper.Map<Vehiculo>(dto));
                return ok ? new() : new() { EsError = true, Mensaje = "No se pudo actualizar el vehículo" };
            }

            public async Task<CustomResponse<VehiculoDto>> EliminarAsync(int id)
            {
                var ok = await _repoVeh.EliminarAsync(id);
                return ok ? new() : new() { EsError = true, Mensaje = "No se pudo eliminar el vehículo" };
            }
        }
    }

}
