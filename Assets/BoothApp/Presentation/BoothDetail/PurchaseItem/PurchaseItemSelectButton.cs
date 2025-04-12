using System;
using System.Collections;
using BoothApp.Presentation.View;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseItem
{
    public class PurchaseItemSelectButton : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private UIButton button;
        [SerializeField] private PurchasableItem item;
        [SerializeField] private float deselectItemTime;

        #endregion

        #region Private Field

        private PurchaseSelectedItemViewModel _viewModel;
        private SelectedBoothView _selectedBoothView;
        [SerializeField] private float _buttonPressedTime = 0f;
        private Coroutine _timerCoroutine;

        private bool _isLongClicked = false;
        
        #endregion

        #region MonoBehaviourEvent

        private void Awake()
        {
            _selectedBoothView = ViewHub.Views.Find(x => x.GetType() == typeof(SelectedBoothView)) as SelectedBoothView;
            _viewModel = FindObjectOfType<PurchaseSelectedItemViewModel>();
            _viewModel.onPurchase.AddListener(item.RefreshOriginalAmount);
            _selectedBoothView.onViewStatusChange.AddListener(SetZeroTryAmount);
            button.onPointerDownEvent.AddListener(() => { _isLongClicked = false; });
            button.onLongClickEvent.AddListener(SelectedItemDeselection);
            button.onClickEvent.AddListener(SelectButtonClick);
        }

        private void OnDestroy()
        {
            _viewModel.onPurchase.RemoveListener(item.RefreshOriginalAmount);
            _selectedBoothView.onViewStatusChange.RemoveListener(SetZeroTryAmount);
        }

        #endregion

        #region Method
        
        private void ClickSelectButton()
        {
            if (_selectedBoothView.ViewStatus == SelectedBoothViewStatus.Normal ||
                _selectedBoothView.ViewStatus == SelectedBoothViewStatus.Purchase)
            {
                _selectedBoothView.ViewStatus = SelectedBoothViewStatus.Purchase;
            }
        }

        private void SelectedItemDeselection()
        {
            item.TryToPurchaseAmount = 0;
            _viewModel.CancelPurchaseItemInfo(item.hash);
            _isLongClicked = true;
        }

        private void SelectButtonClick()
        {
            ClickSelectButton();
            if (_selectedBoothView.ViewStatus == SelectedBoothViewStatus.Purchase)
            {
                if(_isLongClicked == false)
                    AddTryToPurchaseAmount();
            }
        }

        private void AddTryToPurchaseAmount()
        {
            item.TryToPurchaseAmount += 1;
            _viewModel.SetPurchaseItemInfo(item.hash, item.TryToPurchaseAmount);
        }


        private void SetZeroTryAmount(SelectedBoothViewStatus status)
        {
            if (status != SelectedBoothViewStatus.Purchase)
            {
                item.TryToPurchaseAmount = 0;
                _viewModel.CancelPurchaseItemInfo(item.hash);
            }
        }

        #endregion

        #region IEnumerator

        private IEnumerator SetTimer()
        {
            _buttonPressedTime = 0;
            while (true)
            {
                if (_buttonPressedTime >= deselectItemTime)
                {
                    SelectedItemDeselection();
                    break;
                }

                _buttonPressedTime += Time.deltaTime;
                yield return null;
            }
        }

        #endregion
    }
}