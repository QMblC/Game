﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class GameplayPresenter
    {
        private IGameplayView _gameplayView = null;
        private IGameplayModel _gameplayModel = null;

        public GameplayPresenter(
          IGameplayView gameplayView,
          IGameplayModel gameplayModel
        )
        {
            _gameplayView = gameplayView;
            _gameplayModel = gameplayModel;

            _gameplayView.CycleFinished += ViewModelUpdate;
            _gameplayView.PlayerMoved += ViewModelMovePlayer;
            _gameplayModel.Updated += ModelViewUpdate;

        }

        private void ViewModelMovePlayer(object sender, ControlsEventArgs e)
        {
            _gameplayModel.MovePlayer(e.Direction);
        }

        private void ModelViewUpdate(object sender, GameplayEventArgs e)
        {
            _gameplayView.LoadGameCycleParameters(e.PlayerPos);
        }

        private void ViewModelUpdate(object sender, EventArgs e)
        {
            _gameplayModel.Update();
        }

        public void LaunchGame()
        {
            _gameplayView.Run();
        }
    }
}
