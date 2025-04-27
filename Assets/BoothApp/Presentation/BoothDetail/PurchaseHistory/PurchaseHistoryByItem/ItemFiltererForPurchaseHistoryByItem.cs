using System.Collections.Generic;
using System.Linq;
using BoothApp.Presentation.BoothDetail.BoothItemView;
using BoothApp.Presentation.Info;
using Doozy.Runtime.Common.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory.PurchaseHistoryByItem
{
    public class ItemFiltererForPurchaseHistoryByItem
    {
        #region Serialize Field

        [SerializeField] private TotalInvoiceItemGroup itemGroup;
        [SerializeField] private SelectedBoothViewModel selectedBoothViewModel;
        [SerializeField] private ItemTagDropdownSetter ownerDropdown;
        [SerializeField] private ItemTagDropdownSetter tagDropdown;
        [SerializeField] private BoothDataPresenter presenter;

        #endregion

        #region UnityEvent

        public UnityEvent<int, bool> onFilterSetWithTagAmount;

        #endregion

        #region Private Fields

        private List<string> _ownersInPurchase = new();
        private List<string> _itemTagsInPurchase = new();

        #endregion

        #region Property

        private BoothInformationInfo SelectedBooth => selectedBoothViewModel.selectedBooth.boothInformationInfo;

        #endregion

        #region Method

        public void OnRefresh()
        {
            GatheringTags();
            
            ownerDropdown.Refresh(_ownersInPurchase, SelectedBooth.selectedOwners);
            tagDropdown.Refresh(_itemTagsInPurchase, SelectedBooth.selectedTags);
            SetItemTagFilter(SelectedBooth.selectedTags, SelectedBooth.selectedOwners);
        }
        
        public void InitFilter()
        {
            ownerDropdown.SetAllToggleFalse();
            tagDropdown.SetAllToggleFalse();
        }

        private void GatheringTags()
        {
            foreach (var item in SelectedBooth.purchasedItemStatus)
            {
                if (!item.itemInfo.owner.IsNullOrEmpty())
                    _ownersInPurchase.Add(item.itemInfo.owner);
                if (item.itemInfo.itemTag.Count() != 0)
                    _itemTagsInPurchase = _itemTagsInPurchase.Union(item.itemInfo.itemTag).ToList();
            }

            _ownersInPurchase = _ownersInPurchase.Distinct().ToList();
            _itemTagsInPurchase = _itemTagsInPurchase.Distinct().ToList();
        }

        private void SetItemTagFilter(List<string> itemTag, List<string> owner)
        {
            // if (itemTag.Count == 0 && owner.Count == 0 && soldOutFilterToggle.isOn == false)
            // {
            //     foreach (var target in itemGroup.purchasableItems)
            //     {
            //         target.gameObject.SetActive(true);
            //     }
            //
            //     onFilterSetWithTagAmount.Invoke(itemTag.Count + owner.Count, soldOutFilterToggle.isOn);
            //     return;
            // }
            //
            // var result = FilteringItem.FilteringWithTag(selectedBoothViewModel.OriginalItemStatus, itemTag);
            // result = FilteringItem.FilteringWithOwner(result, owner);
            // foreach (var target in itemGroup.purchasableItems)
            // {
            //     if (result.Find(x => x.itemInfo.hash == target.hash) == null)
            //         target.gameObject.SetActive(false);
            //     else
            //     {
            //         // 품절 상품 체크
            //         if (soldOutFilterToggle.isOn &&
            //             selectedBoothViewModel.selectedBooth.GetOriginalItem(target.hash).amount == 0)
            //             target.gameObject.SetActive(false);
            //         else
            //             target.gameObject.SetActive(true);
            //     }
            // }
            //
            // onFilterSetWithTagAmount.Invoke(itemTag.Count + owner.Count, soldOutFilterToggle.isOn);
        }

        #endregion
    }
}