namespace SuplementosAPI.QueryParams
{
    public class QueryParamsProteina : QueryParamsSuplemento
    {
        public string? Sabor { get; set; }
        public string? Tipo { get; set; }          
        public int? PorcentajeMinimo { get; set; } 
        public bool? SoloSinLactosa { get; set; }
    }
}
