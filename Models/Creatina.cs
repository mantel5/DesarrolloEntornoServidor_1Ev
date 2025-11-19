using Models;

public class Creatina : SuplementoBase {

public bool Premium {get;set;}
public string Formato {get;set;} = "";
public string Tipos {get;set;} = "";
public string Sabores {get;set;} = "";


public Creatina(string nombre, double precio, string descripcion, string categoria, double peso, string imagen, bool premium, string formato, string tipos, string sabores ): base(nombre, precio, descripcion, categoria, peso, imagen) {
    Premium = premium;
    Formato = formato;
    Tipos = tipos;
    Sabores = sabores;
   
}

public Creatina() { }
public override void MostrarDetalles() {
    Console.WriteLine($"Creatina: {Nombre}, Precio {Precio:C}, Descripción {Descripcion} ");
}
}