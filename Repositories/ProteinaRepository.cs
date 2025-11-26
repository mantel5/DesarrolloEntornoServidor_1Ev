using System.Text;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using Microsoft.Data.SqlClient;


namespace SuplementosAPI.Repositories
{
    public class ProteinaRepository : IProteinaRepository
    {
        private readonly string _connectionString;

        public ProteinaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SuplementosDB") 
                ?? throw new Exception("Falta conexión en appsettings");
        }

        public async Task AddAsync(Proteina p)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    INSERT INTO Proteina 
                    (Nombre, Descripcion, Imagen, Precio, Stock, Categoria, PesoKg, 
                     Sabor, Tipo, Porcentaje, EsSinLactosa)
                    VALUES 
                    (@Nombre, @Descripcion, @Imagen, @Precio, @Stock, @Categoria, @PesoKg, 
                     @Sabor, @Tipo, @Porcentaje, @EsSinLactosa);";

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
                    
                    // Específicos Proteína
                    cmd.Parameters.AddWithValue("@Sabor", p.Sabor);
                    cmd.Parameters.AddWithValue("@Tipo", p.Tipo);
                    cmd.Parameters.AddWithValue("@Porcentaje", p.Porcentaje);
                    cmd.Parameters.AddWithValue("@EsSinLactosa", p.EsSinLactosa);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<Proteina?> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("SELECT * FROM Proteina WHERE Id = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) return MapReaderToProteina(reader);
                    }
                }
            }
            return null;
        }

        public async Task<List<Proteina>> GetAllAsync(QueryParamsProteina filtros)
        {
            var lista = new List<Proteina>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sb = new StringBuilder("SELECT * FROM Proteina WHERE 1=1");
                var cmd = new SqlCommand();

                // --- Filtros Comunes ---
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
                // (Puedes añadir PrecioMin, PesoMin, PesoMax aquí igual que en Creatina)

                // --- Filtros Específicos PROTEÍNA ---
                if (!string.IsNullOrWhiteSpace(filtros.Tipo))
                {
                    sb.Append(" AND Tipo = @Tipo");
                    cmd.Parameters.AddWithValue("@Tipo", filtros.Tipo);
                }
                if (filtros.PorcentajeMinimo.HasValue)
                {
                    sb.Append(" AND Porcentaje >= @PorcMin"); // ¡Mayor o igual!
                    cmd.Parameters.AddWithValue("@PorcMin", filtros.PorcentajeMinimo.Value);
                }
                if (filtros.SoloSinLactosa == true)
                {
                    sb.Append(" AND EsSinLactosa = 1");
                }

                // Ordenación y Paginación
                sb.Append(" ORDER BY Id ASC"); // Simplificado para el ejemplo
                int saltar = (filtros.Pagina - 1) * filtros.ElementosPorPagina;
                sb.Append(" OFFSET @Saltar ROWS FETCH NEXT @Tomar ROWS ONLY");
                cmd.Parameters.AddWithValue("@Saltar", saltar);
                cmd.Parameters.AddWithValue("@Tomar", filtros.ElementosPorPagina);

                cmd.CommandText = sb.ToString();
                cmd.Connection = connection;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync()) lista.Add(MapReaderToProteina(reader));
                }
            }
            return lista;
        }

        // Faltarían UpdateAsync y DeleteAsync (son iguales que Creatina pero cambiando columnas)
        public async Task UpdateAsync(Proteina p) { /* ... Igual que Creatina ... */ await Task.CompletedTask; }
        public async Task DeleteAsync(int id) 
        {
             using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("DELETE FROM Proteina WHERE Id = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private Proteina MapReaderToProteina(SqlDataReader reader)
        {
            return new Proteina
            {
                Id = Convert.ToInt32(reader["Id"]),
                Nombre = reader["Nombre"].ToString()!,
                Descripcion = reader["Descripcion"].ToString()!,
                Imagen = reader["Imagen"].ToString()!,
                Precio = Convert.ToDecimal(reader["Precio"]),
                Stock = Convert.ToInt32(reader["Stock"]),
                Categoria = reader["Categoria"].ToString()!,
                PesoKg = Convert.ToDouble(reader["PesoKg"]),
                // Específicos
                Sabor = reader["Sabor"].ToString()!,
                Tipo = reader["Tipo"].ToString()!,
                Porcentaje = Convert.ToInt32(reader["Porcentaje"]),
                EsSinLactosa = Convert.ToBoolean(reader["EsSinLactosa"])
            };
        }
    }
}