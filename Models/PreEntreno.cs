namespace SuplementosAPI.Models
{
    public class PreEntreno : SuplementoBase
    {
        public string Formato { get; set; } = "";
        public string Tipo { get; set; } = ""; // Pump vs Estimulante
        public string Sabor { get; set; } = "";
        public int MgCafeina { get; set; }
        public bool TieneBetaAlanina { get; set; }

        public PreEntreno() { }

        public PreEntreno(
            string nombre, decimal precio, int stock, string descripcion, string imagen, string categoria, double pesoKg,
            string formato, string tipo, string sabor, int mgCafeina, bool tieneBetaAlanina)
            : base(nombre, precio, stock, descripcion, imagen, categoria, pesoKg)
        {
            if (mgCafeina < 0) throw new ArgumentException("Cafeína no puede ser negativa.");

            Formato = formato;
            Tipo = tipo;
            Sabor = sabor;
            MgCafeina = mgCafeina;
            TieneBetaAlanina = tieneBetaAlanina;
        }
    }
}