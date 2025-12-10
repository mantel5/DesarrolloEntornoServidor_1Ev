using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SuplementosAPI.Models;

namespace SuplementosAPI.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly string _connectionString;

        public PedidoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SuplementosDB") 
                ?? throw new Exception("Falta la cadena de conexión 'SuplementosDB'");
        }

       
        public async Task AddAsync(Pedido pedido)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // transacción para asegurar integridad
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // insertamos la tabla Pedido y obtenemos su Id
                        string queryPedido = @"
                            INSERT INTO Pedido (UsuarioId, Fecha, Total, Estado) 
                            VALUES (@UsuarioId, @Fecha, @Total, @Estado);
                            SELECT SCOPE_IDENTITY();"; 

                        int nuevoPedidoId;

                        using (var cmd = new SqlCommand(queryPedido, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@UsuarioId", pedido.UsuarioId);
                            cmd.Parameters.AddWithValue("@Fecha", pedido.Fecha);
                            cmd.Parameters.AddWithValue("@Total", pedido.Total);
                            cmd.Parameters.AddWithValue("@Estado", pedido.Estado);

                            var result = await cmd.ExecuteScalarAsync();
                            if (result != null)
                            {
                                nuevoPedidoId = Convert.ToInt32(result);
                                pedido.Id = nuevoPedidoId;
                            }
                            else
                            {
                                throw new Exception("No se pudo obtener el ID del pedido.");
                            }
                        }

                        // insertamos la tabla LineaPedido y asociamos al Pedido
                        string queryLinea = @"
                            INSERT INTO LineaPedido 
                            (PedidoId, ProductoNombre, Cantidad, PrecioUnitario, Subtotal, ProductoIdOriginal, TipoProductoOriginal)
                            VALUES 
                            (@PedidoId, @ProdNombre, @Cant, @Precio, @Subtotal, @ProdId, @Tipo)";

                        foreach (var linea in pedido.Lineas)
                        {
                            using (var cmdLinea = new SqlCommand(queryLinea, connection, transaction))
                            {
                                cmdLinea.Parameters.AddWithValue("@PedidoId", nuevoPedidoId);
                                cmdLinea.Parameters.AddWithValue("@ProdNombre", linea.ProductoNombre);
                                cmdLinea.Parameters.AddWithValue("@Cant", linea.Cantidad);
                                cmdLinea.Parameters.AddWithValue("@Precio", linea.PrecioUnitario);
                                cmdLinea.Parameters.AddWithValue("@Subtotal", linea.Subtotal);
                                cmdLinea.Parameters.AddWithValue("@ProdId", linea.ProductoIdOriginal);
                                cmdLinea.Parameters.AddWithValue("@Tipo", linea.TipoProductoOriginal);

                                await cmdLinea.ExecuteNonQueryAsync();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // en caso de error, deshacemos todo
                        transaction.Rollback();
                        throw; 
                    }
                }
            }
        }

        // GetById
        public async Task<Pedido?> GetByIdAsync(int id)
        {
            Pedido? pedido = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // primero leemos la cabecera
                string queryPedido = "SELECT * FROM Pedido WHERE Id = @Id";
                using (var cmd = new SqlCommand(queryPedido, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            pedido = new Pedido
                            {
                                Id = (int)reader["Id"],
                                UsuarioId = (int)reader["UsuarioId"],
                                Fecha = (DateTime)reader["Fecha"],
                                Total = (decimal)reader["Total"],
                                Estado = reader["Estado"].ToString()!
                            };
                        }
                    }
                }

                if (pedido == null) return null;

                // luego leemos las líneas asociadas
                string queryLineas = "SELECT * FROM LineaPedido WHERE PedidoId = @Id";
                using (var cmd = new SqlCommand(queryLineas, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var linea = new LineaPedido
                            {
                                Id = (int)reader["Id"],
                                PedidoId = (int)reader["PedidoId"],
                                ProductoNombre = reader["ProductoNombre"].ToString()!,
                                Cantidad = (int)reader["Cantidad"],
                                PrecioUnitario = (decimal)reader["PrecioUnitario"],
                                Subtotal = (decimal)reader["Subtotal"],
                                ProductoIdOriginal = (int)reader["ProductoIdOriginal"],
                                TipoProductoOriginal = reader["TipoProductoOriginal"].ToString()!
                            };
                            pedido.Lineas.Add(linea);
                        }
                    }
                }
            }
            return pedido;
        }

        // GetByUsuarioId
        public async Task<List<Pedido>> GetByUsuarioIdAsync(int usuarioId)
        {
            var lista = new List<Pedido>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                string query = "SELECT * FROM Pedido WHERE UsuarioId = @Uid ORDER BY Fecha DESC";
                
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Uid", usuarioId);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Pedido
                            {
                                Id = (int)reader["Id"],
                                UsuarioId = (int)reader["UsuarioId"],
                                Fecha = (DateTime)reader["Fecha"],
                                Total = (decimal)reader["Total"],
                                Estado = reader["Estado"].ToString()!
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}