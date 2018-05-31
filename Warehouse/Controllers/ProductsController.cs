using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    public class ProductsController : Controller
    {
        private WarehouseContext db = new WarehouseContext();

        // GET: Products
        public ActionResult Index()
        {
            //return View(db.Products); //funkar ej alltid, för frågan är "deferred", och gc kan städa bort WarehouseContext
            //using (var dbContext = new WarehouseContext())
            //{
            //    //return View(db.Products); //funkar ej
            //    return View(db.Products.ToList()); //funkar - ToList() gör att frågan exekveras och är omvandlad till ista
            //}
            var list = db.Products.ToList();
            if (ControllerContext.IsChildAction)
            {
                return PartialView(list);
                //return PartialView("_ProductTable", list); //TODO: fixa partial view för produkterna!!!
            }
            else
            {
                return View(list);
            }
        }

        public ActionResult IndexPartial()
        {
            return PartialView("Index", db.Products.ToList());  //Anger vyn som ska användas, istf default IndexPartial
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Quantity,Category,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Details", new { product.Id });
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var productEdit = Mapper.Map<ProductEditViewModel>(product);

            //ProductEditViewModel productEdit = new ProductEditViewModel
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Category = product.Category,
            //    Description = product.Description,
            //    Price = product.Price
            //};
            return View(productEdit);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductEditViewModel productEdit) //POCO (Entity) 
        {
            if (!ModelState.IsValid) return View(productEdit);

            var product = Mapper.Map<Product>(productEdit);
            //var product = new Product
            //{
            //    Id = productEdit.Id,
            //    Name = productEdit.Name,
            //    Category = productEdit.Category,
            //    Description = productEdit.Description,
            //    Price = productEdit.Price
            //};

            db.Entry(product).State = EntityState.Modified;
            db.Entry(product).Property(p => p.Quantity).IsModified = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
