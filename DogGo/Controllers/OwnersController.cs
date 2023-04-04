using DogGo.Models;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogGo.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IOwnerRepository _ownerRepo;
        // ASP.NET will give us an instance of our Repository. This is called "Dependency Injection"

        public OwnersController(IOwnerRepository ownerRepository)
        {
            _ownerRepo = ownerRepository;   
        }
        // GET: OwnerController
        public ActionResult Index()
        {
            List<Owner> owners= _ownerRepo.GetOwners();
            return View(owners);
        }

        // GET: OwnerController/Details/5
        public ActionResult Details(int id)
        {
            Owner owner = _ownerRepo.GetOwner(id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // GET: OwnerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OwnerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Owner owner)
        {
            try
            {
                _ownerRepo.AddOwner(owner);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OwnerController/Edit/5
        public ActionResult Edit(int id)//view Owner detail to be Edited
        {
            Owner owner = _ownerRepo.GetOwner(id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: OwnerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Owner owner)
        {
            try
            {
                _ownerRepo.UpdateOwner(owner);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: OwnerController/Delete/5
        public ActionResult Delete(int id) //view Owner detail to be deleted
        {
            Owner owner = _ownerRepo.GetOwner(id);
            if (owner == null)
            {
                return NotFound();
            }
            return View(owner);
        }

        // POST: OwnerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Owner owner)
        {
            try
            {
                _ownerRepo.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
