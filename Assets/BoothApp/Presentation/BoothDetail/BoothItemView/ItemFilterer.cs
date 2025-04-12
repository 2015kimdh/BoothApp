using System.Collections.Generic;
using BoothApp.Presentation.Info;
using BoothApp.Utility;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail.BoothItemView
{
    public class ItemFilterer : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private PurchasableItemGroup itemGroup;
        [SerializeField] private SelectedBoothViewModel selectedBoothViewModel;
        [SerializeField] private ItemTagDropdownSetter ownerDropdown;
        [SerializeField] private ItemTagDropdownSetter tagDropdown;
        [SerializeField] private BoothDataPresenter presenter;
        [SerializeField] private UIToggle soldOutFilterToggle;

        #endregion

        #region UnityEvent

        public UnityEvent<int, bool> onFilterSetWithTagAmount;

        #endregion

        #region Property

        private BoothInformationInfo SelectedBooth => selectedBoothViewModel.selectedBooth.boothInformationInfo;

        #endregion

        #region Method

        private void Start()
        {
            itemGroup.onRefresh.AddListener(OnRefresh);
        }

        public void OnRefresh()
        {
            ownerDropdown.Refresh(SelectedBooth.owners, SelectedBooth.selectedOwners);
            tagDropdown.Refresh(SelectedBooth.itemTags, SelectedBooth.selectedTags);
            SetItemTagFilter(SelectedBooth.selectedTags, SelectedBooth.selectedOwners);
        }

        public void FilterSet()
        {
            SetItemTagFilter(tagDropdown.SelectedTag, ownerDropdown.SelectedTag);
            SelectedBooth.selectedOwners = ClassCopy.CopyClass(ownerDropdown.SelectedTag);
            SelectedBooth.selectedTags = ClassCopy.CopyClass(tagDropdown.SelectedTag);
            SelectedBooth.modifyAt = DateTimeUtil.DateTimeNowToString();
            presenter.SaveDataAtDisk();
        }

        public void InitFilter()
        {
            ownerDropdown.SetAllToggleFalse();
            tagDropdown.SetAllToggleFalse();
            soldOutFilterToggle.isOn = false;
        }

        private void SetItemTagFilter(List<string> itemTag, List<string> owner)
        {
            if (itemTag.Count == 0 && owner.Count == 0 && soldOutFilterToggle.isOn == false)
            {
                foreach (var target in itemGroup.purchasableItems)
                {
                    target.gameObject.SetActive(true);
                }

                onFilterSetWithTagAmount.Invoke(itemTag.Count + owner.Count, soldOutFilterToggle.isOn);
                return;
            }

            var result = FilteringItem.FilteringWithTag(selectedBoothViewModel.OriginalItemStatus, itemTag);
            result = FilteringItem.FilteringWithOwner(result, owner);
            foreach (var target in itemGroup.purchasableItems)
            {
                if (result.Find(x => x.itemInfo.hash == target.hash) == null)
                    target.gameObject.SetActive(false);
                else
                {
                    // 품절 상품 체크
                    if (soldOutFilterToggle.isOn &&
                        selectedBoothViewModel.selectedBooth.GetOriginalItem(target.hash).amount == 0)
                        target.gameObject.SetActive(false);
                    else
                        target.gameObject.SetActive(true);
                }
            }

            onFilterSetWithTagAmount.Invoke(itemTag.Count + owner.Count, soldOutFilterToggle.isOn);
        }

        #endregion
    }
}