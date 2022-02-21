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

        #endregion


        #region public API

        public int Width => _width;
        public int Height => _height;

        public Cell[,] CellsArray => _cellsArray;

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
            _bombNumber = bombNumber;

            _cellsArray = new Cell[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //int bombProba = Random.Range(0, 3);
                    //bool isBomb = (bombProba == 0);
                    _cellsArray[x, y] = new Cell(x, y, true, false, false, 0);
                }
            }

            //for (int i = 0; i < bombNumber; i++)
            //{
            //    bool isPassed = false;

            //    while (!isPassed)
            //    {
            //        int bombPosHeight = Random.Range(0, height);
            //        int bombPoswidth = Random.Range(0, width);

            //        if (!_cellsArray[bombPosHeight, bombPoswidth].IsBomb)
            //        {
            //            _cellsArray[bombPosHeight, bombPoswidth].IsBomb = true;
            //            isPassed = true;
            //        } 
            //    }
            //}


        }

        //public Cell this[int x, int y] => _cellsArray[x, y];
    }
}