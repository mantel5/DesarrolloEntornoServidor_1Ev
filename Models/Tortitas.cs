namespace SuplementosAPI.Models
{
    public class Tortitas : ComidaBase
    {
        public string Sabor { get; set; } = "";
        public string Tipo { get; set; } = ""; // Harina vs Preparado
        public int PesoGr { get; set; }
        public bool EsSinGluten { get; set; }

        public Tortitas() { }

        public Tortitas(
            string nombre, decimal precio, int stock, string descripcion, string imagen,
            double calorias, double proteinas, double carbohidratos, double grasas,
            string sabor, string tipo, int pesoGr, bool esSinGluten)
            : base(nombre, precio, stock, descripcion, imagen, calorias, proteinas, carbohidratos, grasas)
        {
            if (pesoGr <= 0) throw new ArgumentException("Peso en gramos debe ser mayor a 0.");
            
            Sabor = sabor;
            Tipo = tipo;
            PesoGr = pesoGr;
            EsSinGluten = esSinGluten;
        }
    }
}