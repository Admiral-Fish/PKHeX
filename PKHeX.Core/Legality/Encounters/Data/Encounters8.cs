﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using static PKHeX.Core.EncounterUtil;
using static PKHeX.Core.Shiny;
using static PKHeX.Core.GameVersion;

using static PKHeX.Core.Encounters8Nest;
using System.Linq;

namespace PKHeX.Core
{
    /// <summary>
    /// Generation 8 Encounters
    /// </summary>
    internal static class Encounters8
    {
        private static readonly EncounterArea8[] SlotsSW_Symbol = EncounterArea8.GetAreas(Get("sw_symbol", "sw"), SW, true);
        private static readonly EncounterArea8[] SlotsSH_Symbol = EncounterArea8.GetAreas(Get("sh_symbol", "sh"), SH, true);
        private static readonly EncounterArea8[] SlotsSW_Hidden = EncounterArea8.GetAreas(Get("sw_hidden", "sw"), SW);
        private static readonly EncounterArea8[] SlotsSH_Hidden = EncounterArea8.GetAreas(Get("sh_hidden", "sh"), SH);
        private static byte[][] Get(string resource, string ident) => BinLinker.Unpack(Util.GetBinaryResource($"encounter_{resource}.pkl"), ident);

        internal static readonly EncounterArea8[] SlotsSW = ArrayUtil.ConcatAll(SlotsSW_Symbol, SlotsSW_Hidden);
        internal static readonly EncounterArea8[] SlotsSH = ArrayUtil.ConcatAll(SlotsSH_Symbol, SlotsSH_Hidden);

        static Encounters8()
        {
            foreach (var t in TradeGift_R1)
                t.TrainerNames = TradeOT_R1;

            MarkEncounterTradeStrings(TradeGift_SWSH, TradeSWSH);

            // Include Nest Tables for both versions -- online play can share them across versions! In the IsMatch method we check if it's a valid share.

            CopyBerryTreeFromBridgeFieldToStony(SlotsSW_Hidden, 26);
            CopyBerryTreeFromBridgeFieldToStony(SlotsSH_Hidden, 25);
        }

        private static void CopyBerryTreeFromBridgeFieldToStony(IReadOnlyList<EncounterArea8> arr, int start)
        {
            // The Berry Trees in Bridge Field are right against the map boundary, and can be accessed on the adjacent Map ID (Stony Wilderness)
            // Copy the two Berry Tree encounters from Bridge to Stony, as these aren't overworld (wandering) crossover encounters.
            var bridge = arr[13];
            Debug.Assert(bridge.Location == 142);
            var stony = arr[31];
            Debug.Assert(stony.Location == 144);

            var ss = stony.Slots;
            var ssl = ss.Length;
            Array.Resize(ref ss, ssl + 2);
            Array.Copy(bridge.Slots, start, ss, ssl, 2);
            Debug.Assert(((EncounterSlot8)ss[ssl]).Weather == AreaWeather8.Shaking_Trees);
            Debug.Assert(((EncounterSlot8)ss[ssl+1]).Weather == AreaWeather8.Shaking_Trees);
            stony.Slots = ss;
        }

