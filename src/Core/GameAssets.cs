using System;
using System.IO;
using ArgentumOnline.Core.Types;
using ArgentumOnline.Data;
using Godot;
using FileAccess = Godot.FileAccess;

namespace ArgentumOnline.Core;

public class GrhData
{
    public int FrameCount;
    public int[] Frames;
    public float FrameRate;
    
    public Rect2 Region;
    public int FileId;
}

internal static class GameAssets
{
    public static GrhData[] GrhDataList { get; private set; }
    public static FontData[] FontDataList { get; private set; }

    public static void Load()
    {
        LoadGrhData();
        LoadFonts();
    }

    public static Texture2D GetTexture(int id)
    {
        return ResourceLoader.Load<Texture2D>($"res://assets/textures/{id}.png");
    }
    
    private static void LoadGrhData()
    {
        using MemoryStream stream = new MemoryStream(FileAccess.GetFileAsBytes("res://assets/init/graficos.ind"));
        using BinaryReader reader = new BinaryReader(stream);
        
        //Get File Version
        reader.ReadInt32();
        
        //Get Number Of Grh
        int count = reader.ReadInt32();
        
        GrhDataList = new GrhData[count + 1];
        Array.Fill(GrhDataList, new GrhData());

        while (stream.Position < stream.Length)
        {
            int grhId = reader.ReadInt32();
            int frameCount = reader.ReadInt16();
            
            GrhData grhData = new GrhData();
            
            grhData.FrameCount = frameCount;
            grhData.Frames = new int[frameCount + 1];

            if (frameCount > 1)
            {
                for (int i = 1; i <= frameCount; i++)
                    grhData.Frames[i] = reader.ReadInt32();
                
                grhData.FrameRate =  reader.ReadSingle();
            }
            else
            {
                grhData.FileId = reader.ReadInt32();
                grhData.Frames[1] = grhId;
                
                grhData.Region = new Rect2(
                    x: reader.ReadInt16(),
                    y: reader.ReadInt16(),
                    width: reader.ReadInt16(),
                    height: reader.ReadInt16());
            }
            
            GrhDataList[grhId] = grhData;
        }
    }

    private static void LoadFonts()
    {
        FontDataList = new FontData[21];
        
        FontDataList[(int)FontTypeNames.FontType_Talk] = new FontData(Colors.White, false, false);
        FontDataList[(int)FontTypeNames.FontType_Fight] = new FontData(Colors.Red, true, false);
        FontDataList[(int)FontTypeNames.FontType_Warning] = new FontData(Color.Color8(32, 51, 223), true, true);
        FontDataList[(int)FontTypeNames.FontType_Info] = new FontData(Color.Color8(65, 190, 156), false, false);
        FontDataList[(int)FontTypeNames.FontType_InfoBold] = new FontData(Color.Color8(65, 190, 156), true, false);
        FontDataList[(int)FontTypeNames.FontType_Ejecucion] = new FontData(Color.Color8(130, 130, 130), true, false);
        FontDataList[(int)FontTypeNames.FontType_Party] = new FontData(Color.Color8(255, 180, 250), false, false);
        FontDataList[(int)FontTypeNames.FontType_Veneno] = new FontData(Color.Color8(0, 255, 0), false, false);
        FontDataList[(int)FontTypeNames.FontType_Guild] = new FontData(Colors.White, true, false);
        FontDataList[(int)FontTypeNames.FontType_Server] = new FontData(Color.Color8(0, 185, 0), false, false);
        FontDataList[(int)FontTypeNames.FontType_GuildMsg] = new FontData(Color.Color8(228, 199, 27), false, false);
        FontDataList[(int)FontTypeNames.FontType_Consejo] = new FontData(Color.Color8(130, 130, 255), true, false);
        FontDataList[(int)FontTypeNames.FontType_ConsejoCaos] = new FontData(Color.Color8(255, 60, 0), true, false);
        FontDataList[(int)FontTypeNames.FontType_ConsejoVesA] = new FontData(Color.Color8(0, 200, 255), true, false);
        FontDataList[(int)FontTypeNames.FontType_ConsejoCaosVesA] = new FontData(Color.Color8(255, 50, 0), true, false);
        FontDataList[(int)FontTypeNames.FontType_Centinela] = new FontData(Color.Color8(0, 255, 0), true, false);
        FontDataList[(int)FontTypeNames.FontType_GMMsg] = new FontData(Colors.White, false, true);
        FontDataList[(int)FontTypeNames.FontType_GM] = new FontData(Color.Color8(30, 255, 30), true, false);
        FontDataList[(int)FontTypeNames.FontType_Citizen] = new FontData(Color.Color8(0, 0, 200), true, false);
        FontDataList[(int)FontTypeNames.FontType_Conse] = new FontData(Color.Color8(30, 150, 30), true, false);
        FontDataList[(int)FontTypeNames.FontType_Dios] = new FontData(Color.Color8(250, 250, 150), true, false);
    }
}