using System.Text;
using Models.api;
using Services.api;
using strange.extensions.injector.api;
using UnityEditor;
using UnityEngine;

namespace Tools.Debug
{
    public class GridDebug : MonoBehaviour
    {
        private static ICrossContextInjectionBinder _injectionBinder;

        public static void SetContext(ICrossContextInjectionBinder injectionBinder)
        {
            _injectionBinder = injectionBinder;
        }

        [MenuItem("Debug/Grid/ShowCurrentGrid")]
        public static void DoSomething()
        {
            var stringBuilder = new StringBuilder();

            for (var y = _injectionBinder.GetInstance<IDataService>().GridHeight - 1; y >= 0; y--)
            {
                stringBuilder.Append("[");

                for (var x = 0; x < _injectionBinder.GetInstance<IDataService>().GridWidth; x++)
                {
                    var isTransform = _injectionBinder.GetInstance<IGridModel>().Grid[x, y] == null ? "0" : "3";
                    stringBuilder.Append($"{isTransform}");
                }

                stringBuilder.Append("]\n");

            }

            UnityEngine.Debug.Log(stringBuilder.ToString());
        }
    }
}