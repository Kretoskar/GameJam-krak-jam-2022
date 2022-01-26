namespace Game.Control
{
    public interface IState
    {
        void Enter(StateMachine sm);
        void Execute();
        void Exit();
        bool Finished { get; }
        int Priority { get; set; }
    }
}