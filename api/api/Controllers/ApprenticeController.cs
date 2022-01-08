using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using api.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprenticeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ApprenticeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
            select ApprenticeId, Username, Department, convert(varchar(10), DateOfJoining, 120) as DateOfJoining
            from dbo.Apprentice
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PizzaApprenticesAppCon");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Apprentice apprentice)
        {
            string query = @"
            insert into dbo.Apprentice
            values (@Username,@Department,@DateOfJoining)
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PizzaApprenticesAppCon");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Username", apprentice.Username);
                    myCommand.Parameters.AddWithValue("@Department", apprentice.Department);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", apprentice.DateOfJoining);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Apprentice apprentice)
        {
            string query = @"
            update dbo.Apprentice set Username = @Username,
            Department = @Department,
            DateOfJoining = @DateOfJoining
            where ApprenticeId = @ApprenticeId
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PizzaApprenticesAppCon");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ApprenticeId", apprentice.ApprenticeId);
                    myCommand.Parameters.AddWithValue("@Username", apprentice.Username);
                    myCommand.Parameters.AddWithValue("@Department", apprentice.Department);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", apprentice.DateOfJoining);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
            delete from dbo.Apprentice
            where ApprenticeId = @ApprenticeId
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PizzaApprenticesAppCon");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ApprenticeId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }
    }
}
