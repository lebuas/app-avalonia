using System.Linq;
using System;
using Avalonia.Controls;
using Avalonia.Interactivity;


namespace corte2.ViewForm.FormActors
{
    public partial class BorrarActors : UserControl
    {
        // Constructor
        public BorrarActors()
        {
            InitializeComponent(); // Inicializa los componentes del UserControl
        }

        private void OnGuardarEliminar(object? sender, RoutedEventArgs e)
        {
            var vm = DataContext as MainViewModel; // Obtener el ViewModel desde el DataContext

            // Verificar si se pudo obtener el ViewModel
            if (vm == null)
            {
                Console.WriteLine("No se pudo obtener el ViewModel.");
                return; // Salir del método si no se obtiene el ViewModel
            }

            // Obtener el texto del TextBox
            string texto = ActorCodigoTextBox.Text!;

            //convertir el texto del TextBox a un número
            if (int.TryParse(texto, out int codigo))
            {
                // Buscar el actor con el código especificado
                var actor = vm.Actores.FirstOrDefault(a => a.Codigo == codigo);

                if (actor != null)
                {
                    // Actor encontrado, proceder con la eliminación
                    vm.Actores.Remove(actor); // Eliminar el actor de la colección
                    Console.WriteLine($"Actor con código {codigo} eliminado.");
                }
                else
                {
                    Console.WriteLine("No se encontró un actor con ese código.");
                }
            }
            else
            {
                // La conversión falló (el texto no es un número válido)
                Console.WriteLine("El código ingresado no es un número válido.");
            }
        }
    }
}