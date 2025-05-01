using UnityEngine;
using UnityEngine.UI;

namespace BoothApp.Presentation.BoothDetail
{
    public class ScrollRectVisibilityHandler : MonoBehaviour
    {
        public ScrollRect scrollRect;
        public RectTransform content;
        public float threshold = 0.1f;

        private float itemHeight;
        private int totalCount;

        void Start()
        {
            scrollRect.onValueChanged.AddListener(OnScrollChanged);
            scrollRect.onValueChanged.AddListener(delegate { UpdateContentHeight();});

            // 첫 아이템 높이 측정
            if (content.childCount > 0)
            {
                RectTransform first = content.GetChild(0) as RectTransform;
                itemHeight = first.rect.height;
            }

            totalCount = content.childCount;

            // ContentSizeFitter 대신 수동으로 content 높이 설정
            float contentHeight = itemHeight * totalCount;
            content.sizeDelta = new Vector2(content.sizeDelta.x, contentHeight);

            // 초기 상태 처리
            OnScrollChanged(Vector2.zero);
        }

        private void UpdateContentHeight()
        {
            float itemHeight = 0f;
            float spacing = 0f;
            int activeCount = 0;

            // Layout 정보
            VerticalLayoutGroup layoutGroup = content.GetComponent<VerticalLayoutGroup>();
            if (layoutGroup != null)
            {
                spacing = layoutGroup.spacing;
            }

            for (int i = 0; i < content.childCount; i++)
            {
                RectTransform child = content.GetChild(i) as RectTransform;
                //if (child.gameObject.activeSelf)
                {
                    if (itemHeight == 0f)
                        itemHeight = child.rect.height; // 최초 하나로 계산
                    activeCount++;
                }
            }

            float totalHeight = layoutGroup.padding.top + layoutGroup.padding.bottom
                                                        + activeCount * itemHeight
                                                        + Mathf.Max(0, (activeCount - 1)) * spacing;

            content.sizeDelta = new Vector2(content.sizeDelta.x, totalHeight);
        }
        
        private void OnScrollChanged(Vector2 scrollPosition)
        {
            Rect viewportRect = scrollRect.viewport.rect;
            Vector3[] viewportCorners = new Vector3[4];
            scrollRect.viewport.GetWorldCorners(viewportCorners);
            float viewportMinY = viewportCorners[0].y;
            float viewportMaxY = viewportCorners[1].y;
            float thresholdPixels = viewportRect.height * threshold;

            foreach (RectTransform child in content)
            {
                Vector3[] itemCorners = new Vector3[4];
                child.GetWorldCorners(itemCorners);
                float itemMinY = itemCorners[0].y;
                float itemMaxY = itemCorners[1].y;

                bool isVisible = !(itemMaxY < viewportMinY - thresholdPixels ||
                                   itemMinY > viewportMaxY + thresholdPixels);
                child.gameObject.SetActive(isVisible);
            }
        }
    }
}