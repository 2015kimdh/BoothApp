using BoothApp.Presentation.Info;
using BoothApp.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory.PurchaseHistoryByItem
{
    public class TotalInvoiceItem : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text itemOwner;
        [SerializeField] private TMP_Text itemTags;
        [SerializeField] private TMP_Text itemPrice;
        [SerializeField] private TMP_Text itemAmount;
        [SerializeField] private Image itemImage;

        #endregion

        #region Public Field

        public BoothItemInfo itemInfo;

        #endregion
        
        #region Method
        
        public void SetUI(BoothItemInfo info, int totalPrice, int totalPurchaseCount)
        {
            itemInfo = info;
            itemName.text = info.name;
            itemOwner.text = info.owner;
            itemTags.text = string.Join(", ", info.itemTag);
            itemPrice.text = totalPrice.ToString();
            itemAmount.text = totalPurchaseCount.ToString();
            itemImage.sprite = ImageHub.GetImageWithName(info.imageName);
        }

        #endregion
    }
}