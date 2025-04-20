using System.Linq;
using BoothApp.Presentation.BoothDetail.PurchaseHistory;
using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseReceiptDetail
{
    public class DetailReceiptInvoiceView : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private PurchaseHistoryViewModel viewModel;
        [SerializeField] private TMP_Text invoiceText;
        
        #endregion
        
        #region Method

        public void SetInvoice()
        {
            invoiceText.text = ((int)Calculate()).ToString();
        }

        private float Calculate()
        {
            return viewModel.SelectedItem.receiptInfo.items
                .Sum(item => item.pricePerItem * item.amount);
        }
        
        #endregion
    }
}