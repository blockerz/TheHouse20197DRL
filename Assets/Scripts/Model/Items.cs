using RogueSharp;
using System.Collections.Generic;

public class Chest : IDrawable
{
    public struct Item
    {
        public ItemType type;
        public string name;
        public int health;
        public int moves;
        public int attack;
    }

    public enum ItemType
    {
        Axe,
        HolySymbol,
        MysticSlippers,
        WardingRing,
        Medalion,
        SilverNecklace,
        Dagger,
        Elixir,
        Jacket,
        SoothElixir,
        ZSIZE // LAST
    }

    public static List<Item> ItemData = new List<Item>
    {
        new Item
        {
            type = ItemType.MysticSlippers,
            name = "Mystic Slippers",
            health = 0,
            moves = 1,
            attack = 0

        },
        new Item
        {
            type = ItemType.Axe,
            name = "Axe",
            health = 0,
            moves = 0, 
            attack = 1

        },
        new Item
        {
            type = ItemType.HolySymbol,
            name = "Holy Trinket",
            health = 0,
            moves = 0,
            attack = 1

        },
        new Item
        {
            type = ItemType.Medalion,
            name = "Medalion",
            health = 1,
            moves = 0,
            attack = 0

        },
        new Item
        {
            type = ItemType.WardingRing,
            name = "Warding Ring",
            health = 1,
            moves = 0,
            attack = 0

        },
        new Item
        {
            type = ItemType.SilverNecklace,
            name = "Silver Necklace",
            health = 1,
            moves = 0,
            attack = 0

        },
        new Item
        {
            type = ItemType.Dagger,
            name = "Sharp Dagger",
            health = 0,
            moves = 0,
            attack = 1

        },
        new Item
        {
            type = ItemType.Elixir,
            name = "Savage Elixir",
            health = 0,
            moves = 0,
            attack = 1

        },
        new Item
        {
            type = ItemType.Jacket,
            name = "Sturdy Jacket",
            health = 1,
            moves = 0,
            attack = 0

        },
        new Item
        {
            type = ItemType.SoothElixir,
            name = "Soothing Elixir",
            health = 1,
            moves = 0,
            attack = 0

        }
    };

    public Colors Color { get; set; }
    public ItemType Contains { get; set; }
    public char Symbol { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public string Sprite { get; set; }
    public int Rotation { get; set; }

    private Game game;

    public Chest(Game game)
    {
        this.game = game;
        Sprite = "Art/TreasureChestSprite";
        Symbol = '$';
        game.Manager.AddDrawable(this);

        if (GameData.Data.SlippersFound)
            Contains = (ItemType)Game.Random.Next(1, (int)ItemType.ZSIZE-1); 
        else
            Contains = (ItemType)Game.Random.Next(0, (int)ItemType.ZSIZE - 1);
    }

    public void Draw(IMap map)
    {
        if (!map.GetCell(X, Y).IsExplored)
        {
            return;
        }

        if (map.IsInFov(X, Y))
        {
            Color = Colors.Player;
        }
        else
        {
            Color = Colors.Floor;
        }

        game.SetMapCell(X, Y, Color, Colors.FloorBackground, Symbol, map.GetCell(X, Y).IsExplored);
    }
}
