using SuplementosAPI.Dtos;
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

        public async Task<Opinion> CreateAsync(OpinionCreateDto dto)
        {
            if (dto.Puntuacion < 1 || dto.Puntuacion > 5)
                throw new ArgumentException("La puntuación debe estar entre 1 y 5");

            var opinion = new Opinion
            {
                UsuarioId = dto.UsuarioId,
                UsuarioNombre = dto.UsuarioNombre,
                ProductoId = dto.ProductoId,
                Texto = dto.Texto,
                Puntuacion = dto.Puntuacion,
                Fecha = DateTime.Now
            };

            await _repository.AddAsync(opinion);
            return opinion;
        }

        public async Task<List<Opinion>> GetAllAsync(OpinionQueryParams queryParams)
        {
            return await _repository.GetAllAsync(queryParams);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}