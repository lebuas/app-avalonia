using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using System.Linq;
using corte2.Models;
using corte2.ViewForm;
using System.Collections.Generic;
using System.Threading.Tasks;
using corte2.ViewForm.FormActors;
using corte2.ViewForm.FormSeries;
using MsBox.Avalonia;


namespace corte2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        // Metodo para enviar los mesajes flotantes
        private async Task ShowMessage(string title, string message)
        {
            var box = MessageBoxManager.GetMessageBoxStandard(title, message);
            await box.ShowAsync();
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

        private void Editar_Serie_Click(object? sender, RoutedEventArgs e)
        {
            DataGridSeries.IsReadOnly = false;
            DataGridSeries.Columns[0].IsReadOnly = true;
            ButtonBloquearEdicionSeries.IsVisible = true;
        }

        private void Bloquear_Edicion_Actor_Click(object? sender, RoutedEventArgs e)
        {
            DataGridActores.IsReadOnly = true;
            ButtonBloquearEdicionActores.IsVisible = false;
        }

        private void Bloquear_Edicion_Serie_Click(object? sender, RoutedEventArgs e)
        {
            DataGridSeries.IsReadOnly = true;
            ButtonBloquearEdicionSeries.IsVisible = false;
        }


        async void Eliminar_Actor_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Actor actor)
            {
                var vm = this.DataContext as MainViewModel;
                if (vm != null)
                {
                    vm.Actores.Remove(actor);
                    await ShowMessage("Title", ($"Actor: {actor.Codigo} - {actor.Nombre} Eliminado"));
                }
            }
        }

        async void Eliminar_Serie_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Serie serie)
            {
                var vm = this.DataContext as MainViewModel;
                if (vm != null)
                {
                    vm.Series.Remove(serie);
                    var message = MessageBoxManager.GetMessageBoxStandard(
                        "Title",
                        ($"Serie: {serie.Codigo} {serie.Titulo} Eliminado")
                    );
                    await message.ShowAsync();
                }
            }
        }


        async Task<int> ValidarCodigo(string texto)
        {
            if (int.TryParse(texto, out int codigo))
            {
                return codigo;
            }


            await ShowMessage("Error", "El Código tiene que ser un número.");
            return -1;
        }


        async void Buscar_Actor_Click(object? sender, RoutedEventArgs e)
        {
            // Obtener el texto del TextBox
            var vm = this.DataContext as MainViewModel;
            string texto = GetCodigoActor.Text!;

            //convertir el texto del TextBox a un número
            if (int.TryParse(texto, out int codigo))
            {
                // Buscar el actor con el código especificado
                var actor = vm!.Actores.FirstOrDefault(c => c.Codigo == codigo);

                if (actor != null)
                {
                    vm.Actores.Remove(actor);
                    vm.Actores.Insert(0, actor);
                    var index = vm.Actores.IndexOf(actor);
                    DataGridActores.SelectedIndex = index;
                    GetCodigoActor.Text = "";
                }
                else
                {
                    var message = MessageBoxManager.GetMessageBoxStandard(
                        "Title",
                        "No se encontro el Actor"
                    );
                    await message.ShowAsync();
                }
            }
        }

        async void Buscar_Serie_Click(object? sender, RoutedEventArgs e)
        {
            // Obtener el texto del TextBox
            var vm = this.DataContext as MainViewModel;
            string texto = GetCodigoSerie.Text!;

            //convertir el texto del TextBox a un número
            if (int.TryParse(texto, out int codigo))
            {
                // Buscar el actor con el código especificado
                var Serie = vm.Series.FirstOrDefault(c => c.Codigo == codigo);

                if (Serie != null)
                {
                    vm.Series.Remove(Serie);
                    vm.Series.Insert(0, Serie);
                    var index = vm.Series.IndexOf(Serie);
                    DataGridSeries.SelectedIndex = index;
                    GetCodigoSerie.Text = "";
                }
                else
                {
                    var message = MessageBoxManager.GetMessageBoxStandard(
                        "Title",
                        "No se encontró la Serie"
                    );
                    await message.ShowAsync();
                }
            }
        }

        // Abre la ventana Comodin que llama al UseControl que contiene el formulario
        private void Adicionar_Actor_Click(object? sender, RoutedEventArgs e)
        {
            var ventan = new WindowComodin(new AdicionarActors());
            ventan.DataContext = this.DataContext;
            ventan.Show();
        }


        private void Adicionar_Serie_Click(object? sender, RoutedEventArgs e)
        {
            var ventana = new WindowComodin(new AdicionarSerie());
            ventana.DataContext = this.DataContext;
            ventana.Show();
        }

        private async void Detalle_Actor_Click(object? sender, RoutedEventArgs e)
        {
            var mv = DataContext as MainViewModel;

            if (GetCodigoActor.Text != null)
            {
                var codigo = await ValidarCodigo(GetCodigoActor.Text);

                if (codigo != -1)
                {
                    var listaSeries = mv.ActorTrabaja(codigo);
                    string mensaje = string.Join("\n", listaSeries);
                    GetCodigoSerie.Text = "";
                    await ShowMessage("Information", mensaje);
                }
            }
            else
            {
                await ShowMessage("Error", "Ingrese un codigo");
            }
        }

        private async void Detalle_Serie_Click(object? sender, RoutedEventArgs e)

        {
            var mv = DataContext as MainViewModel;

            if (GetCodigoSerie.Text != null)
            {
                var codigo = await ValidarCodigo(GetCodigoSerie.Text);

                if (codigo != -1)
                {
                    var listaSeries = mv.SerieActores(codigo);
                    string mensaje = string.Join("\n", listaSeries);
                    GetCodigoSerie.Text = "";
                    await ShowMessage("Information", mensaje);
                }
            }
            else
            {
                await ShowMessage("Error", "Ingrese un Codigo");
            }
        }


        private void AsociarDisociarSerieActor(object? sender, RoutedEventArgs e)
        {
            var ventan = new WindowComodin(new AsociarDisociarSerieActor());
            ventan.DataContext = this.DataContext;
            ventan.Show();
        }
    }
}