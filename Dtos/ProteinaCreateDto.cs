namespace SuplementosAPI.Dtos
{
    public class ProteinaCreateDto
    {
        // Nivel 1
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }

        // Nivel 2
        public string Categoria { get; set; }
        public double PesoKg { get; set; }

        // Nivel 3
        public string Sabor { get; set; }
        public string Tipo { get; set; }        
        public int Porcentaje { get; set; }     
        public bool EsSinLactosa { get; set; }
    }
}
