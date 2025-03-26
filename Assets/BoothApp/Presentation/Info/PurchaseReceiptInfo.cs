using System;
using System.Collections.Generic;

namespace BoothApp.Presentation.Info
{
    [Serializable]
    public class PurchaseReceiptInfo
    {
        public List<PurchaseItemInfo> items = new();
        public DateTime purchasedAt;
    }
}