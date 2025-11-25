namespace SuplementosAPI.QueryParams
{
    public class QueryParamsBase
    {
        // Paginación
        public int Pagina { get; set; } = 1;
        public int ElementosPorPagina { get; set; } = 10;

        // Filtros Globales (ProductoBase)
        public string? BuscarNombre { get; set; }
        public decimal? PrecioMin { get; set; }
        public decimal? PrecioMax { get; set; }
        
        // Ordenación
        public string? OrdenarPor { get; set; }
    }
}