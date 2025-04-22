using Avalonia.Controls;
using Avalonia.Input;

namespace corte2.ViewForm;

public partial class WindowComodin : Window
{
    public WindowComodin(UserControl view)
    {
        InitializeComponent();
        UpdateUserControl(view);
    }


    private void UpdateUserControl(UserControl form)
    {
        var formulario = form;
        var formularioContainer = this.FindControl<Grid>("UsersControl")!;
        formularioContainer.Children.Clear();
        formularioContainer.Children.Add(formulario);
    }
    
}