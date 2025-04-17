using System.Linq;
using BoothApp.Presentation.BoothDetail.BoothItemView;
using BoothApp.Presentation.Info;
using BoothApp.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail.DeleteTag
{
    /// <summary>
    /// 존재하는 태그를 삭제하는 View
    /// Original Status의 태그만을 건드리며
    /// Purchase Status의 태그는 Original이 존재하는 경우에만 수정함.
    /// Original이 없는 Purchase의 경우, 이미 더 이상 판매는 하지 않지만
    /// 판매되었던 이력이 있기 때문에 수정하면 안된다.
    /// </summary>
    public class DeleteTagView : MonoBehaviour
    {
        #region Unity Event

        public UnityEvent onDeleteTag; 

        #endregion
        
        [SerializeField] private SelectedBoothViewModel selectedBoothViewModel;
        [SerializeField] private ItemTagDropdownSetter ownerDropdown;
        [SerializeField] private ItemTagDropdownSetter tagDropdown;
        [SerializeField] private BoothDataPresenter presenter;

        #region Property

        private BoothInformationInfo SelectedBooth => selectedBoothViewModel.selectedBooth.boothInformationInfo;

        #endregion

        /// <summary>
        /// View가 열렸을 때 Dropdown 내용 갱신
        /// </summary>
        public void OnRefresh()
        {
            ownerDropdown.RefreshOnly(SelectedBooth.owners);
            tagDropdown.RefreshOnly(SelectedBooth.itemTags);
            InitDropDown();
        }

        public void DeleteTag()
        {
            DeleteOwnerTag();
            DeleteItemTag();
            SetModifyTime();
            OnRefresh();
            onDeleteTag.Invoke();
        }

        private void SetModifyTime()
        {
            SelectedBooth.modifyAt = DateTimeUtil.DateTimeNowToString();
            presenter.SaveDataAtDisk();
        }
        
        private void DeleteOwnerTag()
        {
            if(ownerDropdown.SelectedTag.Count == 0)
                return;
            // Owner를 필터로 선택된 내용을 Owner로 가지고 있는 객체 가져오기
            var result =
                FilteringItem.FilteringWithOwner(selectedBoothViewModel.OriginalItemStatus, ownerDropdown.SelectedTag);
            // 가져온 객체들의 Owner를 초기화
            foreach (var target in result)
            {
                target.itemInfo.owner = "";
                if (selectedBoothViewModel.selectedBooth.GetPurchasedItem(target.itemInfo.hash) != null)
                    selectedBoothViewModel.selectedBooth.GetPurchasedItem(target.itemInfo.hash).itemInfo.owner = "";
            }

            // Booth 정보의 Owner 제거
            SelectedBooth.selectedOwners = SelectedBooth.selectedOwners.Except(ownerDropdown.SelectedTag).ToList();
            SelectedBooth.owners = SelectedBooth.owners.Except(ownerDropdown.SelectedTag).ToList();
        }

        private void DeleteItemTag()
        {
            if(tagDropdown.SelectedTag.Count == 0)
                return;
            // ItemTag를 필터로 선택된 내용을 Tag로 가지고 있는 객체 가져오기
            var result =
                FilteringItem.FilteringWithTag(selectedBoothViewModel.OriginalItemStatus, tagDropdown.SelectedTag);
            foreach (var target in result)
            {
                target.itemInfo.itemTag = target.itemInfo.itemTag.Except(tagDropdown.SelectedTag).ToList();
                if (selectedBoothViewModel.selectedBooth.GetPurchasedItem(target.itemInfo.hash) != null)
                    selectedBoothViewModel.selectedBooth.GetPurchasedItem(target.itemInfo.hash).itemInfo.itemTag =
                        selectedBoothViewModel.selectedBooth.GetPurchasedItem(target.itemInfo.hash).itemInfo.itemTag
                            .Except(tagDropdown.SelectedTag).ToList();
            }
            
            // Booth 정보의 Tag 제거
            SelectedBooth.selectedTags = SelectedBooth.selectedTags.Except(tagDropdown.SelectedTag).ToList();
            SelectedBooth.itemTags = SelectedBooth.itemTags.Except(tagDropdown.SelectedTag).ToList();
        }

        private void InitDropDown()
        {
            ownerDropdown.SetAllToggleFalse();
            tagDropdown.SetAllToggleFalse();
        }
    }
}