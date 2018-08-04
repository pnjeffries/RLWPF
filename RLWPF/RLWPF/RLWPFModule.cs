using Nucleus.Game;
using Nucleus.Geometry;
using Nucleus.Model;
using Nucleus.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLWPF
{
    public class RLWPFModule : GameModule
    {
        public override GameState StartingState()
        {
            // Initialise state, map and stage:
            int mapX = 18;
            int mapY = 16;
            var state = new RLState();
            var stage = new MapStage();
            var map = new SquareCellMap<MapCell>(mapX,mapY);
            stage.Map = map;
            stage.Map.InitialiseCells();
            state.Stage = stage;

            // Create map:
            for (int i = 0; i < mapX; i++)
            {
                for (int j = 0; j < mapY; j++)
                {
                    if (!(i > 1 && i < 8 && j > 1 && j < 6) &&
                        !(i == 4 && j > 5 && j < 12) &&
                        !(i > 4 && i < 14 && j == 11) &&
                        !(i > 11 && i < 17 && j > 7 && j < 15))
                    {
                        var wall = new PointElement("Wall");
                        wall.SetData(new ASCIIStyle("█"), new MapCellCollider(), new VisionBlocker());
                        map[i, j].PlaceInCell(wall);
                    }
                }
            }

            // Create player character
            var hero = new PointElement("Hero");
            hero.SetData(new ASCIIStyle("@"), new MapCellCollider(), new MapAwareness(10));
            map[3,5].PlaceInCell(hero);
            state.Controlled = hero;

            // Create stairs
            var stairs = new PointElement("Stairs");
            stairs.SetData(new ASCIIStyle(">"));//, new MapCellCollider());
            map[15, 12].PlaceInCell(stairs);

            // Create door
            var door = new PointElement("Door");
            door.SetData(new ASCIIStyle("+"), new VisionBlocker());
            map[4, 6].PlaceInCell(door);

            // Create test enemy
            var enemy = new PointElement("Enemy");
            enemy.SetData(new ASCIIStyle("▄"));//, new MapCellCollider());
            map[4, 3].PlaceInCell(enemy);

            var enemy2 = new PointElement("Enemy");
            enemy2.SetData(new ASCIIStyle("s"), new MapCellCollider());
            map[13, 9].PlaceInCell(enemy2);

            return state;
        }
    }
}
