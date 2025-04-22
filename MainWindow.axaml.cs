using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using System.Linq;
using corte2.Models;
using corte2.ViewForm;
using System.Collections.Generic;
using Avalonia.VisualTree;
using corte2.ViewForm.FormActors;

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


        private void OnExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        // Abre la página del repositorio del proyecto
        private void GithubClick(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/lebuas")
            {
                UseShellExecute = true // Es necesario para abrir el navegador
            });
        }

//   Métodos para cargar los formularios UserControl de interaciones con Actores

        private void CargarFormularios(UserControl form)
        {
            var formulario = form;
            var formularioContainer = this.FindControl<Grid>("Formularios")!;
            formularioContainer.Children.Clear();
            formularioContainer.Children.Add(formulario);
        }

        private void OnInfoClick(object? sender, RoutedEventArgs e)
        {
            //
        }

        private void Editar_Actor_Click(object? sender, RoutedEventArgs e)
        {
            DataGridActores.IsReadOnly = false;
            DataGridActores.Columns[0].IsReadOnly = true;
            ButtonBloquearEdicionActores.IsVisible = true;
        }

        private void Bloquear_Edicion_Actor_Click(object? sender, RoutedEventArgs e)
        {
            DataGridActores.IsReadOnly = true;
            ButtonBloquearEdicionActores.IsVisible = false;
        }

        private void Eliminar_Actor_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Actor actor)
            {
                var vm = this.DataContext as MainViewModel;
                if (vm != null)
                {
                    vm.Actores.Remove(actor);
                    Console.WriteLine($"Actor eliminado: {actor.Nombre} {actor.Apellido}");
                }
            }
        }


        private void Buscar_Actor_Click(object? sender, RoutedEventArgs e)
        {
            {
                // Obtener el texto del TextBox
                var vm = this.DataContext as MainViewModel;
                string texto = BuscarActor.Text!;

                //convertir el texto del TextBox a un número
                if (int.TryParse(texto, out int codigo))
                {
                    // Buscar el actor con el código especificado
                    var actor = vm.Actores.FirstOrDefault(c => c.Codigo == codigo);

                    if (actor != null)
                    {
                        vm.Actores.Remove(actor);
                        vm.Actores.Insert(0, actor);
                        var index = vm.Actores.IndexOf(actor);
                        DataGridActores.SelectedIndex = index;
                        BuscarActor.Text = "";
                    }
                    else
                    {
                        Console.WriteLine("No se encontró un actor con ese código.");
                    }
                }
            }
        }

        private void Adicionar_Actor_Click(object? sender, RoutedEventArgs e)
        {
            var ventan = new WindowComodin(new AdicionarActors());
            ventan.DataContext = this.DataContext;
            ventan.Show();
        }

        private void Detalle_Actor_Click(object? sender, RoutedEventArgs e)
        {
            var series = new List<string>
            {
                "Breaking Bad",
                "Better Call Saul",
                "The Mandalorian",
                "The Last of Us"
            };
        }
    }
}