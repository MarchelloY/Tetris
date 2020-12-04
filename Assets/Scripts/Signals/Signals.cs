using strange.extensions.signal.impl;
using UnityEngine;

namespace Signals
{
    public class GameSetupSignal : Signal {}
    public class StartSignal : Signal {}
    public class KeyPressedSignal : Signal<KeyCode> {}
    public class ButtonClickedSignal : Signal<string> {}
    public class ScoreChangedSignal : Signal<int> {}
    public class MainUIUpdatedSignal : Signal<int[]> {}
    public class PauseUIUpdatedSignal : Signal<int[]> {} }