using System;
using System.Collections.Generic;
using RogueSharp;
using RogueSharp.MapCreation;


public class MapSegment
{
    public const int SegmentWidthHeight = 3;

    public const int SegmentCellWidthHeight = 9;
    

    //public Map segmentMap;

    // top left corner position
    public int x = 0;
    public int y = 0;

    public byte ID;
    public SegmentTemplateData template;

    public Rectangle BBox { get { return new Rectangle(x* SegmentCellWidthHeight, y* SegmentCellWidthHeight, 
                                    SegmentCellWidthHeight, SegmentCellWidthHeight); } }

    public enum Direction
    {
        Northwest = 128,
        North = 1,
        Northeast = 2,
        West = 64,
        Center = 255,
        East = 4,
        Southwest = 32,
        South = 16,
        Southeast = 8
    }

    public static byte[,] bitmasks =
    {
        {128, 64, 32},
        {1, 255, 16},
        {2, 4, 8}
    };

    public static byte[] NorthEdgeMask = {128,1,2};
    public static byte[] SouthEdgeMask = {32,16,8};
    public static byte[] EastEdgeMask = {2,4,8};
    public static byte[] WestEdgeMask = {128,64,32};
        
    public static byte[] SegmentIDs =
    {
        0,
        1, 4, 16, 64,
        5, 20, 80, 65,
        7, 28, 112, 193,
        17, 68,
        21, 84, 81, 69,
        23, 92, 113, 197,
        29, 116, 209, 71,
        31, 124, 241, 199,
        85,
        87, 93, 117, 213,
        95, 125, 245, 215,
        119, 221,
        127, 253, 247, 223,
        255
    };

    public static List<byte> matchException = new List<byte>
    {
        119, 221,
        127, 253, 247, 223
    };

    public static List<byte> smallSegments = new List<byte>
    {
        1, 4, 16, 64,
        5, 20, 80, 65,
        17, 68,
        21, 84, 81, 69,
        85
    };

    //public MapSegment()
    //{
    //    //segmentMap = new Map(SegmentCellWidthHeight, SegmentCellWidthHeight);
    //    string mapString = "...\n...\n...";
    //    StringDeserializeMapCreationStrategy<Map> strategy =
    //        new StringDeserializeMapCreationStrategy<Map>(mapString);
    //    segmentMap = strategy.CreateMap();
    //}

    public MapSegment(byte id)
    {
        ID = id;
    }    

    public MapSegmentTile.Tile[,] GetSegmentTiles()
    {
        return MapSegmentTile.GetTiles(ID);
    }

    public static bool EdgesMatch(Direction direction, byte source, byte neighbor)
    {
        byte[] sourceMask = null;
        byte[] neighborMask = null;
            
        switch (direction)
        {
            case Direction.North:
            {
                sourceMask = NorthEdgeMask;
                neighborMask = SouthEdgeMask;
            } break;
                
            case Direction.South:
            {
                sourceMask = SouthEdgeMask;
                neighborMask = NorthEdgeMask;    
            } break;
            case Direction.East:
            {
                sourceMask = EastEdgeMask;
                neighborMask = WestEdgeMask;                  
            } break;
            case Direction.West:
            {
                sourceMask = WestEdgeMask;
                neighborMask = EastEdgeMask;
            } break;
            case Direction.Northwest:
            case Direction.Northeast:
            case Direction.Southeast:
            case Direction.Southwest:
                return false;
        }

        if (sourceMask == null || neighborMask == null)
            return false;
            
        for (int b = 0; b < 3; b++)
        {
//                Console.WriteLine("sourceMask[b]: " + sourceMask[b]);
//                Console.WriteLine("source: " + source);
//                Console.WriteLine("(sourceMask[b] & source): " + (sourceMask[b] & source));
//                Console.WriteLine("neighborMask[b]: " + neighborMask[b]);
//                Console.WriteLine("neighbor: " + neighbor);
//                Console.WriteLine("(neighborMask[b] & neighbor) " + (neighborMask[b] & neighbor));

            bool s = (sourceMask[b] & source) != 0;
            bool n = (neighborMask[b] & neighbor) != 0;

            if (matchException.Contains(neighbor) && n == false && (b == 0 || b == 2))
                continue;

            if (matchException.Contains(source) && s == false && (b == 0 || b == 2))
                continue;

            if (s != n)
                return false;
        }
            
        return true;
    }

    public bool EdgeIsOpen(Direction direction)
    {
        byte[] edgeMask = null;

        switch (direction)
        {
            case Direction.North:
                {
                    edgeMask = NorthEdgeMask;
                }
                break;

            case Direction.South:
                {
                    edgeMask = SouthEdgeMask;
                }
                break;
            case Direction.East:
                {
                    edgeMask = EastEdgeMask;
                }
                break;
            case Direction.West:
                {
                    edgeMask = WestEdgeMask;
                }
                break;
            case Direction.Northwest:
            case Direction.Northeast:
            case Direction.Southeast:
            case Direction.Southwest:
                return false;
        }

        if (edgeMask == null)
            return false;

        for (int b = 0; b < 3; b++)
        {
            if ((edgeMask[b] & ID) != 0)
                return true;
        }

        return false;
    }

    //public bool EdgesMatch(Direction direction, MapSegment comparison)
    //{
    //    if (comparison == null)
    //        return false;

    //    IEnumerable<ICell> edge1 = null;
    //    IEnumerable<ICell> edge2 = null;            

    //    switch (direction)
    //    {
    //        case Direction.North:
    //        {
    //            edge1 = segmentMap.GetCellsInRows(0);
    //            edge2 = comparison.segmentMap.GetCellsInRows(SegmentWidthHeight - 1);
    //        } break;
    //        case Direction.South:
    //        {
    //            edge1 = comparison.segmentMap.GetCellsInRows(SegmentWidthHeight - 1);
    //            edge2 = segmentMap.GetCellsInRows(0);                    
    //        } break;
    //        case Direction.East:
    //        {
    //            edge1 = comparison.segmentMap.GetCellsInColumns(SegmentWidthHeight - 1);
    //            edge2 = segmentMap.GetCellsInRows(0);                    
    //        } break;
    //        case Direction.West:
    //        {
    //            edge1 = segmentMap.GetCellsInRows(0);
    //            edge2 = comparison.segmentMap.GetCellsInColumns(SegmentWidthHeight - 1);
    //        } break;

    //    }

    //    if (edge1 == null || edge2 == null)
    //        return false;

    //    foreach (var e1 in edge1)
    //    {

    //    }

    //    return false;
    //}



}