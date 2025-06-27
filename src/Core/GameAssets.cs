using System;
using System.IO;
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

    public static void Load()
    {
        LoadGrhData();
    }

    public static Texture2D GetTexture(int id)
    {
        return ResourceLoader.Load<Texture2D>($"res//assets/textures/{id}.png");
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
}