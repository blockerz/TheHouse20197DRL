using RogueSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MapMesh : MonoBehaviour
{
    Mesh mapMesh;
    List<Vector3> vertices;
    List<Vector2> uvs;
    List<int> triangles;
    List<Color> colors;

    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = mapMesh = new Mesh();
        mapMesh.name = "Map Mesh";
        vertices = new List<Vector3>();
        uvs = new List<Vector2>();
        triangles = new List<int>();
        colors = new List<Color>();

        MapAtlas.Initialize();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateSegmentComponentMesh(Vector3 position, MapSegment segment)
    {
        mapMesh.Clear();
        vertices.Clear();
        uvs.Clear();
        triangles.Clear();
        colors.Clear();

        Color color = Color.white;

        MapSegmentTile.Tile[,] components = segment.GetSegmentTiles();

        for (int y = 0; y < MapSegment.SegmentWidthHeight; y++)
        {
            for (int x = 0; x < MapSegment.SegmentWidthHeight; x++)
            {
                //Color color = Color.red;

                //if (map.GetCell(x, y).IsWalkable)
                //{
                //    if (map.GetCell(x, y).IsTransparent)
                //    {
                //        color = Color.green;
                //    }
                //    else
                //    {
                //        color = Color.white;
                //    }
                //}

                CreateSegmentComponentQuad(position + new Vector3(x*3, -y*3, 0), new Vector3(3, 3, 0), color, components[x,y]);
            }
        }

        

        mapMesh.vertices = vertices.ToArray();
        mapMesh.uv = uvs.ToArray();
        mapMesh.triangles = triangles.ToArray();
        mapMesh.colors = colors.ToArray();
        mapMesh.RecalculateNormals();

    }

    private void CreateSegmentComponentQuad(Vector3 position, Vector3 size, Color color, MapSegmentTile.Tile tile)
    {
        Vector3 topLeftCorner = position;
        Vector3 width = new Vector3(size.x, 0, 0);
        Vector3 height = new Vector3(0, size.y, 0);


        AddTriangle(topLeftCorner, topLeftCorner + width, topLeftCorner - height);
        AddTriangleColor(color);
        AddTriangle(topLeftCorner + width, topLeftCorner + width - height, topLeftCorner - height);
        AddTriangleColor(color);
        AddUVs(tile);
    }

    public void CreateSegmentMesh(Vector3 position, MapSegment segment)
    {
        mapMesh.Clear();
        vertices.Clear();
        uvs.Clear();
        triangles.Clear();
        colors.Clear();

        int column = 0;
        int rowyUp = 0;

        Color color = Color.white;
        
        CreateSegmentQuad(position, new Vector3(9, 9, 0), color, column, rowyUp);                

        mapMesh.vertices = vertices.ToArray();
        mapMesh.uv = uvs.ToArray();
        mapMesh.triangles = triangles.ToArray();
        mapMesh.colors = colors.ToArray();
        mapMesh.RecalculateNormals();

    }

    private void CreateSegmentQuad(Vector3 position, Vector3 size, Color color, int column, int row)
    {
        Vector3 topLeftCorner = position;
        Vector3 width = new Vector3(size.x, 0, 0);
        Vector3 height = new Vector3(0, size.y, 0);


        AddTriangle(topLeftCorner, topLeftCorner + width, topLeftCorner - height);
        AddTriangleColor(color);
        AddTriangle(topLeftCorner + width, topLeftCorner + width - height, topLeftCorner - height);
        AddTriangleColor(color);
        AddUVs(column, row);
    }

    public void CreateMapMesh(Vector3 position, Map map)
    {
        mapMesh.Clear();
        vertices.Clear();
        uvs.Clear();
        triangles.Clear();
        colors.Clear();

        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                Color color = Color.red;

                if (map.GetCell(x, y).IsWalkable)
                {
                    if (map.GetCell(x, y).IsTransparent)
                    {
                        color = Color.green;
                    }
                    else
                    {
                        color = Color.white;
                    }
                }                

                CreateMapQuad(position + new Vector3(x, -y, 0), new Vector3(1,1,0), color);
                
            }
        }

        mapMesh.vertices = vertices.ToArray();
        mapMesh.uv = uvs.ToArray();
        mapMesh.triangles = triangles.ToArray();
        mapMesh.colors = colors.ToArray();
        mapMesh.RecalculateNormals();
        
    }

    private void CreateMapQuad(Vector3 position, Vector3 size, Color color)
    {
        Vector3 topLeftCorner = position;
        Vector3 width = new Vector3(size.x, 0, 0);
        Vector3 height = new Vector3(0, size.y, 0);


        AddTriangle(topLeftCorner, topLeftCorner + width, topLeftCorner - height);
        AddTriangleColor(color);
        AddTriangle(topLeftCorner + width, topLeftCorner + width - height, topLeftCorner - height);
        AddTriangleColor(color);
    }    

    void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }

    void AddUVs(MapSegmentTile.Tile tile)
    {
        Vector2[] uv = MapAtlas.GetUVs(tile.templateColumn, tile.templateRow, (int)tile.rotation);
        foreach (var u in uv)
        {
            //Debug.Log("Adding UV: " + u.x + ", " + u.y);
            uvs.Add(u);
        }
    }

    void AddUVs(int column, int row)
    {
        Vector2[] uv = MapAtlas.GetUVs(column, row);
        foreach(var u in uv)
        {
            //Debug.Log("Adding UV: " + u.x + ", " + u.y);
            uvs.Add(u);
        }
    }

    void AddTriangleColor(Color color)
    {
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
    }

    bool GetTileNeighbor(byte id, byte bitmask)
    {

        return (id & bitmask) != 0;            

    }

    //public void CreateMesh(MapModel[] mapModel)
    //{
    //    mapMesh.Clear();
    //    vertices.Clear();
    //    triangles.Clear();
    //    colors.Clear();

    //    for (int i = 0; i < mapModel.Length; i++)
    //    {
    //        CreateChunk(mapModel[i]);
    //    }

    //    mapMesh.vertices = vertices.ToArray();
    //    mapMesh.triangles = triangles.ToArray();
    //    mapMesh.colors = colors.ToArray();
    //    mapMesh.RecalculateNormals();

    //}

    //private void CreateChunk(MapModel mapChunk)
    //{
    //    Vector3 bottomLeftCorner = mapChunk.position;
    //    Vector3 width = new Vector3(MapConstants.MapSegmentWidth / mapChunk.SegmentWidth, 0, 0);
    //    Vector3 height = new Vector3(0, MapConstants.MapSegmentHeight / mapChunk.SegmentHeight, 0);

    //    // var values = Enum.GetValues(typeof(MapConstants.SegmentTileIndex3x3Ordered));
    //    int[] tileOrder = { 32, 16, 8, 64, 255, 4, 128, 1, 2 };

    //    int value = 0;



    //    for (int y = 0; y < mapChunk.SegmentHeight; y++)
    //    {
    //        for (int x = 0; x < mapChunk.SegmentWidth; x++)
    //        {
    //            Color color = mapChunk.color;

    //            //Debug.Log("MapID: " + mapChunk.ID + " Value: " + tileOrder[value]);

    //            if ((mapChunk.ID & tileOrder[value]) != 0)
    //            {
    //                color = Color.green;
    //            }

    //            AddTriangle(bottomLeftCorner, bottomLeftCorner + height, bottomLeftCorner + width);
    //            AddTriangleColor(color);
    //            AddTriangle(bottomLeftCorner + width, bottomLeftCorner + height, bottomLeftCorner + width + height);
    //            AddTriangleColor(color);
    //            bottomLeftCorner += width;
    //            value++;
    //        }
    //        bottomLeftCorner.x = mapChunk.position.x;
    //        bottomLeftCorner += height;
    //    }


    //    //AddTriangle(bottomLeftCorner + MapConstants.Corners[0], bottomLeftCorner + MapConstants.Corners[1], bottomLeftCorner + MapConstants.Corners[2]);
    //    //AddTriangle(bottomLeftCorner + MapConstants.Corners[0], bottomLeftCorner + MapConstants.Corners[2], bottomLeftCorner + MapConstants.Corners[3]);
    //}
}
