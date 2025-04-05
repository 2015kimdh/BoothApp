using System;
using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.Button
{
    public class ChangeButtonTextByStatus : MonoBehaviour
    {
        #region Serialize Field

        [SerializeField] private TMP_Text text;
        [SerializeField] private string toString;
        
        #endregion

        private string _originalText;
        
        #region Method

        private void Awake()
        {
            _originalText = text.text;
        }

        public void SetText()
        {
            text.text = toString;
        }

        public void ReturnText()
        {
            text.text = _originalText;
        }

        #endregion
    }
}