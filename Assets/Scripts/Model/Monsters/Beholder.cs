
public class Beholder : Monster
{
    public Beholder(Game game) : base(game) { }

    public bool ActedThisTurn { get; set; }
    public static bool MainActedThisTurn { get; set; }

    public static Beholder Create(int level, Game game)
    {
        return new Beholder(game)
        {
            Attack = 8,
            Awareness = 10,
            Color = Colors.White,
            Health = 24,
            MaxHealth = 24,
            Name = "Tenrodzamon",
            Speed = 10,
            Moves = 1,
            Symbol = 'T',
            Sprite = "Art/Monster/BeholderMainSprite",
            Rotation = 0,
            ActedThisTurn = false
            
        };
    }

    public override void Died()
    {
        base.Died();

        for (int b = 0; b < 6; b++)
        {
            game.World.RemoveMonster(game.beholder[b]);
            game.beholder[b] = null;
            game.Player.LeaveCombat(game.beholder[b]);
        }

        for(int b = 6; b < 9; b++)
        {
            if (game.beholder[b] != null)
                game.beholder[b].Behavior = new StandardMoveAndAttack();
        }

    }
}

public class BeholderLeft : Monster
{
    public BeholderLeft(Game game) : base(game) { }

    public bool actedThisTurn { get; set; }

    public static BeholderLeft Create(int level, Game game)
    {

        return new BeholderLeft(game)
        {
            Attack = 6,
            Awareness = 10,
            Color = Colors.White,
            Health = 12,
            MaxHealth = 12,
            Name = "Tenrodzamon Left Tentacle",
            Speed = 10,
            Moves = 1,
            Symbol = 'T',
            Sprite = "Art/Monster/BeholderTentacleLeft",
            Rotation = 0
        };
    }

    public override void Died()
    {
        base.Died();

        game.beholder[6] = null;

    }
}

public class BeholderCenter : Monster
{
    public BeholderCenter(Game game) : base(game) { }

    public static BeholderCenter Create(int level, Game game)
    {

        return new BeholderCenter(game)
        {
            Attack = 6,
            Awareness = 10,
            Color = Colors.White,
            Health = 12,
            MaxHealth = 12,
            Name = "Tenrodzamon Center Tentacle",
            Speed = 10,
            Moves = 1,
            Symbol = 'T',
            Sprite = "Art/Monster/BeholderTentacleCenter",
            Rotation = 0
        };
    }

    public override void Died()
    {
        base.Died();

        game.beholder[7] = null;

    }
}

public class BeholderRight : Monster
{
    public BeholderRight(Game game) : base(game) { }    

    public static BeholderRight Create(int level, Game game)
    {

        return new BeholderRight(game)
        {
            Attack = 6,
            Awareness = 10,
            Color = Colors.White,
            Health = 12,
            MaxHealth = 12,
            Name = "Tenrodzamon Right Tentacle",
            Speed = 10,
            Moves = 1,
            Symbol = 'T',
            Sprite = "Art/Monster/BeholderTentacleRight",
            Rotation = 0
        };
    }

    public override void Died()
    {
        base.Died();

        game.beholder[8] = null;

    }
}