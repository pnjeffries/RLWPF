using Nucleus.Base;
using Nucleus.Game;
using Nucleus.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace RLWPF
{
    public class ArtitectTester : NotifyPropertyChangedBase
    {
        #region Properties

        /// <summary>
        /// The blueprint that the generator will work on
        /// </summary>
        private SquareCellMap<CellGenerationType> _Blueprint;

        /// <summary>
        /// The blueprint that the generator will work on
        /// </summary>
        public SquareCellMap<CellGenerationType> Blueprint
        {
            get { return _Blueprint; }
            set { _Blueprint = value; NotifyPropertiesChanged("Blueprint"); }
        }

        private IList<SquareCellMap<CellGenerationType>> _SnapShots
            = new List<SquareCellMap<CellGenerationType>>();

        int _Frame = 0;

        private DispatcherTimer _Timer;

        #endregion

        #region Constructor

        public ArtitectTester()
        {
            // Initialise state, map and stage:
            int mapX = 24;
            int mapY = 24;

            // Room templates:
            var stdRoom = new RoomTemplate();
            var largeRoom = new RoomTemplate(RoomType.Room, 3, 6, 4, 6);
            var corridor = new RoomTemplate(RoomType.Circulation, 1, 1, 4, 8);
            //corridor.ExitPlacement = ExitPlacement.Opposite;
            var corridor2 = new RoomTemplate(RoomType.Circulation, 1, 1, 1, 8);
            //corridor2.ExitPlacement = ExitPlacement.Opposite_Side;

            // Create map:
            var generator = new DungeonArtitect(mapX, mapY);
            generator.Snapshots = _SnapShots;
            generator.Templates.Add(stdRoom);
            generator.Templates.Add(largeRoom);
            generator.Templates.Add(corridor);
            generator.Templates.Add(corridor2);
            generator.Generate(10, 14, stdRoom);


            _Timer = new DispatcherTimer();
            _Timer.Interval = new TimeSpan(0,0,0,0,20);
            _Timer.Tick += _Timer_Tick;
            _Timer.Start();

            Blueprint = _SnapShots[0];
        }

        private void _Timer_Tick(object sender, EventArgs e)
        {
            _Frame++;
            if (_Frame >= _SnapShots.Count) _Frame = 0;
            Blueprint = _SnapShots[_Frame];
        }

        #endregion
    }
}
