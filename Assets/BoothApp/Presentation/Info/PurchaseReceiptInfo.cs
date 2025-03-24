using System;
using System.Collections.Generic;

namespace BoothApp.Presentation.Info
{
    public class PurchaseReceiptInfo
    {
        public List<PurchaseItemInfo> items = new();
        public DateTime purchasedAt;
    }
}