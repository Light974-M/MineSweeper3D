using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MineSweeper3D.Classic
{
    ///<summary>
    /// 
    ///</summary>
    [AddComponentMenu("MenuController")]
    public class MenuController : MonoBehaviour
    {

        [SerializeField, Tooltip("list of all panel used for every part of the menu")]
        private GameObject[] _panelList;

        /// <inheritdoc cref="_panelDrawed"/>
        public enum _panel
        {
            Main,
            LevelChoose,
            ClassicMinesweeper,
        }

        [SerializeField, Tooltip("give the current loaded panel")]
        private _panel _panelDrawed;

        [SerializeField, Tooltip("")]
        private LevelParameters _levelParameters;


        [SerializeField, Tooltip("")]
        private Slider _widthSlider;

        [SerializeField, Tooltip("")]
        private Slider _heightSlider;

        [SerializeField, Tooltip("")]
        private Slider _bombSlider;

        private void Awake()
        {
            _widthSlider.value = _levelParameters.Width;
            _heightSlider.value = _levelParameters.Height;
            _bombSlider.value = _levelParameters.BombNumber;
        }

        private void Update()
        {
            _levelParameters.Width = (int)Mathf.Round(_widthSlider.value);
            _levelParameters.Height = (int)Mathf.Round(_heightSlider.value);
            _levelParameters.BombNumber = (int)Mathf.Round(_bombSlider.value);
            _bombSlider.maxValue = _levelParameters.Width * _levelParameters.Height;
        }

        public void OnPlayButton()
        {
            _panelDrawed = _panel.LevelChoose;
            PanelUpdate();
        }

        public void OnClassicMSButton()
        {
            _panelDrawed = _panel.ClassicMinesweeper;

            PanelUpdate();
        }

        public void OnReturnButton()
        {
            if((int)_panelDrawed == 1 || (int)_panelDrawed == 2)
            {
                _panelDrawed--;
            }

            PanelUpdate();
        }

        public void OnStartButton()
        {
            SceneManager.LoadScene("2DLevel");
        }

        public void OnQuitButton()
        {
            Application.Quit();
        }

        private void PanelUpdate()
        {
            for (int i = 0; i < _panelList.Length; i++)
            {
                if (i == (int)_panelDrawed)
                    _panelList[i].SetActive(true);
                else
                    _panelList[i].SetActive(false);
            }
        }
    } 
}
