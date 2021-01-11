using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CURD_Product_DHH.Model
{
    public class Product
    {
        
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime Creation { get; set; }
        public DateTime Modification { get; set; }
    }
    /// <summary>
    /// Clase de funciones crud de tabla products
    /// </summary>
    public class ADO_DB
    {
       SqlConnection cnx = Conn.Instance.GetCnx(); 
        /// <summary>
        /// obtener todos los productos de la base de datos
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAllProducts()
        {
            List<Product> Products = new List<Product>();
            if (cnx != null) {
                try
                {
                    SqlCommand cmd = new SqlCommand("Get_Del_Products", cnx);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Event", "GET:AllPorducts");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Product p = new Product
                        {
                            Id = int.Parse(dr["Id"].ToString()),
                            Name = dr["Name"].ToString(),
                            Price = decimal.Parse(dr["Price"].ToString()),
                            Creation = DateTime.Parse(dr["Creation"].ToString()),
                            Modification = DateTime.Parse(dr["Modification"].ToString()),

                        };
                        Products.Add(p);
                    }
                }
                catch (Exception e) {
                    return null;
                }               
            }
            return Products;
        } 
        /// <summary>
        /// Obtener producto por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Product GetProductById(int Id)
        {
            if(cnx != null)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Get_Del_Products", cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Event", "GET:ProductsById");
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cnx.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    Product P = new Product();
                    if (rd.Read())
                    {
                        P.Id = int.Parse(rd["Id"].ToString());
                        P.Name = rd["Name"].ToString();
                        P.Price = decimal.Parse(rd["Price"].ToString());
                        P.Creation = DateTime.Parse(rd["Creation"].ToString());
                        P.Modification = DateTime.Parse(rd["Modification"].ToString());                     
                    }
                    cnx.Close();
                    return P;                  
                }
                catch
                {
                    return null;
                }
            }
            else { return null; }           
        }
        /// <summary>
        /// eliminar un producto de la tabla
        /// </summary>
        /// <param name="Id">identificador del producto</param>
        /// <returns></returns>
        public  string  DeleteProduct(int Id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Get_Del_Products", cnx);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Event", "DEL:ProductById");
                cmd.Parameters.AddWithValue("@Id", Id);
                cnx.Open();
                cmd.ExecuteNonQuery();
                cnx.Close();
                return "Ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }            
        }
        /// <summary>
        /// Registrar un nuevo producto
        /// </summary>
        /// <param name="Name">Nombre del producto</param>
        /// <param name="Price">Precio del producto</param>
        public void PostProduct(string Name,decimal Price)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Post_Put_Products", cnx);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Event", "POST:InsertNewProdut");
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Price", Price);
                cnx.Open();
                cmd.ExecuteNonQuery();
                cnx.Close();
            }
            catch (Exception)
            {
                throw;
            }            
        }
        /// <summary>
        /// Actiualizar un ptoducto
        /// </summary>
        /// <param name="Id">identificador del producto</param>
        /// <param name="Name">nuevo nombew del producto</param>
        /// <param name="Price">Nuevo precio del producto</param>
        public void PutPoduct(int Id, string Name, decimal Price)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Post_Put_Products", cnx);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Event", "POST:InsertNewProdut");
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Price", Price);
                cnx.Open();
                cmd.ExecuteNonQuery();
                cnx.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    /// <summary>
    /// Clase de conexiones SQL
    /// </summary> 
    public class Conn 
    {
       
        private static Conn instance = null;
        private Conn() { }
        /// <summary>
        /// Crear la instancia de la clase
        /// </summary>
        public static Conn Instance
        {
            get
            {
                if (instance == null)
                    instance = new Conn();
                return instance;
            }           
        }
        /// <summary>
        /// realizar la conexion con el servidor de base de datos
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetCnx()
        {
            try
            {
                SqlConnection cnx = new SqlConnection("Server=tcp:share-sql-test.database.windows.net,1433;Initial Catalog=precandidates;Persist Security Info=False;User ID=candidatos;Password=43yqBMgVZh8WH2wgzhusuYbk2y1Q;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                cnx.Open();
                cnx.Close();
                return cnx;
            }
            catch (Exception e){
                return null;
            }            
        }
    }
}
