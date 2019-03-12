using RogueSharp;

public class Actor : IActor, IDrawable, IScheduleable
{
    // IActor
    private int     attack;
    public  int     Attack          { get { return attack; }        set { attack = value; } }
    private int     health;
    public  int     Health          { get { return health; }        set { health = value; } }
    private int     maxHealth;
    public  int     MaxHealth       { get { return maxHealth; }     set { maxHealth = value; } }
    private int     speed;
    public  int     Speed           { get { return speed; }         set { speed = value; } }
    private int     moves;
    public  int     Moves           { get { return moves; }         set { moves = value; } }
    private int     movesCompleted;
    public  int     MovesCompleted  { get { return movesCompleted; }set { movesCompleted = value; } }
    private string  name;
    public string   Name            { get { return name; }          set { name = value; } }
    private int     awareness;
    public  int     Awareness       { get { return awareness; }     set { awareness = value; } }

    // IDrawable
    public  Colors  Color           { get; set; }
    public  char    Symbol          { get; set; }
    public  int     X               { get; set; }
    public  int     Y               { get; set; }
    public  string  Sprite          { get; set; }
    public  int     Rotation        { get; set; }

    // Ischeduleable
    public  int     Time            { get {return Speed;} }

    protected Game  game;

    public Actor(Game game)
    {
        this.game = game;
        game.Manager.AddDrawable(this);
        MovesCompleted = 0;
    }

    public virtual void Died()
    {
        game.Manager.RemoveDrawable(this);
        
    }

    public void ChangeHealthBy(int amount)
    {
        health += amount;
        if (health > maxHealth)
            health = maxHealth;

        if (health < 0)
            health = 0;
    }

    public void Draw(IMap map)
    {
        // Only draw the actor with the color and symbol when they are in field-of-view
        if (map.IsInFov(X, Y))
        {
            game.SetMapCell(X, Y, Color, Colors.FloorBackgroundFov, Symbol, map.GetCell(X, Y).IsExplored);
        }
        else
        {
            // When not in field-of-view just draw a normal floor
            game.SetMapCell(X, Y, Colors.Floor, Colors.FloorBackground, '.', map.GetCell(X, Y).IsExplored);
        }
    }
}
