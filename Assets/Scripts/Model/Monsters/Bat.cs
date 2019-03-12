
public class Bat : Monster
{
    public Bat(Game game) : base(game) { }

    public static Bat Create(int level, Game game)
    {

        return new Bat(game)
        {
            Attack = 1,
            Awareness = 8,
            Color = Colors.KoboldColor,
            Health = 2,
            MaxHealth = 2,
            Name = "Bat",
            Speed = 10,
            Moves = 2,
            Symbol = 'b',
            Sprite = "Art/Monster/BatSprite",
            Rotation = 0,
            Depth = 0
        };
    }
}