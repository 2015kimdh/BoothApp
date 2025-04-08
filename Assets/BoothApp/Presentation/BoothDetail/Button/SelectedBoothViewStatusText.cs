using System.Collections.Generic;
using BoothApp.Presentation.View;
using Doozy.Runtime.UIManager.Components;
using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.Button
{
    public class SelectedBoothViewStatusText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private string modifyModeString;
        [SerializeField] private string purchaseModeString;
        [SerializeField] private string deleteModeString;
        private SelectedBoothView _view;

        private void Start()
        {
            _view = ViewHub.Views.Find(x => x.GetType() == typeof(SelectedBoothView)) as SelectedBoothView;
            _view.onViewStatusChange.AddListener(SetStatusText);
        }

        private void SetStatusText(SelectedBoothViewStatus status)
        {
            switch (status)
            {
                case SelectedBoothViewStatus.Normal:
                    text.text = _view.selectedBoothViewModel.selectedBooth.boothInformationInfo.boothName;
                    break;
                case SelectedBoothViewStatus.Modify:
                    text.text = modifyModeString;
                    break;
                case SelectedBoothViewStatus.Purchase:
                    text.text = purchaseModeString;
                    break;
                case SelectedBoothViewStatus.DeleteItem:
                    text.text = deleteModeString;
                    break;
            }
        }
    }
}