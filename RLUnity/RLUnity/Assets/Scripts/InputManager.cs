using Nucleus.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    /// <summary>
    /// The mapping of key codes to functions
    /// </summary>
    private Dictionary<KeyCode, InputFunction> _KeyMapping =
            new Dictionary<KeyCode, InputFunction>();

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Make static?
        // Default keymapping:
        _KeyMapping.Add(KeyCode.UpArrow, InputFunction.Up);
        _KeyMapping.Add(KeyCode.DownArrow, InputFunction.Down);
        _KeyMapping.Add(KeyCode.LeftArrow, InputFunction.Left);
        _KeyMapping.Add(KeyCode.RightArrow, InputFunction.Right);
        _KeyMapping.Add(KeyCode.Space, InputFunction.Wait);
    }

    // Update is called once per frame
    void Update()
    {
        KeyDown();
        KeyUp();
    }

    /// <summary>
    /// Deal with input keys being pressed
    /// </summary>
    private void KeyDown()
    {
        if (Input.anyKeyDown)
        {
            foreach (var kvp in _KeyMapping)
            {
                if (Input.GetKeyDown(kvp.Key))
                    GameEngine.Instance.Input.InputPress(kvp.Value);
            }
        }
    }

    /// <summary>
    /// Deal with input keys being released
    /// </summary>
    private void KeyUp()
    {
        foreach (var kvp in _KeyMapping)
        {
            if (Input.GetKeyDown(kvp.Key))
                GameEngine.Instance.Input.InputRelease(kvp.Value);
        }
    }
}
