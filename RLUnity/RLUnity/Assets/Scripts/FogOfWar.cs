using Nucleus.Game;
using Nucleus.Geometry;
using Nucleus.Rendering;
using Nucleus.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : PropertyBinding
{
    /// <summary>
    /// A factor applied to the speed of animations
    /// </summary>
    public double TweenFactor = 1.0;

    private double _Tween = 0;
    private SquareCellMap<int> _OldValue = null;
    private SquareCellMap<int> _NewValue = null;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        UnityMeshBuilder builder = new UnityMeshBuilder();
        // TEMP:
        int mapX = 24;
        int mapY = 18;
        builder.AddQuadGridMesh(new Vector(-1,-1), new Vector(0.5, 0), new Vector(0, 0.5),
            (mapX + 2) * 2 + 1, (mapY + 2) * 2 + 1); 
        builder.Finalize();
        UnityEngine.Mesh mesh = builder.Mesh;
        GetComponent<MeshFilter>().mesh = mesh;
        Color32[] vCols = new Color32[mesh.vertexCount];
        for (int i = 0; i < vCols.Length; i++)
        {
            vCols[i] = new Color32(0, 0, 0, 128);
        }
        mesh.colors32 = vCols;
    }

    public override void Update()
    {
        base.Update();

        //Animate:
        if (_NewValue != null)
        {
            _Tween += Time.deltaTime * TweenFactor;
            if (_Tween > 1) _Tween = 1;
            GetComponent<MeshFilter>().mesh.SetVertexColoursFromVisionMap(_NewValue, _OldValue, _Tween, Nucleus.Maths.Interpolation.Sin, 4);
            if (_Tween >= 1)
            {
                _OldValue = _NewValue;
            }
        }
    }

    public override void RefreshUI()
    {
        object value = Binding.GetBoundValue();
        if (value is ICellMap<int>)
        {
            ICellMap<int> map = (ICellMap<int>)value;
            if (_NewValue != null) _OldValue = _NewValue;
            _NewValue = (SquareCellMap<int>)map;
            _Tween = 0;
        }
    }
}
