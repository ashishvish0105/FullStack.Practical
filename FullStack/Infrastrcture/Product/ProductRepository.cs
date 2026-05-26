using FullStack.Core.Entity;
using FullStack.Core.Iterface;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Xml.Linq;

namespace FullStack.Repository.Product
{
    public class ProductRepository : IProductRepository
    {
        IConfiguration config;
        public ProductRepository(IConfiguration _config)
        {
            config = _config;
        }

        private ProductModel MapProduct(SqlDataReader dr)
        {
            return new ProductModel
            {
                productId = Convert.ToInt32(dr["productId"]),
                name = dr["name"]?.ToString(),
                description = dr["description"]?.ToString(),
                categories = dr["category"]?.ToString(),
                price = Convert.ToDouble(dr["price"])
            };
        }

        public async Task<ProductModel> getProductById(int productId)
        {
            string cs = config.GetConnectionString("defaultConnectionStrings");

            await using var con = new SqlConnection(cs);
            await using var cmd = new SqlCommand("getProductListOrById", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@productId", SqlDbType.Int).Value = productId;

            await con.OpenAsync();

            await using var dr = await cmd.ExecuteReaderAsync();

            if (await dr.ReadAsync())
            {
                return MapProduct(dr);
            }

            return null;
        }

        public async Task<List<ProductModel>> getProductList()
        {
            var productList = new List<ProductModel>();
            string cs = config.GetConnectionString("defaultConnectionStrings");

            await using var con = new SqlConnection(cs);
            await using var cmd = new SqlCommand("getProductListOrById", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            await con.OpenAsync();

            await using var dr = await cmd.ExecuteReaderAsync();

            while (await dr.ReadAsync())
            {
                productList.Add(MapProduct(dr));
            }

            return productList;
        }

        public async Task<int> addEditProduct(ProductModel productObj)
        {
            string cs = config.GetConnectionString("defaultConnectionStrings");

            await using var con = new SqlConnection(cs);
            await using var cmd = new SqlCommand("addEditProduct", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@productId", SqlDbType.Int).Value = productObj.productId;
            cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = productObj.name;
            cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = productObj.description;
            cmd.Parameters.Add("@category", SqlDbType.NVarChar).Value = productObj.categories;
            cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = productObj.price;

            await con.OpenAsync();

            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<PaginationResponse<ProductModel>> GetProducts(
        int pageNumber = 1,
        int pageSize = 10,
        string? search = null)
        {
            var response = new PaginationResponse<ProductModel>();

            string connectionString = config.GetConnectionString("defaultConnectionStrings");

            using SqlConnection con = new SqlConnection(connectionString);

            using SqlCommand cmd = new SqlCommand("sp_GetProducts", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);

            if (string.IsNullOrWhiteSpace(search))
                cmd.Parameters.AddWithValue("@Search", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@Search", search);

            await con.OpenAsync();

            using SqlDataReader dr = await cmd.ExecuteReaderAsync();

            // First ResultSet -> Product List
            while (await dr.ReadAsync())
            {
                response.Data.Add(MapProduct(dr));
            }

            // Second ResultSet -> Total Count
            if (await dr.NextResultAsync())
            {
                if (await dr.ReadAsync())
                {
                    response.TotalRecords =
                        Convert.ToInt32(dr["TotalRecords"]);
                }
            }

            response.PageNumber = pageNumber;
            response.PageSize = pageSize;

            return response;
        }


        //public async Task<List<ProductModel>> getProductList()
        //{
        //    try
        //    {
        //        string connectionString = config.GetConnectionString("defaultConnectionStrings");
        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {
        //            List<ProductModel> productList = new List<ProductModel>();

        //            string query = "getProductListOrById";
        //            using (SqlCommand cmd = new SqlCommand(query, con))
        //            {
        //                con.Open();
        //                SqlDataReader dr = await cmd.ExecuteReaderAsync();
        //                while (dr.Read())
        //                {
        //                    ProductModel productDetails = new ProductModel
        //                    {
        //                        productId = Convert.ToInt16(dr["productId"]),
        //                        name = Convert.ToString(dr["name"]),
        //                        description = Convert.ToString(dr["description"]),
        //                        categories = Convert.ToString(dr["category"]),
        //                        price = Convert.ToDouble(dr["price"]),

        //                    };
        //                    productList.Add(productDetails);
        //                }
        //            }
        //            return productList;

        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        //public async Task<ProductModel> getProductById(int productId)
        //{
        //    try
        //    {
        //        string connectionString = config.GetConnectionString("defaultConnectionStrings");
        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {
        //            string query = "getProductListOrById";
        //            using (SqlCommand cmd = new SqlCommand(query, con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@productId", productId);
        //                con.Open();
        //                SqlDataReader dr = await cmd.ExecuteReaderAsync();
        //                while (dr.Read())
        //                {
        //                    ProductModel productDetails = new ProductModel
        //                    {
        //                        productId = Convert.ToInt16(dr["productId"]),
        //                        name = Convert.ToString(dr["name"]),
        //                        description = Convert.ToString(dr["description"]),
        //                        categories = Convert.ToString(dr["category"]),
        //                        price = Convert.ToDouble(dr["price"]),

        //                    };
        //                    return productDetails;
        //                }
        //            }
        //        }
        //        return null;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        //public async Task<int> addEditProduct(ProductModel productObj)
        //{
        //    try
        //    {
        //        string connectionString = config.GetConnectionString("defaultConnectionStrings");
        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {
        //            string query = "addEditProduct";
        //            using (SqlCommand cmd = new SqlCommand(query, con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                con.Open();

        //                cmd.Parameters.AddWithValue("@productId", productObj.productId);
        //                cmd.Parameters.AddWithValue("@name", productObj.name);
        //                cmd.Parameters.AddWithValue("@description", productObj.description);
        //                cmd.Parameters.AddWithValue("@category", productObj.categories);
        //                cmd.Parameters.AddWithValue("@price", productObj.price);

        //                int insertResult = await cmd.ExecuteNonQueryAsync();
        //                con.Close();
        //                return insertResult;
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}
