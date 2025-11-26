using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using SuplementosAPI.Repositories;

namespace SuplementosAPI.Services
{
    public class ProteinaService : IProteinaService
    {
        private readonly IProteinaRepository _repository;

        public ProteinaService(IProteinaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Proteina> CreateAsync(ProteinaCreateDto dto)
        {
            // Usamos el constructor de Proteina para validar
            var nuevaProteina = new Proteina(
                dto.Nombre, dto.Precio, dto.Stock, dto.Descripcion, dto.Imagen,
                dto.Categoria, dto.PesoKg,
                dto.Sabor, dto.Tipo, dto.Porcentaje, dto.EsSinLactosa
            );

            await _repository.AddAsync(nuevaProteina);
            return nuevaProteina;
        }

        public async Task<List<Proteina>> GetAllAsync(QueryParamsProteina filtros)
        {
            return await _repository.GetAllAsync(filtros);
        }

        public async Task<Proteina?> GetByIdAsync(int id)
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