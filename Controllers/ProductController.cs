using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CURD_Product_DHH.Model;
using Microsoft.AspNetCore.Mvc;
 
namespace CURD_Product_DHH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {        
        [HttpGet]
        //Retornar todos los productos
        public IEnumerable<Product> Get()
        {
            ADO_DB adb = new ADO_DB();
        
            List<Product> products=  adb.GetAllProducts();

            return products.AsEnumerable();            
        }
        //  buscar un producto en especifico 
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            ADO_DB adb = new ADO_DB();
            Product product = adb.GetProductById(id);
            return product;
        }
        // Rgistrar un nuevo producto
        [HttpPost]
        public void Post([FromBody] Product P )
        {
            ADO_DB adb = new ADO_DB();
            adb.PostProduct(P.Name, P.Price);        
        }
        // Actualizar un registro
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product P)
        {
            ADO_DB adb = new ADO_DB();
            adb.PutPoduct(id,P.Name, P.Price);
        }
        // Elimnar un producto
        [HttpDelete("{id}")]
        public void Delete(int id) { 
            ADO_DB adb = new ADO_DB();
           adb.DeleteProduct(id);
         
        }
    }

}
