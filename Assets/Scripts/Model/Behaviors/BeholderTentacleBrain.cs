using RogueSharp;
using System;

public class BeholderTentacleBrain : IBehavior
{
    private Point[] neighbors =
{
        new Point(0, 1),
        new Point(1,0),
        new Point(0,-1),
        new Point(-1,0),
        new Point(1, 1),
        new Point(1,-1),
        new Point(-1,-1),
        new Point(-1,1)

    };

    public bool Act(Monster monster, CommandSystem commandSystem, Game game)
    {
        DungeonMap dungeonMap = game.World;
        Player player = game.Player;
        FieldOfView monsterFov = new FieldOfView(dungeonMap);

        if (game.beholder[0] == null)
        {
            monster.Behavior = new StandardMoveAndAttack();
            return false;
        }


        foreach (Point p in neighbors)
        {
            if (game.Player.X == monster.X + p.X && game.Player.Y == monster.Y + p.Y)
            {
                commandSystem.Attack(monster, game.Player);
                monster.MovesCompleted = monster.Moves;
                break; // only attack once with main
            }
        }


        return true;
    }
}
