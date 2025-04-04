using BoothApp.Presentation.Info;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.EditItem
{
    public class SelectedItem : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private SelectedBooth selectedBooth;

        #endregion

        #region Private Field

        public BoothItemWithAmountInfo selectedItem
        {
            get => selectedItem;
            private set => selectedItem = value;
        }

        public BoothItemWithAmountInfo selectedItemPurchased
        {
            get => selectedItemPurchased;
            private set => selectedItemPurchased = value;
        }

        #endregion

        #region Method

        public void SetSelectedItem(string hash)
        {
            selectedItem = selectedBooth.selectedBooth.GetOriginalItem(hash);;
            selectedItemPurchased = selectedBooth.selectedBooth.GetPurchasedItem(hash);
        }

        #endregion
    }
}