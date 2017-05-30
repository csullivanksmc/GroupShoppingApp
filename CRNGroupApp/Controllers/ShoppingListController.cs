using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CRNGroupApp.Data;
using System;
using CRNGroupApp.Services;
using Microsoft.AspNet.Identity;
using PagedList;
using System.Xml.Linq;

namespace CRNGroupApp.Controllers
{
    public class ShoppingListController : NameController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ShoppingListModel
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var service = CreateListService();
            var model = service.GetLists();
            //var ctxdb = db.ShoppingLists
           //             .Where(e => e.UserId == Guid.Parse(User.Identity.GetUserId()));

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                
                var shopinglistso = from s in db.ShoppingLists
                                    select s;
                //model = shopinglistso.Where(s => s.Name.Contains(searchString));
                model = model.Where(s => s.Name.Contains(searchString));
            }
            else
            {
                searchString = currentFilter;
            }

            //CHRIS: YOU MADE THIS C LOWER CASE DONT FORGET TO REVERT IF THAT DOESNT SOVLE THE ISSUE
            ViewBag.currentFilter = searchString;

            var shopinglists = from s in db.ShoppingLists
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                shopinglists = shopinglists.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    shopinglists = shopinglists.OrderByDescending(s => s.Name);
                    break;
               
                    
                default:
                    shopinglists = shopinglists.OrderBy(s => s.Name);
                    break;
            }
            //var shoppingListItems = db.ShoppingListItems.Include(s => s.ShoppingList);
            

            return View(model.OrderBy(s => s.Name).ToPagedList(pageNumber, pageSize));
            }

           /* int pageSize = 3;
            int pageNumber = (page ?? 1);
            //var shoppingListItems = db.ShoppingListItems.Include(s => s.ShoppingList);
            return View(shopinglists.ToPagedList(pageNumber, pageSize));
        }*/

        // GET: ShoppingListModel/Details/5
        public ActionResult Details(int? id)
        {

            //replace with shoppinglistitem index
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Data.ShoppingList shoppingListModel = db.ShoppingLists.Find(id);
            if (shoppingListModel == null)
            {
                return HttpNotFound();
            }

            //var i = new ShoppingListItem {ShoppingListId = id.Value};
            return View(shoppingListModel);
        }

        //adding ViewItem to ShoppingListController

        // GET: ViewItem/View
        public ActionResult ViewItem(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            ViewBag.ShoppingListId = id;
            ViewBag.ListTitle = db.ShoppingLists.Find(id).Name;
            ViewBag.ShoppingListColor = db.ShoppingLists.Find(id).Color;
            return View(db.ShoppingListItems.Where(s => s.ShoppingListId == id));

        }

        //POST: UpdateCheckBox
        [HttpPost]
        //[ValidateAntiForgeryToken]  //referencing id in order to update IsChecked,creating a new instance of class and calling it "shoppingListItem"
        public ActionResult UpdateCheckbox([Bind(Include = "ShoppingListItemId, IsChecked")] ShoppingListItem shoppingListItem)
        {   //pulling data from db and holding it in memory
            var item = db.ShoppingListItems.Find(shoppingListItem.ShoppingListItemId);
            //referencing IsChecked on item and converting it to IsChecked on shoppingListItem
            item.IsChecked = shoppingListItem.IsChecked;
            //Save changes
            db.SaveChanges();
            return Json("success");
        }



        // GET: ShoppingListItem/Create
        public ActionResult CreateItem(int? id)
        {
            ViewBag.ShoppingListId = id;
            ViewBag.ListTitle = db.ShoppingLists.Find(id).Name;
            return View();
        }

        // POST: ShoppingList/CreateItem
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateItem([Bind(Include = "ShoppingListItemId,ShoppingListId," +
                                                       "Content,Priority,Note,IsChecked,CreatedUtc,ModifiedUtc")]
                                                        ShoppingListItem shoppingListItem, int id)
        {   //added parameter int id to "create".
            if (ModelState.IsValid)
            {   //add shoppinglistitems to a particular list prior to "add"
                shoppingListItem.ShoppingListId = id;
                db.ShoppingListItems.Add(shoppingListItem);
                db.SaveChanges();
                return RedirectToAction("ViewItem", new {id});
            }
            //trying to return to view of shopping list items on a particular list
            return View();
        }


        // GET: ShoppingListModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingListModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShoppingListId,UserId,Name,Color,CreatedUtc,ModifiedUtc")] Data.ShoppingList shoppingListModel)
        {
            if (ModelState.IsValid)
            {
                shoppingListModel.UserId = Guid.Parse(User.Identity.GetUserId());
                shoppingListModel.CreatedUtc = DateTimeOffset.Now;
                db.ShoppingLists.Add(shoppingListModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shoppingListModel);
        }

        //copy and paste both create methods from shoppinglistitem to create a list item

        // GET: ShoppingListModel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Data.ShoppingList shoppingListModel = db.ShoppingLists.Find(id);
            if (shoppingListModel == null)
            {
                return HttpNotFound();
            }
            return View(shoppingListModel);
        }

        // POST: ShoppingListModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShoppingListId,UserId,Name,Color,CreatedUtc,ModifiedUtc")] Data.ShoppingList shoppingListModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoppingListModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shoppingListModel);
        }

        // GET: ShoppingListModel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Data.ShoppingList shoppingListModel = db.ShoppingLists.Find(id);
            if (shoppingListModel == null)
            {
                return HttpNotFound();
            }
            return View(shoppingListModel);
        }

        // POST: ShoppingListModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Data.ShoppingList shoppingListModel = db.ShoppingLists.Find(id);
            db.ShoppingLists.Remove(shoppingListModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private ListService CreateListService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ListService(userId);
            return service;
        }

    }
}
