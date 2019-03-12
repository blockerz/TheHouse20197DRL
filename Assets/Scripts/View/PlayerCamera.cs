using RogueSharp;
using System.Collections.ObjectModel;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public SpriteRenderer fovSprite; 
    
    private Player player;
    private Game game; 

    private float xPos, yPos;

    private SpriteRenderer[,] fov;
    GameObject fovParent; 

    private void LateUpdate()
    {
        if (player != null)
        {
            xPos = player.X + 0.5f;
            yPos = -player.Y - 0.5f;
        }

        transform.position = new Vector3(xPos, yPos, -10);

        FieldOfView playerFOV = new FieldOfView(game.World);
        ReadOnlyCollection<ICell>  cells = playerFOV.ComputeFov(game.Player.X, game.Player.Y, 10, true);

        Vector3 fovRoot  = new Vector3(xPos - 9f, yPos - 7f, -3);
        fovParent.transform.position = fovRoot;

        fovRoot += new Vector3(-0.5f, 0.5f, 0);

        for (int y = fov.GetLength(1) - 1; y >= 0; y--)
        {
            for (int x = 0; x < fov.GetLength(0); x++)
            {                

                if (playerFOV.IsInFov((int)fovRoot.x + x, -(int)fovRoot.y - y))
                {
                    fov[x, y].color = new Color(fov[x, y].color.r,
                                            fov[x, y].color.g,
                                            fov[x, y].color.b,
                                            0f);
                }
                else
                {
                    fov[x, y].color = new Color(fov[x, y].color.r,
                                            fov[x, y].color.g,
                                            fov[x, y].color.b,
                                            1f);
                }
            }
        }
    }

    /// <summary>
    /// Init Camera and position Camera over player.
    /// </summary>
    /// <param name="player"></param>
    public void InitCamera(Game game)
    {
        this.game = game;
        this.player = game.Player;
        xPos = player.X;
        yPos = -player.Y;
        transform.position = new Vector3(player.X, player.Y, -10);

        fov = new SpriteRenderer[19, 15]; 
        
        if (fovParent != null)
        {
            Destroy(fovParent);
        }

        fovParent = new GameObject("FOV");
        fovParent.transform.position = new Vector3(0,0,0);
        fovParent.transform.parent = this.transform;

        for (int y = fov.GetLength(1)-1; y >= 0; y--)
        {
            for (int x = 0; x < fov.GetLength(0); x++)
            {
                fov[x,y] = Instantiate(fovSprite) as SpriteRenderer;
                fov[x, y].transform.parent = fovParent.transform;
                fov[x, y].transform.position = new Vector3(x, y, 0);
            }
        }

    }
}