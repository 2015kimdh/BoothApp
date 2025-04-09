using System;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseItem
{
    public class PurchaseItemTryToPurchaseText : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private GameObject textObject;

        [SerializeField] private PurchasableItem item;

        #endregion

        #region Private Field

        private PurchaseSelectedItemViewModel _viewModel;

        #endregion

        #region Method

        private void Awake()
        {
            _viewModel = FindObjectOfType<PurchaseSelectedItemViewModel>();
            _viewModel.onSetSuccess.AddListener(SetActiveText);
            _viewModel.onPurchase.AddListener(SetActiveText);
        }

        private void OnDestroy()
        {
            _viewModel.onSetSuccess.RemoveListener(SetActiveText);
            _viewModel.onPurchase.RemoveListener(SetActiveText);
        }

        private void SetActiveText()
        {
            if (item.TryToPurchaseAmount == 0)
            {
                textObject.SetActive(false);
            }
            else
            {
                textObject.SetActive(true);
            }
        }
        
        #endregion
    }
}