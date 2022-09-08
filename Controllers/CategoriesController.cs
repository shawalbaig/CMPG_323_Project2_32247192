using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cmpg323project2try.Models;
using System.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;





namespace cmpg323project2try.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly databaseContext _context;

        public CategoriesController(databaseContext context)
        {
            _context = context;
        }

        /*// GET: api/Categories
         [HttpGet]
         public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
         {
             return await _context.Category.ToListAsync();
         }
        */
        // GET: api/Categories/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Category>> GetCategory(Guid id)
        //{
        //    var category = await _context.Category.FindAsync(id);

        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return category;
        //}
        // PUT: api/Categories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Category.Add(category);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CategoryExists(category.CategoryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }



        [HttpGet]
        public IEnumerable<Category> Get()
        {
            using (databaseContext db = new databaseContext())
            {
                return db.Category.ToList();
            }

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetUsingId(Guid id)
        {

            //var catid = _context.Category.FirstOrDefaultAsync(t=>id == t.CategoryId);
            try
            {
                var catid = await _context.Category.FindAsync(id);

                if (catid == null)
                {
                    return NotFound();

                }
                else
                {
                    return catid;
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }

        }

        //ELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> Delete(Guid id)
        {
            using (databaseContext db = new databaseContext())
            {
                //var value =  db.Category.FirstOrDefault(e => e.CategoryId == id);
                //var value = await _context.Category.FirstOrDefault(e => e.CategoryId == id);
                var value = await _context.Category.FindAsync(id);
                if (value == null)
                {
                    return NotFound();
                }
                _context.Category.Remove(value);
                await _context.SaveChangesAsync();

                return NoContent();

            }
        }
        private bool CategoryExists(Guid id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }



        // Patch method 

        [HttpPatch("{id}")]

        public string Update(Guid id,/*[FromBody]*/ string CategoryName, /*string CategoryDescription,*/ Category category)
        {

            var cat = _context.Category.FirstOrDefault(e => e.CategoryId == id);

            /*var des = _context.Category.FirstAsync(d => d.CategoryDescription == CategoryDescription);*/

            cat.CategoryName = CategoryName;
            //cat.CategoryDescription = CategoryDescription;
            return "returned Selected Category:" + cat.CategoryId + ", Updated category name:" + cat.CategoryName + " " /*+ cat.CategoryDescription*/;

        }

        /* var cat = _context.Category.FirstOrDefault(e => e.CategoryId == id);
           var des = _context.Category.FirstAsync(d => d.CategoryDescription == CategoryDescription);
           cat.CategoryName = CategoryName;
               cat.CategoryDescription = CategoryDescription;


               // cat.CategoryName = CategoryName;
               // cat.CategoryDescription = CategoryDescription;
                return "returned Selected Category:" + cat.CategoryId + ", Updated category name:" + cat.CategoryName + " " + cat.CategoryDescription;


            delete method

           [HttpDelete("{id}")]
           public async Task<IActionResult> deleteCategory(Guid id)
           {
               if (_context.Category == null)
               {
                   return NotFound();

               }
               var category = await _context.Category.FindAsync(id);
               if (category == null)
               {
                   return NotFound();
               }
               _context.Category.Remove(category);
               await _context.SaveChangesAsync();
               return NoContent();


           }
           [HttpDelete("{id}")]
           public async Task<ActionResult<Category>> DeleteCategory(Guid id)
           {
               using (databaseContext database = new databaseContext())
               {
                   var category = await _context.Category.FindAsync(id);
                   if (category == null)
                   {
                       return NotFound();

                   }
                   _context.Category.Remove(category);
                   await _context.SaveChangesAsync();

                   return NoContent();
               }

           }*/

    }
}






//PATCH Method




//POST Method


//PUT Method



//DELETE Method
