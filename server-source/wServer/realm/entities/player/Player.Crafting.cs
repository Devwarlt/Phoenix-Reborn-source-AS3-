﻿using System;
using System.Collections.Generic;
using System.Linq;
using db;

namespace wServer.realm.entities
{
    partial class Player
    {
        public Dictionary<List<string>, Item> Recipes = new Dictionary<List<string>, Item>();
        public Dictionary<Tuple<string, string>, Item[]> Reforges = new Dictionary<Tuple<string, string>, Item[]>();
        public Dictionary<Tuple<int, string>, Item[]> SlotTypeReforges = new Dictionary<Tuple<int, string>, Item[]>();

        public void AddRecipes()
        {
            /* Item Forge - Materials Only */
            // AddRecipe("Kitty", "Staff of Someone", "Staff of Someone", "Robe of Someone", "Gauntlet Key");
            AddRecipe("Amulet Core", "Madness Essence", "Crystal Essence", "Unknown Essence", "Ring of Minor Protection");
            AddRecipe("Amulet of Resurrection", "Amulet Core", "Amulet Strap");

            /* Reforge Station - Item, Material (2 slot only) */
            // AddReforge("Ring of Minor Protection", "Amulet Core", "Amulet of Resurrection");

            /* Reforge Station (Slot type input) - Item type, Material, Result(s) (2 slot only) */
            #region Weapons
            #region Swords
            AddSlotTypeReforge("Short Sword", "Stone Essence", "Ancient Stone Sword");
            AddSlotTypeReforge("Short Sword", "Crystal Essence", "Crystal Sword");
            AddSlotTypeReforge("Short Sword", "Demon Essence", "Demon Blade");
            #endregion Swords

            #region Daggers
            AddSlotTypeReforge("Steel Dagger", "Unknown Essence", "Dirk of Cronus", "Spirit Dagger", "Chicken Leg of Doom", "Bone Dagger");
            #endregion Daggers

            #region Bows
            AddSlotTypeReforge("Shortbow", "Coral Essence", "Coral Bow");
            AddSlotTypeReforge("Shortbow", "Doom Essence", "Doom Bow");
            #endregion Bows

            #region Staffs
            AddSlotTypeReforge("Energy Staff", "Sprite Essence", "Staff of Extreme Prejudice");
            AddSlotTypeReforge("Energy Staff", "Unknown Essence", "Anatis Staff");
            #endregion Staffs

            #region Wands
            AddSlotTypeReforge("Fire Wand", "Snake Essence", "Wand of the Bulwark");
            AddSlotTypeReforge("Fire Wand", "Blood Essence", "St. Abraham's Wand");
            AddSlotTypeReforge("Fire Wand", "Madness Essence", "Conducting Wand");
            AddSlotTypeReforge("Fire Wand", "Crystal Essence", "Crystal Wand");
            AddSlotTypeReforge("Fire Wand", "Sprite Essence", "Sprite Wand");
            #endregion Wands

            #region Katanas
            AddSlotTypeReforge("Rusty Katana", "Unknown Essence", "Ray Katana", "Celestial Blade");
            #endregion Katanas
            #endregion Weapons

            #region Abilities
            #region Spells
            AddSlotTypeReforge("Novice's Spell", "Unknown Essence", "Penetrating Blast Spell");
            #endregion Spells

            #region Shields
            AddSlotTypeReforge("Wooden Shield", "Snake Essence", "Snake Skin Shield");
            AddSlotTypeReforge("Wooden Shield", "Lost Essence", "Shield of Ogmur");
            #endregion Shields

            #region Cloaks
            AddSlotTypeReforge("Cloak of Shadows", "Sprite Essence", "Cloak of the Planewalker");
            #endregion Cloaks

            #region Traps
            AddSlotTypeReforge("Hunting Trap", "Coral Essence", "Coral Venom Trap");
            #endregion Traps

            #region Quivers
            AddSlotTypeReforge("Magic Quiver", "Sprite Essence", "Quiver of Thunder");
            #endregion Quivers

            #region Seals
            AddSlotTypeReforge("Seal of the Initiate", "Unknown Essence", "Seal of Blasphemous Prayer");
            #endregion Seals

            #region Prisms
            AddSlotTypeReforge("Decoy Prism", "Unknown Essence", "Ghostly Prism");
            AddSlotTypeReforge("Decoy Prism", "Crystal Essence", "Prism of Dancing Swords");
            #endregion Prisms

            #region Orbs
            AddSlotTypeReforge("Stasis Orb", "Unknown Essence", "Orb of Conflict");
            #endregion Orbs

            #region Scepters
            AddSlotTypeReforge("Lightning Scepter", "Madness Essence", "Scepter of Fulmination");
            #endregion Scepters

            #region Tomes
            AddSlotTypeReforge("Healing Tome", "Ruined Essence", "Tome of Holy Protection");
            AddSlotTypeReforge("Healing Tome", "Blood Essence", "Tome of Purification");
            #endregion Tomes

            #region Helms
            AddSlotTypeReforge("Combat Helm", "Ruined Essence", "Helm of the Juggernaut");
            #endregion Helms

            #region Skulls
            AddSlotTypeReforge("Necrotic Skull", "Demon Essence", "Skull of Endless Torment");
            #endregion Skulls

            #region Poisons
            #endregion Poisons

            #region Stars
            AddSlotTypeReforge("Basic Star", "Unknown Essence", "Midnight Star");
            #endregion Stars
            #endregion Abilities

            #region Armors
            #region Robes
            AddSlotTypeReforge("Robe of the Neophyte", "Madness Essence", "Robe of the Mad Scientist");
            AddSlotTypeReforge("Robe of the Neophyte", "Unknown Essence", "Water Dragon Silk Robe");
            AddSlotTypeReforge("Robe of the Neophyte", "Blood Essence", "Chasuble of Holy Light");
            #endregion Robes

            #region Leather
            AddSlotTypeReforge("Wolfskin Armor", "Unknown Essence", "Spectral Cloth Armor", "Leaf Dragon Hide Armor");
            AddSlotTypeReforge("Wolfskin Armor", "Snake Essence", "Snake Skin Armor");
            AddSlotTypeReforge("Wolfskin Armor", "Coral Essence", "Coral Silk Armor");
            #endregion Leather

            #region Heavy
            AddSlotTypeReforge("Iron Mail", "Unknown Essence", "Fire Dragon Battle Armor");
            #endregion Heavy
            #endregion Armors

            #region Accessories
            AddSlotTypeReforge("Ring of Minor Protection", "Ruined Essence", "Ring of the Sphinx", "Ring of the Nile", "Ring of the Pyramid");
            AddSlotTypeReforge("Ring of Minor Protection", "Blood Essence", "Ring of Divine Faith");
            AddSlotTypeReforge("Ring of Minor Protection", "Madness Essence", "Experimental Ring");
            AddSlotTypeReforge("Ring of Minor Protection", "Snake Essence", "Snake Eye Ring");
            AddSlotTypeReforge("Ring of Minor Protection", "Coral Essence", "Coral Ring");
            #endregion Accessories
        }

