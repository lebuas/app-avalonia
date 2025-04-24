using Avalonia.Controls;
using Avalonia.VisualTree;

namespace corte2.ViewForm;

public partial class WindowComodin : Window
{
    public WindowComodin(UserControl view)
    {
        InitializeComponent();
        UpdateUserControl(view);
    }


  // Cargar en el grid de la ventana Comodin, los formulario que estan en el UserControl
    private void UpdateUserControl(UserControl form)
    {
        var formulario = form;
        var formularioContainer = this.FindControl<Grid>("UsersControl")!;
        formularioContainer.Children.Clear();
        formularioContainer.Children.Add(formulario);
    }
    
}