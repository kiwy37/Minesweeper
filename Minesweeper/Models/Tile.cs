using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models
{
    public class Tile : INotifyPropertyChanged
    {
        public Tile()
        {
            IsFlagged = false;
            IsRevealed = false;
            IsMine = false;
            NumberOfNeighbouringMines = 0;
        }

        public int Row { get; set; }
        public int Column { get; set; }

        private bool _isFlagged;
        public bool IsFlagged
        {
            get => _isFlagged;
            set
            {
                _isFlagged = value;
                OnPropertyChanged();
            }
        }

        private bool _isRevealed;
        public bool IsRevealed
        {
            get => _isRevealed;
            set
            {
                _isRevealed = value;
                OnPropertyChanged();
            }
        }

        private bool _isMine;
        public virtual bool IsMine
        {
            get => _isMine;
            set
            {
                _isMine = value;
                OnPropertyChanged();
            }
        }

        private int _numberOfNeighbouringMines;
        public int NumberOfNeighbouringMines
        {
            get => _numberOfNeighbouringMines;
            set
            {
                _numberOfNeighbouringMines = value;
                OnPropertyChanged();
            }
        }

        private string _image;
        public string Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
