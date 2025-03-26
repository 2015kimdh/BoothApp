using System;
using UnityEngine;
using UnityEngine.Events;

namespace BoothApp.Presentation.Android
{
    public class AndroidBackButtonListener : MonoBehaviour
    {
        public UnityEvent onBackButton;
        private void Update()
        {
            #if UNITY_ANDROID
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("백스페이스");
                onBackButton.Invoke();
            }
            #endif

        }
    }
}