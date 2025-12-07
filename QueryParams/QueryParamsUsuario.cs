namespace SuplementosAPI.QueryParams
{   
    // Clase para manejar los parámetros de consulta para usuarios
    public class QueryParamsUsuario
    {
        // Filtros
        public string? BuscarNombre { get; set; }
        public string? BuscarEmail { get; set; }
        public string? Rol { get; set; } 

        // Paginación 
        public int Pagina { get; set; } = 1;
        public int ElementosPorPagina { get; set; } = 10;
        
        // Ordenación
        public string? OrdenarPor { get; set; } 
    }
}