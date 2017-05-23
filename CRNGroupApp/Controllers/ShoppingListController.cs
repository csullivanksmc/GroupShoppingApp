using CRNGroupApp.Models;
using CRNGroupApp.Controllers;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Mvc;


namespace CRNGroupApp.Models
{
    public class ShoppingListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ShoppingListModel
        public ActionResult Index()
        {
            //var shoppingListItems = db.ShoppingListItems.Include(s => s.ShoppingList);
            return View(db.ShoppingLists.ToList());
        }

        // GET: ShoppingListModel/Details/5
        public ActionResult Details(int? id)
        {

            //replace with shoppinglistitem index
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.ShoppingList shoppingListModel = db.ShoppingLists.Find(id);
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
            //shoppinglistitems, reference shoppinglistitems in db with shoppinglistIDs that match that id submitted
            //Models.ShoppingList shoppingListIndex = db.ShoppingLists.Find(id);
            //if (shoppingListIndex == null)
            //{
            //    return HttpNotFound();
            //}
            ////shopping lists with a specific ID as found above - display shopping list items from that list.
            //return View(shoppingListIndex.ShoppingListItems);

            ViewBag.ShoppingListId = id;
            ViewBag.ListTitle = db.ShoppingLists.Find(id).Name;
            ViewBag.ShoppingListColor = db.ShoppingLists.Find(id).Color;
            return View(db.ShoppingListItems.Where(s => s.ShoppingListId == id));

        }

        //POST: UpdateCheckBox
        [HttpPost]
        //[ValidateAntiForgeryToken]  //referencing id in order to update IsChecked,creating a new instance of class and calling it "shoppingListItem"
        public ActionResult UpdateCheckbox([Bind(Include = "ShoppingListItemId, IsChecked")] CRNGroupApp.Models.ShoppingListItem shoppingListItem)
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
            //ViewBag.ListTitle = db.ShoppingLists.Find(id).Name;
            return View();
        }

        // POST: ShoppingList/CreateItem
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateItem([Bind(Include = "ShoppingListItemId,ShoppingListId," +
                                                       "Content,Priority,Note,IsChecked,CreatedUtc,ModifiedUtc")]
                                                        CRNGroupApp.Models.ShoppingListItem shoppingListItem, int id)
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
        public ActionResult Create([Bind(Include = "ShoppingListId,UserId,Name,Color,CreatedUtc,ModifiedUtc")] CRNGroupApp.Models.ShoppingList shoppingListModel)
        {
            if (ModelState.IsValid)
            {
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
            CRNGroupApp.Models.ShoppingList shoppingListModel = db.ShoppingLists.Find(id);
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
        public ActionResult Edit([Bind(Include = "ShoppingListId,UserId,Name,Color,CreatedUtc,ModifiedUtc")] CRNGroupApp.Models.ShoppingList shoppingListModel)
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
            CRNGroupApp.Models.ShoppingList shoppingListModel = db.ShoppingLists.Find(id);
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
            Models.ShoppingList shoppingListModel = db.ShoppingLists.Find(id);
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
    }
}
