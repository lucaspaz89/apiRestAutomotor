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
    public class ColorController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ColorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select cl.id, cl.colorCards, cl.modelsCards, mdlCar.modelo_models from color cl
                            inner join modelsCards mdlCar on mdlCar.id = cl.modelsCards
                            inner join cards cr on cr.id = mdlCar.marca_brand
                            ";
            DataTable table = new DataTable();
            string sqlDtSource = _configuration.GetConnectionString("devConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDtSource))
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
        public JsonResult Post(Color cl)
        {
            string query = @"insert into color values (@modelsCards, @colorCards)";
            DataTable table = new DataTable();
            string sqlDtSource = _configuration.GetConnectionString("devConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDtSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@modelsCards", cl.modelsCards);
                    myCommand.Parameters.AddWithValue("@colorCards", cl.colorCards);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Success data");
        }

        [HttpPut("{id}")]
        public JsonResult Put(Color cl)
        {
            string query = @"
                            update color set modelsCards = @modelsCards, colorCards = @colorCards
                            where id = @id";
            DataTable table = new DataTable();
            string sqlDtSource = _configuration.GetConnectionString("devConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDtSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", cl.id);
                    myCommand.Parameters.AddWithValue("@modelsCards", cl.modelsCards);
                    myCommand.Parameters.AddWithValue("@colorCards", cl.colorCards);
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
            string query = @"delete from color where id = @id";
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
