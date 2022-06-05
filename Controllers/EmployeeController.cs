using AngularCrud.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IConfiguration configuration,IWebHostEnvironment  webHostEnvironment)
        {
            _configuration = configuration;
            _env = webHostEnvironment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select * from Employee
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

            return new JsonResult(dt);
        }

        public JsonResult Post(Employee emp)
        {
            string query = @"INSERT INTO [dbo].[Employee]
                               ([EmployeeName]
                               ,[Department]
                               ,[DateOfJoining]
                               ,[Photo])
                         VALUES
                               ('"+emp.EmployeeName+ "','"+emp.Department+ "','"+emp.DateOfJoining+ "','"+emp.Photo+@"')
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

            return new JsonResult(" Added");
        }

        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            string query = @"update Employee set [EmployeeName]='" +emp.EmployeeName + "' where EmployeeId=" + emp.EmpoyeeId + @"";
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

            return new JsonResult(" updated");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from  Employee  where EmployeeId=" + id + @"";
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

            return new JsonResult(" Deleted");
        }


        [Route("SaveFile")]
        [HttpPost]
        public JsonResult saveFile() {
            try
            {

                var httpRequest = Request.Form;
                var postedfile = httpRequest.Files[0];
                string filename = postedfile.FileName;
                var path = _env.ContentRootPath + "/photo/" + filename;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    postedfile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception e) {

                return new JsonResult( e.InnerException.Message);
               //return new JsonResult("nothing.png");
            }
        }

        [Route("GetAllDpt")]
        [HttpGet]
        public JsonResult GetAllDepartments ()
        {
            string query = @"
                        select * from Department
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

            return new JsonResult(dt);
        }


    }
}
