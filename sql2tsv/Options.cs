using CommandLine;

namespace sql2tsv
{
    /// <summary>
    /// Class for parsing command line arguments
    /// Source: https://github.com/commandlineparser/commandline
    /// </summary>
    class Options
    {
        [Option('u', "userid", Required = true)]
        public string UserID { get; set; }

        [Option('p', "password", Required = true)]
        public string Password { get; set; }

        [Option('d', "database", Required = true)]
        public string InitialCatalog { get; set; }

        [Option('i', "instance", Required = true)]
        public string Instance { get; set; }

        [Option('t', "table", Required = true)]
        public string Table { get; set; }
    }
}
