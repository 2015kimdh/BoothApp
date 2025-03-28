using System.Collections.Generic;
using System.Linq;
using BoothApp.Presentation.Info;
using BoothApp.Utility;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail
{
    public class PurchasableItemGroup : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private PurchasableItemMaker maker;
        [SerializeField] private SelectedBooth selectedBooth;
        [SerializeField] private SelectedBoothView view;
        [SerializeField] private GameObject addItemButton;

        #endregion

        #region Property

        private BoothInformationInfo boothInfo => selectedBooth.selectedBooth.boothInformationInfo;
        private string _selectedBoothName = "";

        #endregion

        #region Public Fields

        /// <summary>
        /// 판매 품목들
        /// </summary>
        public List<PurchasableItem> purchasableItems = new();

        #endregion

        #region MonoBehaviour Events

        private void Start()
        {
            selectedBooth.onSelected.AddListener(RefreshIfSelectedBoothChanged);
        }

        #endregion

        #region Public Method

        public void RefreshIfSelectedBoothChanged(string selectedBoothName)
        {
            if (selectedBoothName != _selectedBoothName)
            {
                RefreshPurchasableItems();
                _selectedBoothName = selectedBoothName;
            }
        }

        public void RefreshPurchasableItems()
        {
            RemoveUnTrackedItem();
            AddTrackedItem();
        }

        #endregion

        #region Private Method

        private List<string> GetPurchasableItemHash()
        {
            List<string> purchasableItemHash = new();
            foreach (var item in purchasableItems)
                purchasableItemHash.Add(item.hash);
            return purchasableItemHash;
        }

        private List<string> GetOriginalItemHash()
        {
            List<string> originalItemHash = new();
            foreach (var item in boothInfo.originalItemStatus)
                originalItemHash.Add(item.itemInfo.hash);
            return originalItemHash;
        }

        private void RemoveUnTrackedItem()
        {
            var purchasableHash = GetPurchasableItemHash();
            var originalHash = GetOriginalItemHash();
            var except = purchasableHash.Except(originalHash).ToList();
            foreach (var exceptedItem in except)
            {
                var target = purchasableItems.Find(x => x.hash == exceptedItem);
                purchasableItems.Remove(target);
                Destroy(target);
            }
        }

        private void AddTrackedItem()
        {
            var purchasableHash = GetPurchasableItemHash();
            var originalHash = GetOriginalItemHash();
            var except = originalHash.Except(purchasableHash).ToList();
            foreach (var exceptedItem in except)
            {
                var target = boothInfo.originalItemStatus
                    .Find(x => x.itemInfo.hash == exceptedItem);
                var newItem = maker.MakeNewPurchasableItem();

                newItem.hash = exceptedItem;
                newItem.itemImage.sprite = ImageLoader.LoadImageWithName(target.itemInfo.imageName);
                newItem.itemName.text = target.itemInfo.name;
                newItem.owner = target.itemInfo.owner;
                newItem.remainAmount.text = selectedBooth.selectedBooth.RemainItemAmount(exceptedItem).ToString();
                newItem.itemTag = target.itemInfo.itemTag;
                purchasableItems.Add(newItem);
            }

            addItemButton.transform.SetAsLastSibling();
        }

        #endregion
    }
}