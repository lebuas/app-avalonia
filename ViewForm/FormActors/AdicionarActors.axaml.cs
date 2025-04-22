using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Linq;
using corte2.Models;
namespace corte2.ViewForm.FormActors;

public partial class AdicionarActors : UserControl
{
    public AdicionarActors()
    {
        InitializeComponent();
    }

    private void OnGuardarClick(object? sender, RoutedEventArgs e)
    {
        var vm = DataContext as MainViewModel;
        if (vm == null)
        {
            Console.WriteLine("Error: No se pudo acceder al contexto de datos.");
            return;
        }

        // Validar y convertir Código
        if (!int.TryParse(CodigoTextBox.Text?.Trim(), out int codigo))
        {
            Console.WriteLine("Error: El código debe ser un número entero.");
            return;
        }

        string nombre = NombreTextBox.Text?.Trim() ?? "";
        string apellido = ApellidoTextBox.Text?.Trim() ?? "";
        string rol = RolTextBox.Text?.Trim() ?? "";
        string imdb = ImdbTextBox.Text?.Trim() ?? "";
        string edad = EdadlidadTextBox.Text?.Trim() ?? "";

        // Validar campos obligatorios
        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) || string.IsNullOrWhiteSpace(rol))
        {
            Console.WriteLine("Error: Nombre, Apellido y Rol son obligatorios.");
            return;
        }

        // Verificar código duplicado
        bool existe = vm.Actores.Any(a => a.Codigo == codigo);
        if (existe)
        {
            Console.WriteLine($"Error: El código '{codigo}' ya existe en la colección.");
            return;
        }

        // Crear y agregar actor
        vm.Actores.Add(new Actor(
            codigo,
            nombre,
            apellido,
            rol,
            imdb,
            edad
        ));


        Console.WriteLine($"Actor con código '{codigo}' agregado correctamente.");

        // Limpiar formulario
        CodigoTextBox.Text = "";
        NombreTextBox.Text = "";
        ApellidoTextBox.Text = "";
        RolTextBox.Text = "";
        ImdbTextBox.Text = "";
        EdadlidadTextBox.Text = "";
    }

    private void OnEliminarClick(object? sender, RoutedEventArgs e)
    {
        CodigoTextBox.Text = "";
        NombreTextBox.Text = "";
        ApellidoTextBox.Text = "";
        RolTextBox.Text = "";
        ImdbTextBox.Text = "";
        EdadlidadTextBox.Text = "";

        Console.WriteLine("Campos del formulario eliminados.");
    }
}