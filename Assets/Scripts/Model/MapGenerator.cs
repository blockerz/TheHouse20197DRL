using System;
using System.Linq;
using System.Collections.Generic;
using RogueSharp;
using RogueSharp.DiceNotation;

public class MapGenerator
{
    private readonly int        segmentWidth;
    private readonly int        segmentHeight;
    private readonly int        maxRooms;
    private readonly int        roomMaxSize;
    private readonly int        roomMinSize;

    private DungeonMap map;
    private Game       game;

    public MapSegment[,] segments;

    public int emptySegments = 0;
    public int smallSegments = 0;
    private float failedSegmentThresholdPercent = 0.4f;
    private float smallSegmentThresholdPercent = 0.4f;

    public int startXsegment = 0;
    public int startYsegment = 0;

    public int playerXsegment = 0;
    public int playerYsegment = 0;


    private int mapAttemptsMax = 100;
    public int mapAttempts = 0;

    private List<MapSegment> segmentsVisited;
    private List<Point> pointsToBeVisited;

    private Point[] neighbors =
    {
        new Point(0, 1),
        new Point(1,0),
        new Point(0,-1),
        new Point(-1,0)
    };

    MapSegment.Direction[] directions =
    {
        MapSegment.Direction.South,
        MapSegment.Direction.East,
        MapSegment.Direction.North,
        MapSegment.Direction.West
    };

    public MapGenerator(Game game, int segmentWidth, int segmentHeight)
    {
        this.segmentWidth = segmentWidth;
        this.segmentHeight = segmentHeight;
        this.game = game;

        segmentsVisited = new List<MapSegment>();
        pointsToBeVisited = new List<Point>();

    }

    public DungeonMap GenerateMap()
    {
        bool success = false;

        map = new DungeonMap(game);

        mapAttempts = 0; 

        do
        {
            mapAttempts++;
            success = CreateMap(segmentWidth, segmentHeight);

        }
        while (!success || emptySegments > failedSegmentThresholdPercent * (segmentWidth * segmentHeight) ||
                 smallSegments > smallSegmentThresholdPercent * (segmentWidth * segmentHeight));

        ApplySegmentTemplates();

        Point p1 = map.GetRandomWalkableLocationInRoom(map.segments[startXsegment, startYsegment].BBox);

        CreateRooms(map, p1.X, p1.Y);

        //CreateRooms(map, startXsegment * MapSegment.SegmentCellWidthHeight + (MapSegment.SegmentCellWidthHeight / 2),
        //                 startYsegment * MapSegment.SegmentCellWidthHeight + (MapSegment.SegmentCellWidthHeight / 2));

        CreateStairs();
        PlacePlayer(game);
        PlaceMonsters();
        PlaceItems();

        return map;
    }



    private bool CreateMap(int segmentWidth, int segmentHeight)
    {
        segments = new MapSegment[segmentWidth, segmentHeight];

        map.Initialize(segmentWidth * MapSegment.SegmentCellWidthHeight, segmentHeight * MapSegment.SegmentCellWidthHeight);

        do
        {
            startXsegment = Game.Random.Next(0, segmentWidth-1);
            startYsegment = Game.Random.Next(0, segmentHeight-1);
        }
        while (segments[startXsegment, startYsegment] != null);

        segmentsVisited.Clear();


        MapSegment segment = GetConstrainedSegment(segments, new Point(startXsegment, startYsegment));

        if (segment == null || segment.ID == 0)
            return false;

        segment.x = startXsegment;
        segment.y = startYsegment;

        segments[startXsegment, startYsegment] = segment;
        segmentsVisited.Add(segment);

        while (segmentsVisited.Count > 0)
        {
            MapSegment visitedSegment = segmentsVisited[segmentsVisited.Count - 1];

            pointsToBeVisited.Clear();

            for (int p = 0; p < neighbors.Length; p++)
            {
                Point pointToVisit = new Point(visitedSegment.x + neighbors[p].X,
                    visitedSegment.y + neighbors[p].Y);

                if (pointToVisit.X >= 0 &&
                    pointToVisit.X < segmentWidth &&
                    pointToVisit.Y >= 0 &&
                    pointToVisit.Y < segmentHeight)
                {
                    if (segments[pointToVisit.X, pointToVisit.Y] == null)
                    {

                        if (visitedSegment.EdgeIsOpen(directions[p]))
                        {
                            pointsToBeVisited.Add(pointToVisit);
                        }
                    }
                }
            }

            if (pointsToBeVisited.Count > 0)
            {
                Point point;

                int index = Game.Random.Next(0, pointsToBeVisited.Count-1);
                point = pointsToBeVisited[index];


                segment = GetConstrainedSegment(segments, point);

                if (segment == null)
                    return false;

                segment.x = point.X;
                segment.y = point.Y;

                segments[point.X, point.Y] = segment;
                segmentsVisited.Add(segment);

                //Debug.Log("Created Segment " + segment.ID + " at " + point.X + ", " + point.Y);
            }
            else
            {
                segmentsVisited.Remove(visitedSegment);                
            }

        }

        emptySegments = 0;
        smallSegments = 0;

        for (int y = 0; y < segmentHeight; y++)
        {
            for (int x = 0; x < segmentWidth; x++)
            {
                if (segments[x, y] == null) // || segments[x, y].ID == 0)
                {

                    segment = new MapSegment(0);

                    segment.x = x;
                    segment.y = y;

                    segments[x, y] = segment;

                }

                if (segments[x, y].ID == 0)
                {
                    emptySegments++;
                }

                if (MapSegment.smallSegments.Contains(segments[x, y].ID))
                    smallSegments++;

                map.Copy(MapSegmentTile.GetSegmentMap(segments[x, y].ID),
                    x * MapSegment.SegmentCellWidthHeight,
                    y * MapSegment.SegmentCellWidthHeight);
            }
        }        

        map.SegmentWidth = segmentWidth;
        map.SegmentHeight = segmentHeight;
        map.segments = segments;

        return true;
    }

