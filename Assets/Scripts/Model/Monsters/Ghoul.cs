
public class Ghoul : Monster
{
    public Ghoul(Game game) : base(game) { }

    public static Ghoul Create(int level, Game game)
    {

        return new Ghoul(game)
        {
            Attack = 4,
            Awareness = 8,
            Color = Colors.White,
            Health = 4,
            MaxHealth = 4,
            Name = "Ghoul",
            Speed = 10,
            Moves = 1,
            Symbol = 'g',
            Sprite = "Art/Monster/GhoulSprite",
            Rotation = 0,
            Depth = 2
        };
    }
}