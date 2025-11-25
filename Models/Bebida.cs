namespace SuplementosAPI.Models
{
    public class Bebida : ComidaBase
    {
        public string Sabor { get; set; } = "";
        public int Mililitros { get; set; }
        public bool TieneGluten { get; set; }
        public bool TieneGas { get; set; }

        public Bebida() { }

        public Bebida(
            string nombre, decimal precio, int stock, string descripcion, string imagen,
            double calorias, double proteinas, double carbohidratos, double grasas,
            string sabor, int mililitros, bool tieneGluten, bool tieneGas)
            : base(nombre, precio, stock, descripcion, imagen, calorias, proteinas, carbohidratos, grasas)
        {
            if (mililitros <= 0) throw new ArgumentException("Mililitros debe ser mayor a 0.");
            
            Sabor = sabor;
            Mililitros = mililitros;
            TieneGluten = tieneGluten;
            TieneGas = tieneGas;
        }
    }
}