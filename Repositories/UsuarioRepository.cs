using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SuplementosDB") 
                ?? throw new Exception("Falta conexión");
        }

        // Añadir nuevo usuario
        public async Task AddAsync(Usuario u)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    INSERT INTO Usuario (Nombre, Email, Password, Rol, Direccion, Telefono)
                    VALUES (@Nombre, @Email, @Password, @Rol, @Direccion, @Telefono)";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", u.Nombre);
                    cmd.Parameters.AddWithValue("@Email", u.Email);
                    cmd.Parameters.AddWithValue("@Password", u.Password); 
                    cmd.Parameters.AddWithValue("@Rol", u.Rol);
                    cmd.Parameters.AddWithValue("@Direccion", (object?)u.Direccion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Telefono", (object?)u.Telefono ?? DBNull.Value);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // GetByEmail para login
        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Usuario WHERE Email = @Email";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) return MapReaderToUsuario(reader);
                    }
                }
            }
            return null;
        }

        // verificamos si existe un email
        public async Task<bool> ExistsEmailAsync(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT COUNT(1) FROM Usuario WHERE Email = @Email";
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    int count = (int)await cmd.ExecuteScalarAsync();
                    return count > 0;
                }
            }
        }

        // GetById por id
        public async Task<Usuario?> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("SELECT * FROM Usuario WHERE Id = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) return MapReaderToUsuario(reader);
                    }
                }
            }
            return null;
        }

        // Listar usuarios con filtros y paginación
        public async Task<List<Usuario>> GetAllAsync(QueryParamsUsuario filtros)
        {
            var lista = new List<Usuario>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sb = new StringBuilder("SELECT * FROM Usuario WHERE 1=1");
                var cmd = new SqlCommand();

                if (!string.IsNullOrWhiteSpace(filtros.BuscarNombre))
                {
                    sb.Append(" AND Nombre LIKE @Nombre");
                    cmd.Parameters.AddWithValue("@Nombre", $"%{filtros.BuscarNombre}%");
                }
                if (!string.IsNullOrWhiteSpace(filtros.Rol))
                {
                    sb.Append(" AND Rol = @Rol");
                    cmd.Parameters.AddWithValue("@Rol", filtros.Rol);
                }

                sb.Append(" ORDER BY Id ASC");
                int saltar = (filtros.Pagina - 1) * filtros.ElementosPorPagina;
                sb.Append(" OFFSET @Saltar ROWS FETCH NEXT @Tomar ROWS ONLY");
                cmd.Parameters.AddWithValue("@Saltar", saltar);
                cmd.Parameters.AddWithValue("@Tomar", filtros.ElementosPorPagina);

                cmd.CommandText = sb.ToString();
                cmd.Connection = connection;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync()) lista.Add(MapReaderToUsuario(reader));
                }
            }
            return lista;
        }

        // Mapeo de SqlDataReader a Usuario
        private Usuario MapReaderToUsuario(SqlDataReader reader)
        {
            return new Usuario
            {
                Id = Convert.ToInt32(reader["Id"]),
                Nombre = reader["Nombre"].ToString()!,
                Email = reader["Email"].ToString()!,
                Password = reader["Password"].ToString()!,
                Rol = reader["Rol"].ToString()!,
                Direccion = reader["Direccion"] == DBNull.Value ? "" : reader["Direccion"].ToString()!,
                Telefono = reader["Telefono"] == DBNull.Value ? "" : reader["Telefono"].ToString()!
            };
        }
    }
}