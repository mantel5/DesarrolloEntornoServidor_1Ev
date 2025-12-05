using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using SuplementosAPI.Repositories;

namespace SuplementosAPI.Services
{
    public class Omega3Service : IOmega3Service
    {
        private readonly IOmega3Repository _repository;

        public Omega3Service(IOmega3Repository repository)
        {
            _repository = repository;
        }

        public async Task<Omega3> CreateAsync(Omega3CreateDto dto)
        {
            var nuevoOmega = new Omega3(
                dto.Nombre, 
                dto.Precio, 
                dto.Stock, 
                dto.Descripcion, 
                dto.Imagen,
                
                dto.Categoria, 
                dto.PesoKg,
                
                dto.Formato, 
                dto.Origen, 
                dto.MgEPA, 
                dto.MgDHA, 
                dto.CertificadoIFOS
            );

            await _repository.AddAsync(nuevoOmega);
            return nuevoOmega;
        }

        public async Task<List<Omega3>> GetAllAsync(QueryParamsOmega3 filtros)
        {
            return await _repository.GetAllAsync(filtros);
        }

        public async Task<Omega3?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var existe = await _repository.GetByIdAsync(id);
            
            if (existe == null)
            {
                throw new KeyNotFoundException($"No se encontró ningún Omega 3 con ID {id}");
            }

            await _repository.DeleteAsync(id);
        }
    }
}