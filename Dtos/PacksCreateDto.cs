using SuplementosAPI.Models;

 public class PacksCreateDto
 {
    public Proteina Proteina  { get; set; }
    public PreEntreno PreEntreno { get; set; }
    public PreEntreno Creatina { get; set; }
    public PreEntreno Bebida { get; set; }
    public double PrecioTotal { get; set; }
}

