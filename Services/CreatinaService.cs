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

        // 1. CREAR
        public async Task<Creatina> CreateAsync(CreatinaCreateDto dto)
        {
            // Aquí es donde ocurre la MAGIA de la validación.
            // Llamamos al constructor de tu modelo.
            // Si los datos están mal (ej: precio < 0), esto explota (lanza excepción)
            // y no llegamos a llamar al repositorio.
            var nuevaCreatina = new Creatina(
                dto.Nombre,
                dto.Precio,
                dto.Stock,
                dto.Descripcion,
                dto.Imagen,     // Nota: Asegúrate de que el orden coincide con tu constructor
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

        // 2. LEER TODOS (Con filtros)
        public async Task<List<Creatina>> GetAllAsync(QueryParamsCreatina filtros)
        {
            return await _repository.GetAllAsync(filtros);
        }

        // 3. LEER UNO
        public async Task<Creatina?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        // 4. BORRAR
        public async Task DeleteAsync(int id)
        {
            // Primero comprobamos si existe
            var existente = await _repository.GetByIdAsync(id);
            if (existente == null)
            {
                throw new KeyNotFoundException($"No existe ninguna creatina con ID {id}");
            }

            await _repository.DeleteAsync(id);
        }

        // 5. ACTUALIZAR (Opcional, requiere un DTO de Update o reusar el Create)
        public async Task UpdateAsync(int id, CreatinaCreateDto dto)
        {
            var existente = await _repository.GetByIdAsync(id);
            if (existente == null)
            {
                throw new KeyNotFoundException($"No existe ninguna creatina con ID {id}");
            }

            // Creamos un objeto nuevo con los datos nuevos, pero manteniendo el ID viejo
            var creatinaActualizada = new Creatina(
                dto.Nombre, dto.Precio, dto.Stock, dto.Descripcion, dto.Imagen,
                dto.Categoria, dto.PesoKg, dto.Sabor, dto.Tipo, dto.Formato,
                dto.SelloCreapure, dto.EsMicronizada, dto.DosisDiariaGr
            );
            
            creatinaActualizada.Id = id; // ¡Importante! Mantener el ID

            await _repository.UpdateAsync(creatinaActualizada);
        }
    }
}