    private void ApplySegmentTemplates()
    {
        for (int y = 0; y < segmentHeight; y++)
        {
            for (int x = 0; x < segmentWidth; x++)
            {
                // chance of not applying template
                if (Game.Random.Next(100) > 50)
                    continue; 

                if (segments[x, y] != null) // || segments[x, y].ID == 0)
                {
                    List<SegmentTemplateData> templates = SegmentTemplate.FindMatchingSegmentTemplates(1, segments[x, y].ID);

                    if (templates.Count > 0)
                    {
                        int index = Game.Random.Next(templates.Count-1);

                        Map templateMap = SegmentTemplate.GenerateMapFromTemplate(templates[index], segments[x, y].ID);

                        map.Copy(templateMap,
                                x * MapSegment.SegmentCellWidthHeight,
                                y * MapSegment.SegmentCellWidthHeight);

                        segments[x, y].template = templates[index];
                    }
                }
            }
        }
    }

    private void CreateRooms(DungeonMap map, int startX, int startY)
    {

        map.ResetRooms();

        //int[,] flood = FloodFill(map, new Point(startX, startY));        
        map.floodMap = new int[map.Width, map.Height]; 
        FloodFill(map, new Point(startX, startY), 1);
    }

    private int FloodFill(DungeonMap map, Point start, int fill)
    {        
        int nextFill = fill;
        int xMin = int.MaxValue;
        int yMin = int.MaxValue;
        int xMax = 0;
        int yMax = 0;

        Stack<Point> stack = new Stack<Point>();
        Queue<Point> doors = new Queue<Point>();

        stack.Push(start);

        while (stack.Count > 0)
        {
            Point a = stack.Pop();
            if (a.X < map.floodMap.GetLength(0) && a.X > 0 &&
                    a.Y < map.floodMap.GetLength(1) && a.Y > 0)
            {
                if (map.floodMap[a.X, a.Y] == 0 && map.GetCell(a.X, a.Y).IsTransparent)
                {
                    map.floodMap[a.X, a.Y] = fill;
                    stack.Push(new Point(a.X - 1, a.Y));
                    stack.Push(new Point(a.X + 1, a.Y));
                    stack.Push(new Point(a.X, a.Y - 1));
                    stack.Push(new Point(a.X, a.Y + 1));

                    if (a.X < xMin) xMin = a.X;
                    if (a.Y < yMin) yMin = a.Y;
                    if (a.X > xMax) xMax = a.X;
                    if (a.Y > yMax) yMax = a.Y;
                }
                else if (map.floodMap[a.X, a.Y] == 0 && map.GetCell(a.X, a.Y).IsWalkable)
                {
                    map.floodMap[a.X, a.Y] = fill;

                    // Include doors in room?
                    //if (a.X < xMin) xMin = a.X;
                    //if (a.Y < yMin) yMin = a.Y;
                    //if (a.X > xMax) xMax = a.X;
                    //if (a.Y > yMax) yMax = a.Y;

                    if (a == start)
                    {
                        stack.Push(new Point(a.X - 1, a.Y));
                        stack.Push(new Point(a.X + 1, a.Y));
                        stack.Push(new Point(a.X, a.Y - 1));
                        stack.Push(new Point(a.X, a.Y + 1));
                    }

                    Point neighbor = new Point(-1, -1);
                    int connectID = 0;

                    foreach (Point n in neighbors)
                    {
                        Point temp = a + n;

                        if (temp.X < 0 || temp.Y < 0 || temp.X > map.Width - 1 || temp.Y > map.Height - 1)
                            continue; 

                        if (!map.GetCell(temp.X, temp.Y).IsTransparent && map.GetCell(temp.X, temp.Y).IsWalkable)
                        {
                            neighbor = a + n;

                            if (map.floodMap[temp.X, temp.Y] == 0)
                                doors.Enqueue(neighbor);
                            else
                                connectID = map.floodMap[temp.X, temp.Y];
}
                    }

                    map.Doors.Add(new Door(game)
                    {
                        RoomID = fill,
                        ConnectRoomID = connectID,
                        X = a.X,
                        Y = a.Y,
                        connectionX = neighbor.X,
                        connectionY = neighbor.Y,
                        IsOpen = false
                    });

                }
            }
        }

        Room room = new Room(game)
        {
            ID = fill,
            BBox = new Rectangle()
            {
                X = xMin,
                Y = yMin,
                Width = xMax - xMin,
                Height = yMax - yMin
            }
        };

        room.Map = new Map (room.BBox.Width, room.BBox.Height);

        for (int y = yMin, y1 = 0; y < yMax; y++, y1++)
        {
            for (int x = xMin, x1 = 0; x < xMax; x++, x1++)
            {
                if (map.floodMap[x, y] == fill)
                {
                    room.Map.SetCellProperties(x1, y1, true, true);
                }
            }
        }

        map.Rooms.Add(room);

        while (doors.Count > 0)
        {
            Point door = doors.Dequeue();

            if (map.floodMap[door.X, door.Y] > 0)
                continue;

            nextFill = FloodFill(map, door, ++nextFill);

        }

        return nextFill;
    }

