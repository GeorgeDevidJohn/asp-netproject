using System;
using System.Collections.Generic;
using System.Text;

namespace Library_management
{
    public class Constants
    {
        public static class SqlStatements
        {

            public const string selectAvailableBooks = "SELECT * " +
                   "FROM Customers  " +
                   "WHERE Lended_by = null AND Reserved_by =null";

            public const string selectCustomersById = "SELECT CustomerID, Name, Address, City, " +
                    "State, ZipCode " +
                    "FROM Customers " +
                    "WHERE CustomerID = @CustomerID";

            public const string insertStatement = "INSERT Customers " +
                "(Name, Address, City, State, ZipCode) " +
                "VALUES (@Name, @Address, @City, @State, @ZipCode)";

            public const string removeStatement = "DELETE FROM  Customers " +
               "WHERE  CustomerID = @CustomerID";



        }
    }
}
