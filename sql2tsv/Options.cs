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

        [Option('s', "server", Required = true)]
        public string Server { get; set; }

        [Option('t', "table", Required = true)]
        public string Table { get; set; }

        [Option('f', "filter", Required = false)]
        public string Filter { get; set; }

        [Option('o', "order", Required = false)]
        public string Order { get; set; }

        [Option('m', "maxrecords", Required = false, Default = 9999999)]
        public int MaxRecords { get; set; }

        [Option('c', "columns", Required = false, Default = "*")]
        public string Columns { get; set; }

        [Option('q', "query", Required = false, Default = "")]
        public string Query { get; set; }

        [Option('h', "hasheaders", Required = false, Default = 1)]
        public int HasHeaders { get; set; }

        [Option('x', "separator", Required = false, Default = "\t")]
        public string Separator { get; set; }

        [Option('l', "filllength", Required = false, Default = 0)]
        public int FillLength { get; set; }

    }
}
