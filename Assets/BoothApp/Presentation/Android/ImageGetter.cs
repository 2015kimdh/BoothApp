using System.Threading.Tasks;
using BoothApp.Utility;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace BoothApp.Presentation.Android
{
    public class ImageGetter : MonoBehaviour
    {
        public RawImage rawImage;
        [SerializeField]
        private GalleryImageGetter imageGetter;

        public void GetImage()
        {
            imageGetter.rawImage = rawImage;
            StartCoroutine(imageGetter.GetImageFromGallery());
        }
    }
}