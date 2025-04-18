using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory
{
    public class TotalInvoiceView : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private PurchaseHistoryViewModel viewModel;

        [SerializeField] private TMP_Text invoiceText;

        #endregion

        #region Method

        private void Awake()
        {
            viewModel.onDelete.AddListener(SetInvoice);
        }

        public void SetInvoice()
        {
            invoiceText.text = ((int)Calculate()).ToString();
        }

        private float Calculate()
        {
            return viewModel.PurchaseHistory
                .SelectMany(receipt => receipt.items)
                .Sum(item => item.pricePerItem * item.amount);
        }
        
        #endregion
    }
}