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
        /// Print a line of data
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="values"></param>
        private static void PrintLine(string separator, object[] values)
        {
            Console.WriteLine("{0}", string.Join(separator, values.ToArray()));
        }

        /// <summary>
        /// Connect to database and extract TSV info about given table
        /// </summary>
        /// <param name="o"></param>
        private static void RunProgram(Options o)
        {
            try
            {
                var connString = $@"Password={o.Password};Persist Security Info=True;User ID={o.UserID};Initial Catalog={o.InitialCatalog};Data Source={o.Server}";
                using (var connection = new SqlConnection(connString))
                {
                    connection.Open();
                    var sql = o.Query;
                    if (string.IsNullOrEmpty(sql)) sql = string.Format("SELECT TOP({0}) {1} FROM {2} ", o.MaxRecords, o.Columns, o.Table);
                    if (!string.IsNullOrEmpty(o.Filter)) sql = sql + " WHERE " + o.Filter;
                    if (!string.IsNullOrEmpty(o.Order)) sql = sql + " ORDER BY " + o.Order;

                    var cmd = new SqlCommand(sql, connection);
                    var dr = cmd.ExecuteReader();

                    var schema = dr.GetColumnSchema();

                    if (o.HasHeaders == 1) Console.WriteLine("{0}", string.Join(o.Separator, schema.Select(x => x.ColumnName).ToArray()));

                    object[] values = new object[dr.FieldCount];
                    while (dr.Read())
                    {
                        if (o.FillLength == 0)
                        {
                            dr.GetValues(values);
                        }
                        else
                        {                            
                            for(int i = 0; i < dr.FieldCount; i++)
                            {
                                var size = schema.FirstOrDefault(x => x.ColumnName == dr.GetName(i)).ColumnSize;

                                switch (dr.GetFieldType(i).Name)
                                {
                                    case "Int32":
                                        values[i] = dr.GetInt32(i);
                                        break;

                                    case "Int16":
                                        values[i] = dr.GetInt16(i);
                                        break;

                                    case "Int64":
                                        values[i] = dr.GetInt64(i);
                                        break;

                                    case "DateTime":
                                        values[i] = dr.GetDateTime(i);
                                        break;

                                    case "Decimal":
                                        values[i] = dr.GetDecimal(i);
                                        break;

                                    default:
                                        values[i] = dr.GetString(i);
                                        break;
                                }

                                var _length = (int)size - values[i].ToString().Length;
                                if (_length <= 0) _length = values[i].ToString().Length;

                                values[i] =  values[i].ToString() + new string(' ', _length);
                            }
                        }
                        PrintLine(o.Separator, values.ToArray());
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
