using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ProductDAL
    {
        string cf = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
        SqlConnection conn;

        public ProductDAL()
        {
            conn = new SqlConnection(cf);
            conn.Open();
        }

        public List<Product> GetAllProducts() 
        {
            SqlDataAdapter adapter = new SqlDataAdapter("exec sp_show",conn);
            DataTable dt =new  DataTable();
            adapter.Fill(dt);

            List<Product> products = new List<Product>();
            foreach (DataRow dr in dt.Rows) {
                products.Add(new Product
                {
                    Id = int.Parse(dr["Id"].ToString()),
                    pname = dr["pname"].ToString(),
                    pcat = dr["pcat"].ToString(),
                    price = double.Parse(dr["price"].ToString())

                });
            }
            return products;
        }

        public void AddProduct(Product p)
        {
            string q = $"sp_insert {p.pname},{p.pcat},{p.price}";
            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.ExecuteNonQuery();


        }

        public void deleteProduct( int Id) 
        {
            string q = $"deleteproduct {Id}";
            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.ExecuteNonQuery();

        }

        public void EditProduct(Product p)
        {
            string q = $"EditProduct {p.Id},{p.pname},{p.pcat},{p.price}";
            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.ExecuteNonQuery();
        }

    }
}