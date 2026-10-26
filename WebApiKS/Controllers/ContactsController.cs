using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiKS.Data;
using WebApiKS.Models;

namespace ContactsAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        // Dependency Injection
        private readonly ContactsAPIDbContext _dbContext;

        public ContactsController(ContactsAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetContacts() // Listeleme yapar
        {
            return Ok(await _dbContext.Contacts.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact([FromRoute] int id) // id'si verilen kişiyi listeleme
        {
            var contact = await _dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }
        [HttpGet("GetContactByName/{adi}")]
        public async Task<IActionResult> GetContactByName(string adi)
        {
            var contacts = await _dbContext.Contacts
                .Where(c => c.Adi.ToLower().Contains(adi.ToLower()))
                .ToListAsync();

            if (contacts.Count == 0)
            {
                return NotFound("Bu ada sahip bir kişi bulunamadı.");
            }

            return Ok(contacts);
        }
        [HttpGet("GetContactByPhone/{telefon}")]
        public async Task<IActionResult> GetContactByPhone(long telefon)
        {
            var contact = await _dbContext.Contacts
                .FirstOrDefaultAsync(c => c.Telefon == telefon);

            if (contact == null)
            {
                return NotFound("Bu telefon numarasına sahip bir kişi bulunamadı.");
            }

            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] Contact addContextRequest) //ekleme
        {
            var contact = new Contact()
            {
                Adi = addContextRequest.Adi,
                Soyadi = addContextRequest.Soyadi,
                Telefon = addContextRequest.Telefon,
                Iletisim = addContextRequest.Iletisim,
                Cinsiyet = addContextRequest.Cinsiyet,
                Adres = addContextRequest.Adres
            };
            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();
            return Ok(contact);
        }
        //yepyeni satır eklendi v
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, Contact updateContextRequest) //id'ye göre güncelleme
        {
            var contact = await _dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                contact.Adi = updateContextRequest.Adi;
                contact.Soyadi = updateContextRequest.Soyadi;
                contact.Telefon = updateContextRequest.Telefon;
                contact.Iletisim = updateContextRequest.Iletisim;
                contact.Cinsiyet = updateContextRequest.Cinsiyet;
                contact.Adres = updateContextRequest.Adres;
                await _dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id) //id'ye göre silme
        {
            var contact = await _dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                _dbContext.Remove(contact);
                await _dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();  
        }
        
    }
}