using Microsoft.EntityFrameworkCore;
using WebApiKS.Models;

namespace WebApiKS.Data
{
    public class ContactsAPIDbContext : DbContext //DbContext'ten miras alıyor
    {
        public ContactsAPIDbContext(DbContextOptions options) : base(options)
        {
            //options ile bağlantı dizesi alınmış olacak
        }
        public DbSet<Contact> Contacts { get; set; } //Tablo
    }
}




