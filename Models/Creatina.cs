using SuplementosAPI.Models;

namespace SuplementosAPI.Models
{
    public class Creatina : SuplementoBase
    {
        public string Formato { get; set; } = ""; 
        public string Tipo { get; set; } = "";  
        public string Sabor { get; set; } = "";
        public bool SelloCreapure { get; set; }   
        public bool EsMicronizada { get; set; }   
        public int DosisDiariaGr { get; set; }     

        public Creatina() { }

        public Creatina(string nombre, decimal precio, int stock, string descripcion, double peso, string imagen, string formato, string tipo, string sabor,bool selloCreapure, bool esMicronizada, int dosisDiariaGr) : base(nombre, precio, stock, descripcion, peso, imagen)
        {
            if (string.IsNullOrWhiteSpace(formato)) throw new ArgumentException("El formato es obligatorio.");
            if (string.IsNullOrWhiteSpace(tipo)) throw new ArgumentException("El tipo es obligatorio.");
            if (string.IsNullOrWhiteSpace(sabor)) throw new ArgumentException("El sabor es obligatorio.");

            if (dosisDiariaGr <= 0) 
            {
                throw new ArgumentException("La dosis diaria debe ser mayor a 0 gramos.");
            }

            Formato = formato;
            Tipo = tipo;
            Sabor = sabor;
            SelloCreapure = selloCreapure;
            EsMicronizada = esMicronizada;
            DosisDiariaGr = dosisDiariaGr;
        }
    }
}