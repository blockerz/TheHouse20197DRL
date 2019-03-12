using RogueSharp.Random;
using System;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Data;

    public int PrimarySeed;

    public int Levels;
    public int[] LevelSeeds;
    public int CurrentLevel;
    public int LastLevel; 

    public string PlayerName;
    public int Difficulty;

    public int PlayerHealth;
    public int PlayerMaxHealth;
    public int PlayerAttack;
    public int PlayerMoves;
    public int ItemsFound;
    public bool SlippersFound; 

    private void Awake()
    {
        if (Data == null)
        {
            Data = this;
        }
        else if (Data != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);

        Levels = 10;
        LevelSeeds = new int[Levels];
        gameObject.name = "GameData";
        PlayerName = "Tommy";
        Difficulty = 1;

        NewGame();
    }

    public void NewGame()
    {
        PlayerHealth = 0;
        PlayerMaxHealth = 0;
        PlayerAttack = 0;
        PlayerMoves = 0;
        ItemsFound = 0;
        SlippersFound = false; 

        CurrentLevel = 0;
        LastLevel = 0;

        PrimarySeed = (int)DateTime.UtcNow.Ticks;

        IRandom Level = new DotNetRandom(PrimarySeed);

        for (int i = 0; i < Levels; i++)
        {
            LevelSeeds[i] = Level.Next(int.MinValue+1, int.MaxValue-1);
        }        


    }


    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
