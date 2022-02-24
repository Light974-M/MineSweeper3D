using UnityEngine;

namespace MineSweeper3D.Classic
{
    ///<summary>
    /// Level GridMap for minesweeper 2D classic, contain all cells of game.
    ///</summary>
    public class Level
    {
        #region variables

        private Cell[,] _cellsArray = null;

        private int _width = 3;

        private int _height = 3;

        private int _bombNumber = 2;

        private int _coveredCellsNumber = 0;

        private bool _isGameOver = false;

        private int _score = 0;

        #endregion


        #region public API

        public int Width => _width;
        public int Height => _height;

        public int BombNumber => _bombNumber;

        public Cell[,] CellsArray => _cellsArray;

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

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        #endregion

        /// <summary>
        /// Constructor for current level map
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Level(int width, int height, int bombNumber)
        {
            _width = width;
            _height = height;
            _bombNumber = 0;

            _cellsArray = new Cell[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int bombProba = Random.Range(0, 6);
                    bool ProbIsBomb = (bombProba == 0);

                    _cellsArray[x, y] = new Cell(x, y, true, ProbIsBomb, false, 0);

                    if (_cellsArray[x, y].IsBomb)
                        _bombNumber++;

                    _coveredCellsNumber++;
                }
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int nearBombs = 0;

                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                            if (!(x + i >= width || y + j >= height || x + i < 0 || y + j < 0) && (i != 0 || j != 0))
                                if (_cellsArray[x + i, y + j].IsBomb)
                                    nearBombs++;

                    _cellsArray[x, y].NearBombsNumber = nearBombs;
                }
            }


            //for (int i = 0; i < bombNumber; i++)
            //{
            //    int bombPos = Random.Range(0, (width * height) - i);
            //    int bombPosHeight = bombPos / width;
            //    int bombPoswidth = bombPos - width * bombPosHeight;

            //    if (!_cellsArray[bombPosHeight, bombPoswidth].IsBomb)
            //    {
            //        _cellsArray[bombPosHeight, bombPoswidth].IsBomb = true;
            //    }
            //    else
            //    {
            //        while (_cellsArray[bombPosHeight, bombPoswidth].IsBomb)
            //        {
            //            bombPos++;
            //            bombPosHeight = bombPos / width;
            //            bombPoswidth = bombPos - width * bombPosHeight;

            //            if (bombPoswidth >= width || bombPosHeight >= height)
            //            {
            //                bombPos = 0;
            //                bombPosHeight = bombPos / width;
            //                bombPoswidth = bombPos - width * bombPosHeight;
            //            }

            //            if (!_cellsArray[bombPosHeight, bombPoswidth].IsBomb)
            //                _cellsArray[bombPosHeight, bombPoswidth].IsBomb = true;
            //        }
            //    }
            //}
        }

        public void BuildBombs()
        {
            Debug.Log("BUILDING BOMBS...");
        }

        public bool FlagSwitch(Cell linkedCell)
        {
            if (!IsGameOver && linkedCell.IsCovered)
            {
                if (linkedCell.IsCovered)
                    linkedCell.HasFlag = !linkedCell.HasFlag;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DiscoverCell(Cell linkedCell)
        {
            if (!linkedCell.HasFlag && !IsGameOver && linkedCell.IsCovered)
            {
                linkedCell.IsCovered = false;

                if (CoveredCellsNumber == (Width * Height))
                    BuildBombs();

                //consider bomb cells as always covered, this as no impact on game, useful for debug, where you can continue playing after touching a bomb
                if (BombNumber < CoveredCellsNumber)
                    CoveredCellsNumber--;

                if (linkedCell.IsBomb)
                {
                    IsGameOver = true;
                    Score = 0;
                    Debug.Log("lose");
                }
                else if (BombNumber == CoveredCellsNumber)
                {
                    IsGameOver = true;
                    Debug.Log("win");
                }
                else if (linkedCell.NearBombsNumber == 0)
                {
                    int x = linkedCell.Position.x;
                    int y = linkedCell.Position.y;

                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                            if (!(x + i >= _width || y + j >= _height || x + i < 0 || y + j < 0) && (i != 0 || j != 0))
                                DiscoverCell(CellsArray[x + i, y + j]);
                }

                return true;
            }
            else
            {
                return false;
            }

        }
    }
}