using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.Repositories;

namespace SuplementosAPI.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        // Inyectamos TODOS los repositorios de productos
        private readonly ICreatinaRepository _creatinaRepo;
        private readonly IProteinaRepository _proteinaRepo;
        private readonly IOmega3Repository _omega3Repo;
        private readonly IPreEntrenoRepository _preEntrenoRepo;
        private readonly ISalsaRepository _salsaRepo;
        private readonly ITortitasRepository _tortitasRepo;
        private readonly IBebidaRepository _bebidaRepo;

        public PedidoService(
            IPedidoRepository pedidoRepository,
            ICreatinaRepository creatinaRepo,
            IProteinaRepository proteinaRepo,
            IOmega3Repository omega3Repo,
            IPreEntrenoRepository preEntrenoRepo,
            ISalsaRepository salsaRepo,
            ITortitasRepository tortitasRepo,
            IBebidaRepository bebidaRepo
            )
        {
            _pedidoRepository = pedidoRepository;
            _creatinaRepo = creatinaRepo;
            _proteinaRepo = proteinaRepo;
            _omega3Repo = omega3Repo;
            _preEntrenoRepo = preEntrenoRepo;
            _salsaRepo = salsaRepo;
            _tortitasRepo = tortitasRepo;
            _bebidaRepo = bebidaRepo;
        }

        // creamos un nuevo pedido a partir del DTO
        public async Task<Pedido> CreateAsync(PedidoCreateDto dto)
        {
            // preparamos el Pedido vacío
            var nuevoPedido = new Pedido
            {
                UsuarioId = dto.UsuarioId,
                Fecha = DateTime.Now,
                Estado = "Confirmado",
                Lineas = new List<LineaPedido>()
            };

            decimal totalAcumulado = 0;

            // recorremos el carrito del usuario línea por línea
            foreach (var item in dto.Productos)
            {
                string nombreProducto = "";
                decimal precioReal = 0;

                // buscamos cada producto en su repositorio correspondiente
                switch (item.TipoProducto.ToLower())
                {
                    case "creatina":
                        var c = await _creatinaRepo.GetByIdAsync(item.ProductoId);
                        if (c == null) throw new Exception($"Creatina ID {item.ProductoId} no encontrada");
                        if (c.Stock < item.Cantidad) throw new Exception($"Stock insuficiente para {c.Nombre}");
                        nombreProducto = c.Nombre;
                        precioReal = c.Precio;
                        break;

                    case "proteina":
                        var p = await _proteinaRepo.GetByIdAsync(item.ProductoId);
                        if (p == null) throw new Exception($"Proteína ID {item.ProductoId} no encontrada");
                        if (p.Stock < item.Cantidad) throw new Exception($"Stock insuficiente para {p.Nombre}");
                        nombreProducto = p.Nombre;
                        precioReal = p.Precio;
                        break;

                    case "omega3":
                        var o = await _omega3Repo.GetByIdAsync(item.ProductoId);
                        if (o == null) throw new Exception($"Omega3 ID {item.ProductoId} no encontrado");
                        if (o.Stock < item.Cantidad) throw new Exception($"Stock insuficiente para {o.Nombre}");
                        nombreProducto = o.Nombre;
                        precioReal = o.Precio;
                        break;

                    case "preentreno":
                        var pre = await _preEntrenoRepo.GetByIdAsync(item.ProductoId);
                        if (pre == null) throw new Exception($"PreEntreno ID {item.ProductoId} no encontrado");
                        if (pre.Stock < item.Cantidad) throw new Exception($"Stock insuficiente para {pre.Nombre}");
                        nombreProducto = pre.Nombre;
                        precioReal = pre.Precio;
                        break;

                    case "salsa":
                        var s = await _salsaRepo.GetByIdAsync(item.ProductoId);
                        if (s == null) throw new Exception($"Salsa ID {item.ProductoId} no encontrada");
                        if (s.Stock < item.Cantidad) throw new Exception($"Stock insuficiente para {s.Nombre}");
                        nombreProducto = s.Nombre;
                        precioReal = s.Precio;
                        break;

                    case "tortitas":
                        var t = await _tortitasRepo.GetByIdAsync(item.ProductoId);
                        if (t == null) throw new Exception($"Tortitas ID {item.ProductoId} no encontradas");
                        if (t.Stock < item.Cantidad) throw new Exception($"Stock insuficiente para {t.Nombre}");
                        nombreProducto = t.Nombre;
                        precioReal = t.Precio;
                        break;

                    case "bebida":
                        var b = await _bebidaRepo.GetByIdAsync(item.ProductoId);
                        if (b == null) throw new Exception($"Bebida ID {item.ProductoId} no encontrada");
                        if (b.Stock < item.Cantidad) throw new Exception($"Stock insuficiente para {b.Nombre}");
                        nombreProducto = b.Nombre;
                        precioReal = b.Precio;
                        break;

                    default:
                        throw new ArgumentException($"Tipo de producto desconocido: {item.TipoProducto}. Tipos válidos: creatina, proteina, omega3, preentreno, salsa, tortitas, bebida.");
                }

                // Creamos la líneapedido
                var linea = new LineaPedido
                {
                    ProductoIdOriginal = item.ProductoId,
                    TipoProductoOriginal = item.TipoProducto,
                    ProductoNombre = nombreProducto,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = precioReal,
                    Subtotal = precioReal * item.Cantidad
                };

                nuevoPedido.Lineas.Add(linea);
                totalAcumulado += linea.Subtotal;
            }

            // guardamos
            nuevoPedido.Total = totalAcumulado;
            await _pedidoRepository.AddAsync(nuevoPedido);

            return nuevoPedido;
        }

        // GetById de un pedido
        public async Task<Pedido?> GetByIdAsync(int id)
        {
            return await _pedidoRepository.GetByIdAsync(id);
        }

        // GetByUsuarioId
        public async Task<List<Pedido>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _pedidoRepository.GetByUsuarioIdAsync(usuarioId);
        }
    }
}