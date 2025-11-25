namespace SuplementosAPI.QueryParams
{
    public class QueryParamsCreatina : QueryParamsSuplemento
    {
        public string? Sabor { get; set; }
        public string? Tipo { get; set; }
        public string? Formato { get; set; }
        public bool? SoloCreapure { get; set; }
        public bool? SoloMicronizada { get; set; }
    }
}