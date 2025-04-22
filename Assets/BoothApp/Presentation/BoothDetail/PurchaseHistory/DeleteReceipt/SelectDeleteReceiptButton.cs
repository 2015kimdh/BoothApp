using BoothApp.Presentation.Info;
using BoothApp.Presentation.View;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory.DeleteReceipt
{
    public class SelectDeleteReceiptButton : MonoBehaviour
    {
        #region SerializeField

        [SerializeField] private PurchaseHistoryReceiptItem receiptItem;
        [SerializeField] private UIButton button;
        [SerializeField] private GameObject selectedMark;

        #endregion
        
        #region Private Field

        private DeleteSelectedReceiptViewModel _deleteSelectedReceiptViewModel;
        private PurchaseHistoryView _purchaseHistoryView;

        private bool _isSelected = false;

        #endregion
        
        #region MonoBehaviour Event

        private void Awake()
        {
            _deleteSelectedReceiptViewModel = FindObjectOfType<DeleteSelectedReceiptViewModel>();
            _purchaseHistoryView = ViewHub.Views.Find(x => x.GetType() == typeof(PurchaseHistoryView)) as PurchaseHistoryView;
            _purchaseHistoryView.onViewStatusChange.AddListener(EventBinding);
            button.onClickEvent.AddListener(ButtonOperation);
            _deleteSelectedReceiptViewModel.onSelectReceipt.AddListener(SetSelectedStatus);
        }

        private void OnDestroy()
        {
            _purchaseHistoryView.onViewStatusChange.RemoveListener(EventBinding);
            _deleteSelectedReceiptViewModel.onSelectReceipt.RemoveListener(SetSelectedStatus);
        }

        #endregion
        
        #region Private Method

        private void EventBinding(PurchaseHistoryViewStatus status)
        {
            if (status == PurchaseHistoryViewStatus.Delete)
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
            _isSelected = _deleteSelectedReceiptViewModel.CheckIsSelected(receiptItem);
            selectedMark.SetActive(_isSelected);
        }

        private void ButtonOperation()
        {
            if (_purchaseHistoryView.ViewStatus == PurchaseHistoryViewStatus.Delete)
            {
                if (!_isSelected)
                    _deleteSelectedReceiptViewModel.AddSelection(receiptItem);
                else
                    _deleteSelectedReceiptViewModel.RemoveSelection(receiptItem);
            }
        }

        #endregion
    }
}