using crudApplication.Models;
using crudApplication.Service;
using Microsoft.AspNetCore.Mvc;

namespace crudApplication.Controllers
{
    [Route("Admin/[action]")]
    public class ItemController : Controller
    {
        private readonly  ApplicationDbContext context;
        private int pagesize=5;
        IQueryable<Item> queryable;
        public IWebHostEnvironment environment;
        public ItemController(ApplicationDbContext context,IWebHostEnvironment enviornment)
        {
            this.context = context;
            this.environment = enviornment;
        }

        public IActionResult Index(int pageNumber, string? search,string? column,string? orderBy)
        {
            var validlistColumn = new List<String> { "Id", "Name","Description" };
            var validOrder = new List<String> { "asc", "desc" };
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            queryable = context.ItemLists;
            if (search != null)
            {
                queryable = queryable.Where(val => val.Name == search || val.Description==search);
            }
            decimal totalPage = Math.Ceiling((decimal)queryable.Count() / pagesize);
            if (!validlistColumn.Contains(column))
            {
                column = "Id";
            }

            if (!validOrder.Contains(orderBy))
            {
                orderBy = "asc";
            }
            if (validlistColumn.Contains(column))
            {
                if (column == "Name")
                {
                    if (orderBy == "asc")
                    {
                        queryable=queryable.OrderBy(val => val.Name);
                    }
                    else
                    {
                        queryable=queryable.OrderByDescending(val => val.Name);
                    };
                }else if (column == "Id") {
                    if (orderBy == "asc")
                    {
                        queryable = queryable.OrderBy(val => val.Id);
                    }
                    else
                    {
                        queryable = queryable.OrderByDescending(val => val.Id);
                    }
                    ;
                }else if (column == "Description")
                {
                    if (orderBy == "asc")
                    {
                        queryable = queryable.OrderBy(val => val.Description);
                    }
                    else
                    {
                        queryable = queryable.OrderByDescending(val => val.Description);
                    }

                }
                
            }
            


                var ItemList = queryable.Skip((pageNumber - 1) * pagesize).Take(pagesize).ToList();
           
            ViewData["totalPage"] =(int) totalPage;
            ViewData["pageNumber"] = (int)pageNumber;
            ViewData["search"] = search;
            ViewData["orderBy"] = orderBy;
            ViewData["column"] = column;
            Console.WriteLine(ItemList);
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

            if (item.Image == null)
            {
                return View(item);
            }
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmfff");
            fileName += Path.GetExtension(item.Image.FileName);
            var imagePath = environment.WebRootPath + "/product/" + fileName;
            using (var stream = System.IO.File.Create(imagePath))
            {
                item.Image.CopyTo(stream);
            }
            Item itemList = new Item()
            {
                Name = item.Name,
                Description = item.Description,
                ImageFileName = fileName,
                CreatedDate= DateTime.Now,
                
            };

            context.ItemLists.Add(itemList);
            context.SaveChanges();
            return RedirectToAction("Index","Item");
        }
        public IActionResult Edit(int id) { 
        Item item=context.ItemLists.Find(id);
            if (item == null)
            {
                return RedirectToAction("Index", "Item");
            }
            ViewData["ImageName"] = item.ImageFileName;
            return View(item);

        }
        [HttpPost]
        public IActionResult Edit(int id,Item item)
        {
            Item EditItem = context.ItemLists.Find(id);
            if (item == null)
            {
                return RedirectToAction("Index", "Item");
            }
            if (!ModelState.IsValid)
            {
                return View(item);
            }
            EditItem.Name= item.Name;
            EditItem.Description= item.Description;
            string newFileName = item.ImageFileName;
            if (item.Image != null)
            {
                 newFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                newFileName+= Path.GetExtension(item.Image.FileName);
                var filePath = environment.WebRootPath + "/product/" + newFileName;
               using(var stream = System.IO.File.Create(filePath))
                {
                    item.Image.CopyTo(stream);
                }
                string oldPath = environment.WebRootPath + "/product/"+item.ImageFileName;
                //System.IO.File.Delete(oldPath);

            }
            EditItem.ImageFileName= newFileName;
            context.SaveChanges();
            return RedirectToAction("Index","Item");

        }
        public IActionResult Delete(int id) {
            var item = context.ItemLists.Find(id);
            context.ItemLists.Remove(item);
            context.SaveChanges();
            return RedirectToAction("Index", "Item");
        }

    }
}
