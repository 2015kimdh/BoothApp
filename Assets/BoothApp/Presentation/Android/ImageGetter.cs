using BoothApp.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BoothApp.Presentation.Android
{
    public class ImageGetter : MonoBehaviour
    {
        public Image image;
        [SerializeField]
        private GalleryImageGetter imageGetter;

        public UnityEvent<Sprite> onSpriteChange;

        public void GetImage()
        {
            imageGetter.imageComponent = image;
            StartCoroutine(imageGetter.GetImageFromGallery());
            
            onSpriteChange.Invoke(image.sprite);
        }

        public void OnSpriteChangeInvoke()
        {
            onSpriteChange.Invoke(image.sprite);
        }
    }
}