    private MapSegment GetConstrainedSegment(MapSegment[,] existingSegments, Point pointToAdd)
    {

        List<byte> filter = new List<byte>(MapSegment.SegmentIDs);

        string debug = "Neighbors for (" + pointToAdd.X + ", " + pointToAdd.Y + "): ";

        for (int p = 0; p < neighbors.Length; p++)
        {
            Point neighborPoint = neighbors[p] + pointToAdd;

            if (neighborPoint.X >= 0 &&
                neighborPoint.X < existingSegments.GetLength(0) &&
                neighborPoint.Y >= 0 &&
                neighborPoint.Y < existingSegments.GetLength(1))
            {
                if (existingSegments[neighborPoint.X, neighborPoint.Y] != null)
                {
                    debug += directions[p] + " - " + existingSegments[neighborPoint.X, neighborPoint.Y].ID + " ";
                    filter = FilterSegments(filter, directions[p],
                        existingSegments[neighborPoint.X, neighborPoint.Y].ID);
                }
            }
            else
            {
                filter = FilterSegments(filter, directions[p], 0);
            }  

        }

        if (filter.Count > 0)
        {
            debug += " Options available: ";
            foreach (var f in filter)
            {
                debug += f.ToString() + " ";
            }

            if (filter.Count > 1 && filter.Contains(0))
            {
                filter.Remove(0);
            }

            int e = 0;
            while (filter.Count > 1 && e < MapSegment.matchException.Count)
            {
                filter.Remove(MapSegment.matchException[e++]);
            }


            byte selectedSegment = filter[Game.Random.Next(0, filter.Count-1)];

            return new MapSegment(selectedSegment);
        }
        else
        {
            //filter.Add(0);
            //return new MapSegment(0);
            return null;
        }
    }

    public List<byte> FilterSegments(List<byte> segments, MapSegment.Direction direction, byte neighbor)
    {

        foreach (byte b in segments.ToList())
        {
            if (!MapSegment.EdgesMatch(direction, b, neighbor))
            {
                segments.Remove(b);
            }
        }

        return segments;
    }