        private static readonly EncounterStatic8[] Encounter_SWSH =
        {
            // gifts
            new(SWSH) { Gift = true, Species = 810, Shiny = Never, Level = 05, Location = 006, }, // Grookey
            new(SWSH) { Gift = true, Species = 813, Shiny = Never, Level = 05, Location = 006, }, // Scorbunny
            new(SWSH) { Gift = true, Species = 816, Shiny = Never, Level = 05, Location = 006, }, // Sobble

            new(SWSH) { Gift = true, Species = 772, Shiny = Never, Level = 50, Location = 158, FlawlessIVCount = 3, }, // Type: Null
            new(SWSH) { Gift = true, Species = 848, Shiny = Never, Level = 01, Location = 040, IVs = new[]{-1,31,-1,-1,31,-1}, Ball = 11 }, // Toxel, Attack flawless

            new(SWSH) { Gift = true, Species = 880, FlawlessIVCount = 3, Level = 10, Location = 068, }, // Dracozolt @ Route 6
            new(SWSH) { Gift = true, Species = 881, FlawlessIVCount = 3, Level = 10, Location = 068, }, // Arctozolt @ Route 6
            new(SWSH) { Gift = true, Species = 882, FlawlessIVCount = 3, Level = 10, Location = 068, }, // Dracovish @ Route 6
            new(SWSH) { Gift = true, Species = 883, FlawlessIVCount = 3, Level = 10, Location = 068, }, // Arctovish @ Route 6

            new(SWSH) { Gift = true, Species = 004, Shiny = Never, Level = 05, Location = 006, FlawlessIVCount = 3, CanGigantamax = true, Ability = 1 }, // Charmander
            new(SWSH) { Gift = true, Species = 025, Shiny = Never, Level = 10, Location = 156, FlawlessIVCount = 6, CanGigantamax = true }, // Pikachu
            new(SWSH) { Gift = true, Species = 133, Shiny = Never, Level = 10, Location = 156, FlawlessIVCount = 6, CanGigantamax = true }, // Eevee

            // DLC gifts
            new(SWSH) { Gift = true, Species = 001, Level = 05, Location = 196, Shiny = Never, Ability = 1, FlawlessIVCount = 3, CanGigantamax = true }, // Bulbasaur
            new(SWSH) { Gift = true, Species = 007, Level = 05, Location = 196, Shiny = Never, Ability = 1, FlawlessIVCount = 3, CanGigantamax = true }, // Squirtle
            new(SWSH) { Gift = true, Species = 137, Level = 25, Location = 196, Shiny = Never, Ability = 4, FlawlessIVCount = 3 }, // Porygon
            new(SWSH) { Gift = true, Species = 891, Level = 10, Location = 196, Shiny = Never, FlawlessIVCount = 3 }, // Kubfu

            new(SWSH) { Gift = true, Species = 079, Level = 10, Location = 164, Shiny = Never, Ability = 4, FlawlessIVCount = 3 }, // Slowpoke
            new(SWSH) { Gift = true, Species = 722, Level = 05, Location = 164, Shiny = Never, Ability = 4, FlawlessIVCount = 3 }, // Rowlet
            new(SWSH) { Gift = true, Species = 725, Level = 05, Location = 164, Shiny = Never, Ability = 4, FlawlessIVCount = 3 }, // Litten
            new(SWSH) { Gift = true, Species = 728, Level = 05, Location = 164, Shiny = Never, Ability = 4, FlawlessIVCount = 3 }, // Popplio
            new(SWSH) { Gift = true, Species = 026, Level = 30, Location = 164, Shiny = Never, Ability = 1, FlawlessIVCount = 3, Form = 01 }, // Raichu-1
            new(SWSH) { Gift = true, Species = 027, Level = 05, Location = 164, Shiny = Never, Ability = 4, FlawlessIVCount = 3, Form = 01 }, // Sandshrew-1
            new(SWSH) { Gift = true, Species = 037, Level = 05, Location = 164, Shiny = Never, Ability = 4, FlawlessIVCount = 3, Form = 01 }, // Vulpix-1
            new(SWSH) { Gift = true, Species = 052, Level = 05, Location = 164, Shiny = Never, Ability = 4, FlawlessIVCount = 3, Form = 01 }, // Meowth-1
            new(SWSH) { Gift = true, Species = 103, Level = 30, Location = 164, Shiny = Never, Ability = 4, FlawlessIVCount = 3, Form = 01 }, // Exeggutor-1
            new(SWSH) { Gift = true, Species = 105, Level = 30, Location = 164, Shiny = Never, Ability = 4, FlawlessIVCount = 3, Form = 01 }, // Marowak-1
            new(SWSH) { Gift = true, Species = 050, Level = 20, Location = 164, Shiny = Never, Ability = 4, Gender = 0, Nature = Nature.Jolly, FlawlessIVCount = 6, Form = 01 }, // Diglett-1

            new(SWSH) { Gift = true, Species = 789, Level = 05, Location = 206, FlawlessIVCount = 3, Shiny = Never, Ability = 1 }, // Cosmog
            new(SWSH) { Gift = true, Species = 803, Level = 20, Location = 244, FlawlessIVCount = 3, Shiny = Never, Ability = 1, Ball = 26 }, // Poipole

            // Technically a gift, but copies ball from Calyrex.
            new(SWSH) { Species = 896, Level = 75, Location = 220, ScriptedNoMarks = true, FlawlessIVCount = 3, Shiny = Never, Ability = 1, Relearn = new[] {556,0,0,0} }, // Glastrier
            new(SWSH) { Species = 897, Level = 75, Location = 220, ScriptedNoMarks = true, FlawlessIVCount = 3, Shiny = Never, Ability = 1, Relearn = new[] {247,0,0,0} }, // Spectrier

            #region Static Part 1
            // encounters
            new(SW  ) { Species = 888, Level = 70, Location = 66, ScriptedNoMarks = true, Moves = new[] {533,014,442,242}, Shiny = Never, Ability = 1, FlawlessIVCount = 3 }, // Zacian
            new(  SH) { Species = 889, Level = 70, Location = 66, ScriptedNoMarks = true, Moves = new[] {163,242,442,334}, Shiny = Never, Ability = 1, FlawlessIVCount = 3 }, // Zamazenta
            new(SWSH) { Species = 890, Level = 60, Location = 66, ScriptedNoMarks = true, Moves = new[] {440,406,053,744}, Shiny = Never, Ability = 1, FlawlessIVCount = 3 }, // Eternatus-1 (reverts to form 0)

            // Motostoke Stadium Static Encounters
            new(SWSH) { Species = 037, Level = 24, Location = 24, }, // Vulpix at Motostoke Stadium
            new(  SH) { Species = 058, Level = 24, Location = 24, }, // Growlithe at Motostoke Stadium
            new(SWSH) { Species = 607, Level = 25, Location = 24, }, // Litwick at Motostoke Stadium
            new(SWSH) { Species = 850, Level = 25, Location = 24, FlawlessIVCount = 3 }, // Sizzlipede at Motostoke Stadium

            new(SWSH) { Species = 618, Level = 25, Location = 054, Moves = new[] {389,319,279,341}, Form = 01, Ability = 1 }, // Stunfisk in Galar Mine No. 2
            new(SWSH) { Species = 618, Level = 48, Location = 008, Moves = new[] {779,330,340,334}, Form = 01 }, // Stunfisk in the Slumbering Weald
            new(SWSH) { Species = 527, Level = 16, Location = 030, Moves = new[] {000,000,000,000} }, // Woobat in Galar Mine
            new(SWSH) { Species = 838, Level = 18, Location = 030, Moves = new[] {488,397,229,033} }, // Carkol in Galar Mine
            new(SWSH) { Species = 834, Level = 24, Location = 054, Moves = new[] {317,029,055,044} }, // Drednaw in Galar Mine No. 2
            new(SWSH) { Species = 423, Level = 50, Location = 054, Moves = new[] {240,414,330,246}, FlawlessIVCount = 3, Form = 01 }, // Gastrodon in Galar Mine No. 2
            new(SWSH) { Species = 859, Level = 31, Location = 076, Moves = new[] {259,389,207,372} }, // Impidimp in Glimwood Tangle
            new(SWSH) { Species = 860, Level = 38, Location = 076, Moves = new[] {793,399,259,389} }, // Morgrem in Glimwood Tangle
            new(SWSH) { Species = 835, Level = 08, Location = 018, Moves = new[] {039,033,609,000} }, // Yamper on Route 2
            new(SWSH) { Species = 834, Level = 50, Location = 018, Moves = new[] {710,746,068,317}, FlawlessIVCount = 3 }, // Drednaw on Route 2
            new(SWSH) { Species = 833, Level = 08, Location = 018, Moves = new[] {044,055,000,000} }, // Chewtle on Route 2
            new(SWSH) { Species = 131, Level = 55, Location = 018, Moves = new[] {056,240,058,034}, FlawlessIVCount = 3 }, // Lapras on Route 2
            new(SWSH) { Species = 862, Level = 50, Location = 018, Moves = new[] {269,068,792,184} }, // Obstagoon on Route 2
            new(SWSH) { Species = 822, Level = 18, Location = 028, Moves = new[] {681,468,031,365}, Shiny = Never }, // Corvisquire on Route 3
            new(SWSH) { Species = 050, Level = 17, Location = 032, Moves = new[] {523,189,310,045} }, // Diglett on Route 4
            new(SWSH) { Species = 830, Level = 22, Location = 040, Moves = new[] {178,496,075,047} }, // Eldegoss on Route 5
            new(SWSH) { Species = 558, Level = 40, Location = 086, Moves = new[] {404,350,446,157} }, // Crustle on Route 8
            new(SWSH) { Species = 870, Level = 40, Location = 086, Moves = new[] {748,660,179,203} }, // Falinks on Route 8
            new(SWSH) { Species = 362, Level = 55, Location = 090, Moves = new[] {573,329,104,182}, FlawlessIVCount = 3 }, // Glalie on Route 9
            new(SWSH) { Species = 853, Level = 50, Location = 092, Moves = new[] {753,576,276,179} }, // Grapploct on Route 9 (in Circhester Bay)
            new(SWSH) { Species = 822, Level = 35, Location =  -1, Moves = new[] {065,184,269,365} }, // Corvisquire
            new(SWSH) { Species = 614, Level = 55, Location = 106, Moves = new[] {276,059,156,329} }, // Beartic on Route 10
            new(SWSH) { Species = 460, Level = 55, Location = 106, Moves = new[] {008,059,452,275} }, // Abomasnow on Route 10
            new(SWSH) { Species = 342, Level = 50, Location = 034, Moves = new[] {242,014,534,400}, FlawlessIVCount = 3 }, // Crawdaunt in the town of Turffield
            #endregion

            #region Static Part 2
            // Some of these may be crossover cases. For now, just log the locations they can show up in and re-categorize later.
            new(SWSH) { Species = 095, Level = 26, Location = 122, }, // Onix in the Rolling Fields (in a Wild Area)
            new(SWSH) { Species = 416, Level = 26, Location = 122, }, // Vespiquen in the Rolling Fields (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 675, Level = 32, Locations = new[] {122, 124}}, // Pangoro in the Rolling Fields, in the Dappled Grove (in a Wild Area)
            new(SWSH) { Species = 291, Level = 15, Location = 122, }, // Ninjask in the Rolling Fields (in a Wild Area)
            new(SWSH) { Species = 315, Level = 15, Location = 122, }, // Roselia in the Rolling Fields (in a Wild Area)
            new(SWSH) { Species = 045, Level = 36, Location = 124, }, // Vileplume in the Dappled Grove (in a Wild Area)
            new(SWSH) { Species = 760, Level = 34, Location = 124, }, // Bewear in the Dappled Grove (in a Wild Area)
            new(SWSH) { Species = 275, Level = 34, Location = 124, }, // Shiftry in the Dappled Grove (in a Wild Area)
            new(SWSH) { Species = 272, Level = 34, Location = 124, }, // Ludicolo in the Dappled Grove (in a Wild Area)
            new(SWSH) { Species = 426, Level = 34, Location = 126, }, // Drifblim at Watchtower Ruins (in a Wild Area)
            new EncounterStatic8S(SWSH)  { Species = 623, Level = 40, Locations = new[]{126, 130}, }, // Golurk at Watchtower Ruins and West Lake Axewell (in a Wild Area) 
            new(SWSH) { Species = 195, Level = 15, Location = 130, }, // Quagsire at West Lake Axewell (in a Wild Area)
            new(SWSH) { Species = 099, Level = 28, Location = 130, }, // Kingler at West Lake Axewell (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 660, Level = 15, Locations = new [] {122, 130}, }, // Diggersby in the Rolling Fields, West Lake Axewell (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 178, Level = 26, Locations = new[] {128, 138}}, // Xatu at East Lake Axewell, North Lake Miloch (in a Wild Area)
            new(SWSH) { Species = 569, Level = 36, Location = 128, }, // Garbodor at East Lake Axewell (in a Wild Area)
            new(SWSH) { Species = 510, Level = 28, Location = 138, }, // Liepard at North Lake Miloch (in a Wild Area)
            new(SWSH) { Species = 750, Level = 31, Location = 122, }, // Mudsdale in the Rolling Fields (in a Wild Area)
            new(SWSH) { Species = 067, Level = 26, Location = 134, }, // Machoke at South Lake Miloch (in a Wild Area)
            new(SWSH) { Species = 435, Level = 34, Location = 134, }, // Skuntank at South Lake Miloch (in a Wild Area)
            new(SWSH) { Species = 099, Level = 31, Location = 134, }, // Kingler at South Lake Miloch (in a Wild Area)
            new(SWSH) { Species = 342, Level = 31, Location = 134, }, // Crawdaunt at South Lake Miloch (in a Wild Area)
            new(SWSH) { Species = 208, Level = 50, Location = 136, }, // Steelix near the Giant’s Seat (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 823, Level = 50, Locations = new[] {138, 150} }, // Corviknight at North Lake Miloch & on the Hammerlocke Hills (in a Wild Area)
            new(SWSH) { Species = 448, Level = 36, Location = 138, }, // Lucario at North Lake Miloch (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 112, Level = 46, Locations = new[] {136, 142}, }, // Rhydon near the Giant’s Seat & in Bridge Field (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 625, Level = 52, Locations = new[] {136, 140} }, // Bisharp near the Giant’s Seat, Motostoke Riverbank (in a Wild Area)
            new(SWSH) { Species = 738, Level = 46, Location = 136, }, // Vikavolt near the Giant’s Seat (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 091, Level = 46, Locations = new[] {128, 130}, }, // Cloyster at East/West Lake Axewell (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 131, Level = 56, Locations = new[] {128, 130, 134, 138, 154 }, }, // Lapras at North/East/South/West Lake Miloch/Axwell, the Lake of Outrage (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 119, Level = 46, Locations = new[] {128, 130, 134, 138, 142, 154 }, }, // Seaking in Bridge Field, at North/East/South/West Lake Miloch/Axwell, at the Lake of Outrage (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 130, Level = 56, Locations = new[] {128, 142, 146}, }, // Gyarados in East Lake Axewell, in Bridge Field, Dusty Bowl (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 279, Level = 46, Locations = new[] {128, 142}, }, // Pelipper in East Lake Axewell, Bridge Field (in a Wild Area)
            new(SWSH) { Species = 853, Level = 56, Location = 130, }, // Grapploct at West Lake Axewell (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 593, Level = 46, Locations = new[] {128, 130, 134, 138, 142, 154 }, }, // Jellicent at North/East/South/West Lake Miloch/Axwell, the Lake of Outrage, Bridge Field (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 171, Level = 46, Locations = new[] {128,      134,      154 }, }, // Lanturn at East Lake Axewell & South Lake Miloch (in a Wild Area), the Lake of Outrage
            new EncounterStatic8S(SWSH) { Species = 340, Level = 46, Locations = new[] {128, 130, 134, 138, 154 }, }, // Whiscash at North/East/South/West Lake Miloch/Axwell (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 426, Level = 46, Locations = new[] {128, 130, 134, 138, 154 }, }, // Drifblim at North/East/South/West Lake Miloch/Axwell (in a Wild Area)
            new(SWSH) { Species = 224, Level = 46, Location = 134, }, // Octillery at South Lake Miloch (in a Wild Area)
            new(SWSH) { Species = 612, Level = 60, Location = 132, Ability = 1, }, // Haxorus on Axew’s Eye (in a Wild Area)
            new(SWSH) { Species = 143, Level = 36, Location = 140, }, // Snorlax at the Motostoke Riverbank (in a Wild Area)
            new(SWSH) { Species = 452, Level = 40, Location = 140, }, // Drapion at the Motostoke Riverbank (in a Wild Area)
            new(SWSH) { Species = 561, Level = 36, Location = 140, }, // Sigilyph at the Motostoke Riverbank (in a Wild Area)
            new(SWSH) { Species = 534, Level = 55, Location = 140, Ability = 1, }, // Conkeldurr at the Motostoke Riverbank (in a Wild Area)
            new(SWSH) { Species = 320, Level = 56, Location = 140, }, // Wailmer at the Motostoke Riverbank (in a Wild Area)
            new(SWSH) { Species = 561, Level = 40, Location = 140, }, // Sigilyph at the Motostoke Riverbank (in a Wild Area)
            new(SWSH) { Species = 569, Level = 40, Location = 142, }, // Garbodor in Bridge Field (in a Wild Area)
            new(SWSH) { Species = 743, Level = 40, Location = 142, }, // Ribombee in Bridge Field (in a Wild Area)
            new(SWSH) { Species = 475, Level = 60, Location = 142, }, // Gallade in Bridge Field (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 264, Level = 40, Locations = new[] {140, 142}, Form = 01, }, // Linoone at the Motostoke Riverbank & in Bridge Field (in a Wild Area)
            new(SWSH) { Species = 606, Level = 42, Location = 142, }, // Beheeyem in Bridge Field (in a Wild Area)
            new(SWSH) { Species = 715, Level = 50, Location = 142, }, // Noivern in Bridge Field (in a Wild Area)
            new(SWSH) { Species = 537, Level = 46, Location = 142, }, // Seismitoad in Bridge Field (in a Wild Area)
            new(SWSH) { Species = 768, Level = 50, Location = 142, }, // Golisopod in Bridge Field (in a Wild Area)
            new(SWSH) { Species = 760, Level = 42, Location = 142, }, // Bewear in Bridge Field (in a Wild Area)
            new(SWSH) { Species = 820, Level = 42, Location = 142, }, // Greedent in Bridge Field (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 598, Level = 40, Locations = new[] {142, 144}, }, // Ferrothorn in Bridge Field (in a Wild Area)
            new(SWSH) { Species = 344, Level = 42, Location = 144, }, // Claydol in the Stony Wilderness (in a Wild Area)
            new(SWSH) { Species = 477, Level = 60, Location = 144, }, // Dusknoir in the Stony Wilderness (in a Wild Area)
            new(SWSH) { Species = 623, Level = 43, Location = 144, }, // Golurk in the Stony Wilderness (in a Wild Area)
            new(SWSH) { Species = 561, Level = 40, Location = 144, }, // Sigilyph in the Stony Wilderness (in a Wild Area)
            new(SWSH) { Species = 558, Level = 34, Location = 144, }, // Crustle in the Stony Wilderness (in a Wild Area)
            new(SWSH) { Species = 112, Level = 41, Location = 144, }, // Rhydon in the Stony Wilderness (in a Wild Area)
            new(SWSH) { Species = 763, Level = 36, Location = 144, }, // Tsareena in the Stony Wilderness (in a Wild Area)
            new(SWSH) { Species = 750, Level = 41, Location = 146, }, // Mudsdale in Dusty Bowl (in a Wild Area)
            new(SWSH) { Species = 185, Level = 41, Location = 146, }, // Sudowoodo in Dusty Bowl (in a Wild Area)
            new(SWSH) { Species = 437, Level = 41, Location = 146, }, // Bronzong in Dusty Bowl (in a Wild Area)
            new(SWSH) { Species = 248, Level = 60, Location = 146, }, // Tyranitar in Dusty Bowl (in a Wild Area)
            new(SWSH) { Species = 784, Level = 60, Location = 146, Ability = 1, }, // Kommo-o in Dusty Bowl (in a Wild Area)
            new(SWSH) { Species = 213, Level = 34, Location = 146, }, // Shuckle in Dusty Bowl (in a Wild Area)
            new(SWSH) { Species = 330, Level = 51, Location = 146, }, // Flygon in Dusty Bowl (in a Wild Area)
            new(SWSH) { Species = 526, Level = 51, Location = 146, }, // Gigalith in Dusty Bowl (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 423, Level = 56, Locations = new[] {146, 148}, Form = 01, }, // Gastrodon in Dusty Bowl (in a Wild Area) and the Giant’s Mirror
            new(SWSH) { Species = 208, Level = 50, Location = 148, }, // Steelix around the Giant’s Mirror (in a Wild Area)
            new(SWSH) { Species = 068, Level = 60, Location = 148, Ability = 1, }, // Machamp around the Giant’s Mirror (in a Wild Area)
            new(SWSH) { Species = 182, Level = 41, Location = 148, }, // Bellossom around the Giant’s Mirror (in a Wild Area)
            new(SWSH) { Species = 521, Level = 41, Location = 148, }, // Unfezant around the Giant’s Mirror (in a Wild Area)
            new(SWSH) { Species = 701, Level = 36, Location = 150, }, // Hawlucha on the Hammerlocke Hills (in a Wild Area)
            new(SWSH) { Species = 094, Level = 60, Location = 152, }, // Gengar near the Giant’s Cap (in a Wild Area)
            new(SWSH) { Species = 823, Level = 39, Location = 152, }, // Corviknight near the Giant’s Cap (in a Wild Area)
            new(SWSH) { Species = 573, Level = 46, Location = 152, }, // Cinccino near the Giant’s Cap (in a Wild Area)
            new(SWSH) { Species = 826, Level = 41, Location = 152, }, // Orbeetle near the Giant’s Cap (in a Wild Area)
            new(SWSH) { Species = 834, Level = 36, Location = 152, }, // Drednaw near the Giant’s Cap (in a Wild Area)
            new(SWSH) { Species = 680, Level = 56, Location = 152, }, // Doublade near the Giant’s Cap (in a Wild Area)
            new(SWSH) { Species = 711, Level = 41, Location = 150, }, // Gourgeist on the Hammerlocke Hills (in a Wild Area)
            new(SWSH) { Species = 600, Level = 46, Location = 150, }, // Klang on the Hammerlocke Hills (in a Wild Area)
            new(SWSH) { Species = 045, Level = 41, Location = 148, }, // Vileplume around the Giant’s Mirror (in a Wild Area)
            new(SWSH) { Species = 823, Level = 38, Location = 150, }, // Corviknight on the Hammerlocke Hills (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 130, Level = 60, Locations = new[] {128, 130, 134, 138, 154 }, }, // Gyarados at North/East/South/West Lake Miloch/Axwell, the Lake of Outrage (in a Wild Area)
            new(SWSH) { Species = 853, Level = 56, Location = 154, }, // Grapploct at the Lake of Outrage (in a Wild Area)
            new(SWSH) { Species = 282, Level = 60, Location = 154, }, // Gardevoir at the Lake of Outrage (in a Wild Area)
            new(SWSH) { Species = 470, Level = 56, Location = 154, }, // Leafeon at the Lake of Outrage (in a Wild Area)
            new(SWSH) { Species = 510, Level = 31, Location =  -1, }, // Liepard
            new(SWSH) { Species = 832, Level = 65, Location = 122, }, // Dubwool in the Rolling Fields (in a Wild Area)
            new(SWSH) { Species = 826, Level = 65, Location = 124, }, // Orbeetle in the Dappled Grove (in a Wild Area)
            new(SWSH) { Species = 823, Level = 65, Location = 126, }, // Corviknight at Watchtower Ruins (in a Wild Area)
            new(SWSH) { Species = 110, Level = 65, Location = 128, Form = 01, }, // Weezing at East Lake Axewell (in a Wild Area)
            new(SWSH) { Species = 834, Level = 65, Location = 130, }, // Drednaw at West Lake Axewell (in a Wild Area)
            new(SWSH) { Species = 845, Level = 65, Location = 132, }, // Cramorant on Axew’s Eye (in a Wild Area)
            new(SWSH) { Species = 828, Level = 65, Location = 134, }, // Thievul at South Lake Miloch (in a Wild Area)
            new(SWSH) { Species = 884, Level = 65, Location = 136, }, // Duraludon near the Giant’s Seat (in a Wild Area)
            new(SWSH) { Species = 836, Level = 65, Location = 138, }, // Boltund at North Lake Miloch (in a Wild Area)
            new(SWSH) { Species = 830, Level = 65, Location = 140, }, // Eldegoss at the Motostoke Riverbank (in a Wild Area)
            new(SWSH) { Species = 862, Level = 65, Location = 142, }, // Obstagoon in Bridge Field (in a Wild Area)
            new(SWSH) { Species = 861, Level = 65, Location = 144, Gender = 0, }, // Grimmsnarl in the Stony Wilderness (in a Wild Area)
            new(SWSH) { Species = 844, Level = 65, Location = 146, }, // Sandaconda in Dusty Bowl (in a Wild Area)
            new(SWSH) { Species = 863, Level = 65, Location = 148, }, // Perrserker around the Giant’s Mirror (in a Wild Area)
            new(SWSH) { Species = 879, Level = 65, Location = 150, }, // Copperajah on the Hammerlocke Hills (in a Wild Area)
            new(SWSH) { Species = 839, Level = 65, Location = 152, }, // Coalossal near the Giant’s Cap (in a Wild Area)
            new(SWSH) { Species = 858, Level = 65, Location = 154, Gender = 1 }, // Hatterene at the Lake of Outrage (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 279, Level = 26, Locations = new[] {128, 130, 134, 138, 154 }, }, // Pelipper at North/South/East/West Lake Miloch/Axwell (in a Wild Area)
            new(SWSH) { Species = 310, Level = 26, Location = 122, }, // Manectric in the Rolling Fields (in a Wild Area)
            new(SWSH) { Species = 660, Level = 26, Location = 122, }, // Diggersby in the Rolling Fields (in a Wild Area)
            new(SWSH) { Species = 281, Level = 26, Location = 122, }, // Kirlia in the Rolling Fields (in a Wild Area)
            new(SWSH) { Species = 025, Level = 15, Location = 122, }, // Pikachu in the Rolling Fields (in a Wild Area)
            new(SWSH) { Species = 439, Level = 15, Location = 122, }, // Mime Jr. in the Rolling Fields (in a Wild Area)
            new(SWSH) { Species = 221, Level = 33, Location = 122, }, // Piloswine in the Rolling Fields (in a Wild Area)
            new(SWSH) { Species = 558, Level = 34, Location = 122, }, // Crustle in the Rolling Fields (in a Wild Area)
            new(SWSH) { Species = 282, Level = 32, Location = 122, }, // Gardevoir in the Rolling Fields (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 537, Level = 36, Locations = new[] {124, 138, 142}, }, // Seismitoad in the Dappled Grove, in Bridge Field, at North Lake Miloch (in a Wild Area)
            new(SWSH) { Species = 583, Level = 36, Location = 124, }, // Vanillish in the Dappled Grove (in a Wild Area)
            new(SWSH) { Species = 344, Level = 36, Location = 124, }, // Claydol in the Dappled Grove (in a Wild Area)
            new(SWSH) { Species = 093, Level = 34, Location = 126, }, // Haunter at Watchtower Ruins (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 356, Level = 40, Locations = new[] {126, 130}, }, // Dusclops at Watchtower Ruins & at West Lake Axewell (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 362, Level = 40, Locations = new[] {126, 130}, }, // Glalie at Watchtower Ruins & at West Lake Axewell (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 279, Level = 28, Locations = new[] {138, 130}, }, // Pelipper at North Lake Miloch, West Lake Axewell (in a Wild Area)
            new(SWSH) { Species = 536, Level = 28, Location = 130, }, // Palpitoad at West Lake Axewell (in a Wild Area)
            new(SWSH) { Species = 660, Level = 28, Location = 130, }, // Diggersby at West Lake Axewell (in a Wild Area)
            new(SWSH) { Species = 221, Level = 36, Location = 128, }, // Piloswine at East Lake Axewell (in a Wild Area)
            new(SWSH) { Species = 750, Level = 36, Location = 128, }, // Mudsdale at East Lake Axewell (in a Wild Area)
            new(SWSH) { Species = 437, Level = 36, Location = 128, }, // Bronzong at East Lake Axewell (in a Wild Area)
            new(SWSH) { Species = 536, Level = 34, Location = 134, }, // Palpitoad at South Lake Miloch (in a Wild Area)
            new(SWSH) { Species = 279, Level = 26, Location = 122, }, // Pelipper in the Rolling Fields (in a Wild Area)
            new(SWSH) { Species = 093, Level = 31, Location = 122, }, // Haunter in the Rolling Fields (in a Wild Area)
            new(SWSH) { Species = 221, Level = 33, Location = 128, }, // Piloswine at East Lake Axewell (in a Wild Area)
            new(SWSH) { Species = 558, Level = 34, Location = 134, }, // Crustle at South Lake Miloch (in a Wild Area)
            new(SWSH) { Species = 067, Level = 31, Location = 134, }, // Machoke at South Lake Miloch (in a Wild Area)
            new(SWSH) { Species = 426, Level = 31, Location = 134, }, // Drifblim at South Lake Miloch (in a Wild Area)
            new(SWSH) { Species = 435, Level = 36, Location = 138, }, // Skuntank at North Lake Miloch (in a Wild Area)
            new(SWSH) { Species = 583, Level = 36, Location = 138, }, // Vanillish at North Lake Miloch (in a Wild Area)
            new(SWSH) { Species = 426, Level = 36, Location = 138, }, // Drifblim at North Lake Miloch (in a Wild Area)
            new(SWSH) { Species = 437, Level = 46, Location = 136, }, // Bronzong near the Giant’s Seat (in a Wild Area)
            new(SWSH) { Species = 460, Level = 46, Location = 136, }, // Abomasnow near the Giant’s Seat (in a Wild Area)
            new(SWSH) { Species = 750, Level = 46, Location = 136, }, // Mudsdale near the Giant’s Seat (in a Wild Area)
            new(SWSH) { Species = 623, Level = 46, Location = 136, }, // Golurk near the Giant’s Seat (in a Wild Area)
            new(SWSH) { Species = 356, Level = 46, Location = 136, }, // Dusclops near the Giant’s Seat (in a Wild Area)
            new(SWSH) { Species = 518, Level = 46, Location = 136, }, // Musharna near the Giant’s Seat (in a Wild Area)
            new(SWSH) { Species = 362, Level = 46, Location = 136, }, // Glalie near the Giant’s Seat (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 596, Level = 46, Locations = new[] {134, 136}, }, // Galvantula at South Lake Miloch and near the Giant’s Seat (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 584, Level = 47, Locations = new[] {128, 130, 134, 138, 142}, }, // Vanilluxe at North/East/South/West Lake Miloch/Axwell, Bridge Field (in a Wild Area)
            new(SWSH) { Species = 537, Level = 60, Location = 132, }, // Seismitoad on Axew’s Eye (in a Wild Area)
            new(SWSH) { Species = 460, Level = 60, Location = 132, }, // Abomasnow on Axew’s Eye (in a Wild Area)
            new(SWSH) { Species = 036, Level = 36, Location = 140, }, // Clefable at the Motostoke Riverbank (in a Wild Area)
            new(SWSH) { Species = 743, Level = 40, Location = 140, }, // Ribombee at the Motostoke Riverbank (in a Wild Area)
            new(SWSH) { Species = 112, Level = 55, Location = 140, }, // Rhydon at the Motostoke Riverbank (in a Wild Area)
            new(SWSH) { Species = 823, Level = 40, Location = 140, }, // Corviknight at the Motostoke Riverbank (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 760, Level = 40, Locations = new[] {140, 142}, }, // Bewear in Bridge Field, Motostoke Riverbank (in a Wild Area)
            new(SWSH) { Species = 614, Level = 60, Location = 142, }, // Beartic in Bridge Field (in a Wild Area)
            new(SWSH) { Species = 461, Level = 60, Location = 142, }, // Weavile in Bridge Field (in a Wild Area)
            new(SWSH) { Species = 518, Level = 60, Location = 142, }, // Musharna in Bridge Field (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 437, Level = 42, Locations = new[] {142, 144}, }, // Bronzong in Bridge Field & Stony Wilderness (in a Wild Area)
            new(SWSH) { Species = 344, Level = 42, Location = 142, }, // Claydol in Bridge Field (in a Wild Area)
            new(SWSH) { Species = 452, Level = 50, Location = 142, }, // Drapion in Bridge Field (in a Wild Area)
            new(SWSH) { Species = 164, Level = 50, Location = 142, }, // Noctowl in Bridge Field (in a Wild Area)
            new(SWSH) { Species = 760, Level = 46, Location = 130, }, // Bewear at West Lake Axewell (in a Wild Area)
            new(SWSH) { Species = 675, Level = 42, Location = 142, }, // Pangoro in Bridge Field (in a Wild Area)
            new(SWSH) { Species = 584, Level = 50, Location = 142, }, // Vanilluxe in Bridge Field (in a Wild Area)
            new(SWSH) { Species = 112, Level = 50, Location = 142, }, // Rhydon in Bridge Field (in a Wild Area)
            new(SWSH) { Species = 778, Level = 50, Location = 142, }, // Mimikyu in Bridge Field (in a Wild Area)
            new(SWSH) { Species = 521, Level = 40, Location = 144, }, // Unfezant in the Stony Wilderness (in a Wild Area)
            new(SWSH) { Species = 752, Level = 34, Location = 144, }, // Araquanid in the Stony Wilderness (in a Wild Area)
            new(SWSH) { Species = 537, Level = 41, Location = 146, }, // Seismitoad in Dusty Bowl (in a Wild Area)
            new(SWSH) { Species = 435, Level = 41, Location = 146, }, // Skuntank in Dusty Bowl (in a Wild Area)
            new(SWSH) { Species = 221, Level = 41, Location = 146, }, // Piloswine in Dusty Bowl (in a Wild Area)
            new(SWSH) { Species = 356, Level = 41, Location = 146, }, // Dusclops in Dusty Bowl (in a Wild Area)
            new(SWSH) { Species = 344, Level = 41, Location = 146, }, // Claydol in Dusty Bowl (in a Wild Area)
            new(SWSH) { Species = 689, Level = 60, Location = 146, }, // Barbaracle in Dusty Bowl (in a Wild Area)
            new(SWSH) { Species = 561, Level = 51, Location = 146, }, // Sigilyph in Dusty Bowl (in a Wild Area)
            new(SWSH) { Species = 623, Level = 51, Location = 146, }, // Golurk in Dusty Bowl (in a Wild Area)
            new(SWSH) { Species = 537, Level = 60, Location = 148, }, // Seismitoad around the Giant’s Mirror (in a Wild Area)
            new(SWSH) { Species = 460, Level = 60, Location = 148, }, // Abomasnow around the Giant’s Mirror (in a Wild Area)
            new(SWSH) { Species = 045, Level = 41, Location = 150, }, // Vileplume on the Hammerlocke Hills (in a Wild Area)
            new(SWSH) { Species = 178, Level = 41, Location = 148, }, // Xatu around the Giant’s Mirror (in a Wild Area)
            new(SWSH) { Species = 768, Level = 60, Location = 152, }, // Golisopod near the Giant’s Cap (in a Wild Area)
            new(SWSH) { Species = 614, Level = 60, Location = 152, }, // Beartic near the Giant’s Cap (in a Wild Area)
            new(SWSH) { Species = 530, Level = 46, Location = 152, }, // Excadrill near the Giant’s Cap (in a Wild Area)
            new(SWSH) { Species = 362, Level = 46, Location = 152, }, // Glalie near the Giant’s Cap (in a Wild Area)
            new(SWSH) { Species = 537, Level = 46, Location = 152, }, // Seismitoad near the Giant’s Cap (in a Wild Area)
            new(SWSH) { Species = 681, Level = 58, Location = 152, }, // Aegislash near the Giant’s Cap (in a Wild Area)
            new(SWSH) { Species = 601, Level = 49, Location = 150, }, // Klinklang on the Hammerlocke Hills (in a Wild Area)
            new(SWSH) { Species = 407, Level = 41, Location = 150, }, // Roserade on the Hammerlocke Hills (in a Wild Area)
            new(SWSH) { Species = 460, Level = 41, Location = 150, }, // Abomasnow on the Hammerlocke Hills (in a Wild Area)
            new EncounterStatic8S(SWSH) { Species = 350, Level = 60, Locations = new[] {128, 130, 134, 138, 154 }, Gender = 0, Ability = 1, }, // Milotic at the Lake of Outrage, at North/South/East/West Lake Miloch/Axwell (in a Wild Area)
            new(SWSH) { Species = 112, Level = 60, Location = 154, }, // Rhydon at the Lake of Outrage (in a Wild Area)
            new(SWSH) { Species = 609, Level = 60, Location = 154, }, // Chandelure at the Lake of Outrage (in a Wild Area)
            new(SWSH) { Species = 713, Level = 60, Location = 154, }, // Avalugg at the Lake of Outrage (in a Wild Area)
            new(SWSH) { Species = 756, Level = 60, Location = 154, }, // Shiinotic at the Lake of Outrage (in a Wild Area)
            new(SWSH) { Species = 134, Level = 56, Location = 154, }, // Vaporeon at the Lake of Outrage (in a Wild Area)
            new(SWSH) { Species = 135, Level = 56, Location = 154, }, // Jolteon at the Lake of Outrage (in a Wild Area)
            new(SWSH) { Species = 196, Level = 56, Location = 154, }, // Espeon at the Lake of Outrage (in a Wild Area)
            new(SWSH) { Species = 471, Level = 56, Location = 154, }, // Glaceon at the Lake of Outrage (in a Wild Area)
            new(SWSH) { Species = 136, Level = 56, Location = 154, }, // Flareon at the Lake of Outrage (in a Wild Area)
            new(SWSH) { Species = 197, Level = 56, Location = 154, }, // Umbreon at the Lake of Outrage (in a Wild Area)
            new(SWSH) { Species = 700, Level = 56, Location = 154, }, // Sylveon at the Lake of Outrage (in a Wild Area)
            #endregion

            #region R1 Static Encounters
            new(SWSH) { Species = 079, Level = 12, Location = 016, Form = 01, Shiny = Never }, // Slowpoke-1 at Wedgehurst Station
            new(SWSH) { Species = 748, Level = 20, Location = 164 }, // Toxapex in the Fields of Honor (on the Isle of Armor)
            new(SWSH) { Species = 099, Level = 20, Location = 164 }, // Kingler in the Fields of Honor (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 834, Level = 20, Locations = new[] {164, 166} }, // Drednaw in the Fields of Honor, in the Soothing Wetlands (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 687, Level = 26, Locations = new[] {164, 166} }, // Malamar in the Fields of Honor, in the Soothing Wetlands (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 764, Level = 15, Locations = new[] {164, 166} }, // Comfey in the Fields of Honor, in the Soothing Wetlands (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 404, Level = 20, Locations = new[] {164, 166} }, // Luxio in the Fields of Honor, in the Soothing Wetlands (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 744, Level = 15, Locations = new[] {164, 166} }, // Rockruff in the Fields of Honor, in the Soothing Wetlands (on the Isle of Armor)
            new(SWSH) { Species = 195, Level = 20, Location = 166 }, // Quagsire in the Soothing Wetlands (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 570, Level = 15, Locations = new[] {164, 166} }, // Zorua in the Fields of Honor, in the Soothing Wetlands (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 040, Level = 27, Locations = new[] {164, 166} }, // Wigglytuff in the Fields of Honor, in the Soothing Wetlands (on the Isle of Armor)
            new(SWSH) { Species = 626, Level = 20, Location = 166 }, // Bouffalant in the Soothing Wetlands (on the Isle of Armor)
            new(SWSH) { Species = 242, Level = 22, Location = 166 }, // Blissey in the Soothing Wetlands (on the Isle of Armor)
            new(SWSH) { Species = 452, Level = 22, Location = 166 }, // Drapion in the Soothing Wetlands (on the Isle of Armor)
            new(SWSH) { Species = 463, Level = 27, Location = 166 }, // Lickilicky in the Soothing Wetlands (on the Isle of Armor)
            new(SWSH) { Species = 834, Level = 21, Location = -01 }, // Drednaw
            new(SWSH) { Species = 405, Level = 32, Location = 166 }, // Luxray in the Soothing Wetlands (on the Isle of Armor)
            new(SWSH) { Species = 121, Level = 20, Location = 164 }, // Starmie in the Fields of Honor (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 428, Level = 22, Locations = new[] {164, 166} }, // Lopunny in the Fields of Honor, in the Soothing Wetlands (on the Isle of Armor)
            new(SWSH) { Species = 453, Level = 20, Location = 166 }, // Croagunk in the Soothing Wetlands (on the Isle of Armor)
            new(SWSH) { Species = 186, Level = 32, Location = 166 }, // Politoed in the Soothing Wetlands (on the Isle of Armor)
            new(SWSH) { Species = 061, Level = 20, Location = 166 }, // Poliwhirl in the Soothing Wetlands (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 183, Level = 15, Locations = new[] {164, 166, 170} }, // Marill in the Fields of Honor, in the Soothing Wetlands, on Challenge Beach (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 662, Level = 20, Locations = new[] {164, 166} }, // Fletchinder in the Fields of Honor, in the Soothing Wetlands (on the Isle of Armor)
            new(SWSH) { Species = 768, Level = 26, Location = -01 }, // Golisopod
            new(SWSH) { Species = 636, Level = 15, Location = 168 }, // Larvesta in the Forest of Focus (on the Isle of Armor)
            new(SWSH) { Species = 549, Level = 22, Location = 166 }, // Lilligant in the Soothing Wetlands (on the Isle of Armor)
            new(SWSH) { Species = 025, Level = 22, Location = 168 }, // Pikachu in the Forest of Focus (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 064, Level = 20, Locations = new[] {164, 166} }, // Kadabra in the Fields of Honor, in the Soothing Wetlands (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 026, Level = 26, Locations = new[] {166, 168} }, // Raichu in the Soothing Wetlands, in the Forest of Focus (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 025, Level = 15, Locations = new[] {164, 166} }, // Pikachu in the Fields of Honor, in the Soothing Wetlands (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 184, Level = 21, Locations = new[] {166, 168} }, // Azumarill in the Soothing Wetlands, in the Forest of Focus (on the Isle of Armor)
            new(SW  ) { Species = 559, Level = 20, Location = 166 }, // Scraggy in the Soothing Wetlands (on the Isle of Armor)
            new(SWSH) { Species = 663, Level = 32, Location = 166 }, // Talonflame in the Soothing Wetlands (on the Isle of Armor)
            new(SW  ) { Species = 766, Level = 26, Location = 168 }, // Passimian in the Forest of Focus (on the Isle of Armor)
            new(  SH) { Species = 765, Level = 26, Location = 168 }, // Oranguru in the Forest of Focus (on the Isle of Armor)
            new(SWSH) { Species = 342, Level = 26, Location = 168 }, // Crawdaunt in the Forest of Focus (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 754, Level = 27, Locations = new[] {168, 170} }, // Lurantis in the Forest of Focus, on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 040, Level = 26, Location = 168 }, // Wigglytuff in the Forest of Focus (on the Isle of Armor)
            new(SWSH) { Species = 028, Level = 26, Location = 168 }, // Sandslash in the Forest of Focus (on the Isle of Armor)
            new(SWSH) { Species = 545, Level = 32, Location = 168 }, // Scolipede in the Forest of Focus (on the Isle of Armor)
            new(  SH) { Species = 214, Level = 26, Location = 168 }, // Heracross in the Forest of Focus (on the Isle of Armor)
            new(  SH) { Species = 704, Level = 20, Location = 168 }, // Goomy in the Forest of Focus (on the Isle of Armor)
            new(SWSH) { Species = 172, Level = 20, Location = 168 }, // Pichu in the Forest of Focus (on the Isle of Armor)
            new(SWSH) { Species = 845, Level = 20, Location = 168 }, // Cramorant in the Forest of Focus (on the Isle of Armor)
            new(SWSH) { Species = 465, Level = 36, Location = 168 }, // Tangrowth in the Forest of Focus (on the Isle of Armor)
            new(SWSH) { Species = 589, Level = 32, Location = 168 }, // Escavalier in the Forest of Focus (on the Isle of Armor)
            new(SWSH) { Species = 617, Level = 32, Location = 168 }, // Accelgor in the Forest of Focus (on the Isle of Armor)
            new(SWSH) { Species = 591, Level = 26, Location = 168 }, // Amoonguss in the Forest of Focus (on the Isle of Armor)
            new(SWSH) { Species = 764, Level = 22, Location = 168 }, // Comfey in the Forest of Focus (on the Isle of Armor)
            new(SWSH) { Species = 570, Level = 22, Location = 168 }, // Zorua in the Forest of Focus (on the Isle of Armor)
            new(SWSH) { Species = 104, Level = 20, Location = 168 }, // Cubone in the Forest of Focus (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 282, Level = 36, Locations = new[] {168, 180} }, // Gardevoir in the Forest of Focus (on the Isle of Armor)
            new(SW  ) { Species = 127, Level = 26, Location = 168 }, // Pinsir in the Forest of Focus (on the Isle of Armor)
            new(SWSH) { Species = 055, Level = 26, Location = 168 }, // Golduck in the Forest of Focus (on the Isle of Armor)
            new(SWSH) { Species = 462, Level = 36, Location = 170 }, // Magnezone on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 475, Level = 20, Location = -01 }, // Gallade
            new(SWSH) { Species = 625, Level = 20, Location = -01 }, // Bisharp
            new(SWSH) { Species = 082, Level = 27, Location = -01 }, // Magneton
            new(SW  ) { Species = 616, Level = 20, Location = 168 }, // Shelmet in the Forest of Focus (on the Isle of Armor)
            new(SWSH) { Species = 105, Level = 20, Location = -01 }, // Marowak
            new(SWSH) { Species = 637, Level = 42, Location = 170 }, // Volcarona on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 687, Level = 29, Location = 170 }, // Malamar on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 428, Level = 27, Location = 170 }, // Lopunny on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 452, Level = 27, Location = 170 }, // Drapion on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 026, Level = 29, Location = 170 }, // Raichu on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 558, Level = 20, Location = -01 }, // Crustle
            new(SWSH) { Species = 764, Level = 25, Location = 170 }, // Comfey on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 877, Level = 25, Location = 170 }, // Morpeko on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 834, Level = 26, Location = 170 }, // Drednaw on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 040, Level = 29, Location = 170 }, // Wigglytuff on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 528, Level = 27, Location = 170 }, // Swoobat on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 279, Level = 26, Location = 170 }, // Pelipper on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 082, Level = 26, Location = 170 }, // Magneton on Challenge Beach (on the Isle of Armor)
            new EncounterStatic8S(SW) { Species = 782, Level = 22, Locations = new[] {172, 174, 180} }, // Jangmo-o on Challenge Road, in the Training Lowlands (on the Isle of Armor) and in Brawlers' Cave
            new(SWSH) { Species = 426, Level = 26, Location = 170 }, // Drifblim on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 768, Level = 36, Location = 170 }, // Golisopod on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 662, Level = 26, Location = 170 }, // Fletchinder on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 342, Level = 27, Location = 170 }, // Crawdaunt on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 184, Level = 27, Location = 170 }, // Azumarill on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 549, Level = 26, Location = 170 }, // Lilligant on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 845, Level = 24, Location = 170 }, // Cramorant on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 055, Level = 27, Location = 170, Ability = 2 }, // Golduck on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 702, Level = 25, Location = 170 }, // Dedenne on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 113, Level = 27, Location = -01 }, // Chansey
            new(SWSH) { Species = 405, Level = 36, Location = 170 }, // Luxray on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 099, Level = 26, Location = 170 }, // Kingler on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 121, Level = 26, Location = 170 }, // Starmie on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 748, Level = 26, Location = 170 }, // Toxapex on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 062, Level = 36, Location = 172 }, // Poliwrath in Brawlers’ Cave (on the Isle of Armor)
            new(SWSH) { Species = 294, Level = 26, Location = 172 }, // Loudred in Brawlers’ Cave (on the Isle of Armor)
            new(SWSH) { Species = 528, Level = 26, Location = 172 }, // Swoobat in Brawlers’ Cave (on the Isle of Armor)
            new(SWSH) { Species = 621, Level = 36, Location = 172 }, // Druddigon in Brawlers’ Cave (on the Isle of Armor)
            new(SWSH) { Species = 055, Level = 26, Location = 172 }, // Golduck in Brawlers’ Cave (on the Isle of Armor)
            new(SWSH) { Species = 526, Level = 42, Location = 172 }, // Gigalith in Brawlers’ Cave (on the Isle of Armor)
            new(SWSH) { Species = 620, Level = 28, Location = 174 }, // Mienshao on Challenge Road (on the Isle of Armor)
            new(SWSH) { Species = 625, Level = 36, Location = 174 }, // Bisharp on Challenge Road (on the Isle of Armor)
            new EncounterStatic8S(SH) { Species = 454, Level = 26, Locations = new[] {172, 174, 180} }, // Toxicroak on Challenge Road (on the Isle of Armor) and in Brawlers’ Cave
            new EncounterStatic8S(SW) { Species = 560, Level = 26, Locations = new[] {172, 174, 180} }, // Scrafty on Challenge Road, in the Training Lowlands (on the Isle of Armor) and in Brawlers’ Cave
            new(SWSH) { Species = 758, Level = 28, Location = 174, Gender = 1 }, // Salazzle on Challenge Road (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 558, Level = 26, Locations = new[] {172, 174, 180} }, // Crustle on Challenge Road, in the Training Lowlands (on the Isle of Armor) and in Brawlers’ Cave
            new(SWSH) { Species = 475, Level = 32, Location = 174 }, // Gallade on Challenge Road (on the Isle of Armor)
            new(SWSH) { Species = 745, Level = 32, Location = 174 }, // Lycanroc on Challenge Road (on the Isle of Armor)
            new(SWSH) { Species = 745, Level = 32, Location = 174, Form = 01 }, // Lycanroc-1 on Challenge Road (on the Isle of Armor)
            new(SWSH) { Species = 212, Level = 40, Location = 174 }, // Scizor on Challenge Road (on the Isle of Armor)
            new(  SH) { Species = 214, Level = 26, Location = 174 }, // Heracross on Challenge Road (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 744, Level = 22, Locations = new[] {172,  174} }, // Rockruff on Challenge Road (on the Isle of Armor) and in Brawlers' Cave
            new(SW  ) { Species = 127, Level = 26, Location = 174 }, // Pinsir on Challenge Road (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 227, Level = 26, Locations = new[] {174, 180} }, // Skarmory on Challenge Road (on the Isle of Armor), in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 426, Level = 26, Location = 174 }, // Drifblim on Challenge Road (on the Isle of Armor)
            new(  SH) { Species = 630, Level = 26, Location = 174 }, // Mandibuzz on Challenge Road (on the Isle of Armor)
            new(SW  ) { Species = 628, Level = 26, Location = 174 }, // Braviary on Challenge Road (on the Isle of Armor)
            new(SWSH) { Species = 082, Level = 26, Location = 174 }, // Magneton on Challenge Road (on the Isle of Armor)
            new(SWSH) { Species = 558, Level = 28, Location = 176 }, // Crustle in Courageous Cavern (on the Isle of Armor)
            new(SWSH) { Species = 526, Level = 42, Location = -01 }, // Gigalith
            new(SWSH) { Species = 768, Level = 32, Location = 176 }, // Golisopod in Courageous Cavern (on the Isle of Armor)
            new(SWSH) { Species = 528, Level = 28, Location = 176 }, // Swoobat in Courageous Cavern (on the Isle of Armor)
            new(SWSH) { Species = 834, Level = 28, Location = 176 }, // Drednaw in Courageous Cavern (on the Isle of Armor)
            new(SWSH) { Species = 113, Level = 30, Location = -01 }, // Chansey
            new(SWSH) { Species = 687, Level = 32, Location = 178 }, // Malamar in Loop Lagoon (on the Isle of Armor)
            new(SWSH) { Species = 040, Level = 32, Location = 178 }, // Wigglytuff in Loop Lagoon (on the Isle of Armor)
            new(SWSH) { Species = 768, Level = 32, Location = -01 }, // Golisopod
            new(SWSH) { Species = 404, Level = 30, Location = 178 }, // Luxio in Loop Lagoon (on the Isle of Armor)
            new(SWSH) { Species = 834, Level = 30, Location = 178 }, // Drednaw in Loop Lagoon (on the Isle of Armor)
            new(SWSH) { Species = 558, Level = 30, Location = -01 }, // Crustle
            new(SWSH) { Species = 871, Level = 22, Location = 178 }, // Pincurchin in Loop Lagoon (on the Isle of Armor)
            new(SWSH) { Species = 748, Level = 26, Location = 178 }, // Toxapex in Loop Lagoon (on the Isle of Armor)
            new(SWSH) { Species = 853, Level = 32, Location = 178 }, // Grapploct in Loop Lagoon (on the Isle of Armor)
            new(SWSH) { Species = 770, Level = 32, Location = 178 }, // Palossand in Loop Lagoon (on the Isle of Armor)
            new EncounterStatic8S(SWSH)  { Species = 065, Level = 50, Locations = new[] { 178, 190 } }, // Alakazam in Loop Lagoon (on the Isle of Armor) and in the Insular Sea (on the Isle of Armor)
            new(SWSH) { Species = 571, Level = 50, Location = 178 }, // Zoroark in Loop Lagoon (on the Isle of Armor)
            new(SWSH) { Species = 462, Level = 50, Location = 178 }, // Magnezone in Loop Lagoon (on the Isle of Armor)
            new(SWSH) { Species = 744, Level = 40, Location = 178 }, // Rockruff in Loop Lagoon (on the Isle of Armor)
            new(SWSH) { Species = 636, Level = 40, Location = 178 }, // Larvesta in Loop Lagoon (on the Isle of Armor)
            new(SWSH) { Species = 279, Level = 42, Location = 178 }, // Pelipper in Loop Lagoon (on the Isle of Armor)
            new(SWSH) { Species = 405, Level = 50, Location = 178 }, // Luxray in Loop Lagoon (on the Isle of Armor)
            new(SWSH) { Species = 663, Level = 50, Location = 178 }, // Talonflame in Loop Lagoon (on the Isle of Armor)
            new(SWSH) { Species = 508, Level = 42, Location = 180 }, // Stoutland in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 625, Level = 36, Location = 180 }, // Bisharp in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 405, Level = 36, Location = 180 }, // Luxray in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 663, Level = 36, Location = 180 }, // Talonflame in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 040, Level = 30, Location = 180 }, // Wigglytuff in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 099, Level = 28, Location = 180 }, // Kingler in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 115, Level = 32, Location = 180 }, // Kangaskhan in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 123, Level = 28, Location = 180 }, // Scyther in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 404, Level = 28, Location = 180 }, // Luxio in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 764, Level = 28, Location = 180 }, // Comfey in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 452, Level = 28, Location = 180 }, // Drapion in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 279, Level = 28, Location = 180 }, // Pelipper in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 127, Level = 28, Location = 180 }, // Pinsir in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 528, Level = 28, Location = 180 }, // Swoobat in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 241, Level = 28, Location = 180 }, // Miltank in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 082, Level = 28, Location = 180 }, // Magneton in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 662, Level = 28, Location = 180 }, // Fletchinder in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 128, Level = 28, Location = 180 }, // Tauros in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 687, Level = 28, Location = 180 }, // Malamar in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 214, Level = 28, Location = 180 }, // Heracross in the Training Lowlands (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 507, Level = 28, Locations = new[] {174, 180} }, // Herdier on Challenge Road and in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 549, Level = 28, Location = 180 }, // Lilligant in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 426, Level = 28, Location = 180 }, // Drifblim in the Training Lowlands (on the Isle of Armor
            new(SWSH) { Species = 055, Level = 26, Location = 180 }, // Golduck in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 184, Level = 26, Location = 180 }, // Azumarill in the Training Lowlands (on the Isle of Armor
            new(SWSH) { Species = 617, Level = 36, Location = 180 }, // Accelgor in the Training Lowlands (on the Isle of Armor
            new(SWSH) { Species = 212, Level = 42, Location = 180 }, // Scizor in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 589, Level = 36, Location = 180 }, // Escavalier in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 616, Level = 26, Location = 180 }, // Shelmet in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 588, Level = 26, Location = 180 }, // Karrablast in the Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 553, Level = 50, Location = 184 }, // Krookodile in the Potbottom Desert (on the Isle of Armor)
            new(SWSH) { Species = 464, Level = 50, Location = 184 }, // Rhyperior in the Potbottom Desert (on the Isle of Armor)
            new(SWSH) { Species = 105, Level = 42, Location = 184 }, // Marowak in the Potbottom Desert (on the Isle of Armor)
            new(SWSH) { Species = 552, Level = 42, Location = 184 }, // Krokorok in the Potbottom Desert (on the Isle of Armor)
            new(SWSH) { Species = 112, Level = 42, Location = 184 }, // Rhydon in the Potbottom Desert (on the Isle of Armor)
            new(SWSH) { Species = 324, Level = 42, Location = 184 }, // Torkoal in the Potbottom Desert (on the Isle of Armor)
            new(SWSH) { Species = 844, Level = 42, Location = 184 }, // Sandaconda in the Potbottom Desert (on the Isle of Armor)
            new(SWSH) { Species = 637, Level = 50, Location = 184 }, // Volcarona in the Potbottom Desert (on the Isle of Armor)
            new(SW  ) { Species = 628, Level = 42, Location = 184 }, // Braviary in the Potbottom Desert (on the Isle of Armor)
            new(  SH) { Species = 630, Level = 42, Location = 184 }, // Mandibuzz in the Potbottom Desert (on the Isle of Armor)
            new(SWSH) { Species = 103, Level = 50, Location = 190 }, // Exeggutor in the Insular Sea (on the Isle of Armor)
         // new(SWSH) { Species = 132, Level = 50, Location = 186, FlawlessIVCount = 3 }, // Ditto in the Workout Sea (on the Isle of Armor) -- collision with wild Ditto in the same area
            new(SWSH) { Species = 242, Level = 50, Location = -01 }, // Blissey
            new(SWSH) { Species = 571, Level = 50, Location = 190 }, // Zoroark in the Insular Sea (on the Isle of Armor)
            new(SWSH) { Species = 462, Level = 50, Location = 190 }, // Magnezone in the Insular Sea (on the Isle of Armor)
            new(SWSH) { Species = 637, Level = 50, Location = 190 }, // Volcarona in the Insular Sea (on the Isle of Armor)
            new(SWSH) { Species = 279, Level = 45, Location = 190 }, // Pelipper in the Insular Sea (on the Isle of Armor)
            new(SWSH) { Species = 549, Level = 45, Location = 194 }, // Lilligant on Honeycalm Island (on the Isle of Armor)
            new(SWSH) { Species = 415, Level = 40, Location = 194 }, // Combee on Honeycalm Island (on the Isle of Armor)
            new(SWSH) { Species = 028, Level = 42, Location = 184 }, // Sandslash in the Potbottom Desert (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 587, Level = 20, Locations = new[] {166, 168} }, // Emolga in the Soothing Wetlands and in the Forest of Focus (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 847, Level = 42, Locations = new[] {166, 170, 176, 180} }, // Barraskewda in the Soothing Wetlands, on Challenge Beach, in Courageous Cavern, and Training Lowlands (on the Isle of Armor)
            new(SWSH) { Species = 224, Level = 45, Location = 170 }, // Octillery on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 171, Level = 42, Location = 170 }, // Lanturn on Challenge Beach (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 593, Level = 42, Locations = new[] {170, 178} }, // Jellicent on Challenge Beach and in Loop Lagoon (on the Isle of Armor)
            new(SWSH) { Species = 091, Level = 42, Location = 170 }, // Cloyster on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 130, Level = 50, Location = 170 }, // Gyarados on Challenge Beach (on the Isle of Armor)
            new(SWSH) { Species = 073, Level = 42, Location = 176 }, // Tentacruel in Courageous Cavern (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 340, Level = 42, Locations = new[] {168, 172, 176} }, // Whiscash in the Forest of Focus, in Brawlers’ Cave, in Courageous Cavern (on the Isle of Armor)
            new(SWSH) { Species = 342, Level = 42, Location = 180 }, // Crawdaunt in the Training Lowlands (on the Isle of Armor)
            // new(SWSH) { Species = 479, Level = 50, Location = 186, FlawlessIVCount = 3 }, // Rotom in the Workout Sea (on the Isle of Armor) -- collision with subsequent static Rotom
            new(SWSH) { Species = 479, Level = 50, Location = 186, Moves = new[] {435,506,268}, Form = 01 }, // Rotom-1 in the Workout Sea (on the Isle of Armor)
            new(SWSH) { Species = 479, Level = 50, Location = 186, Moves = new[] {435,506,268}, Form = 02 }, // Rotom-2 in the Workout Sea (on the Isle of Armor)
            new(SWSH) { Species = 479, Level = 50, Location = 186, Moves = new[] {435,506,268}, Form = 03 }, // Rotom-3 in the Workout Sea (on the Isle of Armor)
            new(SWSH) { Species = 479, Level = 50, Location = 186, Moves = new[] {435,506,268}, Form = 04 }, // Rotom-4 in the Workout Sea (on the Isle of Armor)
            new(SWSH) { Species = 479, Level = 50, Location = 186, Moves = new[] {435,506,268}, Form = 05 }, // Rotom-5 in the Workout Sea (on the Isle of Armor)
            new(SWSH) { Species = 230, Level = 60, Location = 192 }, // Kingdra in the Honeycalm Sea (on the Isle of Armor)
            new(SWSH) { Species = 117, Level = 45, Location = 192 }, // Seadra in the Honeycalm Sea (on the Isle of Armor)
            new(SWSH) { Species = 321, Level = 80, Location = 186 }, // Wailord in the Workout Sea (on the Isle of Armor)
            new(SWSH) { Species = 039, Level = 20, Location = 168 }, // Jigglypuff in the Forest of Focus (on the Isle of Armor)
            new EncounterStatic8S(SWSH) { Species = 764, Level = 50, Locations = new[] {190, 194} }, // Comfey in the Insular Sea and in the Honeycalm Sea (on the Isle of Armor)
            new(SWSH) { Species = 621, Level = 42, Location = 176 }, // Druddigon in Courageous Cavern (on the Isle of Armor)
            #endregion

            #region R2 Static Encounters
            new EncounterStatic8S(SWSH) { Species = 144, Level = 70, Locations = new[] {208, 210, 212, 214}, Moves = new[] {821,542,427,375}, FlawlessIVCount = 3, Shiny = Never, Ability = 1, Form = 01 }, // Articuno-1 in the Crown Tundra
            new EncounterStatic8S(SWSH) { Species = 145, Level = 70, Locations = new[] {122, 124, 126, 128, 130}, Moves = new[] {823,065,179,116}, FlawlessIVCount = 3, Shiny = Never, Ability = 1, Form = 01 }, // Zapdos-1 in a Wild Area
            new EncounterStatic8S(SWSH) { Species = 146, Level = 70, Locations = new[] {164, 166, 170, 178, 186, 188, 190}, Moves = new[] {822,542,389,417}, FlawlessIVCount = 3, Shiny = Never, Ability = 1, Form = 01 }, // Moltres-1 on the Isle of Armor
            new(SWSH) { Species = 377, Level = 70, Location = 236, Moves = new[] {276,444,359,174}, FlawlessIVCount = 3, Ability = 1 }, // Regirock
            new(SWSH) { Species = 378, Level = 70, Location = 238, Moves = new[] {058,192,133,196}, FlawlessIVCount = 3, Ability = 1 }, // Regice
            new(SWSH) { Species = 379, Level = 70, Location = 240, Moves = new[] {484,430,334,451}, FlawlessIVCount = 3, Ability = 1 }, // Registeel
            new(SWSH) { Species = 894, Level = 70, Location = 242, Moves = new[] {819,527,245,393}, FlawlessIVCount = 3, Ability = 1 }, // Regieleki
            new(SWSH) { Species = 895, Level = 70, Location = 242, Moves = new[] {820,337,359,673}, FlawlessIVCount = 3, Ability = 1 }, // Regidrago
            new(SWSH) { Species = 486, Level =100, Location = 210, Moves = new[] {416,428,359,462}, FlawlessIVCount = 3, Ability = 1, DynamaxLevel = 10, ScriptedNoMarks = true }, // Regigigas in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 638, Level = 70, Location = 226, FlawlessIVCount = 3, Ability = 1 }, // Cobalion
            new(SWSH) { Species = 639, Level = 70, Location = 232, FlawlessIVCount = 3, Ability = 1 }, // Terrakion
            new(SWSH) { Species = 640, Level = 70, Location = 210, FlawlessIVCount = 3, Ability = 1 }, // Virizion
            new(SWSH) { Species = 647, Level = 65, Location = 230, Moves = new[] {548,533,014,056}, FlawlessIVCount = 3, Shiny = Never, Ability = 1, Form = 01, Fateful = true }, // Keldeo-1 at Ballimere Lake (in the Crown Tundra)
            // new(SWSH) { Species = 896, Level = 75, Location = -01, Moves = new[] {556,037,419,023}, FlawlessIVCount = 3, Shiny = Never, Ability = 1 }, // Glastrier
            // new(SWSH) { Species = 897, Level = 75, Location = -01, Moves = new[] {247,037,506,024}, FlawlessIVCount = 3, Shiny = Never, Ability = 1 }, // Spectrier
            new(SWSH) { Species = 898, Level = 80, Location = 220, Moves = new[] {202,094,473,505}, FlawlessIVCount = 3, Shiny = Never, Ability = 1, ScriptedNoMarks = true }, // Calyrex

            // suspected unused or uncatchable
            // new(SWSH) { Species = 803, Level = 60, Location = -01, FlawlessIVCount = 3, Shiny = Never, Ability = 1 }, // Poipole
            // new(SWSH) { Species = 789, Level = 60, Location = -01, FlawlessIVCount = 3, Shiny = Never, Ability = 1 }, // Cosmog
            // new(SWSH) { Species = 494, Level = 70, Location = -01, FlawlessIVCount = 3, Shiny = Never, Ability = 1 }, // Victini

            new(SWSH) { Species = 473, Level = 65, Location = 204 }, // Mamoswine on Slippery Slope (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 698, Level = 60, Locations = new[] {204, 208}, }, // Amaura on Slippery Slope, in Frostpoint Field (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 858, Level = 65, Locations = new[] {208, 210}, }, // Hatterene in Frostpoint Field, in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 461, Level = 63, Location = 208 }, // Weavile in Frostpoint Field (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 832, Level = 63, Locations = new[] {204, 208, 210}, }, // Dubwool on Slippery Slope, in Frostpoint Field, in the Giant’s Bed (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 333, Level = 60, Locations = new[] {204, 208, 210}, }, // Swablu on Slippery Slope, in Frostpoint Field, in the Giant’s Bed (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 124, Level = 62, Locations = new[] {204, 208}, }, // Jynx on Slippery Slope, in Frostpoint Field (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 615, Level = 62, Locations = new[] {204, 208, 210}, }, // Cryogonal on Slippery Slope, in Frostpoint Field, in the Giant’s Bed (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 778, Level = 62, Locations = new[] {204, 208, 210, 212}, }, // Mimikyu on Slippery Slope, in Frostpoint Field, in the Giant’s Bed, in the Old Cemetery (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 460, Level = 65, Locations = new[] {204, 208}, }, // Abomasnow on Slippery Slope, in Frostpoint Field (in the Crown Tundra)
            new(SWSH) { Species = 872, Level = 60, Location = 204 }, // Snom on Slippery Slope (in the Crown Tundra)
            new EncounterStatic8S(SW  ) { Species = 576, Level = 65, Locations = new[] {204, 208}, }, // Gothitelle on Slippery Slope, in Frostpoint Field (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 133, Level = 60, Locations = new[] {208, 210}, }, // Eevee in Frostpoint Field, in the Giant’s Bed (in the Crown Tundra)
            new EncounterStatic8S(  SH) { Species = 579, Level = 65, Locations = new[] {204, 208}, }, // Reuniclus on Slippery Slope, in Frostpoint Field (in the Crown Tundra)
            new(SWSH) { Species = 857, Level = 62, Location = 204 }, // Hattrem on Slippery Slope (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 584, Level = 65, Locations = new[] {208, 210}, }, // Vanilluxe in Frostpoint Field, in the Giant’s Bed (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 126, Level = 62, Locations = new[] {204, 210}, }, // Magmar on Slippery Slope, in the Giant’s Bed (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 143, Level = 65, Locations = new[] {204, 208}, }, // Snorlax on Slippery Slope, in Frostpoint Field (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 861, Level = 65, Locations = new[] {204, 210}, }, // Grimmsnarl on Slippery Slope, in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 709, Level = 63, Location = 212 }, // Trevenant in the Old Cemetery (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 437, Level = 63, Locations = new[] {210, 214} }, // Bronzong in the Giant’s Bed, on Snowslide Slope (in the Crown Tundra)
            new(SWSH) { Species = 470, Level = 63, Location = 210 }, // Leafeon in the Giant’s Bed (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 034, Level = 65, Locations = new[] {208, 210}, }, // Nidoking in Frostpoint Field, in the Giant’s Bed (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 030, Level = 63, Locations = new[] {208, 210}, }, // Nidorina in Frostpoint Field, in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 033, Level = 63, Location = 210 }, // Nidorino in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 534, Level = 65, Location = 210 }, // Conkeldurr in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 874, Level = 63, Location = 210 }, // Stonjourner in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 820, Level = 65, Location = 210 }, // Greedent in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 031, Level = 65, Location = 210 }, // Nidoqueen in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 862, Level = 65, Location = 210 }, // Obstagoon in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 609, Level = 65, Location = 210 }, // Chandelure in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 752, Level = 65, Location = 210 }, // Araquanid in the Giant’s Bed (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 334, Level = 65, Locations = new[] {210, 222, 226, 230}, }, // Altaria in the Giant’s Bed, at the Giant’s Foot, at the Frigid Sea, at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 134, Level = 63, Location = 210 }, // Vaporeon in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 596, Level = 63, Location = 210 }, // Galvantula in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 466, Level = 65, Location = 210 }, // Electivire in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 135, Level = 63, Location = 210 }, // Jolteon in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 125, Level = 63, Location = 210 }, // Electabuzz in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 467, Level = 63, Location = 210 }, // Magmortar in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 631, Level = 63, Location = 210 }, // Heatmor in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 632, Level = 63, Location = 210 }, // Durant in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 136, Level = 63, Location = 210 }, // Flareon in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 197, Level = 63, Location = 210 }, // Umbreon in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 196, Level = 63, Location = 210 }, // Espeon in the Giant’s Bed (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 478, Level = 65, Locations = new[] {210, 212, 216}, }, // Froslass in the Giant’s Bed, in the Old Cemetery, in the Tunnel to the Top (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 362, Level = 65, Locations = new[] {210, 214}, }, // Glalie in the Giant’s Bed (in the Crown Tundra), on Snowslide Slope (in the Crown Tundra)
            new(SWSH) { Species = 359, Level = 65, Location = 210 }, // Absol in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 471, Level = 63, Location = 210 }, // Glaceon in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 700, Level = 63, Location = 210 }, // Sylveon in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 036, Level = 63, Location = 210 }, // Clefable in the Giant’s Bed (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 855, Level = 63, Locations = new[] {210, 212}, }, // Polteageist in the Giant’s Bed, in the Old Cemetery (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 887, Level = 65, Locations = new[] {210, 212 }, }, // Dragapult in the Giant’s Bed (in the Crown Tundra), in the Old Cemetery (in the Crown Tundra)
            new(SWSH) { Species = 872, Level = 62, Location = 214 }, // Snom on Snowslide Slope (in the Crown Tundra)
            new(SWSH) { Species = 698, Level = 62, Location = 214 }, // Amaura on Snowslide Slope (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 621, Level = 65, Locations = new[] {214, 216, 218}, }, // Druddigon on Snowslide Slope, in the Tunnel to the Top, on the Path to the Peak (in the Crown Tundra)
            new(SWSH) { Species = 832, Level = 65, Location = 214 }, // Dubwool on Snowslide Slope (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 375, Level = 63, Locations = new[] {214, 216 } }, // Metang on Snowslide Slope, in the Tunnel to the Top (in the Crown Tundra)
            new(SWSH) { Species = 699, Level = 65, Location = 214 }, // Aurorus on Snowslide Slope (in the Crown Tundra)
            new(SWSH) { Species = 376, Level = 68, Location = 214 }, // Metagross on Snowslide Slope (in the Crown Tundra)
            new(SWSH) { Species = 461, Level = 65, Location = 214 }, // Weavile on Snowslide Slope (in the Crown Tundra)
            new(SWSH) { Species = 709, Level = 65, Location = 214 }, // Trevenant on Snowslide Slope (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 126, Level = 65, Locations = new[] {214, 230}, }, // Magmar on Snowslide Slope, at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 467, Level = 67, Location = 214 }, // Magmortar on Snowslide Slope (in the Crown Tundra)
            new(SWSH) { Species = 362, Level = 67, Location = 214 }, // Glalie on Snowslide Slope (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 615, Level = 65, Locations = new[] {214, 222, 230}, }, // Cryogonal on Snowslide Slope, at the Giant’s Foot, at Ballimere Lake (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 614, Level = 67, Locations = new[] {214, 226, 228}, }, // Beartic on Snowslide Slope, at the Frigid Sea, in Three-Point Pass (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 584, Level = 67, Locations = new[] {214, 230}, }, // Vanilluxe on Snowslide Slope, at Ballimere Lake (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 478, Level = 65, Locations = new[] {214, 216}, }, // Froslass on Snowslide Slope, in the Tunnel to the Top (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 359, Level = 67, Locations = new[] {214, 218, 222}, }, // Absol on Snowslide Slope, on the Path to the Peak, at the Giant’s Foot (in the Crown Tundra)
            new(SW  ) { Species = 555, Level = 67, Location = 214, Form = 02 }, // Darmanitan-2 on Snowslide Slope (in the Crown Tundra)
            new(SWSH) { Species = 861, Level = 67, Location = 214 }, // Grimmsnarl on Snowslide Slope (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 778, Level = 65, Locations = new[] {214, 222, 230}, }, // Mimikyu on Snowslide Slope, at the Giant’s Foot, at Ballimere Lake (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 036, Level = 65, Locations = new[] {214, 216}, }, // Clefable on Snowslide Slope, in the Tunnel to the Top (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 041, Level = 63, Locations = new[] {216, 224}, }, // Zubat in the Tunnel to the Top, in Roaring-Sea Caves (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 042, Level = 65, Locations = new[] {216, 224}, }, // Golbat in the Tunnel to the Top, in Roaring-Sea Caves (in the Crown Tundra)
            new(SW  ) { Species = 371, Level = 65, Location = 216 }, // Bagon in the Tunnel to the Top (in the Crown Tundra)
            new(  SH) { Species = 443, Level = 65, Location = 216 }, // Gible in the Tunnel to the Top (in the Crown Tundra)
            new EncounterStatic8S(SW  ) { Species = 373, Level = 68, Locations = new[] {216, 218} }, // Salamence in the Tunnel to the Top, on the Path to the Peak (in the Crown Tundra)
            new EncounterStatic8S(  SH) { Species = 445, Level = 68, Locations = new[] {216, 218} }, // Garchomp in the Tunnel to the Top, on the Path to the Peak (in the Crown Tundra)
            new(SWSH) { Species = 703, Level = 65, Location = 216 }, // Carbink in the Tunnel to the Top (in the Crown Tundra)
            new(SWSH) { Species = 873, Level = 65, Location = 218 }, // Frosmoth on the Path to the Peak (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 334, Level = 65, Locations = new[] {218, 222}, }, // Altaria on the Path to the Peak, at the Giant’s Foot (in the Crown Tundra)
            new(SWSH) { Species = 851, Level = 67, Location = 222 }, // Centiskorch at the Giant’s Foot (in the Crown Tundra)
            new(SWSH) { Species = 879, Level = 67, Location = 222 }, // Copperajah at the Giant’s Foot (in the Crown Tundra)
            new(SWSH) { Species = 534, Level = 67, Location = 222 }, // Conkeldurr at the Giant’s Foot (in the Crown Tundra)
            new(SWSH) { Species = 566, Level = 63, Location = 222 }, // Archen at the Giant’s Foot (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 344, Level = 65, Locations = new[] {210, 222 } }, // Claydol in the Giant’s Bed, at the Giant’s Foot (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 437, Level = 65, Locations = new[] {208, 222 } }, // Bronzong at the Giant’s Foot, in Frostpoint Field (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 752, Level = 67, Locations = new[] {222, 230}, }, // Araquanid at Ballimere Lake, at the Giant’s Foot (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 125, Level = 65, Locations = new[] {222, 230}, }, // Electabuzz at the Giant’s Foot, at Ballimere Lake (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 466, Level = 68, Locations = new[] {226, 228, 230}, }, // Electivire at the Frigid Sea, in Three-Point Pass, at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 126, Level = 65, Location = 222 }, // Magmar at the Giant’s Foot (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 467, Level = 68, Locations = new[] {226, 230}, }, // Magmortar at the Frigid Sea, at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 567, Level = 67, Location = -01 }, // Archeops
            new(SW  ) { Species = 635, Level = 68, Location = 224 }, // Hydreigon in Roaring-Sea Caves (in the Crown Tundra)
            new(  SH) { Species = 248, Level = 68, Location = 224 }, // Tyranitar in Roaring-Sea Caves (in the Crown Tundra)
            new(SWSH) { Species = 448, Level = 67, Location = 224 }, // Lucario in Roaring-Sea Caves (in the Crown Tundra)
            new(SWSH) { Species = 363, Level = 63, Location = 226 }, // Spheal at the Frigid Sea (in the Crown Tundra)
            new(SWSH) { Species = 364, Level = 65, Location = 226 }, // Sealeo at the Frigid Sea (in the Crown Tundra)
            new(SWSH) { Species = 564, Level = 63, Location = 226 }, // Tirtouga at the Frigid Sea (in the Crown Tundra)
            new(SWSH) { Species = 713, Level = 65, Location = 226 }, // Avalugg at the Frigid Sea (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 858, Level = 67, Locations = new[] {226, 230}, }, // Hatterene at the Frigid Sea, at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 365, Level = 68, Location = 226 }, // Walrein at the Frigid Sea (in the Crown Tundra)
            new(SWSH) { Species = 565, Level = 67, Location = 226 }, // Carracosta at the Frigid Sea (in the Crown Tundra)
            new(SWSH) { Species = 871, Level = 65, Location = 226 }, // Pincurchin at the Frigid Sea (in the Crown Tundra)
            new(  SH) { Species = 875, Level = 65, Location = 226 }, // Eiscue at the Frigid Sea (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 623, Level = 65, Locations = new[] {226, 228} }, // Golurk at the Frigid Sea, in Three-Point Pass (in the Crown Tundra)
            new(SWSH) { Species = 887, Level = 68, Location = 228 }, // Dragapult in Three-Point Pass (in the Crown Tundra)
            new(  SH) { Species = 141, Level = 68, Location = 224 }, // Kabutops in Roaring-Sea Caves (in the Crown Tundra)
            new(SW  ) { Species = 139, Level = 68, Location = 224 }, // Omastar in Roaring-Sea Caves (in the Crown Tundra)
            new(SWSH) { Species = 823, Level = 68, Location = 230 }, // Corviknight at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 862, Level = 68, Location = 230 }, // Obstagoon at Ballimere Lake (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 715, Level = 67, Locations = new[] {230, 232}, }, // Noivern at Ballimere Lake, in Lakeside Cave (in the Crown Tundra)
            new(SWSH) { Species = 547, Level = 65, Location = 230 }, // Whimsicott at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 836, Level = 67, Location = 230 }, // Boltund at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 830, Level = 65, Location = 230 }, // Eldegoss at Ballimere Lake (in the Crown Tundra)
            new(SW  ) { Species = 876, Level = 65, Location = 230 }, // Indeedee at Ballimere Lake (in the Crown Tundra)
            new(  SH) { Species = 876, Level = 65, Location = 230, Form = 01 }, // Indeedee-1 at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 696, Level = 63, Location = 230 }, // Tyrunt at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 213, Level = 65, Location = 230 }, // Shuckle at Ballimere Lake (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 820, Level = 68, Locations = new[] {230, 234}, }, // Greedent at Ballimere Lake, at Dyna Tree Hill (in the Crown Tundra)
            new(SWSH) { Species = 877, Level = 65, Location = 230 }, // Morpeko at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 596, Level = 67, Location = 230 }, // Galvantula at Ballimere Lake (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 839, Level = 68, Locations = new[] {230, 232}, }, // Coalossal at Ballimere Lake, in Lakeside Cave (in the Crown Tundra)
            new(SWSH) { Species = 697, Level = 69, Location = 230 }, // Tyrantrum at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 531, Level = 65, Location = 230 }, // Audino at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 304, Level = 63, Location = 230 }, // Aron at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 149, Level = 70, Location = 230 }, // Dragonite at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 306, Level = 68, Location = 232 }, // Aggron in Lakeside Cave (in the Crown Tundra)
            new(SWSH) { Species = 598, Level = 67, Location = 232 }, // Ferrothorn in Lakeside Cave (in the Crown Tundra)
            new(SWSH) { Species = 305, Level = 63, Location = 232 }, // Lairon in Lakeside Cave (in the Crown Tundra)
            new(SWSH) { Species = 348, Level = 67, Location = 230 }, // Armaldo at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 340, Level = 65, Location = 210 }, // Whiscash in the Giant’s Bed (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 130, Level = 67, Locations = new[] {210, 230 } }, // Gyarados in the Giant’s Bed, at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 350, Level = 67, Location = 210 }, // Milotic in the Giant’s Bed (in the Crown Tundra)
            new(  SH) { Species = 140, Level = 63, Location = 222 }, // Kabuto at the Giant’s Foot (in the Crown Tundra)
            new(SW  ) { Species = 138, Level = 63, Location = 222 }, // Omanyte at the Giant’s Foot (in the Crown Tundra)
            new(SWSH) { Species = 347, Level = 63, Location = 230 }, // Anorith at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 369, Level = 65, Location = 230 }, // Relicanth at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 147, Level = 63, Location = 230 }, // Dratini at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 148, Level = 65, Location = 230 }, // Dragonair at Ballimere Lake (in the Crown Tundra)
            new(  SH) { Species = 078, Level = 67, Location = 212, Form = 01 }, // Rapidash-1 in the Old Cemetery (in the Crown Tundra)
            new(SWSH) { Species = 478, Level = 63, Location = 204 }, // Froslass on Slippery Slope (in the Crown Tundra)
            new(SWSH) { Species = 362, Level = 63, Location = 204 }, // Glalie on Slippery Slope (in the Crown Tundra)
            new(SWSH) { Species = 467, Level = 65, Location = 204 }, // Magmortar on Slippery Slope (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 029, Level = 60, Locations = new[] {208, 210}, }, // Nidoran♀ in Frostpoint Field, in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 032, Level = 60, Location = 208 }, // Nidoran♂ in Frostpoint Field (in the Crown Tundra)
            new EncounterStatic8S(SWSH) { Species = 531, Level = 62, Locations = new[] {204, 208, 210, 222, 230}, }, // Audino on Slippery Slope, in Frostpoint Field, in the Giant’s Bed at the Giant’s Foot, at Ballimere Lake (in the Crown Tundra)
            new(SWSH) { Species = 359, Level = 62, Location = 208 }, // Absol in Frostpoint Field (in the Crown Tundra)
            new(SWSH) { Species = 142, Level = 65, Location = 210 }, // Aerodactyl in the Giant’s Bed (in the Crown Tundra)
            new(SWSH) { Species = 442, Level = 72, Location = 230, FlawlessIVCount = 3, Ability = 4 }, // Spiritomb at Ballimere Lake (in the Crown Tundra)
            #endregion
        };

