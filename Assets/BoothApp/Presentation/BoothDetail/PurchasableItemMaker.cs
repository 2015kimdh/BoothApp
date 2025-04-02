using UnityEngine;

namespace BoothApp.Presentation.BoothDetail
{
    public class PurchasableItemMaker : MonoBehaviour
    {
        #region Property

        public GameObject ParentObject => parentObject;

        #endregion
        
        #region Serialize Fields

        [SerializeField] private GameObject parentObject;
        [SerializeField]
        private GameObject purchasableItemPrefab;

        #endregion

        #region Method

        public PurchasableItem MakeNewPurchasableItem()
        {
            var newItem = Instantiate(purchasableItemPrefab,parentObject.transform);
            return newItem.GetComponentInChildren<PurchasableItem>();
        }
        
        #endregion
    }
}