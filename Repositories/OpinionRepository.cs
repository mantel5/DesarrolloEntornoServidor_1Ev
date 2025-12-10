using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Repositories
{
    public class OpinionRepository : IOpinionRepository
    {
        private readonly string _connectionString;

        public OpinionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SuplementosDB") 
                ?? throw new Exception("Connection string not found");
        }

        public async Task AddAsync(Opinion opinion)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    INSERT INTO Opinion (UsuarioId, UsuarioNombre, ProductoId, Texto, Puntuacion, Fecha) 
                    VALUES (@UsuarioId, @UsuarioNombre, @ProductoId, @Texto, @Puntuacion, @Fecha);
                    SELECT SCOPE_IDENTITY();";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UsuarioId", opinion.UsuarioId);
                    cmd.Parameters.AddWithValue("@UsuarioNombre", opinion.UsuarioNombre);
                    cmd.Parameters.AddWithValue("@ProductoId", opinion.ProductoId);
                    cmd.Parameters.AddWithValue("@Texto", opinion.Texto);
                    cmd.Parameters.AddWithValue("@Puntuacion", opinion.Puntuacion);
                    cmd.Parameters.AddWithValue("@Fecha", opinion.Fecha);

                    var result = await cmd.ExecuteScalarAsync();
                    if (result != null)
                    {
                        opinion.Id = Convert.ToInt32(result);
                    }
                }
            }
        }

        public async Task<List<Opinion>> GetAllAsync(OpinionQueryParams queryParams)
        {
            var lista = new List<Opinion>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sb = new StringBuilder("SELECT * FROM Opinion WHERE 1=1");

                if (queryParams.ProductoId.HasValue)
                    sb.Append(" AND ProductoId = @ProductoId");
                
                if (queryParams.UsuarioId.HasValue)
                    sb.Append(" AND UsuarioId = @UsuarioId");

                if (queryParams.PuntuacionMin.HasValue)
                    sb.Append(" AND Puntuacion >= @PuntuacionMin");

                sb.Append(" ORDER BY Fecha DESC");

                using (var cmd = new SqlCommand(sb.ToString(), connection))
                {
                    if (queryParams.ProductoId.HasValue)
                        cmd.Parameters.AddWithValue("@ProductoId", queryParams.ProductoId);
                    
                    if (queryParams.UsuarioId.HasValue)
                        cmd.Parameters.AddWithValue("@UsuarioId", queryParams.UsuarioId);

                    if (queryParams.PuntuacionMin.HasValue)
                        cmd.Parameters.AddWithValue("@PuntuacionMin", queryParams.PuntuacionMin);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Opinion
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                UsuarioId = Convert.ToInt32(reader["UsuarioId"]),
                                UsuarioNombre = reader["UsuarioNombre"].ToString()!,
                                ProductoId = Convert.ToInt32(reader["ProductoId"]),
                                Texto = reader["Texto"].ToString()!,
                                Puntuacion = Convert.ToInt32(reader["Puntuacion"]),
                                Fecha = Convert.ToDateTime(reader["Fecha"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Opinion WHERE Id = @Id";
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}