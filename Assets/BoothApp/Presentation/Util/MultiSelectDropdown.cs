using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoothApp.Presentation.Util
{
    public class MultiSelectDropdown : MonoBehaviour
    {
        public TMP_Dropdown dropdown;
        public GameObject template;  // 기존 드롭다운의 템플릿 (Panel)
        public Transform content;    // 옵션들이 배치될 부모 오브젝트
        public Toggle togglePrefab;  // 옵션용 토글 프리팹

        private List<Toggle> toggles = new List<Toggle>();
        private List<string> selectedOptions = new List<string>();

    }
}