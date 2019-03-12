using RogueSharp;
using System;

public class BeholderBrain : IBehavior
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

        if (!(monster is Beholder))
            return false;

        Monster[] beholder = game.beholder;

        if (beholder[0] == null)
            return false; 

        if (!beholder[0].TurnsAlerted.HasValue)
        {
            monsterFov.ComputeFov(beholder[0].X, beholder[0].Y, beholder[0].Awareness, true);
            if (monsterFov.IsInFov(player.X, player.Y))
            {
                game.MessageLog.Add(beholder[0].Name + " wants to eat you.");
                beholder[0].TurnsAlerted = 1;
                game.Player.EngageCombat(beholder[0]);
            }
        }

        if (beholder[0].TurnsAlerted.HasValue)
        {

            // Can attack with main body?
            for (int b = 0; b < 6; b++)
            {
                foreach (Point p in neighbors)
                {
                    if (game.Player.X == beholder[b].X + p.X && game.Player.Y == beholder[b].Y + p.Y)
                    {
                        commandSystem.Attack(beholder[0], game.Player);
                        beholder[0].MovesCompleted = beholder[0].Moves;
                        break; // only attack once with main
                    }
                }
            }

            if ((beholder[0].MovesCompleted < beholder[0].Moves))
            { 
                int closestSectionIndex = 0;
                int closestSectionDist = 10000;
                bool tentaclesGone = true;

                for (int b = 6; b < 9; b++)
                {
                    if (beholder[b] != null)
                        tentaclesGone = false;
                }

                for (int b = 0; b < 9; b++)
                {
                    if (beholder[b] == null)
                        continue;

                    dungeonMap.SetIsWalkable(beholder[b].X, beholder[b].Y, true);

                    int distToPlayer = Math.Max(Math.Abs(beholder[b].X - player.X), Math.Abs(beholder[b].Y - player.Y));

                    if (distToPlayer < closestSectionDist)
                        closestSectionIndex = b;
                }

                dungeonMap.SetIsWalkable(player.X, player.Y, true);

                PathFinder pathFinder = new PathFinder(dungeonMap, 1d);
                Path path = null;

                try
                {
                    path = pathFinder.ShortestPath(
                        dungeonMap.GetCell(beholder[closestSectionIndex].X, beholder[closestSectionIndex].Y),
                        dungeonMap.GetCell(player.X, player.Y));
                }
                catch (PathNotFoundException)
                {

                }

                dungeonMap.SetIsWalkable(player.X, player.Y, false);

                if (path != null)
                {
                    try
                    {
                        Cell cell = path.StepForward() as Cell;
                        int dX = cell.X - beholder[closestSectionIndex].X;
                        int dY = cell.Y - beholder[closestSectionIndex].Y;

                        if (game.World.CanRectFitInRoom(new Rectangle(beholder[0].X + dX, beholder[0].Y + dY, 3, (tentaclesGone) ? 2 : 3)))
                        {
                            for (int b = 0; b < 9; b++)
                            {
                                if (beholder[b] == null)
                                    continue;

                                beholder[b].X = beholder[b].X + dX;
                                beholder[b].Y = beholder[b].Y + dY;
                                //game.World.SetIsWalkable(beholder[b].X, beholder[b].Y, false);


                            }
                        }


                    }
                    catch (NoMoreStepsException)
                    {
                        game.MessageLog.Add(beholder[0].Name + " is stuck.");
                    }
                }

                for (int b = 0; b < 9; b++)
                {
                    if (beholder[b] == null)
                        continue;

                    dungeonMap.SetIsWalkable(beholder[b].X, beholder[b].Y, false);
                }
            }

            beholder[0].TurnsAlerted++;

            if (monster.TurnsAlerted > 10)
            {
                if (!monsterFov.IsInFov(player.X, player.Y))
                    game.Player.LeaveCombat(beholder[0]);

                beholder[0].TurnsAlerted = null;
            }
        }
        return true;
    }
}
