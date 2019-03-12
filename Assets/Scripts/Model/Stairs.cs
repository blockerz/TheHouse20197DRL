using RogueSharp;

public class Stairs : IDrawable
{
    public Colors   Color           { get; set; }
    public bool     IsUp            { get; set; }
    public char     Symbol          { get; set; }
    public int      X               { get; set; }
    public int      Y               { get; set; }
    public string   Sprite          { get; set; }
    public int      Rotation        { get; set; }

    private Game game;

    public Stairs(Game game)
    {
        this.game = game;
        Sprite = "Art/StairsDown";
        game.Manager.AddDrawable(this);
    }

    public void Draw(IMap map)
    {
        if (!map.GetCell(X, Y).IsExplored)
        {
            return;
        }

        Symbol = IsUp ? '<' : '>';

        if (map.IsInFov(X, Y))
        {
            Color = Colors.Player;
        }
        else
        {
            Color = Colors.Floor;
        }

        game.SetMapCell(X, Y, Color, Colors.FloorBackground, Symbol, map.GetCell(X, Y).IsExplored);
    }
}
