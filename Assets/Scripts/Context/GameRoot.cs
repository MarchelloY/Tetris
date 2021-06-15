using strange.extensions.context.impl;

namespace Context
{
    public class GameRoot : ContextView
    {
        private void Awake()
        {
            context = new GameContext(this);
        }
    }
}