
public class Demon : Monster
{
    public Demon(Game game) : base(game) { }

    public static Demon Create(int level, Game game)
    {

        return new Demon(game)
        {
            Attack = 2,
            Awareness = 8,
            Color = Colors.White,
            Health = 3,
            MaxHealth = 3,
            Name = "Demon",
            Speed = 10,
            Moves = 1,
            Symbol = 'd',
            Sprite = "Art/Monster/DemonSprite",
            Rotation = 0,
            Depth = 1
        };
    }
}