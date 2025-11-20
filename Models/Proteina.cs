using SuplementosAPI.Models;

public class Proteina : SuplementoBase {

public string Formato {get;set;} = "";
public string Tipo {get;set;} = "";
public string Sabor {get;set;} = "";


public Proteina(string nombre, decimal precio, int stock, string descripcion, double peso, string imagen, string formato, string tipo, string sabor): base(nombre, precio, stock, descripcion, peso, imagen) {
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

