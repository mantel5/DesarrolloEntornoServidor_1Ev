namespace SuplementosAPI.Dtos
{
    public class BebidaCreateDto
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

        // --- Nivel 3: Bebida ---
        public string Sabor { get; set; }
        public int Mililitros { get; set; }
        public bool TieneGluten { get; set; }
        public bool TieneGas { get; set; }
    }
}