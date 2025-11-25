namespace SuplementosAPI.QueryParams
{
    public class QueryParamsPreEntreno : QueryParamsSuplemento
    {
        public string? Formato { get; set; }
        public string? Tipo { get; set; } // Estimulante vs Pump
        public string? Sabor { get; set; }
        public int? MgCafeinaMin { get; set; }
        public bool? SoloConBetaAlanina { get; set; }
    }
}