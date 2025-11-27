using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Repositories
{
    public class TortitasRepository : ITortitasRepository
    {
        private readonly string _connectionString;

        public TortitasRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SuplementosDB") 
                ?? throw new Exception("Falta conexión en appsettings");
        }

        public async Task AddAsync(Tortitas t)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    INSERT INTO Tortitas 
                    (
                        -- Abuelo
                        Nombre, Descripcion, Imagen, Precio, Stock,
                        -- Padre (Macros)
                        Calorias, Proteinas, Carbohidratos, Grasas,
                        -- Hijo
                        Sabor, Tipo, PesoGr, EsSinGluten
                    )
                    VALUES 
                    (
                        @Nombre, @Descripcion, @Imagen, @Precio, @Stock,
                        @Calorias, @Proteinas, @Carbohidratos, @Grasas,
                        @Sabor, @Tipo, @PesoGr, @EsSinGluten
                    );";

                using (var cmd = new SqlCommand(query, connection))
                {
                    // Mapeo masivo
                    cmd.Parameters.AddWithValue("@Nombre", t.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", t.Descripcion);
                    cmd.Parameters.AddWithValue("@Imagen", t.Imagen);
                    cmd.Parameters.AddWithValue("@Precio", t.Precio);
                    cmd.Parameters.AddWithValue("@Stock", t.Stock);
                    
                    cmd.Parameters.AddWithValue("@Calorias", t.Calorias);
                    cmd.Parameters.AddWithValue("@Proteinas", t.Proteinas);
                    cmd.Parameters.AddWithValue("@Carbohidratos", t.Carbohidratos);
                    cmd.Parameters.AddWithValue("@Grasas", t.Grasas);
                    
                    cmd.Parameters.AddWithValue("@Sabor", t.Sabor);
                    cmd.Parameters.AddWithValue("@Tipo", t.Tipo);
                    cmd.Parameters.AddWithValue("@PesoGr", t.PesoGr);
                    cmd.Parameters.AddWithValue("@EsSinGluten", t.EsSinGluten);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<Tortitas?> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("SELECT * FROM Tortitas WHERE Id = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) return MapReaderToTortitas(reader);
                    }
                }
            }
            return null;
        }

        public async Task<List<Tortitas>> GetAllAsync(QueryParamsTortitas filtros)
        {
            var lista = new List<Tortitas>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sb = new StringBuilder("SELECT * FROM Tortitas WHERE 1=1");
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
                if (filtros.ProteinasMin.HasValue)
                {
                    sb.Append(" AND Proteinas >= @ProtMin");
                    cmd.Parameters.AddWithValue("@ProtMin", filtros.ProteinasMin.Value);
                }

                // --- Filtros Hijo (Tortitas) ---
                if (!string.IsNullOrWhiteSpace(filtros.Sabor))
                {
                    sb.Append(" AND Sabor = @Sabor");
                    cmd.Parameters.AddWithValue("@Sabor", filtros.Sabor);
                }
                if (!string.IsNullOrWhiteSpace(filtros.Tipo))
                {
                    sb.Append(" AND Tipo = @Tipo");
                    cmd.Parameters.AddWithValue("@Tipo", filtros.Tipo);
                }
                if (filtros.SoloSinGluten == true)
                {
                    sb.Append(" AND EsSinGluten = 1");
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
                    while (await reader.ReadAsync()) lista.Add(MapReaderToTortitas(reader));
                }
            }
            return lista;
        }

        // (Omitimos Update y Delete por brevedad, son iguales a los anteriores cambiando la query)
        public async Task UpdateAsync(Tortitas t) { await Task.CompletedTask; } 
        public async Task DeleteAsync(int id) 
        {
             using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("DELETE FROM Tortitas WHERE Id = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private Tortitas MapReaderToTortitas(SqlDataReader reader)
        {
            return new Tortitas
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
                Tipo = reader["Tipo"].ToString()!,
                PesoGr = Convert.ToInt32(reader["PesoGr"]),
                EsSinGluten = Convert.ToBoolean(reader["EsSinGluten"])
            };
        }
    }
}