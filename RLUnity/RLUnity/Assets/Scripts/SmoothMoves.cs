using Nucleus.Geometry;
using Nucleus.Rendering;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behaviour that adds custom animation capabilities
/// </summary>
public class SmoothMoves : MonoBehaviour
{
    private PropertyAnimation _Animation = null;

    /// <summary>
    /// Get or set the current animation
    /// </summary>
    public PropertyAnimation Animation
    {
        get { return _Animation; }
        set { _Animation = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Animation != null)
        {
            Animation.Advance(Time.deltaTime);
            Animation.Apply();

            if (Animation.Finished) Animation = null;
        }
    }
}
