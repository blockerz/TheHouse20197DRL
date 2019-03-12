using RogueSharp;
using RogueSharp.MapCreation;
using System.Collections.Generic;

public class MapSegmentTile
{
    private MapSegment segment;

    public struct Tile
    {
        public Tilename name;
        public string symbols;
        public int templateColumn;
        public int templateRow;
        public Rotation rotation;
    }

    public struct Segment
    {
        public byte ID;
        public Tilename[] tiles;
    }

    public enum Tilename
    {
        SOLID = 0,
        CORNER_NE,
        CORNER_NW,
        CORNER_SE,
        CORNER_SW,
        POINT_NE,
        POINT_NW,
        POINT_SE,
        POINT_SW,
        EDGE_N,
        EDGE_E,
        EDGE_S,
        EDGE_W,
        DOOR_N,
        DOOR_E,
        DOOR_S,
        DOOR_W,
        OPEN
    }

    public enum Rotation
    {
        Rotate0 = 0,
        Rotate90 = 90,
        Rotate180 = 180,
        Rotate270 = 270
    }

    public static Map GetSegmentMap(byte segment)
    {
        Map map = new Map(MapSegment.SegmentCellWidthHeight, MapSegment.SegmentCellWidthHeight);

        Segment template;
        if (segments.TryGetValue(segment, out template))
        {
            for (int y = 0; y < MapSegment.SegmentWidthHeight; y++)
            {
                for (int x = 0; x < MapSegment.SegmentWidthHeight; x++)
                {
                    string mapString = components[(int)template.tiles[(y * 3) + x]].symbols;

                    StringDeserializeMapCreationStrategy<Map> strategy = new StringDeserializeMapCreationStrategy<Map>(mapString);
                    Map tempMap = strategy.CreateMap();

                    map.Copy(tempMap, x * MapSegment.SegmentWidthHeight, y * MapSegment.SegmentWidthHeight);
                }
            }
        }

        return map;
    }

    public static Tile[,] GetTiles(byte segment)
    {
        Tile[,] tiles = new Tile[MapSegment.SegmentWidthHeight, MapSegment.SegmentWidthHeight];

        Segment template;
        if (segments.TryGetValue(segment, out template))
        {
            for (int y = 0; y < MapSegment.SegmentWidthHeight; y++)
            {
                for (int x = 0; x < MapSegment.SegmentWidthHeight; x++)
                {
                    tiles[x,y] = components[(int)template.tiles[(y * 3) + x]];
                }
            }
        }

        return tiles;
    }

