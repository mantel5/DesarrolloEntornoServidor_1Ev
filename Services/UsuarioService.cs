using SuplementosAPI.Dtos;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using SuplementosAPI.Repositories;

namespace SuplementosAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        // Registro
        public async Task<UsuarioViewDto> RegisterAsync(UsuarioRegisterDto dto)
        {
            bool existe = await _repository.ExistsEmailAsync(dto.Email);
            if (existe)
            {
                throw new ArgumentException("El email ya está registrado.");
            }

       
            var nuevoUsuario = new Usuario(
                dto.Nombre,
                dto.Email,
                dto.Password, 
                "Cliente", 
                dto.Direccion,
                dto.Telefono
            );

            await _repository.AddAsync(nuevoUsuario);

            return MapToViewDto(nuevoUsuario);
        }

        // Login
        public async Task<UsuarioViewDto?> LoginAsync(UsuarioLoginDto dto)
        {
            var usuario = await _repository.GetByEmailAsync(dto.Email);

            if (usuario == null) return null;

            if (usuario.Password != dto.Password)
            {
                return null; 
            }

            return MapToViewDto(usuario);
        }

       //GetById
        public async Task<UsuarioViewDto?> GetByIdAsync(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null) return null;

            return MapToViewDto(usuario);
        }

       // Get All Usuarios
        public async Task<List<UsuarioViewDto>> GetAllAsync(QueryParamsUsuario filtros)
        {
            var usuarios = await _repository.GetAllAsync(filtros);
            
            // Convertimos la lista de Modelos a lista de DTOs
            var listaDtos = new List<UsuarioViewDto>();
            foreach (var u in usuarios)
            {
                listaDtos.Add(MapToViewDto(u));
            }
            
            return listaDtos;
        }

        // mapeo para devolver usuario
        private UsuarioViewDto MapToViewDto(Usuario u)
        {
            return new UsuarioViewDto
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Email = u.Email,
                Rol = u.Rol,
                Direccion = u.Direccion
            };
        }
    }
}