using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace corte2.ViewForm.FormActors;

public partial class AdicionarActors : UserControl
{
    public AdicionarActors()
    {
        InitializeComponent();
    }

    private void OnGuardarClick(object? sender, RoutedEventArgs e)
    {
        // Obtener los valores de los TextBox
        string codigo = CodigoTextBox.Text ?? "";
        string nombre = NombreTextBox.Text ?? "";
        

        // Validar y mostrar en consola
        Console.WriteLine($"Código: {codigo}");
        Console.WriteLine($"Nombre: {nombre}");
        
    }

    private void OnEliminarClick(object? sender, RoutedEventArgs e)
    {
        // También puedes borrar los campos si quieres
        CodigoTextBox.Text = "";
        NombreTextBox.Text = "";
        

        Console.WriteLine("Campos eliminados");
    }
}
