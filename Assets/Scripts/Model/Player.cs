
using System;
using System.Collections.Generic;

public class Player : Actor
{
    public int turnsOutOfCombat { get; set; }
    public int ItemsFound { get; set; }

    private List<Monster> monstersInCombat;

    public Player(Game game) : base(game)
    {
        Attack          = 1;
        Awareness       = 8;
        Color           = Colors.Player;
        Health          = 10;
        MaxHealth       = 10;
        Name            = "Tommy";
        Speed           = 10;
        Moves           = 2;
        Symbol          = '@';
        Sprite          = "Art/PlayerSprite";
        Rotation        = 0;

        monstersInCombat = new List<Monster>();
    }

    public void EngageCombat(Monster monster)
    {
        if (monster != null && !monstersInCombat.Contains(monster))
        {
            monstersInCombat.Add(monster); 
        }
    }

    public void LeaveCombat(Monster monster)
    {
        if (monster != null && monstersInCombat.Contains(monster))
        {
            monstersInCombat.Remove(monster);
        }
    }

    public bool inCombat()
    {   
        foreach (Monster m in monstersInCombat)
        {
            if (m == null || m.Health <= 0)
                monstersInCombat.Remove(m);
        }

        return (monstersInCombat.Count > 0);
    }

    public bool inCombat(Monster monster)
    {
        if (monster == null)
            return false;

        return (monstersInCombat.Contains(monster));
    }

    public override void Died()
    {
        base.Died();
        game.PlayerDied();
    }

    public void DrawStats()
    {
        //game.DrawPlayerStats();
    }

    public void TurnCompleted()
    {
        MovesCompleted = 0;

        if (!inCombat())
        {
            turnsOutOfCombat++;

            if (turnsOutOfCombat > 2)   
                game.Player.ChangeHealthBy(1);
        }
        else
        {
            turnsOutOfCombat = 0;
        }
    }

    public void CheckForItems()
    {
        if (game.World.Chests == null)
            return;

        Chest chest = game.World.Chests.Find(item => item.X == this.X && item.Y == this.Y); 

        if (chest != null)
        {
            Chest.Item reward = Chest.ItemData.Find(data => data.type == chest.Contains);

            if (reward.name != null && reward.name.Length > 0)
            {
                string attribute = "";

                if (reward.health > 0)
                {
                    attribute += "max health by " + reward.health;
                    Health += reward.health;
                    MaxHealth += reward.health;
                }

                if (reward.moves > 0)
                {
                    if (attribute.Length > 0)
                        attribute += " & ";
                    attribute += "moves by " + reward.moves;
                    Moves += reward.moves;
                    GameData.Data.SlippersFound = true;
                }

                if (reward.attack > 0)
                {
                    if (attribute.Length > 0)
                        attribute += " & ";
                    attribute += "attack by " + reward.attack;
                    Attack += reward.attack;                    
                }
                
                game.MessageLog.Add("You received a " + reward.name + " which increases " + attribute + ".");
                ItemsFound++;

                game.Manager.RemoveDrawable(chest);
                game.World.Chests.Remove(chest);
                
            }
        }
    }
}
