using Diagnostics;
using Tools.Abstract;
using Tools.Diagnostics;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class Singleton<T> : MonoBehaviour, IInitializable where T : Singleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var inst = FindObjectOfType(typeof(T)) as T;

                    if (inst != null)
                    {
                        inst.SetSingleton();
                        _instance = inst;
                    }

                    Debugger.LogWarning(LogEntryCategory.General,"Couldn't find singleton");
                }

                return _instance;
            }
        }

        private void SetSingleton()
        {
            if (_instance == null)
            {
                _instance = (T) this;
                _instance.Init();
            }
            else if (_instance != this)
            {
                Debugger.LogWarning(LogEntryCategory.General, "Found duplicate of " + typeof(T));
                Destroy(gameObject);
            }
        }

        private void Awake()
        {
            SetSingleton();
        }

        public abstract void Init();

        private void OnApplicationQuit()
        {
            _instance = null;
        }

        protected virtual void OnDestroy()
        {
            _instance = null;
        }
    }
}