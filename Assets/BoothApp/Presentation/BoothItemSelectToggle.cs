using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoothApp.Presentation
{
    public class BoothItemSelectToggle : MonoBehaviour
    {
        #region Property
        
        public bool toggleValue => toggle.isOn;

        #endregion
        
        #region Serialize Field

        [SerializeField] private Toggle toggle;
        [SerializeField] private TMP_Text itemName;
        
        #endregion

        #region Method

        public void SetActiveToggle()
        {
            toggle.gameObject.SetActive(true);
            toggle.isOn = false;
        }

        public void SetDisableToggle()
        {
            toggle.gameObject.SetActive(false);
        }
        
        #endregion
    }
}