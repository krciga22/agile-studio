using AgileStudioCLI.FixtureSets;
using AgileStudioServer.Data;

namespace AgileStudioCLI.Commands
{
    public class LoadFixturesCommand : AbstractCommand
    {
        private readonly DBContext _DBContext;

        private readonly BaseFixtureSet _BaseFixtureSet;

        public LoadFixturesCommand(DBContext dbContext, BaseFixtureSet baseFixtureSet)
        {
            _DBContext = dbContext;
            _BaseFixtureSet = baseFixtureSet;

            SetName("LoadFixtures");
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            // todo - ask user which fixtures they want to load
            _BaseFixtureSet.LoadFixtures(_DBContext);
        }
    }
}
