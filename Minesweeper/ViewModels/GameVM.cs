using Minesweeper.Core;
using Minesweeper.Interfaces;
using Minesweeper.Models;
using Minesweeper.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace Minesweeper.ViewModels;

public class GameVM : Core.ViewModel
{
    private INavigationService _navigation;
    private ObservableCollection<Statistic> _statistics;
    private DifficultyLevel _difficultyLevel;
    private StartingVM _startingVM;
    private Commands _commands;
    Board _board;
    private RelayCommand _navigateToStartingCommand;
    
    public IGame Game { get; set; }

    public DifficultyLevel DifficultyLevel
    {
        get => _difficultyLevel;
        set
        {
            _difficultyLevel = value;
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
        get => _statistics;
        set
        {
            _statistics = value;
            OnPropertyChanged();

            StartingVM?.UpdateInfo(value);
        }
    }
    public Board Board
    {
        get => _board;
        set
        {
            _board = value;
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
    public RelayCommand NavigateToStartingCommand
    {
        get
        {
            Game = IGame.Produce(DifficultyLevel);
            UIListener uiListener = new UIListener(Game);
            Game.AddListener(uiListener);

            Board = Game.Board;
            for (int i = 0; i < Board.Height; i++)
            {
                for (int j = 0; j < Board.Width; j++)
                {
                    Game.Board.Tiles[i][j].Row = i;
                    Game.Board.Tiles[i][j].Column = j;
                    Game.Board.Tiles[i][j].Image = "square";
                    Board.Tiles[i][j] = Game.Board.Tiles[i][j];
                }
            }
            return _navigateToStartingCommand;
        }
        set
        {
            _navigateToStartingCommand = value;
        }
    }

    public GameVM(INavigationService navigation, StartingVM startingVM)
    {
        Navigation = navigation;
        StartingVM = startingVM;

        NavigateToStartingCommand = new RelayCommand(
            execute: o => { Navigation.NavigateTo<StartingVM>(Statistics, DifficultyLevel); },

            canExecute: o => true
        );
    }
}

