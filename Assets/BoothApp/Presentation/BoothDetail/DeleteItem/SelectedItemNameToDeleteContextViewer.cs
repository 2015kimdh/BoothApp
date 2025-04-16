using System;
using TMPro;
using UnityEngine;

namespace BoothApp.Presentation.BoothDetail.DeleteItem
{
    /// <summary>
    /// 삭제할 항목들을 보여주는 UI 내용 채워주는 컴포넌트
    /// </summary>
    public class SelectedItemNameToDeleteContextViewer : MonoBehaviour
    {
        [SerializeField] private DeleteSelectedItemViewModel viewModel;
        [SerializeField] private TMP_Text text;

        private void Awake()
        {
            viewModel.onSelectItem.AddListener(SetText);
        }

        private void SetText()
        {
            string contents = "";
            
            for (int i = 0; i < viewModel.SelectedItemName.Count; i++)
            {
                if (i != 0)
                    contents += "<br>";
                contents += viewModel.SelectedItemName[i];
            }

            text.text = contents;
        }
    }
}