namespace SuplementosAPI.QueryParams
{
    public class QueryParamsBebida : QueryParamsComida
    {
        public string? Sabor { get; set; }
        public bool? SoloSinGluten { get; set; }
        public bool? SoloConGas { get; set; }
        public int? MililitrosMax { get; set; } // Para limitar el tamaño
    }
}