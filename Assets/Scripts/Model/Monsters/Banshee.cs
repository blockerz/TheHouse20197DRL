
public class Banshee : Monster
{
    public Banshee(Game game) : base(game) { }

    public static Banshee Create(int level, Game game)
    {

        return new Banshee(game)
        {
            Attack = 5,
            Awareness = 8,
            Color = Colors.KoboldColor,
            Health = 14,
            MaxHealth = 14,
            Name = "Banshee",
            Speed = 10,
            Moves = 2,
            Symbol = 'E',
            Sprite = "Art/Monster/BansheeSprite",
            Rotation = 0,
            Depth = 8
        };
    }
}