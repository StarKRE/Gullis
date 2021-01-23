namespace Gameknit
{
    /// <summary>
    ///     <para>A lifecycle status of game context.</para>
    /// </summary>
    public enum GameStatus
    {
        CREATING = 1,
        PREPARING = 2,
        READY = 3,
        PLAYING = 4,
        PAUSING = 5,
        FINISHING = 6,
        DESTROYING = 7
    }
}