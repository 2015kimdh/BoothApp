using Newtonsoft.Json;
using UnityEngine;

namespace BoothApp.Utility
{
    public static class ClassCopy
    {
        public static T CopyClass<T>(this T source) where T : new()
        {
            // 직렬화가 불가능한 경우
            if (!typeof(T).IsSerializable)
            {
                Debug.LogError(source.GetType().Name + "은 직렬화가 불가능한 객체입니다.");
                return source;
            }
            
            var stringData = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(stringData);
        }
    }
}