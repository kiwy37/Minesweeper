using Minesweeper.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Minesweeper.Models;
using Minesweeper.Interfaces;
using System.Collections.ObjectModel;

namespace Minesweeper.Core;

public class Commands     // aici apelez metode din game
{
    private readonly GameVM _gameVM;
    private readonly StatisticsVM _statisticsVM;

    public Commands(GameVM gameVM)
    {
        _gameVM = gameVM;
    }
    public Commands(StatisticsVM statisticsVM)
    {
        _statisticsVM = statisticsVM;
    }

    #region Start Game
    private ICommand _startGameCommand;
    
    public ICommand StartGameCommand
    {
        get
        {
            if (_startGameCommand == null)
            {
                _startGameCommand = new RelayCommand(StartGame);
            }
            return _startGameCommand;
        }
    }

    //IGame Game { get; set; }

    private void StartGame(object obj)
    {
        //Game = IGame.Produce(DifficultyLevel.Easy);
        //// IGame game = IGame.Produce(DifficultyLevel.Medium);
        //// IGame game = IGame.Produce(DifficultyLevel.Hard);
        //UIListener uiListener = new UIListener(Game);
        //Game.AddListener(uiListener);

        //Board = Game.Board;
        //for (int i = 0; i < Board.Height; i++)
        //{
        //    for (int j = 0; j < Board.Width; j++)
        //    {
        //        Game.Board.Tiles[i][j].Row = j;
        //        Game.Board.Tiles[i][j].Column = i;
        //        Game.Board.Tiles[i][j].Image = "square";
        //        Board.Tiles[i][j] = Game.Board.Tiles[i][j];
        //    }
        //}
    }

    #endregion

    #region Click
    private ICommand _leftClickCommand;
    public ICommand LeftClickCommand
    {
        get
        {
            if (_leftClickCommand == null)
            {
                _leftClickCommand = new RelayCommand(LeftClickImplementation);
            }
            return _leftClickCommand;
        }
    }

    private void LeftClickImplementation(object obj)
    {
        if (obj == null)
        {
            return;
        }

        Tile tile = obj as Tile;
         _gameVM.Game.LeftClickOnTile(tile);
        if (_gameVM.Game.GameState == GameState.Won)
        {
            if (_gameVM.Statistics == null)
            {
                _gameVM.Statistics = new ObservableCollection<Statistic>();
            }
            _gameVM.Statistics.Add(new Statistic
            {
                Result = _gameVM.Game.GameState,
                GameFinished = _gameVM.Game.EndTime,
                Difficulty = _gameVM.Game.DifficultyLevel,
                Time = _gameVM.Game.FormattedTime
            });
            _gameVM.NavigateToStartingCommand.Execute(null);
        }
        else if (_gameVM.Game.GameState == GameState.Lost)
        {
            if(_gameVM.Statistics == null)
            {
                _gameVM.Statistics = new ObservableCollection<Statistic>();
            }
            _gameVM.Statistics.Add(new Statistic
            {
                Result = _gameVM.Game.GameState,
                GameFinished = _gameVM.Game.EndTime,
                Difficulty = _gameVM.Game.DifficultyLevel,
                Time = _gameVM.Game.FormattedTime
            });
            _gameVM.NavigateToStartingCommand.Execute(null);

        }
    }

    private ICommand _rightClickCommand;
    public ICommand RightClickCommand
    {
        get
        {
            if (_rightClickCommand == null)
            {
                _rightClickCommand = new RelayCommand(RightClickImplementation);
            }
            return _rightClickCommand;
        }
    }

    private void RightClickImplementation(object obj)
    {
        if (obj == null)
        {
            return;
        }

        Tile tile = obj as Tile;
        _gameVM.Game.RightClickOnTile(tile);
        if(_gameVM.Game.GameState == GameState.Won)
        {
            if (_gameVM.Statistics == null)
            {
                _gameVM.Statistics = new ObservableCollection<Statistic>();
            }
            _gameVM.Statistics.Add(new Statistic
            {
                Result = _gameVM.Game.GameState,
                GameFinished = _gameVM.Game.EndTime,
                Difficulty = _gameVM.Game.DifficultyLevel,
                Time = _gameVM.Game.FormattedTime
            });
            _gameVM.NavigateToStartingCommand.Execute(null);
        }
        else if(_gameVM.Game.GameState == GameState.Lost)
        {
            if (_gameVM.Statistics == null)
            {
                _gameVM.Statistics = new ObservableCollection<Statistic>();
            }
            _gameVM.Statistics.Add(new Statistic
            {
                Result = _gameVM.Game.GameState,
                GameFinished = _gameVM.Game.EndTime,
                Difficulty = _gameVM.Game.DifficultyLevel,
                Time = _gameVM.Game.FormattedTime
            });
            _gameVM.NavigateToStartingCommand.Execute(null);
        }
    }
    #endregion

    private ICommand _deleteSavincsCommand;
    public ICommand DeleteSavincsCommand
    {
        get
        {
            if (_deleteSavincsCommand == null)
            {
                _deleteSavincsCommand = new RelayCommand(DeleteSavingsImplementation);
            }
            return _deleteSavincsCommand;
        }
    }

    private void DeleteSavingsImplementation(object obj)
    {
        _statisticsVM.Statistics.Clear();
    }
}