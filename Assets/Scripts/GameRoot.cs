using strange.extensions.context.impl;

public class GameRoot : ContextView
{
    private void Awake()
    {
        context = new GameContext(this);
    }
}