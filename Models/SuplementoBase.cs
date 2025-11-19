namespace Models;

public abstract class SuplementoBase {

    public int Id  {get;set;}
    public string Nombre {get;set;} = "";
    public double Precio {get;set;} = 0.0;
    public string Descripcion {get;set;} = "";
    public string Categoria {get;set;} = "";
    public double Peso { get; set; } = 0.0;
    public string Imagen {get;set;} = "";


    public SuplementoBase(){}

    public SuplementoBase(string nombre, double precio, string descripcion, string categoria, double peso, string imagen) {
        Nombre = nombre;
        Precio = precio;
        Descripcion = descripcion;
        Categoria = categoria;
        Peso = peso;
        Imagen = imagen;

        if (nombre is null) {
            throw new ArgumentException("El nombre no puede ser nulo");
        }

        if (precio < 0) {
            throw new ArgumentException("El precio no puede ser negativo");
        }

        if (categoria is null) {
            throw new ArgumentException("La categoría no puede ser nulo");
        }

        if (peso < 0) {
            throw new ArgumentException("El peso no puede ser negativo");
        }
    }

    public abstract void MostrarDetalles();
 
}