    public List<byte> FilterSegments(List<byte> segments, byte[] edgeMask)
    {
        if (edgeMask == null || edgeMask.Length != 3)
            return segments;

        foreach (byte b in segments.ToList())
        {
            if ((edgeMask[0] & b) != edgeMask[0] ||
                (edgeMask[1] & b) != edgeMask[1] ||
                (edgeMask[2] & b) != edgeMask[2])
            {
                segments.Remove(b);                
            }
        }
        
        return segments;
    }

    /// <summary>
    /// Generate a new map that places rooms randomly.
    /// </summary>
    /// <returns></returns>
    //public DungeonMap CreateMap()
    //{          
    //    map.Initialize(width, height);                                                  // Set the properties of all cells to false

    //    for (int r = 0; r < maxRooms; r++)                                              // Try to place as many rooms as the specified maxRooms
    //    {
    //        int roomWidth = Game.Random.Next(roomMinSize, roomMaxSize);                 // Determine the size and position of the room randomly
    //        int roomHeight = Game.Random.Next(roomMinSize, roomMaxSize);
    //        int roomXPosition = Game.Random.Next(0, width - roomWidth - 1);
    //        int roomYPosition = Game.Random.Next(0, height - roomHeight - 1);

    //        var newRoom = new Rectangle(roomXPosition, roomYPosition, roomWidth, roomHeight);// All of our rooms can be represented as Rectangles
    //        bool newRoomIntersects = map.Rooms.Any(room => newRoom.Intersects(room));   // Check to see if the room rectangle intersects with any other rooms

    //        if (!newRoomIntersects)                                                     // As long as it doesn't intersect add it to the list of rooms
    //        {
    //            map.Rooms.Add(newRoom);
    //        }
    //    }

    //    for (int r = 1; r < map.Rooms.Count; r++)                                       // Iterate through each room that was generated
    //    {                                                                               // Don't do anything with the first room, so start at r = 1 instead of r = 0               
    //        int previousRoomCenterX = map.Rooms[r - 1].Center.X;                        // For all remaing rooms get the center of the room and the previous room
    //        int previousRoomCenterY = map.Rooms[r - 1].Center.Y;
    //        int currentRoomCenterX = map.Rooms[r].Center.X;
    //        int currentRoomCenterY = map.Rooms[r].Center.Y;

    //        if (Game.Random.Next(1, 2) == 1)                                            // Give a 50/50 chance of which 'L' shaped connecting hallway to tunnel out
    //        {
    //            CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, previousRoomCenterY);
    //            CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, currentRoomCenterX);
    //        }
    //        else
    //        {
    //            CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX);
    //            CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, currentRoomCenterY);
    //        }
    //    }

    //    foreach (Rectangle room in map.Rooms)                                           // Iterate through each room that we wanted placed call CreateRoom to make it
    //    {
    //        CreateRoom(room);
    //        CreateDoors(room);
    //    }

    //    CreateStairs();
    //    PlacePlayer(game);
    //    PlaceMonsters();

    //    return map;
    //}

    /// <summary>
    /// Find the center of the first room that we created and place the Player there.
    /// </summary>
    private void PlacePlayer(Game game)
    {
        Player player = game.Player;

        if (player == null)
        {
            player = new Player(game);
            player.Name = GameData.Data.PlayerName;

            if (GameData.Data.CurrentLevel == 0 && GameData.Data.LastLevel <= GameData.Data.CurrentLevel)
            {
                player.Health = player.Health + (int)(GameData.Data.Difficulty / 2);
                player.Attack = player.Attack + (int)(GameData.Data.Difficulty / 2);
            }
            else
            {
                player.Health = GameData.Data.PlayerHealth;
                player.MaxHealth = GameData.Data.PlayerMaxHealth;
                player.Attack = GameData.Data.PlayerAttack;
                player.Moves = GameData.Data.PlayerMoves;
                player.ItemsFound = GameData.Data.ItemsFound;
            }
        }

        if (GameData.Data.LastLevel > GameData.Data.CurrentLevel)
        {
            Rectangle stairArea = new Rectangle(map.StairsDown.X - 2, map.StairsDown.Y - 2, 5, 5);

            map.SetIsWalkable(map.StairsDown.X, map.StairsDown.Y, false);
            Point p = map.GetRandomWalkableLocationInRoom(stairArea);
            map.SetIsWalkable(map.StairsDown.X, map.StairsDown.Y, true);

            player.X = p.X;
            player.Y = p.Y;
        }
        else
        {
            Rectangle stairArea = new Rectangle(map.StairsUp.X - 2, map.StairsUp.Y - 2, 5, 5);

            map.SetIsWalkable(map.StairsUp.X, map.StairsUp.Y, false);
            Point p = map.GetRandomWalkableLocationInRoom(stairArea);
            map.SetIsWalkable(map.StairsUp.X, map.StairsUp.Y, true);

            player.X = p.X;
            player.Y = p.Y;
        }

        playerXsegment = player.X / MapSegment.SegmentCellWidthHeight;
        playerYsegment = player.Y / MapSegment.SegmentCellWidthHeight;

        map.AddPlayer(player);
    }

