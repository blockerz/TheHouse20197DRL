
public class Spider : Monster
{
    public Spider(Game game) : base(game) { }

    public static Spider Create(int level, Game game)
    {

        return new Spider(game)
        {
            Attack = 3,
            Awareness = 8,
            Color = Colors.White,
            Health = 6,
            MaxHealth = 6,
            Name = "Giant Spider",
            Speed = 10,
            Moves = 2,
            Symbol = 's',
            Sprite = "Art/Monster/SpiderSprite",
            Rotation = 0,
            Depth = 4
        };
    }
}