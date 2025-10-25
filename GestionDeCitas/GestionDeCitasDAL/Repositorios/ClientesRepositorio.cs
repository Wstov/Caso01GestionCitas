using GestionDeCitasDAL.Entidades;


namespace GestionDeCitasDAL.Repositorios
{
    public class ClientesRepositorio : IClientesRepositorio
    {
        private readonly List<Cliente> _clientes = new()
        {
            new Cliente { Id = 1, Identificacion = "10101010", Nombre = "Ana",  Edad = 30 },
            new Cliente { Id = 2, Identificacion = "20202020", Nombre = "Luis", Edad = 26 }
        };

        public Task<List<Cliente>> ObtenerAsync() => Task.FromResult(_clientes);

        public Task<Cliente?> ObtenerPorIdAsync(int id) =>
            Task.FromResult(_clientes.FirstOrDefault(x => x.Id == id));

        public Task<Cliente?> ObtenerPorIdentificacionAsync(string identificacion) =>
            Task.FromResult(_clientes.FirstOrDefault(x => x.Identificacion == identificacion));

        public Task<bool> AgregarAsync(Cliente cliente)
        {
            cliente.Id = _clientes.Any() ? _clientes.Max(c => c.Id) + 1 : 1;
            _clientes.Add(cliente);
            return Task.FromResult(true);
        }

        public Task<bool> ActualizarAsync(Cliente cliente)
        {
            var db = _clientes.FirstOrDefault(c => c.Id == cliente.Id);
            if (db is null) return Task.FromResult(false);
            db.Nombre = cliente.Nombre;
            db.Edad = cliente.Edad;
            db.Identificacion = cliente.Identificacion;
            return Task.FromResult(true);
        }

        public Task<bool> EliminarAsync(int id)
        {
            var db = _clientes.FirstOrDefault(c => c.Id == id);
            if (db is null) return Task.FromResult(false);
            _clientes.Remove(db);
            return Task.FromResult(true);
        }
    }
}
