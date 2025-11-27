using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using SuplementosAPI.Repositories;

namespace SuplementosAPI.Services
{
    public class CreatinaService : ICreatinaService
    {
        private readonly ICreatinaRepository _repository;

        public CreatinaService(ICreatinaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Creatina> CreateAsync(CreatinaCreateDto dto)
        {
             
            var nuevaCreatina = new Creatina(
                dto.Nombre,
                dto.Precio,
                dto.Stock,
                dto.Descripcion,
                dto.Imagen,     
                dto.Categoria,
                dto.PesoKg,
                dto.Sabor,
                dto.Tipo,
                dto.Formato,
                dto.SelloCreapure,
                dto.EsMicronizada,
                dto.DosisDiariaGr
            );

            await _repository.AddAsync(nuevaCreatina);
            return nuevaCreatina;
        }

        // Get ALL con filtros
        public async Task<List<Creatina>> GetAllAsync(QueryParamsCreatina filtros)
        {
            return await _repository.GetAllAsync(filtros);
        }

        // GetById
        public async Task<Creatina?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        // Delete
        public async Task DeleteAsync(int id)
        {
            var existente = await _repository.GetByIdAsync(id);
            if (existente == null)
            {
                throw new KeyNotFoundException($"No existe ninguna creatina con ID {id}");
            }

            await _repository.DeleteAsync(id);
        }

        // Update                               
        public async Task UpdateAsync(int id, CreatinaCreateDto dto)
        {
            var existente = await _repository.GetByIdAsync(id);
            if (existente == null)
            {
                throw new KeyNotFoundException($"No existe ninguna creatina con ID {id}");
            }

            var creatinaActualizada = new Creatina(
                dto.Nombre, dto.Precio, dto.Stock, dto.Descripcion, dto.Imagen,
                dto.Categoria, dto.PesoKg, dto.Sabor, dto.Tipo, dto.Formato,
                dto.SelloCreapure, dto.EsMicronizada, dto.DosisDiariaGr
            );
            
            creatinaActualizada.Id = id; 

            await _repository.UpdateAsync(creatinaActualizada);
        }
    }
}