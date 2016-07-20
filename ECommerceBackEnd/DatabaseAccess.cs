using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace ECommerceBackEnd
{
    /// <summary>Class to add an item to the shopping basket.</summary>
    public static class DatabaseAccess
    {
        /// <summary>Fetches and sets up the connection string for the database.</summary>
        /// <returns>The connection string for the database.</returns>
        public static string getConnectionString()
        {
            string host = ConfigurationManager.AppSettings["host"].ToString();
            string port = ConfigurationManager.AppSettings["port"].ToString();
            string oracleSID = ConfigurationManager.AppSettings["oracleSID"].ToString();
            string username = ConfigurationManager.AppSettings["username"].ToString();
            string password = ConfigurationManager.AppSettings["password"].ToString();

            string connectionString = string.Format(ConfigurationManager.ConnectionStrings["FDMOracleODP"].ToString(), host, port, oracleSID, username, password);

            return connectionString;
        }

        /// <summary>Class to add an item to the basket.</summary>
        /// <param name="entryToAdd">The item to add to the database.</param>
        /// <returns>A bool for whether the interaction was successful.</returns>
        public static bool AddItem(BasketItem entryToAdd)
        {
            string _connectionString = getConnectionString();

            string _sqlStatement = "INSERT INTO basket(itemid, itemname, itemprice, basketid) VALUES (:itemid, :itemname, :itemprice, :basketid)";
            IDbConnection connection = new OracleConnection(_connectionString);
            OracleCommand command = new OracleCommand(_sqlStatement, (OracleConnection)connection);
            command.BindByName = true;
            IDbDataParameter param = new OracleParameter(":itemid", OracleDbType.Int16);
            param.Value = entryToAdd.itemID;
            command.Parameters.Add(param);
            param = new OracleParameter(":itemname", OracleDbType.Varchar2);
            param.Value = entryToAdd.itemName;
            command.Parameters.Add(param);
            param = new OracleParameter(":itemprice", OracleDbType.Double);
            param.Value = entryToAdd.itemPrice;
            command.Parameters.Add(param);
            param = new OracleParameter(":basketid", OracleDbType.Int16);
            param.Value = entryToAdd.basketID;
            command.Parameters.Add(param);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (OracleException exception)
            {
                Console.WriteLine("Error: {0} Inner Exception: {1}", exception.Message, exception.InnerException);
            }
            finally
            {
                connection.Close();
            }

            return true;
        }

        /// <summary>Fetches an item from the listed items for sale.</summary>
        /// <param name="itemToFind">The name of the item to find.</param>
        /// <returns>The details of the item as a BasketItem class instance.</returns>
        public static BasketItem GetItem(string itemToFind)
        {
            string connectionString = getConnectionString();

            string sqlStatement = string.Format(CultureInfo.InvariantCulture, "SELECT * FROM itemlistings WHERE itemname = '{0}'", itemToFind);

            IDbConnection cn = new OracleConnection(connectionString);
            IDbCommand cmd = new OracleCommand(sqlStatement, (OracleConnection)cn);

            var itemToReturn = new BasketItem(0, null, 0, 0);

            try
            {
                cn.Open();

                IDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    itemToReturn = new BasketItem(
                        int.Parse((dr["itemid"].ToString())),
                        dr["itemname"].ToString(),
                        double.Parse((dr["itemprice"].ToString())),
                        int.Parse((dr["basketid"].ToString()))
                        );
                }
            }
            catch (OracleException exception)
            {
                Console.WriteLine("Error: {0} Inner Exception: {1}", exception.Message, exception.InnerException);
            }
            finally
            {
                cn.Close();
            }

            return itemToReturn;
        }

        /// <summary>Fetches all items in the shopping basket.</summary>
        /// <param name="basketID">The ID of the basket.</param>
        /// <returns>A list of all items.</returns>
        public static List<BasketItem> GetBasket(string basketID)
        {
            string connectionString = getConnectionString();

            string sqlStatement = string.Format(CultureInfo.InvariantCulture, "SELECT * FROM basket WHERE basketid = '{0}'", basketID);

            IDbConnection cn = new OracleConnection(connectionString);
            IDbCommand cmd = new OracleCommand(sqlStatement, (OracleConnection)cn);

            var listToReturn = new List<BasketItem>();

            try
            {
                cn.Open();

                IDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listToReturn.Add(new BasketItem(
                        int.Parse((dr["itemid"].ToString())),
                        dr["itemname"].ToString(),
                        double.Parse((dr["itemprice"].ToString())),
                        int.Parse((dr["basketid"].ToString()))
                        ));
                }
            }
            catch (OracleException exception)
            {
                Console.WriteLine("Error: {0} Inner Exception: {1}", exception.Message, exception.InnerException);
            }
            finally
            {
                cn.Close();
            }

            return listToReturn;
        }

        /// <summary>Removes all items with a given ID from the database.</summary>
        /// <param name="idToRemove">The basket ID to remove from the database.</param>
        public static void RemoveItems(string idToRemove)
        {
            string connectionString = getConnectionString();
            string sqlStatement = string.Format("DELETE FROM basket WHERE basketid = '{0}'", idToRemove);
            IDbConnection connection = new OracleConnection(connectionString);
            OracleCommand command = new OracleCommand(sqlStatement, (OracleConnection)connection);
            command.BindByName = true;
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (OracleException exception)
            {
                Console.WriteLine("Error: {0} Inner Exception: {1}", exception.Message, exception.InnerException);
            }
            finally { connection.Close(); }
        }
    }
}
