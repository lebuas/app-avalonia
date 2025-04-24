using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Linq;
using Avalonia.VisualTree;
using corte2.Models;
using MsBox.Avalonia;

namespace corte2.ViewForm.FormActors;

public partial class AdicionarActors : UserControl
{
    public AdicionarActors()
    {
        InitializeComponent();
    }


    private async System.Threading.Tasks.Task ShowMessage(string title, string message)
    {
        var box = MessageBoxManager.GetMessageBoxStandard(title, message);
        await box.ShowAsync();
    }
    
    
    // Realiaza las valiadaciones del formulario
    async void OnGuardarClick(object? sender, RoutedEventArgs e)
    {
        var vm = DataContext as MainViewModel;

        // Validar y convertir Código
        if (!int.TryParse(CodigoTextBox.Text?.Trim(), out int codigo))
        {
            await ShowMessage("Error", "El código debe ser un número entero.");
            return;
        }

        string nombre = NombreTextBox.Text?.Trim() ?? "";
        string imdb = UrlTextBox.Text?.Trim() ?? "";
        string pais = PaisTextBox.Text?.Trim() ?? "";
        string edadTexto = EdadTextBox.Text?.Trim() ?? "";
        string grammyTexto = GramyTextBox.Text?.Trim() ?? "";

        // Validar campos obligatorios
        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(imdb) ||
            string.IsNullOrWhiteSpace(pais) || string.IsNullOrWhiteSpace(edadTexto))
        {
            await ShowMessage("Error", "Nombre, País, Edad y URL IMDB son obligatorios.");
            return;
        }

        // Validar grammy
        if (!int.TryParse(grammyTexto, out int grammy))
        {
            await ShowMessage("Error", "El Grammy debe ser un número entero.");
            return;
        }

        // Validar edad
        if (!int.TryParse(edadTexto, out int edad))
        {
            await ShowMessage("Error", "La edad debe ser un número entero.");
            return;
        }

        // Llama a la funcioa ActorAdiciona y verifica si esta no no registrado
        var resutadoOperacion = vm.ActorAdiciona(codigo, nombre, imdb, pais, edad, grammy);
        if (!resutadoOperacion)
        {
            await ShowMessage("Error", $"El código '{codigo}' ya existe en la colección.");
        }
        else
        {
            await ShowMessage("Éxito", $"Actor con código '{codigo}' agregado correctamente.");

            // Limpiar formulario
            CodigoTextBox.Text = "";
            NombreTextBox.Text = "";
            PaisTextBox.Text = "";
            UrlTextBox.Text = "";
            EdadTextBox.Text = "";
            GramyTextBox.Text = "";
        }
    }
    
    private void OnSalirClick(object? sender, RoutedEventArgs e)
    {
        var ventana = this.GetVisualRoot() as Window;
        ventana?.Close();
    }
}