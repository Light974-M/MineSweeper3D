using UnityEngine;

namespace MineSweeper3D.Classic3D
{
    ///<summary>
    /// Level GridMap for minesweeper 2D classic, contain all cells of game.
    ///</summary>
    public class Level
    {
        #region variables

        private Cell[,,] _cellsArray = null;

        private int _width = 3;

        private int _height = 3;

        private int _length = 3;

        private int _bombNumber = 2;

        private int _coveredCellsNumber = 0;

        private bool _isGameOver = false;

        private bool _hasWin = false;

        private bool _isPaused = false;

        #endregion


        #region public API

        public int Width => _width;
        public int Height => _height;
        public int Length => _length;

        public int BombNumber => _bombNumber;

        public Cell[,,] CellsArray => _cellsArray;

        public int CoveredCellsNumber
        {
            get { return _coveredCellsNumber; }
            set { _coveredCellsNumber = value; }
        }

        public bool IsGameOver
        {
            get { return _isGameOver; }
            set { _isGameOver = value; }
        }

        public bool HasWin
        {
            get { return _hasWin; }
            set { _hasWin = value; }
        }

        public bool IsPaused
        {
            get { return _isPaused; }
            set { _isPaused = value; }
        }

        #endregion

        /// <summary>
        /// Constructor for current level map
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="length"></param>
        public Level(int width, int height, int length, int bombNumber)
        {
            _width = width;
            _height = height;
            _length = length;
            _bombNumber = Mathf.Clamp(bombNumber, 0, (_width * _height * _length) - 1);

            _cellsArray = new Cell[width, height, length];

            for (int z = 0; z < length; z++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        _cellsArray[x, y, z] = new Cell(x, y, z, true, false, false, 0);

                        _coveredCellsNumber++;
                    }
                }
            }
        }

        public void BuildBombs(Cell clickedCell)
        {
            for (int bombIndex = 0; bombIndex < _bombNumber; bombIndex++)
            {
                int bombPos = Random.Range(0, (_width * _height * _length) - bombIndex);
                int bombPosLength = bombPos / (_width * _height);
                int bombPosHeight = bombPos - ((_height * _width) * bombPosLength) / _width;
                int bombPoswidth = bombPos - (((_width * _height) * bombPosLength) + (_width * bombPosHeight));
                bool isOneOfSelectedCell = false;

                int x = clickedCell.Position.x;
                int y = clickedCell.Position.y;
                int z = clickedCell.Position.z;

                isOneOfSelectedCell = verifyBombPosComparedToInput(x, y, z, bombPoswidth, bombPosHeight, bombPosLength);


                if (!_cellsArray[bombPoswidth, bombPosHeight, bombPosLength].IsBomb && !isOneOfSelectedCell)
                {
                    _cellsArray[bombPoswidth, bombPosHeight, bombPosLength].IsBomb = true;
                }
                else
                {
                    int cellsTested = 0;
                    bool hasPlacedCell = false;

                    while ((_cellsArray[bombPoswidth, bombPosHeight, bombPosLength].IsBomb || isOneOfSelectedCell) && cellsTested < (_width * _height * _length))
                    {
                        bombPos++;
                        bombPosLength = bombPos / (_width * _height);
                        bombPosHeight = bombPos - ((_height * _width) * bombPosLength) / _width;
                        bombPoswidth = bombPos - (((_width * _height) * bombPosLength) + (_width * bombPosHeight));

                        if (bombPoswidth >= _width || bombPosHeight >= _height || bombPosLength >= _length)
                        {
                            bombPos = 0;
                            bombPosLength = bombPos / (_width * _height);
                            bombPosHeight = bombPos - ((_height * _width) * bombPosLength) / _width;
                            bombPoswidth = bombPos - (((_width * _height) * bombPosLength) + (_width * bombPosHeight));
                        }

                        isOneOfSelectedCell = verifyBombPosComparedToInput(x, y, z, bombPoswidth, bombPosHeight, bombPosLength);

                        if (!_cellsArray[bombPoswidth, bombPosHeight, bombPosLength].IsBomb && !isOneOfSelectedCell)
                        {
                            _cellsArray[bombPoswidth, bombPosHeight, bombPosLength].IsBomb = true;
                            hasPlacedCell = true;
                            break;
                        }

                        cellsTested++;
                    }

                    if (!hasPlacedCell)
                    {
                        cellsTested = 0;

                        if (!_cellsArray[bombPoswidth, bombPosHeight, bombPosLength].IsBomb && !(x == bombPoswidth && y == bombPosHeight && z == bombPosLength))
                        {
                            _cellsArray[bombPoswidth, bombPosHeight, bombPosLength].IsBomb = true;
                        }
                        else
                        {
                            while ((_cellsArray[bombPoswidth, bombPosHeight, bombPosLength].IsBomb || (x == bombPoswidth && y == bombPosHeight && z == bombPosLength)) && cellsTested < (_width * _height * _length))
                            {
                                bombPos++;
                                bombPosLength = bombPos / (_width * _height);
                                bombPosHeight = bombPos - ((_height * _width) * bombPosLength) / _width;
                                bombPoswidth = bombPos - (((_width * _height) * bombPosLength) + (_width * bombPosHeight));

                                if (bombPoswidth >= _width || bombPosHeight >= _height || bombPosLength >= _length)
                                {
                                    bombPos = 0;
                                    bombPosLength = bombPos / (_width * _height);
                                    bombPosHeight = bombPos - ((_height * _width) * bombPosLength) / _width;
                                    bombPoswidth = bombPos - (((_width * _height) * bombPosLength) + (_width * bombPosHeight));
                                }

                                if (!_cellsArray[bombPoswidth, bombPosHeight, bombPosLength].IsBomb && !(x == bombPoswidth && y == bombPosHeight && z == bombPosLength))
                                {
                                    _cellsArray[bombPoswidth, bombPosHeight, bombPosLength].IsBomb = true;
                                    hasPlacedCell = true;
                                    break;
                                }

                                cellsTested++;
                            }
                        }
                    }
                }
            }

            UpdateNearBombNumber();
        }

        private bool verifyBombPosComparedToInput(int x, int y, int z, int bombPoswidth, int bombPosHeight, int bombPosLength)
        {
            bool isOneOfSelectedCell = false;

            for (int h = -1; h <= 1; h++)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (!(x + h >= _width || y + i >= _height || z + j >= _length || x + h < 0 || y + i < 0 || z + j < 0))
                        {
                            Coords3D testedCellPos = _cellsArray[x + h, y + i, z + j].Position;

                            if (testedCellPos.x == bombPoswidth && testedCellPos.y == bombPosHeight && testedCellPos.z == bombPosLength)
                                isOneOfSelectedCell = true;
                        }
                    }
                }
            }

            return isOneOfSelectedCell;
        }

        private void UpdateNearBombNumber()
        {
            for (int z = 0; z < _length; z++)
            {
                for (int y = 0; y < _height; y++)
                {
                    for (int x = 0; x < _width; x++)
                    {
                        int nearBombs = 0;

                        for (int h = -1; h <= 1; h++)
                            for (int i = -1; i <= 1; i++)
                                for (int j = -1; j <= 1; j++)
                                    if (!(x + h >= _width || y + i >= _height || z + j >= _length || x + h < 0 || y + i < 0 || z + j < 0) && (h != 0 || i != 0 || j != 0))
                                        if (_cellsArray[x + h, y + i, z + j].IsBomb)
                                            nearBombs++;

                        _cellsArray[x, y, z].NearBombsNumber = nearBombs;
                    }
                }
            }
        }

        public bool FlagSwitch(Cell linkedCell)
        {
            bool isInteractable = !IsGameOver && linkedCell.IsCovered && !IsPaused;

            if (isInteractable)
                if (linkedCell.IsCovered)
                    linkedCell.HasFlag = !linkedCell.HasFlag;

            return isInteractable;
        }

        public bool DiscoverCell(Cell linkedCell)
        {
            bool isInteractable = !linkedCell.HasFlag && !IsGameOver && linkedCell.IsCovered && !_isPaused;
            if (isInteractable)
            {
                linkedCell.IsCovered = false;

                if (CoveredCellsNumber == (Width * Height * Length))
                    //BuildBombs(linkedCell);

                //consider bomb cells as always covered, this as no impact on game, useful for debug, where you can continue playing after touching a bomb
                if (BombNumber < CoveredCellsNumber)
                    CoveredCellsNumber--;


                if (linkedCell.IsBomb)
                {
                    Loose();
                }
                else if (BombNumber == CoveredCellsNumber)
                {
                    Win();
                }
                else if (linkedCell.NearBombsNumber == 0)
                {
                    int x = linkedCell.Position.x;
                    int y = linkedCell.Position.y;
                    int z = linkedCell.Position.z;

                    for (int h = -1; h <= 1; h++)
                        for (int i = -1; i <= 1; i++)
                            for (int j = -1; j <= 1; j++)
                                if (!(x + h >= _width || y + i >= _height || z + j >= _length || x + h < 0 || y + i < 0 || z + j < 0) && (h != 0 || i != 0 || j != 0))
                                    DiscoverCell(CellsArray[x + h, y + i, z + j]);
                }
            }

            return isInteractable;
        }

        private void Win()
        {
            IsGameOver = true;
            _hasWin = true;

            DiscoverAll();
        }

        private void Loose()
        {
            IsGameOver = true;
            _hasWin = false;

            DiscoverAll();
        }

        private void DiscoverAll()
        {
            foreach (Cell _cell in _cellsArray)
                _cell.IsCovered = false;
        }

        public void PauseSwitch()
        {
            _isPaused = !_isPaused;
        }
    }
}