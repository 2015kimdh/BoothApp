using System.Collections.Generic;
using BoothApp.Presentation.Info;
using BoothApp.Utility;
using Doozy.Runtime.UIManager.Containers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BoothApp.Presentation.BoothDetail
{
    public class AddNewItemView : MonoBehaviour
    {
        #region Unity Event

        public UnityEvent onViewShow;
        public UnityEvent onRefresh;

        public UnityEvent onFailToAdd;
        public UnityEvent onSuccessToAdd;

        #endregion

        #region Property

        public BoothInfo SelectedBooth => selectedBooth.selectedBooth;
        public List<string> ItemTags => SelectedBooth.boothInformationInfo.itemTags;
        public List<string> Owners => SelectedBooth.boothInformationInfo.owners;

        #endregion

        #region Serialize Field

        [SerializeField] private SelectedBooth selectedBooth;

        #endregion

        #region Public Field

        public string itemName = "";
        public int itemPrice = 0;
        public int itemAmount = 0;
        public Image itemImage;
        public List<string> selectedTags;
        public string owner;
        public bool namePriceAmountAbility = false;

        #endregion


        #region Method

        private void Awake()
        {
            onViewShow.AddListener(InitVariable);
        }

        public void OnShowInvoke()
        {
            onViewShow.Invoke();
        }
        
        public void AddNewItem()
        {
            if (!namePriceAmountAbility)
            {
                onFailToAdd.Invoke();
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
            onSuccessToAdd.Invoke();
        }

        private void InitVariable()
        {
            itemAmount = 0;
            itemName = "";
            itemPrice = 0;
            selectedTags = new();
            owner = "";
            namePriceAmountAbility = false;
            itemImage.sprite = null;
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