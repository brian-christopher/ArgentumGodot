using ArgentumOnline.Core;
using ArgentumOnline.Core.AutoLoads;
using ArgentumOnline.Core.Types;
using ArgentumOnline.Net;
using Godot;

namespace ArgentumOnline.UI.Gameplay;

public partial class SpellListView : PanelContainer
{
    private const string EmptySpellLabel = "(None)";
    
    #region Exported Properties
    [Export] private ItemList SpellItemList { get; set; }
    #endregion

    public int SelectedSpellIndex
    {
        get
        {
            var selectedItem = SpellItemList.GetSelectedItems();
            return selectedItem.IsEmpty() ? Declares.InvalidSlot : selectedItem[0];
        }
    }

    public override void _Ready()
    {
        for (int i = 0; i < Declares.MaxUserHechizos; i++)
        {
            SpellItemList.AddItem(EmptySpellLabel);
        }
    }

    public void SetSpellLabel(int index, string label)
    {
        SpellItemList.SetItemText(index, label);
    }
    
    private void OnCastButtonPressed()
    {
        if (SelectedSpellIndex == Declares.InvalidSlot)
            return;

        if (SpellItemList.GetItemText(SelectedSpellIndex) == EmptySpellLabel)
            return;
        
        NetworkClient.Instance.SendCastSpell(SelectedSpellIndex + 1);
        NetworkClient.Instance.SendWork(Skill.Magia);
    }
    
    private void OnSpellDetailsButtonPressed()
    {
        if (SelectedSpellIndex == Declares.InvalidSlot)
            return;
        
        NetworkClient.Instance.SendSpellInfo(SelectedSpellIndex + 1);
    }

    private void OnScrollUpButtonPressed()
    {
        if (SelectedSpellIndex <= 0)
            return;

        SpellItemList.MoveItem(SelectedSpellIndex, SelectedSpellIndex - 1);
        SpellItemList.Select(SelectedSpellIndex);
        NetworkClient.Instance.SendMoveSpell(false, SelectedSpellIndex + 1);
    }

    private void OnScrollDownButtonPressed()
    {
        if (SelectedSpellIndex != Declares.InvalidSlot &&
            SelectedSpellIndex + 1 == Declares.MaxUserHechizos)
            return;

        SpellItemList.MoveItem(SelectedSpellIndex, SelectedSpellIndex + 1);
        SpellItemList.Select(SelectedSpellIndex);
        NetworkClient.Instance.SendMoveSpell(true, SelectedSpellIndex + 1);
    }
}