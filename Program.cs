using System;
using CommandLine;
using NLog;
using Newtonsoft.Json;

namespace ConsoleTemplate
{
    class Program
    {
        static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        const string DefaultConfigFilename = "./default.config.save";

        public class Options
        {
            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; } = false;

            [Option('c', "config", Required = false, Default = "default.config", HelpText = "Config file.")]
            public string Config { get; set; } = "./default.config";

            [Option("saveconfig", Required = false, HelpText = $"Save a default config file to {DefaultConfigFilename}.")]
            public bool SaveConfig { get; set; }
        }

        public static Options options = new Options();
        public static Config config = new Config();

        public static void EnableAllForAllRules()
        {
            EnableLevelForAllRules(LogLevel.Trace);
            EnableLevelForAllRules(LogLevel.Debug);
            EnableLevelForAllRules(LogLevel.Info);
            EnableLevelForAllRules(LogLevel.Warn);
            EnableLevelForAllRules(LogLevel.Error);
            EnableLevelForAllRules(LogLevel.Fatal);
        }

        public static void EnableLevelForAllRules(LogLevel level)
        {
            foreach (var rule in LogManager.Configuration.LoggingRules)
            {
                rule.EnableLoggingForLevel(level);
            }

            //Call to update existing Loggers created with GetLogger() or
            //GetCurrentClassLogger()
            LogManager.ReconfigExistingLoggers();
        }

        static void LoadConfig()
        {
            try
            {
                // get the path of the current executable and load the config file
                var path = AppContext.BaseDirectory;
                path = Path.Combine(path, options.Config);

                logger.Info($"Loading config file {path}");
                if (!File.Exists(path))
                {
                    logger.Warn($"Config file {path} does not exist. Using default config.");
                    return;
                }

                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(path)) ?? new Config();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Failed to load config file {0}", options.Config);
                Environment.Exit(1);
            }
        }

        static void HandleDefaultOptions(Options opts)
        {
            options = opts;

            // run program
            if (options.Verbose)
            {
                logger.Info("Verbose logging enabled");
                EnableAllForAllRules();
            }

            if (options.SaveConfig)
            {
                logger.Info($"Saving config file {DefaultConfigFilename}");
                File.WriteAllText(DefaultConfigFilename, JsonConvert.SerializeObject(config));
            }

            LoadConfig();
        }

        static void Run(Options opts)
        {
            try
            {
                HandleDefaultOptions(opts);

                // ... application code here ...

                logger.Fatal("Hello fatal World!");
                logger.Error("Hello error World!");
                logger.Warn("Hello warn World!");
                logger.Info("Hello info World!");
                logger.Debug("Hello debug World!");
                logger.Trace("Hello trace World!");
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Failed to run");
                Environment.Exit(1);
            }
        }

        static void HandleError(IEnumerable<Error> errs)
        {
            try
            {
                foreach (var err in errs)
                {
                    logger.Error(err.ToString());
                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Failed to run");
                Environment.Exit(1);
            }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(Run)
                .WithNotParsed<Options>(HandleError);
        }
    }
}
