using System.Collections.Generic;
using System.Linq;
using BoothApp.Presentation.Info;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseItem
{
    public static class FilteringItem
    {
        public static List<BoothItemWithAmountInfo> FilteringWithTag(List<BoothItemWithAmountInfo> original, List<string> tag)
        {
            List<BoothItemWithAmountInfo> result = new();
            foreach (var item in original)
            {
                if(item.itemInfo.itemTag.Intersect(tag).ToList().Count != 0)
                    result.Add(item);
            }
            return result;
        }
        
        public static List<BoothItemWithAmountInfo> FilteringWithOwner(List<BoothItemWithAmountInfo> original, List<string> owner)
        {
            List<BoothItemWithAmountInfo> result = new();
            foreach (var item in original)
            {
                if(owner.Find(x=>x == item.itemInfo.owner) != null)
                    result.Add(item);
            }
            return result;
        }
    }
}