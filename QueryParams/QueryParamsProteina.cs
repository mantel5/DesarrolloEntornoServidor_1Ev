namespace SuplementosAPI.QueryParams
{
    public class QueryParamsProteina : QueryParamsSuplemento
    {
        public string? Sabor { get; set; }
        public string? Tipo { get; set; }          // Whey, Isolate...
        public int? PorcentajeMinimo { get; set; } // "Mínimo 90% de pureza"
        public bool? SoloSinLactosa { get; set; }
    }
}