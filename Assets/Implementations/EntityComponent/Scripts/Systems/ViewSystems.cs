using Entitas;

namespace EntityComponent
{
    /// <summary>
    /// This is a master feature of the view systems.  Acts as a sub-group in the inspector view.
    /// </summary>
    public class ViewSystems : Feature
    {
        public ViewSystems(Contexts contexts) : base("View Systems")
        {
            Add(new AddViewSystem(contexts));
            Add(new SetPositionSystem(contexts));
            Add(new SetDirectionSystem(contexts));
        }
    }
}