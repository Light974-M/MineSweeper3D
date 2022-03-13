using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MineSweeper3D.Classic2D
{
    ///<summary>
    /// 
    ///</summary>
    [AddComponentMenu("MineSweeper3D/Classic/UIRenderer")]
    public class UIRenderer : MonoBehaviour
    {

        [SerializeField, Tooltip("")]
        private Text _timerUI;

        [SerializeField, Tooltip("")]
        private GameObject _pauseUIMenu;

        [SerializeField, Tooltip("")]
        private GameObject _retryUIMenu;

        [SerializeField, Tooltip("")]
        private Text _retryMessage;

        [SerializeField, Tooltip("")]
        private LevelRenderer _levelRenderer;

        private void Update()
        {
            if (_levelRenderer.Level.IsGameOver || _levelRenderer.Level.IsPaused)
            {
                RetryAndPauseMenuRenderer();
            }
            else
            {
                _retryUIMenu.SetActive(false);
                _pauseUIMenu.SetActive(false);
            }

            TimerUIUpdate();
        }

        private void RetryAndPauseMenuRenderer()
        {
            if (_levelRenderer.Level.IsPaused)
            {
                _pauseUIMenu.SetActive(true);
            }
            else
            {
                _retryUIMenu.SetActive(true);

                if (_levelRenderer.Level.HasWin)
                {
                    _retryMessage.text = "Win";
                }
                else
                {
                    _retryMessage.text = "YOU LOOSE";
                }
            }
        }

        private void TimerUIUpdate()
        {
            _timerUI.text = (Mathf.Round(_levelRenderer.Timer * 1000) / 1000).ToString();
        }

        public void OnRetryButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void OnResumeButton()
        {
            _levelRenderer.Level.IsPaused = false;
        }

        public void OnQuitButton()
        {
            SceneManager.LoadScene("Menu");
        }
    } 
}
