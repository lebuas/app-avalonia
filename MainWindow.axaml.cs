using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using System.Linq;
using corte2.Models;
using corte2.ViewForm;
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

        // Muestra un mensaje emergente
        private async Task ShowMessage(string title, string message)
        {
            var box = MessageBoxManager.GetMessageBoxStandard(title, message);
            await box.ShowAsync();
        }

        // Muestra la vista de actores y oculta las demás
        private void OnActorsClick(object sender, RoutedEventArgs e)
        {
            Home.IsVisible = false;
            DataGridSeries.IsVisible = false;
            GridSeries.IsVisible = false;
            DataGridActores.IsVisible = true;
            GridActors.IsVisible = true;
        }

        // Muestra la vista de series y oculta las demás
        private void OnSeriesClick(object sender, RoutedEventArgs e)
        {
            Home.IsVisible = false;
            DataGridActores.IsVisible = false;
            GridActors.IsVisible = false;
            DataGridSeries.IsVisible = true;
            GridSeries.IsVisible = true;
        }

        // Muestra la vista de inicio
        private void OnHomeClik(object? sender, RoutedEventArgs e)
        {
            Home.IsVisible = true;
        }

        // Cierra la ventana principal
        private void OnExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Abre el repositorio de GitHub en el navegador
        private void GithubClick(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/lebuas")
            {
                UseShellExecute = true
            });
        }

        // Evento vacío para información
        private void OnInfoClick(object? sender, RoutedEventArgs e)
        {
        }

        // Habilita edición de actores
        async void Editar_Actor_Click(object? sender, RoutedEventArgs e)
        {
            DataGridActores.IsReadOnly = false;
            DataGridActores.Columns[0].IsReadOnly = true;
            ButtonBloquearEdicionActores.IsVisible = true;
            await ShowMessage("Información",
                "Esta disponible la edición en pantalla.\nDar doble clic sobre el campo que desea Editar de la tabla.\nNo olvide darle al boton Actualizar cuando termine de editar ");
        }

        // Habilita edición de series
        async void Editar_Serie_Click(object? sender, RoutedEventArgs e)
        {
            DataGridSeries.IsReadOnly = false;
            DataGridSeries.Columns[0].IsReadOnly = true;
            ButtonBloquearEdicionSeries.IsVisible = true;
            await ShowMessage("Información",
                "Esta disponible la edición en pantalla.\nDar doble clic sobre el campo que desea editar de la tabla.\nNo olvide darle al boton Actualizar cuando termine de editar ");
        }

        // Bloquea edición de actores
        private void Bloquear_Edicion_Actor_Click(object? sender, RoutedEventArgs e)
        {
            DataGridActores.IsReadOnly = true;
            ButtonBloquearEdicionActores.IsVisible = false;
        }

        // Bloquea edición de series
        private void Bloquear_Edicion_Serie_Click(object? sender, RoutedEventArgs e)
        {
            DataGridSeries.IsReadOnly = true;
            ButtonBloquearEdicionSeries.IsVisible = false;
        }

        // Elimina un actor del listado
        async void Eliminar_Actor_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Actor actor)
            {
                var vm = this.DataContext as MainViewModel;
                if (vm != null)
                {
                    var resutaldo = vm.ActorBorra(actor.Codigo);
                    if (!resutaldo)
                    {
                        await ShowMessage("Error",
                            "No se puede eliminar el Actor/Actirz porque esta trabajando en una serie");
                    }
                }
            }
        }

        // Elimina una serie del listado
        async void Eliminar_Serie_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Serie serie)
            {
                var vm = this.DataContext as MainViewModel;
                if (vm != null)
                {
                    var resultado = vm.SerieBorra(serie.Codigo);
                    if (!resultado)
                    {
                        await ShowMessage("Error", "Hubo un problema al eliminar la serie");
                    }
                }
            }
        }

        // Valida que un texto sea numérico y lo retorna como entero
        async Task<int> ValidarCodigo(string texto)
        {
            if (int.TryParse(texto, out int codigo))
                return codigo;

            await ShowMessage("Error", "El Código tiene que ser un número.");
            return -1;
        }

        // Busca un actor por código
        async void Buscar_Actor_Click(object? sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as MainViewModel;
            string texto = GetCodigoActor.Text;
            if (texto != null)
            {
                if (int.TryParse(texto, out int codigo))
                {
                    var actor = vm!.Actores.FirstOrDefault(c => c.Codigo == codigo);

                    if (actor != null)
                    {
                        vm.Actores.Remove(actor);
                        vm.Actores.Insert(0, actor);
                        var index = vm.Actores.IndexOf(actor);
                        DataGridActores.SelectedIndex = index;
                        GetCodigoActor.Text = null;
                    }
                    else
                    {
                        await ShowMessage("Búsqueda", "No se encontró el Actor");
                        GetCodigoActor.Text = null;
                    }
                }
                else
                {
                    await ShowMessage("Información", "El código tiene que se un número entero");
                    GetCodigoActor.Text = null;
                }
            }
            else
            {
                await ShowMessage("Información", "Ingrese un Código de Actor");
            }
        }

        // Busca una serie por código
        async void Buscar_Serie_Click(object? sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as MainViewModel;
            string texto = GetCodigoSerie.Text;
            if (texto != null)
            {
                if (int.TryParse(texto, out int codigo))
                {
                    var serie = vm.Series.FirstOrDefault(c => c.Codigo == codigo);

                    if (serie != null)
                    {
                        vm.Series.Remove(serie);
                        vm.Series.Insert(0, serie);
                        var index = vm.Series.IndexOf(serie);
                        DataGridSeries.SelectedIndex = index;
                        GetCodigoSerie.Text = null;
                    }
                    else
                    {
                        await ShowMessage("Información", "No se encontró la Serie");
                        GetCodigoSerie.Text = null;
                    }
                }
                else
                {
                    await ShowMessage("Información", "El codigo tiene que ser un numero entero");
                    GetCodigoSerie.Text = null;
                }
            }
            else
            {
                await ShowMessage("Información", "Ingrese un Código de Serie");
            }
        }

        // Abre la ventana para adicionar un nuevo actor
        private void Adicionar_Actor_Click(object? sender, RoutedEventArgs e)
        {
            var ventan = new WindowComodin(new AdicionarActors());
            ventan.DataContext = this.DataContext;
            ventan.Show();
        }

        // Abre la ventana para adicionar una nueva serie
        private void Adicionar_Serie_Click(object? sender, RoutedEventArgs e)
        {
            var ventana = new WindowComodin(new AdicionarSerie());
            ventana.DataContext = this.DataContext;
            ventana.Show();
        }

        // Muestra detalle de las series en que trabaja un actor
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
                    GetCodigoActor.Text = null;
                    await ShowMessage("Series:", mensaje);
                }

                GetCodigoActor.Text = null;
            }
            else
            {
                await ShowMessage("Información", "Ingrese un Código de Actor para ver en que series trabaja");
            }
        }

        // Muestra detalle de los actores que trabajan en una serie
        private async void Detalle_Serie_Click(object? sender, RoutedEventArgs e)
        {
            var mv = DataContext as MainViewModel;

            if (GetCodigoSerie.Text != null)
            {
                var codigo = await ValidarCodigo(GetCodigoSerie.Text);

                if (codigo != -1)
                {
                    if (mv.PosSerie(codigo) >=0)
                    {
                        var listaSeries = mv.SerieActores(codigo);
                        string mensaje = string.Join("\n", listaSeries);
                        GetCodigoSerie.Text = null;
                        await ShowMessage("Actores", mensaje);
                    }
                    else
                    {
                        await ShowMessage("Información", "No se encontro la Serie");
                        GetCodigoSerie.Text = null;
                    }
                }

                GetCodigoSerie.Text = null;
            }
            else
            {
                await ShowMessage("Error", "Ingrese un Código de Serie para ver que actores trabajan");
            }
        }

        // Abre la ventana de asociación entre series y actores
        private void AsociarDisociarSerieActor(string evento)
        {
            var ventan = new WindowComodin(new AsociarDisociarSerieActor(evento));
            ventan.DataContext = this.DataContext;
            ventan.Show();
        }

        private void AsociarSerieActor(object? sender, RoutedEventArgs e)
        {
            AsociarDisociarSerieActor("A");
        }

        private void DisociarSerieActor(object? sender, RoutedEventArgs e)
        {
            AsociarDisociarSerieActor("D");
        }
    }
}