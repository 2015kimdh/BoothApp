using System.Collections.Generic;
using BoothApp.Data;
using BoothApp.Mapper;
using BoothApp.Presentation.Info;
using BoothApp.Utility;
using UnityEngine;

namespace BoothApp.Presentation
{
    public class BoothDataPresenter : MonoBehaviour
    {
        #region Public Field

        public List<BoothInfo> boothInfo = new();

        #endregion

        #region Serialize Field

        [SerializeField] private bool isInitialize = false;
        [SerializeField] private BoothDataService boothDataService;

        #endregion

        #region MonoBehaviour Event

        private void Awake()
        {
            GetDataFromService();
        }

        #endregion

        #region Method

        /// <summary>
        /// 서비스에서 데이터를 가져오는 것은 딱 한 번
        /// </summary>
        private void GetDataFromService()
        {
            if (isInitialize)
                return;
            foreach (var data in boothDataService.data)
                boothInfo.Add(data.ToInfo());
            boothInfo.Sort(((infoA, infoB) =>
            {
                return DateTimeUtil.DateTimeStringToDateTime(infoA.boothInformationInfo.createdAt)
                    .CompareTo(DateTimeUtil.DateTimeStringToDateTime(infoB.boothInformationInfo.createdAt));
            }));
            isInitialize = true;
        }

        /// <summary>
        /// Presentation 레이어 상에서 변경된 변경점들을 적용하기 위한
        /// Set 함수
        /// </summary>
        public void SetDataAtService()
        {
            List<BoothData> data = new();
            foreach (var info in boothInfo)
                data.Add(info.ToData());

            boothDataService.data = data;
        }

        public void SaveDataAtDisk()
        {
            SetDataAtService();
            boothDataService.DeleteNoExistData();
            boothDataService.SaveData();
        }

        #endregion
    }
}