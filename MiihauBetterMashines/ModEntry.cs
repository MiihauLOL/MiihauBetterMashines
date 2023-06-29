using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Objects;
using StardewValley.Tools;
using xTile.Dimensions;

namespace MiihauBetterMashines
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }

        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;


            if (e.Button == SButton.MouseRight && Game1.player.CursorSlotItem is StardewValley.Object obj && obj.name == "preserves_jar_upgrade_kit")
            {
                Upgrader("Preserves Jar", "better_preserves_jar");
            }
        }
        private void Upgrader(String startItem, String endItem)
        {
            Int32 endItemID = GetParentSheetIndexByName(endItem);
            bool hasNeededItem = false;
            foreach (Item item in Game1.player.items)
            {
                if (item is StardewValley.Object obj && obj.name == startItem)
                {
                    hasNeededItem = true;
                    break;
                }
                
            }
            
            // Use the result (hasPreserveJar) for further processing
            if (hasNeededItem)
            {

                for (int i = 0; i < Game1.player.items.Count; i++)
                {
                    if (Game1.player.items[i] is StardewValley.Object obj1 && obj1.name == startItem)
                    {
                        // Remove the Preserve Jar from the player's inventory
                        if (obj1.Stack > 1)
                        {
                            obj1.Stack--;
                        }
                        else
                        {
                            Game1.player.items.RemoveAt(i);
                        }
                        Game1.player.addItemByMenuIfNecessary(new StardewValley.Object(Vector2.Zero, endItemID));

                        Game1.playSound("axe");
                        //Game1.drawObjectDialogue("Your " + startItem + " upgraded!");
                        if (Game1.player.CursorSlotItem.Stack > 1)
                        {
                            Game1.player.CursorSlotItem.Stack--;
                        }
                        else
                        {
                            Game1.player.CursorSlotItem = null;
                        }
                        break;
                    }

                }
                /*for (int i = 0; i < Game1.player.items.Count; i++)
                {
                    if (Game1.player.items[i] is StardewValley.Object obj1 && obj1.name == kitName)
                    {
                        if (obj1.Stack > 1)
                        {
                            obj1.Stack--;
                        }
                        else
                        {
                            Game1.player.items.RemoveAt(i);
                        }
                        break;
                    }
                }*/
            }
        }
        private int GetParentSheetIndexByName(string itemName)
        {
            foreach (var kvp in Game1.bigCraftablesInformation)
            {
                if (kvp.Value.Split('/')[0].Equals(itemName))
                {
                    //Monitor.Log("key is: " + kvp.Key, LogLevel.Debug);
                    return Convert.ToInt32(kvp.Key); 
                }
            }
            //Monitor.Log("key was not found :(", LogLevel.Debug);
            return -1; // Return -1 if the item name was not found
        }
    }
}

