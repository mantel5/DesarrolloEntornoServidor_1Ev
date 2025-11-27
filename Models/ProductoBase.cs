namespace SuplementosAPI.Models
{
    // Abstracta: No puedes vender un "Producto" genérico.
    public abstract class ProductoBase
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public string Imagen { get; set; } = "";
        public decimal Precio { get; set; }      
        public int Stock { get; set; }           

        public ProductoBase() { }

        // usamos un constructor protegido para que solo las clases derivadas puedan llamarlo
        protected ProductoBase(string nombre, string descripcion, string imagen, decimal precio, int stock)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("El nombre es obligatorio.");
            if (precio < 0) throw new ArgumentException("El precio no puede ser negativo.");
            if (stock < 0) throw new ArgumentException("El stock no puede ser negativo.");

            Nombre = nombre;
            Descripcion = descripcion;
            Imagen = imagen;
            Precio = precio;
            Stock = stock;
        }
    }
}