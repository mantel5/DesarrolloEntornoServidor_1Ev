namespace SuplementosAPI.QueryParams
{
    public class QueryParamsSuplemento : QueryParamsBase
    {
        // Filtros para 'PesoKg' (que está en SuplementoBase)
        public double? PesoMin { get; set; }
        public double? PesoMax { get; set; }
        
        // no hace falta agregar un filtro por Categoria aquí, porque ya está en QueryParamsBase
        

        
    }
}