using UnityEngine;

namespace MineSweeper3D.Classic
{
    ///<summary>
    /// renderer for every game cells, making textures, and collision detections
    ///</summary>
    [AddComponentMenu("MineSweeper3D/Classic/CellRenderer")]
    public class CellRenderer : MonoBehaviour
    {
        private Cell _linkedCell = null;

        private Levelrenderer levelRenderer;

        [SerializeField]
        private Sprite _coverCellSprite;

        [SerializeField]
        private Sprite _coverCellFlagSprite;

        [SerializeField]
        private Sprite _cellSprite;

        [SerializeField]
        private Sprite _cellBombSprite;

        [SerializeField]
        private SpriteRenderer _cellTile;


        public Cell LinkedCell
        {
            get { return _linkedCell; }
            set { _linkedCell = value; }
        }

        private void Start()
        {
            levelRenderer = FindObjectOfType<Levelrenderer>();
        }

        private void Update()
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
                if(_linkedCell.IsBomb)
                    _cellTile.sprite = _cellBombSprite;
                else
                    _cellTile.sprite = _cellSprite;
            }
        }

        private void OnMouseOver()
        {
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (_linkedCell.IsCovered)
                    _linkedCell.HasFlag = !_linkedCell.HasFlag;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!_linkedCell.HasFlag)
                {
                    _linkedCell.IsCovered = false;

                    if (levelRenderer.BombNumber < levelRenderer.CoveredCellsNumber)
                        levelRenderer.CoveredCellsNumber--;
                    
                    if (levelRenderer.BombNumber == levelRenderer.CoveredCellsNumber)
                        Debug.Log("Win !");

                    if (_linkedCell.IsBomb)
                        Debug.Log("YOU LOSE, WHY IS AGA SOFIA THIS, AND NOT BEEEUUU !");
                }
            }
        }
    } 
}