    /// <summary>
    /// Given a rectangular area on the map set the cell properties for that area to true.
    /// </summary>
    /// <param name="room"></param>
    private void CreateRoom(Rectangle room)
    {
        for (int x = room.Left + 1; x < room.Right; x++)
        {
            for (int y = room.Top + 1; y < room.Bottom; y++)
            {
                map.SetCellProperties(x, y, true, true, false);
            }
        }
    }

    /// <summary>
    /// Given a room with hallways dug out check to see if doors can be placed.
    /// </summary>
    /// <param name="room"></param>
    private void CreateDoors(Rectangle room)
    {
        int xMin = room.Left;                                                           // The the boundries of the room
        int xMax = room.Right;
        int yMin = room.Top;
        int yMax = room.Bottom;

        List<ICell> borderCells = new List<ICell>();                                    // Put the rooms border cells into a list
        borderCells.AddRange(map.GetCellsAlongLine(xMin, yMin, xMax, yMin));
        borderCells.AddRange(map.GetCellsAlongLine(xMin, yMin, xMin, yMax));
        borderCells.AddRange(map.GetCellsAlongLine(xMin, yMax, xMax, yMax));
        borderCells.AddRange(map.GetCellsAlongLine(xMax, yMin, xMax, yMax));

            
        foreach (Cell cell in borderCells)                                              // Go through each of the rooms border cells and look for locations to place doors.
        {
            if (IsPotentialDoor(cell))
            {
                map.SetCellProperties(cell.X, cell.Y, false, true);                    // A door must block field-of-view when it is closed.
                map.Doors.Add(new Door(game)
                {
                    X = cell.X,
                    Y = cell.Y,
                    IsOpen = false
                });
            }
        }
    }

    private void PlaceItems()
    {


        if (GameData.Data.LastLevel > GameData.Data.CurrentLevel)
        {
            if (GameData.Data.ItemsFound > GameData.Data.CurrentLevel*2)
                return;
        }

        int roomSize = map.Rooms.Count; 
        Point p1 = map.GetRandomWalkableLocationInRoom(map.Rooms[map.Rooms.Count/3].BBox);
        Point p2 = map.GetRandomWalkableLocationInRoom(map.Rooms[(map.Rooms.Count / 3) * 2].BBox);

        map.Chests = new List<Chest>();

        map.Chests.Add(new Chest(game) 
        {
            X = p1.X,
            Y = p1.Y
        });

        map.Chests.Add(new Chest(game)
        {
            X = p2.X,
            Y = p2.Y
        });
    }

    /// <summary>
    /// Create stairs up in the first room created and stairs down in the last room created.
    /// </summary>
    private void CreateStairs()
    {
        Point p1 = map.GetRandomWalkableLocationInRoom(map.Rooms.First().BBox);
        Point p2 = map.GetRandomWalkableLocationInRoom(map.Rooms.Last().BBox);

        if (GameData.Data.CurrentLevel != 0)
        {
            map.StairsUp = new Stairs(game)
            {
                X = p1.X,
                Y = p1.Y,
                IsUp = true,
                Sprite = "Art/StairsUpSprite"
            };
        }
        else
        {
            map.StairsUp = new Stairs(game)
            {
                X = p1.X,
                Y = p1.Y,
                Sprite = "Art/Transparent128",
                IsUp = true
            };
        }

        if (GameData.Data.CurrentLevel != GameData.Data.Levels - 1)
        {
            map.StairsDown = new Stairs(game)
            {
                X = p2.X,
                Y = p2.Y,
                IsUp = false
            };
        }        
        else
        {
            map.StairsDown = new Stairs(game)
            {
                X = p2.X,
                Y = p2.Y,
                Sprite = "Art/Transparent128",
                IsUp = false
            };
        }

    }

