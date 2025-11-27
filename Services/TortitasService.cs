using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using SuplementosAPI.Repositories;

namespace SuplementosAPI.Services
{
    public class TortitasService : ITortitasService
    {
        private readonly ITortitasRepository _repository;

        public TortitasService(ITortitasRepository repository)
        {
            _repository = repository;
        }

        public async Task<Tortitas> CreateAsync(TortitasCreateDto dto)
        {
            var nuevasTortitas = new Tortitas(
                // Abuelo
                dto.Nombre, dto.Precio, dto.Stock, dto.Descripcion, dto.Imagen,
                // Padre (Macros)
                dto.Calorias, dto.Proteinas, dto.Carbohidratos, dto.Grasas,
                // Hijo
                dto.Sabor, dto.Tipo, dto.PesoGr, dto.EsSinGluten
            );

            await _repository.AddAsync(nuevasTortitas);
            return nuevasTortitas;
        }

        public async Task<List<Tortitas>> GetAllAsync(QueryParamsTortitas filtros)
        {
            return await _repository.GetAllAsync(filtros);
        }

        public async Task<Tortitas?> GetByIdAsync(int id)
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