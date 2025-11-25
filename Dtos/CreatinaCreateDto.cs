namespace SuplementosAPI.Dtos
{
    public class CreatinaCreateDto
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

        // --- Nivel 3: Creatina ---
        public string Sabor { get; set; }
        public string Tipo { get; set; }        // Monohidrato...
        public string Formato { get; set; }     // Polvo...
        public bool SelloCreapure { get; set; }
        public bool EsMicronizada { get; set; }
        public int DosisDiariaGr { get; set; }
    }
}