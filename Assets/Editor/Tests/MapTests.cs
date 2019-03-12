using NUnit.Framework;
using RogueSharp;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MapTests
{
    // A Test behaves as an ordinary method
    //[Test]
    //public void MapTestsSimplePasses()
    //{
    //    // Use the Assert class to test conditions
    //}

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    //[UnityTest]
    //public IEnumerator MapTestsWithEnumeratorPasses()
    //{
    //    // Use the Assert class to test conditions.
    //    // Use yield to skip a frame.
    //    yield return null;
    //}

    //[Test]
    //public void EdgesMatch()
    //{
    //    bool match = MapSegment.EdgesMatch(MapSegment.Direction.West, 64, 245);
    //    Assert.AreEqual(true, match);

    //    //match = MapSegment.EdgesMatch(MapSegment.Direction.North, 64, 247);
    //    //Assert.AreEqual(true, match);

    //    List<byte> filter = new List<byte>(MapSegment.SegmentIDs);

    //    MapModel map = new MapModel();

    //    map.FilterSegments(filter, MapSegment.Direction.North, 5);
    //    map.FilterSegments(filter, MapSegment.Direction.East, 29);
    //    map.FilterSegments(filter, MapSegment.Direction.South, 255);
    //    map.FilterSegments(filter, MapSegment.Direction.West, 85);
    //    //Assert.AreEqual(true, filter.Contains(64));
    //    Assert.AreEqual(true, filter.Count > 0);
    //}

    [Test]
    public void FloodFill()
    {
        GameManager gm = new GameManager();
        Game g = new Game(null, 1);

        g.GenerateMap();
        string map = "";

        for (int y = 0; y < g.World.floodMap.GetLength(1); y++)
        {
            for (int x = 0; x < g.World.floodMap.GetLength(0); x++)
            {
                if (g.World.floodMap[x, y] < 10)
                    map += "0";
                map += g.World.floodMap[x, y] + " ";
            }
            map += "\n";
        }
        Debug.Log(map);

        foreach(Room r in g.World.Rooms)
        {
            Debug.Log("Room " + r.ID + " - Position: " + r.BBox.X + ", " + r.BBox.Y + " Size: " + r.BBox.Width + ", " + r.BBox.Height);
            Debug.Log(r.Map.ToString());
        }
        foreach (Door d in g.World.Doors)
        {
            Debug.Log("Door " + d.RoomID + " - Position: " + d.X + ", " + d.Y + " connection (" + d.ConnectRoomID + ") : " + d.connectionX + ", " + d.connectionY);
        }
    }

    [Test]
    public void SegmentTemplateTest()
    {
        List<SegmentTemplateData> templates = SegmentTemplate.FindMatchingSegmentTemplates(1, 1);

        Console.WriteLine("Templates Found: " + templates.Count);

        Map map = SegmentTemplate.GenerateMapFromTemplate(templates[0], 0);
        Debug.Log(map.ToString());

        map = SegmentTemplate.GenerateMapFromTemplate(templates[0], 90);
        Debug.Log(map.ToString());

        map = SegmentTemplate.GenerateMapFromTemplate(templates[0], 180);
        Debug.Log(map.ToString());

        map = SegmentTemplate.GenerateMapFromTemplate(templates[0], 270);
        Debug.Log(map.ToString());
    }
}

