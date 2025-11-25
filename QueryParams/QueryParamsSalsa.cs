namespace SuplementosAPI.QueryParams
{
    public class QueryParamsSalsa : QueryParamsComida // <--- ¡Hereda de Comida!
    {
        public string? Sabor { get; set; }
        public bool? SoloZero { get; set; }    // Filtro para EsZero
        public bool? SoloPicante { get; set; } // Filtro para EsPicante
    }
}