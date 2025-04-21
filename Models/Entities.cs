namespace corte2.Models;

// Clase Actor
public class Actor
{
    public int Codigo { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Rol { get; set; }
    public string Serie { get; set; }
    public string Edad { get; set; }

    // Constructor para inicializar las propiedades
    public Actor(int codigo, string nombre, string apellido, string rol, string serie, string edad)
    {
        Codigo = codigo;
        Nombre = nombre;
        Apellido = apellido;
        Rol = rol;
        Serie = serie;
        Edad = edad;
    }
}

// Clase Serie
public class Serie
{
    public string Titulo { get; set; }
    public string Genero { get; set; }
    public string Año { get; set; }
    public string Plataforma { get; set; }
    public string Temporadas { get; set; }

    // Constructor para inicializar las propiedades
    public Serie(string titulo, string genero, string año, string plataforma, string temporadas)
    {
        Titulo = titulo;
        Genero = genero;
        Año = año;
        Plataforma = plataforma;
        Temporadas = temporadas;
    }
}