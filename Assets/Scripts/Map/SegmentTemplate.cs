using System;
using System.Collections.Generic;
using System.Text;
using RogueSharp;
using RogueSharp.MapCreation;
using UnityEngine;

public class SegmentTemplate
{
    public static List<SegmentTemplateData> TemplateData = new List<SegmentTemplateData>
    {
        new SegmentTemplateData
        {
            ThemeID = 1,
            SegmentID = new List<int> {1, 4, 16, 64},
            Image = "Hole_1_1",
            Definition =
                "####s####\n" +
                "#.......#\n" +
                "#.......#\n" +
                "#..ooo..#\n" +
                "#..ooo..#\n" +
                "#..ooo..#\n" +
                "#.......#\n" +
                "#.......#\n" +
                "#########"
        },

        new SegmentTemplateData
        {
            ThemeID = 1,
            SegmentID = new List<int> {85,-1,-1,-1},
            Image = "LivingRoom_1_85",
            Definition =
                "####s####\n" +
                "#.......#\n" +
                "#.......#\n" +
                "###.....#\n" +
                "s.#..oo.s\n" +
                "#.#..oo.#\n" +
                "#.#..oo.#\n" +
                "#.......#\n" +
                "####s####"
        },

        new SegmentTemplateData
        {
            ThemeID = 1,
            SegmentID = new List<int> {5, 20, 80, 65},
            Image = "Knights_1_5",
            Definition =
                "####s####\n" +
                "###...###\n" +
                "###...###\n" +
                "###.....#\n" +
                "##o.....s\n" +
                "##o.....#\n" +
                "###oo####\n" +
                "#########\n" +
                "#########"
        },

        new SegmentTemplateData
        {
            ThemeID = 1,
            SegmentID = new List<int> {7, 28, 112, 193},
            Image = "Pentagram_1_7",
            Definition =
                "#........\n" +
                "#........\n" +
                "#........\n" +
                "#........\n" +
                "#........\n" +
                "#........\n" +
                "#........\n" +
                "#........\n" +
                "#########"
        },

        new SegmentTemplateData
        {
            ThemeID = 1,
            SegmentID = new List<int> {7, 28, 112, 193},
            Image = "Table_1_7",
            Definition =
                "#........\n" +
                "#........\n" +
                "#..oooo..\n" +
                "#..oooo..\n" +
                "#..oooo..\n" +
                "#..oooo..\n" +
                "#........\n" +
                "#........\n" +
                "#########"
        },

        new SegmentTemplateData
        {
            ThemeID = 1,
            SegmentID = new List<int> {17, 68, -1, -1},
            Image = "Hallway_1_17",
            Definition =
                "####s####\n" +
                "####.####\n" +
                "####.####\n" +
                "####.####\n" +
                "####.####\n" +
                "####.####\n" +
                "####.####\n" +
                "####.####\n" +
                "####s####"
        },

        new SegmentTemplateData
        {
            ThemeID = 1,
            SegmentID = new List<int> {17, 68, -1,  -1},
            Image = "HallwayRug_1_17",
            Definition =
                "####s####\n" +
                "####.####\n" +
                "####.####\n" +
                "####.####\n" +
                "####.####\n" +
                "####.####\n" +
                "####.####\n" +
                "####.####\n" +
                "####s####"
        },

        new SegmentTemplateData
        {
            ThemeID = 1,
            SegmentID = new List<int> {21, 84, 81, 69},
            Image = "THall_1_21",
            Definition =
                "####s####\n" +
                "####.####\n" +
                "####.####\n" +
                "####.####\n" +
                "####....s\n" +
                "####.####\n" +
                "####.####\n" +
                "####.####\n" +
                "####s####"
        },

        new SegmentTemplateData
        {
            ThemeID = 1,
            SegmentID = new List<int> {21, 84, 81, 69},
            Image = "THall_Skeletons_1_21",
            Definition =
                "####s####\n" +
                "#oo#.####\n" +
                "#..#.####\n" +
                "#..#.####\n" +
                "#.......s\n" +
                "#..#.####\n" +
                "#..#.####\n" +
                "#oo#.####\n" +
                "####s####"
        },

        new SegmentTemplateData
        {
            ThemeID = 1,
            SegmentID = new List<int> {23, 92, 113, 197},
            Image = "Fireplace_1_23",
            Definition =
                "#........\n" +
                "#........\n" +
                "#o.......\n" +
                "#o.......\n" +
                "#o.......\n" +
                "#........\n" +
                "#........\n" +
                "#........\n" +
                "####s####"
        },

        new SegmentTemplateData
        {
            ThemeID = 1,
            SegmentID = new List<int> {31, 124, 241, 199},
            Image = "Altar_1_31",
            Definition =
                "#........\n" +
                "#........\n" +
                "#........\n" +
                "#........\n" +
                "#..ooo...\n" +
                "#..ooo...\n" +
                "#..ooo...\n" +
                "#........\n" +
                "#........"
        },
        new SegmentTemplateData
        {
            ThemeID = 1,
            SegmentID = new List<int> {119,221,-1,-1},
            Image = "Checker_1_119",
            Definition =
                "#........\n" +
                ".........\n" +
                ".........\n" +
                ".........\n" +
                ".........\n" +
                ".........\n" +
                ".........\n" +
                ".........\n" +
                "........#"
        },
        new SegmentTemplateData
        {
            ThemeID = 1,
            SegmentID = new List<int> {127,253,247,223},
            Image = "Prison_1_127",
            Definition =
                "#........\n" +
                ".oooooo#.\n" +
                ".o.....#.\n" +
                ".o.....#.\n" +
                ".o.....#.\n" +
                ".o.....#.\n" +
                ".o.....#.\n" +
                ".#######.\n" +
                "........."
        },
        new SegmentTemplateData
        {
            ThemeID = 1,
            SegmentID = new List<int> {255,-1,-1,-1},
            Image = "Deco_1_255",
            Definition =
                ".........\n" +
                ".........\n" +
                ".........\n" +
                ".........\n" +
                ".........\n" +
                ".........\n" +
                ".........\n" +
                ".........\n" +
                "........."
        },
        new SegmentTemplateData
        {
            ThemeID = 1,
            SegmentID = new List<int> {255,-1,-1,-1},
            Image = "Skull_1_255",
            Definition =
                ".........\n" +
                ".........\n" +
                ".........\n" +
                ".........\n" +
                ".........\n" +
                ".........\n" +
                ".........\n" +
                ".........\n" +
                "........."
        },
        new SegmentTemplateData
        {
            ThemeID = 1,
            SegmentID = new List<int> {255,-1,-1,-1},
            Image = "Pit_1_255",
            Definition =
                ".........\n" +
                ".oo.oooo.\n" +
                ".o.....o.\n" +
                ".o.....o.\n" +
                ".o.....o.\n" +
                ".o.....o.\n" +
                ".o.....o.\n" +
                ".ooooooo.\n" +
                "........."
        },
        new SegmentTemplateData
        {
            ThemeID = 1,
            SegmentID = new List<int> {95,125,245,215},
            Image = "Pillars_1_95",
            Definition =
                "#........\n" +
                "#........\n" +
                "##.......\n" +
                "#........\n" +
                "s........\n" +
                "#........\n" +
                "##.......\n" +
                "#........\n" +
                "#........"
        },
    };

