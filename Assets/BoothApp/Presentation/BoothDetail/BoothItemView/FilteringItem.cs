using System.Collections.Generic;
using System.Linq;
using BoothApp.Presentation.Info;
using BoothApp.Utility;

namespace BoothApp.Presentation.BoothDetail.BoothItemView
{
    public static class FilteringItem
    {
        public static List<BoothItemWithAmountInfo> FilteringWithTag(List<BoothItemWithAmountInfo> original,
            List<string> tag)
        {
            if (tag.Count == 0)
                return original;
            List<BoothItemWithAmountInfo> result = new();
            foreach (var item in original)
            {
                if (item.itemInfo.itemTag.Intersect(tag).ToList().Count != 0)
                    result.Add(item);
            }

            return result;
        }

        public static List<BoothItemWithAmountInfo> FilteringWithOwner(List<BoothItemWithAmountInfo> original,
            List<string> owner)
        {
            if (owner.Count == 0)
                return original;
            List<BoothItemWithAmountInfo> result = new();
            foreach (var item in original)
            {
                if (owner.Find(x => x == item.itemInfo.owner) != null)
                    result.Add(item);
            }

            return result;
        }

        public static List<PurchaseReceiptInfo> FilteringReceiptByDate(List<PurchaseReceiptInfo> original,
            string fromDate, string toDate)
        {
            var list = original.AsEnumerable();

            if (fromDate != "")
            {
                var from = DateTimeUtil.DateTimeStringToDateTime(fromDate);
                list = list.Where(x => x.purchasedAt >= from);
            }

            if (toDate != "")
            {
                var to = DateTimeUtil.DateTimeStringToDateTime(toDate);
                list = list.Where(x => x.purchasedAt <= to);
            }

            // 날짜 필터 완료

            return list.ToList();
        }

        public static List<PurchaseReceiptInfo> FilteringReceiptByItemTags(List<BoothItemWithAmountInfo> purchased,
            List<PurchaseReceiptInfo> original,
            List<string> itemTags)
        {
            if (itemTags.Count == 0)
                return original;

            var filteredWithTagItem = FilteringWithTag(purchased, itemTags);
            List<PurchaseReceiptInfo> result = new();
            result = original.Where(x =>        // 영수증의 판매된 내역 중
                    x.items.Count(t => filteredWithTagItem  // 필터에서 걸러지지 않은 물건이
                        .Count(filtered => filtered.itemInfo.hash == t.hash) != 0) // 1개라도 있는 영수증에 대해 가져옴
                    != 0).ToList();
            // 아래 있는 줄과 같은 기능
            
            // foreach (var item in original)
            //     foreach (var target in item.items)
            //         if (filteredWithTagItem.Count(x => x.itemInfo.hash == target.hash) != 0)
            //             result.Add(item);

            return result;
        }
        
        public static List<PurchaseReceiptInfo> FilteringReceiptByOwners(List<BoothItemWithAmountInfo> purchased,
            List<PurchaseReceiptInfo> original,
            List<string> owners)
        {
            if (owners.Count == 0)
                return original;

            var filteredWithTagItem = FilteringWithOwner(purchased, owners);
            List<PurchaseReceiptInfo> result = new();
            result = original.Where(x =>        // 영수증의 판매된 내역 중
                x.items.Count(t => filteredWithTagItem  // 필터에서 걸러지지 않은 물건이
                    .Count(filtered => filtered.itemInfo.hash == t.hash) != 0) // 1개라도 있는 영수증에 대해 가져옴
                != 0).ToList();
            
            return result;
        }
    }
}