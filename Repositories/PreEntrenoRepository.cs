using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Repositories
{
    public class PreEntrenoRepository : IPreEntrenoRepository
    {
        private readonly string _connectionString;

        public PreEntrenoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SuplementosDB") 
                ?? throw new Exception("Falta conexión en appsettings");
        }

        public async Task AddAsync(PreEntreno p)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    INSERT INTO PreEntreno 
                    (
                        -- Abuelo
                        Nombre, Descripcion, Imagen, Precio, Stock,
                        -- Padre (Macros)
                        Categoria, PesoKg,
                        -- Hijo
                        Formato, Tipo, Sabor, MgCafeina,TieneBetaAlanina
                    )
                    VALUES 
                    (
                        @Nombre, @Descripcion, @Imagen, @Precio, @Stock,
                        @Categoria, @PesoKg,
                        @Formato, @Tipo, @Sabor, @MgCafeina,@TieneBetaAlanina
                    );";

                using (var cmd = new SqlCommand(query, connection))
                {
                    // Mapeo masivo
                    cmd.Parameters.AddWithValue("@Nombre", p.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", p.Descripcion);
                    cmd.Parameters.AddWithValue("@Imagen", p.Imagen);
                    cmd.Parameters.AddWithValue("@Precio", p.Precio);
                    cmd.Parameters.AddWithValue("@Stock", p.Stock);
                    
                    cmd.Parameters.AddWithValue("@Categoria", p.Categoria);
                    cmd.Parameters.AddWithValue("@PesoKg", p.PesoKg);
                    
                    cmd.Parameters.AddWithValue("@Formato", p.Formato);
                    cmd.Parameters.AddWithValue("@Tipo", p.Tipo);
                    cmd.Parameters.AddWithValue("@Sabor", p.Sabor);
                    cmd.Parameters.AddWithValue("@MgCafeina", p.MgCafeina);
                    cmd.Parameters.AddWithValue("@TieneBetaAlanina", p.TieneBetaAlanina);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<PreEntreno?> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("SELECT * FROM PreEntreno WHERE Id = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) return MapReaderToPreEntreno(reader);
                    }
                }
            }
            return null;
        }

        public async Task<List<PreEntreno>> GetAllAsync(QueryParamsPreEntreno filtros)
        {
            var lista = new List<PreEntreno>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sb = new StringBuilder("SELECT * FROM PreEntreno WHERE 1=1");
                var cmd = new SqlCommand();

                 // Filtros Comunes 
                if (!string.IsNullOrWhiteSpace(filtros.BuscarNombre))
                {
                    sb.Append(" AND Nombre LIKE @Nombre");
                    cmd.Parameters.AddWithValue("@Nombre", $"%{filtros.BuscarNombre}%");
                }
                if (filtros.PrecioMax.HasValue)
                {
                    sb.Append(" AND Precio <= @PrecioMax");
                    cmd.Parameters.AddWithValue("@PrecioMax", filtros.PrecioMax.Value);
                }

                // Filtros Específicos PreEntreno
                if (!string.IsNullOrWhiteSpace(filtros.Tipo))
                {
                    sb.Append(" AND Tipo = @Tipo");
                    cmd.Parameters.AddWithValue("@Tipo", filtros.Tipo);
                }
                if (!string.IsNullOrWhiteSpace(filtros.Formato))
                {
                    sb.Append(" AND Formato = @Formato");
                    cmd.Parameters.AddWithValue("@Formato", filtros.Formato);
                }
                if (!string.IsNullOrWhiteSpace(filtros.Sabor))
                {
                    sb.Append(" AND Sabor = @Sabor");
                    cmd.Parameters.AddWithValue("@Sabor", filtros.Sabor);
                }

                if (filtros.SoloConBetaAlanina.HasValue && filtros.SoloConBetaAlanina.Value)
                {
                    sb.Append(" AND TieneBetaAlanina = 1");
                }
                if (filtros.MgCafeinaMin.HasValue)
                {
                    sb.Append(" AND MgCafeina >= @MgCafeinaMin");
                    cmd.Parameters.AddWithValue("@MgCafeinaMin", filtros.MgCafeinaMin.Value);
                }
                // Ordenación y Paginación
                sb.Append(" ORDER BY Id ASC"); 
                int saltar = (filtros.Pagina - 1) * filtros.ElementosPorPagina;
                sb.Append(" OFFSET @Saltar ROWS FETCH NEXT @Tomar ROWS ONLY");
                cmd.Parameters.AddWithValue("@Saltar", saltar);
                cmd.Parameters.AddWithValue("@Tomar", filtros.ElementosPorPagina);

                cmd.CommandText = sb.ToString();
                cmd.Connection = connection;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync()) lista.Add(MapReaderToPreEntreno(reader));
                }
            }
            return lista;
        }

        // (Omitimos Update y Delete por brevedad, son iguales cambiando la tabla)
        public async Task UpdateAsync(PreEntreno b) { await Task.CompletedTask; }
        public async Task DeleteAsync(int id) 
        {
             using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("DELETE FROM PreEntreno WHERE Id = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private PreEntreno MapReaderToPreEntreno(SqlDataReader reader)
        {
            return new PreEntreno
            {
                Id = Convert.ToInt32(reader["Id"]),
                // Abuelo
                Nombre = reader["Nombre"].ToString()!,
                Descripcion = reader["Descripcion"].ToString()!,
                Imagen = reader["Imagen"].ToString()!,
                Precio = Convert.ToDecimal(reader["Precio"]),
                Stock = Convert.ToInt32(reader["Stock"]),
                // Padre
                Categoria = reader["Categoria"].ToString()!,
                PesoKg = Convert.ToDouble(reader["PesoKg"]),
                // Hijo
                Sabor = reader["Sabor"].ToString()!,
                Formato = reader["Formato"].ToString()!,
                MgCafeina = Convert.ToInt32(reader["MgCafeina"]),
                TieneBetaAlanina = Convert.ToBoolean(reader["TieneBetaAlanina"]),
                Tipo = reader["Tipo"].ToString()!
                
            };
        }
    }
}