
public class Wraith : Monster
{
    public Wraith(Game game) : base(game) { }

    public static Wraith Create(int level, Game game)
    {

        return new Wraith(game)
        {
            Attack = 4,
            Awareness = 8,
            Color = Colors.KoboldColor,
            Health = 8,
            MaxHealth = 8,
            Name = "Wraith",
            Speed = 10,
            Moves = 2,
            Symbol = 'w',
            Sprite = "Art/Monster/WraithSprite",
            Rotation = 0,
            Depth = 5
        };
    }
}