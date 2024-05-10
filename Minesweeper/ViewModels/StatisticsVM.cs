using Minesweeper.Core;
using Minesweeper.Models;
using Minesweeper.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Minesweeper.ViewModels;

public class StatisticsVM : Core.ViewModel
{
    private INavigationService _navigation;
    private StartingVM _startingVM;
    private ObservableCollection<Statistic> _statistics;
    private DifficultyLevel _difficultyLevel;
    private Commands _commands;

    public Commands Commands
    {
        get
        {
            if (_commands == null)
            {
                _commands = new Commands(this);
            }
            return _commands;
        }
    }
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
    public StartingVM StartingVM
    {
        get => _startingVM;
        set
        {
            _startingVM = value;
            OnPropertyChanged();
        }
    }
    public ObservableCollection<Statistic> Statistics
    {
        get { return _statistics; }
        set
        {
            _statistics = value;
            OnPropertyChanged("Statistics");

            StartingVM?.UpdateInfo(value);
        }
    }
    public RelayCommand NavigateToStartingCommand { get; set; }

    public StatisticsVM(INavigationService navigation, StartingVM startingVM)
    {
        Navigation = navigation;
        StartingVM = startingVM;

        NavigateToStartingCommand = new RelayCommand(
            execute: o => { Navigation.NavigateTo<StartingVM>(Statistics, DifficultyLevel); },
            canExecute: o => true
        );
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
