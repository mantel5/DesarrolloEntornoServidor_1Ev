using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using SuplementosAPI.Repositories;

namespace SuplementosAPI.Services
{
    public class BebidaService : IBebidaService
    {
        private readonly IBebidaRepository _repository;

        public BebidaService(IBebidaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Bebida> CreateAsync(BebidaCreateDto dto)
        {
            var nuevaBebida = new Bebida(
                // 1. Abuelo (ProductoBase) - ¡Orden Correcto!
                dto.Nombre, 
                dto.Precio, 
                dto.Stock,        // <--- ¡FALTABA ESTE!
                dto.Descripcion, 
                dto.Imagen,
                
                // 2. Padre (ComidaBase - Macros)
                dto.Calorias, 
                dto.Proteinas, 
                dto.Carbohidratos, 
                dto.Grasas,
                
                // 3. Hijo (Bebida)
                dto.Sabor, 
                dto.Mililitros, 
                dto.TieneGluten, 
                dto.TieneGas
            );
            
            // NOTA: Si al copiar te da error, REVISA EL ORDEN del constructor en Models/Bebida.cs
            // porque lo modificamos antes para que fuera coherente.

            await _repository.AddAsync(nuevaBebida);
            return nuevaBebida;
        }

        public async Task<List<Bebida>> GetAllAsync(QueryParamsBebida filtros)
        {
            return await _repository.GetAllAsync(filtros);
        }

        public async Task<Bebida?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var existe = await _repository.GetByIdAsync(id);
            if (existe == null) throw new KeyNotFoundException("ID no encontrado");
            await _repository.DeleteAsync(id);
        }
    }
}