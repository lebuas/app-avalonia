using System;
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
            Process.Start(new ProcessStartInfo("https://github.com/lebuas")
            {
                UseShellExecute = true // Es necesario para abrir el navegador
            });
        }

//   Métodos para cargar los formularios UserControl

        private void CargarFormularios(UserControl form)
        {
            var formulario = form;
            var formularioContainer = this.FindControl<Grid>("Formularios")!;
            formularioContainer.Children.Clear();
            formularioContainer.Children.Add(formulario);
            
        }
        private void OnActorAddClick(object sender, RoutedEventArgs e)
        {
           CargarFormularios(new AdicionarActors());
        }

        private void OnActorsEditClick(object? sender, RoutedEventArgs e)
        {
            CargarFormularios(new EditarActors());
        }

        private void OnInfoClick(object? sender, RoutedEventArgs e)
        {
            //
        }

        private void OnActorsBorrarClic(object? sender, RoutedEventArgs e)
        {
            CargarFormularios(new BorrarActors());
        }

        private void OnActorsDetalleClick(object? sender, RoutedEventArgs e)
        {
            CargarFormularios(new DetalleActors());
        }
    }
}