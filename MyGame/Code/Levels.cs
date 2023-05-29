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
        FirstLevel = 1,
        SecondLevel,
    }
    public static class Levels
    {
        public static class FirstLevel
        {
            public static readonly List<string> Cells = new()
            {
                "############",
                "#  #K  S# K#",
                "# #### ## ##",
                "# # K# #   #",
                "# # ## # # #",
                "#M         #",
                "# #### ### #",
                "# K#K###   #",
                "# ## # # # #",
                "# K#     #M#",
                "##   ###  K#",
                "############"
            };
            public static readonly Vector2 StartPos = new(1200, 1200);
            public static readonly int KeyCount = 3;
            public static readonly int SpotCount = 7;
        }

        public static class SecondLevel
        {
            public static readonly List<string> Cells = new()
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
            public static readonly Vector2 StartPos = new(1200, 1200);
            public static readonly int KeyCount = 9;
            public static readonly int SpotCount = 9;
        }
    }
}