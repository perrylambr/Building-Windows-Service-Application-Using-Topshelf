using log4net;

namespace TopshelfDemo
{
    public class MyWindowsService
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(MyWindowsService));

        public void Start()
        {
            _log.Info("Starting...");
            // business logic goes here
            _log.Info("Started succesfully.");
        }

        public void Stop()
        {
            _log.Info("Stopping...");
            // business logic goes here
            _log.Info("Stopped succesfully.");
        }
        public void Pause()
        {
        }

        public void Resume()
        {
        }

        public void Shutdown()
        {
        }
    }
}
