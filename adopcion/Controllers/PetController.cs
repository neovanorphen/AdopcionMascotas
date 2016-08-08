using Adopcion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Adopcion.Controllers
{
    public class PetController : Controller
    {
        //
        // GET: /Pet/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult  Details (long id)
        {
            var pet = new Pet();
            pet.Details(id);
            return View(pet);
        }

        [HttpPost]
        public ActionResult Details(Pet pet)
        {
            
            pet.Adopt();
            return RedirectToAction("Details/"+pet.ID);
        }


        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Pet pet)
        {
            pet.Add();
            return View("AddDetails", pet);
            
        }


        public ActionResult Find()
        {
            return View("FindGet");
        }

        [HttpPost]
        public ActionResult Find(int city, int race, int type)
        {
            var pets = new Models.Pet().Find(city,race,type);
            return View(pets);

        }


        public ActionResult Edit(long id)
        {
            var pet = new Pet();
            pet.Details(id);

            return View(pet);


        }

        [HttpPost]
        public ActionResult Edit(Pet pet)
        {

            pet.Edit();
            return RedirectToAction("Details" ,pet);
        }



    }
}
