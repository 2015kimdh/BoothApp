using UnityEngine;
using UnityEngine.Pool;

namespace BoothApp.Presentation.BoothDetail.PurchaseReceiptDetail
{
    public class ReceiptDetailItemMaker : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private Transform prefabParent;

        #endregion

        #region Private Fields

        public IObjectPool<ReceiptDetailItem> pool;

        #endregion

        #region Method

        private void Awake()
        {
            pool = new ObjectPool<ReceiptDetailItem>(Create, actionOnGet: Get, actionOnRelease: Release);
        }

        private ReceiptDetailItem Create()
        {
            var newItem = Instantiate(itemPrefab, prefabParent);
            var detailItem = newItem.GetComponent<ReceiptDetailItem>();
            detailItem.gameObject.transform.SetParent(prefabParent);
            return detailItem;
        }
        
        public void Get(ReceiptDetailItem item)
        {
            item.gameObject.SetActive(true);
        }

        public void Release(ReceiptDetailItem element)
        {
            element.gameObject.SetActive(false);
        }

        public void Clear()
        {
            pool.Clear();
        }

        #endregion
    }
}