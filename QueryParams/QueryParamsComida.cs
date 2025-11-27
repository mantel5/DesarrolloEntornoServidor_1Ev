namespace SuplementosAPI.QueryParams
{
    public class QueryParamsComida : QueryParamsBase
    {
        // Filtros Nutricionales 
        public double? CaloriasMax { get; set; }
        
        // "Quiero algo con al menos 20g de proteína"
        public double? ProteinasMin { get; set; }
        
        // "Quiero algo bajo en grasa"
        public double? GrasasMax { get; set; }
        
        // "Quiero algo bajo en carbohidratos (Keto)"
        public double? CarbohidratosMax { get; set; }
    }
}
