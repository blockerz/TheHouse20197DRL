
public class Doll : Monster
{
    public Doll(Game game) : base(game) { }

    public static Doll Create(int level, Game game)
    {

        return new Doll(game)
        {
            Attack = 3,
            Awareness = 5,
            Color = Colors.KoboldColor,
            Health = 1,
            MaxHealth = 1,
            Name = "Creepy Doll",
            Speed = 10,
            Moves = 0,
            Symbol = 'c',
            Sprite = "Art/Monster/DollSprite",
            Rotation = 0,
            Depth = 0
        };
    }
}