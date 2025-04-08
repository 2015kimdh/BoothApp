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
        private float _buttonPressedTime = 0f;
        private Coroutine _timerCoroutine;
        
        #endregion

        #region MonoBehaviourEvent

        private void Awake()
        {
            _selectedBoothView = ViewHub.Views.Find(x => x.GetType() == typeof(SelectedBoothView)) as SelectedBoothView;
            _viewModel = FindObjectOfType<PurchaseSelectedItemViewModel>();
            button.onPointerDownEvent.AddListener(ClickSelectButton);
        }

        #endregion

        #region Method

        private void ClickSelectButton()
        {
            if (_selectedBoothView.ViewStatus == SelectedBoothViewStatus.Normal ||
                _selectedBoothView.ViewStatus == SelectedBoothViewStatus.Purchase)
            {
                _selectedBoothView.ViewStatus = SelectedBoothViewStatus.Purchase;
                _timerCoroutine = StartCoroutine(SetTimer());
            }
        }

        private void SelectedItemDeselection()
        {
            item.TryToPurchaseAmount = 0;
        }
        
        private void SelectButtonUp()
        {
            if (_selectedBoothView.ViewStatus == SelectedBoothViewStatus.Purchase)
            {
                
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
                    break;
                _buttonPressedTime += Time.deltaTime;
                yield return null;
            }
        }

        #endregion
    }
}