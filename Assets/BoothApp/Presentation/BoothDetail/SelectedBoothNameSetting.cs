using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail
{
    public class SelectedBoothNameSetting : MonoBehaviour
    {
        #region Public Fields
        
        public TMP_Text boothNameTitle;

        #endregion

        #region Private Fields

        private SelectedBoothView _view;

        #endregion

        #region Method

        private void Start()
        {
            _view = FindObjectOfType<SelectedBoothView>();
            _view.onViewShow.AddListener(SetBoothNameText);
        }
        
        private void SetBoothNameText()
        {
            boothNameTitle.text = _view.selectedBooth.selectedBooth.boothInformationInfo.boothName;
        }

        #endregion
    }
}