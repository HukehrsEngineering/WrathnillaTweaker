using System.Windows;
using WrathnillaTweaker.ViewModels;

namespace WrathnillaTweaker.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new WrathnillaConfigViewModel();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        var vm = (WrathnillaConfigViewModel)DataContext;
        if (vm.ShouldPromptForFile)
            vm.OpenFilePickerCommand.Execute(null);
    }
}
