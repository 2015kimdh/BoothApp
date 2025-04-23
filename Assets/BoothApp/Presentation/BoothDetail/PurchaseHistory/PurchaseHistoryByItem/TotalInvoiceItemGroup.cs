using System.Collections.Generic;
using BoothApp.Presentation.Info;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory.PurchaseHistoryByItem
{
    public class TotalInvoiceItemGroup : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private PurchaseHistoryByItemViewModel viewModel;
        [SerializeField] private TotalInvoiceItemMaker maker;
        [SerializeField] private LayoutGroupForcedRebuild reBuilder;

        #endregion

        #region Public Fields

        public List<TotalInvoiceItem> items = new();
        
        #endregion

        #region Property

        private BoothInfo SelectedBooth => viewModel.SelectedBooth;

        #endregion
        
        #region Method

        private void Refresh()
        {
            RefreshItemList();
            SetItemList();
            reBuilder.ForceRebuildLayout();
        }

        private void RefreshItemList()
        {
            var purchasedItems = SelectedBooth.boothInformationInfo.purchasedItemStatus;
            var gap = items.Count - purchasedItems.Count;
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
            var purchasedItems = SelectedBooth.boothInformationInfo.purchasedItemStatus;
            for (int i = 0; i < items.Count; i++)
            {
                int totalInvoice = viewModel.GetTotalInvoice(purchasedItems[i].itemInfo);
                int totalAmount = viewModel.GetTotalInvoice(purchasedItems[i].itemInfo);
                items[i].SetUI(purchasedItems[i].itemInfo, totalInvoice, totalAmount);
            }
        }

        #endregion
    }
}