        public void AddRecipe(string result, params string[] recipe)
        {
            Recipes.Add(recipe.Select(_ => _.ToLower()).ToList(), Manager.GameData.Items[Manager.GameData.IdToObjectType[result]]);
        }

        public void AddReforge(string item, string material, params string[] result)
        {
            Reforges.Add(Tuple.Create(item.ToLower(), material.ToLower()), result.Select(res => Manager.GameData.Items[Manager.GameData.IdToObjectType[res]]).ToArray());
        }

        public void AddSlotTypeReforge(string item, string material, params string[] result)
        {
            SlotTypeReforges.Add(Tuple.Create(Manager.GameData.Items[Manager.GameData.IdToObjectType[item]].SlotType, material.ToLower()), result.Select(res => Manager.GameData.Items[Manager.GameData.IdToObjectType[res]]).ToArray());
        }

        public Item GetCraftResult(List<Item> items)
        {
            foreach (var r in Recipes)
            {
                List<string> requirements = r.Key.Select(_ => _).ToList();
                bool pass = false;
                foreach (Item i in items)
                {
                    if (requirements.Count == 0)
                    {
                        pass = true;
                        break;
                    }
                    if (requirements.Contains(i.ObjectId.ToLower()))
                        requirements.Remove(i.ObjectId.ToLower());
                }
                if (requirements.Count == 0 && !pass)
                    return r.Value;
            }
            return null;
        }

        public Item GetReforgeResult(Item item, Item material)
        {
            Tuple<string, string> key = Tuple.Create(item.ObjectId.ToLower(), material.ObjectId.ToLower());
            Item[] ret = null;
            Reforges.TryGetValue(key, out ret);
            if (ret == null)
                if ((ret = GetSlotTypeReforgeResults(item.SlotType, material)) == null)
                    return null;
            Random rand = new Random();
            return ret[Random.Next(0, ret.Length)];
        }

        public Item[] GetSlotTypeReforgeResults(int slottype, Item material)
        {
            Tuple<int, string> key = Tuple.Create(slottype, material.ObjectId.ToLower());
            Item[] ret = null;
            SlotTypeReforges.TryGetValue(key, out ret);
            return ret;
        }
    }
}