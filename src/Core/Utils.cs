using ArgentumOnline.Core.Types;
using Godot;

namespace ArgentumOnline.Core;

public static class Utils
{
    public static Color GetNickColor(int nickColor, int privileges)
    {
        NickColor temp = (NickColor)nickColor;
        
        if (privileges == 0)
        {
            if ((temp & NickColor.IeAtacable) != NickColor.None) 
            {
                return GameAssets.PlayerColors[49];
            }

            if ((temp & NickColor.IeCriminal) != NickColor.None)
            {
                return GameAssets.PlayerColors[50];
            }
            return GameAssets.PlayerColors[49];
        }
        return GameAssets.PlayerColors[(int)privileges];
    }
}