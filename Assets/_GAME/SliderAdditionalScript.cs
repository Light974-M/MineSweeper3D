using UnityEngine;
using UnityEngine.UI;

namespace MineSweeper3D
{
    ///<summary>
    /// additional script for generic slider process
    ///</summary>
    [AddComponentMenu("SliderAdditionalScript")]
    public class SliderAdditionalScript : MonoBehaviour
    {

        [SerializeField, Tooltip("text UI used to render slider value")]
        private Text _valueUI;

        //called every frame
        private void Update()
        {
            //set text UI to slider value
            _valueUI.text = Mathf.Round(GetComponent<Slider>().value).ToString();
        }
    } 
}
