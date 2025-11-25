
using System.Text; 
using Microsoft.Data.SqlClient;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Repositories
{
    public class CreatinaRepository : ICreatinaRepository
    {
        private readonly string _connectionString;

        public CreatinaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SuplementosDB") 
                ?? throw new Exception("Falta la cadena de conexión 'SuplementosDB' en appsettings.json");
        }

        
        public async Task AddAsync(Creatina c)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                    INSERT INTO Creatina 
                    (
                        -- Abuelo (ProductoBase)
                        Nombre, Descripcion, Imagen, Precio, Stock,
                        -- Padre (SuplementoBase)
                        Categoria, PesoKg,
                        -- Hijo (Creatina)
                        Sabor, Tipo, Formato, SelloCreapure, EsMicronizada, DosisDiariaGr
                    )
                    VALUES 
                    (
                        @Nombre, @Descripcion, @Imagen, @Precio, @Stock,
                        @Categoria, @PesoKg,
                        @Sabor, @Tipo, @Formato, @SelloCreapure, @EsMicronizada, @DosisDiariaGr
                    );";

                using (var cmd = new SqlCommand(query, connection))
                {
                    // Mapeo de parámetros
                    cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", c.Descripcion);
                    cmd.Parameters.AddWithValue("@Imagen", c.Imagen);
                    cmd.Parameters.AddWithValue("@Precio", c.Precio);
                    cmd.Parameters.AddWithValue("@Stock", c.Stock);
                    
                    cmd.Parameters.AddWithValue("@Categoria", c.Categoria);
                    cmd.Parameters.AddWithValue("@PesoKg", c.PesoKg);
                    
                    cmd.Parameters.AddWithValue("@Sabor", c.Sabor);
                    cmd.Parameters.AddWithValue("@Tipo", c.Tipo);
                    cmd.Parameters.AddWithValue("@Formato", c.Formato);
                    cmd.Parameters.AddWithValue("@SelloCreapure", c.SelloCreapure);
                    cmd.Parameters.AddWithValue("@EsMicronizada", c.EsMicronizada);
                    cmd.Parameters.AddWithValue("@DosisDiariaGr", c.DosisDiariaGr);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

    
        public async Task<Creatina?> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT * FROM Creatina WHERE Id = @Id";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapReaderToCreatina(reader);
                        }
                    }
                }
            }
            return null;
        }

       
        public async Task<List<Creatina>> GetAllAsync(QueryParamsCreatina filtros)
        {
            var lista = new List<Creatina>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Construcción dinámica de SQL
                var queryBuilder = new StringBuilder("SELECT * FROM Creatina WHERE 1=1");
                var cmd = new SqlCommand();

                // --- FILTROS BASE (ProductoBase) ---
                if (!string.IsNullOrWhiteSpace(filtros.BuscarNombre))
                {
                    queryBuilder.Append(" AND Nombre LIKE @Nombre");
                    cmd.Parameters.AddWithValue("@Nombre", $"%{filtros.BuscarNombre}%");
                }
                if (filtros.PrecioMin.HasValue)
                {
                    queryBuilder.Append(" AND Precio >= @PrecioMin");
                    cmd.Parameters.AddWithValue("@PrecioMin", filtros.PrecioMin.Value);
                }
                if (filtros.PrecioMax.HasValue)
                {
                    queryBuilder.Append(" AND Precio <= @PrecioMax");
                    cmd.Parameters.AddWithValue("@PrecioMax", filtros.PrecioMax.Value);
                }

                // --- FILTROS PADRE (SuplementoBase) ---
                if (filtros.PesoMin.HasValue)
                {
                    queryBuilder.Append(" AND PesoKg >= @PesoMin");
                    cmd.Parameters.AddWithValue("@PesoMin", filtros.PesoMin.Value);
                }
                if (filtros.PesoMax.HasValue)
                {
                    queryBuilder.Append(" AND PesoKg <= @PesoMax");
                    cmd.Parameters.AddWithValue("@PesoMax", filtros.PesoMax.Value);
                }

                // --- FILTROS HIJO (Creatina) ---
                if (!string.IsNullOrWhiteSpace(filtros.Sabor))
                {
                    // Búsqueda exacta para desplegables
                    queryBuilder.Append(" AND Sabor = @Sabor"); 
                    cmd.Parameters.AddWithValue("@Sabor", filtros.Sabor);
                }
                if (!string.IsNullOrWhiteSpace(filtros.Tipo))
                {
                    queryBuilder.Append(" AND Tipo = @Tipo");
                    cmd.Parameters.AddWithValue("@Tipo", filtros.Tipo);
                }
                if (!string.IsNullOrWhiteSpace(filtros.Formato))
                {
                    queryBuilder.Append(" AND Formato = @Formato");
                    cmd.Parameters.AddWithValue("@Formato", filtros.Formato);
                }
                if (filtros.SoloCreapure == true)
                {
                    queryBuilder.Append(" AND SelloCreapure = 1");
                }
                if (filtros.SoloMicronizada == true)
                {
                    queryBuilder.Append(" AND EsMicronizada = 1");
                }

                // --- ORDENACIÓN ---
                if (!string.IsNullOrWhiteSpace(filtros.OrdenarPor))
                {
                    switch (filtros.OrdenarPor.ToLower())
                    {
                        case "precio_asc": queryBuilder.Append(" ORDER BY Precio ASC"); break;
                        case "precio_desc": queryBuilder.Append(" ORDER BY Precio DESC"); break;
                        case "nombre": queryBuilder.Append(" ORDER BY Nombre ASC"); break;
                        default: queryBuilder.Append(" ORDER BY Id ASC"); break;
                    }
                }
                else
                {
                    queryBuilder.Append(" ORDER BY Id ASC"); // Necesario para paginación
                }

                // --- PAGINACIÓN ---
                int saltar = (filtros.Pagina - 1) * filtros.ElementosPorPagina;
                queryBuilder.Append(" OFFSET @Saltar ROWS FETCH NEXT @Tomar ROWS ONLY");
                cmd.Parameters.AddWithValue("@Saltar", saltar);
                cmd.Parameters.AddWithValue("@Tomar", filtros.ElementosPorPagina);

                // Ejecución
                cmd.CommandText = queryBuilder.ToString();
                cmd.Connection = connection;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(MapReaderToCreatina(reader));
                    }
                }
            }
            return lista;
        }

        public async Task UpdateAsync(Creatina c)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"
                    UPDATE Creatina SET 
                        Nombre = @Nombre, 
                        Descripcion = @Descripcion, 
                        Imagen = @Imagen, 
                        Precio = @Precio, 
                        Stock = @Stock,
                        Categoria = @Categoria, 
                        PesoKg = @PesoKg,
                        Sabor = @Sabor, 
                        Tipo = @Tipo, 
                        Formato = @Formato, 
                        SelloCreapure = @SelloCreapure, 
                        EsMicronizada = @EsMicronizada, 
                        DosisDiariaGr = @DosisDiariaGr
                    WHERE Id = @Id";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", c.Id);
                    cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", c.Descripcion);
                    cmd.Parameters.AddWithValue("@Imagen", c.Imagen);
                    cmd.Parameters.AddWithValue("@Precio", c.Precio);
                    cmd.Parameters.AddWithValue("@Stock", c.Stock);
                    cmd.Parameters.AddWithValue("@Categoria", c.Categoria);
                    cmd.Parameters.AddWithValue("@PesoKg", c.PesoKg);
                    cmd.Parameters.AddWithValue("@Sabor", c.Sabor);
                    cmd.Parameters.AddWithValue("@Tipo", c.Tipo);
                    cmd.Parameters.AddWithValue("@Formato", c.Formato);
                    cmd.Parameters.AddWithValue("@SelloCreapure", c.SelloCreapure);
                    cmd.Parameters.AddWithValue("@EsMicronizada", c.EsMicronizada);
                    cmd.Parameters.AddWithValue("@DosisDiariaGr", c.DosisDiariaGr);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

      
        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Creatina WHERE Id = @Id";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

    
        private Creatina MapReaderToCreatina(SqlDataReader reader)
        {
            // Usamos el constructor vacío para asignar propiedades manualmente
            return new Creatina
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
                Tipo = reader["Tipo"].ToString()!,
                Formato = reader["Formato"].ToString()!,
                SelloCreapure = Convert.ToBoolean(reader["SelloCreapure"]),
                EsMicronizada = Convert.ToBoolean(reader["EsMicronizada"]),
                DosisDiariaGr = Convert.ToInt32(reader["DosisDiariaGr"])
            };
        }
    }
}