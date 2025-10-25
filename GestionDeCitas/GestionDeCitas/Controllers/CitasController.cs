using GestionDeCitasBLL.Dtos;
using GestionDeCitasBLL.Servicios;
using GestionDeCitasDAL.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace GestionDeCitas.Controllers
{
    public class CitasController : Controller
    {
        private readonly ICitasServicio _citasSvc;
        private readonly IClientesServicio _clientesSvc;
        private readonly IVehiculosServicio _vehiculosSvc;

        public CitasController(ICitasServicio citasSvc, IClientesServicio clientesSvc, IVehiculosServicio vehiculosSvc)
        {
            _citasSvc = citasSvc;
            _clientesSvc = clientesSvc;
            _vehiculosSvc = vehiculosSvc;
        }

        public async Task<IActionResult> Index()
        {
            var r = await _citasSvc.ObtenerAsync();
            return View(r.Data ?? new List<CitaDto>());
        }

        [HttpGet]
        public IActionResult Create() => PartialView("_Form", new CitaDto { Fecha = DateTime.Today });

        [HttpPost]
        public async Task<IActionResult> Create(CitaDto dto)
        {
            var r = await _citasSvc.AgregarAsync(dto);
            if (r.EsError) return BadRequest(r.Mensaje);
            return Ok("Cita registrada correctamente");
        }

        [HttpGet]
        public IActionResult CambiarEstado(int id, int estadoActual)
            => PartialView("_CambiarEstado", new CitaDto { Id = id, Estado = (EstadoCita)estadoActual });

        [HttpPost]
        public async Task<IActionResult> CambiarEstadoConfirm(int id, int nuevoEstado)
        {
            var r = await _citasSvc.CambiarEstadoAsync(id, nuevoEstado);
            if (r.EsError) return BadRequest(r.Mensaje);
            return Ok("Estado actualizado");
        }

        [HttpGet]
        public IActionResult Delete(int id) => PartialView("_Delete", id);

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var r = await _citasSvc.EliminarAsync(id);
            if (r.EsError) return BadRequest(r.Mensaje);
            return Ok("Cita eliminada");
        }

        // === Helpers para selects dependientes ===
        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var r = await _clientesSvc.ObtenerAsync();
            var items = (r.Data ?? new List<ClienteDto>()).Select(c => new { c.Id, Texto = $"{c.Identificacion} - {c.Nombre}" });
            return Json(items);
        }

        [HttpGet]
        public async Task<IActionResult> GetVehiculosPorCliente(int clienteId)
        {
            var r = await _vehiculosSvc.ObtenerAsync();
            var items = (r.Data ?? new List<VehiculoDto>())
                .Where(v => v.ClienteId == clienteId)
                .Select(v => new { v.Id, Texto = $"{v.Placa} - {v.Marca} ({v.Anio})" });
            return Json(items);
        }
    }
}
