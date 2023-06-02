using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Code
{
    public enum LevelId
    {
        Pause = -1,
        Menu = 0,
        FirstLevel = 1,
        SecondLevel,
        ThirdLevel,
        Death
    }
    public abstract class Level
    {
        public abstract Texture2D Texture { get; }

        public abstract List<string> Cells { get; }

        public abstract Vector2 StartPos { get; }
        public abstract int KeyCount { get;}
        public abstract int SpotCount { get;}
    }
    public class Pause : Level
    {
        public override Texture2D Texture => GameCycle.textures[12];

        public override List<string> Cells => throw new NotImplementedException();

        public override Vector2 StartPos => throw new NotImplementedException();

        public override int KeyCount => throw new NotImplementedException();

        public override int SpotCount => throw new NotImplementedException();
    }

    public class Menu : Level
    {
        public override Texture2D Texture => GameCycle.textures[12];
        public override List<string> Cells => throw new NotImplementedException();

        public override Vector2 StartPos => throw new NotImplementedException();

        public override int KeyCount => throw new NotImplementedException();

        public override int SpotCount => throw new NotImplementedException();
    }

    public class FirstLevel : Level
    {
        public override List<string> Cells => new()
        {
            "############",
            "#  #K  S# K#",
            "# #### ## ##",
            "# # K# #   #",
            "# # ## # # #",
            "#M         #",
            "# #### ### #",
            "# K#K#     #",
            "# ## # # # #",
            "# K#     #M#",
            "##   ###  K#",
            "############"
        };

        public override Vector2 StartPos => new(1200, 1200);
        public override int KeyCount => 3;
        public override int SpotCount
        {
            get
            {
                var spots = 0;
                foreach (var line in Cells)
                    foreach (var cell in line)
                        if (cell == 'K')
                            spots++;
                return spots;
            }
        }

        public override Texture2D Texture => throw new NotImplementedException();
    }

    public class SecondLevel : Level
    {
        public override List<string> Cells => new()
            {
                "########################",
                "#         #   #K  ###K #",
                "# ### #  ## # ###   ## #",
                "#     ##   M   #  #  # #",
                "# ## ##  ## ##   ###   #",
                "###M    ##   ###     ###",
                "#K## # ##### #K#### ## #",
                "#    ###   #   # K# M  #",
                "## ###K# ### ### ## ## #",
                "#  #K  #       #  ###  #",
                "# #### #  ### ## ## # ##",
                "#   #    ##K#    #     #",
                "#  ## ####  ###    ### #",
                "##       #       ###   #",
                "## # ###   ## #### #K###",
                "#  ### ##  #    #  ###K#",
                "# ##    ##   ##K# ## # #",
                "#  ##  M ###    #      #",
                "##  #   ##K## ####### ##",
                "### ## ##  #   ##     ##",
                "#K#  ###  ## #  # ###  #",
                "# ##   # ##  ##M  #S## #",
                "#    #M     ##K #   #K #",
                "########################"
            };
        public override Vector2 StartPos => new(1200, 1200);
        public override int KeyCount => 5;
        public override int SpotCount
        {
            get
            {
                var spots = 0;
                foreach (var line in Cells)
                    foreach (var cell in line)
                        if (cell == 'K')
                            spots++;
                return spots;
            }
        }

        public override Texture2D Texture => throw new NotImplementedException();
    }

    public class ThirdLevel : Level
    {
        public override List<string> Cells => new()
            {
                "########################",
                "#  #        #  ##    ###",
                "# ## ## ### ##     #  ##",
                "#    #    #  ##  ####  #",
                "# ##### ###   ## #  #  #",
                "#                      #",
                "### #####              #",
                "# ### ###              #",
                "#  ##   ###            #",
                "##  ###                #",
                "#       ##             #",
                "###### ###             #",
                "#   #    ##            #",
                "# ### ##  ##          ##",
                "#      ##            ###",
                "##  ##  #  #  ## ## ####",
                "###  #    ##  #   # # #",
                "#### #    ##  ###### #  #",
                "#       ##   #  ##  ## #",
                "# ##### ## ###         #",
                "#  #       ##    ## ## #",
                "## # ## ##  ## ###  #  #",
                "#     #  ##     #     ##",
                "########################"
            };
        public override Vector2 StartPos => new(3600, 3600);
        public override int KeyCount => 5;
        public override int SpotCount
        {
            get
            {
                var spots = 0;
                foreach (var line in Cells)
                    foreach (var cell in line)
                        if (cell == 'K')
                            spots++;
                return spots;
            }
        }

        public override Texture2D Texture => throw new NotImplementedException();
    }

    public class Death : Level
    {
        public override Texture2D Texture => GameCycle.textures[12];

        public override List<string> Cells => throw new NotImplementedException();

        public override Vector2 StartPos => throw new NotImplementedException();

        public override int KeyCount => throw new NotImplementedException();

        public override int SpotCount => throw new NotImplementedException();
    }
}