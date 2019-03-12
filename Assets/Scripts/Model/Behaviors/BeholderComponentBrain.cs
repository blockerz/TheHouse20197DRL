using RogueSharp;
using System;

public class BeholderComponentBrain : IBehavior
{
    public bool Act(Monster monster, CommandSystem commandSystem, Game game)
    {
        // Beholder Brain handles all action on beholder[0]
        return true; 
    }
}
