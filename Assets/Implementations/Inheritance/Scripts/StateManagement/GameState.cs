namespace Inheritance
{
    /// <summary>
    /// Abstract class, the base for all overall game states.
    /// Allows each state to implement its own logic, transitions, and indicate when complete.
    /// 
    /// </summary>
    public abstract class GameState
    {
        public bool IsComplete
        {
            get;
            protected set;
        }

        public virtual void Begin()
        {

        }

        public virtual void Execute()
        {
        
        }

        /// <summary>
        /// Will be invoked when this game state is transitioning out.
        /// Game states should manage all objects they create, and make sure to clean up after themselves here.
        /// </summary>
        /// <returns>The next game state to push onto the stack.  If null, the state underneath this one
        /// will be re-activated. </returns>
        public virtual GameState Complete()
        {
            return null;
        }
    }
}
