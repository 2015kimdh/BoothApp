using System;

namespace BoothApp.Presentation.Info
{
    [Serializable]
    public class BoothInfo
    {
        public BoothInformationInfo boothInformationInfo;
        public string savedAt;

        public int RemainItemAmount(string itemName)
        {
            var original = boothInformationInfo.originalItemStatus
                .Find(x => x.itemInfo.name == itemName);
            var purchased = boothInformationInfo.purchasedItemStatus
                .Find(x => x.itemInfo.name == itemName);
            return original.amount - purchased.amount;
        }

        public int GetOriginalItemAmount(string itemName) => boothInformationInfo.originalItemStatus
            .Find(x => x.itemInfo.name == itemName).amount;
        public int GetPurchasedItemAmount(string itemName) => boothInformationInfo.purchasedItemStatus
            .Find(x => x.itemInfo.name == itemName).amount;
    }
}