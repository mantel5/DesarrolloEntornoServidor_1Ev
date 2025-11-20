namespace SuplementosAPI.Models

{
    public abstract class SuplementoBase
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public decimal Precio { get; set; } = 0;
        public int Stock { get; set; } = 0; 
        public string Descripcion { get; set; } = "";
        public double Peso { get; set; } = 0.0;
        public string Imagen { get; set; } = ""; 

        public SuplementoBase() { }

        public SuplementoBase(string nombre, decimal precio, int stock, string descripcion, double peso, string imagen)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("El nombre no puede estar vacío.");
            if (precio < 0) throw new ArgumentException("El precio no puede ser negativo.");
            if (stock < 0) throw new ArgumentException("El stock no puede ser negativo.");
            if (peso < 0) throw new ArgumentException("El peso no puede ser negativo.");

            Nombre = nombre;
            Precio = precio;
            Stock = stock;
            Descripcion = descripcion;
            Peso = peso;
            Imagen = imagen;
        }
    }
}