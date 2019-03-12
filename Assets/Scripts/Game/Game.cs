using RogueSharp.Random;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Game
{
    public static IRandom Random { get; private set; }
    
    public MessageLog MessageLog { get; set; }
    public SchedulingSystem SchedulingSystem { get; private set; }

    public Player Player { get; set; }
    public Monster[] beholder = null; 
    public DungeonMap World { get; private set; }

    public bool gameOver = false;

    public GameManager Manager;

    private int segmentWidth = 6;
    private int segmentHeight = 6;

    private CommandSystem commandSystem;
    private bool renderRequired = true;

    public Game(GameManager manager, int seed)
    {
        this.Manager = manager;

        Random = new DotNetRandom(seed);

        commandSystem = new CommandSystem(this);
        MessageLog = new MessageLog(this);
        SchedulingSystem = new SchedulingSystem();

        MessageLog.Add("You have entered Level " + (GameData.Data.CurrentLevel + 1));

        if (GameData.Data.CurrentLevel == GameData.Data.Levels-1)
            MessageLog.Add("You sense an ominous presence.");
    }

    public void Reset()
    {
        int seed = (int)DateTime.UtcNow.Ticks;
        Random = new DotNetRandom(seed);

        //MessageLog.Clear();
        SchedulingSystem.Clear();
        gameOver = false;
    }

    public void PlayerDied()
    {
        Manager.GameOver();
        gameOver = true; 
    }

    internal void SetMapCell(int x, int y, Colors color, Colors floorBackground, char symbol, bool isExplored)
    {
        throw new NotImplementedException();
    }

    public void GenerateMap()
    {
        MapGenerator generator = new MapGenerator(this, segmentWidth, segmentHeight);
        World = generator.GenerateMap();        
    }

    public void Update()
    {
        if (!gameOver)
            CheckKeyboard();

        MessageLog.Draw();
        CheckWin();
    }

    public void PostMessageLog(Queue<string> messages, Colors color)
    {
        Manager.PostMessageLog(messages, color);
    }

    private void CheckKeyboard()
    {

        InputCommands command = Manager.GetUserCommand();

        if (commandSystem.IsPlayerTurn)
        {
            switch (command)
            {
                case InputCommands.None:
                    return;
                case InputCommands.UpLeft:
                    Player.MovesCompleted += commandSystem.MovePlayer(Direction.UpLeft);
                    break;
                case InputCommands.Up:
                    Player.MovesCompleted += commandSystem.MovePlayer(Direction.Up);
                    break;
                case InputCommands.UpRight:
                    Player.MovesCompleted += commandSystem.MovePlayer(Direction.UpRight);
                    break;
                case InputCommands.Left:
                    Player.MovesCompleted += commandSystem.MovePlayer(Direction.Left);
                    break;
                case InputCommands.Right:
                    Player.MovesCompleted += commandSystem.MovePlayer(Direction.Right);
                    break;
                case InputCommands.DownLeft:
                    Player.MovesCompleted += commandSystem.MovePlayer(Direction.DownLeft);
                    break;
                case InputCommands.Down:
                    Player.MovesCompleted += commandSystem.MovePlayer(Direction.Down);
                    break;
                case InputCommands.DownRight:
                    Player.MovesCompleted += commandSystem.MovePlayer(Direction.DownRight);
                    break;
                case InputCommands.Wait:
                    World.UpdatePlayerFieldOfView(Player);
                    Player.MovesCompleted = Player.Moves;
                    break;
                case InputCommands.Stairs:
                    ConfirmStairs();
                    break;
                case InputCommands.CloseGame:
                    SceneManager.LoadScene("MenuScene");
                    break;
                default:
                    break;
            }

            if (Player.MovesCompleted >= Player.Moves)
            {
                renderRequired = true;
                commandSystem.EndPlayerTurn();
            }
            else
            {
                renderRequired = true;
            }
        }
        else
        {
            commandSystem.ActivateMonsters();
            renderRequired = true;
        }
    }

    private bool ConfirmStairs()
    {
        if (World.StairsUp.X == Player.X && World.StairsUp.Y == Player.Y)
        {
            if (GameData.Data.CurrentLevel > 0)
                Manager.ChangeLevel(GameData.Data.CurrentLevel-1);
            return true;
        }

        if (World.StairsDown.X == Player.X && World.StairsDown.Y == Player.Y)
        {
            if (GameData.Data.CurrentLevel < GameData.Data.Levels-1)
                Manager.ChangeLevel(GameData.Data.CurrentLevel+1);
            return true;
        }

        //SceneManager.LoadScene("MenuScene");
        return false;


    }

    public void CheckWin()
    {
        if (beholder != null)
        {
            bool didWin = true;

            for (int b = 0; b < 9; b++)
            {
                if (beholder[b] != null)
                    didWin = false;
            }

            if (didWin)
            {
                PlayerWon();
            }
        }
    }

    public void PlayerWon()
    {
        SceneManager.LoadScene("WinScene");
    }
}
