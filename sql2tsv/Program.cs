using CommandLine;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace sql2tsv
{
    class Program
    {
        static void Main(string[] args)
        {
            // Parse named options
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o => RunProgram(o))
                   .WithNotParsed<Options>(e => ErrorMessage(e));
        }

        /// <summary>
        /// Print error messages if expected parameters are missing
        /// </summary>
        /// <param name="e"></param>
        private static void ErrorMessage(IEnumerable<Error> e)
        {
            Console.WriteLine("Wrong or missing parameters. Unable to run program.");
        }

        /// <summary>
        /// Connect to database and extract TSV info about given table
        /// </summary>
        /// <param name="o"></param>
        private static void RunProgram(Options o)
        {
            try
            {
                var connString = $@"Password={o.Password};Persist Security Info=True;User ID={o.UserID};Initial Catalog={o.InitialCatalog};Data Source={o.Instance}";
                using (var connection = new SqlConnection(connString))
                {
                    connection.Open();
                    var sql = string.Format("SELECT TOP({0}) {1} FROM {2}", o.MaxRecords, o.Columns, o.Table);
                    if (!string.IsNullOrEmpty(o.Filter)) sql = sql + " WHERE " + o.Filter;
                    if (!string.IsNullOrEmpty(o.Order)) sql = sql + " ORDER BY " + o.Order;

                    var cmd = new SqlCommand(sql, connection);
                    var dr = cmd.ExecuteReader();

                    Console.WriteLine("{0}", string.Join('\t', dr.GetColumnSchema().Select(x => x.ColumnName).ToArray()));
                    while (dr.Read())
                    {
                        object[] valori = new object[dr.FieldCount];
                        dr.GetValues(valori);

                        Console.WriteLine("{0}", string.Join('\t', valori));
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
