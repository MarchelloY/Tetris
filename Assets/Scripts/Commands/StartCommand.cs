using Signals;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using Views;

namespace Commands
{
    public class StartCommand : Command
    {
        [Inject] public GameSetupSignal GameSetup { get; set; }
        [Inject(ContextKeys.CONTEXT_VIEW)] public GameObject ContextView { get; set; }

        public override void Execute()
        {
            var buttonControls = new GameObject {name = "Button Controls"};
            buttonControls.AddComponent<KeyControlView>();
            buttonControls.transform.parent = ContextView.transform;

            GameSetup.Dispatch();
        }
    }
}
