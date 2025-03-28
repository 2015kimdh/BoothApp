using System;

namespace BoothApp.Presentation.Info
{
    [Serializable]
    public class BoothInfo
    {
        public BoothInformationInfo boothInformationInfo;
        public string savedAt;

        public int RemainItemAmount(string hash)
        {
            var original = boothInformationInfo.originalItemStatus
                .Find(x => x.itemInfo.hash == hash);
            var purchased = boothInformationInfo.purchasedItemStatus
                .Find(x => x.itemInfo.hash == hash);
            return original.amount - purchased.amount;
        }

        public int GetOriginalItemAmount(string hash) => boothInformationInfo.originalItemStatus
            .Find(x => x.itemInfo.hash == hash).amount;
        public int GetPurchasedItemAmount(string hash) => boothInformationInfo.purchasedItemStatus
            .Find(x => x.itemInfo.hash == hash).amount;
    }
}