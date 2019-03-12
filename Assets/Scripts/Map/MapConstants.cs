
using UnityEngine;

public static class MapConstants 
{

    public const float MapSegmentWidth = 9f;
    public const float MapSegmentHeight = 9f;

    public const int MapChunkCellWidth = 9;
    public const int MapChunkCellHeight = 9;    

    public static Vector3[] Corners =
    {
        new Vector3(0f, 0f, 0f),
        new Vector3(0f, MapSegmentHeight, 0f),
        new Vector3(MapSegmentWidth, MapSegmentHeight, 0f),
        new Vector3(MapSegmentWidth, 0f, 0f)
    };

    public enum TileIndex
    {
        North = 1,
        Northeast = 2,
        East = 4,
        Southeast = 8,
        South = 16,
        Southwest = 32,
        West = 64,
        Northwest = 128
    }

    public enum SegmentTileIndex3x3Ordered
    {
        Southwest = 32,
        South = 16,
        Southeast = 8,
        West = 64,
        Center = 255,
        East = 4,
        Northwest = 128,
        North = 1,
        Northeast = 2        
    }

    public enum BlobTileset
    {
        // 000
        // 000
        // 000
        Empty = 0,
        // 010
        // 010
        // 000
        CulNorth = 1,
        CulEast = 4, 
        CulSouth = 16,
        CulWest = 64,
        // 010
        // 011
        // 000
        LNorth = 5,
        LEast = 20,
        LSouth = 80,
        LWest = 65,
        // 011
        // 011
        // 000
        CornerNorth = 7,
        CornerEast = 28,
        CornerSouth = 112,
        CornerWest = 193,
        // 010
        // 010
        // 010
        PassNorthSouth = 17,
        PassEastWest = 68,
        // 010
        // 011
        // 010
        TNorth = 21,
        TEast = 84,
        TSouth = 81,
        TWest = 69,
        // 011
        // 011
        // 010
        PNorth = 23,
        PEast = 92,
        PSouth = 113,
        PWest = 197,
        // 010
        // 011
        // 011
        BNorth = 29, 
        BEast = 116,
        BSouth = 209,
        BWest = 71,
        // 011
        // 011
        // 011
        EdgeNorth = 31,
        EdgeEast = 124,
        EdgeSouth = 241, 
        EdgeWest = 199,
        // 010
        // 111
        // 010
        Cross = 85,
        // 011
        // 111
        // 010
        Cross3CornerNorth = 87, 
        Cross3CornerEast = 93,
        Cross3CornerSouth = 117,
        Cross3CornerWest = 213,
        // 011
        // 111
        // 011
        Edge2CornerNorth = 95,
        Edge2CornerEast = 125,
        Edge2CornerSouth = 245,
        Edge2CornerWest = 215,
        // 011
        // 111
        // 110
        OppositeCornerNorth = 119,
        OppositeCornerSouth = 221,
        // 011
        // 111
        // 111
        Cross1CornerNorth = 127,
        Cross1CornerEast = 253,
        Cross1CornerSouth = 247,
        Cross1CornerWest = 223,
        // 111
        // 111
        // 111
        Full = 255
        

    }

    public static Vector3[] Segments =
{
        new Vector3(0f, 0f, 0f),
        new Vector3(0f, MapSegmentHeight, 0f),
        new Vector3(MapSegmentWidth, MapSegmentHeight, 0f),
        new Vector3(MapSegmentWidth, 0f, 0f),

        new Vector3(0f, 0f, 0f),
        new Vector3(0f, MapSegmentHeight, 0f),
        new Vector3(MapSegmentWidth, MapSegmentHeight, 0f),
        new Vector3(MapSegmentWidth, 0f, 0f),

        new Vector3(0f, 0f, 0f),
        new Vector3(0f, MapSegmentHeight, 0f),
        new Vector3(MapSegmentWidth, MapSegmentHeight, 0f),
        new Vector3(MapSegmentWidth, 0f, 0f),


        new Vector3(0f, 0f, 0f),
        new Vector3(0f, MapSegmentHeight, 0f),
        new Vector3(MapSegmentWidth, MapSegmentHeight, 0f),
        new Vector3(MapSegmentWidth, 0f, 0f),

        new Vector3(0f, 0f, 0f),
        new Vector3(0f, MapSegmentHeight, 0f),
        new Vector3(MapSegmentWidth, MapSegmentHeight, 0f),
        new Vector3(MapSegmentWidth, 0f, 0f),

        new Vector3(0f, 0f, 0f),
        new Vector3(0f, MapSegmentHeight, 0f),
        new Vector3(MapSegmentWidth, MapSegmentHeight, 0f),
        new Vector3(MapSegmentWidth, 0f, 0f),


        new Vector3(0f, 0f, 0f),
        new Vector3(0f, MapSegmentHeight, 0f),
        new Vector3(MapSegmentWidth, MapSegmentHeight, 0f),
        new Vector3(MapSegmentWidth, 0f, 0f),

        new Vector3(0f, 0f, 0f),
        new Vector3(0f, MapSegmentHeight, 0f),
        new Vector3(MapSegmentWidth, MapSegmentHeight, 0f),
        new Vector3(MapSegmentWidth, 0f, 0f),

        new Vector3(0f, 0f, 0f),
        new Vector3(0f, MapSegmentHeight, 0f),
        new Vector3(MapSegmentWidth, MapSegmentHeight, 0f),
        new Vector3(MapSegmentWidth, 0f, 0f)
    };
}
