using System.Collections.Generic;
using UnityEngine;

namespace MineSweeper3D
{
    ///<summary>
    /// (beta) saves for best times records
    ///</summary>
    [CreateAssetMenu(fileName = "NewTimeSaves", menuName = "TimeSaves")]
    public class TimeSaves : ScriptableObject
    {
        [SerializeField, Tooltip("list of best times")]
        private List<float> _bestTimes;
    } 
}