        private const string tradeSWSH = "tradeswsh";
        private static readonly string[][] TradeSWSH = Util.GetLanguageStrings10(tradeSWSH, "zh2");
        private static readonly string[] TradeOT_R1 = { string.Empty, "チホコ", "Regina", "Régiona", "Regionalia", "Regine", string.Empty, "Tatiana", "지민", "易蒂", "易蒂" };
        private static readonly int[] TradeIVs = {15, 15, 15, 15, 15, 15};

        private static readonly EncounterTrade8[] TradeGift_Regular =
        {
            new(SWSH, 052,18,08,000,04,5) { Ability = 2, TID7 = 263455, IVs = TradeIVs, DynamaxLevel = 1, OTGender = 0, Gender = 0, Nature = Nature.Timid, Relearn = new[] {387,000,000,000}   }, // Meowth
            new(SWSH, 819,10,01,044,01,2) { Ability = 1, TID7 = 648753, IVs = TradeIVs, DynamaxLevel = 1, OTGender = 1, Gender = 0, Nature = Nature.Mild,                                      }, // Skwovet
            new(SWSH, 546,23,11,000,09,5) { Ability = 1, TID7 = 101154, IVs = TradeIVs, DynamaxLevel = 1, OTGender = 1, Gender = 1, Nature = Nature.Modest,                                    }, // Cottonee
            new(SWSH, 175,25,02,010,10,6) { Ability = 2, TID7 = 109591, IVs = TradeIVs, DynamaxLevel = 1, OTGender = 1, Gender = 0, Nature = Nature.Timid, Relearn = new[] {791,000,000,000}   }, // Togepi
            new(SW  , 856,30,09,859,08,3) { Ability = 2, TID7 = 101101, IVs = TradeIVs, DynamaxLevel = 1, OTGender = 0, Gender = 1, Nature = Nature.Quiet,                                     }, // Hatenna
            new(  SH, 859,30,43,000,07,6) { Ability = 1, TID7 = 256081, IVs = TradeIVs, DynamaxLevel = 1, OTGender = 0, Gender = 0, Nature = Nature.Brave, Relearn = new[] {252,000,000,000}   }, // Impidimp
            new(SWSH, 562,35,16,310,15,5) { Ability = 1, TID7 = 102534, IVs = TradeIVs, DynamaxLevel = 2, OTGender = 1, Gender = 0, Nature = Nature.Bold, Relearn = new[] {261,000,000,000}    }, // Yamask
            new(SW  , 538,37,17,129,20,7) { Ability = 2, TID7 = 768945, IVs = TradeIVs, DynamaxLevel = 2, OTGender = 0, Gender = 0, Nature = Nature.Adamant,                                   }, // Throh
            new(  SH, 539,37,17,129,14,6) { Ability = 1, TID7 = 881426, IVs = TradeIVs, DynamaxLevel = 2, OTGender = 0, Gender = 0, Nature = Nature.Adamant,                                   }, // Sawk
            new(SWSH, 122,40,56,000,12,4) { Ability = 1, TID7 = 891846, IVs = TradeIVs, DynamaxLevel = 1, OTGender = 0, Gender = 0, Nature = Nature.Calm,                                      }, // Mr. Mime
            new(SWSH, 884,50,15,038,06,2) { Ability = 2, TID7 = 101141, IVs = TradeIVs, DynamaxLevel = 3, OTGender = 0, Gender = 0, Nature = Nature.Adamant, Relearn = new[] {400,000,000,000} }, // Duraludon
        };

