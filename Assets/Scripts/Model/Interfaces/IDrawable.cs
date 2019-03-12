using RogueSharp;


public interface IDrawable
{
    Colors  Color   { get; set; }
    char    Symbol  { get; set; }
    int     X       { get; set; }
    int     Y       { get; set; }
    string  Sprite  { get; set; }
    int     Rotation { get; set; }


    void Draw(IMap map);
}
