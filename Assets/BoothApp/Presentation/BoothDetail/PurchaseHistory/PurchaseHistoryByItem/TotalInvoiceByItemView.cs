using System.Linq;
using BoothApp.Presentation.BoothDetail.BoothItemView;
using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory.PurchaseHistoryByItem
{
    public class TotalInvoiceByItemView : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private PurchaseHistoryByItemViewModel viewModel;

        [SerializeField] private TMP_Text invoiceText;

        #endregion

        #region Method

        public void SetInvoice()
        {
            invoiceText.text = ((int)Calculate()).ToString();
        }

        // 활성화 된 객체들만 계산함
        private float Calculate()
        {
            var filteredItemResult = FilteringItem.FilteringWithTag(viewModel.PurchasedItemStatus,
                viewModel.FilterAttribute.selectedItemTags);
            filteredItemResult = FilteringItem.FilteringWithOwner(filteredItemResult,
                viewModel.FilterAttribute.selectedOwner);
            var filteredReceipt = viewModel.FilteredReceipt;

            filteredItemResult = filteredItemResult
                .Where(x => viewModel.GetTotalAmount(filteredReceipt, x.itemInfo) != 0)
                .ToList();
            int totalInvoice = 0;
            for (int i = 0; i < filteredItemResult.Count; i++)
                totalInvoice += viewModel.GetTotalInvoice(viewModel.FilteredReceipt, filteredItemResult[i].itemInfo);

            return totalInvoice;
        }

        #endregion
    }
}