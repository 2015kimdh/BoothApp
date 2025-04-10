using System.Collections.Generic;
using BoothApp.Presentation.Info;

namespace BoothApp.Presentation.BoothDetail.PurchaseItem
{
    public static class CalculateTotalPurchaseExchange
    {
        public static float CalculateExchange(PurchaseReceiptInfo receiptInfo)
        {
            float totalExchange = 0;
            foreach (var item in receiptInfo.items)
            {
                totalExchange += item.pricePerItem * item.amount;
            }
            return totalExchange;
        }

        public static float CalculateExchange(List<PurchaseReceiptInfo> receiptInfos)
        {
            float totalExchange = 0;
            foreach (var item in receiptInfos)
            {
                totalExchange += CalculateExchange(item);
            }
            return totalExchange;
        }

        public static float CalculateExchange(List<PurchaseItemInfo> purchaseItemInfos)
        {
            float totalExchange = 0;
            foreach (var item in purchaseItemInfos)
            {
                totalExchange += item.pricePerItem * item.amount;
            }
            return totalExchange;
        }
    }
}