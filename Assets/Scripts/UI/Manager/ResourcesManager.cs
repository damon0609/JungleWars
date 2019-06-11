using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ResourcesManager : MonoSingleton<ResourcesManager>
    {
        public UnityEngine.Object Load(string path)
        {
            return Resources.Load(path);
        }
        public T Load<T>(string path) where T : UnityEngine.Object
        {
            return Resources.Load<T>(path);
        }

        public T LoadAndInstantiate<T>(string path) where T:UnityEngine.Object
        {
            return GameObject.Instantiate<T>(Resources.Load<T>(path));
        }
    }
}
