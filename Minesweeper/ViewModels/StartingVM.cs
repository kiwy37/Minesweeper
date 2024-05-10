using Minesweeper.Core;
using Minesweeper.Models;
using Minesweeper.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.ViewModels;

public class StartingVM : Core.ViewModel
{
    private INavigationService _navigation;
    private ObservableCollection<Statistic> _statistics;

    //initializare implicita si pe UI 
    private DifficultyLevel _difficultyLevel = DifficultyLevel.Easy;
    public ObservableCollection<string> DifficultyLevels { get; set; } = new ObservableCollection<string>()
    {
            DifficultyLevel.Easy.ToString(),
            DifficultyLevel.Medium.ToString(),
            DifficultyLevel.Hard.ToString()
    };

    public DifficultyLevel DifficultyLevel
    {
        get => _difficultyLevel;
        set
        {
            _difficultyLevel = value;
            OnPropertyChanged();
        }
    }
    public INavigationService Navigation
    {
        get => _navigation;
        set
        {
            _navigation = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Statistic> Statistics
    {
        get => _statistics;
        set
        {
            _statistics = value;
            OnPropertyChanged();
        }
    }

    public void UpdateInfo(ObservableCollection<Statistic> statistic)
    {
        Statistics = statistic;
    }

    public RelayCommand NavigateToGameCommand { get; set; }
    public RelayCommand NavigateToStatisticsCommand { get; set; }

    public StartingVM(INavigationService navigation)
    {
        Navigation = navigation;

        NavigateToStatisticsCommand = new RelayCommand(
            execute: o => { Navigation.NavigateTo<StatisticsVM>(Statistics, DifficultyLevel); },
            canExecute: o => true
        );

        NavigateToGameCommand = new RelayCommand(
            execute: o => { Navigation.NavigateTo<GameVM>(Statistics, DifficultyLevel); },
            canExecute: o => true
        );
    }
}