using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Code
{
    public class GameState
    {
        private readonly Player player;
        private readonly Map map;

        public GameState(Player player, Map map)
        {
            this.player = player;
            this.map = map;
        }

        public GameStates State
        {
            get
            {
                if (!player.IsAlive)
                    return GameStates.PlayerIsDead;

                if (!map.IsLevelStarted && map.Keys.Count != map.KeyCount)
                    return GameStates.LevelsIsGenerating;

                if (Keyboard.GetState().GetPressedKeys().Contains(Keys.Escape))
                    return GameStates.IsPaused;

                if (map.IsAbleToLeave(player) && Keyboard.GetState().GetPressedKeys().Contains(Keys.E))
                    return GameStates.PlayerIsAbleToFinish;

                return GameStates.IsInMenu;
            }
        }
    }

    public enum GameStates
    {
        IsInMenu,
        LevelsIsGenerating,
        LevelIsActive,
        PlayerIsDead,
        PlayerIsAbleToFinish,
        IsPaused
    }

}
