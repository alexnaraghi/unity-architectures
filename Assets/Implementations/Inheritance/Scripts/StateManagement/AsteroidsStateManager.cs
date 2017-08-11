namespace Inheritance
{
    public class AsteroidsStateManager : StateManager
    {
        protected void Awake()
        {
            PushState(new QuitGameState());
            PushState(new StartState());
        }
    }
}    