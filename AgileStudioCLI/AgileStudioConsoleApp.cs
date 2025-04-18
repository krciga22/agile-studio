
using AgileStudioCLI.Commands;

namespace AgileStudioCLI
{
    internal class AgileStudioConsoleApp : AbstractConsoleApp
    {
        private readonly ClearFixturesCommand _ClearFixturesCommand;
        private readonly LoadFixturesCommand _LoadFixturesCommand;

        public AgileStudioConsoleApp(
            ClearFixturesCommand clearFixturesCommand, 
            LoadFixturesCommand loadFixturesCommand)
        {
            _ClearFixturesCommand = clearFixturesCommand;
            _LoadFixturesCommand = loadFixturesCommand;
        }

        protected void Configure()
        {
            AddCommand(_ClearFixturesCommand);
            AddCommand(_LoadFixturesCommand);
        }

        public override void Start()
        {
            Console.WriteLine("Agile Studio CLI");
            Console.WriteLine(String.Empty);

            Configure();

            ListCommands();

            base.Start();
        }
    }
}
