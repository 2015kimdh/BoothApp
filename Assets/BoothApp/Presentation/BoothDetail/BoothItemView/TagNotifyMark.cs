using UnityEngine;
using UnityEngine.UI;

namespace BoothApp.Presentation.BoothDetail.BoothItemView
{
    public class TagNotifyMark : MonoBehaviour
    {
        [SerializeField] private Image mark;

        public void SetMark(int tagAmount, bool soldOutFilter)
        {
            mark.gameObject.SetActive(tagAmount != 0 || soldOutFilter);
        }
    }
}