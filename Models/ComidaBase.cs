namespace Models;

public abstract class ComidaBase 
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public string Imagen { get; set; } = "";
    public decimal Precio { get; set; } = 0; 
    public double Calorias { get; set; } = 0.0;
    public double Proteinas { get; set; } = 0.0;
    public double Carbohidratos { get; set; } = 0.0;
    public double Grasas { get; set; } = 0.0;


    public ComidaBase() { }

    public ComidaBase(string nombre, string descripcion, string imagen, decimal precio, double calorias, double proteinas, double carbohidratos, double grasas) 
    {
        Nombre = nombre;
        Descripcion = descripcion;
        Imagen = imagen;
        Precio = precio;
        Calorias = calorias;
        Proteinas = proteinas;
        Carbohidratos = carbohidratos;
        Grasas = grasas;

        if (string.IsNullOrWhiteSpace(nombre)) {
            throw new ArgumentException("El nombre no puede estar vacío");
        }

        if (precio < 0) {
            throw new ArgumentException("El precio no puede ser negativo");
        }
        
        if (calorias < 0 || proteinas < 0 || carbohidratos < 0 || grasas < 0) {
            throw new ArgumentException("Los valores nutricionales no pueden ser negativos");
        }
    }
}