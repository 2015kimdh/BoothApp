using System;
using BoothApp.Presentation.Info;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail
{
    public class SelectedBooth : MonoBehaviour
    {
        public UnityEvent<string> onSelected;

        public BoothInfo selectedBooth = new();

        private BoothDataPresenter _presenter;

        private void Awake()
        {
            _presenter = FindObjectOfType<BoothDataPresenter>();
            onSelected.AddListener(SetSelectedBoothName);
        }

        private void SetSelectedBoothName(string boothName)
        {
            selectedBooth = _presenter.boothInfo.Find(x => x.boothInformationInfo.boothName == boothName);
        }
    }
}