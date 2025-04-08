using System.Collections.Generic;
using System.Linq;
using BoothApp.Presentation.Info;
using BoothApp.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace BoothApp.Presentation.BoothDetail
{
    public class PurchasableItemGroup : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private PurchasableItemMaker maker;
        [FormerlySerializedAs("selectedBooth")] [SerializeField] private SelectedBoothViewModel selectedBoothViewModel;
        [SerializeField] private SelectedBoothView view;
        [SerializeField] private GameObject addItemButton;

        #endregion

        #region Property

        private BoothInformationInfo boothInfo => selectedBoothViewModel.selectedBooth.boothInformationInfo;
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
            selectedBoothViewModel.onSelected.AddListener(RefreshIfSelectedBoothChanged);
            view.onViewShow.AddListener(RefreshPurchasableItems);
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
            RefreshTrackedItem();
            RemoveUnTrackedItem();
            AddNewTrackedItem();
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
                Destroy(target.gameObject);
            }
        }

        private void AddNewTrackedItem()
        {
            var purchasableHash = GetPurchasableItemHash();
            var originalHash = GetOriginalItemHash();
            var except = originalHash.Except(purchasableHash).ToList();
            foreach (var exceptedItem in except)
            {
                var target = boothInfo.originalItemStatus
                    .Find(x => x.itemInfo.hash == exceptedItem);
                var newItem = maker.MakeNewPurchasableItem();

                SetPurchaseAbleItemData(newItem, target);
                purchasableItems.Add(newItem);
            }

            addItemButton.transform.SetAsLastSibling();
        }

        private void RefreshTrackedItem()
        {
            var purchasableHash = GetPurchasableItemHash();
            var originalHash = GetOriginalItemHash();
            var intersect = purchasableHash.Intersect(originalHash).ToList();
            foreach (var item in intersect)
            {
                var target = purchasableItems.Find(x => x.hash == item);
                var original =
                    boothInfo.originalItemStatus.Find(x => x.itemInfo.hash == item);
                SetPurchaseAbleItemData(target, original);
            }
        }

        private void SetPurchaseAbleItemData(PurchasableItem purchasableItem, BoothItemWithAmountInfo boothItem)
        {
            purchasableItem.hash = boothItem.itemInfo.hash;
            purchasableItem.SetPurchasableItem(
                ImageHub.GetImageWithName(boothItem.itemInfo.imageName),
                boothItem.itemInfo.name,
                boothItem.itemInfo.itemTag,
                boothItem.itemInfo.owner);
            
            purchasableItem.originalAmount.text =
                selectedBoothViewModel.selectedBooth.GetOriginalItemAmount(purchasableItem.hash).ToString();
        }

        #endregion
    }
}