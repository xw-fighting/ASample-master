
namespace ASample.DesignMode.Test.Example.Command
{
    public class DownCommand : ICommand
    {
        public DownCommand(Light Light)
        {
            _light = Light;
        }
        private Light _light;
        public void Execute()
        {
            _light.TurnOff();
        }
    }
}
