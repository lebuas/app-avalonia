using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using corte2.Models;
using MsBox.Avalonia;

namespace corte2.ViewForm.FormSeries;

public partial class AsociarDisociarSerieActor : UserControl
{
    public AsociarDisociarSerieActor()
    {
        InitializeComponent();
        UpdateDataGrid();
    }


    //Carga el DataGrid que está en un UserControl
    //Contiene el nombre y código tanto de serie como de series.
    public void UpdateDataGrid()
    {
        GetDataGridSeriesActores.Child = new DatagridSerieActor();
    }

    async void AsociarSerieClick(object? sender, RoutedEventArgs e)
    {
        var textoActor = CodigoActorTextBox.Text;
        var textoSerie = CodigoSerieTextBox.Text;

        if ((int.TryParse(textoActor, out int codigoActor)) & (int.TryParse(textoSerie, out int codigoSerie)))
        {
            var vm = DataContext as MainViewModel;

            var resultadoOperacion = vm!.SerieAsocia(codigoSerie, codigoActor);
            if (resultadoOperacion)
            {
                await ShowMessage("Informacion",
                    $"$Operacion exitosa: Actor:{codigoActor} Asociado con Serie:{codigoSerie}");
            }
            else
            {
                await ShowMessage("Error", "Actor no no esta en la lista o ya esta en la serie.");
            }
        }
    }

    async void DisociarSerieClick(object? sender, RoutedEventArgs e)
    {
        var textoActor = CodigoActorTextBox.Text;
        var textoSerie = CodigoSerieTextBox.Text;

        if ((int.TryParse(textoActor, out int codigoActor)) & (int.TryParse(textoSerie, out int codigoSerie)))
        {
            var vm = DataContext as MainViewModel;

            var resultadoOperacion = vm!.SerieDisocia(codigoSerie, codigoActor);
            if (resultadoOperacion)
            {
                await ShowMessage("Informacion",
                    $"$Operacion exitosa: Actor:{codigoActor} Disociado de Serie:{codigoSerie}");
            }
            else
            {
                await ShowMessage("Error", "Actor no no esta en la lista o No esta en la serie.");
            }
        }
    }

    private void Salir(object? sender, RoutedEventArgs e)
    {
        var ventana = this.GetVisualRoot() as Window;
        ventana?.Close();
    }

    private async Task ShowMessage(string title, string message)
    {
        var box = MessageBoxManager.GetMessageBoxStandard(title, message);
        await box.ShowAsync();
    }
}