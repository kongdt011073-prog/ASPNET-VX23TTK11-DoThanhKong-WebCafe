
using Microsoft.EntityFrameworkCore;
using SCoffee.Data;
using SCoffee.Models.Domain;
using SCoffee.Models.Interfaces;

namespace SCoffee.Models.Services
{
    public class ContactRepository: IContactRepository
    {
        private SCoffeeDbContext dbContext;
        public ContactRepository(SCoffeeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void AddContact(Contact contact)
        {
            dbContext.Contacts.Add(contact);
        }
        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

    }
}
