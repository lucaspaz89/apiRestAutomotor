using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using apiRestAutomotor.Models;

namespace apiRestAutomotor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        
        public CardsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select id,marca_brand from cards";
            DataTable table = new DataTable();
            string sqlDtSource = _configuration.GetConnectionString("devConnection");
            SqlDataReader myReader;
            using(SqlConnection myCon = new SqlConnection(sqlDtSource))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon))
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
        public JsonResult Post(Cards cars)
        {
            string query = @"insert into cards values (@marca_brand)";
            DataTable table = new DataTable();
            string sqlDtSource = _configuration.GetConnectionString("devConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDtSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@marca_brand", cars.marca_brand);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Success data");
        }

        [HttpPut]
        public JsonResult Put(Cards cars)
        {
            string query = @"
                            update cards set marca_brand = @marca_brand
                            where id = @id";
            DataTable table = new DataTable();
            string sqlDtSource = _configuration.GetConnectionString("devConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDtSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", cars.id);
                    myCommand.Parameters.AddWithValue("@marca_brand", cars.marca_brand);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Success Update data");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from cards where id = @id";
            DataTable table = new DataTable();
            string sqlDtSource = _configuration.GetConnectionString("devConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDtSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Delete data");
        }

    }
}
