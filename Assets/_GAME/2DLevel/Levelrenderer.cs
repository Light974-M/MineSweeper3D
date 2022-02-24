using UnityEngine;

namespace MineSweeper3D.Classic
{
    ///<summary>
    /// renderer of minesweeper2D levelMap for unityEngine
    ///</summary>
    [AddComponentMenu("MineSweeper3D/Classic/Level")]
    public class Levelrenderer : MonoBehaviour
    {
        #region variables

        private const int _intInfinity = 2147483647;

        [SerializeField, Tooltip("")]
        private int _width = 4;

        [SerializeField, Tooltip("")]
        private int _height = 4;

        [SerializeField]
        private GameObject _cellObjectPrefab;

        [SerializeField, Tooltip("")]
        private Camera _gameCamera;

        [SerializeField]
        private GameObject _cellsParentObject;

        [SerializeField]
        private int _bombNumber = 2;
        

        private Level _level = null;

        private float _timer = 0;


        #endregion

        #region public API

        //make a get of Level, and make at the same time sure that _level is not null, if it is, it will make a new level
        public Level Level
        {
            get
            {
                if (_level == null)
                    _level = new Level(_width, _height, _bombNumber);

                return _level;
            }
        }

        #endregion

        private void Awake()
        {
            LevelBuild();

            //delete when true proba system finished
            _bombNumber = Level.BombNumber;
        }

        private void Update()
        {
            if (!_level.IsGameOver)
            {
                _timer += Time.deltaTime;
            }
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                if (_gameCamera == null)
                    _gameCamera = FindObjectOfType<Camera>();

                if (_cellsParentObject == null)
                {
                    if (transform.Find("Cells") == null)
                    {
                        _cellsParentObject = Instantiate(new GameObject("Cells"));
                        _cellsParentObject.transform.SetParent(transform);
                    }
                    else
                    {
                        _cellsParentObject = transform.Find("Cells").gameObject;
                    } 
                }

                _width = Mathf.Clamp(_width, 1, _intInfinity);
                _height = Mathf.Clamp(_height, 1, _intInfinity);

                _gameCamera.transform.position = new Vector3((Level.Width / 2) * transform.localScale.x, (Level.Height / 2) * transform.localScale.y,  -100) + transform.position;
                _gameCamera.orthographicSize = (Level.Width + Level.Height) / 2.5f;

                for (int y = 0; y < Level.Height; y++)
                {
                    for (int x = 0; x < Level.Width; x++)
                    {
                        Gizmos.DrawWireCube((new Vector3(x * transform.localScale.x, y * transform.localScale.y) + transform.position), Vector2.one * transform.localScale);
                    }
                }

                if ((_width != Level.Width || _height != Level.Height))
                {
                    _level = null; 
                }
            }
        }

        private void LevelBuild()
        {
            _level = new Level(_width, _height, _bombNumber);

            for (int y = 0; y < Level.Height; y++)
            {
                for (int x = 0; x < Level.Width; x++)
                {
                    GameObject cellPrefab = Instantiate(_cellObjectPrefab, new Vector3(x * transform.localScale.x, y * transform.localScale.y) + transform.position, Quaternion.identity);
                    CellRenderer cellScript = cellPrefab.GetComponent<CellRenderer>();

                    cellPrefab.transform.SetParent(_cellsParentObject.transform);
                    cellScript.LinkedCell = Level.CellsArray[x, y];

                    if (Level.CellsArray[x, y].IsBomb)
                        _bombNumber++;
                }
            }
        }

        public void GraphicsUpdate()
        {
            CellRenderer[] cellRendererArray = FindObjectsOfType<CellRenderer>();

            foreach (CellRenderer cellRenderer in cellRendererArray)
            {
                cellRenderer.GraphicUpdate();
            }
        }
    } 
}
