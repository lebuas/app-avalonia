/*
 Sistema de Gestión de Actores y Serie
 Integrante1: Leymar Buenaventura Asprilla
 Integrante2: Juan Gernando Cano
 Integrante3: 
 * 
 */

using System.Collections.Generic;
using System;

namespace corte2.Models;

//Datos del actor o actriz
public class Actor
{
    public int Codigo { get; set; }
    public string Nombre { get; set; }
    public string URLIMDB { get; set; }
    public  string Pais { get; set; }
    public int Edad { get; set; }
    public int Grammy { get; set; }


    //Constructor
    public Actor(int codigo, string nombre, string urlimdb, string pais, int edad, int grammy)
    {
        this.Codigo = codigo;
        this.Nombre = nombre;
        this.URLIMDB = "https://www.imdb.com/name/" + urlimdb;
        this.Pais = pais;
        this.Edad = edad;
        this.Grammy = grammy;
    }
}

//Datos de la serie de televisión
public class Serie
{
    public int Codigo { get; set; }
    public string Titulo { get; set; }
    public string URLIMDB { get; set; }
    public int AnioEstreno { get; set; }
    public string Genero { get; set; }
    public int Temporada { get; set; }

    //Listado de códigos de actores que actúan en la serie
    public List<int> Actor = [];

    //Constructor
    public Serie(int codigo, string titulo, string urlimdb, int anioEstreno, string genero, int temporada)
    {
        this.Codigo = codigo;
        this.Titulo = titulo;
        this.URLIMDB = "https://www.imdb.com/title/" + urlimdb;
        this.AnioEstreno = anioEstreno;
        this.Genero = genero;
        this.Temporada = temporada;
    }
}