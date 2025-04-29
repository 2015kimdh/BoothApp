using System.Collections.Generic;
using System.Linq;
using BoothApp.Presentation.Info;
using BoothApp.Utility;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory
{
    public class PurchaseHistoryItemGroup : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private PurchaseHistoryViewModel viewModel;
        [SerializeField] private PurchaseHistoryView view;
        [SerializeField] private PurchaseReceiptItemMaker maker;

        [FormerlySerializedAs("rebuilder")] [SerializeField]
        private LayoutGroupForcedRebuild reBuilder;

        #endregion

        #region Public Fields

        public List<PurchaseHistoryReceiptItem> receipts = new();

        #endregion

        #region Property

        private BoothInfo SelectedBooth => viewModel.SelectedBoothViewModel.selectedBooth;

        private PurchaseHistoryFilterAttributeInfo FilterAttributeInfo =>
            SelectedBooth.boothInformationInfo.purchaseHistoryFilterAttribute;

        #endregion

        #region Unity Event

        public UnityEvent onRefresh;

        #endregion

        #region Private Fields

        private List<PurchaseReceiptInfo> _receiptInfos = new();

        private List<PurchaseReceiptInfo> _beDisplayTarget = new();

        #endregion

        #region Methods

        private void Awake()
        {
            view.onViewShow.AddListener(Refresh);
            viewModel.onDelete.AddListener(Refresh);
        }

        public void Refresh()
        {
            RefreshReceiptList();
            ReleaseUnTracked();
            MakeItems();
            SetSiblingOderByTime();
            _beDisplayTarget = FilteringReceiptByDate();
            DisplayFilteredList();
            reBuilder.ForceRebuildLayout();
            onRefresh.Invoke();
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
                sorted[i].SetIndex(sorted.Count() - i);
            }
        }

        private void MakeItems()
        {
            var needToMake = _receiptInfos.Except(receipts.Select(item => item.receiptInfo).ToList());
            foreach (var receiptInfo in needToMake)
            {
                var newOne = maker.MakeItem();
                newOne.SetDataAndUI(viewModel, receiptInfo);
                receipts.Add(newOne);
            }
        }

        private void ReleaseUnTracked()
        {
            var remove = receipts.Select(item => item.receiptInfo).Except(_receiptInfos).ToList();
            var removeObject = receipts.Where(item => remove.Contains(item.receiptInfo)).ToList();

            foreach (var target in removeObject)
                receipts.Remove(target);

            for (int i = 0; i < removeObject.Count; i++)
                Destroy(removeObject[i].gameObject);
            removeObject.Clear();
        }

        private void DisplayFilteredList()
        {
            foreach (var item in receipts)
                item.gameObject.SetActive(_beDisplayTarget.Contains(item.receiptInfo));
        }

        private List<PurchaseReceiptInfo> FilteringReceiptByDate()
        {
            var list = viewModel.PurchaseHistory.AsEnumerable();

            if (FilterAttributeInfo.limit1 != "")
            {
                var from = DateTimeUtil.DateTimeStringToDateTime(FilterAttributeInfo.limit1);
                list = list.Where(x => x.purchasedAt >= from);
            }

            if (FilterAttributeInfo.limit2 != "")
            {
                var to = DateTimeUtil.DateTimeStringToDateTime(FilterAttributeInfo.limit2);
                list = list.Where(x => x.purchasedAt <= to);
            }

            return list.ToList();
        }

        #endregion
    }
}