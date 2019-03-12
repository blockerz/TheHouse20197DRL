using UnityEngine;


public class MapAtlas
{
    private const float mapAtlasWidth = 1152f;
    private const float mapAtlasHeight = 1152f;
    private const float tileWidth = 384f;
    private const float tileHeight = 384f;

    public static Material material;

    public static void Initialize()
    {
        material = Resources.Load<Material>("Art/Basic Dungeon");
        if (material == null)
        {
            Debug.LogError("Atlas Material Not Found.");
        }
    }

    //public static Vector2[] GetUVs(int col, int rowYup)
    //{
    //    float xLeft = (((float)col) * tileWidth) / mapAtlasWidth;
    //    float xRight = (((float)col + 1) * tileWidth) / mapAtlasWidth;
    //    float yTop = (((float)rowYup + 1) * tileHeight) / mapAtlasHeight;
    //    float yBottom = (((float)rowYup) * tileHeight) / mapAtlasHeight;

    //    return new Vector2[]
    //    {   new Vector2 (xLeft,yTop),
    //        new Vector2 (xRight,yTop),
    //        new Vector2 (xRight,yBottom),
    //        new Vector2 (xLeft,yBottom)
    //    };
    //}

    public static Vector2[] GetUVs(int col, int rowYup, int rotation)
    {
        float xLeft = (((float)col) * tileWidth) / mapAtlasWidth;
        float xRight = (((float)col + 1) * tileWidth) / mapAtlasWidth;
        float yTop = (((float)rowYup + 1) * tileHeight) / mapAtlasHeight;
        float yBottom = (((float)rowYup) * tileHeight) / mapAtlasHeight;

        if (rotation == 90)
        {
            return new Vector2[]
            {
                new Vector2 (xLeft,yBottom),
                new Vector2 (xLeft,yTop),
                new Vector2 (xRight,yBottom),
                new Vector2 (xLeft,yTop),
                new Vector2 (xRight,yTop),
                new Vector2 (xRight,yBottom)
            };
        }
        else if (rotation == 180)
        {
            return new Vector2[]
            {
                new Vector2 (xRight,yBottom),
                new Vector2 (xLeft,yBottom),
                new Vector2 (xRight,yTop),
                new Vector2 (xLeft,yBottom),
                new Vector2 (xLeft,yTop),
                new Vector2 (xRight,yTop)
            };
        }
        else if (rotation == 270)
        {
            return new Vector2[]
            {
                new Vector2 (xRight,yTop),
                new Vector2 (xRight,yBottom),
                new Vector2 (xLeft,yTop),
                new Vector2 (xRight,yBottom),
                new Vector2 (xLeft,yBottom),
                new Vector2 (xLeft,yTop)
            };
        }

        return new Vector2[]
        {
            new Vector2 (xLeft,yTop),
            new Vector2 (xRight,yTop),
            new Vector2 (xLeft,yBottom),
            new Vector2 (xRight,yTop),
            new Vector2 (xRight,yBottom),
            new Vector2 (xLeft,yBottom)

        };
    }

    public static Vector2[] GetUVs(int col, int rowYup)
    {
        float xLeft = (((float)col) * tileWidth) / mapAtlasWidth;
        float xRight = (((float)col + 1) * tileWidth) / mapAtlasWidth;
        float yTop = (((float)rowYup + 1) * tileHeight) / mapAtlasHeight;
        float yBottom = (((float)rowYup) * tileHeight) / mapAtlasHeight;

        return new Vector2[]
        {
            new Vector2 (xLeft,yTop),
            new Vector2 (xRight,yTop),
            new Vector2 (xLeft,yBottom),
            new Vector2 (xRight,yTop),
            new Vector2 (xRight,yBottom),
            new Vector2 (xLeft,yBottom)

        };
    }

    public static Vector2 GetUV00(int col, int rowYup)
    {
        Vector2 uv;
        uv.x = (((float)col) * tileWidth) / mapAtlasWidth;
        uv.y = (((float)rowYup) * tileHeight) / mapAtlasHeight;
        return uv;
    }
}
