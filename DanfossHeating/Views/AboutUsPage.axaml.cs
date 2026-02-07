using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DanfossHeating.ViewModels;
using System;
using System.Diagnostics;

namespace DanfossHeating.Views;

public partial class AboutUsPage : UserControl
{
    private AboutUsViewModel? _viewModel;
    
    public AboutUsPage()
    {
        InitializeComponent();
        Console.WriteLine("AboutUsPage constructed");
        
        Loaded += Page_Loaded;
        DataContextChanged += Page_DataContextChanged;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void Page_Loaded(object? sender, EventArgs e)
    {
        Console.WriteLine("AboutUsPage loaded and visible");
        UpdateThemeClass();
    }
    
    private void Page_DataContextChanged(object? sender, EventArgs e)
    {
        if (_viewModel != null)
        {
            _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }
        
        _viewModel = DataContext as AboutUsViewModel;
        
        if (_viewModel != null)
        {
            Console.WriteLine($"AboutUsPage received DataContext with userName: {_viewModel.UserName}");
            _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            UpdateThemeClass();
        }
        else
        {
            Console.WriteLine("WARNING: AboutUsPage DataContext is not AboutUsViewModel");
        }
    }
    
    private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AboutUsViewModel.IsDarkTheme))
        {
            UpdateThemeClass();
        }
    }
    
    private void UpdateThemeClass()
    {
        try
        {
            if (_viewModel != null)
            {
                Classes.Set("dark", _viewModel.IsDarkTheme);
                Console.WriteLine($"Updated AboutUsPage theme class: {(_viewModel.IsDarkTheme ? "dark" : "light")}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting theme class: {ex.Message}");
        }
    }

    private void OpenGitHub(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is string url && !string.IsNullOrWhiteSpace(url))
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to open GitHub link: {ex.Message}");
            }
        }
    }
}
