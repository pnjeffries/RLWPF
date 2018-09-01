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
            int mapX = 24;
            int mapY = 18;
            var state = new RLState();
            var stage = new MapStage();
            var map = new SquareCellMap<MapCell>(mapX,mapY);
            stage.Map = map;
            stage.Map.InitialiseCells();
            state.Stage = stage;

            // Room templates:
            var stdRoom = new RoomTemplate();
            var largeRoom = new RoomTemplate(RoomType.Room, 3, 6, 4, 6);
            var corridor = new RoomTemplate(RoomType.Circulation, 1, 1, 4, 8);

            // Create map:
            var generator = new DungeonArtitect(mapX, mapY);
            generator.Templates.Add(stdRoom);
            generator.Templates.Add(largeRoom);
            generator.Templates.Add(corridor);
            generator.Generate(10, 14, stdRoom);

            var blueprint = generator.Blueprint;

            // Build dungeon from blueprint:
            Random rng = new Random();
            for (int i = 0; i < mapX; i++)
            {
                for (int j = 0; j < mapY; j++)
                {
                    if (blueprint[i,j].IsWall())
                    {
                        var wall = new PointElement("Wall");
                        wall.SetData(new ASCIIStyle("█"), new MapCellCollider(),
                            new VisionBlocker(), new Memorable());
                        map[i, j].PlaceInCell(wall);
                    }
                    else if (blueprint[i,j] == CellGenerationType.Door && rng.NextDouble() > 0.5)
                    {
                        // Create door
                        var door = new PointElement("Door");
                        door.SetData(new ASCIIStyle("+"), new VisionBlocker(), new Memorable());
                        map[i, j].PlaceInCell(door);
                    }
                }
            }

            /*for (int i = 0; i < mapX; i++)
            {
                for (int j = 0; j < mapY; j++)
                {
                    if (!(i > 1 && i < 8 && j > 1 && j < 6) &&
                        !(i == 4 && j > 5 && j < 12) &&
                        !(i > 4 && i < 14 && j == 11) &&
                        !(i > 11 && i < 17 && j > 7 && j < 15))
                    {
                        var wall = new PointElement("Wall");
                        wall.SetData(new ASCIIStyle("█"), new MapCellCollider(), 
                            new VisionBlocker(), new Memorable());
                        map[i, j].PlaceInCell(wall);
                    }
                }
            }

            for (int i = 12; i < 17; i++)
            {
                for (int j = 8; j < 11; j++)
                {
                    var water = new PointElement("Water");
                    water.SetData(new ASCIIStyle("~"));
                    map[i, j].PlaceInCell(water);
                }
            }*/

            // Create stairs
            var stairs = new PointElement("Stairs");
            stairs.SetData(new ASCIIStyle("<"), new Memorable());//, new MapCellCollider());
            map[10, 13].PlaceInCell(stairs);

            // Create player character
            var hero = new PointElement("Hero");
            hero.SetData(new ASCIIStyle("@"), new MapCellCollider(), 
                new MapAwareness(10), new Memorable(), 
                new AvailableActions(), new TurnCounter(),
                new WaitAbility(), new MoveCellAbility());
            map[10,13].PlaceInCell(hero);
            state.Elements.Add(hero);
            state.Controlled = hero;

                    

            // Create test enemy
            var enemy = new PointElement("Enemy");
            enemy.SetData(new ASCIIStyle("M"), new VisionBlocker());//, new MapCellCollider());
            //map[4, 3].PlaceInCell(enemy);

            var enemy2 = new PointElement("Enemy");
            enemy2.SetData(new ASCIIStyle("s"), new MapCellCollider());
            //map[13, 9].PlaceInCell(enemy2);

            return state;
        }
    }
}
