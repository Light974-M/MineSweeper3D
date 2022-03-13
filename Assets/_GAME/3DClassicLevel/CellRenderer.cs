using UnityEngine;

namespace MineSweeper3D.Classic3D
{
    ///<summary>
    /// renderer for every game cells, making textures, and collision detections
    ///</summary>
    [AddComponentMenu("MineSweeper3D/Classic3D/CellRenderer")]
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
        private Collider _tileCollider;

        [SerializeField]
        private SpriteRenderer[] _cellTileArray;

        [SerializeField]
        private Sprite _blankCellTile;

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
                    AssignTexture(_coverCellFlagSprite);
                else
                    AssignTexture(_coverCellSprite);
            }
            else
            {
                _tileCollider.enabled = false;

                if (_linkedCell.IsBomb)
                    AssignTexture(_cellBombSprite, _numberTileList[0]);
                else
                {
                    if(_linkedCell.NearBombsNumber == 0)
                        AssignTexture(_blankCellTile);
                    else
                        AssignTexture(_numberTileList[_linkedCell.NearBombsNumber], _numberTileList[0]);
                }
            }

            _hasBombDebug = _linkedCell.IsBomb;
            _nearBombNumberDebug = _linkedCell.NearBombsNumber;
        }

        private void AssignTexture(Sprite toAssign, Sprite blank)
        {
            foreach (SpriteRenderer _cellTile in _cellTileArray)
                _cellTile.sprite = blank;

            _cellTileArray[_cellTileArray.Length - 1].sprite = toAssign;
        }

        private void AssignTexture(Sprite toAssign)
        {
            foreach (SpriteRenderer _cellTile in _cellTileArray)
                _cellTile.sprite = toAssign;
        }
    }
}
