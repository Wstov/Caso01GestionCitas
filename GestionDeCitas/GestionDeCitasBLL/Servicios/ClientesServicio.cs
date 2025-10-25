using AutoMapper;
using GestionDeCitasBLL.Dtos;
using GestionDeCitasDAL.Entidades;
using GestionDeCitasDAL.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeCitasBLL.Servicios
{
    public class ClientesServicio : IClientesServicio
    {
        private readonly IClientesRepositorio _repo;
        private readonly IMapper _mapper;

        public ClientesServicio(IClientesRepositorio repo, IMapper mapper)
        {
            _repo = repo; _mapper = mapper;
        }

        public async Task<CustomResponse<List<ClienteDto>>> ObtenerAsync()
        {
            var list = await _repo.ObtenerAsync();
            return new() { Data = _mapper.Map<List<ClienteDto>>(list) };
        }

        public async Task<CustomResponse<ClienteDto>> AgregarAsync(ClienteDto dto)
        {
            var r = new CustomResponse<ClienteDto>();

            // Validaciones
            if (dto.Edad < 18)
                return new() { EsError = true, Mensaje = "No puede registrar clientes menores de edad" };

            var repetido = await _repo.ObtenerPorIdentificacionAsync(dto.Identificacion);
            if (repetido is not null)
                return new() { EsError = true, Mensaje = "Ya existe un cliente con la misma identificación" };

            var ok = await _repo.AgregarAsync(_mapper.Map<Cliente>(dto));
            if (!ok) return new() { EsError = true, Mensaje = "No se pudo registrar el cliente" };
            return r;
        }

        public async Task<CustomResponse<ClienteDto>> ActualizarAsync(ClienteDto dto)
        {
            // Validaciones (edad y colisión de identificación con otro cliente)
            if (dto.Edad < 18)
                return new() { EsError = true, Mensaje = "No puede registrar clientes menores de edad" };

            var existeIdent = await _repo.ObtenerPorIdentificacionAsync(dto.Identificacion);
            if (existeIdent is not null && existeIdent.Id != dto.Id)
                return new() { EsError = true, Mensaje = "La identificación pertenece a otro cliente" };

            var ok = await _repo.ActualizarAsync(_mapper.Map<Cliente>(dto));
            return ok ? new() : new() { EsError = true, Mensaje = "No se pudo actualizar el cliente" };
        }

        public async Task<CustomResponse<ClienteDto>> EliminarAsync(int id)
        {
            var ok = await _repo.EliminarAsync(id);
            return ok ? new() : new() { EsError = true, Mensaje = "No se pudo eliminar el cliente" };
        }
    }
}
