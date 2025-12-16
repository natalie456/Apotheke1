using Apotheke1.Data;
using Apotheke1.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apotheke1.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicinesApiController : ControllerBase
    {
        private readonly ApothekeDbContext _db;

        public MedicinesApiController(ApothekeDbContext db)
        {
            _db = db;
        }

        // GET: api/MedicinesApi
        [HttpGet]
        public async Task<ActionResult<List<Medicine>>> GetAll()
        {
            var meds = await _db.Medicines
                .Include(m => m.Category)
                .Include(m => m.Supplier)
                .ToListAsync();

            return Ok(meds);
        }

        // GET: api/MedicinesApi/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Medicine>> GetById(int id)
        {
            var med = await _db.Medicines
                .Include(m => m.Category)
                .Include(m => m.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (med == null) return NotFound();
            return Ok(med);
        }

        // POST: api/MedicinesApi
        [HttpPost]
        public async Task<ActionResult<Medicine>> Create([FromBody] Medicine med)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _db.Medicines.Add(med);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = med.Id }, med);
        }

        // PUT: api/MedicinesApi/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Medicine med)
        {
            if (id != med.Id) return BadRequest("Id mismatch");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var exists = await _db.Medicines.AnyAsync(x => x.Id == id);
            if (!exists) return NotFound();

            _db.Entry(med).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/MedicinesApi/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var med = await _db.Medicines.FindAsync(id);
            if (med == null) return NotFound();

            _db.Medicines.Remove(med);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
