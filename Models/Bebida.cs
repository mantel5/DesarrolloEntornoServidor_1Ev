using System.Runtime.CompilerServices;
using Models;

public class Bebida : ComidaBase {

public string Sabores {get;set;} = "";
public int Cantidad  {get;set;} 
public bool Gluten   {get;set;} 



public Bebida(string nombre, double calorias, string descripcion, double precio, string imagen, string sabores, int cantidad, bool gluten ): base(nombre, calorias, descripcion, precio, imagen) {

    Sabores = sabores;
    Cantidad = cantidad;
    Gluten = gluten;
   

    if ( cantidad < 0)
    {
        throw new ArgumentException("La cantidad no puede ser 0");
    }

    if ( gluten is not true or false)
    {
        throw new ArgumentException("Se espera un si o no");
    }
   
}

public Bebida() { }

}
