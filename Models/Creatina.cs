using Models;

public class Creatina : SuplementoBase {

public bool Premium {get;set;}
public string Formato {get;set;} = "";
public string Tipo {get;set;} = "";
public string Sabores {get;set;} = "";


public Creatina(string nombre, double precio, string descripcion, string categoria, double peso, string imagen, bool premium, string formato, string tipos, string sabores ): base(nombre, precio, descripcion, categoria, peso, imagen) {
    Premium = premium;
    Formato = formato;
    Tipo = tipos;
    Sabores = sabores;

    if ( formato is null)
    {
        throw new ArgumentException("El formato no puede ser nulo");
    }

     if ( tipos is null)
    {
        throw new ArgumentException("El tipo no puede ser nulo");
    }
   
}

public Creatina() { }

}
