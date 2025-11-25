namespace SuplementosAPI.Dtos
{
    public class TortitasCreateDto
    {
        // --- Nivel 1 ---
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }

        // --- Nivel 2 (Macros) ---
        public double Calorias { get; set; }
        public double Proteinas { get; set; }
        public double Carbohidratos { get; set; }
        public double Grasas { get; set; }

        // --- Nivel 3: Tortitas ---
        public string Sabor { get; set; }
        public string Tipo { get; set; } // Harina vs Preparado
        public int PesoGr { get; set; }
        public bool EsSinGluten { get; set; }
    }
}