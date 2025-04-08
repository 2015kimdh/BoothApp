using BoothApp.Presentation.Info;
using Doozy.Runtime.Signals;
using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace BoothApp.Presentation.BoothDetail.EditItem
{
    public class SelectedItemViewModel : MonoBehaviour
    {
        #region UnityEvent

        public UnityEvent onItemSet;

        #endregion
        
        #region Serialize Field

        [FormerlySerializedAs("selectedBooth")] [SerializeField] private SelectedBoothViewModel selectedBoothViewModel;

        #endregion

        #region Private Field

        private BoothItemWithAmountInfo _selectedItemPurchased;

        [SerializeField]
        private BoothItemWithAmountInfo _selectedItem;
        public BoothItemWithAmountInfo selectedItem
        {
            get => _selectedItem;
            private set => _selectedItem = value;
        }

        public BoothItemWithAmountInfo selectedItemPurchased
        {
            get => _selectedItemPurchased;
            private set => _selectedItemPurchased = value;
        }

        #endregion

        #region Method

        public void SetSelectedItem(string hash)
        {
            selectedItem = selectedBoothViewModel.selectedBooth.GetOriginalItem(hash);
            selectedItemPurchased = selectedBoothViewModel.selectedBooth.GetPurchasedItem(hash);
            onItemSet.Invoke();
            SignalStream stream = SignalsService.GetStream(nameof(UISelectable), nameof(UIButton));
            stream.SendSignal(new UIButtonSignalData("ViewChangeButton", "ToEditSelectedItem", ButtonTrigger.Click));
        }

        #endregion
    }
}