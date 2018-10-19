
namespace ASample.DesignMode.Test.Example.Command
{
    public class UpCommand : ICommand
    {
        public UpCommand(Light light)
        {
            _light = light ;
        }

        private   Light _light { get; set; }
        public void Execute()
        {
            _light.TurnOn();
        }
    }
}
