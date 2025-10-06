using Microsoft.AspNetCore.Mvc;
using SCoffee.Models.Domain;
using SCoffee.Models.Interfaces;

namespace CoffeeShop.Controllers
{
    public class ContactController : Controller
    {
        private IContactRepository contactRepository;
        public ContactController(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }
        public ActionResult Add()
        {
            return View();
        }

        // POST: /Contact/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    contactRepository.AddContact(contact);
                    contactRepository.SaveChanges();
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "An error occurred while processing your request. Please try again later.";
                    return View(contact);
                }
            }
            return View(contact);
        }
    }
}
