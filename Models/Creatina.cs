namespace SuplementosAPI.Models
{
    public class Creatina : SuplementoBase
    {
        public string Sabor { get; set; } = "";
        public string Tipo { get; set; } = ""; // Monohidrato...
        public string Formato { get; set; } = ""; // Polvo, Capsulas
        public bool SelloCreapure { get; set; }
        public bool EsMicronizada { get; set; }
        public int DosisDiariaGr { get; set; }

        public Creatina() { }

        public Creatina(
            string nombre, decimal precio, int stock, string descripcion, string imagen, string categoria, double pesoKg,
            string sabor, string tipo, string formato, bool selloCreapure, bool esMicronizada, int dosisDiariaGr)
            : base(nombre, precio, stock, descripcion, imagen, categoria, pesoKg)
        {
            if (string.IsNullOrWhiteSpace(sabor)) throw new ArgumentException("Sabor obligatorio.");
            if (dosisDiariaGr <= 0) throw new ArgumentException("Dosis inválida.");

            Sabor = sabor;
            Tipo = tipo;
            Formato = formato;
            SelloCreapure = selloCreapure;
            EsMicronizada = esMicronizada;
            DosisDiariaGr = dosisDiariaGr;
        }
    }
}