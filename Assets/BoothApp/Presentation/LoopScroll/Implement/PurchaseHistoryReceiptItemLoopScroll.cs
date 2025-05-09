using System.Linq;
using BoothApp.Presentation.BoothDetail.PurchaseHistory;
using BoothApp.Presentation.Info;
using BoothApp.Utility;
using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.LoopScroll.Implement
{
    public class PurchaseHistoryReceiptItemLoopScroll : MonoBehaviour, ILoopScrollItem<PurchaseHistoryReceiptData>
    {
        #region Serialize Field

        [SerializeField] private TMP_Text purchasedAt;
        [SerializeField] private TMP_Text purchasedItemList;
        [SerializeField] private TMP_Text purchaseCost;
        [SerializeField] private TMP_Text receiptIndex;
        
        #endregion

        #region Property

        public PurchaseHistoryReceiptData receiptData = new();

        #endregion
        
        public void UpdateItem(PurchaseHistoryReceiptData data, int index)
        {
            receiptIndex.text = "No." + data.ReceiptIndex;
            receiptData = data;
            SetUI(data);
        }
        
        private void SetUI(PurchaseHistoryReceiptData data)
        {
            SetPurchasedAt(data.receiptInfo);
            SetPurchaseItemList(data);
            SetPurchaseCost(data.receiptInfo);
        }
        
        private void SetPurchasedAt(PurchaseReceiptInfo info)
        {
            purchasedAt.text = DateTimeUtil.DateTimeStringForPurchaseHistoryItem(info.purchasedAt);
        }

        private void SetPurchaseItemList(PurchaseHistoryReceiptData info)
        {
            var display = info.receiptInfo.items.Take(info.maxDisplayAmount)
                .Select(item => $"{info.SelectedBooth.GetPurchasedItem(item.hash).itemInfo.name} {item.amount}개")
                .ToList();

            int remain = info.receiptInfo.items.Count - info.maxDisplayAmount;
            string result = string.Join(", ", display);
            if (remain > 0)
                result += $" 외 {remain}개 항목";
            purchasedItemList.text = result;
        }
        
        private void SetPurchaseCost(PurchaseReceiptInfo info)
        {
            int result = info.items
                .Select(item => item.amount * item.pricePerItem)
                .Sum();
            purchaseCost.text = result.ToString();
        }
    }
}