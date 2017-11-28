using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MCDataBookLibrary.Context;
using MCDataBookLibrary.Entities;
using PagedList;

namespace OnlineBookLibrary.Controllers
{
    public class BooksController : Controller
    {
        private LibraryDbContext db = new LibraryDbContext();

        // GET: Books
        public ActionResult Index(int? page, string titleSearch, string sortOrder)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            IQueryable<Book> books = db.Books.AsQueryable();

            ViewBag.TitleSearch = titleSearch;

            if (!String.IsNullOrEmpty(titleSearch))
            {
                books = books.Where(x => x.Title.Contains(titleSearch));
            }
            ViewBag.CurrentSortParm = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.WriterSortParm = sortOrder == "writer_asc" ? "writer_desc" : "writer_asc";
            switch (sortOrder)
            {
                case "title_desc":
                    books = books.OrderByDescending(x => x.Title);
                    break;
                case "writer_asc":
                    books = books.OrderBy(x => x.Writer.FirstName);
                    break;
                case "writer_desc":
                    books = books.OrderByDescending(x => x.Writer.FirstName);
                    break;
                default:
                    books = books.OrderBy(x => x.Title);
                    break;
            }

            return View(books.ToPagedList(pageNumber, pageSize));
        }

        // GET: Books/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {

            ViewBag.Writers = new SelectList(db.Writers, "Id", "FirstName");
            ViewBag.Genres = new SelectList(db.Genres, "Id", "GenreName");
           
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "bookId,Title,ReleaseDate,AuthorId,WriterId,GenreId,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.Writers = new SelectList(db.Writers, "Id", "FirstName");
            ViewBag.Genres = new SelectList(db.Genres, "Id", "GenreName");

            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            ViewBag.Writers = new SelectList(db.Writers, "Id", "FirstName");
            ViewBag.Genres = new SelectList(db.Genres, "Id", "GenreName");

            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "bookId,Title,ReleaseDate,AuthorId,WriterId,GenreId,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Writers = new SelectList(db.Writers, "Id", "FirstName");
            ViewBag.Genres = new SelectList(db.Genres, "Id", "GenreName");

            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
