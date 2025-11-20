using Models;

public class PreEntreno : SuplementoBase {

public string Formato {get;set;} = "";
public string Tipo {get;set;} = "";
public string Sabor {get;set;} = "";
public int MgCafeina {get;set;}


public PreEntreno(string nombre, double precio, string descripcion, string categoria, double peso, string imagen, string formato, string tipo, string sabor, int mgCafeina): base(nombre, precio, descripcion, categoria, peso, imagen) {
    Formato = formato;
    Tipo = tipo;
    Sabor = sabor;
    MgCafeina = mgCafeina;
    
    if ( formato is null)
    {
        throw new ArgumentException("El formato no puede ser nulo");
    }

     if ( tipo is null)
    {
        throw new ArgumentException("El tipo no puede ser nulo");
    }

     if ( mgCafeina < 0)
    {
        throw new ArgumentException("Los miligramos no pueden ser 0");
    }
}

public PreEntreno() { }

}

