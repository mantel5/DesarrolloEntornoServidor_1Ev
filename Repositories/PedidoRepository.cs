using System.Data;
using Microsoft.Data.SqlClient;
using SuplementosAPI.Models;

namespace SuplementosAPI.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly string _connectionString;

        // Inyectamos la configuración igual que en UsuarioRepository
        public PedidoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SuplementosDB") 
                ?? throw new Exception("Falta conexión");
        }

        public async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            var lista = new List<Pedido>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                // OJO: Asegúrate de que los nombres de columnas coinciden con tu base de datos
                string query = "SELECT Id, Fecha, Total, UsuarioId FROM Pedido";

                using (var cmd = new SqlCommand(query, connection))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Pedido
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Fecha = Convert.ToDateTime(reader["Fecha"]),
                                Total = Convert.ToDecimal(reader["Total"]),
                                UsuarioId = Convert.ToInt32(reader["UsuarioId"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public async Task AddAsync(Pedido pedido)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Pedido (Fecha, Total, UsuarioId) VALUES (@Fecha, @Total, @UsuarioId)";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Fecha", pedido.Fecha);
                    cmd.Parameters.AddWithValue("@Total", pedido.Total);
                    cmd.Parameters.AddWithValue("@UsuarioId", pedido.UsuarioId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}