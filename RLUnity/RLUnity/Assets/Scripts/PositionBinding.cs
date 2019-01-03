using Nucleus.Geometry;
using Nucleus.Maths;
using Nucleus.Rendering;
using Nucleus.UI;
using Nucleus.Unity;
using Nucleus.Unity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class PositionBinding : PropertyBinding
    {
        public override void RefreshUI()
        {
            object value = Binding.GetBoundValue();
            if (value is Vector)
            {
                Vector pos = (Vector)value;

                // Animate (if applicable):
                var smoothie = GetComponent<SmoothMoves>();
                if (smoothie == null)
                {
                    transform.localPosition = pos.ToUnityVector3();
                }
                else
                {
                    var animation = new PropertyAnimation(smoothie.gameObject, "transform.localPosition",
                        pos.ToUnityVector3(), 0.1,
                        Interpolation.Sin,
                        delegate (object v1, object v2, double t, Interpolation i)
                        {
                            Vector pt1 = ((Vector3)v1).ToNucleusVector();
                            Vector pt2 = ((Vector3)v2).ToNucleusVector();
                            return i.Interpolate(pt1, pt2, t).ToUnityVector3();
                        });
                    smoothie.Animation = animation;
                }
            }
        }
    }
}
