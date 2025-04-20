using System;
using System.Collections.Generic;
using UnityEngine.TestTools;

namespace BoothApp.Presentation.Info
{
    [Serializable]
    public class PurchaseReceiptInfo
    {
        public List<PurchaseItemInfo> items = new();
        public DateTime purchasedAt;

        public override bool Equals(object obj)
        {
            if (obj is not PurchaseReceiptInfo other) return false;
            return this.purchasedAt == other.purchasedAt;
        }
        
        public override int GetHashCode() => purchasedAt.GetHashCode();
    }
}