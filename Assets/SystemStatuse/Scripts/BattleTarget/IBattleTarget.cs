namespace StatusGroup
{
    public interface IBattleTarget
    {
        string Id { get; }
        StatusManager StatusManager { get; }
    }
}