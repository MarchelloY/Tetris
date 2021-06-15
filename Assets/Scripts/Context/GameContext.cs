using Commands;
using Mediators;
using Models;
using Models.api;
using Services;
using Services.api;
using Signals;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using Tools.Debug;
using UnityEngine;
using Views;

namespace Context
{
    public class GameContext : MVCSContext
    {
        public GameContext(MonoBehaviour view) : base(view) { }

        public override IContext Start()
        {
            base.Start();

            var startSignal = injectionBinder.GetInstance<GameSetupSignal>();
            startSignal.Dispatch();
            GridDebug.SetContext(injectionBinder);

            return this;
        }

        protected override void mapBindings()
        {
            BindModels();
            BindServices();
            BindSignalsToCommands();
            BindSignals();
            BindViewToMediators();
        }

        private void BindModels()
        {
            injectionBinder.Bind<IGridModel>().To<GridModel>().ToSingleton();
            injectionBinder.Bind<IGameStateModel>().To<GameStateModel>().ToSingleton();
            injectionBinder.Bind<IScoreModel>().To<ScoreModel>().ToSingleton();
            injectionBinder.Bind<ITetrominoModel>().To<TetrominoModel>().ToSingleton();
        }

        private void BindServices()
        {
            injectionBinder.Bind<IDataService>().To<DataService>().ToSingleton();
            injectionBinder.Bind<ISaveService>().To<SaveService>().ToSingleton();
        }

        private void BindSignalsToCommands()
        {
            commandBinder.Bind<GameSetupSignal>().To<GameSetupCommand>();
            commandBinder.Bind<PlayerActionHappenedSignal>().To<PlayerActionHappenedCommand>();
            commandBinder.Bind<ButtonClickedSignal>().To<ButtonClickedCommand>();
            commandBinder.Bind<LinesCollectedSignal>().To<LinesCollectedCommand>();
            commandBinder.Bind<FellSignal>().To<FellCommand>();
        }

        private void BindSignals()
        {
            injectionBinder.Bind<MainUIUpdatedSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<PauseUIUpdatedSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<SetActivePanelSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<ChangeLightBulbsColorSignal>().ToSingleton().CrossContext();
        }

        private void BindViewToMediators()
        {
            mediationBinder.Bind<TetrominoView>().To<TetrominoMediator>();
#if UNITY_EDITOR
            mediationBinder.Bind<KeyControlView>().To<KeyControlMediator>();
#endif
            mediationBinder.Bind<ButtonView>().To<ButtonMediator>();
            mediationBinder.Bind<UIView>().To<UIMediator>();
        }
    }
}