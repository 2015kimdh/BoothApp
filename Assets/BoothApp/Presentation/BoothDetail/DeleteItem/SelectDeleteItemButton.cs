using BoothApp.Presentation.View;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

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

        private DeleteSelectedItemViewModel _deleteSelectedItemViewModel;
        private SelectedBoothView _selectedBoothView;

        private bool _isSelected = false;

        #endregion

        #region MonoBehaviour Event

        private void Awake()
        {
            _deleteSelectedItemViewModel = FindObjectOfType<DeleteSelectedItemViewModel>();
            _selectedBoothView = ViewHub.Views.Find(x => x.GetType() == typeof(SelectedBoothView)) as SelectedBoothView;
            _selectedBoothView.onViewStatusChange.AddListener(EventBinding);
            button.onClickEvent.AddListener(ButtonOperation);
            _deleteSelectedItemViewModel.onSelectItem.AddListener(SetSelectedStatus);
        }

        private void OnDestroy()
        {
            _selectedBoothView.onViewStatusChange.RemoveListener(EventBinding);
            _deleteSelectedItemViewModel.onSelectItem.RemoveListener(SetSelectedStatus);
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
            _isSelected = _deleteSelectedItemViewModel.CheckIsSelected(purchasableItem);
            selectedMark.SetActive(_isSelected);
        }

        private void ButtonOperation()
        {
            if (_selectedBoothView.ViewStatus == SelectedBoothViewStatus.DeleteItem)
            {
                if (!_isSelected)
                    _deleteSelectedItemViewModel.AddSelection(purchasableItem);
                else
                    _deleteSelectedItemViewModel.RemoveSelection(purchasableItem);
            }
        }

        #endregion
    }
}