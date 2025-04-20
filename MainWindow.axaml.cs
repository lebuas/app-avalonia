using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using System.Linq;
using corte2.ViewForm.FormActors;
using Tmds.DBus.Protocol;

namespace corte2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void limpiarcelda(int row, int column)
        {
            object? elementosencelda;
            elementosencelda = MyGrid.Children
                .Where(el => Grid.GetRow(el) == row && Grid.GetColumn(el) == column)
                .Tolist();

            foreach (var elemento in elementosencelda)
            {
                MyGrid.Children.Remove(elemento);
            }
        }

        // Al hacer clic en el botón Actors, se abrirá el menú emergente (Popup)
        private void OnActorsClick(object sender, RoutedEventArgs e)
        {
            // Mostrar las opciones de actores y ocultar las de series
            Home.IsVisible = false;
            DataGridSeries.IsVisible = false;
            GridSeries.IsVisible = false;
            DataGridActores.IsVisible = true;
            GridActors.IsVisible = true;
        }

        private void OnSeriesClick(object sender, RoutedEventArgs e)
        {
            // Mostrar las opciones de series y ocultar las de actores
            Home.IsVisible = false;
            DataGridActores.IsVisible = false;
            GridActors.IsVisible = false;
            DataGridSeries.IsVisible = true;
            GridSeries.IsVisible = true;
        }

        private void OnHomeClik(object? sender, RoutedEventArgs e)
        {
            Home.IsVisible = true;
        }

        // Al hacer clic en el botón Salir, se cierra la aplicación
        private void OnExitClick(object sender, RoutedEventArgs e)
        {
            this.Close(); // Esto cerrará la ventana principal.
        }


        // Abre la página de Google en el navegador predeterminado
        private void GithubClick(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.google.com")
            {
                UseShellExecute = true // Es necesario para abrir el navegador
            });
        }

//   Metodos para cargar los formularios UserControl
        private void OnActorAddClick(object sender, RoutedEventArgs e)
        {
            var formulario = new AdicionarActors();
            var formularioContainer = this.FindControl<Grid>("FormActorAdd")!;
            formularioContainer.Children.Clear();
            formularioContainer.Children.Add(formulario);
        }


        private void OnActorsEditClick(object? sender, RoutedEventArgs e)
        {
            var formulario = new EditarActors();
            var formularioContainer = this.FindControl<Grid>("FormActorEdit")!;
            formularioContainer.Children.Clear();
            formularioContainer.Children.Add(formulario);
        }

        private void OnInfoClick(object? sender, RoutedEventArgs e)
        {
            //
        }
    }
}