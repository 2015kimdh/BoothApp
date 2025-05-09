using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private List<PurchaseReceiptInfo> selectedReceipt;

        #endregion

        #region Property

        public int SelectedReceiptCount => selectedReceipt.Count;
        public List<PurchaseReceiptInfo> SelectedReceipt => selectedReceipt;
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
            var targets = selectedReceipt.ToList();
            viewModel.DeletePurchaseHistory(targets);

            InitSelection();
            onDelete.Invoke();
        }
        
        public void AddSelection(PurchaseReceiptInfo itSelf)
        {
            if (!selectedReceipt.Contains(itSelf))
            {
                selectedReceipt.Add(itSelf);
                onSelectReceipt.Invoke();
            }
        }

        public void RemoveSelection(PurchaseReceiptInfo itSelf)
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
        
        public bool CheckIsSelected(PurchaseReceiptInfo itSelf)
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