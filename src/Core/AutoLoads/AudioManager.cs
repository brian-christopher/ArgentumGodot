using System.Diagnostics;
using Godot;

namespace ArgentumOnline.Core.AutoLoads;

public partial class AudioManager : Node
{
    public static AudioManager Instance { get; private set; }
    
    public AudioManager()
    {
        Debug.Assert(Instance == null, "Only one instance of AudioManager is allowed.");
        Instance = this;
    }

    public void PlayAudio(int waveId)
    {
        if (!ResourceLoader.Exists($"res://assets/sounds/{waveId}.wav"))
        {
            GD.PrintErr($"Audio file {waveId} not found.");
            return;
        }
        
        AudioStream audioStream = ResourceLoader.Load<AudioStream>($"res://assets/sounds/{waveId}.wav");
        AudioStreamPlayer streamPlayer = new();
        AddChild(streamPlayer);
        
        streamPlayer.Stream = audioStream;
        streamPlayer.Bus = "sfx";
        streamPlayer.Finished += () => streamPlayer.QueueFree();
        streamPlayer.Play();
    }
}