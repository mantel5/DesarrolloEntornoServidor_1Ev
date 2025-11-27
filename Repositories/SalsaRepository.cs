using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Repositories
{
    public class SalsaRepository : ISalsaRepository
    {
        private readonly string _connectionString;

        public SalsaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SuplementosDB") 
                ?? throw new Exception("Falta conexión en appsettings");
        }

        // Guardamos en la BD una nueva Salsa
        public async Task AddAsync(Salsa s)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                string query = @"
                    INSERT INTO Salsa 
                    (
                        -- Abuelo (ProductoBase)
                        Nombre, Descripcion, Imagen, Precio, Stock,
                        -- Padre (ComidaBase - Macros)
                        Calorias, Proteinas, Carbohidratos, Grasas,
                        -- Hijo (Salsa)
                        Sabor, EsPicante, EsZero
                    )
                    VALUES 
                    (
                        @Nombre, @Descripcion, @Imagen, @Precio, @Stock,
                        @Calorias, @Proteinas, @Carbohidratos, @Grasas,
                        @Sabor, @EsPicante, @EsZero
                    );";

                using (var cmd = new SqlCommand(query, connection))
                {
                    // Abuelo
                    cmd.Parameters.AddWithValue("@Nombre", s.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", s.Descripcion);
                    cmd.Parameters.AddWithValue("@Imagen", s.Imagen);
                    cmd.Parameters.AddWithValue("@Precio", s.Precio);
                    cmd.Parameters.AddWithValue("@Stock", s.Stock);
                    
                    // Padre (Macros)
                    cmd.Parameters.AddWithValue("@Calorias", s.Calorias);
                    cmd.Parameters.AddWithValue("@Proteinas", s.Proteinas);
                    cmd.Parameters.AddWithValue("@Carbohidratos", s.Carbohidratos);
                    cmd.Parameters.AddWithValue("@Grasas", s.Grasas);
                    
                    // Hijo
                    cmd.Parameters.AddWithValue("@Sabor", s.Sabor);
                    cmd.Parameters.AddWithValue("@EsPicante", s.EsPicante);
                    cmd.Parameters.AddWithValue("@EsZero", s.EsZero);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

       // GetById
        public async Task<Salsa?> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("SELECT * FROM Salsa WHERE Id = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) return MapReaderToSalsa(reader);
                    }
                }
            }
            return null;
        }

        // GetAll con filtros
        public async Task<List<Salsa>> GetAllAsync(QueryParamsSalsa filtros)
        {
            var lista = new List<Salsa>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sb = new StringBuilder("SELECT * FROM Salsa WHERE 1=1");
                var cmd = new SqlCommand();

                // FILTROS ABUELO (ProductoBase
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

                // FILTROS PADRE (ComidaBase - Macros)
                
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
                if (filtros.GrasasMax.HasValue)
                {
                    sb.Append(" AND Grasas <= @GrasasMax");
                    cmd.Parameters.AddWithValue("@GrasasMax", filtros.GrasasMax.Value);
                }
                if (filtros.CarbohidratosMax.HasValue)
                {
                    sb.Append(" AND Carbohidratos <= @CarbsMax");
                    cmd.Parameters.AddWithValue("@CarbsMax", filtros.CarbohidratosMax.Value);
                }

                // FILTROS HIJO (Salsa)
                if (!string.IsNullOrWhiteSpace(filtros.Sabor))
                {
                    sb.Append(" AND Sabor = @Sabor");
                    cmd.Parameters.AddWithValue("@Sabor", filtros.Sabor);
                }
                if (filtros.SoloZero == true)
                {
                    sb.Append(" AND EsZero = 1");
                }
                if (filtros.SoloPicante == true)
                {
                    sb.Append(" AND EsPicante = 1");
                }

                // ORDENACIÓN Y PAGINACIÓN 
                sb.Append(" ORDER BY Id ASC"); 
                
                int saltar = (filtros.Pagina - 1) * filtros.ElementosPorPagina;
                sb.Append(" OFFSET @Saltar ROWS FETCH NEXT @Tomar ROWS ONLY");
                cmd.Parameters.AddWithValue("@Saltar", saltar);
                cmd.Parameters.AddWithValue("@Tomar", filtros.ElementosPorPagina);

                cmd.CommandText = sb.ToString();
                cmd.Connection = connection;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync()) lista.Add(MapReaderToSalsa(reader));
                }
            }
            return lista;
        }

        
        
        // Update
        public async Task UpdateAsync(Salsa s)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    UPDATE Salsa SET 
                        Nombre = @Nombre, Descripcion = @Descripcion, Imagen = @Imagen, 
                        Precio = @Precio, Stock = @Stock,
                        Calorias = @Calorias, Proteinas = @Proteinas, 
                        Carbohidratos = @Carbohidratos, Grasas = @Grasas,
                        Sabor = @Sabor, EsPicante = @EsPicante, EsZero = @EsZero
                    WHERE Id = @Id";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", s.Id);
                    cmd.Parameters.AddWithValue("@Nombre", s.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", s.Descripcion);
                    cmd.Parameters.AddWithValue("@Imagen", s.Imagen);
                    cmd.Parameters.AddWithValue("@Precio", s.Precio);
                    cmd.Parameters.AddWithValue("@Stock", s.Stock);
                    cmd.Parameters.AddWithValue("@Calorias", s.Calorias);
                    cmd.Parameters.AddWithValue("@Proteinas", s.Proteinas);
                    cmd.Parameters.AddWithValue("@Carbohidratos", s.Carbohidratos);
                    cmd.Parameters.AddWithValue("@Grasas", s.Grasas);
                    cmd.Parameters.AddWithValue("@Sabor", s.Sabor);
                    cmd.Parameters.AddWithValue("@EsPicante", s.EsPicante);
                    cmd.Parameters.AddWithValue("@EsZero", s.EsZero);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        
        // Delete
        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("DELETE FROM Salsa WHERE Id = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        
        // HELPER METHOD - Mapeo de DataReader a Salsa
        private Salsa MapReaderToSalsa(SqlDataReader reader)
        {
            return new Salsa
            {
                Id = Convert.ToInt32(reader["Id"]),
                // Abuelo
                Nombre = reader["Nombre"].ToString()!,
                Descripcion = reader["Descripcion"].ToString()!,
                Imagen = reader["Imagen"].ToString()!,
                Precio = Convert.ToDecimal(reader["Precio"]),
                Stock = Convert.ToInt32(reader["Stock"]),
                // Padre (Macros - Convertimos a double)
                Calorias = Convert.ToDouble(reader["Calorias"]),
                Proteinas = Convert.ToDouble(reader["Proteinas"]),
                Carbohidratos = Convert.ToDouble(reader["Carbohidratos"]),
                Grasas = Convert.ToDouble(reader["Grasas"]),
                // Hijo
                Sabor = reader["Sabor"].ToString()!,
                EsPicante = Convert.ToBoolean(reader["EsPicante"]),
                EsZero = Convert.ToBoolean(reader["EsZero"])
            };
        }
    }
}