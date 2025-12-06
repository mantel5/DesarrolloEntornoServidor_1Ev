namespace SuplementosAPI.Dtos
{
    public class PacksCreateDto
    {
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public string Categoria { get; set; } 
        public double PesoKg { get; set; }

        public int ProteinaId { get; set; }
        public int PreEntrenoId { get; set; }
        public int CreatinaId { get; set; }
        public int BebidaId { get; set; }
    }
}