namespace SuplementosAPI.Dtos
{
    public class ProteinaCreateDto
    {
        // --- Nivel 1: ProductoBase ---
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }

        // --- Nivel 2: SuplementoBase ---
        public string Categoria { get; set; }
        public double PesoKg { get; set; }

        // --- Nivel 3: Proteina ---
        public string Sabor { get; set; }
        public string Tipo { get; set; }        // Whey, Isolate...
        public int Porcentaje { get; set; }     // Pureza
        public bool EsSinLactosa { get; set; }
    }
}