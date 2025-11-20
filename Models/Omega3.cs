using SuplementosAPI.Models;

namespace SuplementosAPI.Models
{
    public class Omega3 : SuplementoBase 
    {
        public string Formato { get; set; } = ""; 
        public string Origen { get; set; } = "";  
        public int MgEPA { get; set; } 
        public int MgDHA { get; set; } 
        public bool CertificadoIFOS { get; set; } 

        public Omega3() { }

        public Omega3(string nombre, decimal precio, int stock, string descripcion, double peso, string imagen, string formato, string origen, int mgEPA, int mgDHA, bool certificadoIFOS): base(nombre, precio, stock, descripcion, peso, imagen) 
        {
            if (string.IsNullOrWhiteSpace(formato)) 
                throw new ArgumentException("El formato es obligatorio.");
            
            if (string.IsNullOrWhiteSpace(origen)) 
                throw new ArgumentException("El origen es obligatorio.");

            if (mgEPA < 0) throw new ArgumentException("Los mg de EPA no pueden ser negativos.");
            if (mgDHA < 0) throw new ArgumentException("Los mg de DHA no pueden ser negativos.");

            Formato = formato;
            Origen = origen;
            MgEPA = mgEPA;
            MgDHA = mgDHA;
            CertificadoIFOS = certificadoIFOS;
        }
    }
}