using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using SuplementosAPI.Repositories;

namespace SuplementosAPI.Services
{
    public class PreEntrenoService : IPreEntrenoService
    {
        private readonly IPreEntrenoRepository _repository;

        public PreEntrenoService(IPreEntrenoRepository repository)
        {
            _repository = repository;
        }
  
        public async Task<PreEntreno> CreateAsync(PreEntrenoCreateDto dto)
        {
            // Usamos el constructor de PreEntreno para validar
            var nuevaPreEntreno = new PreEntreno(
                dto.Nombre, dto.Precio, dto.Stock, dto.Descripcion, dto.Imagen,
                dto.Categoria, dto.PesoKg,
                dto.Sabor, dto.Tipo, dto.Formato, dto.MgCafeina, dto.TieneBetaAlanina
            );

            await _repository.AddAsync(nuevaPreEntreno);
            return nuevaPreEntreno;
        }

        public async Task<List<PreEntreno>> GetAllAsync(QueryParamsPreEntreno filtros)
        {
            return await _repository.GetAllAsync(filtros);
        }

        public async Task<PreEntreno?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var existe = await _repository.GetByIdAsync(id);
            if (existe == null) throw new KeyNotFoundException("ID no encontrado");
            await _repository.DeleteAsync(id);
        }

        public async Task AddAsync(PreEntreno nuevaPreEntreno)
        {
            await _repository.AddAsync(nuevaPreEntreno);
        }
    }
}   