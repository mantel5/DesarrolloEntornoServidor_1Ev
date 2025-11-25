namespace SuplementosAPI.Models
{
    public class Proteina : SuplementoBase
    {
        public string Sabor { get; set; } = "";
        public string Tipo { get; set; } = ""; // Whey, Isolate...
        public int Porcentaje { get; set; }    // Pureza (ej: 90)
        public bool EsSinLactosa { get; set; }

        public Proteina() { }

        public Proteina(
            string nombre, decimal precio, int stock, string descripcion, string imagen, string categoria, double pesoKg,
            string sabor, string tipo, int porcentaje, bool esSinLactosa)
            : base(nombre, precio, stock, descripcion, imagen, categoria, pesoKg)
        {
            if (string.IsNullOrWhiteSpace(sabor)) throw new ArgumentException("Sabor obligatorio.");
            if (string.IsNullOrWhiteSpace(tipo)) throw new ArgumentException("Tipo obligatorio.");
            if (porcentaje <= 0 || porcentaje > 100) throw new ArgumentException("Porcentaje inválido.");

            Sabor = sabor;
            Tipo = tipo;
            Porcentaje = porcentaje;
            EsSinLactosa = esSinLactosa;
        }
    }
}