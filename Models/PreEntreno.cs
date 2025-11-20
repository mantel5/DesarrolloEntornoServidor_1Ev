using SuplementosAPI.Models;

namespace SuplementosAPI.Models
{
    public class PreEntreno : SuplementoBase 
    {
        public string Formato { get; set; } = ""; 
        public string Tipo { get; set; } = "";    
        public string Sabor { get; set; } = "";   
        public int MgCafeina { get; set; }        
        public bool TieneBetaAlanina { get; set; } 

        public PreEntreno() { }

        public PreEntreno(string nombre, decimal precio, int stock, string descripcion, double peso, string imagen, string formato, string tipo, string sabor, int mgCafeina, bool tieneBetaAlanina) 
            : base(nombre, precio, stock, descripcion, peso, imagen) 
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(formato)) 
                throw new ArgumentException("El formato es obligatorio.");

            if (string.IsNullOrWhiteSpace(tipo)) 
                throw new ArgumentException("El tipo es obligatorio.");

            if (string.IsNullOrWhiteSpace(sabor)) 
                throw new ArgumentException("El sabor es obligatorio.");

            if (mgCafeina < 0) 
            {
                throw new ArgumentException("La cafeína no puede ser negativa.");
            }

            Formato = formato;
            Tipo = tipo;
            Sabor = sabor;
            MgCafeina = mgCafeina;
            TieneBetaAlanina = tieneBetaAlanina;
        }
    }
}