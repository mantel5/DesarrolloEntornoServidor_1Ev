using Models;

namespace SuplementosAPI.Models
{
    public class Tortitas : ComidaBase
    {
        public string Sabor { get; set; } = "";   
        public string Tipo { get; set; } = "";     
        public int PesoGr { get; set; }           
        public bool EsSinGluten { get; set; } 

        public Tortitas() { }

        public Tortitas(string nombre, string descripcion, string imagen, decimal precio, double calorias, double proteinas, double carbohidratos, double grasas,string sabor, string tipo, int pesoGr, bool esSinGluten) : base(nombre, descripcion, imagen, precio, calorias, proteinas, carbohidratos, grasas) 
        {
            if (string.IsNullOrWhiteSpace(sabor)) 
            {
                throw new ArgumentException("El sabor es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(tipo)) 
            {
                throw new ArgumentException("El tipo es obligatorio (Harina o Preparado).");
            }

            if (pesoGr <= 0) 
            {
                throw new ArgumentException("El peso debe ser mayor que 0 gramos.");
            }

            Sabor = sabor;
            Tipo = tipo;
            PesoGr = pesoGr;
            EsSinGluten = esSinGluten;
        }
    }
}