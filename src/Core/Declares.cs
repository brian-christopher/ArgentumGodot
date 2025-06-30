using ArgentumOnline.Core.Types;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace ArgentumOnline.Core;

public static class Declares
{
    public const int InfiniteLoops = -1;
    public const int NoAnim = 2;

    public const float DefaultSpeed = 120.0f;
    public const int TileSize = 32;
    public const int MapSize = 100;

    public const int MaxSkills = 20;
    public const int NumSkills = 20;
    public const int NumAtributos = 5;
    public const int NumClases = 12;
    public const int NumRazas = 5;
    public const int NumCiudades = 6;
    public const int MaxSkillpoints = 100;

    public const int MaxNormalInventorySlots = 20;
    public const int MaxInventorySlots = 25;
    public const int MaxNpcInventorySlots = 50;
    public const int MaxInventoryObjs = 10000;
    public const int Flagoro = MaxInventorySlots + 1;
    public const int MaxBankInventorySlots = 40;
    public const int MaxUserHechizos = 35;

    public const int bCabeza = 1;
    public const int bPiernaIzquierda = 2;
    public const int bPiernaDerecha = 3;
    public const int bBrazoDerecho = 4;
    public const int bBrazoIzquierdo = 5;
    public const int bTorso = 6;

    public const int CabezaCasper = 500;
    public const int CuerpoFragataFantasmal = 87;

    public const int Paso1Wav = 23;
    public const int Paso2Wav = 24;
    public const int PasoNavegandoWav = 25;

    public static readonly ImmutableArray<int> VesselIds =
    [
        //Fragatas
        87, 190, 189,
        //Embarcaciones normales
        84, 85, 86,
        //Embarcaciones ciudas
        395, 552, 397, 560, 399, 556,
        //Embarcaciones reales
        550, 553, 558, 561, 554, 557,
        //Embarcaciones PK
        396, 398, 400,
        //Embarcaciones caos
        551, 559, 555
    ];

    public static readonly Dictionary<PlayerClass, string> ClassNames = new()
    {
        [PlayerClass.Mage] = "Mago",
        [PlayerClass.Cleric] = "Clerigo",
        [PlayerClass.Warrior] = "Guerrero",
        [PlayerClass.Assassin] = "Asesino",
        [PlayerClass.Thief] = "Ladron",
        [PlayerClass.Bard] = "Bardo",
        [PlayerClass.Druid] = "Druida",
        [PlayerClass.Bandit] = "Bandido",
        [PlayerClass.Paladin] = "Paladin",
        [PlayerClass.Hunter] = "Cazador",
        [PlayerClass.Worker] = "Trabajador",
        [PlayerClass.Pirate] = "Pirata"
    };

    public static readonly Dictionary<PlayerHome, string> HomeNames = new()
    {
        [PlayerHome.Arghal] = "Arghal",
        [PlayerHome.Arkhein] = "Arkhein",
        [PlayerHome.Lindos] = "Lindos",
        [PlayerHome.Ullathorpe] = "Ullathorpe",
        [PlayerHome.Banderbill] = "Banderbill",
        [PlayerHome.Nix] = "Nix"
    };

    public static readonly Dictionary<PlayerRace, string> RaceNames = new()
    {
        [PlayerRace.Human] = "Humano",
        [PlayerRace.Elf] = "Elfo",
        [PlayerRace.Drow] = "Drow",
        [PlayerRace.Dwarf] = "Enano",
        [PlayerRace.Gnome] = "Gnomo",
    };
}