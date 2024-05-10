using Minesweeper.Core;
using Minesweeper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.ViewModels;

public class MainViewModel : Core.ViewModel
{
    private INavigationService _navigation;

    public INavigationService Navigation
    {
        get => _navigation;
        set
        {
            _navigation = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand NavigateToGameCommand { get; set; }
    public RelayCommand NavigateToStatisticsCommand { get; set; }
    public MainViewModel(INavigationService navService)
    {
        Navigation = navService;
        Navigation.CurrentView = new StartingVM(Navigation);
        NavigateToGameCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<GameVM>(); }, canExecute: o => true);
        NavigateToStatisticsCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<StatisticsVM>(); }, canExecute: o => true);
    }
}

