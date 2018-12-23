using Nucleus.Game;
using Nucleus.UI;
using Nucleus.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertyBinding : BindingBase
{
    #region Properties

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
    public void Start()
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
    void Update()
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
        double value;
        string textValue = Binding.GetBoundValueString();

        // Will currently work for input fields and text:
        var field = GetComponent<InputField>();
        if (field != null)
        {
            if (double.TryParse(Binding.GetBoundValue().ToString(), out value)) // output is a number
            {
                field.text = string.Format(StringFormat, value.ToString());
            }
            else
            {
                field.text = string.Format(StringFormat, textValue);
            }

        }
        else
        {
            // Assuming it's text...
            Text text = GetComponent<Text>();
            if (text != null)
            {
                //text.text = string.Format(StringFormat, textValue);

                if (double.TryParse(Binding.GetBoundValue().ToString(), out value)) // output is a number
                {
                    text.text = string.Format(StringFormat, value.ToString("N2"));
                }
                else
                {
                    text.text = string.Format(StringFormat, textValue);
                }
            }
        }
    }

    #endregion
}
