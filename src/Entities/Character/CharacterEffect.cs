using ArgentumOnline.Core;
using Godot;

namespace ArgentumOnline.Entities.Character;

/// <summary>
/// Represents a visual effect that can be played on a character.
/// Inherits from the AnimatedSprite2D class and allows visualization of various animations
/// associated with the character.
/// </summary>
public partial class CharacterEffect : AnimatedSprite2D
{
    public int EffectId { get; private set; }
    public int EffectLoops { get; private set; }

    public override void _Ready()
    {
        AnimationFinished += OnAnimationFinished;
    }

    /// <summary>
    /// Plays a visual effect animation on the character.
    /// </summary>
    /// <param name="id">The ID of the effect to play. Must be greater than 0.</param>
    /// <param name="loops">Number of times to repeat the effect. Use 0 for infinite loops.</param>
    public void PlayEffect(int id, int loops)
    {
        EffectId = id;
        EffectLoops = loops;

        if (EffectId > 0)
        {
            SpriteFrames = ResourceLoader
                .Load<SpriteFrames>($"res://resources/character/fxs/fx_{EffectId}.tres");

            float height = SpriteFrames
                .GetFrameTexture("default", 0)
                .GetHeight();

            float offset = SpriteFrames
                .GetMeta("offset_y")
                .AsSingle();
            
            Position = Position with
            {
                Y = height / 2.0f + offset
            };
            
            Show();
            Play("default");
        }
        else
        {
            StopEffect();
        }
    }
    
    /// <summary>
    /// Stops the current effect animation.
    /// </summary>
    public void StopEffect()
    {
        Stop();

        EffectId = 0;
        EffectLoops = 0;
        Hide();
    }

    private void OnAnimationFinished()
    {
        if (EffectLoops == Declares.InfiniteLoops)
        {
            Play("default");
        }
        else
        {
            EffectLoops -= 1;
            
            if (EffectLoops > 0)
                Play("default");
            else
                StopEffect();
        }
    }
}