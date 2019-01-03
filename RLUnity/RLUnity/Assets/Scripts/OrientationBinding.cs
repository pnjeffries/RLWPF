using Nucleus.Geometry;
using Nucleus.Maths;
using Nucleus.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationBinding : PropertyBinding
{
    public override void RefreshUI()
    {
        object value = Binding.GetBoundValue();
        if (value is Angle)
        {
            Angle angle = (Angle)value;

            // Animate (if applicable):
            var smoothie = GetComponent<SmoothRotate>();
            if (smoothie == null)
            {
                transform.rotation = angle.ToUnityQuaternion();
            }
            else
            {
                var animation = new OrientationAnimation(smoothie.gameObject, 
                    smoothie.transform.rotation.ToNucleusAngle(), angle, 0.06,
                    Interpolation.Sin);
                smoothie.Animation = animation;
            }
        }
    }
}
