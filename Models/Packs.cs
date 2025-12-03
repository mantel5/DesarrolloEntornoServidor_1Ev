namespace SuplementosAPI.Models
{
    public class Packs : SuplementoBase
    {
        public Proteina Proteina  { get; set; }
        public  PreEntreno PreEntreno { get; set; } 
        public  Creatina Creatina { get; set; }    
        public Bebida Bebida { get; set; }

        public decimal PrecioTotal { get; set; }

        public Packs() { }

        public Packs(Proteina proteina, PreEntreno preEntreno, Creatina creatina, Bebida bebida) {
            Proteina = proteina;
            PreEntreno = preEntreno;
            Creatina = creatina;
            Bebida = bebida;
            PrecioTotal =  CalcularPrecio();

        }
         private decimal CalcularPrecio() {
            decimal precio = Proteina.Precio + PreEntreno.Precio + Creatina.Precio + Bebida.Precio;
            return precio;
        }
    }
}


   
