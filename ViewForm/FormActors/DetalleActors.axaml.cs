using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;

namespace corte2.ViewForm.FormActors;

public partial class DetalleActors : UserControl
{
    public DetalleActors()
    {
        InitializeComponent();
    }

    private void OnGuardarDetalle(object? sender, RoutedEventArgs e)
    {
        var series = new List<string>
        {
            "Breaking Bad",
            "Better Call Saul",
            "The Mandalorian",
            "The Last of Us"
        };
        
        SeriesList.ItemsSource = series;
    }
}