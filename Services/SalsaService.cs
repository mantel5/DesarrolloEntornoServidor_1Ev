using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using SuplementosAPI.Repositories;

namespace SuplementosAPI.Services
{
    public class SalsaService : ISalsaService
    {
        private readonly ISalsaRepository _repository;

        public SalsaService(ISalsaRepository repository)
        {
            _repository = repository;
        }

        // Crear
        public async Task<Salsa> CreateAsync(SalsaCreateDto dto)
        {
            
            var nuevaSalsa = new Salsa(
                // Abuelo (ProductoBase)
                dto.Nombre, 
                dto.Precio, 
                dto.Stock, 
                dto.Descripcion, 
                dto.Imagen,
                
                // Padre (ComidaBase - Macros)
                dto.Calorias, 
                dto.Proteinas, 
                dto.Carbohidratos, 
                dto.Grasas,
                
                // Hijo (Salsa)
                dto.Sabor, 
                dto.EsPicante, 
                dto.EsZero
            );

            await _repository.AddAsync(nuevaSalsa);
            return nuevaSalsa;
        }

        // Get ALL con filtros
        public async Task<List<Salsa>> GetAllAsync(QueryParamsSalsa filtros)
        {
            return await _repository.GetAllAsync(filtros);
        }

        // GetById
        public async Task<Salsa?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        // Delete
        public async Task DeleteAsync(int id)
        {
            var existe = await _repository.GetByIdAsync(id);
            
            if (existe == null)
            {
                throw new KeyNotFoundException($"No existe ninguna salsa con ID {id}");
            }

            await _repository.DeleteAsync(id);
        }
    }
}