    public static Tile[] components =
    {
        new Tile
        {
            name = Tilename.SOLID,
            symbols = "###\n" +
                      "###\n" +
                      "###",
            templateColumn = 1,
            templateRow = 0,
            rotation = Rotation.Rotate0
        },
        new Tile{
            name = Tilename.CORNER_NE,
            symbols = "###\n" +
                      "..#\n" +
                      "..#",
            templateColumn = 0,
            templateRow = 2,
            rotation = Rotation.Rotate90
        },
        new Tile{
            name = Tilename.CORNER_NW,
            symbols = "###\n" +
                      "#..\n" +
                      "#..",
            templateColumn = 0,
            templateRow = 2,
            rotation = Rotation.Rotate0
        },
        new Tile{
            name = Tilename.CORNER_SE,
            symbols = "..#\n" +
                      "..#\n" +
                      "###",
            templateColumn = 0,
            templateRow = 2,
            rotation = Rotation.Rotate180
        },
        new Tile{
            name = Tilename.CORNER_SW,
            symbols = "#..\n" +
                      "#..\n" +
                      "###",
            templateColumn = 0,
            templateRow = 2,
            rotation = Rotation.Rotate270
        },
        new Tile{
            name = Tilename.POINT_NE,
            symbols = "..#\n" +
                      "...\n" +
                      "...",
            templateColumn = 1,
            templateRow = 2,
            rotation = Rotation.Rotate90
        },
        new Tile{
            name = Tilename.POINT_NW,
            symbols = "#..\n" +
                      "...\n" +
                      "...",
            templateColumn = 1,
            templateRow = 2,
            rotation = Rotation.Rotate0
        },
        new Tile{
            name = Tilename.POINT_SE,
            symbols = "...\n" +
                      "...\n" +
                      "..#",
            templateColumn = 1,
            templateRow = 2,
            rotation = Rotation.Rotate180
        },
        new Tile{
            name = Tilename.POINT_SW,
            symbols = "...\n" +
                      "...\n" +
                      "#..",
            templateColumn = 1,
            templateRow = 2,
            rotation = Rotation.Rotate270
        },
        new Tile{
            name = Tilename.EDGE_N,
            symbols = "###\n" +
                      "...\n" +
                      "...",
            templateColumn = 0,
            templateRow = 1,
            rotation = Rotation.Rotate90
        },
        new Tile{
            name = Tilename.EDGE_E,
            symbols = "..#\n" +
                      "..#\n" +
                      "..#",
            templateColumn = 0,
            templateRow = 1,
            rotation = Rotation.Rotate180
        },
        new Tile{
            name = Tilename.EDGE_S,
            symbols = "...\n" +
                      "...\n" +
                      "###",
            templateColumn = 0,
            templateRow = 1,
            rotation = Rotation.Rotate270
        },
        new Tile{
            name = Tilename.EDGE_W,
            symbols = "#..\n" +
                      "#..\n" +
                      "#..",
            templateColumn = 0,
            templateRow = 1,
            rotation = Rotation.Rotate0
        },
        new Tile{
            name = Tilename.DOOR_N,
            symbols = "#s#\n" +
                      "...\n" +
                      "...",
            templateColumn = 0,
            templateRow = 0,
            rotation = Rotation.Rotate90
        },
        new Tile{
            name = Tilename.DOOR_E,
            symbols = "..#\n" +
                      "..s\n" +
                      "..#",
            templateColumn = 0,
            templateRow = 0,
            rotation = Rotation.Rotate180
        },
        new Tile{
            name = Tilename.DOOR_S,
            symbols = "...\n" +
                      "...\n" +
                      "#s#",
            templateColumn = 0,
            templateRow = 0,
            rotation = Rotation.Rotate270
        },
        new Tile{
            name = Tilename.DOOR_W,
            symbols = "#..\n" +
                      "s..\n" +
                      "#..",
            templateColumn = 0,
            templateRow = 0,
            rotation = Rotation.Rotate0
        },
        new Tile{
            name = Tilename.OPEN,
            symbols = "...\n" +
                      "...\n" +
                      "...",
            templateColumn = 1,
            templateRow = 1,
            rotation = Rotation.Rotate0
        },

    };

