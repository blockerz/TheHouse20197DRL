using UnityEngine;
using UnityEngine.UI;

public class MapView : MonoBehaviour
{
    public MapMesh mapMeshPrefab;
    public Text mapSegmentTextPrefab;
    public SpriteRenderer segmentTemplatePrefab;

    private Color defaultColor = Color.white;
    private Canvas mapCanvas;

    void Awake()
    {
        mapCanvas = GetComponentInChildren<Canvas>();        
    }

    private void Start()
    {
        //CreateMapView(firstMap);
    }

    public void CreateMapView(DungeonMap dungeonMap)
    {

        Vector3 parentPosition;
        parentPosition.x = 0;
        parentPosition.y = 0;
        parentPosition.z = 0;

        GameObject mapParent = new GameObject("Map Mesh");
        mapParent.transform.position = parentPosition;
        mapParent.transform.parent = this.transform;

        GameObject segmentParent = new GameObject("Segment Mesh");
        segmentParent.transform.position = parentPosition;
        segmentParent.transform.parent = this.transform;

        GameObject segmentTemplateParent = new GameObject("Segment Template Mesh");
        segmentParent.transform.position = parentPosition;
        segmentParent.transform.parent = this.transform;



        for (int y = 0; y < dungeonMap.SegmentHeight; y++)
        {
            for (int x = 0; x < dungeonMap.SegmentWidth; x++)
            {
                Vector3 childPosition;
                childPosition.x = x * MapConstants.MapSegmentWidth;
                childPosition.y = y * -MapConstants.MapSegmentHeight;
                childPosition.z = 0;

                MapMesh mesh = Instantiate<MapMesh>(mapMeshPrefab);
                mesh.name = "Map X_" + x + " Y_" + y + " ID_" + dungeonMap.segments[x, y].ID;
                mesh.transform.parent = mapParent.transform;
                mesh.transform.position = parentPosition;
                mesh.CreateMapMesh(childPosition, dungeonMap.CreateSegmentMap(x,y));

                childPosition.z = -0.1f;
                MapMesh mesh2 = Instantiate<MapMesh>(mapMeshPrefab);
                mesh2.name = "Segment X_" + x + " Y_" + y + " ID_" + dungeonMap.segments[x, y].ID;
                mesh2.transform.parent = segmentParent.transform;
                mesh2.transform.position = parentPosition;
                mesh2.CreateSegmentComponentMesh(childPosition, dungeonMap.segments[x, y]);

                if (dungeonMap.segments[x, y].template.ThemeID != 0)
                {
                    Vector3 templatePosition = new Vector3(childPosition.x + MapSegment.SegmentCellWidthHeight / 2f, childPosition.y - MapSegment.SegmentCellWidthHeight / 2f, -0.2f);
                    SpriteRenderer sprite1 = Instantiate<SpriteRenderer>(segmentTemplatePrefab);
                    sprite1.name = "Segment Template X_" + x + " Y_" + y + " ID_" + dungeonMap.segments[x, y].ID;
                    sprite1.transform.parent = segmentTemplateParent.transform;
                    sprite1.transform.position = templatePosition;
                    sprite1.sprite = Resources.Load<Sprite>("Art/Theme1/" + dungeonMap.segments[x, y].template.Image);

                    
                    sprite1.transform.rotation = Quaternion.Euler(Vector3.forward * SegmentTemplate.GetTemplateSpriteRotation(dungeonMap.segments[x, y].template, dungeonMap.segments[x, y].ID));
                }
                
                CreateMapSegmentTextOverlay(childPosition, dungeonMap.segments[x, y].ID);
            }
        }
    }

    private void CreateMapSegmentTextOverlay(Vector3 position, int segment)
    {
        position.x += (0.5f * MapConstants.MapSegmentWidth);
        position.y -= (0.5f * MapConstants.MapSegmentHeight);     

        Text label = Instantiate<Text>(mapSegmentTextPrefab);
        label.name = "Segment: " + segment + " Position: " + position.x + ", " + position.y;
        label.rectTransform.SetParent(mapCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.y);
        label.text = segment.ToString();
    }

    void Update()
    {

    }
}
