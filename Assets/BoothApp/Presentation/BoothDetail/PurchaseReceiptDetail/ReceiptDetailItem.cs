using System.Linq;
using BoothApp.Presentation.Info;
using BoothApp.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoothApp.Presentation.BoothDetail.PurchaseReceiptDetail
{
    public class ReceiptDetailItem : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text itemOwner;
        [SerializeField] private TMP_Text itemTags;
        [SerializeField] private TMP_Text itemPrice;
        [SerializeField] private TMP_Text itemAmount;
        [SerializeField] private Image itemImage;

        #endregion

        #region Method
        
        public void SetUI(BoothItemInfo itemInfo, PurchaseItemInfo receipt)
        {
            itemName.text = itemInfo.name;
            itemOwner.text = itemInfo.owner;
            itemTags.text = string.Join(", ", itemInfo.itemTag);
            itemPrice.text = receipt.pricePerItem.ToString();
            itemAmount.text = receipt.amount.ToString();
            itemImage.sprite = ImageHub.GetImageWithName(itemInfo.imageName);
        }

        #endregion
    }
}