namespace SuplementosAPI.Models
{
    public class Omega3 : SuplementoBase
    {
        public string Formato { get; set; } = "";
        public string Origen { get; set; } = "";
        public int MgEPA { get; set; }
        public int MgDHA { get; set; }
        public bool CertificadoIFOS { get; set; }

        public Omega3() { }

        public Omega3(
            string nombre, decimal precio, int stock, string descripcion, string imagen, string categoria, double pesoKg,
            string formato, string origen, int mgEPA, int mgDHA, bool certificadoIFOS)
            : base(nombre, precio, stock, descripcion, imagen, categoria, pesoKg)
        {
            if (mgEPA < 0 || mgDHA < 0) throw new ArgumentException("Valores mg inválidos.");

            Formato = formato;
            Origen = origen;
            MgEPA = mgEPA;
            MgDHA = mgDHA;
            CertificadoIFOS = certificadoIFOS;
        }
    }
}