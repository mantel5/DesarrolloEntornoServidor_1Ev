using SuplementosAPI.Dtos;
using SuplementosAPI.DTOs; // <--- Ojo: Asegúrate si es "Dtos" o "DTOs" en tu carpeta
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using SuplementosAPI.Repositories;

namespace SuplementosAPI.Services
{
    public class OpinionService : IOpinionService
    {
        private readonly IOpinionRepository _repository;

        public OpinionService(IOpinionRepository repository)
        {
            _repository = repository;
        }

        // CAMBIO 1: Renombrado a AddAsync para coincidir con el Controller
        public async Task AddAsync(OpinionCreateDto dto)
        {
            // 1. Validaciones de negocio
            if (dto.Puntuacion < 1 || dto.Puntuacion > 5)
                throw new ArgumentException("La puntuación debe estar entre 1 y 5");

            // 2. Convertimos el DTO (entrada) a Modelo (Base de datos)
            var opinion = new Opinion
            {
                UsuarioId = dto.UsuarioId,
                // Si tu DTO no tiene UsuarioNombre, quita esta línea o búscalo en BD
                UsuarioNombre = dto.UsuarioNombre, 
                ProductoId = dto.ProductoId,
                Texto = dto.Texto, // O dto.Mensaje, según como lo llamaste en el DTO
                Puntuacion = dto.Puntuacion,
                Fecha = DateTime.UtcNow // Mejor UtcNow para servidores
            };

            await _repository.AddAsync(opinion);
        }

        // CAMBIO 2: Devolvemos OpinionDto en vez de Opinion
        // (He puesto 'queryParams' como opcional por si el controller no lo envía aún)
        public async Task<IEnumerable<OpinionDto>> GetAllAsync(OpinionQueryParams? queryParams = null)
        {
            // Si el controller no manda filtros, creamos unos vacíos
            queryParams ??= new OpinionQueryParams(); 

            var listaDesdeBD = await _repository.GetAllAsync(queryParams);

            // 3. LA MAGIA: Traducimos de Modelo -> DTO
            return listaDesdeBD.Select(x => new OpinionDto
            {
                Id = x.Id,
                Mensaje = x.Texto, // O x.Mensaje, revisa tu modelo
                Puntuacion = x.Puntuacion,
                UsuarioId = x.UsuarioId,
                Fecha = x.Fecha
                // ProductoId = x.ProductoId // Descomenta si lo añadiste al OpinionDto
            });
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}