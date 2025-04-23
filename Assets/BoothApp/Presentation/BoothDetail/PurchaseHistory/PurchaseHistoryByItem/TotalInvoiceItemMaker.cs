using UnityEngine;
using UnityEngine.Pool;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory.PurchaseHistoryByItem
{
    public class TotalInvoiceItemMaker : MonoBehaviour
    {
        #region SerializeField

        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private Transform prefabParent;

        #endregion
        
        #region Private Fields

        public IObjectPool<TotalInvoiceItem> pool;

        #endregion
        
        #region Method

        private void Awake()
        {
            pool = new ObjectPool<TotalInvoiceItem>(Create, actionOnGet: Get, actionOnRelease: Release);
        }

        private TotalInvoiceItem Create()
        {
            var newItem = Instantiate(itemPrefab, prefabParent);
            var totalInvoiceItem = newItem.GetComponent<TotalInvoiceItem>();
            totalInvoiceItem.gameObject.transform.SetParent(prefabParent);
            return totalInvoiceItem;
        }
        
        public void Get(TotalInvoiceItem item)
        {
            item.gameObject.SetActive(true);
        }

        public void Release(TotalInvoiceItem element)
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