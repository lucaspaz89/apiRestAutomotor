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
    public class ModelsCardsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ModelsCardsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select mdlCar.id, cr.marca_brand, cr.id, mdlCar.modelo_models from modelsCards mdlCar 
                            inner join cards cr on mdlCar.marca_brand = cr.id
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

        [HttpGet("{id}")]
        public JsonResult Get(int idMarcaBrand)
        {
            string query = @"
                            select mdlCar.id, cr.marca_brand, mdlCar.modelo_models from modelsCards mdlCar 
                            inner join cards cr on mdlCar.marca_brand = cr.id where mdlCar.marca_brand = @idMarcaBrand
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
        public JsonResult Post(ModelsCards models)
        {
            string query = @"insert into modelsCards values (@marca_brand, @modelo_models)";
            DataTable table = new DataTable();
            string sqlDtSource = _configuration.GetConnectionString("devConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDtSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@marca_brand", models.marca_breand);
                    myCommand.Parameters.AddWithValue("@modelo_models", models.modelo_models);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Success data");
        }

        [HttpPut]
        public JsonResult Put(ModelsCards models)
        {
            string query = @"
                            update modelsCards set marca_brand = @marca_brand, modelo_models = @modelo_models
                            where id = @id";
            DataTable table = new DataTable();
            string sqlDtSource = _configuration.GetConnectionString("devConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDtSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", models.id);
                    myCommand.Parameters.AddWithValue("@marca_brand", models.marca_breand);
                    myCommand.Parameters.AddWithValue("@modelo_models", models.modelo_models);
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
            string query = @"delete from modelsCards where id = @id";
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
