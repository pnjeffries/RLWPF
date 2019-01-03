using Nucleus.Game;
using Nucleus.Model;
using Nucleus.Rendering;
using Nucleus.UI;
using Nucleus.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script which allows child items to be generated from the
/// contents of a databound collection
/// </summary>
public class ItemsBinding : BindingBase
{ 
    #region Fields

    /// <summary>
    /// The default template to be used when generating
    /// </summary>
    public GameObject DefaultTemplate = null;

    /// <summary>
    /// A dictionary which maps source objects to their unity representation
    /// </summary>
    protected IDictionary<object, GameObject> _ItemRepresentationMap = new Dictionary<object, GameObject>();

    #endregion

    #region Properties

    protected ItemsSourceBinding _Binding = new ItemsSourceBinding();

    public override DataBinding Binding
    {
        get
        {
            return _Binding;
        }
    }

    #endregion

    #region Methods

    // Use this for initialization
    public void Start()
    {
        if (DataContext == null)
            DataContext = GameEngine.Instance;
        InitialiseBinding();
    }

    // Update is called once per frame
    void Update()
    {
        BindingUpdates();
    }

    public override void RefreshUI()
    {
        //The UI of the control itself does not need to be refreshed
    }

    public override void BindingUpdates()
    {
        base.BindingUpdates();

        // Update child items
        if (_Binding.ItemsRefreshRequired) RefreshChildren();
    }

    protected virtual GameObject GetTemplate(object forObject)
    {
        if (forObject is Element)
        {
            var dataOwner = (Element)forObject;
            if (dataOwner.HasData())
            {
                PrefabStyle style = dataOwner.Data.GetData<PrefabStyle>();
                if (style != null && style.PrefabKey != null)
                {
                    var result = Resources.Load<GameObject>(style.PrefabKey);
                    if (result != null) return result;
                }
            }
        }
        return DefaultTemplate;
    }

    /// <summary>
    /// Create or destroy unity objects to represent the items added to or removed
    /// from the bound collection.
    /// </summary>
    public void RefreshChildren()
    {
        if (_Binding.RemovedItems != null)
        {
            foreach (object item in _Binding.RemovedItems)
            {
                if (_ItemRepresentationMap.ContainsKey(item))
                {
                    var representation = _ItemRepresentationMap[item];
                    Destroy(representation);
                }
            }
        }

        if (_Binding.AddedItems != null)
        {

            foreach (object item in _Binding.AddedItems)
            {
                if (!_ItemRepresentationMap.ContainsKey(item))
                {
                    // Create representation from template:
                    var template = GetTemplate(item);
                    if (template != null)
                    {
                        var representation = Instantiate(template, this.transform);
                        representation.SetDataContext(item);
                        _ItemRepresentationMap.Add(item, representation);
                    }
                }
            }
        }
    }


    #endregion
}
