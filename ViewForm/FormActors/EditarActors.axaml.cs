using System.Linq;
using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace corte2.ViewForm.FormActors
{
    public partial class EditarActors : UserControl
    {
        public EditarActors()
        {
            InitializeComponent();
        }


        private void OnBuscarClick(object? sender, RoutedEventArgs e)
        {
            // Obtener el texto del TextBox
            var vm = DataContext as MainViewModel;
            string texto = BuscarCodigoTextBox.Text!;

            //convertir el texto del TextBox a un número
            if (int.TryParse(texto, out int codigo))
            {
                // Buscar el actor con el código especificado
                var actor = vm.Actores.FirstOrDefault(a => a.Codigo == codigo);

                if (actor != null)
                {
                    vm.Actores.Remove(actor);
                    vm.Actores.Insert(0, actor);
                }
                else
                {
                    Console.WriteLine("No se encontró un actor con ese código.");
                }
            }
        }


        private void OnGuardarClick(object? sender, RoutedEventArgs e)
        {
            // MainWindows
            var mainWindows = this.VisualRoot as MainWindow;
            
            
        }
    }
}