        private static readonly EncounterTrade8[] TradeGift_R1 =
        {
            new(SWSH, 052,15,01,033,04,2) { Ability = 4, TID7 = 101141, FlawlessIVCount = 3, DynamaxLevel = 5, OTGender = 1, Shiny = Shiny.Random, IsNicknamed = false, Relearn = new[] {387,000,000,000}               }, // Meowth
            new(SW  , 083,15,01,013,10,2) { Ability = 4, TID7 = 101141, FlawlessIVCount = 3, DynamaxLevel = 5, OTGender = 1, Shiny = Shiny.Random, IsNicknamed = false, Relearn = new[] {098,000,000,000}               }, // Farfetch’d
            new(  SH, 222,15,01,069,12,2) { Ability = 4, TID7 = 101141, FlawlessIVCount = 3, DynamaxLevel = 5, OTGender = 1, Shiny = Shiny.Random, IsNicknamed = false, Relearn = new[] {457,000,000,000}               }, // Corsola
            new(  SH, 077,15,01,047,06,2) { Ability = 4, TID7 = 101141, FlawlessIVCount = 3, DynamaxLevel = 5, OTGender = 1, Shiny = Shiny.Random, IsNicknamed = false, Relearn = new[] {234,000,000,000}               }, // Ponyta
            new(SWSH, 122,15,01,005,04,2) { Ability = 4, TID7 = 101141, FlawlessIVCount = 3, DynamaxLevel = 5, OTGender = 1, Shiny = Shiny.Random, IsNicknamed = false, Relearn = new[] {252,000,000,000}               }, // Mr. Mime
            new(SW  , 554,15,01,040,12,2) { Ability = 4, TID7 = 101141, FlawlessIVCount = 3, DynamaxLevel = 5, OTGender = 1, Shiny = Shiny.Random, IsNicknamed = false, Relearn = new[] {326,000,000,000}               }, // Darumaka
            new(SWSH, 263,15,01,045,04,2) { Ability = 4, TID7 = 101141, FlawlessIVCount = 3, DynamaxLevel = 5, OTGender = 1, Shiny = Shiny.Random, IsNicknamed = false, Relearn = new[] {245,000,000,000}               }, // Zigzagoon
            new(SWSH, 618,15,01,050,05,2) { Ability = 4, TID7 = 101141, FlawlessIVCount = 3, DynamaxLevel = 5, OTGender = 1, Shiny = Shiny.Random, IsNicknamed = false, Relearn = new[] {281,000,000,000}               }, // Stunfisk
            new(SWSH, 110,15,01,040,12,2) { Ability =-1, TID7 = 101141, FlawlessIVCount = 3, DynamaxLevel = 5, OTGender = 1, Shiny = Shiny.Random, IsNicknamed = false, Relearn = new[] {220,000,000,000}               }, // Weezing
            new(SWSH, 103,15,01,038,06,2) {              TID7 = 101141, FlawlessIVCount = 3, DynamaxLevel = 5, OTGender = 1, Shiny = Shiny.Random, IsNicknamed = false, Relearn = new[] {246,000,000,000}, Form = 1     }, // Exeggutor-1
            new(SWSH, 105,15,01,038,06,2) {              TID7 = 101141, FlawlessIVCount = 3, DynamaxLevel = 5, OTGender = 1, Shiny = Shiny.Random, IsNicknamed = false, Relearn = new[] {174,000,000,000}, Form = 1     }, // Marowak-1
        };

        internal static readonly EncounterTrade8[] TradeGift_SWSH = TradeGift_Regular.Concat(TradeGift_R1).ToArray();

        internal static readonly EncounterStatic[] StaticSW = ArrayUtil.ConcatAll<EncounterStatic>(Nest_Common, Nest_SW, Nest_SH, Dist_Common, Dist_SW, Dist_SH, GetEncounters(Crystal_SWSH, SW), DynAdv_SWSH, GetEncounters(Encounter_SWSH, SW));
        internal static readonly EncounterStatic[] StaticSH = ArrayUtil.ConcatAll<EncounterStatic>(Nest_Common, Nest_SW, Nest_SH, Dist_Common, Dist_SW, Dist_SH, GetEncounters(Crystal_SWSH, SH), DynAdv_SWSH, GetEncounters(Encounter_SWSH, SH));
    }
}
