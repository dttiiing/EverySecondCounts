public interface IPlayer
{

    bool IsJumpTwiceValid();
    bool IsBreakWallValid();
    PlayerState GetPlayerState();
}