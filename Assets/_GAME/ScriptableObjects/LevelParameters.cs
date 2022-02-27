using UnityEngine;

namespace MineSweeper3D
{
    ///<summary>
    /// 
    ///</summary>
    [CreateAssetMenu(fileName = "NewLevelParameters", menuName = "LevelParameters")]
    public class LevelParameters : ScriptableObject
    {
        [SerializeField, Tooltip("")]
        private int _width = 4;

        [SerializeField, Tooltip("")]
        private int _height = 4;

        [SerializeField, Tooltip("")]
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

        public int BombNumber
        {
            get { return _bombNumber; }
            set { _bombNumber = value; }
        }

        #endregion
    }
}