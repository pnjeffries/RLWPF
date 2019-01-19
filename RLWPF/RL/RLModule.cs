using Nucleus.Game;
using Nucleus.Geometry;
using Nucleus.Model;
using Nucleus.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RL
{
    public class RLModule : GameModule
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

            // Items library:
            var items = new ItemsLibrary();

            // Room templates:
            var stdRoom = new RoomTemplate();
            var largeRoom = new RoomTemplate(RoomType.Room, 3, 6, 4, 6);
            var corridor = new RoomTemplate(RoomType.Circulation, 1, 1, 4, 8);
            var exit = new RoomTemplate(RoomType.Exit, 1, 1,1,1);

            // Create map:
            var generator = new DungeonArtitect(mapX, mapY);
            generator.Templates.Add(stdRoom);
            generator.Templates.Add(largeRoom);
            generator.Templates.Add(corridor);
            generator.Templates.Add(exit);
            generator.Generate(10, 14, stdRoom, CompassDirection.South);

            var blueprint = generator.Blueprint;


            var playerFaction = new Faction("Player");
            var enemyFaction = new Faction("Enemy");


            // Build dungeon from blueprint:
            Random rng = new Random();
            for (int i = 0; i < mapX; i++)
            {
                for (int j = 0; j < mapY; j++)
                {
                    CellGenerationType cGT = blueprint[i, j].GenerationType;
                    if (cGT.IsWall() || cGT == CellGenerationType.Untouched)
                    {
                        var wall = new GameElement("Wall");
                        wall.SetData(new ASCIIStyle("█"), new PrefabStyle("Wall"), new MapCellCollider(),
                            new VisionBlocker(), new Memorable(), new Inertia(true));
                        map[i, j].PlaceInCell(wall);
                        state.Elements.Add(wall);
                    }
                    else if (cGT == CellGenerationType.Door && rng.NextDouble() > 0.5)
                    {
                        // Create door
                        /*var door = new PointElement("Door");
                        door.SetData(new ASCIIStyle("+"), new VisionBlocker(), new Memorable(), new Inertia(true));
                        map[i, j].PlaceInCell(door);
                        state.Elements.Add(door);*/
                    }
                    else if (cGT == CellGenerationType.Void)
                    {
                        if (rng.NextDouble() < 0.1)
                        {
                            var eSword = items.Sword();

                            // Create enemy
                            var enemy2 = new GameElement("Enemy");
                            enemy2.SetData(enemyFaction, new ASCIIStyle("e"), new PrefabStyle("Meeple"),
                                new MapCellCollider(), new MapAwareness(10), new Memorable(),
                                new HitPoints(3),
                                new AvailableActions(), new TurnCounter(),
                                new WaitAbility(),
                                new MoveCellAbility(),
                                new BumpAttackAbility(),
                                new Equipped(
                                    new EquipmentSlot("1", InputFunction.Ability_1, eSword),
                                    new EquipmentSlot("2", InputFunction.Ability_2),
                                    new EquipmentSlot("3", InputFunction.Ability_3),
                                    new EquipmentSlot("4", InputFunction.Ability_4),
                                    new EquipmentSlot("5", InputFunction.Ability_5),
                                    new EquipmentSlot("6", InputFunction.Ability_6)),
                                new UseItemAbility());
                            map[i, j].PlaceInCell(enemy2);
                            state.Elements.Add(enemy2);
                        }
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
            var stairs = new GameElement("Stairs");
            stairs.SetData(new ASCIIStyle("<"), new Memorable(), new Inertia(true));//, new MapCellCollider());
            map[10, 13].PlaceInCell(stairs);
            //state.Elements.Add(stairs);

            var sword = items.Sword();
            //map[10, 12].PlaceInCell(sword);
            //state.Elements.Add(sword);

            var spear = items.Spear();
            map[10, 12].PlaceInCell(spear);
            state.Elements.Add(spear);

            // Create player character
            var hero = new GameElement("Hero");
            hero.SetData(playerFaction,
                new ASCIIStyle("@"), new PrefabStyle("Meeple2"),
                new MapCellCollider(),
                new MapAwareness(10), new Memorable(),
                new HitPoints(9),
                new Equipped(
                    new EquipmentSlot("1", InputFunction.Ability_1, sword),
                    new EquipmentSlot("2", InputFunction.Ability_2),
                    new EquipmentSlot("3", InputFunction.Ability_3),
                    new EquipmentSlot("4", InputFunction.Ability_4),
                    new EquipmentSlot("5", InputFunction.Ability_5),
                    new EquipmentSlot("6", InputFunction.Ability_6)),
                new AvailableActions(), new TurnCounter(),
                new WaitAbility(), 
                new MoveCellAbility(),
                new BumpAttackAbility(),
                new UseItemAbility(),
                new PickUpAbility());
            map[10,13].PlaceInCell(hero);
            state.Elements.Add(hero);
            state.Controlled = hero;

            // Create test enemy
            var enemy = new GameElement("Enemy");
            enemy.SetData(enemyFaction, new ASCIIStyle("M"), new VisionBlocker());//, new MapCellCollider());
            //map[4, 3].PlaceInCell(enemy);

           

            return state;
        }
    }
}
