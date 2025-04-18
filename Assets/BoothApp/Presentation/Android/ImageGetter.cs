using System.Collections;
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
            StartCoroutine(GetImageRoutine());
        }

        public void OnSpriteChangeInvoke()
        {
            onSpriteChange.Invoke(image.sprite);
        }

        IEnumerator GetImageRoutine()
        {
            Coroutine coroutine = StartCoroutine(imageGetter.GetImageFromGallery());
            yield return coroutine;
            onSpriteChange.Invoke(image.sprite);
        }
    }
}