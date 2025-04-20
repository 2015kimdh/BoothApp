using System;
using System.Collections.Generic;
using System.Linq;
using BoothApp.Presentation.BoothDetail.PurchaseHistory;
using BoothApp.Presentation.Info;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseReceiptDetail
{
    public class ReceiptDetailItemGroup : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private PurchaseHistoryViewModel viewModel;
        [SerializeField] private PurchaseReceiptDetailView view;
        [SerializeField] private ReceiptDetailItemMaker maker;
        [SerializeField] private LayoutGroupForcedRebuild reBuilder;
        
        #endregion
        
        #region Public Fields

        public List<ReceiptDetailItem> items = new();

        #endregion

        #region Property

        private BoothInfo SelectedBooth => viewModel.SelectedBooth;

        #endregion
        
        #region Method

        private void Awake()
        {
            view.onViewShow.AddListener(Refresh);
        }

        private void Refresh()
        {
            RefreshItemList();
            SetItemList();
            reBuilder.ForceRebuildLayout();
        }

        private void RefreshItemList()
        {
            var selectedItem = viewModel.SelectedItem;
            var gap = items.Count() - selectedItem.receiptInfo.items.Count;
            if (gap > 0)
            {
                for (int i = 0; i < gap; i++)
                {
                    maker.pool.Release(items[0]);
                    items.Remove(items[0]);
                }
            }
            else
                for (int i = gap; i < 0; i++)
                    items.Add(maker.pool.Get());
        }

        private void SetItemList()
        {
            for (int i = 0; i < items.Count; i++)
            {
                var receipt = viewModel.SelectedItem.receiptInfo;
                var purchasedItem = SelectedBooth.GetPurchasedItem(receipt.items[i].hash);
                items[i].SetUI(purchasedItem.itemInfo, receipt.items[i]);
            }
        }

        #endregion
    }
}