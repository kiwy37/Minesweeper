using Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Minesweeper.Interfaces
{
    public class IGame: Core.ViewModel
    {
        private int _flagsAvailable;
        private int _flagsRemaining;
        private int _flagsUsed;
        public int DistanceToMine { get; set; }
        public int Time { get; set;}
        public Board Board { get; set; }
        public GameState GameState { get; set; }
        public DifficultyLevel DifficultyLevel { get; set; }
        public Timer Timer { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        private String _formattedTime;

        private List<IGameListener> observers = new List<IGameListener>();

        public IGame() 
        {
            GameState = GameState.InProgress;
            FlagsUsed = 0;
            Time = 0;
            StartTime = DateTime.Now;

            Timer = new Timer();
            Timer.Interval = 1000;
            Timer.Elapsed += Timer_Elapsed;
            Timer.Start();
        }

        public String FormattedTime
        {
            get => _formattedTime;
            set
            {
                _formattedTime = value;
                OnPropertyChanged();
            }
        }

        public int FlagsAvailable
        {
            get => _flagsAvailable;
            set
            {
                _flagsAvailable = value;
                FlagsRemaining = FlagsAvailable;
                OnPropertyChanged();
            }
        }
        public int FlagsUsed
        {
            get => _flagsUsed;
            set
            {
                _flagsUsed = value;
                FlagsRemaining = FlagsAvailable - _flagsUsed;
                OnPropertyChanged();
            }
        }

        public int FlagsRemaining
        {
            get => _flagsRemaining;
            set
            {
                _flagsRemaining = value;
                OnPropertyChanged();
            }
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Time++;
            GetFormattedTime();
           // Trace.WriteLine("Timer Tick! " + GetFormattedTime());
        }

        public String GetFormattedTime()
        {
            int minutes = Time / 60;  
            int seconds = Time % 60;

            FormattedTime = $"{minutes:D2}:{seconds:D2}";
            return $"{minutes:D2}:{seconds:D2}";
        }


        // metoda factory
        public static IGame Produce(DifficultyLevel difficultyLevel)
        {
            IGame game = CreateGame(difficultyLevel);
            IDifficultyStrategy difficultyStrategy = IDifficultyStrategy.CreateDifficultyStrategy(difficultyLevel);
            difficultyStrategy.SetDifficulty(game);
            return game;
        }

        private static IGame CreateGame(DifficultyLevel difficultyLevel)
        {
            switch (difficultyLevel)
            {
                case DifficultyLevel.Easy:
                    return new EasyGame();
                case DifficultyLevel.Medium:
                    return new MediumGame();
                case DifficultyLevel.Hard:
                    return new HardGame();
                default:
                    return new EasyGame();
            }
        }

        public void AddListener(IGameListener observer)
        {
            observers.Add(observer);
        }

        public void RemoveListener(IGameListener observer)
        {
            observers.RemoveAll(listener => listener == observer);
        }

        public void LeftClickOnTile(Tile tile)
        {
            if (GameState != GameState.InProgress)
            {
                return;
            }
            if (tile.IsFlagged)
            {
                tile.IsFlagged = false;
                FlagsUsed--;
            }
            if (tile.IsMine == true)
            {
                tile.Image = "mine";
                GameState = GameState.Lost;
                EndTime = DateTime.Now;
                Timer.Stop();
                foreach (var observer in observers)
                {
                    observer.OnGameLost();
                }
                return;
            }
           
            RevealNeighboringTiles(tile);

            if (AreAllNonMinesRevealed())
            {
                RevealAllTiles();
                GameState = GameState.Won;
                EndTime = DateTime.Now;
                Timer.Stop();
                foreach (var observer in observers)
                {
                    observer.OnGameWon();
                }
                return;
            }

            foreach (var observer in observers)
            {
                observer.OnLeftClickOnTile();
            }
        }

        private void RevealNonMineTile(Tile tile)
        {
            if (tile.IsRevealed == true)
            {
                return;
            }

            tile.IsRevealed = true;
            switch (tile.NumberOfNeighbouringMines)
            {
                case 0:
                    tile.Image = "SquareOutline";
                    break;
                case 1:
                    tile.Image = "Numeric1";
                    break;
                case 2:
                    tile.Image = "Numeric2";
                    break;
                case 3:
                    tile.Image = "Numeric3";
                    break;
                case 4:
                    tile.Image = "Numeric4";
                    break;
                case 5:
                    tile.Image = "Numeric5";
                    break;
                case 6:
                    tile.Image = "Numeric6";
                    break;
                case 7:
                    tile.Image = "Numeric7";
                    break;
                case 8:
                    tile.Image = "Numeric8";
                    break;
                default:
                    tile.Image = "SquareOutline";
                    break;
            }
        }

        public virtual void RevealNeighboringTiles(Tile tile)
        {
            RevealNonMineTile(tile);
            if (tile.NumberOfNeighbouringMines != 0)
            {
                return;
            }

            for (int i = Math.Max(0, tile.Row - 1); i <= Math.Min(Board.Height - 1, tile.Row + 1); i++)
            {
                for (int j = Math.Max(0, tile.Column - 1); j <= Math.Min(Board.Width - 1, tile.Column + 1); j++)
                {
                    if (!Board.Tiles[i][j].IsMine && !Board.Tiles[i][j].IsRevealed)
                    {
                        if (Board.Tiles[i][j].IsFlagged)
                        {
                            Board.Tiles[i][j].IsFlagged = false;
                            FlagsUsed--;
                        }
                        RevealNeighboringTiles(Board.Tiles[i][j]);
                    }
                }
            }
        }

        public virtual bool AreAllNonMinesRevealed()
        {
            for (int i = 0; i < Board.Height; i++)
            { 
                for (int j = 0; j < Board.Width; j++)
                {
                    if (!Board.Tiles[i][j].IsMine && Board.Tiles[i][j].IsRevealed == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public virtual void RevealAllTiles()
        {
            for (int i = 0; i < Board.Height; i++)
            {
                for (int j = 0; j < Board.Width; j++)
                {
                    if (Board.Tiles[i][j].IsRevealed == false)
                    {
                        if (Board.Tiles[i][j].IsMine == false)
                        {
                            RevealNonMineTile(Board.Tiles[i][j]);
                        }
                        else
                        {
                            Board.Tiles[i][j].Image = "mine";
                        }
                        Board.Tiles[i][j].IsRevealed = true;
                    }
                }
            }
        }

        public void RightClickOnTile(Tile tile)
        {
            if (GameState != GameState.InProgress)
            {
                return;
            }

            // mark or unmark as mine
            if (tile.Image == "square")
            {
                if (FlagsUsed == FlagsAvailable)
                {
                    return;
                }
                tile.Image = "flag";
                tile.IsFlagged = true;
                FlagsUsed++;
            }
            else if (tile.Image == "flag")
            {
                tile.Image = "square";
                tile.IsFlagged = false;
                FlagsUsed--;
            }

            foreach (var observer in observers)
            {
                observer.OnRightClickOnTile();
            }
        }
    }
}