    /// <summary>
    /// Checks to see if a cell is a good candidate for placement of a door.
    /// </summary>
    /// <param name="cell"></param>
    /// <returns></returns>
    private bool IsPotentialDoor(Cell cell)
    {
        if (!cell.IsWalkable)                                                           // If the cell is not walkable then it is a wall and not a good place for a door
        {
            return false;
        }

        Cell right  = (Cell)map.GetCell(cell.X + 1, cell.Y);                            // Store references to all of the neighboring cells 
        Cell left   = (Cell)map.GetCell(cell.X - 1, cell.Y);
        Cell top    = (Cell)map.GetCell(cell.X, cell.Y - 1);
        Cell bottom = (Cell)map.GetCell(cell.X, cell.Y + 1);

        if (map.GetDoor(cell.X, cell.Y) != null ||
            map.GetDoor(right.X, right.Y) != null ||
            map.GetDoor(left.X, left.Y) != null ||
            map.GetDoor(top.X, top.Y) != null ||
            map.GetDoor(bottom.X, bottom.Y) != null)                                    // Make sure there is not already a door here
        {
            return false;
        }

        if (right.IsWalkable && left.IsWalkable && !top.IsWalkable && !bottom.IsWalkable)// This is a good place for a door on the left or right side of the room
        {
            return true;
        }

        if (!right.IsWalkable && !left.IsWalkable && top.IsWalkable && bottom.IsWalkable)// This is a good place for a door on the top or bottom of the room
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Carve a tunnel out of the map parallel to the x-axis.
    /// </summary>
    /// <param name="xStart"></param>
    /// <param name="xEnd"></param>
    /// <param name="yPosition"></param>
    private void CreateHorizontalTunnel(int xStart, int xEnd, int yPosition)
    {
        for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++)
        {
            map.SetCellProperties(x, yPosition, true, true);
        }
    }

    /// <summary>
    /// Carve a tunnel out of the map parallel to the y-axis.
    /// </summary>
    /// <param name="yStart"></param>
    /// <param name="yEnd"></param>
    /// <param name="xPosition"></param>
    private void CreateVerticalTunnel(int yStart, int yEnd, int xPosition)
    {
        for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++)
        {
            map.SetCellProperties(xPosition, y, true, true);
        }
    }

    /// <summary>
    /// Make a chance to place 1 to 4 monsters in every room except the starting room with the player.
    /// </summary>
    private void PlaceMonsters()
    {

        if (GameData.Data.CurrentLevel == GameData.Data.Levels - 1)
        {
            GenerateBeholder();
        }

        foreach (var segment in map.segments)
        {
            if (segment.x == playerXsegment && segment.y == playerYsegment)
                continue;

            if (Dice.Roll("1D10") <= 8)                                                  // Each seggment has a 70% chance of having monsters
            {                  
                var numberOfMonsters = Dice.Roll("1D4");                                // Generate between 1 and 4 monsters

                for (int i = 1; i < numberOfMonsters; i++)                              // Not starting at zero. Player is in room zero.
                {
                    Point randomRoomLocation = map.GetRandomWalkableLocationInRoom(segment.BBox);// Find a random walkable location in the room to place the monster                       
                        
                    if (randomRoomLocation != Point.Zero)                               // It's possible that the room doesn't have space to place a monster
                    {                                                                   // In that case skip creating the monster                           
                        var monster = MonsterGenerator.GetDepthAppropriateMonster(game, GameData.Data.Difficulty);                        // Temporarily hard code this monster to be created at level 1
                        monster.X = randomRoomLocation.X;
                        monster.Y = randomRoomLocation.Y;
                        map.AddMonster(monster);
                    }
                }
            }
        }
    }

    private void GenerateBeholder()
    {
        Monster[] beholder = MonsterGenerator.CreateBeholder(game, GameData.Data.Difficulty);
        Point p = map.GetBeholderLocationInMap();

        for (int y = p.Y, row = 0; y < p.Y + 3; y++, row++)
        {
            for (int x = p.X, col = 0; x < p.X + 3; x++, col++)
            {
                int index = col + (row * 3);
                beholder[index].X = x;
                beholder[index].Y = y;
                map.AddMonster(beholder[index]);

            }
        }

        game.beholder = beholder;
    }
}
