using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;

namespace SuplementosAPI.Repositories
{
    public class Omega3Repository : IOmega3Repository
    {
        private readonly string _connectionString;

        public Omega3Repository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SuplementosDB") 
                ?? throw new Exception("Falta conexión en appsettings");
        }


        public async Task AddAsync(Omega3 o)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    INSERT INTO Omega3 
                    (
                        -- Abuelo (ProductoBase)
                        Nombre, Descripcion, Imagen, Precio, Stock,
                        -- Padre (SuplementoBase)
                        Categoria, PesoKg,
                        -- Hijo (Omega3)
                        Formato, Origen, MgEPA, MgDHA, CertificadoIFOS
                    )
                    VALUES 
                    (
                        @Nombre, @Descripcion, @Imagen, @Precio, @Stock,
                        @Categoria, @PesoKg,
                        @Formato, @Origen, @MgEPA, @MgDHA, @CertificadoIFOS
                    );";

                using (var cmd = new SqlCommand(query, connection))
                {
                    // Mapeo masivo
                    cmd.Parameters.AddWithValue("@Nombre", o.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", o.Descripcion);
                    cmd.Parameters.AddWithValue("@Imagen", o.Imagen);
                    cmd.Parameters.AddWithValue("@Precio", o.Precio);
                    cmd.Parameters.AddWithValue("@Stock", o.Stock);
                    
                    cmd.Parameters.AddWithValue("@Categoria", o.Categoria);
                    cmd.Parameters.AddWithValue("@PesoKg", o.PesoKg);
                    
                    cmd.Parameters.AddWithValue("@Formato", o.Formato);
                    cmd.Parameters.AddWithValue("@Origen", o.Origen);
                    cmd.Parameters.AddWithValue("@MgEPA", o.MgEPA);
                    cmd.Parameters.AddWithValue("@MgDHA", o.MgDHA);
                    cmd.Parameters.AddWithValue("@CertificadoIFOS", o.CertificadoIFOS);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<Omega3?> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("SELECT * FROM Omega3 WHERE Id = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) return MapReaderToOmega3(reader);
                    }
                }
            }
            return null;
        }


        public async Task<List<Omega3>> GetAllAsync(QueryParamsOmega3 filtros)
        {
            var lista = new List<Omega3>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sb = new StringBuilder("SELECT * FROM Omega3 WHERE 1=1");
                var cmd = new SqlCommand();

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

                if (filtros.PesoMax.HasValue)
                {
                    sb.Append(" AND PesoKg <= @PesoMax");
                    cmd.Parameters.AddWithValue("@PesoMax", filtros.PesoMax.Value);
                }

                
                if (!string.IsNullOrWhiteSpace(filtros.Formato))
                {
                    sb.Append(" AND Formato = @Formato");
                    cmd.Parameters.AddWithValue("@Formato", filtros.Formato);
                }
                
                if (!string.IsNullOrWhiteSpace(filtros.Origen))
                {
                    sb.Append(" AND Origen = @Origen");
                    cmd.Parameters.AddWithValue("@Origen", filtros.Origen);
                }

                if (filtros.MgEPAMin.HasValue)
                {
                    sb.Append(" AND MgEPA >= @EpaMin");
                    cmd.Parameters.AddWithValue("@EpaMin", filtros.MgEPAMin.Value);
                }
                if (filtros.MgDHAMin.HasValue)
                {
                    sb.Append(" AND MgDHA >= @DhaMin");
                    cmd.Parameters.AddWithValue("@DhaMin", filtros.MgDHAMin.Value);
                }

                if (filtros.SoloCertificadoIFOS == true)
                {
                    sb.Append(" AND CertificadoIFOS = 1");
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
                    while (await reader.ReadAsync()) lista.Add(MapReaderToOmega3(reader));
                }
            }
            return lista;
        }

        public async Task UpdateAsync(Omega3 o)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = @"
                    UPDATE Omega3 SET 
                        Nombre=@Nombre, Descripcion=@Descripcion, Imagen=@Imagen, Precio=@Precio, Stock=@Stock,
                        Categoria=@Categoria, PesoKg=@PesoKg,
                        Formato=@Formato, Origen=@Origen, MgEPA=@MgEPA, MgDHA=@MgDHA, CertificadoIFOS=@CertificadoIFOS
                    WHERE Id = @Id";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", o.Id);
                    cmd.Parameters.AddWithValue("@Nombre", o.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", o.Descripcion);
                    cmd.Parameters.AddWithValue("@Imagen", o.Imagen);
                    cmd.Parameters.AddWithValue("@Precio", o.Precio);
                    cmd.Parameters.AddWithValue("@Stock", o.Stock);
                    cmd.Parameters.AddWithValue("@Categoria", o.Categoria);
                    cmd.Parameters.AddWithValue("@PesoKg", o.PesoKg);
                    cmd.Parameters.AddWithValue("@Formato", o.Formato);
                    cmd.Parameters.AddWithValue("@Origen", o.Origen);
                    cmd.Parameters.AddWithValue("@MgEPA", o.MgEPA);
                    cmd.Parameters.AddWithValue("@MgDHA", o.MgDHA);
                    cmd.Parameters.AddWithValue("@CertificadoIFOS", o.CertificadoIFOS);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("DELETE FROM Omega3 WHERE Id = @Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private Omega3 MapReaderToOmega3(SqlDataReader reader)
        {
            return new Omega3
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
                Formato = reader["Formato"].ToString()!,
                Origen = reader["Origen"].ToString()!,
                MgEPA = Convert.ToInt32(reader["MgEPA"]),
                MgDHA = Convert.ToInt32(reader["MgDHA"]),
                CertificadoIFOS = Convert.ToBoolean(reader["CertificadoIFOS"])
            };
        }
    }
}