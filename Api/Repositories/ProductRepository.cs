using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Api.DataAccess.Interfaces;
using Api.Repositories.Interfaces;
using Dapper;
using Entities;
using Entities.Dto;
using Entities.Http;
using MySql.Data.MySqlClient;

namespace Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IData _data;

        public ProductRepository(IData data)
        {
            _data = data;
        }

        public async Task<IEnumerable<Products>> GetAllProducts()
        {

            var db = _data.DbConnection;


            var sql = "Select * From products";

            return await db.QueryAsync<Products>(sql, new {});
        }

        public async Task<Products> GetOneProduct(string ID)
        {
            var db = _data.DbConnection;


            var sql = @"SELECT * 
                        FROM products WHERE 
                        ID=LPAD(@ID, 8, '0')";

            return await db.QueryFirstOrDefaultAsync<Products>(sql, new {ID = ID});
        }

        public async Task<bool> InsertProduct(Products products)
        {
            
            using (MySqlConnection cn = new MySqlConnection("Server=lin-2126-2215-mysql-primary.servers.linodedb.net;Port=3306;Database=services;Uid=linroot;Pwd=Q2OWP^YbR5kfcrqV;AllowUserVariables=True;"))
            {
                var cmd = new MySqlCommand("SELECT COUNT(*) FROM products WHERE ID = LPAD(@ID, 8, '0')", cn);

                cmd.Parameters.AddWithValue("@ID", products.ID);

                cn.Open();

                if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                {
                    //Insertar fila
                    var db = _data.DbConnection;


                    var sql = @"Insert Into products (ID, Description) 
                                Values (LPAD(@ID, 8, '0'), @Description)";
                    

                    var result =  await db.ExecuteAsync(sql, new {products.ID, products.Description});

                    return result >  0;
                }
                else
                {
                    //El valor ya existe en la tabla
                    return false;
                }
            }
          
        }

        public async Task<bool> UpdateProduct(Products products)
        {
            
           var db = _data.DbConnection;


            var sql = @"UPDATE products
                            SET Description = @Description
                        WHERE ID =LPAD(@ID, 8, '0')";

            var result =  await db.ExecuteAsync(sql, new {products.Description, products.ID});

            return result >  0;
        }

        public async Task<bool> DeleteProduct(string ID)
        {
            var db = _data.DbConnection;


            var sql = @"DELETE 
                        FROM products 
                        WHERE ID=LPAD(@ID, 8, '0')";

            var result = await db.ExecuteAsync(sql, new {ID = ID});

            return result > 0;
        }

        public async Task<List<ProductDto>> SearchByID(ProductFilter filter)
        {
            dynamic param = new ExpandoObject();
            

            var sql = @"SELECT ID, Description FROM products Where 1 = 1";

            if (!string.IsNullOrEmpty(filter.ID))
            {
                param.Productfilter  ="%"+filter.ID+"%";
                sql += "AND ID LIKE @Productfilter";
            }


            var products = await _data.DbConnection.QueryAsync<Products, Products,ProductDto>(sql, (product, product2) => 
                new ProductDto
                {
                    ID = product.ID,
                    Description = product2.Description
                }, param: (object) param, splitOn: "ID"
            
            );


            return products.ToList();
            
            
            




        }

    }
}