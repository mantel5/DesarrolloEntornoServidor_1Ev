namespace SuplementosAPI.QueryParams
{
    public class QueryParamsOmega3 : QueryParamsSuplemento
    {
        public string? Formato { get; set; }
        public string? Origen { get; set; }
        public int? MgEPAMin { get; set; }
        public int? MgDHAMin { get; set; }
        public bool? SoloCertificadoIFOS { get; set; }
    }
}