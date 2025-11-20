using Models;

public class Proteina : SuplementoBase {

public string Formato {get;set;} = "";
public string Tipo {get;set;} = "";
public string Sabor {get;set;} = "";


public Proteina(string nombre, double precio, string descripcion, string categoria, double peso, string imagen, string formato, string tipo, string sabor): base(nombre, precio, descripcion, categoria, peso, imagen) {
    Formato = formato;
    Tipo = tipo;
    Sabor = sabor;
    
    if ( formato is null)
    {
        throw new ArgumentException("El formato no puede ser nulo");
    }

     if ( tipo is null)
    {
        throw new ArgumentException("El tipo no puede ser nulo");
    }
   
}

public Proteina() { }

}