    public static Dictionary<byte, Segment> segments = new Dictionary<byte, Segment>()
    {
        {
            0,
            new Segment
            {
                ID = 0,
                tiles =
                new Tilename[]
                {
                    Tilename.SOLID,
                    Tilename.SOLID,
                    Tilename.SOLID,

                    Tilename.SOLID,
                    Tilename.SOLID,
                    Tilename.SOLID,

                    Tilename.SOLID,
                    Tilename.SOLID,
                    Tilename.SOLID
                }
            }
        },
        {
            1,
            new Segment
            {
                ID = 1,
                tiles =
                new Tilename[]
                {
                    Tilename.CORNER_NW,
                    Tilename.DOOR_N,
                    Tilename.CORNER_NE,

                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.EDGE_E,

                    Tilename.CORNER_SW,
                    Tilename.EDGE_S,
                    Tilename.CORNER_SE
                }
            }
        },
        {
            4,
            new Segment
            {
                ID = 4,
                tiles =
                new Tilename[]
                {
                    Tilename.CORNER_NW,
                    Tilename.EDGE_N,
                    Tilename.CORNER_NE,

                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.DOOR_E,

                    Tilename.CORNER_SW,
                    Tilename.EDGE_S,
                    Tilename.CORNER_SE
                }
            }
        },
        {
            16,
            new Segment
            {
                ID = 16,
                tiles =
                new Tilename[]
                {
                    Tilename.CORNER_NW,
                    Tilename.EDGE_N,
                    Tilename.CORNER_NE,

                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.EDGE_E,

                    Tilename.CORNER_SW,
                    Tilename.DOOR_S,
                    Tilename.CORNER_SE
                }
            }
        },
        {
            64,
            new Segment
            {
                ID = 64,
                tiles =
                new Tilename[]
                {
                    Tilename.CORNER_NW,
                    Tilename.EDGE_N,
                    Tilename.CORNER_NE,

                    Tilename.DOOR_W,
                    Tilename.OPEN,
                    Tilename.EDGE_E,

                    Tilename.CORNER_SW,
                    Tilename.EDGE_S,
                    Tilename.CORNER_SE
                }
            }
        },
        {
            5,
            new Segment
            {
                ID = 5,
                tiles =
                new Tilename[]
                {
                    Tilename.CORNER_NW,
                    Tilename.DOOR_N,
                    Tilename.CORNER_NE,

                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.DOOR_E,

                    Tilename.CORNER_SW,
                    Tilename.EDGE_S,
                    Tilename.CORNER_SE
                }
            }
        },
        {
            20,
            new Segment
            {
                ID = 20,
                tiles =
                new Tilename[]
                {
                    Tilename.CORNER_NW,
                    Tilename.EDGE_N,
                    Tilename.CORNER_NE,

                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.DOOR_E,

                    Tilename.CORNER_SW,
                    Tilename.DOOR_S,
                    Tilename.CORNER_SE
                }
            }
        },
        {
            80,
            new Segment
            {
                ID = 80,
                tiles =
                new Tilename[]
                {
                    Tilename.CORNER_NW,
                    Tilename.EDGE_N,
                    Tilename.CORNER_NE,

                    Tilename.DOOR_W,
                    Tilename.OPEN,
                    Tilename.EDGE_E,

                    Tilename.CORNER_SW,
                    Tilename.DOOR_S,
                    Tilename.CORNER_SE
                }
            }
        },
        {
            65,
            new Segment
            {
                ID = 65,
                tiles =
                new Tilename[]
                {
                    Tilename.CORNER_NW,
                    Tilename.DOOR_N,
                    Tilename.CORNER_NE,

                    Tilename.DOOR_W,
                    Tilename.OPEN,
                    Tilename.EDGE_E,

                    Tilename.CORNER_SW,
                    Tilename.EDGE_S,
                    Tilename.CORNER_SE
                }
            }
        },
        {
            7,
            new Segment
            {
                ID = 7,
                tiles =
                new Tilename[]
                {
                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.CORNER_SW,
                    Tilename.EDGE_S,
                    Tilename.EDGE_S
                }
            }
        },
        {
            28,
            new Segment
            {
                ID = 28,
                tiles =
                new Tilename[]
                {
                    Tilename.CORNER_NW,
                    Tilename.EDGE_N,
                    Tilename.EDGE_N,

                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.OPEN
                }
            }
        },
        {
            112,
            new Segment
            {
                ID = 112,
                tiles =
                new Tilename[]
                {
                    Tilename.EDGE_N,
                    Tilename.EDGE_N,
                    Tilename.CORNER_NE,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.EDGE_E,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.EDGE_E
                }
            }
        },
        {
            193,
            new Segment
            {
                ID = 193,
                tiles =
                new Tilename[]
                {
                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.EDGE_E,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.EDGE_E,

                    Tilename.EDGE_S,
                    Tilename.EDGE_S,
                    Tilename.CORNER_SE
                }
            }
        },
        {
            17,
            new Segment
            {
                ID = 17,
                tiles =
                new Tilename[]
                {
                    Tilename.CORNER_NW,
                    Tilename.DOOR_N,
                    Tilename.CORNER_NE,

                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.EDGE_E,

                    Tilename.CORNER_SW,
                    Tilename.DOOR_S,
                    Tilename.CORNER_SE
                }
            }
        },
        {
            68,
            new Segment
            {
                ID = 68,
                tiles =
                new Tilename[]
                {
                    Tilename.CORNER_NW,
                    Tilename.EDGE_N,
                    Tilename.CORNER_NE,

                    Tilename.DOOR_W,
                    Tilename.OPEN,
                    Tilename.DOOR_E,

                    Tilename.CORNER_SW,
                    Tilename.EDGE_S,
                    Tilename.CORNER_SE
                }
            }
        },
        {
            21,
            new Segment
            {
                ID = 21,
                tiles =
                new Tilename[]
                {
                    Tilename.CORNER_NW,
                    Tilename.DOOR_N,
                    Tilename.CORNER_NE,

                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.DOOR_E,

                    Tilename.CORNER_SW,
                    Tilename.DOOR_S,
                    Tilename.CORNER_SE
                }
            }
        },
        {
            84,
            new Segment
            {
                ID = 84,
                tiles =
                new Tilename[]
                {
                    Tilename.CORNER_NW,
                    Tilename.EDGE_N,
                    Tilename.CORNER_NE,

                    Tilename.DOOR_W,
                    Tilename.OPEN,
                    Tilename.DOOR_E,

                    Tilename.CORNER_SW,
                    Tilename.DOOR_S,
                    Tilename.CORNER_SE
                }
            }
        },
        {
            81,
            new Segment
            {
                ID = 81,
                tiles =
                new Tilename[]
                {
                    Tilename.CORNER_NW,
                    Tilename.DOOR_N,
                    Tilename.CORNER_NE,

                    Tilename.DOOR_W,
                    Tilename.OPEN,
                    Tilename.EDGE_E,

                    Tilename.CORNER_SW,
                    Tilename.DOOR_S,
                    Tilename.CORNER_SE
                }
            }
        },
        {
            69,
            new Segment
            {
                ID = 69,
                tiles =
                new Tilename[]
                {
                    Tilename.CORNER_NW,
                    Tilename.DOOR_N,
                    Tilename.CORNER_NE,

                    Tilename.DOOR_W,
                    Tilename.OPEN,
                    Tilename.DOOR_E,

                    Tilename.CORNER_SW,
                    Tilename.EDGE_S,
                    Tilename.CORNER_SE
                }
            }
        },
        {
            23,
            new Segment
            {
                ID = 23,
                tiles =
                new Tilename[]
                {
                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.CORNER_SW,
                    Tilename.DOOR_S,
                    Tilename.EDGE_S
                }
            }
        },
        {
            92,
            new Segment
            {
                ID = 92,
                tiles =
                new Tilename[]
                {
                    Tilename.CORNER_NW,
                    Tilename.EDGE_N,
                    Tilename.EDGE_N,

                    Tilename.DOOR_W,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.OPEN
                }
            }
        },
        {
            113,
            new Segment
            {
                ID = 113,
                tiles =
                new Tilename[]
                {
                    Tilename.EDGE_N,
                    Tilename.DOOR_N,
                    Tilename.CORNER_NE,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.EDGE_E,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.EDGE_E
                }
            }
        },
        {
            197,
            new Segment
            {
                ID = 197,
                tiles =
                new Tilename[]
                {
                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.EDGE_E,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.DOOR_E,

                    Tilename.EDGE_S,
                    Tilename.EDGE_S,
                    Tilename.CORNER_SE
                }
            }
        },
        {
            29,
            new Segment
            {
                ID = 29,
                tiles =
                new Tilename[]
                {
                    Tilename.CORNER_NW,
                    Tilename.DOOR_N,
                    Tilename.EDGE_N,

                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.OPEN
                }
            }
        },
        {
            116,
            new Segment
            {
                ID = 116,
                tiles =
                new Tilename[]
                {
                    Tilename.EDGE_N,
                    Tilename.EDGE_N,
                    Tilename.CORNER_NE,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.DOOR_E,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.EDGE_E
                }
            }
        },
        {
            209,
            new Segment
            {
                ID = 209,
                tiles =
                new Tilename[]
                {
                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.EDGE_E,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.EDGE_E,

                    Tilename.EDGE_S,
                    Tilename.DOOR_S,
                    Tilename.CORNER_SE
                }
            }
        },
        {
            71,
            new Segment
            {
                ID = 71,
                tiles =
                new Tilename[]
                {
                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.DOOR_W,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.CORNER_SW,
                    Tilename.EDGE_S,
                    Tilename.EDGE_S
                }
            }
        },
        {
            31,
            new Segment
            {
                ID = 31,
                tiles =
                new Tilename[]
                {
                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.OPEN
                }
            }
        },
        {
            124,
            new Segment
            {
                ID = 124,
                tiles =
                new Tilename[]
                {
                    Tilename.EDGE_N,
                    Tilename.EDGE_N,
                    Tilename.EDGE_N,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN
                }
            }
        },
        {
            241,
            new Segment
            {
                ID = 241,
                tiles =
                new Tilename[]
                {
                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.EDGE_E,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.EDGE_E,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.EDGE_E
                }
            }
        },
        {
            199,
            new Segment
            {
                ID = 199,
                tiles =
                new Tilename[]
                {
                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.EDGE_S,
                    Tilename.EDGE_S,
                    Tilename.EDGE_S
                }
            }
        },
        {
            85,
            new Segment
            {
                ID = 85,
                tiles =
                new Tilename[]
                {
                    Tilename.CORNER_NW,
                    Tilename.DOOR_N,
                    Tilename.CORNER_NE,

                    Tilename.DOOR_W,
                    Tilename.OPEN,
                    Tilename.DOOR_E,

                    Tilename.CORNER_SW,
                    Tilename.DOOR_S,
                    Tilename.CORNER_SE
                }
            }
        },
        {
            87,
            new Segment
            {
                ID = 87,
                tiles =
                new Tilename[]
                {
                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.DOOR_W,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.CORNER_SW,
                    Tilename.DOOR_S,
                    Tilename.EDGE_S
                }
            }
        },
        {
            93,
            new Segment
            {
                ID = 93,
                tiles =
                new Tilename[]
                {
                    Tilename.CORNER_NW,
                    Tilename.DOOR_N,
                    Tilename.EDGE_N,

                    Tilename.DOOR_W,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.OPEN
                }
            }
        },
        {
            117,
            new Segment
            {
                ID = 117,
                tiles =
                new Tilename[]
                {
                    Tilename.EDGE_N,
                    Tilename.DOOR_N,
                    Tilename.CORNER_NE,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.DOOR_E,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.EDGE_E
                }
            }
        },
        {
            213,
            new Segment
            {
                ID = 213,
                tiles =
                new Tilename[]
                {
                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.EDGE_E,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.DOOR_E,

                    Tilename.EDGE_S,
                    Tilename.DOOR_S,
                    Tilename.CORNER_SE
                }
            }
        },
        {
            95,
            new Segment
            {
                ID = 95,
                tiles =
                new Tilename[]
                {
                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.DOOR_W,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.EDGE_W,
                    Tilename.OPEN,
                    Tilename.OPEN
                }
            }
        },
        {
            125,
            new Segment
            {
                ID = 125,
                tiles =
                new Tilename[]
                {
                    Tilename.EDGE_N,
                    Tilename.DOOR_N,
                    Tilename.EDGE_N,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN
                }
            }
        },
        {
            245,
            new Segment
            {
                ID = 245,
                tiles =
                new Tilename[]
                {
                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.EDGE_E,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.DOOR_E,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.EDGE_E
                }
            }
        },
        {
            215,
            new Segment
            {
                ID = 215,
                tiles =
                new Tilename[]
                {
                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.EDGE_S,
                    Tilename.DOOR_S,
                    Tilename.EDGE_S
                }
            }
        },
        {
            119,
            new Segment
            {
                ID = 119,
                tiles =
                new Tilename[]
                {
                    Tilename.POINT_NW,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.POINT_SE
                }
            }
        },
        {
            221,
            new Segment
            {
                ID = 221,
                tiles =
                new Tilename[]
                {
                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.POINT_NE,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.POINT_SW,
                    Tilename.OPEN,
                    Tilename.OPEN
                }
            }
        },
        {
            127,
            new Segment
            {
                ID = 127,
                tiles =
                new Tilename[]
                {
                    Tilename.POINT_NW,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN
                }
            }
        },
        {
            253,
            new Segment
            {
                ID = 253,
                tiles =
                new Tilename[]
                {
                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.POINT_NE,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN
                }
            }
        },
        {
            247,
            new Segment
            {
                ID = 247,
                tiles =
                new Tilename[]
                {
                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.POINT_SE
                }
            }
        },
        {
            223,
            new Segment
            {
                ID = 223,
                tiles =
                new Tilename[]
                {
                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.POINT_SW,
                    Tilename.OPEN,
                    Tilename.OPEN
                }
            }
        },
        {
            255,
            new Segment
            {
                ID = 255,
                tiles =
                new Tilename[]
                {
                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN,

                    Tilename.OPEN,
                    Tilename.OPEN,
                    Tilename.OPEN
                }
            }
        }
    };


    public MapSegmentTile(MapSegment segment)
    {
        this.segment = segment;
    }


}
