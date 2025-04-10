using BoothApp.Data;
using BoothApp.Presentation.Info;

namespace BoothApp.Mapper
{
    public static class BoothItemDataMapper
    {
        public static BoothItemInfo ToInfo(this BoothItemData data)
        {
            return new BoothItemInfo
            {
                name = data.name,
                owner = data.owner,
                price = data.price,
                imageName = data.imageName,
                hash = data.hash,
                itemTag = data.itemTag
            };
        }

        public static BoothItemData ToData(this BoothItemInfo info)
        {
            return new BoothItemData()
            {
                name = info.name,
                owner = info.owner,
                price = info.price,
                imageName = info.imageName,
                hash = info.hash,
                itemTag = info.itemTag
            };
        }

        public static BoothItemWithAmountInfo ToInfo(this BoothItemWithAmountData data)
        {
            return new BoothItemWithAmountInfo()
            {
                itemInfo = data.itemData.ToInfo(),
                amount = data.amount
            };
        }

        public static BoothItemWithAmountData ToData(this BoothItemWithAmountInfo info)
        {
            return new BoothItemWithAmountData()
            {
                itemData = info.itemInfo.ToData(),
                amount = info.amount
            };
        }

        public static PurchaseItemInfo ToInfo(this PurchaseItemData data)
        {
            return new PurchaseItemInfo()
            {
                hash = data.hash,
                amount = data.amount,
                pricePerItem = data.pricePerItem
            };
        }

        public static PurchaseItemData ToData(this PurchaseItemInfo info)
        {
            return new PurchaseItemData()
            {
                hash = info.hash,
                amount = info.amount,
                pricePerItem = info.pricePerItem
            };
        }

        public static BoothInfo ToInfo(this BoothData data)
        {
            BoothInfo info = new();
            BoothInformationInfo informationInfo = new()
            {
                boothName = data.boothInformationData.boothName,
                imageName = data.boothInformationData.imageName,
                createdAt = data.boothInformationData.createdAt,
                modifyAt = data.boothInformationData.modifyAt
            };
            info.boothInformationInfo = informationInfo;
            info.savedAt = data.savedAt;

            // 판매기록 매핑
            foreach (var purchaseData in data.boothInformationData.purchasedHistory)
            {
                var purchaseInfo = new PurchaseReceiptInfo
                {
                    purchasedAt = purchaseData.purchasedAt
                };
                foreach (var item in purchaseData.items)
                    purchaseInfo.items.Add(item.ToInfo());
                info.boothInformationInfo.purchasedHistory.Add(purchaseInfo);
            }

            // 원본 판매 목록 매핑
            foreach (var originalItemStatus in data.boothInformationData.originalItemStatus)
                info.boothInformationInfo.originalItemStatus.Add(originalItemStatus.ToInfo());
            
            // 판매된 목록 매핑
            foreach (var purchasedItemStatus in data.boothInformationData.purchasedItemStatus)
                info.boothInformationInfo.purchasedItemStatus.Add(purchasedItemStatus.ToInfo());

            info.boothInformationInfo.owners = data.boothInformationData.owners;
            info.boothInformationInfo.itemTags = data.boothInformationData.itemTags;
            info.boothInformationInfo.selectedOwners = data.boothInformationData.owners;
            info.boothInformationInfo.selectedTags = data.boothInformationData.selectedTags;
            
            return info;
        }
        
        public static BoothData ToData(this BoothInfo info)
        {
            BoothData data = new();
            BoothInformationData informationData = new()
            {
                boothName = info.boothInformationInfo.boothName,
                imageName = info.boothInformationInfo.imageName,
                createdAt = info.boothInformationInfo.createdAt,
                modifyAt = info.boothInformationInfo.modifyAt,
            };
            data.boothInformationData = informationData;
            data.savedAt = info.savedAt;

            // 판매기록 매핑
            foreach (var purchaseInfo in info.boothInformationInfo.purchasedHistory)
            {
                var purchaseData = new PurchaseReceiptData
                {
                    purchasedAt = purchaseInfo.purchasedAt
                };
                foreach (var item in purchaseInfo.items)
                    purchaseData.items.Add(item.ToData());
                data.boothInformationData.purchasedHistory.Add(purchaseData);
            }

            // 원본 판매 목록 매핑
            foreach (var originalItemStatus in info.boothInformationInfo.originalItemStatus)
                data.boothInformationData.originalItemStatus.Add(originalItemStatus.ToData());
            
            // 판매된 목록 매핑
            foreach (var purchasedItemStatus in info.boothInformationInfo.purchasedItemStatus)
                data.boothInformationData.purchasedItemStatus.Add(purchasedItemStatus.ToData());

            data.boothInformationData.owners = info.boothInformationInfo.owners;
            data.boothInformationData.itemTags = info.boothInformationInfo.itemTags;
            data.boothInformationData.selectedOwners = info.boothInformationInfo.owners;
            data.boothInformationData.selectedTags = info.boothInformationInfo.selectedTags;
            
            return data;
        }
    }
}