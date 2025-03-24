using System;

#if UNITY_EDITOR

namespace Editor
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : Attribute
    {
        public string buttonText;

        public ButtonAttribute(string buttonText)
        {
            this.buttonText = buttonText;
        }
    }
}
#endif