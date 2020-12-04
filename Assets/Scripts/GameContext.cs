using Commands;
using Mediators;
using Models;
using Models.api;
using Services;
using Services.api;
using Signals;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;
using Views;

public class GameContext : MVCSContext
{
    public GameContext(MonoBehaviour view) : base(view) { }

    public override IContext Start()
    {
        base.Start();
        var startSignal = injectionBinder.GetInstance<StartSignal>();
        startSignal.Dispatch();
        return this;
    }

    protected override void mapBindings()
    {
        injectionBinder.Bind<IGridModel>().To<GridModel>().ToSingleton();
        injectionBinder.Bind<IGameStateModel>().To<GameStateModel>().ToSingleton();
        injectionBinder.Bind<IScoreModel>().To<ScoreModel>().ToSingleton();
        injectionBinder.Bind<IAudioModel>().To<AudioModel>().ToSingleton();
        injectionBinder.Bind<ITetrominoModel>().To<TetrominoModel>().ToSingleton();

        injectionBinder.Bind<IDataService>().To<DataService>().ToSingleton();
        injectionBinder.Bind<ISaveService>().ToValue(new SaveService("saves.json")).ToSingleton().ToName("Save");

        commandBinder.Bind<StartSignal>().To<StartCommand>().Once();
        commandBinder.Bind<GameSetupSignal>().To<GameSetupCommand>();
        commandBinder.Bind<KeyPressedSignal>().To<KeyPressedCommand>();
        commandBinder.Bind<ButtonClickedSignal>().To<ButtonClickedCommand>();
        commandBinder.Bind<ScoreChangedSignal>().To<ScoreChangedCommand>();

        injectionBinder.Bind<MainUIUpdatedSignal>().ToSingleton();
        injectionBinder.Bind<PauseUIUpdatedSignal>().ToSingleton();
        
        mediationBinder.Bind<TetrominoView>().To<TetrominoMediator>();
        mediationBinder.Bind<KeyControlView>().To<KeyControlMediator>();
        mediationBinder.Bind<ButtonView>().To<ButtonMediator>();
        mediationBinder.Bind<UIView>().To<UIMediator>();
    }
}
