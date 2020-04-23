using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Shop.Controllers
{
  [Route("v1/products")]
  public class ProductController : ControllerBase
  {

    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    public async Task<ActionResult<List<Product>>> Get([FromServices]DataContext context)
    {
      var products = await context.Products.Include(x => x.Category).AsNoTracking().ToListAsync();
      return products;      
    }

    [HttpGet]
    [Route("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<Product>> GetById(int id, [FromServices]DataContext context)
    {
      var product = await context
          .Products
          .Include(x => x.Category)
          .AsNoTracking()
          .FirstOrDefaultAsync(x => x.Id == id);
      return product;      
    }

    [HttpGet]
    [Route("categories/{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<List<Product>>> GetByCategory(int id, [FromServices]DataContext context)
    {
      var products = await context
          .Products
          .Include(x => x.Category)
          .AsNoTracking()
          .Where(x => x.Category.Id == id)
          .ToListAsync();
        return products;
          
    }

  [HttpPost]
  [Route("")]
  [Authorize(Roles="employee")]
  public async Task<ActionResult<List<Product>>> Post(
    [FromBody]Product model, 
    [FromServices]DataContext context
  )
  {
    if(ModelState.IsValid)
    {
      context.Products.Add(model);
      await context.SaveChangesAsync();
      return Ok(model);
    }
    else
    {
      return BadRequest(new {message = "Não foi possível criar o produto"});    
    }
  }

  }
}