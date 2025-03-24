using System;
using System.Collections.Generic;

namespace BoothApp.Data
{
    [Serializable]
    public class PurchaseReceiptData
    {
        public List<PurchaseItemData> items = new();
        public DateTime purchasedAt;
    }
}