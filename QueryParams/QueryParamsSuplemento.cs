namespace SuplementosAPI.QueryParams
{
    public class QueryParamsSuplemento : QueryParamsBase
    {
        // Filtros para 'PesoKg' (que está en SuplementoBase)
        public double? PesoMin { get; set; }
        public double? PesoMax { get; set; }
        
        // Nota: No ponemos filtro de 'Categoria' porque cada Controller
        // ya sabe qué categoría es (ej: el CreatinaController solo devuelve creatinas).
    }
}