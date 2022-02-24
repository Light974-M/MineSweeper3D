using UnityEngine;

namespace MineSweeper3D.Classic
{
    ///<summary>
    /// represent cells grid for minesweeper 2D
    ///</summary>
    public class Cell
    {
        private Coords2D _position;

        private bool _isCovered;

        private bool _isBomb;

        private bool _hasFlag;

        private int _nearBombsNumber;

        #region public API

        public bool IsBomb
        {
            get { return _isBomb; }
            set { _isBomb = value; }
        }

        public bool IsCovered
        {
            get { return _isCovered; }
            set { _isCovered = value; }
        }

        public bool HasFlag
        {
            get { return _hasFlag; }
            set { _hasFlag = value; }
        }

        public int NearBombsNumber
        {
            get { return _nearBombsNumber; }
            set { _nearBombsNumber = value; }
        }

        public Coords2D Position => _position;

        #endregion

        public Cell(int x, int y, bool isCovered, bool isBomb, bool hasFlag, int nearBombsNumber)
        {
            _position = new Coords2D(x, y);
            _isCovered = isCovered;
            _isBomb = isBomb;
            _hasFlag = hasFlag;
            _nearBombsNumber = nearBombsNumber;
        }
    } 
}
