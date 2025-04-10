using System.Collections.Generic;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.BoothItemView
{
    public class ItemFilterer : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private PurchasableItemGroup itemGroup;
        [SerializeField] private SelectedBoothViewModel selectedBoothViewModel;
        
        #endregion

        #region Method

        public void SetItemTagFilter(List<string> tag, List<string> owner)
        {
            if (tag.Count == 0 && owner.Count == 0)
            {
                foreach (var target in itemGroup.purchasableItems)
                    target.gameObject.SetActive(true);
                return;
            }

            var result = FilteringItem.FilteringWithTag(selectedBoothViewModel.OriginalItemStatus, tag);
            result = FilteringItem.FilteringWithOwner(result, owner);
            foreach (var target in itemGroup.purchasableItems)
            {
                if (result.Find(x => x.itemInfo.hash == target.hash) == null)
                    target.gameObject.SetActive(true);
                else
                    target.gameObject.SetActive(false);
            }
        }

        #endregion
    }
}