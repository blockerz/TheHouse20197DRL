using System.Text;
using RogueSharp;
using RogueSharp.DiceNotation;

public class CommandSystem
{
    private Game game;
    public bool IsPlayerTurn { get; set; }

    public CommandSystem(Game game)
    {
        this.game = game;
    }

    /// <summary>
    /// Return value is true if the player was able to move false when the player couldn't move, such as trying to move into a wall.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public int MovePlayer(Direction direction)
    {
        int x = game.Player.X;
        int y = game.Player.Y;

        switch (direction)
        {
            case Direction.Up:
                {
                    game.Player.Rotation = 180;
                    y = game.Player.Y - 1;
                    break;
                }
            case Direction.Down:
                {
                    game.Player.Rotation = 0;
                    y = game.Player.Y + 1;
                    break;
                }
            case Direction.Left:
                {
                    game.Player.Rotation = 270;
                    x = game.Player.X - 1;
                    break;
                }
            case Direction.Right:
                {
                    game.Player.Rotation = 90;
                    x = game.Player.X + 1;
                    break;
                }
            case Direction.UpLeft:
                {
                    game.Player.Rotation = 225;
                    x = game.Player.X - 1;
                    y = game.Player.Y - 1;
                    break;
                }
            case Direction.UpRight:
                {
                    game.Player.Rotation = 135;
                    x = game.Player.X + 1;
                    y = game.Player.Y - 1;
                    break;
                }
            case Direction.DownLeft:
                {
                    game.Player.Rotation = 315;
                    x = game.Player.X - 1;
                    y = game.Player.Y + 1;
                    break;
                }
            case Direction.DownRight:
                {
                    game.Player.Rotation = 45;
                    x = game.Player.X + 1;
                    y = game.Player.Y + 1;
                    break;
                }
            default:
                {
                    return 0;
                }
        }

        if (game.World.SetActorPosition(game.Player, x, y))
        {
            game.Player.CheckForItems();

            return 1;
        }

        Monster monster = game.World.GetMonsterAt(x, y);

        if (monster != null)
        {
            game.Player.EngageCombat(monster);

            Attack(game.Player, monster);
            return game.Player.Moves;
        }

        return 0;
    }

    public void EndPlayerTurn()
    {
        IsPlayerTurn = false;

        game.Player.TurnCompleted();
    }

    public void ActivateMonsters()
    {
        IScheduleable scheduleable = game.SchedulingSystem.Get();

        if (scheduleable is Player)
        {
            IsPlayerTurn = true;
            game.SchedulingSystem.Add(game.Player);
        }
        else
        {
            Monster monster = scheduleable as Monster;

            if (monster != null)
            {
                do
                {
                    monster.PerformAction(this);
                    monster.MovesCompleted++;
                }
                while (monster.MovesCompleted < monster.Moves);

                monster.MovesCompleted = 0;
                game.SchedulingSystem.Add(monster);
            }

            ActivateMonsters();
        }
    }

    public void MoveMonster(Monster monster, Cell cell)
    {
        if (!game.World.SetActorPosition(monster, cell.X, cell.Y))
        {            
            if (game.Player.X == cell.X && game.Player.Y == cell.Y)
            {
                Attack(monster, game.Player);
                monster.MovesCompleted = monster.Moves;
            }
        }        

    }

    public void Attack(Actor attacker, Actor defender)
    {
        if (defender.Health <= 0)
            return;

        StringBuilder attackMessage = new StringBuilder();

        int damage = attacker.Attack;

        attackMessage.AppendFormat("{0} attacks {1} for {2}.", attacker.Name, defender.Name, damage);

        game.MessageLog.Add(attackMessage.ToString());

        if (defender is Beholder)
        {
            for (int b = 0; b < 6; b++)
            {
                if (game.beholder[b] != null)
                    ResolveDamage(game.beholder[b], damage);
            }
        }
        else
        {
            ResolveDamage(defender, damage);
        }
    }

    // Apply any damage that wasn't blocked to the defender
    private void ResolveDamage(Actor defender, int damage)
    {
        if (damage > 0)
        {
            defender.Health = defender.Health - damage;         

            if (defender.Health <= 0)
            {
                ResolveDeath(defender);
            }
        }
    }

    // Remove the defender from the map and add some messages upon death.
    private void ResolveDeath(Actor defender)
    {
        if (defender is Player)
        {
            game.MessageLog.Add(defender.Name + " has died.");
            defender.Died();
        }
        else if (defender is Beholder)
        {
            if (game.beholder[0] != null && game.beholder[0] == defender)
                game.MessageLog.Add(defender.Name + " died.");

            defender.Died();
        }
        else if (defender is Monster)
        {            

            game.World.RemoveMonster((Monster)defender);            

            game.MessageLog.Add(defender.Name + " died.");

            defender.Died();
        }
    }
}
