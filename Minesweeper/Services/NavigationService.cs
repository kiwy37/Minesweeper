using Minesweeper.Core;
using Minesweeper.Models;
using Minesweeper.ViewModels;
using Minesweeper.Views;
using System;
using System.Collections.ObjectModel;

namespace Minesweeper.Services;

public interface INavigationService
{
    Core.ViewModel CurrentView { get; set; }

    void NavigateTo<T>(ObservableCollection<Statistic> transmittedInfo = null, DifficultyLevel difficultyLevel = DifficultyLevel.Easy) where T : Core.ViewModel;
}

public class NavigationService : ObservableObject, INavigationService
{
    private readonly Func<Type, Core.ViewModel> _viewModelFactory;

    private Core.ViewModel _currentView;

    public Core.ViewModel CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public NavigationService(Func<Type, Core.ViewModel> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public void NavigateTo<T>(ObservableCollection<Statistic> transmittedInfo = null, DifficultyLevel difficultyLevel = DifficultyLevel.Easy) where T : Core.ViewModel
    {
        Core.ViewModel viewModel = _viewModelFactory.Invoke(typeof(T));

        if (viewModel is StatisticsVM statisticsVM)
        {
            statisticsVM.Statistics = transmittedInfo;
            statisticsVM.DifficultyLevel = difficultyLevel;
            MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
            mainWindow.Width = 500;
            mainWindow.Height = 500;
        }

        if (viewModel is GameVM gameVM)
        {
            MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
            gameVM.Statistics = transmittedInfo;
            gameVM.DifficultyLevel = difficultyLevel;
            switch (difficultyLevel)
            {
                case DifficultyLevel.Easy:
                    mainWindow.Width = 400;
                    mainWindow.Height = 400;
                    break;
                case DifficultyLevel.Medium:
                    mainWindow.Width = 400;
                    mainWindow.Height = 600; 
                    break;
                case DifficultyLevel.Hard:
                    mainWindow.Width = 600;
                    mainWindow.Height = 700; 
                    break;
            }
        }
        CurrentView = viewModel;
    }
}
