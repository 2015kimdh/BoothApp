using System;
using System.Collections.Generic;
using System.Linq;
using BoothApp.Presentation.Info;
using UnityEngine;
using UnityEngine.Pool;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory
{
    public class PurchaseHistoryItemGroup : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private PurchaseHistoryViewModel viewModel;
        [SerializeField] private PurchaseHistoryView view;
        [SerializeField] private PurchaseReceiptItemMaker maker;
        [SerializeField] private LayoutGroupForcedRebuild rebuilder;
        
        #endregion

        #region Public Fields

        public List<PurchaseHistoryReceiptItem> receipts = new();

        #endregion

        #region Private Fields

        private List<PurchaseReceiptInfo> _receiptInfos = new();

        #endregion

        #region Methods

        private void Awake()
        {
            view.onViewShow.AddListener(Refresh);
            viewModel.onDelete.AddListener(Refresh);
        }

        private void Refresh()
        {
            RefreshReceiptList();
            ReleaseUnTracked();
            MakeItems();
            SetSiblingOderByTime();
            rebuilder.ForceRebuildLayout();
        }
        
        private void RefreshReceiptList()
        {
            List<PurchaseReceiptInfo> my = receipts.Select(item => item.receiptInfo).ToList();
            // ViewModel과 자신의 교집합
            var intersect = my.Intersect(viewModel.PurchaseHistory);
            // ViewModel에만 있는 객체 추리기
            var insert = viewModel.PurchaseHistory.Except(my);
            _receiptInfos = intersect.Union(insert).ToList();
        }

        private void SetSiblingOderByTime()
        {
            var sorted = receipts.OrderByDescending(item => item.receiptInfo.purchasedAt).ToList();
            for (int i = 0; i < sorted.Count(); i++)
            {
                sorted[i].gameObject.transform.SetSiblingIndex(i);
            }
        }
        
        private void MakeItems()
        {
            var needToMake = _receiptInfos.Except(receipts.Select(item => item.receiptInfo).ToList());
            foreach (var receiptInfo in needToMake)
            {
                var newOne = maker.pool.Get();
                newOne.SetDataAndUI(viewModel.SelectedBoothViewModel, receiptInfo);
                receipts.Add(newOne);
            }
        }

        private void ReleaseUnTracked()
        {
            var remove = receipts.Select(item => item.receiptInfo).Except(_receiptInfos).ToList();
            var removeObject = receipts.Where(item => remove.Contains(item.receiptInfo)).ToList();
            foreach (var target in removeObject)
            {
                maker.pool.Release(target);
                receipts.Remove(target);
            }
        }
        
        #endregion
    }
}