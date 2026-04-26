using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using WrathnillaTweaker.GameConf;
using WrathnillaTweaker.GameConf.WriterParser;

namespace WrathnillaTweaker.ViewModels;

public partial class WrathnillaConfigViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsConfigLoaded))]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private WrathnillaConfig? _config;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ConfigFileName))]
    private string? _configFilePath;

    [ObservableProperty]
    private string? _message;

    private int _validationErrorCount;

    public bool IsConfigLoaded => Config != null;

    public void AdjustValidationErrorCount(int delta)
    {
        _validationErrorCount += delta;
        SaveCommand.NotifyCanExecuteChanged();
    }

    public string ConfigFileName =>
        ConfigFilePath != null ? Path.GetFileName(ConfigFilePath) : "WrathnillaTweaker";

    public bool ShouldPromptForFile { get; private set; }

    public WrathnillaConfigViewModel()
    {
        var appConf = AppConf.LoadOrDefault();
        if (!string.IsNullOrEmpty(appConf.GameplayConfigPath))
            LoadFile(appConf.GameplayConfigPath);
        else
            ShouldPromptForFile = true;
    }

    [RelayCommand]
    private void OpenFilePicker()
    {
        var dialog = new OpenFileDialog
        {
            Title = "Select your Wrathnilla Server's gameplay.conf file (SERVER\\configs\\gameplay.conf)",
            Filter = "Gameplay Config (gameplay.conf)|gameplay.conf",
        };
        if (dialog.ShowDialog() == true)
            LoadFile(dialog.FileName);
    }

    public void LoadFile(string filePath)
    {
        try
        {
            Config = new Parser().Parse<WrathnillaConfig>(filePath);
            ConfigFilePath = filePath;
            new AppConf { GameplayConfigPath = filePath }.Save();
            Message = $"Loaded {Path.GetFileName(filePath)}";
        }
        catch (Exception ex)
        {
            Message = $"Load failed: {ex.Message}";
        }
    }

    private bool CanSave() => IsConfigLoaded && _validationErrorCount == 0;

    [RelayCommand(CanExecute = nameof(CanSave))]
    private void Save()
    {
        if (ConfigFilePath == null || Config == null) return;
        try
        {
            new Writer().Write(ConfigFilePath, Config);
            Message = $"Saved {Path.GetFileName(ConfigFilePath)}";
        }
        catch (Exception ex)
        {
            Message = $"Save failed: {ex.Message}";
        }
    }
}
