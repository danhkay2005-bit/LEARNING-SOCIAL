using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace Common
{
    public class DatabaseHelper
    {
        private readonly string connNguoiDung = ConfigurationManager.ConnectionStrings["NguoiDung"].ConnectionString;
        private readonly string connHocTap = ConfigurationManager.ConnectionStrings["HocTap"].ConnectionString;
        private readonly string connMXH = ConfigurationManager.ConnectionStrings["MXH"].ConnectionString;

        public string GetConnectionString(DatabaseType dpType)
        {
            return dpType switch
            {
                DatabaseType.NguoiDung => connNguoiDung,
                DatabaseType.HocTap => connHocTap,
                DatabaseType.MXH => connMXH,
                _ => connNguoiDung,
            };
        }
        //Tạo Connection

        public SqlConnection GetConnection(DatabaseType dpType)
        {
            string connString = GetConnectionString(dpType);
            return new SqlConnection(connString);
        }

        //SELECT - Trả về DataTable
        public DataTable ExecuteQuery(string query, DatabaseType dpType, SqlParameter[]? parameters = null)
        {
            var dt = new DataTable();
            using SqlConnection conn = GetConnection(dpType);
            using var cmd = new SqlCommand(query, conn);
                if(parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    conn.Open();
            using SqlDataReader rd = cmd.ExecuteReader();
                        dt.Load(rd);   
                
            return dt;
        }

        //INSERT, UPDATE, DELETE - Trả về số dòng bị ảnh hưởng
        public int ExecuteNonQuery(string query, DatabaseType dpType, SqlParameter[]? parameters = null)
        {
            using SqlConnection conn = GetConnection(dpType);
            using var cmd = new SqlCommand(query, conn); 
                    if(parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    conn.Open();
            return cmd.ExecuteNonQuery();
        }

        //COUNT, MAX - Trả về giá trị đơn
        public object? ExecuteScalar(string query, DatabaseType dpType, SqlParameter[]? parameters = null)
        {
            using SqlConnection conn = GetConnection(dpType);
            using var cmd = new SqlCommand(query, conn);
                    if(parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    conn.Open();
            return cmd.ExecuteScalar();
        }

        public enum DatabaseType
        {
            NguoiDung,
            HocTap,
            MXH
        }
    }
}
