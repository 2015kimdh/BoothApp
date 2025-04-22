using System.Collections.Generic;
using BoothApp.Presentation.Info;
using BoothApp.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory.DeleteReceipt
{
    public class DeleteSelectedReceiptViewModel : MonoBehaviour
    {
        #region Unity Event

        public UnityEvent onDelete;

        /// <summary>
        /// 아이템이 선택되었을 때
        /// </summary>
        public UnityEvent onSelectReceipt;

        #endregion

        #region Serialize Field

        [SerializeField] private PurchaseHistoryView view;
        [SerializeField] private PurchaseHistoryViewModel viewModel;
        [SerializeField] private List<PurchaseHistoryReceiptItem> selectedReceipt;

        #endregion

        #region Property

        public int SelectedReceiptCount => selectedReceipt.Count;
        
        private BoothInfo SelectedBooth => viewModel.SelectedBooth;

        #endregion

        #region Method

        private void Awake()
        {
            view.onViewStatusChange.AddListener(OnStatusChange);
        }
        
        /// <summary>
        /// 항목 지우기.
        /// </summary>
        public void DeleteSelectedReceipt()
        {
            foreach (var target in selectedReceipt)
                viewModel.DeletePurchaseHistory(target.receiptInfo);

            InitSelection();
            onDelete.Invoke();
        }
        
        public void AddSelection(PurchaseHistoryReceiptItem itSelf)
        {
            if (!selectedReceipt.Contains(itSelf))
            {
                selectedReceipt.Add(itSelf);
                onSelectReceipt.Invoke();
            }
        }

        public void RemoveSelection(PurchaseHistoryReceiptItem itSelf)
        {
            if (selectedReceipt.Contains(itSelf))
            {
                selectedReceipt.Remove(itSelf);
                onSelectReceipt.Invoke();
            }
        }
        
        public void InitSelection()
        {
            selectedReceipt.Clear();
            onSelectReceipt.Invoke();
        }
        
        public bool CheckIsSelected(PurchaseHistoryReceiptItem itSelf)
        {
            return selectedReceipt.Contains(itSelf);
        }
        
        private void OnStatusChange(PurchaseHistoryViewStatus status)
        {
            if(status != PurchaseHistoryViewStatus.Delete)
                InitSelection();
        }

        #endregion
    }
}