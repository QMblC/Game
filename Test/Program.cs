using System;

namespace Test
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            GameplayPresenter gp = new GameplayPresenter(new GameCycleView (), new GameCycle());
            gp.LaunchGame();
        }
    }
}