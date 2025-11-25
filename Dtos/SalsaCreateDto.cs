namespace SuplementosAPI.Dtos
{
    public class SalsaCreateDto
    {
        // --- Nivel 1: ProductoBase ---
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }

        // --- Nivel 2: ComidaBase (Macros) ---
        public double Calorias { get; set; }
        public double Proteinas { get; set; }
        public double Carbohidratos { get; set; }
        public double Grasas { get; set; }

        // --- Nivel 3: Salsa ---
        public string Sabor { get; set; }
        public bool EsPicante { get; set; }
        public bool EsZero { get; set; }
    }
}