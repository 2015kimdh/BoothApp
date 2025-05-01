using System.Collections.Generic;
using System.Linq;
using BoothApp.Presentation.BoothDetail.BoothItemView;
using BoothApp.Presentation.Info;
using BoothApp.Utility;
using Doozy.Runtime.Common.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory.Filter
{
    public class ItemFiltererForPurchaseHistoryByItem
    {
        #region Serialize Field

        [SerializeField] private SelectedBoothViewModel selectedBoothViewModel;
        [SerializeField] private ItemTagDropdownSetter ownerDropdown;
        [SerializeField] private ItemTagDropdownSetter tagDropdown;
        [SerializeField] private BoothDataPresenter presenter;

        #endregion

        #region UnityEvent

        public UnityEvent<int> onFilterSetWithTagAmount;
        public UnityEvent onFilterSet;

        #endregion

        #region Private Fields

        private List<string> _ownersInPurchase = new();
        private List<string> _itemTagsInPurchase = new();

        #endregion

        #region Property

        private BoothInfo SelectedBooth => selectedBoothViewModel.selectedBooth;
        private PurchaseHistoryFilterAttributeInfo FilterAttribute => SelectedBooth.boothInformationInfo.purchaseHistoryFilterAttribute;

        #endregion

        #region Method

        public void OnRefresh()
        {
            GatheringTags();

            ownerDropdown.Refresh(_ownersInPurchase, FilterAttribute.selectedOwner);
            tagDropdown.Refresh(_itemTagsInPurchase, FilterAttribute.selectedItemTags);
            SelectedBooth.UpdateModifyTime();
            presenter.SaveDataAtDisk();
            onFilterSetWithTagAmount.Invoke(
                FilterAttribute.selectedOwner.Count + FilterAttribute.selectedItemTags.Count);
            onFilterSet.Invoke();
        }

        public void FilterSet()
        {
            FilterAttribute.selectedOwner = ClassCopy.CopyClass(ownerDropdown.SelectedTag);
            FilterAttribute.selectedItemTags = ClassCopy.CopyClass(tagDropdown.SelectedTag);
            SelectedBooth.UpdateModifyTime();
            presenter.SaveDataAtDisk();
            onFilterSetWithTagAmount.Invoke(
                FilterAttribute.selectedOwner.Count + FilterAttribute.selectedItemTags.Count);
            onFilterSet.Invoke();
        }

        public void InitFilter()
        {
            ownerDropdown.SetAllToggleFalse();
            tagDropdown.SetAllToggleFalse();
        }

        private void GatheringTags()
        {
            foreach (var item in SelectedBooth.boothInformationInfo.purchasedItemStatus)
            {
                if (!item.itemInfo.owner.IsNullOrEmpty())
                    _ownersInPurchase.Add(item.itemInfo.owner);
                if (item.itemInfo.itemTag.Count() != 0)
                    _itemTagsInPurchase = _itemTagsInPurchase.Union(item.itemInfo.itemTag).ToList();
            }

            _ownersInPurchase = _ownersInPurchase.Distinct().ToList();
            _itemTagsInPurchase = _itemTagsInPurchase.Distinct().ToList();

            FilterAttribute.selectedOwner = FilterAttribute.selectedOwner.Intersect(_ownersInPurchase).ToList();
            FilterAttribute.selectedItemTags = FilterAttribute.selectedItemTags.Intersect(_itemTagsInPurchase).ToList();
            SelectedBooth.UpdateModifyTime();
            presenter.SaveDataAtDisk();
        }

        #endregion
    }
}