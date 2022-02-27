using System.Collections.Generic;
using UnityEngine;

namespace MineSweeper3D
{
    ///<summary>
    /// 
    ///</summary>
    [CreateAssetMenu(fileName = "NewTimeSaves", menuName = "TimeSaves")]
    public class TimeSaves : ScriptableObject
    {
        [SerializeField]
        private List<float> _bestTimes;
    } 
}