using UnityEngine;

public class InputKeyboard : MonoBehaviour
{
    private InputCommands input;
    private float delay = 0.15f;
    private float elapsedTime = 0; 

    /// <summary>
    /// Will return the last keyboard up pressed. Will then clear the input to None.
    /// </summary>
    public InputCommands Command
    {
        get
        {
            InputCommands returnVal = input;
            input = InputCommands.None;
            return returnVal;
        }
    }

    private void Update()
    {
        input = GetKeyboardValue(Time.deltaTime);
    }

    private InputCommands GetKeyboardValue(float time)
    {
        elapsedTime += time; 

        if (elapsedTime < delay)
            return InputCommands.None;

        InputCommands command = InputCommands.None;

        if (Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Y))
        {
            command = InputCommands.UpLeft;
        }
        else if (Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.K))
        {
            command = InputCommands.Up;
        }
        else if (Input.GetKey(KeyCode.Keypad3) || Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.U))
        {
            command = InputCommands.UpRight;
        }
        else if (Input.GetKey(KeyCode.Keypad4) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.H))
        {
            command = InputCommands.Left;
        }
        else if (Input.GetKey(KeyCode.Keypad6) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.L))
        {
            command = InputCommands.Right;
        }
        else if (Input.GetKey(KeyCode.Keypad7) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.B))
        {
            command = InputCommands.DownLeft;
        }
        else if (Input.GetKey(KeyCode.Keypad8) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.X))
        {
            command = InputCommands.Down;
        }
        else if (Input.GetKey(KeyCode.Keypad9) || Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.N))
        {
            command = InputCommands.DownRight;
        }
        else if (Input.GetKey(KeyCode.Keypad5) || Input.GetKey(KeyCode.Space))
        {
            command = InputCommands.Wait;
        }
        else if (Input.GetKey(KeyCode.Period) || Input.GetKey(KeyCode.Comma) || Input.GetKey(KeyCode.Return))
        {
            command = InputCommands.Stairs;
        }
        else if (Input.GetKey(KeyCode.Escape))
        {
            command = InputCommands.CloseGame;
        }

        if (command != InputCommands.None)
            elapsedTime = 0;

        return command;
    }
}