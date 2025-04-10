using System;
using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseItem
{
    public class DisplayCalculatedExchange : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private PurchaseSelectedItemViewModel viewModel;
        [SerializeField] private TMP_Text targetText;

        #endregion

        #region MonoBehaviour Event

        private void Awake()
        {
            viewModel.onSetSuccess.AddListener(SetDisplayedExchange);
        }

        #endregion

        #region Method

        private void SetDisplayedExchange()
        {
            var exchange = CalculateTotalPurchaseExchange.CalculateExchange(viewModel.PurchaseItemInfos);
            targetText.text = exchange.ToString();
        }

        #endregion
    }
}