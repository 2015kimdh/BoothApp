using System;
using BoothApp.Presentation.Info;
using UnityEngine;
using UnityEngine.Pool;

namespace BoothApp.Presentation.BoothDetail.PurchaseHistory
{
    public class PurchaseReceiptItemMaker : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private Transform prefabParent;

        #endregion

        #region Private Fields

        public IObjectPool<PurchaseHistoryReceiptItem> pool;

        #endregion

        #region Method

        private void Awake()
        {
            pool = new ObjectPool<PurchaseHistoryReceiptItem>(Create, actionOnGet:Get,actionOnRelease:Release);
        }

        public PurchaseHistoryReceiptItem MakeItem()
        {
            var receiptItem = Instantiate(itemPrefab).GetComponent<PurchaseHistoryReceiptItem>();
            receiptItem.gameObject.transform.SetParent(prefabParent);
            return receiptItem;
        }
        
        #endregion

        private PurchaseHistoryReceiptItem Create()
        {
            var newItem = Instantiate(itemPrefab, prefabParent);
            var receiptItem = newItem.GetComponent<PurchaseHistoryReceiptItem>();
            receiptItem.gameObject.transform.SetParent(prefabParent);
            return receiptItem;
        }
        
        public void Get(PurchaseHistoryReceiptItem item)
        {
            item.gameObject.SetActive(true);
        }

        public void Release(PurchaseHistoryReceiptItem element)
        {
            element.gameObject.SetActive(false);
        }

        public void Clear()
        {
            pool.Clear();
        }
    }
}