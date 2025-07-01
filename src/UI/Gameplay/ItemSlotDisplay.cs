using System;
using Godot;

namespace ArgentumOnline.UI.Gameplay;

public partial class ItemSlotDisplay : Panel
{
    #region Exported Properties
    [Export] private Label QuantityLabel { get; set; }
    [Export] private Label EquippedLabel { get; set; }
    [Export] private TextureRect IconTextureRect { get; set; }
    #endregion

    public event Action Pressed; 
    public int Index { get; set; } = -1;

    public void SetQuantity(int quantity)
    {
        QuantityLabel.Text = quantity > 1 ?
            quantity.ToString() :
            string.Empty;
    }
    
    public void SetEquipped(bool equipped)
    {
        EquippedLabel.Visible = equipped;
    }

    public void SetIcon(Texture2D iconTexture)
    {
        IconTextureRect.Texture = iconTexture;
    }

    public void SetSelected(bool selected)
    {
        Modulate = selected ? Colors.Salmon : Colors.White;
    }

    public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton button)
        {
            if (button.Pressed && button.ButtonIndex is MouseButton.Right or MouseButton.Left)
            {
                Pressed?.Invoke();
            }
        }
    }
}