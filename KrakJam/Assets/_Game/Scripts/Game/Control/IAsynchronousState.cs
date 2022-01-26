namespace Game.Control
{
    public interface IAsynchronousState
    {
        void Enter();
        void Execute();
        void Disturb();
        void Exit();
    }
}