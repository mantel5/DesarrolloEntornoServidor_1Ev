using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using SuplementosAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuplementosAPI.Services
{
    public class PacksService : IPacksService
    {
        private readonly IPacksRepository _packsRepo;
        private readonly IProteinaRepository _proteinaRepo;
        private readonly IPreEntrenoRepository _preEntrenoRepo;
        private readonly ICreatinaRepository _creatinaRepo;
        private readonly IBebidaRepository _bebidaRepo;

        public PacksService(
            IPacksRepository packsRepo,
            IProteinaRepository proteinaRepo,
            IPreEntrenoRepository preEntrenoRepo,
            ICreatinaRepository creatinaRepo,
            IBebidaRepository bebidaRepo)
        {
            _packsRepo = packsRepo;
            _proteinaRepo = proteinaRepo;
            _preEntrenoRepo = preEntrenoRepo;
            _creatinaRepo = creatinaRepo;
            _bebidaRepo = bebidaRepo;
        }

        private async Task<Packs> LoadComponentProducts(Packs pack)
        {
            pack.Proteina = await _proteinaRepo.GetByIdAsync(pack.ProteinaId)
                ?? throw new KeyNotFoundException($"Proteina con ID {pack.ProteinaId} no encontrada.");
            pack.PreEntreno = await _preEntrenoRepo.GetByIdAsync(pack.PreEntrenoId)
                ?? throw new KeyNotFoundException($"PreEntreno con ID {pack.PreEntrenoId} no encontrado.");
            pack.Creatina = await _creatinaRepo.GetByIdAsync(pack.CreatinaId)
                ?? throw new KeyNotFoundException($"Creatina con ID {pack.CreatinaId} no encontrada.");
            pack.Bebida = await _bebidaRepo.GetByIdAsync(pack.BebidaId)
                ?? throw new KeyNotFoundException($"Bebida con ID {pack.BebidaId} no encontrada.");

            pack.Precio = pack.CalcularPrecio();
            return pack;
        }

        public async Task<Packs> CreateAsync(PacksCreateDto dto)
        {
            var proteina = await _proteinaRepo.GetByIdAsync(dto.ProteinaId)
                ?? throw new KeyNotFoundException($"Proteina con ID {dto.ProteinaId} no encontrada.");
            var preEntreno = await _preEntrenoRepo.GetByIdAsync(dto.PreEntrenoId)
                ?? throw new KeyNotFoundException($"PreEntreno con ID {dto.PreEntrenoId} no encontrado.");
            var creatina = await _creatinaRepo.GetByIdAsync(dto.CreatinaId)
                ?? throw new KeyNotFoundException($"Creatina con ID {dto.CreatinaId} no encontrada.");
            var bebida = await _bebidaRepo.GetByIdAsync(dto.BebidaId)
                ?? throw new KeyNotFoundException($"Bebida con ID {dto.BebidaId} no encontrada.");

            var pack = new Packs(
                dto.Nombre,
                dto.Descripcion,
                dto.Imagen,
                dto.Categoria,
                dto.PesoKg,
                dto.Stock,
                proteina,
                preEntreno,
                creatina,
                bebida
            );

            pack.Id = await _packsRepo.AddAsync(pack);
            return pack;
        }

        public async Task<Packs?> GetByIdAsync(int id)
        {
            var pack = await _packsRepo.GetByIdAsync(id);
            if (pack == null) return null;
            return await LoadComponentProducts(pack);
        }

        public async Task<List<Packs>> GetAllAsync()
        {
            var packs = await _packsRepo.GetAllAsync(new QueryParamsPacks());
            var result = new List<Packs>();
            foreach (var pack in packs)
            {
                result.Add(await LoadComponentProducts(pack));
            }
            return result;
        }

        public async Task UpdateAsync(int id, PacksCreateDto dto)
        {
            var proteina = await _proteinaRepo.GetByIdAsync(dto.ProteinaId)
                ?? throw new KeyNotFoundException($"Proteina con ID {dto.ProteinaId} no encontrada.");
            var preEntreno = await _preEntrenoRepo.GetByIdAsync(dto.PreEntrenoId)
                ?? throw new KeyNotFoundException($"PreEntreno con ID {dto.PreEntrenoId} no encontrado.");
            var creatina = await _creatinaRepo.GetByIdAsync(dto.CreatinaId)
                ?? throw new KeyNotFoundException($"Creatina con ID {dto.CreatinaId} no encontrada.");
            var bebida = await _bebidaRepo.GetByIdAsync(dto.BebidaId)
                ?? throw new KeyNotFoundException($"Bebida con ID {dto.BebidaId} no encontrada.");

            var pack = new Packs(
                dto.Nombre,
                dto.Descripcion,
                dto.Imagen,
                dto.Categoria,
                dto.PesoKg,
                dto.Stock,
                proteina,
                preEntreno,
                creatina,
                bebida
            );

            pack.Id = id;
            await _packsRepo.UpdateAsync(pack);
        }

        public async Task DeleteAsync(int id)
        {
            await _packsRepo.DeleteAsync(id);
        }

        public Task<List<Packs>> GetAllAsync(QueryParamsPacks filtros)
        {
            throw new NotImplementedException();
        }
    }
}
