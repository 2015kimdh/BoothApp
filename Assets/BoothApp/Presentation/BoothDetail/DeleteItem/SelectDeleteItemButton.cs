using System;
using BoothApp.Presentation.View;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;
using UnityEngine.UI;

namespace BoothApp.Presentation.BoothDetail.DeleteItem
{
    public class SelectDeleteItemButton : MonoBehaviour
    {
        #region SerializeField

        [SerializeField] private PurchasableItem purchasableItem;
        [SerializeField] private UIButton button;
        [SerializeField] private GameObject selectedMark;

        #endregion

        #region Private Field

        private DeletedSelectedItemViewModel _deletedSelectedItemViewModel;
        private SelectedBoothView _selectedBoothView;

        private bool _isSelected = false;

        #endregion

        #region MonoBehaviour Event

        private void Awake()
        {
            _deletedSelectedItemViewModel = FindObjectOfType<DeletedSelectedItemViewModel>();
            _selectedBoothView = ViewHub.Views.Find(x => x.GetType() == typeof(SelectedBoothView)) as SelectedBoothView;
            _selectedBoothView.onViewStatusChange.AddListener(EventBinding);
            button.onClickEvent.AddListener(ButtonOperation);
            _deletedSelectedItemViewModel.onSelectItem.AddListener(SetSelectedStatus);
        }

        private void OnDestroy()
        {
            _selectedBoothView.onViewStatusChange.RemoveListener(EventBinding);
            _deletedSelectedItemViewModel.onSelectItem.RemoveListener(SetSelectedStatus);
        }

        #endregion

        #region Private Method

        private void EventBinding(SelectedBoothViewStatus status)
        {
            if (status == SelectedBoothViewStatus.DeleteItem)
            {
                _isSelected = false;
            }
            else
            {
                selectedMark.SetActive(false);
            }
        }

        private void SetSelectedStatus()
        {
            _isSelected = _deletedSelectedItemViewModel.CheckIsSelected(purchasableItem);
            selectedMark.SetActive(_isSelected);
        }

        private void ButtonOperation()
        {
            if (_selectedBoothView.ViewStatus == SelectedBoothViewStatus.DeleteItem)
            {
                if (!_isSelected)
                    _deletedSelectedItemViewModel.AddSelection(purchasableItem);
                else
                    _deletedSelectedItemViewModel.RemoveSelection(purchasableItem);
            }
        }

        #endregion
    }
}