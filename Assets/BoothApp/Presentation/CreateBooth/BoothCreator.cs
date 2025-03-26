using System;
using System.Linq;
using BoothApp.Presentation.Info;
using BoothApp.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BoothApp.Presentation.CreateBooth
{
    public class BoothCreator : MonoBehaviour
    {
        #region Unity Events

        public UnityEvent onCreationSuccess;
        public UnityEvent onCreationFail;

        #endregion
        
        #region Serialize Field

        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private RawImage boothImage;

        #endregion

        #region Private Fields

        private BoothDataPresenter _presenter;

        #endregion

        #region MonoBehaviour Events

        private void Awake()
        {
            // 기존 부스 정보를 가져오기 위해서 필요함
            _presenter = FindObjectOfType<BoothDataPresenter>();
        }

        private void OnEnable()
        {
            inputField.text = "";
            boothImage.texture = null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 부스 생성 시도 함수.
        /// 부스 생성에 실패할 시 false 반환
        /// </summary>
        /// <returns></returns>
        public void TryToMakeBooth()
        {
            bool isValid = CheckBoothNameValidation();
            if (!isValid)
            {
                onCreationFail.Invoke();
                return;
            }

            BoothInfo newInfo = new();
            
            newInfo.boothInformationInfo = new BoothInformationInfo();
            newInfo.savedAt = DateTimeUtil.DateTimeNowToString();
            newInfo.boothInformationInfo.boothName = inputField.text;
            newInfo.boothInformationInfo.createdAt = newInfo.savedAt;
            newInfo.boothInformationInfo.modifyAt = newInfo.savedAt;
            
            if(boothImage.texture != null)
                newInfo.boothInformationInfo.imageName = boothImage.texture.name;

            _presenter.boothInfo.Add(newInfo);
            _presenter.SaveDataAtDisk();
            
            onCreationSuccess.Invoke();
            return;
        }

        private bool CheckBoothNameValidation()
        {
            // ReSharper disable once ReplaceWithSingleAssignment.True
            bool isValid = true;
            if (inputField.text.Length == 0)
                isValid = false;
            
            // 같은 명칭의 부스 아이템이 있는지 검사하여 반환
            var conclusion = _presenter.boothInfo
                .Where(t => t.boothInformationInfo.boothName == inputField.text);
            if (conclusion.Count() != 0)
                isValid = false;

            return isValid;
        }

        public void InitInputData()
        {
            inputField.text = "";
            boothImage.texture = null;
        }
        
        #endregion
    }
}