using Diagnostics;
using strange.extensions.mediation.impl;
using Tools.Diagnostics;

namespace Views
{
    public class CleanerView : View
    {
        private void Update()
        {
            if (transform.childCount == 0)
            {
                Destroy(gameObject);

                Debugger.Log(LogEntryCategory.Tetromino, $"Tetromino {gameObject.name} has been removed");
            }
        }
    }
}