using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SuplementosAPI.Models;
using SuplementosAPI.QueryParams;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuplementosAPI.Repositories
{
    public class PacksRepository : IPacksRepository
    {
        private readonly string _connectionString;

        public PacksRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SuplementosDB")
                 ?? throw new Exception("Falta la cadena de conexión 'SuplementosDB' en appsettings.json");
        }

        private Packs MapReaderToPacks(SqlDataReader reader)
        {
            return new Packs(
                nombre: reader["Nombre"].ToString()!,
                precio: Convert.ToDecimal(reader["Precio"]),
                stock: Convert.ToInt32(reader["Stock"]),
                descripcion: reader["Descripcion"].ToString()!,
                imagen: reader["Imagen"].ToString()!,
                categoria: reader["Categoria"].ToString()!,
                pesoKg: Convert.ToDouble(reader["PesoKg"]),
                proteinaId: Convert.ToInt32(reader["ProteinaId"]),
                preEntrenoId: Convert.ToInt32(reader["PreEntrenoId"]),
                creatinaId: Convert.ToInt32(reader["CreatinaId"]),
                bebidaId: Convert.ToInt32(reader["BebidaId"])
            )
            {
                Id = Convert.ToInt32(reader["Id"])
            };
        }

        public async Task<int> AddAsync(Packs p)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = @"
                INSERT INTO Packs 
                (Nombre, Descripcion, Imagen, Precio, Stock, Categoria, PesoKg, ProteinaId, PreEntrenoId, CreatinaId, BebidaId)
                VALUES 
                (@Nombre, @Descripcion, @Imagen, @Precio, @Stock, @Categoria, @PesoKg, @ProteinaId, @PreEntrenoId, @CreatinaId, @BebidaId);
                SELECT CAST(scope_identity() AS INT);";

            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Nombre", p.Nombre);
            cmd.Parameters.AddWithValue("@Descripcion", p.Descripcion);
            cmd.Parameters.AddWithValue("@Imagen", p.Imagen);
            cmd.Parameters.AddWithValue("@Precio", p.Precio);
            cmd.Parameters.AddWithValue("@Stock", p.Stock);
            cmd.Parameters.AddWithValue("@Categoria", p.Categoria);
            cmd.Parameters.AddWithValue("@PesoKg", p.PesoKg);
            cmd.Parameters.AddWithValue("@ProteinaId", p.ProteinaId);
            cmd.Parameters.AddWithValue("@PreEntrenoId", p.PreEntrenoId);
            cmd.Parameters.AddWithValue("@CreatinaId", p.CreatinaId);
            cmd.Parameters.AddWithValue("@BebidaId", p.BebidaId);

            return (int)await cmd.ExecuteScalarAsync();
        }

        public async Task<Packs?> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = "SELECT * FROM Packs WHERE Id = @Id";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return MapReaderToPacks(reader);
            }

            return null;
        }

        public async Task<List<Packs>> GetAllAsync(QueryParamsPacks filtros)
        {
            var lista = new List<Packs>();
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = "SELECT * FROM Packs ORDER BY Id ASC";
            using var cmd = new SqlCommand(query, connection);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(MapReaderToPacks(reader));
            }

            return lista;
        }

        public async Task UpdateAsync(Packs p)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = @"
                UPDATE Packs SET 
                    Nombre = @Nombre, 
                    Descripcion = @Descripcion, 
                    Imagen = @Imagen, 
                    Precio = @Precio, 
                    Stock = @Stock,
                    Categoria = @Categoria, 
                    PesoKg = @PesoKg,
                    ProteinaId = @ProteinaId, 
                    PreEntrenoId = @PreEntrenoId, 
                    CreatinaId = @CreatinaId, 
                    BebidaId = @BebidaId
                WHERE Id = @Id";

            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", p.Id);
            cmd.Parameters.AddWithValue("@Nombre", p.Nombre);
            cmd.Parameters.AddWithValue("@Descripcion", p.Descripcion);
            cmd.Parameters.AddWithValue("@Imagen", p.Imagen);
            cmd.Parameters.AddWithValue("@Precio", p.Precio);
            cmd.Parameters.AddWithValue("@Stock", p.Stock);
            cmd.Parameters.AddWithValue("@Categoria", p.Categoria);
            cmd.Parameters.AddWithValue("@PesoKg", p.PesoKg);
            cmd.Parameters.AddWithValue("@ProteinaId", p.ProteinaId);
            cmd.Parameters.AddWithValue("@PreEntrenoId", p.PreEntrenoId);
            cmd.Parameters.AddWithValue("@CreatinaId", p.CreatinaId);
            cmd.Parameters.AddWithValue("@BebidaId", p.BebidaId);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = "DELETE FROM Packs WHERE Id = @Id";
            using var cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
