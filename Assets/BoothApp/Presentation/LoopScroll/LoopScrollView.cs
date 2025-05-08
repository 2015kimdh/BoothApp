using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoothApp.Presentation.LoopScroll
{
    public class LoopScrollView<T> : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private RectTransform content;
        [SerializeField] private float itemHight;
        [SerializeField] private float itemGap;
        [SerializeField] private int poolSize;

        #endregion

        #region Private Fields

        public List<RectTransform> _itemsRect = new();
        public List<int> _itemIndices = new();
        public List<T> _dataList = new();
        private float _previousScrollPos = 0f;

        #endregion

        #region MonoBehaviour Event

        private void Start()
        {
            InitPool();
            scrollRect.onValueChanged.AddListener(OnScroll);
        }

        #endregion

        #region Method

        private void InitPool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                var item = Instantiate(itemPrefab, content);
                var rect = item.GetComponent<RectTransform>();
                _itemsRect.Add(rect);
                _itemIndices.Add(-1);
            }

            content.sizeDelta = new Vector2(content.sizeDelta.x, _dataList.Count * (itemHight + itemGap));
        }

        public void SetData(List<T> newData)
        {
            _dataList = newData;
            content.sizeDelta = new Vector2(content.sizeDelta.x, _dataList.Count * (itemHight + itemGap));
            for(int i = 0; i < _itemIndices.Count; i++)
                _itemIndices[i] = -1;
            RefreshVisibleItems();
        }
        
        private void OnScroll(Vector2 pos)
        {
            float scrollY = content.anchoredPosition.y;
            
            if(Mathf.Abs(scrollY - _previousScrollPos) < (itemHight+itemGap) / 2f) return;
            _previousScrollPos = scrollY;

            RefreshVisibleItems();
        }

        private void RefreshVisibleItems()
        {
            //if (_dataList == null || _dataList.Count == 0) return;
            
            int startIndex = Mathf.FloorToInt(content.anchoredPosition.y / itemHight);
            startIndex = Mathf.Clamp(startIndex, 0, Mathf.Max(0, _dataList.Count - poolSize));

            for (int i = 0; i < _itemsRect.Count; i++)
            {
                int dataIndex = startIndex + i;

                if (dataIndex >= _dataList.Count)
                {
                    _itemsRect[i].gameObject.SetActive(false);
                    continue;
                }

                _itemsRect[i].gameObject.SetActive(true);
                SetItemPosition(_itemsRect[i], dataIndex);

                if (_itemIndices[i] != dataIndex)
                {
                    _itemIndices[i] = dataIndex;
                    var view = _itemsRect[i].GetComponent<ILoopScrollItem<T>>();
                    if (view != null)
                    {
                        view.UpdateItem(_dataList[dataIndex], dataIndex);
                    }
                }
            }
        }

        private void SetItemPosition(RectTransform item, int index)
        {
            item.anchoredPosition = new Vector2(0, -index * (itemHight + itemGap));
        }
        
        #endregion
    }
}