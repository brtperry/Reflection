using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLAParameter;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;

namespace Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new MySQLData() {

                Age = 55,

                Name = "Axel Pressbutton",

                Other = "Cyborg"
            };

            var controller = new SQLDataController(data);

            var response = controller.Save();

            if (response)
            {
                Console.WriteLine("Everyone's happy.");
            }
            else
            {
                Console.WriteLine("No one's happy.");
            }
        }
    }

    class MySQLData
    {
        [DataAttribute("@Name", true)]
        public string Name { get; set; }

        [DataAttribute("@Age", true)]
        public int Age { get; set; }

        [DataAttribute("@Other", false)]
        public string Other { get; set; }
    }

    class SQLDataController
    {
        private MySQLData myData;

        public SQLDataController(MySQLData data)
        {
            myData = data;
        }

        public bool Save()
        {
            const string cnn = @"Server=.\SQLEXPRESS;Initial Catalog=TestDatabase;Trusted_Connection=True;Connect Timeout=30;";

            const string storedProcedure = "AddData";

            using (var conn = new SqlConnection(cnn))
            {
                conn.Open();

                using (var dbCmd = new SqlCommand(storedProcedure, conn) { CommandType = CommandType.StoredProcedure })
                {
                    foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(myData))
                    {
                        DataAttribute attr = (DataAttribute)prop.Attributes[typeof(DataAttribute)];

                        // if there's no attribute on the property, i.e. a private field it will be ignored.
                        if (attr == null)
                        {
                            continue;
                        }

                        // don't add the properties value when attr.Include is false.
                        if (!attr.Include)
                        {
                            continue;
                        }

                        dbCmd.Parameters.AddWithValue(attr.FieldName, prop.GetValue(myData));
                    }
                    // The result is -1 if successful.
                    return dbCmd.ExecuteNonQuery() == -1;
                }
            }
        }
    }
}
