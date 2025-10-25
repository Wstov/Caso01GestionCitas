using GestionDeCitasBLL.Dtos;
using AutoMapper;
using GestionDeCitasDAL.Entidades;

namespace GestionDeCitasBLL.Mapeos
{
    public class MapeoClases : Profile
    {
        public MapeoClases()
        {
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Vehiculo, VehiculoDto>().ReverseMap();
            CreateMap<Cita, CitaDto>().ReverseMap();
        }
    }
}
