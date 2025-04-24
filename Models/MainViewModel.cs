/*
 Sistema de Gestión de Actores y Serie
 Integrante1: Leymar Buenaventura Asprilla
 Integrante2: Juan Gernando Cano
 Integrante3:
 *
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace corte2.Models;

public class MainViewModel

{
    public ObservableCollection<Actor> Actores { get; }
    public ObservableCollection<Serie> Series { get; }


    public MainViewModel()
    {
        // Inicialización de las colecciones con elementos predeterminados
        // Simulación de la persistencia

        Actores = new ObservableCollection<Actor>
        {
            // Carga un listado de actores y actrices

            new Actor(78, "Ana María Orozco", "nm0650450", "Colombia", 40, 1),
            new Actor(81, "Laura Londoño", "nm2256810", "Colombia", 37, 1),
            new Actor(84, "Carolina Ramírez", "nm1329835", "México", 39, 1),
            new Actor(93, "Catherine Siachoque", "nm0796171", "Colombia", 45, 1),
            new Actor(98, "Carmenza González", "nm1863990", "Venezuela", 57, 1),
            new Actor(99, "Andrés Londoño", "nm2150265", "Colombia", 34, 1),
            new Actor(102, "Manuel José Chaves", "nm0332951", "España", 43, 1),
            new Actor(105, "Marcela Carvajal", "nm0973920", "Colombia", 54, 1),
            new Actor(108, "Sebastián Martínez", "nm1458821", "Argentina", 41, 1),
            new Actor(112, "Paola Rey", "nm1340592", "Colombia", 45, 1),
            new Actor(115, "Diego Cadavid", "nm1023045", "Colombia", 47, 1),
            new Actor(119, "Natalia Reyes", "nm2004573", "Colombia", 38, 1),
            new Actor(123, "Juan Pablo Raba", "nm1024758", "Colombia", 48, 1),
            new Actor(126, "Juliana Galvis", "nm1448209", "Colombia", 41, 1),
            new Actor(130, "Andrés Parra", "nm1728149", "Colombia", 47, 1),
            new Actor(134, "Majida Issa", "nm1802746", "México", 43, 1)
        };
        Series = new ObservableCollection<Serie>
        {
            // Caraga Un listado de series

            new Serie(16, "Yo soy Betty, la fea", "tt0233127", 2016, "comendia", 5),
            new Serie(43, "La reina del flow", "tt8560918", 2016, "comendia", 5),
            new Serie(60, "Café con Aroma de Mujer", "tt14471346", 2016, "comendia", 5),
            new Serie(62, "Los Briceño", "tt10348478", 2016, "comendia", 5),
            new Serie(70, "Distrito Salvaje", "tt8105958", 2016, "comendia", 5),
            new Serie(72, "Mil Colmillos", "tt9701670", 2016, "comendia", 5),
            new Serie(83, "Perdida", "tt10064124", 2016, "comendia", 5),
            new Serie(90, "El Robo del Siglo", "tt11454460", 2020, "drama", 1),
            new Serie(94, "Frontera Verde", "tt10580064", 2019, "suspenso", 1),
            new Serie(97, "La Venganza de Analía", "tt12404264", 2014, "drama", 1),
            new Serie(103, "Pálpito", "tt14875660", 2020, "thriller", 2),
            new Serie(106, "La Primera Vez", "tt21828972", 2014, "romance", 1),
            new Serie(109, "Goles en Contra", "tt21620894", 2016, "drama", 1),
            new Serie(113, "Diomedes, el Cacique de La Junta", "tt4402620", 2015, "biografía", 1),
            new Serie(117, "Las Villamizar", "tt19782920", 2014, "acción", 1),
            new Serie(120, "La Ley del Corazón", "tt6314572", 2016, "drama", 2),
            new Serie(125, "Bolívar: Una lucha admirable", "tt9378850", 2019, "historia", 1),
            new Serie(129, "Siempre Bruja", "tt8021824", 2019, "fantasía", 2),
        };


        //Obsérvese que un mismo actor o actriz puede
        //estar en dos series distintas
        Series[0].Actor.Add(78);
        Series[0].Actor.Add(93);
        Series[0].Actor.Add(98);
        Series[6].Actor.Add(78);
        Series[4].Actor.Add(78);
    }

    //Adicionar actor
    public bool ActorAdiciona(int codigo, string nombre, string url, string pais, int edad, int grammy)
    {
        for (int cont = 0; cont < Actores.Count; cont++)
        {
            if (Actores[cont].Codigo == codigo)
                return false;
        }

        Actores.Add(new Actor(codigo, nombre, url, pais, edad, grammy));
        return true;
    }

    //Adicionar serie
    public bool SerieAdiciona(int codigo, string titulo, string urlimdb, int anioEstreno, string genero, int temporada)
    {
        for (int cont = 0; cont < Series.Count; cont++)
        {
            if (Series[cont].Codigo == codigo)
                return false;
        }

        Series.Add(new Serie(codigo, titulo, urlimdb, anioEstreno, genero, temporada));
        return true;
    }


    //Dado el código de la serie, retorna su posición
    public int PosSerie(int codigoSerie)
    {
        for (int cont = 0; cont < Series.Count; cont++)
            if (Series[cont].Codigo == codigoSerie)
            {
                return cont;
            }

        return -1;
    }

    //Detalle Retorna una lista de series donde el actor trabaja
    public List<string> ActorTrabaja(int codigoActor)
    {
        List<string> listaSeries = [];
        for (int cont = 0; cont < Series.Count; cont++)
        {
            for (int num = 0; num < Series[cont].Actor.Count; num++)
            {
                if (Series[cont].Actor[num] == codigoActor)
                    listaSeries.Add(Series[cont].Titulo);
            }
        }

        return listaSeries;
    }

    //Dado el código, retorna el nombre del actor/actriz
    public string NombreActor(int codigoActor)
    {
        for (int cont = 0; cont < Actores.Count; cont++)
        {
            if (Actores[cont].Codigo == codigoActor)
                return Actores[cont].Nombre;
        }

        return "N/A";
    }

    //Retornar los actores que trabajan en la serie
    public List<string> SerieActores(int codigoSerie)
    {
        int pos = PosSerie(codigoSerie);
        List<string> nombres = [];
        for (int cont = 0; cont < Series[pos].Actor.Count; cont++)
            nombres.Add("[" + Series[pos].Actor[cont] + "] " + NombreActor(Series[pos].Actor[cont]));
        return nombres;
    }

//Añade un actor a una serie
    public bool SerieAsocia(int codigoSerie, int codigoActor)
    {
        int posSerial = PosSerie(codigoSerie);
        if (posSerial >= 0)
        {
            if (Series[posSerial].Actor.Contains(codigoActor) == false)
            {
                Series[posSerial].Actor.Add(codigoActor);
                return true;
            }
            else
                return false;
        }

        return false;
    }

    //Quita un actor de una serie
    public bool SerieDisocia(int codigoSerie, int codigoActor)
    {
        int posSerial = PosSerie(codigoSerie);
        if (posSerial >= 0)
        {
            if (Series[posSerial].Actor.Contains(codigoActor))
            {
                Series[posSerial].Actor.Remove(codigoActor);
                return true;
            }
            else
                return false;
        }

        return false;
    }
}