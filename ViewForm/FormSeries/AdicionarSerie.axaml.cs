using Avalonia.Controls;
using Avalonia.Interactivity;
using corte2.Models;
using MsBox.Avalonia;
using Avalonia.VisualTree;

namespace corte2.ViewForm.FormSeries
{
    public partial class AdicionarSerie : UserControl
    {
        public AdicionarSerie()
        {
            InitializeComponent();
        }

        private async System.Threading.Tasks.Task ShowMessage(string title, string message)
        {
            var box = MessageBoxManager.GetMessageBoxStandard(title, message);
            await box.ShowAsync();
        }

        // Validaciones del formulario
        private async void OnGuardarClick(object? sender, RoutedEventArgs e)
        {
            var vm = DataContext as MainViewModel;
            if (!int.TryParse(CodigoTextBox.Text?.Trim(), out int codigo))
            {
                await ShowMessage("Error", "El código debe ser un número entero.");
                return;
            }

            string titulo = NombreTextBox.Text?.Trim() ?? "";
            string imdb = UrlTextBox.Text?.Trim() ?? "";
            string anioTexto = AnioEstrenoTextBox.Text?.Trim() ?? "";
            string genero = GeneroTextBox.Text?.Trim() ?? "";
            string temporadaTexto = TemporadaTextBox.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(imdb) ||
                string.IsNullOrWhiteSpace(anioTexto) || string.IsNullOrWhiteSpace(genero) ||
                string.IsNullOrWhiteSpace(temporadaTexto))
            {
                await ShowMessage("Error", "Todos los campos son obligatorios.");
                return;
            }

            if (!int.TryParse(anioTexto, out int anio))
            {
                await ShowMessage("Error", "El año debe ser un número entero.");
                return;
            }

            if (!int.TryParse(temporadaTexto, out int temporadas))
            {
                await ShowMessage("Error", "La temporada debe ser un número entero.");
                return;
            }

            //Llamar el metodo SerieAdiciona y crea una nueva serie si su codigo no esta repetido
            var resutadoOperacion = vm!.SerieAdiciona(codigo, titulo, imdb, anio, genero, temporadas);
            if (!resutadoOperacion)
            {
                await ShowMessage("Error", $"El código '{codigo}' ya está registrado.");
            }
            else
            {
                await ShowMessage("Éxito", $"Serie '{titulo}' agregada correctamente.");
                // Limpiar campos
                CodigoTextBox.Text = "";
                NombreTextBox.Text = "";
                UrlTextBox.Text = "";
                AnioEstrenoTextBox.Text = "";
                GeneroTextBox.Text = "";
                TemporadaTextBox.Text = "";
            }
        }

        private void OnSalirClick(object? sender, RoutedEventArgs e)
        {
            var ventana = this.GetVisualRoot() as Window;
            ventana?.Close();
        }
    }
}