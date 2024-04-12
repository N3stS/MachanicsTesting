using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using TMPro;


public class DebugController : MonoBehaviour
{
    bool _showConsole;

    string _input;

    public static DebugCommand<int> PLAYER_SPEED;
    public static DebugCommand DEBUG_TEST;
    public static DebugCommand<string> CONSOLE_TEXT;

    public List<object> commandList;

    /*[SerializeField] PlayerStatsController _playerStatsController;
    [SerializeField] VirusPopUpController _virusPopUpController;
    [SerializeField] ObstacleSpikeController _obstacleSpikeController;
    [SerializeField] GlitchingPlattformController _glitchingPlattformController;
    [SerializeField] ConsoleController _consoleController;*/

    public void OnOpenCommandPrompt(InputValue value)
    {
        _showConsole = !_showConsole;
    }

    public void OnSendCommand(InputValue value)
    {
        if (_showConsole)
        {
            HandleInput();
            _input = "";
        }
    }

    private void Awake()
    {
        DEBUG_TEST = new DebugCommand("debug_test", "Tests Debugger Functionality", "debug_test", () =>
        {
            Debug.Log("Debugger Is Working! ^^");
        });

        /*PLAYER_JUMP = new DebugCommand<int>("player_jump", "Sets the new player jump strength", "player_jump <jump_strength>", (x) =>
        {
            _playerStatsController.SetPlayerJump(x);
        });

        CLOSE_POPUP = new DebugCommand("close_popup()", "Closes down a pop up", "close_popup()", () =>
        {
            _virusPopUpController.ClosePopUp();
        });

        PAUSE_SPIKES = new DebugCommand("pause_spikes()", "Pauses the spikes movement", "pause_spikes()", () =>
        {
            _obstacleSpikeController.PauseSpikes();
        });

        PAUSE_GLITCH = new DebugCommand("pause_glitch()", "Pauses the glitching of plattforms", "pause_glitch()", () =>
        {
            _glitchingPlattformController.PauseGlitch();
        });

        CONSOLE_COLOR = new DebugCommand<string>("console_color", "Sets the background color of the console", "console_color <color>", (x) =>
        {
            _consoleController.SetConsoleColor(x);
        });

        CONSOLE_TEXT = new DebugCommand<string>("console_text", "Sets the text color of the console", "console_text <color>", (x) =>
        {
            _consoleController.SetConsoleTextColor(x);
            GUIColor(x);
        });*/

        commandList = new List<object>
        {
            DEBUG_TEST,
        };
    }

    private void OnGUI()
    {
        if (!_showConsole) { return; }

        float y = 0f;

        GUI.Box(new Rect(0, y, Screen.width, 100), "");
        GUI.SetNextControlName("console");
        _input = GUI.TextField(new Rect(1f, y + 10f, Screen.width -10, 90f), _input);
        GUI.FocusControl("console");
    }

    private void HandleInput()
    {

        string[] properties = _input.Split(' ');

        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (_input.Contains(commandBase.CommandID))
            {
                if (commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }
                else if (commandList[i] as DebugCommand<int> != null)
                {
                    (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                }
                else if (commandList[i] as DebugCommand<string> != null)
                {
                    (commandList[i] as DebugCommand<string>).Invoke(properties[1]);
                }
            }
        }
    }
}
