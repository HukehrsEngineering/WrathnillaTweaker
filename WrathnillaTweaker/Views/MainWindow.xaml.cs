using System.Windows;
using System.Windows.Controls;
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

    private void OnValidationError(object sender, ValidationErrorEventArgs e)
    {
        var vm = (WrathnillaConfigViewModel)DataContext;
        vm.AdjustValidationErrorCount(e.Action == ValidationErrorEventAction.Added ? 1 : -1);
    }
}
