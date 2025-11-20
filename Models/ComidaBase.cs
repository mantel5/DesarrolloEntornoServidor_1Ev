namespace Models;

public abstract class ComidaBase {

    public int Id  {get;set;}
    public string Nombre {get;set;} = "";
    public double Calorias {get;set;} = 0.0;
    public string Descripcion {get;set;} = "";
    public double Precio {get;set;} = 0.0;
    public string Imagen {get;set;} = "";


    public ComidaBase(){}

    public ComidaBase(string nombre, double calorias, string descripcion, double precio, string imagen) {
        Nombre = nombre;
        Calorias = calorias;
        Descripcion = descripcion;
        Precio = precio;
        Imagen = imagen;

        if (nombre is null) {
            throw new ArgumentException("El nombre no puede ser nulo");
        }

        if (precio < 0) {
            throw new ArgumentException("El precio no puede ser negativo");
        }

    }
 
}
