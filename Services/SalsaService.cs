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

        // 1. CREAR
        public async Task<Salsa> CreateAsync(SalsaCreateDto dto)
        {
            // Aquí instanciamos el modelo.
            // Si el DTO trae macros negativos o precio negativo,
            // el constructor de 'ComidaBase' o 'ProductoBase' lanzará la excepción.
            
            var nuevaSalsa = new Salsa(
                // --- Abuelo (ProductoBase) ---
                dto.Nombre, 
                dto.Precio, 
                dto.Stock, 
                dto.Descripcion, 
                dto.Imagen,
                
                // --- Padre (ComidaBase - Macros) ---
                dto.Calorias, 
                dto.Proteinas, 
                dto.Carbohidratos, 
                dto.Grasas,
                
                // --- Hijo (Salsa) ---
                dto.Sabor, 
                dto.EsPicante, 
                dto.EsZero
            );

            await _repository.AddAsync(nuevaSalsa);
            return nuevaSalsa;
        }

        // 2. LEER TODOS (Passthrough al repo)
        public async Task<List<Salsa>> GetAllAsync(QueryParamsSalsa filtros)
        {
            return await _repository.GetAllAsync(filtros);
        }

        // 3. LEER UNO
        public async Task<Salsa?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        // 4. BORRAR (Con validación previa)
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