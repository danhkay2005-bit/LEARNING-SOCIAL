using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace Common
{
    public class DatabaseHelper
    {
        private static readonly string connNguoiDung = ConfigurationManager.ConnectionStrings["NguoiDung"].ConnectionString;
        private static readonly string connHocTap = ConfigurationManager.ConnectionStrings["HocTap"].ConnectionString;
        private static readonly string connMXH = ConfigurationManager.ConnectionStrings["MXH"].ConnectionString;

        public static string GetConnectionString(DatabaseType dpType)
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

        public static SqlConnection GetConnection(DatabaseType dpType)
        {
            string connString = GetConnectionString(dpType);
            return new SqlConnection(connString);
        }

        //SELECT - Trả về DataTable
        public static DataTable ExecuteQuery(string query, DatabaseType dpType, SqlParameter[]? parameters = null)
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
        public static int ExecuteNonQuery(string query, DatabaseType dpType, SqlParameter[]? parameters = null)
        {
            using SqlConnection conn = GetConnection(dpType);
            using var cmd = new SqlCommand(query, conn); 
                    if(parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    conn.Open();
            return cmd.ExecuteNonQuery();
        }

        //COUNT, MAX - Trả về giá trị đơn
        public static object? ExecuteScalar(string query, DatabaseType dpType, SqlParameter[]? parameters = null)
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
