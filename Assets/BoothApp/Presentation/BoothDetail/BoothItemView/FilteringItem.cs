using System.Collections.Generic;
using System.Linq;
using BoothApp.Presentation.Info;

namespace BoothApp.Presentation.BoothDetail.BoothItemView
{
    public static class FilteringItem
    {
        public static List<BoothItemWithAmountInfo> FilteringWithTag(List<BoothItemWithAmountInfo> original, List<string> tag)
        {
            if (tag.Count == 0)
                return original;
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
            if (owner.Count == 0)
                return original;
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