namespace SuplementosAPI.Dtos
{
    public class Omega3CreateDto
    {
        // --- Nivel 1 ---
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }

        // --- Nivel 2 ---
        public string Categoria { get; set; }
        public double PesoKg { get; set; }

        // --- Nivel 3: Omega3 ---
        public string Formato { get; set; }
        public string Origen { get; set; }
        public int MgEPA { get; set; }
        public int MgDHA { get; set; }
        public bool CertificadoIFOS { get; set; }
    }
}