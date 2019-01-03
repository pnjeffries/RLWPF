using Nucleus.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nucleus.Unity;

public class HotbarButtonClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button b = GetComponent<Button>();
        if (b != null)
        {
            b.onClick.AddListener(OnClick);
        }
        //TODO: Press down as well?  (Use an EventTrigger instead if so)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        InputFunction function = InputFunction.Undefined;
        EquipmentSlot slot = this.gameObject.GetBindingDataContext() as EquipmentSlot;
        GameEngine.Instance.Input.InputRelease(slot.HotKey);
    }
}
