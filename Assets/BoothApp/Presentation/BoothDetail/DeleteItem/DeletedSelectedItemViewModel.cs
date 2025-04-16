using System;
using System.Collections.Generic;
using System.Linq;
using BoothApp.Presentation.Info;
using BoothApp.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.BoothDetail.DeleteItem
{
    public class DeletedSelectedItemViewModel : MonoBehaviour
    {
        #region Unity Event

        public UnityEvent onDelete;

        /// <summary>
        /// 아이템이 선택되었을 때
        /// </summary>
        public UnityEvent onSelectItem;

        #endregion

        #region Serialize Field

        [SerializeField] private SelectedBoothViewModel selectedBoothViewModel;
        [SerializeField] private SelectedBoothView selectedBoothView;

        [SerializeField] private List<PurchasableItem> selectedItems = new();

        #endregion

        #region Property

        private BoothInfo SelectedBooth => selectedBoothViewModel.selectedBooth;

        #endregion

        #region Private Method

        private void Awake()
        {
            selectedBoothView.onViewStatusChange.AddListener(OnStatusChange);
        }

        /// <summary>
        /// 항목 지우기.
        /// 판매 기록에 있는 아이템은 지우지 않음. (판매 내역의 내용을 보여주어야 하기 때문)
        /// </summary>
        public void DeleteSelectedItem()
        {
            List<BoothItemWithAmountInfo> needToDelete = new();
            foreach (var target in selectedItems)
            {
                var removable = SelectedBooth.GetOriginalItem(target.hash);
                if (removable != null)
                    needToDelete.Add(removable);
            }

            foreach (var target in needToDelete)
            {
                SelectedBooth.boothInformationInfo.originalItemStatus.Remove(target);
            }

            SelectedBooth.boothInformationInfo.modifyAt = DateTimeUtil.DateTimeNowToString();
            InitSelection();
            onDelete.Invoke();
        }

        public void InitSelection()
        {
            selectedItems.Clear();
            onSelectItem.Invoke();
        }

        public void AddSelection(PurchasableItem itSelf)
        {
            if (!selectedItems.Contains(itSelf))
            {
                selectedItems.Add(itSelf);
                onSelectItem.Invoke();
            }
        }

        public void RemoveSelection(PurchasableItem itSelf)
        {
            if (selectedItems.Contains(itSelf))
            {
                selectedItems.Remove(itSelf);
                onSelectItem.Invoke();
            }
        }

        public bool CheckIsSelected(PurchasableItem itSelf)
        {
            return selectedItems.Contains(itSelf);
        }

        private void OnStatusChange(SelectedBoothViewStatus status)
        {
            if(status != SelectedBoothViewStatus.DeleteItem)
                InitSelection();
        }
        
        #endregion
    }
}