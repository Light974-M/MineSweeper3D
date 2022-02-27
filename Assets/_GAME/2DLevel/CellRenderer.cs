using UnityEngine;

namespace MineSweeper3D.Classic
{
    ///<summary>
    /// renderer for every game cells, making textures, and collision detections
    ///</summary>
    [AddComponentMenu("MineSweeper3D/Classic/CellRenderer")]
    public class CellRenderer : MonoBehaviour
    {
        [Header("INPUT TEXTURES\n")]

        [SerializeField]
        private Sprite _coverCellSprite;

        [SerializeField]
        private Sprite _coverCellFlagSprite;

        [SerializeField]
        private Sprite _cellBombSprite;

        [SerializeField]
        private SpriteRenderer _cellTile;

        [SerializeField]
        private Sprite[] _numberTileList;

        [Header("DEBUG\n")]

        [SerializeField]
        private bool _hasBombDebug = false;

        [SerializeField]
        private int _nearBombNumberDebug = 0;

        private Cell _linkedCell = null;

        private LevelRenderer _levelRenderer;

        #region Public API

        public Cell LinkedCell
        {
            get { return _linkedCell; }
            set { _linkedCell = value; }
        }

        #endregion

        private void Start()
        {
            _levelRenderer = FindObjectOfType<LevelRenderer>();

            _hasBombDebug = _linkedCell.IsBomb;
            _nearBombNumberDebug = _linkedCell.NearBombsNumber;
        }

        private void OnMouseOver()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
                if (_levelRenderer.Level.FlagSwitch(_linkedCell))
                    GraphicUpdate();

            if (Input.GetKeyDown(KeyCode.Mouse0))
                if (_levelRenderer.Level.DiscoverCell(_linkedCell))
                    _levelRenderer.GraphicsUpdate();
        }

        public void GraphicUpdate()
        {
            if (_linkedCell.IsCovered)
            {
                if (_linkedCell.HasFlag)
                    _cellTile.sprite = _coverCellFlagSprite;
                else
                    _cellTile.sprite = _coverCellSprite;
            }
            else
            {
                if (_linkedCell.IsBomb)
                    _cellTile.sprite = _cellBombSprite;
                else
                    _cellTile.sprite = _numberTileList[_linkedCell.NearBombsNumber];
            }

            _hasBombDebug = _linkedCell.IsBomb;
            _nearBombNumberDebug = _linkedCell.NearBombsNumber;
        }
    }
}
