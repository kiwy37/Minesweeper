using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Minesweeper.Models
{
    public class Board
    {
        public Board(int noOfMines, int height, int width)
        {
            Width = width;
            Height = height;
            InitializeTiles();
            PlaceMines(noOfMines);
            DetermineNoOfNeighbouringMines();
        }
        public int Width { get; set; }
        public int Height { get; set; }
        public ObservableCollection<ObservableCollection<Tile>> Tiles { get; set; }

        private void InitializeTiles()
        {
            Tiles = new ObservableCollection<ObservableCollection<Tile>>();
            for (int i = 0; i < Height; i++)
            {
                var row = new ObservableCollection<Tile>();
                for (int j = 0; j < Width; j++)
                {
                    row.Add(new Tile());
                }
                Tiles.Add(row);
            }
        }
        private void PlaceMines(int noOfMines)
        {
            Random rnd = new Random();
            int i = 0;
            while (i < noOfMines)
            {
                int h = rnd.Next(Height);
                int w = rnd.Next(Width);
                if (!Tiles[h][w].IsMine)
                {
                    Tiles[h][w].IsMine = true;
                    i++;
                }

            }
        }

        private void DetermineNoOfNeighbouringMines()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (!Tiles[i][j].IsMine)
                    {
                        int countMines = 0;

                        for (int k = Math.Max(0, i - 1); k <= Math.Min(Height - 1, i + 1); k++)
                        {
                            for (int l = Math.Max(0, j - 1); l <= Math.Min(Width - 1, j + 1); l++)
                            {
                                if (Tiles[k][l].IsMine)
                                {
                                    countMines++;
                                }
                            }
                        }

                        Tiles[i][j].NumberOfNeighbouringMines = countMines;
                        //Trace.Write(Tiles[i][j].NumberOfNeighbouringMines + " ");
                    }
                    //else
                    //{
                    //    Trace.Write("9 "); 
                    //}
                }
               // Trace.WriteLine(" ");
            }
        }



    }
}