using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using corte2.Models;
using MsBox.Avalonia;

namespace corte2.ViewForm.FormSeries;

public partial class AsociarDisociarSerieActor : UserControl
{
    public AsociarDisociarSerieActor(string evento)
    {
        InitializeComponent();
        if (evento == "A")
        {
            UpdateDataGrid();
            AsociarForm.IsVisible = true;
        }
        if (evento == "D")
        {
            UpdateDataGrid();
            DisociarFrom.IsVisible = true;
        }
    }


    //Carga el DataGrid que está en un UserControl
    //Contiene el nombre y código tanto de serie como de series.
    public void UpdateDataGrid()
    {
        GetDataGridSeriesActores.Child = new DatagridSerieActor();
    }

    async void AsociarSerieClick(object? sender, RoutedEventArgs e)
    {
        AsociarForm.IsVisible = true;
        var textoActor = CodigoActorTextBox.Text;
        var textoSerie = CodigoSerieTextBox.Text;
        if (textoSerie != null && textoActor != null)
        {
            if ((int.TryParse(textoActor, out int codigoActor)) & (int.TryParse(textoSerie, out int codigoSerie)))
            {
                var vm = DataContext as MainViewModel;

                var resultadoOperacion = vm!.SerieAsocia(codigoSerie, codigoActor);
                if (resultadoOperacion)
                {
                    await ShowMessage("Información",
                        $"Operación exitosa: Actor:{codigoActor} Asociado con Serie:{codigoSerie}");

                    CodigoActorTextBox.Text = "";
                    CodigoSerieTextBox.Text = "";
                }
                else
                {
                    await ShowMessage("Información", "Actor no esta registrado o ya trabaja en la Serie.");
                }
            }
            else
            {
                await ShowMessage("Información", "Ingrese códigos de Actor y Serie");
            }
        }
    }

    async void DisociarSerieClick(object? sender, RoutedEventArgs e)
    {
        var textoActor = CodigoActorTextBox.Text;
        var textoSerie = CodigoSerieTextBox.Text;

        if (textoSerie != null && textoActor != null)
        {
            if ((int.TryParse(textoActor, out int codigoActor)) & (int.TryParse(textoSerie, out int codigoSerie)))
            {
                var vm = DataContext as MainViewModel;

                var resultadoOperacion = vm!.SerieDisocia(codigoSerie, codigoActor);
                if (resultadoOperacion)
                {
                    await ShowMessage("Información",
                        $"Operación exitosa: Actor:{codigoActor} Disociado de Serie:{codigoSerie}");
                    CodigoActorTextBox.Text = "";
                    CodigoSerieTextBox.Text = "";
                }
                else
                {
                    await ShowMessage("Error", "Actor no esta registrado o No trabaja en serie.");
                }
            }
        }
        else
        {
            await ShowMessage("Información", "Ingrese códigos de Actor y Serie");
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