namespace SuplementosAPI.Models
{
    public class Packs : SuplementoBase
    {
        public int ProteinaId { get; set; }
        public int PreEntrenoId { get; set; }
        public int CreatinaId { get; set; }
        public int BebidaId { get; set; }

        public ProductoBase? Proteina { get; set; }
        public ProductoBase? PreEntreno { get; set; }
        public ProductoBase? Creatina { get; set; }
        public ProductoBase? Bebida { get; set; }

        public Packs() { }

        // Constructor usado por SERVICE
        public Packs(
            string nombre,
            string descripcion,
            string imagen,
            string categoria,
            double pesoKg,
            int stock,
            ProductoBase proteina,
            ProductoBase preEntreno,
            ProductoBase creatina,
            ProductoBase bebida
        )
            : base(nombre, 0, stock, descripcion, imagen, categoria, pesoKg)
        {
            Proteina = proteina;
            PreEntreno = preEntreno;
            Creatina = creatina;
            Bebida = bebida;

            ProteinaId = proteina.Id;
            PreEntrenoId = preEntreno.Id;
            CreatinaId = creatina.Id;
            BebidaId = bebida.Id;

            Precio = CalcularPrecio();
        }

        // Constructor usado por REPOSITORY
        public Packs(
            string nombre,
            decimal precio,
            int stock,
            string descripcion,
            string imagen,
            string categoria,
            double pesoKg,
            int proteinaId,
            int preEntrenoId,
            int creatinaId,
            int bebidaId
        )
            : base(nombre, precio, stock, descripcion, imagen, categoria, pesoKg)
        {
            ProteinaId = proteinaId;
            PreEntrenoId = preEntrenoId;
            CreatinaId = creatinaId;
            BebidaId = bebidaId;
        }

        public decimal CalcularPrecio()
        {
            decimal precio = 0;
            if (Proteina != null) precio += Proteina.Precio;
            if (PreEntreno != null) precio += PreEntreno.Precio;
            if (Creatina != null) precio += Creatina.Precio;
            if (Bebida != null) precio += Bebida.Precio;
            return precio;
        }
    }
}
