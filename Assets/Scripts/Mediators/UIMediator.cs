using Data;
using Signals;
using strange.extensions.mediation.impl;
using UnityEngine;
using Views;

namespace Mediators
{
    public class UIMediator : Mediator
    {
        [Inject]
        private readonly UIView _view = null;
        [Inject]
        private readonly MainUIUpdatedSignal _mainUIUpdatedSignal = null;
        [Inject]
        private readonly PauseUIUpdatedSignal _pauseUIUpdatedSignal = null;
        [Inject]
        private readonly SetActivePanelSignal _setActivePanelSignal = null;
        [Inject]
        private readonly ChangeLightBulbsColorSignal _changeLightBulbsColorSignal = null;

        public override void OnRegister()
        {
            base.OnRegister();

            _setActivePanelSignal.AddListener(OnPanelStateChanged);
            _mainUIUpdatedSignal.AddListener(OnMainUIUpdated);
            _pauseUIUpdatedSignal.AddListener(OnPauseUIUpdated);
            _changeLightBulbsColorSignal.AddListener(OnChangeLightBulbsColor);
        }
    
        public override void OnRemove()
        {
            base.OnRemove();

            _setActivePanelSignal.RemoveListener(OnPanelStateChanged);
            _mainUIUpdatedSignal.RemoveListener(OnMainUIUpdated);
            _pauseUIUpdatedSignal.RemoveListener(OnPauseUIUpdated);
            _changeLightBulbsColorSignal.RemoveListener(OnChangeLightBulbsColor);
        }

        private void OnMainUIUpdated(GameData gameData)
        {
            _view.UpdateMainUI(gameData);
        }

        private void OnPauseUIUpdated(GameData gameData)
        {
            _view.UpdatePauseUI(gameData);
        }

        private void OnPanelStateChanged(string panelName, bool isActive)
        {
            _view.SetActivePanelByName(panelName, isActive);
        }

        private void OnChangeLightBulbsColor(Color32 color)
        {
            _view.SetLightBulbsColor(color);
        }
    }
}