using crudApplication.Models;
using crudApplication.Service;
using Microsoft.AspNetCore.Mvc;

namespace crudApplication.Controllers
{
    public class ItemController : Controller
    {
        private readonly  ApplicationDbContext context;
        private int pagesize=5;
        IQueryable<Item> queryable;
        public ItemController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index(int pageNumber, string? search)
        {
           
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            queryable = context.ListItem;
            if (search != null)
            {
                queryable = queryable.Where(val => val.Name == search || val.Description==search);
            }

            decimal totalPage = Math.Ceiling((decimal)queryable.Count() / pagesize);
            
          
            var ItemList=queryable.Skip((pageNumber-1)*pagesize).Take(pagesize).ToList();
           
            ViewData["totalPage"] =(int) totalPage;
            ViewData["pageNumber"] = (int)pageNumber;
            ViewData["search"] = search;
            return View(ItemList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Item item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

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
