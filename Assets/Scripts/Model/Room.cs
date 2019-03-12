using RogueSharp;

public class Room
{
    public int ID { get; set; }
    public Rectangle BBox { get; set; }
    public Map Map; 
    public Colors Color { get; set; }
    public Colors BackgroundColor { get; set; }

    private Game game;

    public Room(Game game)
    {
        //Symbol = '+';
        Color = Colors.Floor;
        BackgroundColor = Colors.FloorBackground;
        this.game = game;
    }
}
