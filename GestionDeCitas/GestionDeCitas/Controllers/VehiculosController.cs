using GestionDeCitasBLL.Dtos;
using GestionDeCitasBLL.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestionDeCitas.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly IVehiculosServicio _vehiculosSvc;
        private readonly IClientesServicio _clientesSvc;

        public VehiculosController(IVehiculosServicio vehiculosSvc, IClientesServicio clientesSvc)
        {
            _vehiculosSvc = vehiculosSvc;
            _clientesSvc = clientesSvc;
        }

        public async Task<IActionResult> Index()
        {
            var r = await _vehiculosSvc.ObtenerAsync();
            return View(r.Data ?? new List<VehiculoDto>());
        }

        [HttpGet]
        public IActionResult Create() => PartialView("_Form", new VehiculoDto());

        [HttpPost]
        public async Task<IActionResult> Create(VehiculoDto dto)
        {
            var r = await _vehiculosSvc.AgregarAsync(dto);
            if (r.EsError) return BadRequest(r.Mensaje);
            return Ok("Vehículo registrado correctamente");
        }

        [HttpGet]
        public IActionResult Edit(int id, string placa, string marca, int anio, int clienteId)
            => PartialView("_Form", new VehiculoDto { Id = id, Placa = placa, Marca = marca, Anio = anio, ClienteId = clienteId });

        [HttpPost]
        public async Task<IActionResult> Edit(VehiculoDto dto)
        {
            var r = await _vehiculosSvc.ActualizarAsync(dto);
            if (r.EsError) return BadRequest(r.Mensaje);
            return Ok("Vehículo actualizado correctamente");
        }

        [HttpGet]
        public IActionResult Delete(int id) => PartialView("_Delete", id);

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var r = await _vehiculosSvc.EliminarAsync(id);
            if (r.EsError) return BadRequest(r.Mensaje);
            return Ok("Vehículo eliminado");
        }

        // === Helpers para llenar selects ===
        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var res = await _clientesSvc.ObtenerAsync();
            var list = (res.Data ?? new List<ClienteDto>())
                .Select(c => new { id = c.Id, text = $"{c.Identificacion} - {c.Nombre}" });
            return Json(list);
        }
    }
}
