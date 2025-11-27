using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Repositories
{
    public class BebidaRepository : IBebidaRepository
    {
        private readonly string _connectionString;

        public BebidaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SuplementosDB") 
                ?? throw new Exception("Falta conexión en appsettings");
        }

        public async Task AddAsync(Bebida b)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    INSERT INTO Bebida 
                    (
                        -- Abuelo
                        Nombre, Descripcion, Imagen, Precio, Stock,
                        -- Padre (Macros)
                        Calorias, Proteinas, Carbohidratos, Grasas,
                        -- Hijo
                        Sabor, Mililitros, TieneGluten, TieneGas
                    )
                    VALUES 
                    (
                        @Nombre, @Descripcion, @Imagen, @Precio, @Stock,
                        @Calorias, @Proteinas, @Carbohidratos, @Grasas,
                        @Sabor, @Mililitros, @TieneGluten, @TieneGas
                    );";

                using (var cmd = new SqlCommand(query, connection))
                {
                    // Mapeo masivo
                    cmd.Parameters.AddWithValue("@Nombre", b.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", b.Descripcion);
                    cmd.Parameters.AddWithValue("@Imagen", b.Imagen);
                    cmd.Parameters.AddWithValue("@Precio", b.Precio);
                    cmd.Parameters.AddWithValue("@Stock", b.Stock);
                    
                    cmd.Parameters.AddWithValue("@Calorias", b.Calorias);
                    cmd.Parameters.AddWithValue("@Proteinas", b.Proteinas);
                    cmd.Parameters.AddWithValue("@Carbohidratos", b.Carbohidratos);
                    cmd.Parameters.AddWithValue("@Grasas", b.Grasas);
                    
                    cmd.Parameters.AddWithValue("@Sabor", b.Sabor);
                    cmd.Parameters.AddWithValue("@Mililitros", b.Mililitros);
                    cmd.Parameters.AddWithValue("@TieneGluten", b.TieneGluten);
                    cmd.Parameters.AddWithValue("@TieneGas", b.TieneGas);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<Bebida?> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("SELECT * FROM Bebida WHERE Id = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) return MapReaderToBebida(reader);
                    }
                }
            }
            return null;
        }

        public async Task<List<Bebida>> GetAllAsync(QueryParamsBebida filtros)
        {
            var lista = new List<Bebida>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sb = new StringBuilder("SELECT * FROM Bebida WHERE 1=1");
                var cmd = new SqlCommand();

                // --- Filtros Abuelo ---
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

                // --- Filtros Padre (Macros) ---
                if (filtros.CaloriasMax.HasValue)
                {
                    sb.Append(" AND Calorias <= @CalMax");
                    cmd.Parameters.AddWithValue("@CalMax", filtros.CaloriasMax.Value);
                }
                
                // --- Filtros Hijo (Bebida) ---
                if (!string.IsNullOrWhiteSpace(filtros.Sabor))
                {
                    sb.Append(" AND Sabor = @Sabor");
                    cmd.Parameters.AddWithValue("@Sabor", filtros.Sabor);
                }
                
                if (filtros.SoloSinGluten == true)
                {
                    // Si el usuario quiere SIN gluten, TieneGluten debe ser 0 (false)
                    sb.Append(" AND TieneGluten = 0");
                }
                
                if (filtros.SoloConGas == true)
                {
                    sb.Append(" AND TieneGas = 1");
                }

                if (filtros.MililitrosMax.HasValue)
                {
                    sb.Append(" AND Mililitros <= @MlMax");
                    cmd.Parameters.AddWithValue("@MlMax", filtros.MililitrosMax.Value);
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
                    while (await reader.ReadAsync()) lista.Add(MapReaderToBebida(reader));
                }
            }
            return lista;
        }

        // (Omitimos Update y Delete por brevedad, son iguales cambiando la tabla)
        public async Task UpdateAsync(Bebida b) { await Task.CompletedTask; }
        public async Task DeleteAsync(int id) 
        {
             using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("DELETE FROM Bebida WHERE Id = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private Bebida MapReaderToBebida(SqlDataReader reader)
        {
            return new Bebida
            {
                Id = Convert.ToInt32(reader["Id"]),
                // Abuelo
                Nombre = reader["Nombre"].ToString()!,
                Descripcion = reader["Descripcion"].ToString()!,
                Imagen = reader["Imagen"].ToString()!,
                Precio = Convert.ToDecimal(reader["Precio"]),
                Stock = Convert.ToInt32(reader["Stock"]),
                // Padre
                Calorias = Convert.ToDouble(reader["Calorias"]),
                Proteinas = Convert.ToDouble(reader["Proteinas"]),
                Carbohidratos = Convert.ToDouble(reader["Carbohidratos"]),
                Grasas = Convert.ToDouble(reader["Grasas"]),
                // Hijo
                Sabor = reader["Sabor"].ToString()!,
                Mililitros = Convert.ToInt32(reader["Mililitros"]),
                TieneGluten = Convert.ToBoolean(reader["TieneGluten"]),
                TieneGas = Convert.ToBoolean(reader["TieneGas"])
            };
        }
    }
}