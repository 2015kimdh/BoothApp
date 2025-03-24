#if UNITY_EDITOR

using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(MonoBehaviour), true)]  // 모든 MonoBehaviour를 대상으로
    public class DynamicButtonEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            MonoBehaviour targetScript = (MonoBehaviour)target;

            // 대상 객체의 모든 public 메서드 가져오기
            MethodInfo[] methods = targetScript.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);

            foreach (var method in methods)
            {
                // ButtonAttribute가 붙은 메서드만 찾아서 버튼 생성
                ButtonAttribute buttonAttribute = (ButtonAttribute)Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));
            
                if (buttonAttribute != null && method.GetParameters().Length == 0) // 매개변수가 없는 public 메서드만
                {
                    // 버튼 텍스트는 ButtonAttribute에서 설정한 값 사용
                    if (GUILayout.Button(buttonAttribute.buttonText))
                    {
                        // 버튼 클릭 시 해당 메서드 호출
                        method.Invoke(targetScript, null);
                    }
                }
            }
        }
    }
}
#endif