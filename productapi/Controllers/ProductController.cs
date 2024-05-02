using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using productapi.Models;
using System.Data;
using System.Data.SqlClient;

namespace productapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProductController(IConfiguration configuration)
        {
            _configuration= configuration;
        }
        [Route("GetallProducts")]
        [HttpGet]
        public async Task<IActionResult> GetallProducts()
        {
            List<Products> products = new List<Products>();
            DataTable dt=new DataTable();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("Con"));
            SqlCommand cmd = new SqlCommand("Select * from products", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            for(int i = 0;i<dt.Rows.Count;i++)
            {
                Products pro = new Products();
                pro.Id = Convert.ToInt32(dt.Rows[i]["id"]);
                pro.ProductName = dt.Rows[i]["Pname"].ToString();
                pro.ProductPrice = dt.Rows[i]["PPrice"].ToString();
                pro.DateTime = Convert.ToDateTime(dt.Rows[i]["Date"]);
                products.Add(pro);
            }

            return Ok(products);
        }

        [Route("PostProducts")]
        [HttpPost]
        public async Task<IActionResult> PostProducts(Products obj)
        {
            try
            {
                SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("Con"));
                SqlCommand cmd = new SqlCommand("insert into products values('" + obj.ProductName + "','" + obj.ProductPrice + "',getdate())", conn);
              conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
        [Route("UpdateProducts")]
        [HttpPut]
        public async Task<IActionResult> UpdateProducts(Products obj)
        {
            try
            {
                SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("Con"));
                SqlCommand cmd = new SqlCommand("update products set Pname='" + obj.ProductName + "',PPrice='" + obj.ProductPrice + "'where id='"+obj.Id+"' ", conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
