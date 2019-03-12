
using System.Collections.Generic;

public class MonsterGenerator 
{
    public static Monster[] CreateBeholder(Game game, int level)
    {
        Monster[] beholder = new Monster[9];

        for (int i = 0; i < 6; i++)
        {
            
            beholder[i] = Beholder.Create(level, game);

            if (i == 0)
                beholder[i].Behavior = new BeholderBrain();
            else
                beholder[i].Behavior = new BeholderComponentBrain();

            beholder[i].Health = beholder[i].Health + (level - 1);
            beholder[i].Attack = beholder[i].Attack + (level - 1);
        }

        beholder[6] = BeholderLeft.Create(level, game);
        beholder[6].Behavior = new BeholderTentacleBrain();
        beholder[6].Health = beholder[6].Health + (level - 1);
        beholder[6].Attack = beholder[6].Attack + (level - 1);

        beholder[7] = BeholderCenter.Create(level, game);
        beholder[7].Behavior = new BeholderTentacleBrain();
        beholder[7].Health = beholder[7].Health + (level - 1);
        beholder[7].Attack = beholder[7].Attack + (level - 1);

        beholder[8] = BeholderRight.Create(level, game);
        beholder[8].Behavior = new BeholderTentacleBrain();
        beholder[8].Health = beholder[8].Health + (level - 1);
        beholder[8].Attack = beholder[8].Attack + (level - 1);

        return beholder;
    }

    public static Monster GetRandomMonster(Game game, int level)
    {
        MonsterList monster = (MonsterList)Game.Random.Next(System.Enum.GetNames(typeof(MonsterList)).Length-1);

        return CreateMonster(monster, game, level); 
    }

    public static Monster GetDepthAppropriateMonster(Game game, int level)
    {
        int depth = GameData.Data.CurrentLevel;

        List<MonsterList> list = new List<MonsterList>();

        if (depth >= 9)
        {
            list.Add(MonsterList.Banshee);
        }

        if (depth >= 8)
        {
            list.Add(MonsterList.Banshee);
        }

        if (depth >= 7)
        {
            list.Add(MonsterList.LivingArmor);

            if (Game.Random.Next(100) < 20)
                list.Add(MonsterList.Banshee);
        }

        if (depth >= 6)
        {
            if (Game.Random.Next(100) < 20)
                list.Add(MonsterList.LivingArmor);
        }

        if (depth >= 5)
        {
            list.Add(MonsterList.Wraith);
        }

        if (depth >= 4)
        {
            list.Add(MonsterList.Spider);

            if (Game.Random.Next(100) < 20)
                list.Add(MonsterList.Wraith);
        }

        if (depth >= 3)
        {
            list.Add(MonsterList.Ghoul);

            if (Game.Random.Next(100) < 20)
                list.Add(MonsterList.Spider);
        }

        if (depth >= 2)
        {
            if (Game.Random.Next(100) < 20)
                list.Add(MonsterList.Ghoul);
        }

        if (depth >= 1)
        {
            list.Add(MonsterList.Demon);
        }

        if (Game.Random.Next(100) < 20)
            list.Add(MonsterList.Demon);

        list.Add(MonsterList.Bat);
        list.Add(MonsterList.Doll);
        
        

        MonsterList monster = list[Game.Random.Next(list.Count-1)];

        return CreateMonster(monster, game, level);
    }

    public static Monster CreateMonster(MonsterList monster, Game game, int level)
    {
        Monster newMonster = null; 

        switch (monster)
        {
            case MonsterList.Bat:
                newMonster = Bat.Create(level, game);
                newMonster.Behavior = new StandardMoveAndAttack();
                break;

            case MonsterList.Banshee:
                newMonster = Banshee.Create(level, game);
                newMonster.Behavior = new TeleportAroundPlayer();
                break;

            case MonsterList.Demon:
                newMonster = Demon.Create(level, game);
                newMonster.Behavior = new StandardMoveAndAttack();
                break;

            case MonsterList.Doll:
                newMonster = Doll.Create(level, game);
                newMonster.Behavior = new DontLookAway();
                break;

            case MonsterList.Ghoul:
                newMonster = Ghoul.Create(level, game);
                newMonster.Behavior = new StandardMoveAndAttack();
                break;

            case MonsterList.LivingArmor:
                newMonster = LivingArmor.Create(level, game);
                newMonster.Behavior = new DontLookAway();
                break;

            case MonsterList.Spider:
                newMonster = Spider.Create(level, game);
                newMonster.Behavior = new StandardMoveAndAttack();
                break;

            case MonsterList.Wraith:
                newMonster = Wraith.Create(level, game);
                newMonster.Behavior = new TeleportAroundPlayer();
                break;

        }

        newMonster.Health = newMonster.Health + (level - 1);
        newMonster.Attack = newMonster.Attack + (level - 1);

        return newMonster;
    }



}
