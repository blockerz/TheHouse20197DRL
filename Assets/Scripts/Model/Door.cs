using RogueSharp;

public class Door : IDrawable
{
    public bool     IsOpen          { get; set; }
    public Colors   Color           { get; set; }
    public Colors   BackgroundColor { get; set; }
    public char     Symbol          { get; set; }
    public int      RoomID          { get; set; }
    public int      ConnectRoomID   { get; set; }
    public int      X               { get; set; }
    public int      Y               { get; set; }
    public int      connectionX     { get; set; }
    public int      connectionY     { get; set; }
    public string   Sprite          { get; set; }
    public int      Rotation        { get; set; }

    private Game    game;

    public Door(Game game)
    {
        Symbol          = '+';
        Color           = Colors.Door;
        BackgroundColor = Colors.DoorBackground;
        this.game       = game;
        Sprite          = "Art/PlayerSprite";
    }

    public void Draw(IMap map)
    {
        if (!map.GetCell(X, Y).IsExplored)
        {
            return;
        }

        Symbol = IsOpen ? '-' : '+';

        if (map.IsInFov(X, Y))
        {
            Color = Colors.DoorFov;
            BackgroundColor = Colors.DoorBackgroundFov;
        }
        else
        {
            Color = Colors.Door;
            BackgroundColor = Colors.DoorBackground;
        }

        game.SetMapCell(X, Y, Color, BackgroundColor, Symbol, map.GetCell(X, Y).IsExplored);
    }
}
