
public class LivingArmor : Monster
{
    public LivingArmor(Game game) : base(game) { }

    public static LivingArmor Create(int level, Game game)
    {

        return new LivingArmor(game)
        {
            Attack = 6,
            Awareness = 5,
            Color = Colors.KoboldColor,
            Health = 4,
            MaxHealth = 4,
            Name = "Living Armor",
            Speed = 10,
            Moves = 0,
            Symbol = 'L',
            Sprite = "Art/Monster/LivingArmorSprite",
            Rotation = 0,
            Depth = 7
        };
    }
}