namespace SuplementosAPI.QueryParams
{
    public class QueryParamsPacks
    {
        // Paginación
        public int Pagina { get; set; } = 1;
        public int ElementosPorPagina { get; set; } = 10;

        // Filtros Base
        public string? BuscarNombre { get; set; }
        public decimal? PrecioMin { get; set; }
        public decimal? PrecioMax { get; set; }

        // Filtros Suplemento
        public double? PesoMin { get; set; }
        public double? PesoMax { get; set; }

        // Ordenación
        public string? OrdenarPor { get; set; } 
    }
}