using UnityEngine;
using UnityEngine.UI;

namespace MineSweeper3D.Classic3D
{
    ///<summary>
    /// renderer of minesweeper2D levelMap for unityEngine
    ///</summary>
    [AddComponentMenu("MineSweeper3D/Classic/Level")]
    public class LevelRenderer : MonoBehaviour
    {
        #region variables

        private const int _intInfinity = 2147483647;


        [Header("USER PARAMETERS")]
        [SerializeField, Tooltip("")]
        private LevelParameters _levelParameters;


        [Header("LEVEL PARAMETERS")]
        [SerializeField]
        private GameObject _cellObjectPrefab;

        [SerializeField]
        private GameObject _cellsParentObject;


        [Header("TOOLS PARAMETERS")]
        [SerializeField, Tooltip("")]
        private Transform _gameCamera;


        [Header("DEBUG")]
        [SerializeField]
        private int _cellCoveredNumberDebug = 0;

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
                    _level = new Level(_levelParameters.Width, _levelParameters.Height, _levelParameters.Length, _levelParameters.BombNumber);

                return _level;
            }
        }

        public float Timer
        {
            get { return _timer; }
            set { _timer = value; }
        }

        #endregion

        private void Awake()
        {
            _gameCamera.position = new Vector3((Level.Width / 2f - 0.5f) * transform.localScale.x, (Level.Height / 2f - 0.5f) * transform.localScale.y, (Level.Length / 2f - 0.5f) * transform.localScale.z) + transform.position;

            LevelBuild();
        }

        private void Update()
        {
            if (!Level.IsPaused && !Level.IsGameOver && (Level.CoveredCellsNumber != (Level.Width * Level.Height * Level.Length)))
                TimerUpdate();



            PauseInputManager();
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
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

                _levelParameters.Width = Mathf.Clamp(_levelParameters.Width, 1, _intInfinity);
                _levelParameters.Height = Mathf.Clamp(_levelParameters.Height, 1, _intInfinity);
                _levelParameters.Length = Mathf.Clamp(_levelParameters.Length, 1, _intInfinity);

                _gameCamera.position = new Vector3((Level.Width / 2f - 0.5f) * transform.localScale.x, (Level.Height / 2f - 0.5f) * transform.localScale.y, (Level.Length / 2f - 0.5f) * transform.localScale.z) + transform.position;

                for (int z = 0; z < Level.Length; z++)
                {
                    for (int y = 0; y < Level.Height; y++)
                    {
                        for (int x = 0; x < Level.Width; x++)
                        {
                            Gizmos.DrawWireCube((new Vector3(x * transform.localScale.x, y * transform.localScale.y, z * transform.localScale.z) + transform.position), new Vector3(1 * transform.localScale.x, 1 * transform.localScale.y, 1 * transform.localScale.z));
                        }
                    } 
                }

                if (_levelParameters.Width != Level.Width || _levelParameters.Height != Level.Height || _levelParameters.Length != Level.Length)
                {
                    _level = null;
                }
            }
        }

        private void LevelBuild()
        {
            _level = new Level(_levelParameters.Width, _levelParameters.Height, _levelParameters.Length, _levelParameters.BombNumber);

            for (int z = 0; z < Level.Length; z++)
            {
                for (int y = 0; y < Level.Height; y++)
                {
                    for (int x = 0; x < Level.Width; x++)
                    {
                        GameObject cellPrefab = Instantiate(_cellObjectPrefab, new Vector3(x * transform.localScale.x, y * transform.localScale.y, z * transform.localScale.z) + transform.position, Quaternion.identity);
                        CellRenderer cellScript = cellPrefab.GetComponent<CellRenderer>();

                        cellPrefab.transform.SetParent(_cellsParentObject.transform);
                        cellScript.LinkedCell = Level.CellsArray[x, y, z];

                        if (Level.CellsArray[x, y, z].IsBomb)
                            _levelParameters.BombNumber++;
                    }
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

            _levelParameters.BombNumber = Level.BombNumber;
            _cellCoveredNumberDebug = Level.CoveredCellsNumber;
        }

        private void TimerUpdate()
        {
            _timer += Time.deltaTime;
        }

        private void PauseInputManager()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                _level.PauseSwitch();
        }
    }
}
