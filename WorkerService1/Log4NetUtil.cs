using log4net.Config;
using log4net.Repository;
using log4net;


namespace WorkerService1
{
    public class Log4NetUtil
    {
        private static ILoggerRepository _repository;
        private static ILog _log;

        private static ILog log
        {
            get
            {
                if (_log == null)
                {
                    Configure();
                }
                return _log;
            }
        }

        public static void Configure(string repositoryName = "NETCoreRepository", string configFile = "log4net.config")
        {
            _repository = LogManager.CreateRepository(repositoryName);
            XmlConfigurator.Configure(_repository, new FileInfo(configFile));
            _log = LogManager.GetLogger(repositoryName, "");
        }

        public static void Info(string msg)
        {
            log.Info(msg);
        }

        public static void Warn(string msg)
        {
            log.Warn(msg);
        }

        public static void Error(string msg)
        {
            log.Error(msg);
        }
    }
}
