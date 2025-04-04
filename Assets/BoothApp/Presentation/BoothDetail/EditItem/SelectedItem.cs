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

        private BoothItemWithAmountInfo _selectedItem;

        #endregion

        #region Method

        public void SetSelectedItem(string hash)
        {
            var result = selectedBooth.OriginalItemStatus
                .Find(x => x.itemInfo.hash == hash);
            _selectedItem = result;
        }

        #endregion
    }
}