namespace SuplementosAPI.Models
{
    public abstract class SuplementoBase : ProductoBase
    {
        public string Categoria { get; set; } = ""; // Ej: "Recuperación", "Energía"
        public double PesoKg { get; set; }          // Para calcular envíos

        public SuplementoBase() { }

        // Constructor que recibe lo suyo + lo del padre
        protected SuplementoBase(
            string nombre, decimal precio, int stock, string descripcion, string imagen, // Del Padre
            string categoria, double pesoKg) // Suyos
            : base(nombre, descripcion, imagen, precio, stock) // Enviamos al Padre
        {
            if (string.IsNullOrWhiteSpace(categoria)) throw new ArgumentException("La categoría es obligatoria.");
            if (pesoKg <= 0) throw new ArgumentException("El peso debe ser mayor a 0.");

            Categoria = categoria;
            PesoKg = pesoKg;
        }
    }
}