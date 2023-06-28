using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Xna.Framework;
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
            this.Monitor.Log("IN FUNKTION", LogLevel.Debug);
            // Check if the player has a Preserve Jar (Preservers Jar) in their inventory
            bool hasPreserveJar = false;
            bool hasKit = false;
            foreach (Item item in Game1.player.items)
            {
                if (item is StardewValley.Object obj && obj.name == "Preserves Jar")
                {
                    this.Monitor.Log("Hat JAR", LogLevel.Debug);
                    hasPreserveJar = true;
                    break;

                }
            }
            foreach (Item item in Game1.player.items)
            {
                if (item is StardewValley.Object obj && obj.name == "Stone")
                {
                    this.Monitor.Log("Hat Stone", LogLevel.Debug);
                    hasKit = true;
                    break;

                }
            }

            // Use the result (hasPreserveJar) for further processing
            if (hasPreserveJar && hasKit)
            {
                if (e.Button == SButton.MouseLeft && Game1.player.CursorSlotItem is StardewValley.Object obj && obj.name == "stone")
                {
                    // Player has a Preserve Jar (Preservers Jar) in their inventory
                    for (int i = 0; i < Game1.player.items.Count; i++)
                    {
                        if (Game1.player.items[i] is StardewValley.Object obj1 && obj1.name == "Preserves Jar")
                        {
                            // Remove the Preserve Jar from the player's inventory
                            Game1.player.items.RemoveAt(i);

                            Game1.player.addItemByMenuIfNecessary(new StardewValley.Object(Vector2.Zero, 12));

                            Game1.playSound("axe");
                            Game1.drawObjectDialogue("Your Preserve Jar has been transformed into a Keg!");
                            break;
                        }
                    }

                }
                else
                {
                    this.Monitor.Log("no Jar", LogLevel.Debug);
                    // Player does not have a Preserve Jar (Preservers Jar) in their inventory
                }
            }
        }




    }
}
