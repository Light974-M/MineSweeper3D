using UnityEngine;

namespace MineSweeper3D.Classic3D
{
    ///<summary>
    /// 
    ///</summary>
    [AddComponentMenu("MineSweeper3D/Classic3D/AutoTargetCamera")]
    public class AutoTargetCamera : MonoBehaviour
    {
        private void Update()
        {
            transform.LookAt(Camera.main.transform.position);
        }
    } 
}
