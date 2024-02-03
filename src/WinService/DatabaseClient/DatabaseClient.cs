using InterProcessCommunication.gTools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinService.Models;
using WinService.Configuration;
using System.Text.RegularExpressions;

namespace WinService.Database
{
    public partial class DatabaseClient : IDisposable
    {
        #region Initialization

        public delegate void LogDelegate(string message);
        public event LogDelegate LogError;

        private SqlConnection _sqlConn;
        private StringBuilder _outputXml;

        #region Enums

        public enum ObjectType : int
        {
            Product = 16,
            Contractor = 32,
            ShippingAddress = 896
        }

        #endregion

        public DatabaseClient(WinConfiguration.DatabaseConfiguration config)
        {
            _sqlConn = new SqlConnection();
            _sqlConn.InfoMessage += _sqlConn_InfoMessage;

            Connect(config);
        }

        public void Dispose()
        {
            if (_sqlConn != null && _sqlConn.State == ConnectionState.Open)
            {
                _sqlConn.Close();
                _sqlConn.Dispose();
            }
        }

        #endregion

        #region Events

        void _sqlConn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            try
            {
                MatchCollection mc = Regex.Matches(e.Message, @"<\w*>.*</\w*>");
                foreach (Match m in mc)
                    _outputXml.Append($"    {m.Value}\r\n");

                Console.WriteLine(e.Message);
            }
            catch (Exception ex)
            {
                LogError?.Invoke(ex.Message);
            }
        }

        #endregion

        #region Methods

        private void Connect(WinConfiguration.DatabaseConfiguration config)
        {
            try
            {
                _sqlConn.ConnectionString = String.Format(@"                    
                    data source={0};
                    initial catalog={1};
                    user id={2}; 
                    pwd={3};
                    persist security info=False;
                    packet size=4096",
                    config.Address,
                    config.Name,
                    config.User,
                    config.Password);

                _sqlConn.Open();
            }
            catch (Exception ex)
            {
                LogError?.Invoke(ex.Message);                
            }
        }

        private void Disconnect()
        {
            try
            {
                _sqlConn?.Close();
            }
            catch (Exception ex)
            {
                LogError?.Invoke(ex.Message);                
            }
        }

        public int GetOperatorId(string code)
        {            
            if (string.IsNullOrEmpty(code))
                return 0;

            try
            {
                var commandText = @"
                    select 
                        Ope_GIDNumer
                    from 
                        CDN.OpeKarty
                    where
                        Ope_Ident = @code
                ";

                using (var cmd = new SqlCommand(commandText, _sqlConn))
                {
                    cmd.Parameters.Add(new SqlParameter("@code", code));

                    var obj = cmd.ExecuteScalar();
                    if (obj == null)
                        return 0;

                    return Convert.ToInt32(obj);
                }
            }
            catch (Exception ex)
            {
                LogError?.Invoke(ex.Message);
            }

            return 0;
        }

        public void ErrorLog_Insert(string errorMessage)
        {
            try
            {
                string commandText = @"                        
                    insert into CDN.DS_Techsam_WebApi_ErrorLog 
                    (
                        Timestamp,
                        ErrorMessage
                    )
                    values
                    (
                        @Timestamp,
                        @ErrorMessage
                    )
                ";

                using (var cmd = new SqlCommand(commandText, _sqlConn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Timestamp", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@ErrorMessage", errorMessage));

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                gLog.Write(ex.ToString());
            }
        }
        
        #endregion
    }
}
