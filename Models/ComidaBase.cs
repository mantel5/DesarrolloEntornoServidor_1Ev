namespace SuplementosAPI.Models
{
    public abstract class ComidaBase : ProductoBase
    {
        // Nutrición por 100g
        public double Calorias { get; set; }
        public double Proteinas { get; set; }
        public double Carbohidratos { get; set; }
        public double Grasas { get; set; }

        public ComidaBase() { }

        protected ComidaBase(
            string nombre, decimal precio, int stock, string descripcion, string imagen, // Del Padre
            double calorias, double proteinas, double carbohidratos, double grasas) // Suyos
            : base(nombre, descripcion, imagen, precio, stock) // Enviamos al Padre
        {
            if (calorias < 0 || proteinas < 0 || carbohidratos < 0 || grasas < 0)
                throw new ArgumentException("Los macros no pueden ser negativos.");

            Calorias = calorias;
            Proteinas = proteinas;
            Carbohidratos = carbohidratos;
            Grasas = grasas;
        }
    }
}