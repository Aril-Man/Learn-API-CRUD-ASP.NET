using ContactApi.Data;
using ContactApi.Models;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactApi.Controllers
{
    [ApiController]
    [Route("api/contact")]
    public class ContactController : Controller
    {
        private readonly ContactDbContext dbContext;

        public ContactController(ContactDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetOneContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> InsertContact(ContactRequest request)
        {
            var contact = new Contact()
            {
                id = Guid.NewGuid(),
                name = request.name,
                phone = request.phone,
                address = request.address,
            };
            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, ContactRequest request)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            contact.name = request.name;
            contact.phone = request.phone;
            contact.address = request.address;
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            
            if (contact == null)
            {
                return NotFound();
            }

            dbContext.Remove(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }
    }
}
