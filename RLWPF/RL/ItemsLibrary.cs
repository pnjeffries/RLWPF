using Nucleus.Game;
using Nucleus.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL
{
    /// <summary>
    /// Class containing a library of functions to create different items
    /// </summary>
    public class ItemsLibrary
    {
        /// <summary>
        /// Base setup for all weapons
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private GameElement Weapon(string name)
        {
            var result = new GameElement(name);
            result.SetData(
                new EquippableItem(),
                new PickUp());
            return result;
        }


        /// <summary>
        /// A standard sword
        /// </summary>
        /// <returns></returns>
        public GameElement Spear()
        {
            var sword = Weapon("Spear");
            sword.SetData(
                new ASCIIStyle("↑"),
                new PrefabStyle("Sword"),
                new ItemActions(
                    new WindUpAction(
                        new AOEAttackActionFactory(2, 0, 1, 0))));
            return sword;
        }

        /// <summary>
        /// A standard sword
        /// </summary>
        /// <returns></returns>
        public GameElement Sword()
        {
            var sword = Weapon("Sword");
            sword.SetData(
                new ASCIIStyle("↑"),
                new PrefabStyle("Sword"),
                new ItemActions(
                    new WindUpAction(
                        new AOEAttackActionFactory(1, -1, 1, 0, 1, 1))));
            return sword;
        }
    }
}
