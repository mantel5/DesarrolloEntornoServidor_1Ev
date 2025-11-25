namespace SuplementosAPI.QueryParams
{
    public class QueryParamsTortitas : QueryParamsComida
    {
        public string? Sabor { get; set; }
        public string? Tipo { get; set; } // Harina vs Preparado
        public bool? SoloSinGluten { get; set; }
    }
}