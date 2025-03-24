using BoothApp.Data;
using BoothApp.Presentation.Info;
using UnityEngine;

namespace BoothApp.Presentation
{
    public class BoothColumnMaker : MonoBehaviour
    {
        #region SerializeField

        [SerializeField] private GameObject originalPrefab;
        [SerializeField] private GameObject parentsGameObject;
        
        #endregion

        #region PublicField


        #endregion

        #region Method

        public BoothColumn MakeBoothColumn()
        {
            GameObject newItem = Instantiate(originalPrefab, parentsGameObject.transform);
            return newItem.GetComponent<BoothColumn>();
        }
        
        public BoothColumn MakeBoothColumn(BoothInfo info)
        {
            GameObject newItem = Instantiate(originalPrefab, parentsGameObject.transform);
            var columnUI = GetComponent<BoothColumn>();
            columnUI.boothName.text = info.boothInformationInfo.boothName;
            columnUI.createdAt.text = info.boothInformationInfo.createdAt;
            columnUI.updatedAt.text = info.boothInformationInfo.modifyAt;
            return columnUI;
        }

        #endregion
    }
}