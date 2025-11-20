using Models;

namespace SuplementosAPI.Models
{
    public class Bebida : ComidaBase 
    {
        public string Sabor { get; set; } = ""; 
        public int Mililitros { get; set; }     
        public bool TieneGluten { get; set; }   
        public bool TieneGas { get; set; }  

        public Bebida() { }

        public Bebida(string nombre, string descripcion, string imagen, decimal precio, double calorias, double proteinas, double carbohidratos, double grasas,string sabor, int mililitros, bool tieneGluten, bool tieneGas) : base(nombre, descripcion, imagen, precio, calorias, proteinas, carbohidratos, grasas) 
        {
            if (string.IsNullOrWhiteSpace(sabor)) 
            {
                throw new ArgumentException("El sabor es obligatorio.");
            }

            if (mililitros <= 0) 
            {
                throw new ArgumentException("Los mililitros deben ser mayor que 0.");
            }

            Sabor = sabor;
            Mililitros = mililitros;
            TieneGluten = tieneGluten;
            TieneGas = tieneGas;
        }
    }
}