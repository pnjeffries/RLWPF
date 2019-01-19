using Nucleus.Game;
using Nucleus.UI;
using Nucleus.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nucleus.Extensions;

public class PropertyBinding : BindingBase
{
    #region Properties

    /// <summary>
    /// Field to specify the target component type.
    /// If null, a default will be automatically assumed.
    /// </summary>
    public string TargetType = null;

    /// <summary>
    /// Field to specify the target path on the target component.
    /// If null, a default will be automatically assumed.
    /// </summary>
    public string TargetPath = null;

    /// <summary>
    /// The binding manager
    /// </summary>
    protected DataBinding _Binding = new DataBinding();

    /// <summary>
    /// Get the DataBinding manager
    /// </summary>
    public override DataBinding Binding
    {
        get { return _Binding; }
    }

    #endregion

    #region Methods

    // Use this for initialization
    public virtual void Start()
    {
        if (DataContext == null)
            DataContext = GameEngine.Instance;
        InitialiseBinding();

        var field = GetComponent<InputField>();
        if (field != null)
        {
            // TODO: Register listener to set bound value
            field.onValueChanged.AddListener(delegate { ValueChanged(); });
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        BindingUpdates();
    }


    public void ValueChanged()
    {
        var field = GetComponent<UnityEngine.UI.InputField>();
        Binding.SetBoundValue(field.text);
    }

    /// <summary>
    /// Is the InputFIeld this binding is attached to currently being edited?
    /// </summary>
    /// <returns></returns>
    public override bool IsLocked()
    {
        var field = GetComponent<InputField>();
        if (field != null && field.isFocused) return true;

        return false;
    }

    /// <summary>
    /// Refresh the UI with the bound value
    /// </summary>
    public override void RefreshUI()
    {
        // User override of target properties:
        if (TargetType != null)
        {
            Component compo = GetComponent(TargetType);
            compo.SetByPath(TargetPath, Binding.GetBoundValue());
        }


        // Will currently work for input fields and text:
        InputField field = GetComponent<InputField>();
        if (field != null)
        {
            field.text = Binding.GetBoundValueString();
        }
        else
        {
            Slider slider = GetComponent<Slider>();
            if (slider != null)
            {
                slider.value = Convert.ToSingle(Binding.GetBoundValue());
            }
            else
            {
                // Assuming it's text...
                Text text = GetComponent<Text>();
                if (text != null)
                {
                    text.text = Binding.GetBoundValueString();
                }
            }
        }
    }

    #endregion
}
