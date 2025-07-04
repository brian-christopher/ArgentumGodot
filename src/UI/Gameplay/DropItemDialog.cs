using ArgentumOnline.Core;
using ArgentumOnline.Core.AutoLoads;
using ArgentumOnline.Net;
using Godot;

namespace ArgentumOnline.UI.Gameplay;

public partial class DropItemDialog : Panel
{
    #region Exported Properties
    [Export] private SpinBox QuantitySpinBox { get; set; }
    [Export] private Label TitleLabel { get; set; }
    #endregion

    public int Quantity => (int)QuantitySpinBox.Value;
    public int SlotIndex { get; set; } = -1;
    
    public override void _Ready()
    {
        QuantitySpinBox.MinValue = 0;
        QuantitySpinBox.MaxValue = Declares.MaxInventoryObjs;
    }

    private void OnCloseButtonPressed()
    {
        QueueFree();
    }

    private void OnConfirmDropButtonPressed()
    {
        Drop(Quantity);
    }
    
    private void OnDropAllButtonPressed()
    {
        Drop(Declares.MaxInventoryObjs);
    }

    private void Drop(int quantity)
    {
        if (quantity > 0)
        {
            NetworkClient.Instance.SendDrop(SlotIndex, quantity);
        }
        QueueFree();
    }
}