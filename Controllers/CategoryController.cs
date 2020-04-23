using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;

[Route("v1/categories")]
public class CategoryController : ControllerBase{

  [HttpGet]
  [Route("")]
  [AllowAnonymous]
  public async Task<ActionResult<List<Category>>> Get(
    [FromServices]DataContext context
  )
  {
    var categories = await context.Categorys.AsNoTracking().ToListAsync();
    return Ok(categories);
  }

  [HttpGet]
  [Route("{id:int}")]
  [AllowAnonymous]
  public async Task<ActionResult<Category>> GetById(
    int id,
    [FromServices]DataContext context
  )
  {
    var categories = await context.Categorys.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    return Ok(categories);
  }

  [HttpPost]
  [Route("")]
  [Authorize(Roles="employee")]
  public async Task<ActionResult<List<Category>>> Post(
    [FromBody]Category model, 
    [FromServices]DataContext context
  )
  {
    if(!ModelState.IsValid)
      return BadRequest(ModelState);

    try
    {
      context.Categorys.Add(model);
      await context.SaveChangesAsync();
      return Ok(model);
    }
    catch
    {
      return BadRequest(new {message = "Não foi possível criar a categoria"});    
    }
  }

  [HttpPut]
  [Route("{id:int}")]
  [Authorize(Roles="employee")]
  public async Task<ActionResult<List<Category>>> Put(
    int id,
    [FromBody]Category model,
    [FromServices]DataContext context
  )
  {
    if(id != model.Id)
      return NotFound(new {message = "Categoria não encontrada"});

    if(!ModelState.IsValid)
      return BadRequest(ModelState);
    
    try
    {
      context.Entry<Category>(model).State = EntityState.Modified;
      await context.SaveChangesAsync();
      return Ok(model);
    }
    catch (DbUpdateConcurrencyException)
    {
      return BadRequest(new {message = "Este registro já foi atualizado"});
    }
    catch (Exception)
    {
      return BadRequest(new {message = "Não  registro já foi atualizado"});
    }
    
  }

  [HttpDelete]
  [Route("{id:int}")]
  [Authorize(Roles="employee")]
  public async Task<ActionResult<List<Category>>> Delete (
    int id,
    [FromServices]DataContext context)
  {
    var category = await context.Categorys.FirstOrDefaultAsync(x => x.Id == id);
    
    if(category == null)
      return NotFound(new {message = "Categoria não encontrada."});

    try
    {
      context.Categorys.Remove(category);
      await context.SaveChangesAsync();
      return Ok(new {message = "categoria excluida com sucesso."});
    }
    catch(Exception)
    {
      return NotFound(new {message = "Não foi possível removar a categoria"});
    }    
  }


}