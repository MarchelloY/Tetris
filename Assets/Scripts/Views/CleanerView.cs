using strange.extensions.mediation.impl;

namespace Views
{
    public class CleanerView : View
    {
        private void Update()
        {
            if (transform.childCount == 0) Destroy(gameObject);
        }
    }
}