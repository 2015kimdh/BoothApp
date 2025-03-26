using Doozy.Runtime.UIManager.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace BoothApp.Presentation.BoothDetail
{
    public class SelectedBoothSetter : MonoBehaviour
    {
        private UIButton _boothButton;
        private SelectedBooth _selectedBooth;
        [SerializeField]
        private BoothColumn boothColumn;

        private void Awake()
        {
            _boothButton = GetComponent<UIButton>();
            _selectedBooth = FindObjectOfType<SelectedBooth>();
            
            _boothButton.onClickEvent.AddListener(SetEvent);
        }

        private void SetEvent()
        {
            _selectedBooth.onSelected.Invoke(boothColumn.boothName.text);
        }
    }
}