namespace SuplementosAPI.Models
{
    public class Salsa : ComidaBase
    {
        public string Sabor { get; set; } = "";
        public bool EsPicante { get; set; }
        public bool EsZero { get; set; }

        public Salsa() { }

        public Salsa(
            string nombre, decimal precio, int stock, string descripcion, string imagen, 
            double calorias, double proteinas, double carbohidratos, double grasas,
            string sabor, bool esPicante, bool esZero)
            : base(nombre, precio, stock, descripcion, imagen, calorias, proteinas, carbohidratos, grasas)
        {
            if (string.IsNullOrWhiteSpace(sabor)) throw new ArgumentException("Sabor obligatorio.");
            
            Sabor = sabor;
            EsPicante = esPicante;
            EsZero = esZero;
        }
    }
}