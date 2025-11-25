namespace SuplementosAPI.QueryParams
{
    public class QueryParamsComida : QueryParamsBase
    {
        // Filtros Nutricionales (¡Esto es oro para una tienda fitness!)
        // "Quiero algo con menos de 50 kcal"
        public double? CaloriasMax { get; set; }
        
        // "Quiero algo con al menos 20g de proteína"
        public double? ProteinasMin { get; set; }
        
        // "Quiero algo bajo en grasa"
        public double? GrasasMax { get; set; }
        
        // "Quiero algo bajo en carbohidratos (Keto)"
        public double? CarbohidratosMax { get; set; }
    }
}