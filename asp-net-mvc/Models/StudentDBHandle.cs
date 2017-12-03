using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace asp_net_mvc.Models
{
    public class StudentDBHandle
    {
        private OracleConnection con;
        private void connection()
        {
            //string constring = ConfigurationManager.ConnectionStrings["studentconn"].ToString();
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["studentconn"].ConnectionString;
        }

        public List<StudentModel> GetStudent()
        {
            connection();
            List<StudentModel> studentlist = new List<StudentModel>();

            OracleCommand cmd = new OracleCommand(
                 "select id, name from Student", con);
            OracleDataAdapter sd = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                studentlist.Add(
                    new StudentModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"])
                    });
            }
            return studentlist;
        }


    }
}