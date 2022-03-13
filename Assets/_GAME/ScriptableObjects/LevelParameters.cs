using UnityEngine;

namespace MineSweeper3D
{
    ///<summary>
    /// parameter of level that will be used between scene loads
    ///</summary>
    [CreateAssetMenu(fileName = "NewLevelParameters", menuName = "LevelParameters")]
    public class LevelParameters : ScriptableObject
    {
        [SerializeField, Tooltip("width of level in x")]
        private int _width = 3;

        [SerializeField, Tooltip("height of level in y")]
        private int _height = 3;

        [SerializeField, Tooltip("length of level(not in 2D)")]
        private int _length = 3;

        [SerializeField, Tooltip("number of bombs that will be load(clamped to cells number)")]
        private int _bombNumber = 2;


        #region public API

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }

        public int BombNumber
        {
            get { return _bombNumber; }
            set { _bombNumber = value; }
        }

        #endregion
    }
}