
public class Monster : Actor
{
    public int? TurnsAlerted { get; set; }

    public IBehavior Behavior { get; set; }
    public int Depth { get; set; }

    public Monster(Game game) : base(game) { }

    public void DrawStats(int position)
    {
        //game.DrawMonsterStats(this, position);
    }

    public override void Died()
    {
        base.Died();

        game.Player.LeaveCombat(this);
    }

    public virtual void PerformAction(CommandSystem commandSystem)
    {
        if (TurnsAlerted != null && TurnsAlerted == 0 && !game.Player.inCombat(this))
        {
            ChangeHealthBy(1);
        }

        if (Behavior == null)
        {
            Behavior = new StandardMoveAndAttack();
        }

        Behavior.Act(this, commandSystem, game);
    }
}
