using System;
using BoothApp.Presentation.Android;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace BoothApp.Presentation.CreateBooth
{
    public class GetImageTextShower : MonoBehaviour
    {
        [SerializeField] private ImageGetter imageGetter;
        [SerializeField] GameObject imageAnnouncementText;

        private void Awake()
        {
            imageGetter.onSpriteChange.AddListener(SetTextShow);
        }

        private void SetTextShow(Sprite sprite)
        {
            if (sprite == null)
            {
                imageAnnouncementText.gameObject.SetActive(true);
            }
            else
            {
                imageAnnouncementText.gameObject.SetActive(false);
            }
        }
    }
}