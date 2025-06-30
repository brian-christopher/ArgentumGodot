using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Godot;

namespace ArgentumOnline.Core;

public sealed class TickIntervals
{
    private static class ActionTimers
    {
        public const int MacroSpells = 0;
        public const int MacroWork = 1;
        public const int Attack = 2;
        public const int AttackWithBow = 3;
        public const int Spell = 4;
        public const int AttackSpell = 5;
        public const int SpellAttack = 6;
        public const int Work = 7;
        public const int UseItemWithU = 8;
        public const int UseItemWithDoubleClick = 9;
        public const int RequestPositionUpdate = 10;
        public const int Tame = 11;
        public const int Steal = 12;
        public const int Size = 13;
    }
    
    private class TimerTick
    {
        public ulong Interval;
        public ulong ElapsedTime;
        
        public ulong Tick => ElapsedTime + Interval;
    }

    private readonly TimerTick[] _timers;

    public TickIntervals()
    {
        _timers = new TimerTick[(int)ActionTimers.Size];
        for (int i = 0; i < _timers.Length; i++)
            _timers[i] = new TimerTick();
        
        _timers[ActionTimers.MacroSpells].Interval = 2788;
        _timers[ActionTimers.MacroWork].Interval = 900;
        _timers[ActionTimers.Attack].Interval = 1500;
        _timers[ActionTimers.AttackWithBow].Interval = 1400;
        _timers[ActionTimers.Spell].Interval = 1400;
        _timers[ActionTimers.AttackSpell].Interval = 1000;
        _timers[ActionTimers.SpellAttack].Interval = 1000;
        _timers[ActionTimers.Work].Interval = 700;
        _timers[ActionTimers.UseItemWithU].Interval = 450;
        _timers[ActionTimers.UseItemWithDoubleClick].Interval = 500;
        _timers[ActionTimers.RequestPositionUpdate].Interval = 2000;
        _timers[ActionTimers.Tame].Interval = 700;
        _timers[ActionTimers.Steal].Interval = 700;
    }

    private bool RequestTimer(int index)
    {
        ulong tick = Time.GetTicksMsec();
        TimerTick timer = _timers[index];

        if (tick > timer.Tick)
        {
            timer.ElapsedTime = tick;
            return true;
        }
        
        return false;
    }
    
    public bool RequestPositionUpdate()
    {
        return RequestTimer(ActionTimers.RequestPositionUpdate);
    }
    
    public bool RequestMacroWork()
    {
        return RequestTimer(ActionTimers.MacroWork);
    }
    
    public bool RequestWork()
    {
        return RequestTimer(ActionTimers.Work);
    }
    
    public bool RequestMacroSpells()
    {
        return RequestTimer(ActionTimers.MacroSpells);
    }

    public bool RequestAttack()
    {
        ulong tick = Time.GetTicksMsec();
        var attackTimer = _timers[ActionTimers.Attack];
        var attackWithBow = _timers[ActionTimers.AttackWithBow];
        var spellAttack = _timers[ActionTimers.SpellAttack];

        if (tick <= attackWithBow.Tick) return false;
        if (tick <= spellAttack.Tick) return false;
        if (tick <= attackTimer.Tick) return false;
        
        attackTimer.ElapsedTime = tick;
        _timers[ActionTimers.AttackSpell].ElapsedTime = tick;
        return true;
    }
    
    public bool RequestCastSpell()
    {
        ulong tick = Time.GetTicksMsec();
        var spellTimer = _timers[ActionTimers.Spell];
        var attackWithBow = _timers[ActionTimers.AttackWithBow];
        var attackSpell = _timers[ActionTimers.AttackSpell];

        if (tick <= attackWithBow.Tick) return false;
        if (tick <= attackSpell.Tick) return false;
        if (tick <= spellTimer.Tick) return false;
        
        spellTimer.ElapsedTime = tick;
        _timers[ActionTimers.SpellAttack].ElapsedTime = tick;
        return true;
    }

    public bool RequestAttackWithBow()
    {
        return RequestTimer(ActionTimers.AttackWithBow);
    }
    
    public bool RequestUseItemWithDoubleClick()
    {
        return RequestTimer(ActionTimers.UseItemWithDoubleClick);
    }
    
    public bool RequestUseItemWithU()
    {
        return RequestTimer(ActionTimers.UseItemWithU);
    }
    
    public bool RequestTame()
    {
        return RequestTimer(ActionTimers.Tame);
    }
    
    public bool RequestSteal()
    {
        return RequestTimer(ActionTimers.Steal);
    }
}