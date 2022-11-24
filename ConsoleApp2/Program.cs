using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string s = "select c.client_name from client as c where c.client_id = ${clientId} and c.type_client = '${clientType}'";
            
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("clientId", "120");
            dictionary.Add("clientType", "REAL");

            Console.WriteLine(RegexSQL(s, dictionary));


            Console.Read();



        }

        public static string RegexSQL(string sqlString, Dictionary<string, string> dictionary)
        {
            Regex regex = new Regex(@"\$\{(\w*)\}");
            MatchCollection matches = regex.Matches(sqlString);
            if (matches.Count > 0)
            {
                string newSQLString = sqlString;

                foreach (Match match in matches)
                {
                    string RemSumb = (@"\$\{");
                    string tarEmpt = "";
                    Regex helpReg = new Regex(RemSumb);
                    string cleanVariable = helpReg.Replace(match.Value, tarEmpt);

                    RemSumb = (@"\}");
                    helpReg= new Regex(RemSumb);
                    cleanVariable = helpReg.Replace(cleanVariable, tarEmpt);


                    string pattern = (@"\$\{" +cleanVariable + @"\}");

                    if (!dictionary.ContainsKey(cleanVariable)) throw new NullReferenceException($"Dictionary does not contained a key value = {cleanVariable}") ;

                    string target = dictionary[cleanVariable];
                    Regex replaseRegex = new Regex(pattern);

                    newSQLString = replaseRegex.Replace(newSQLString, target);
                }

                return newSQLString;
            }

            return sqlString;
        }

    }
}
