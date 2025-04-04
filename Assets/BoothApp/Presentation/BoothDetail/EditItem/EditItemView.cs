using System;
using BoothApp.Presentation.Info;
using BoothApp.Utility;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.EditItem
{
    public class EditItemView : ItemViewBase
    {
        #region Serialize Field

        /// <summary>
        /// 선택된 아이템에 대한 정보
        /// </summary>
        [SerializeField] private SelectedItem selectedItem;

        #endregion

        #region Property

        private BoothItemWithAmountInfo SelectedItemInfo => selectedItem.selectedItem;

        #endregion
        
        #region Private Fields

        private string _hash;

        #endregion

        #region Method

        protected override void Awake()
        {
            base.Awake();
            selectedItem.onItemSet.AddListener(SetSelectedInfo);
        }

        private void SetSelectedInfo()
        {
            InitVariable();
            SetVariable();
        }
        
        /// <summary>
        /// 뷰가 열렸을 때 클릭한 아이템 (수정하고자 하는 항목)
        /// 데이터를 설정
        /// </summary>
        private void SetVariable()
        {
            itemName = SelectedItemInfo.itemInfo.name;
            itemPrice = SelectedItemInfo.itemInfo.price;
            itemAmount = SelectedItemInfo.amount;
            itemImage.sprite = ImageHub.GetImageWithName(SelectedItemInfo.itemInfo.imageName);
            selectedTags = ClassCopy.CopyClass(SelectedItemInfo.itemInfo.itemTag);
            owner = SelectedItemInfo.itemInfo.owner;
            _hash = SelectedItemInfo.itemInfo.hash;
            
            namePriceAmountAbility = CheckViability();
        }

        public void ApplyEditItem()
        {
            if (!namePriceAmountAbility)
            {
                onFail.Invoke();
                return;
            }

            SetItemImage(SelectedItemInfo.itemInfo);
            SetDetailInfo(SelectedItemInfo.itemInfo);
            SelectedItemInfo.amount = itemAmount;

            if (selectedItem.selectedItemPurchased != null)
            {
                SetItemImage(selectedItem.selectedItemPurchased.itemInfo);
                SetDetailInfo(selectedItem.selectedItemPurchased.itemInfo);
            }

            selectedBooth.selectedBooth.boothInformationInfo.modifyAt = DateTimeUtil.DateTimeNowToString();
            onSuccess.Invoke();
        }
        
        private void SetItemImage(BoothItemInfo info)
        {
            if (itemImage.sprite == null)
            {
                info.imageName = "";
                info.image = null;
            }
            else
            {
                info.imageName = itemImage.sprite.name;
                info.image = itemImage.sprite;
            }
        }
        
        private void SetDetailInfo(BoothItemInfo info)
        {
            info.owner = owner;
            info.name = itemName;
            info.price = itemPrice;
            info.itemTag = ClassCopy.CopyClass(selectedTags);
            info.hash = _hash;
        }
        
        private bool CheckViability()
        {
            if (itemName == "")
                return false;
            if (itemPrice < 0)
                return false;
            if (itemAmount < 1)
                return false;
            return true;
        }
        
        #endregion
    }
}