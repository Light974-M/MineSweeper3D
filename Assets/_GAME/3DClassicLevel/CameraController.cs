using UnityEngine;

namespace MineSweeper3D.Classic3D
{
    ///<summary>
    /// 
    ///</summary>
    [AddComponentMenu("MineSweeper3D/Classic3D/CameraController")]
    public class CameraController : MonoBehaviour
    {
        [SerializeField, Tooltip("")]
        private float _scrollSpeed = 0.5f;

        [SerializeField, Tooltip("camera used to render fps game view")]
        private Transform _camera;

        [SerializeField, Tooltip("player where camera is")]
        private Transform _player;

        [SerializeField, Tooltip("speed of mouse look in X and Y")]
        private Vector2 _lookSpeed = Vector2.one;


        private Vector2 _rotation = Vector2.zero;

        private void Update()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                _camera.localPosition = new Vector3(_camera.localPosition.x, _camera.localPosition.y, _camera.localPosition.z / (_scrollSpeed + 1));
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
                _camera.localPosition = new Vector3(_camera.localPosition.x, _camera.localPosition.y, _camera.localPosition.z * (_scrollSpeed + 1));

            if (Input.GetKey(KeyCode.Mouse2))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Look();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }

        private void Look()
        {
            Vector2 mouse = new Vector2(Input.GetAxis("Mouse X") * _lookSpeed.x, Input.GetAxis("Mouse Y") * _lookSpeed.y);
            _rotation += new Vector2(-mouse.y, mouse.x);

            _rotation.x = Mathf.Clamp(_rotation.x, -90, 90);

            _player.eulerAngles = new Vector3(_rotation.x, _rotation.y, 0.0f);
        }
    }
}