    public Map map;
    public const int Width = 9;
    public const int Height = 9;

    public SegmentTemplate()
    {

    }


    public static List<SegmentTemplateData> FindMatchingSegmentTemplates(int themeID, int segmentID)
    {
        return TemplateData.FindAll(data => data.ThemeID == themeID && data.SegmentID.Contains(segmentID));
    }


    public static Map GenerateMapFromTemplate(SegmentTemplateData template, int segmentID)
    {
        int rotation = template.SegmentID.IndexOf(segmentID);

        string definition = template.Definition;

        if (rotation == 1)
        {
            definition = RotateDefinitionCW(definition);
        }
        else if (rotation == 2)
        {
            definition = RotateDefinitionCW(definition);
            definition = RotateDefinitionCW(definition);
            //definition = FlipDefinition(definition);
        }
        else if (rotation == 3)
        {
            definition = RotateDefinitionCCW(definition);
        }

        StringDeserializeMapCreationStrategy<Map> strategy =
            new StringDeserializeMapCreationStrategy<Map>(definition);

        Map map = strategy.CreateMap();

        return map;
    }

    public static string FlipDefinition(string definition)
    {
        string[] split = definition.Split('\n');

        StringBuilder builder = new StringBuilder();

        for (int s = split.Length - 1; s >= 0; s--)
        {
            builder.Append(split[s]);

            if (s > 0)
                builder.Append('\n');
        }
        return builder.ToString();
    }

    public static string RotateDefinitionCW(string definition)
    {
        string[] split = definition.Split('\n');

        StringBuilder builder = new StringBuilder();

        for (int x = 0; x < split.Length; x++)
        {
            for (int y = split.Length - 1; y >= 0; y--)
            {
                builder.Append((split[y])[x]);
            }

            if (x < split.Length - 1)
                builder.Append('\n');
        }

        return builder.ToString();
    }

    public static string RotateDefinitionCCW(string definition)
    {
        string[] split = definition.Split('\n');

        StringBuilder builder = new StringBuilder();

        for (int x = split.Length - 1; x >= 0; x--)
        {
            for (int y = 0; y < split.Length; y++)
            {
                builder.Append((split[y])[x]);
            }

            if (x > 0)
                builder.Append('\n');
        }
        return builder.ToString();
    }

    public static int GetTemplateSpriteRotation(SegmentTemplateData template, int ID)
    {

        int rotation = template.SegmentID.IndexOf(ID);

        if (rotation == 1)
        {
            return 270;
        }
        else if (rotation == 2)
        {
            return 180;
        }
        else if (rotation == 3)
        {
            return 90;
        }

        return 0;
    }
}
