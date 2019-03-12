using RogueSharp;

public class DontLookAway : IBehavior
{
    public bool isPlayerLookingAway(Monster monster, Player player)
    {
        int rotation = player.Rotation;

        switch(rotation)
        {
            case 0:
                if (player.Y <= monster.Y)
                    return false;
                break;
            case 90:
                if (player.X <= monster.X)
                    return false;
                break;
            case 180:
                if (player.Y >= monster.Y)
                    return false;
                break;
            case 270:
                if (player.X >= monster.X)
                    return false;
                break;
            case 225:
                if (player.X + player.Y >=  monster.X + monster.Y )
                    return false;
                break;
            case 135:
                if (player.X - player.Y <= monster.X * monster.Y)
                    return false;
                break;
            case 315:
                if (player.X - player.Y >= monster.X * monster.Y)
                    return false;
                break;
            case 45:
                if (player.X + player.Y <= monster.X + monster.Y)
                    return false;
                break;
        }



        return true;
    }

    public bool Act(Monster monster, CommandSystem commandSystem, Game game)
    {
        DungeonMap dungeonMap = game.World;
        Player player = game.Player;
        FieldOfView monsterFov = new FieldOfView(dungeonMap);

        // If the monster has not been alerted, compute a field-of-view 
        // Use the monster's Awareness value for the distance in the FoV check
        // If the player is in the monster's FoV then alert it
        // Add a message to the MessageLog regarding this alerted status
        if (!monster.TurnsAlerted.HasValue)
        {
            monsterFov.ComputeFov(monster.X, monster.Y, monster.Awareness, true);
            if (monsterFov.IsInFov(player.X, player.Y))
            {
                game.MessageLog.Add(monster.Name + " can see you.");
                monster.TurnsAlerted = 1;
                game.Player.EngageCombat(monster);
            }
        }

        if (monster.TurnsAlerted.HasValue)
        {

            if (isPlayerLookingAway(monster, player))
            {
                if (monster.Moves > 0 && monster.TurnsAlerted > 1)
                {
                    RushPlayer(monster, commandSystem, game, dungeonMap, player, monsterFov);
                    monster.Moves = 0;
                }
                else
                {
                    // First Turn Makes Movement Visible to give player a chance to turn back
                    monster.MovesCompleted = 10;
                    monster.Moves = 10;
                }
            }
            else
            {
                monster.TurnsAlerted = 0;
                monster.Moves = 0;
            }

        }

        monster.TurnsAlerted++;

        if (monster.TurnsAlerted > 10)
        {
            if (!monsterFov.IsInFov(player.X, player.Y))
                game.Player.LeaveCombat(monster);

            monster.TurnsAlerted = null;
        }

        return true;
    }

    public bool RushPlayer(Monster monster, CommandSystem commandSystem, Game game, DungeonMap dungeonMap, Player player, FieldOfView monsterFov)
    { 

            // Before we find a path, make sure to make the monster and player Cells walkable
            dungeonMap.SetIsWalkable(monster.X, monster.Y, true);
            dungeonMap.SetIsWalkable(player.X, player.Y, true);

            PathFinder pathFinder = new PathFinder(dungeonMap, 1d);
            Path path = null;

            try
            {
                path = pathFinder.ShortestPath(
                dungeonMap.GetCell(monster.X, monster.Y),
                dungeonMap.GetCell(player.X, player.Y));
            }
            catch (PathNotFoundException)
            {
                // The monster can see the player, but cannot find a path to him
                // This could be due to other monsters blocking the way
                // Add a message to the message log that the monster is waiting
                //game.MessageLog.Add(monster.Name + " waits for a turn.");
            }

            // Don't forget to set the walkable status back to false
            dungeonMap.SetIsWalkable(monster.X, monster.Y, false);
            dungeonMap.SetIsWalkable(player.X, player.Y, false);

            // In the case that there was a path, tell the CommandSystem to move the monster
            if (path != null)
            {
                try
                {
                // TODO: This should be path.StepForward() but there is a bug in RogueSharp V3
                // The bug is that a Path returned from a PathFinder does not include the source Cell

                    do
                    {
                        commandSystem.MoveMonster(monster, path.StepForward() as Cell);
                        monster.MovesCompleted++;
                    } while (monster.MovesCompleted < monster.Moves); 

                }
                catch (NoMoreStepsException)
                {
                    //game.MessageLog.Add(monster.Name + " cannot move.");
                }
            }
        
        return true;
    }
}
