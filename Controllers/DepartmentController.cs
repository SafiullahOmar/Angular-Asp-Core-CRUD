using AngularCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get() {
            string query = @"
                        select DepartmentId,DepartmentName from Department
                ";
            DataTable dt = new DataTable();
            string Source = _configuration.GetConnectionString("Conn");
            SqlDataReader dataReader;
            using (SqlConnection con = new SqlConnection(Source)) {
                con.Open();
                using (SqlCommand com = new SqlCommand(query, con)) {
                    dataReader = com.ExecuteReader();
                    dt.Load(dataReader);
                    con.Close();
                    dataReader.Close();
                }
            }

            return new JsonResult(dt);
        }

        public JsonResult Post(Department department) {
            string query = @"
                        insert into Department values ('" + department.DepartmentName + @"')
                ";
            DataTable dt = new DataTable();
            string Source = _configuration.GetConnectionString("Conn");
            SqlDataReader dataReader;
            using (SqlConnection con = new SqlConnection(Source))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    dataReader = com.ExecuteReader();
                    dt.Load(dataReader);
                    con.Close();
                    dataReader.Close();
                }
            }

            return new JsonResult("Department Added");
        }

        [HttpPut]
        public JsonResult Put(Department department)
        {
            string query = @"update Department set DepartmentName='" + department.DepartmentName + "' where DepartmentId=" + department.DepartmentId + @"";
            DataTable dt = new DataTable();
            string Source = _configuration.GetConnectionString("Conn");
            SqlDataReader dataReader;
            using (SqlConnection con = new SqlConnection(Source))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    dataReader = com.ExecuteReader();
                    dt.Load(dataReader);
                    con.Close();
                    dataReader.Close();
                }
            }

            return new JsonResult("Department updated");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from  Department  where DepartmentId=" + id + @"";
            DataTable dt = new DataTable();
            string Source = _configuration.GetConnectionString("Conn");
            SqlDataReader dataReader;
            using (SqlConnection con = new SqlConnection(Source))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    dataReader = com.ExecuteReader();
                    dt.Load(dataReader);
                    con.Close();
                    dataReader.Close();
                }
            }

            return new JsonResult("Department Deleted");
        }


    }
}
