using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public MessageView messageView;
    public MapView mapViewPrefab;
    public DrawableSprite drawableSpritePrefab;
    public StatsSprite statsSpritePrefab;
    public PlayerCamera playerCamera;
    public Monster Beholder;

    private InputKeyboard inputKeyboard;
    private MapView mapInstance;
    private Game game;    
    private List<DrawableSprite> sprites;
    private GameObject deathPane;    

    // Start is called before the first frame update
    void Start()
    {
        Beholder = null;

        if (GameData.Data == null)
        {
            Debug.LogError("GameData not found");
        }        

        inputKeyboard = GetComponent<InputKeyboard>();
        messageView = GetComponent<MessageView>();

        deathPane = GameObject.Find("DeathPane");
        deathPane.SetActive(false);

        BeginGame();
    }

    // Update is called once per frame
    void Update()
    {
        game.Update();
    }

    private void BeginGame()
    {
        sprites = new List<DrawableSprite>();

        int seed = GameData.Data.LevelSeeds[GameData.Data.CurrentLevel];

        game = new Game(this, seed);
        game.GenerateMap();

        //Debug.Log(game.World.ToString());

        mapInstance = Instantiate(mapViewPrefab) as MapView;
        mapInstance.gameObject.name = "Map";
        mapInstance.CreateMapView(game.World);

        playerCamera.InitCamera(game);        

    }

    public void GameOver()
    {
        //ResetGame();
        //BeginGame();
        //SceneManager.LoadScene("DeathScene");
        deathPane.SetActive(true);
    }

    private void ResetGame()
    {
        GameObject segmentMesh = GameObject.Find("Segment Template Mesh");

        if (segmentMesh != null)
            Destroy(segmentMesh);

        Destroy(mapInstance.gameObject);

        foreach(var s in sprites)
        {
            Destroy(s.gameObject);
        }
        sprites.Clear();

        deathPane.SetActive(false);
    }

    public void AddDrawable(IDrawable drawable)
    {

        if (drawable is Beholder)
        {
            if (Beholder != null)
                return;
            else
                Beholder = drawable as Monster;
        }

        DrawableSprite instance = Instantiate(drawableSpritePrefab) as DrawableSprite;
        instance.drawable = drawable;        

        if (drawable is Actor)
        {

            Actor actor = drawable as Actor;
            StatsSprite stats = Instantiate(statsSpritePrefab) as StatsSprite;
            stats.actor = actor;
            stats.transform.SetParent(instance.transform, false);

        }

        sprites.Add(instance);
    }

    internal void RemoveDrawable(IDrawable drawableSprite)
    {
        DrawableSprite dead = sprites.Find(sprite => sprite.drawable.X == drawableSprite.X 
                        && sprite.drawable.Y == drawableSprite.Y
                        && sprite.drawable.Sprite == drawableSprite.Sprite);

        if (drawableSprite is Beholder)
        {
            if (Beholder == null)
                return;
            else
            {
                dead = sprites.Find(sprite => sprite.drawable.Sprite.Equals(Beholder.Sprite));
                Beholder = null;
            }
        }        

        if (dead != null)
        {
            Destroy(dead.gameObject);

            sprites.Remove(dead);
        }
    }

    public InputCommands GetUserCommand()
    {
        return inputKeyboard.Command;
    }

    public void PostMessageLog(Queue<string> messages, Colors color)
    {
        messageView.PostMessageLog(messages, ColorMap.UnityColors[color]);
    }

    internal void ChangeLevel(int level)
    {
        GameData.Data.PlayerAttack = game.Player.Attack;
        GameData.Data.PlayerHealth = game.Player.Health;
        GameData.Data.PlayerMaxHealth = game.Player.MaxHealth;
        GameData.Data.PlayerMoves = game.Player.Moves;
        GameData.Data.ItemsFound = game.Player.ItemsFound;

        GameData.Data.LastLevel = GameData.Data.CurrentLevel;
        GameData.Data.CurrentLevel = level;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
