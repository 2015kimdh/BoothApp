using BoothApp.Presentation.Info;
using BoothApp.Utility;

namespace BoothApp.Presentation.BoothDetail
{
    public class AddNewItemView : ItemViewBase
    {
        #region Method

        protected override void Awake()
        {
            base.Awake();
            onViewShow.AddListener(InitVariable);
        }
        
        public void AddNewItem()
        {
            if (!namePriceAmountAbility)
            {
                onFail.Invoke();
                return;
            }

            BoothItemWithAmountInfo newItem = new();
            BoothItemInfo detailInfo = new();
            SetItemImage(detailInfo);
            SetDetailInfo(detailInfo);

            newItem.amount = itemAmount;
            newItem.itemInfo = detailInfo;
            
            SelectedBooth
                .boothInformationInfo.originalItemStatus.Add(newItem);
            selectedBooth.selectedBooth.boothInformationInfo.modifyAt = DateTimeUtil.DateTimeNowToString();
            InitVariable();
            onSuccess.Invoke();
        }

        private void SetDetailInfo(BoothItemInfo info)
        {
            info.owner = owner;
            info.name = itemName;
            info.price = itemPrice;
            info.itemTag = ClassCopy.CopyClass(selectedTags);
            info.hash = GetUniqueHash();
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

        private string GetUniqueHash()
        {
            while (true)
            {
                var hash = RandomString.RandomStringGenerate(12);
                var result = SelectedBooth
                    .boothInformationInfo.originalItemStatus
                    .Find(x => x.itemInfo.hash == hash);
                if (result == null)
                    return hash;
            }
        }

        #endregion
    }
}