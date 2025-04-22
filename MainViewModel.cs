using System.Collections.ObjectModel;
using corte2.Models;

namespace corte2
{
    public class MainViewModel

    {
    public ObservableCollection<Actor> Actores { get; }
    public ObservableCollection<Serie> Series { get; }
    

    public MainViewModel()
    {
        // Inicialización de las colecciones con elementos predeterminados
        Actores = new ObservableCollection<Actor>
        {
            new Actor(20, "Keanu", "Reeves", "Neo", "Matrix", "60"),
            new Actor(30, "Bryan", "Cranston", "Walter", "Breaking Bad", "65"),
            new Actor(50, "Ellen", "Page", "Juno", "Juno", "35"),
            new Actor(60, "Robert", "Downey Jr.", "Iron Man", "Marvel", "56")
        };

        Series = new ObservableCollection<Serie>
        {
            new Serie(20, "Matrix", "Ciencia ficción", "1999", "Warner Bros", "3"),
            new Serie(30, "Breaking Bad", "Crimen", "2008", "AMC", "5")
        };

        // Agregar un actor y una serie después de la inicialización
        Actores.Add(new Actor(15, "Tom", "Hanks", "Forrest Gump", "Forrest Gump", "65"));
        Series.Add(new Serie(20,"Stranger Things", "Ciencia ficción", "2016", "Netflix", "4"));
    }
    }
}