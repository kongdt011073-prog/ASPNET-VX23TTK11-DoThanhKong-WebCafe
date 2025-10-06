using SCoffee.Models.Domain;

namespace SCoffee.Models.Interfaces
{
    public interface IContactRepository
    {
        void AddContact(Contact contact);
        void SaveChanges();
    }
}
