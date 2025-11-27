namespace SuplementosAPI.QueryParams
{
    public class QueryParamsSalsa : QueryParamsComida 
    {
        public string? Sabor { get; set; }
        public bool? SoloZero { get; set; }   
        public bool? SoloPicante { get; set; } 
    }
}