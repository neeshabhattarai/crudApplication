using crudApplication.Models;
using crudApplication.Service;
using Microsoft.AspNetCore.Mvc;

namespace crudApplication.Controllers
{
    public class ItemController : Controller
    {
        private readonly  ApplicationDbContext context;
        public ItemController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var ItemList=context.ListItem.ToList();
            return View(ItemList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Item item)
        {

            context.ListItem.Add(item);
            context.SaveChanges();
            return RedirectToAction("Index","Item");
        }
        public IActionResult Edit(int id) { 
        Item item=context.ListItem.Find(id);
            if (item == null)
            {
                return RedirectToAction("Index", "Item");
            }
            return View(item);

        }
        [HttpPost]
        public IActionResult Edit(int id,Item item)
        {
            Item EditItem = context.ListItem.Find(id);
            if (item == null)
            {
                return RedirectToAction("Index", "Item");
            }
            EditItem.Name= item.Name;
            EditItem.Description= item.Description;
            context.SaveChanges();
            return RedirectToAction("Index","Item");

        }
        public IActionResult Delete(int id) {
            var item=context.ListItem.Find(id);
            context.ListItem.Remove(item);
            context.SaveChanges();
            return RedirectToAction("Index", "Item");
        }

    }
}
