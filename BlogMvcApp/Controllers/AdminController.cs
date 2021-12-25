using BlogMvcApp.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogMvcApp.Controllers
{
    public class AdminController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Admin
        public string adm;
        public ActionResult Index()
        {
            return View();
        }
        private bool Authenticate(Admin a)
        {
            var admin = db.Adminler.Where(i => i.userName == a.userName && i.password == a.password).FirstOrDefault();
            
            if (admin!=null)
            {
                // Warning: you should not be redirecting here. You should only
                // create and set the authentication cookie. The redirect should be done
                // in an MVCish way, i.e. by returning a RedirectToAction result if this
                // method returns true
                adm = (string)admin.userName;
                FormsAuthentication.SetAuthCookie(admin.userName, false);
                return true;
            }

            // wrong username or password
            return false;
        }
        [HttpPost]
        public ActionResult Index(Admin a)
        {
            var admin = db.Adminler.Where(i => i.userName == a.userName && i.password == a.password).FirstOrDefault();
            if(Authenticate(a))
            {
                TempData["Username"] = a.userName;
                return RedirectToAction("List", "Admin");
            }
            else
            {
                ViewBag.Message = string.Format("Kullanıcı Adı veye şifre yanlış");
                return View();
            }
            
        }
        [Authorize]
        public ActionResult List()
        {
            
            var bloglar = db.Bloglar.Include(b => b.Category).OrderByDescending(i => i.EklenmeTarihi);
            return View(bloglar.ToList());
        }

        public ActionResult Edit(int? id) 
        {
            TempData["username"] = adm;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Bloglar.Find(id);
            if(blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Kategoriler, "Id", "KategoriAdi", blog.CategoryId);
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Baslik,Aciklama,Resim,Icerik,Onay,Anasayfa,CategoryId")] Blog blog)
        {   
            if (ModelState.IsValid)
            {
                var entity = db.Bloglar.Find(blog.Id);
                if (entity != null)
                {
                    entity.Baslik = blog.Baslik;
                    entity.Aciklama = blog.Aciklama;
                    entity.Resim = blog.Resim;
                    entity.Icerik = blog.Icerik;
                    entity.Onay = blog.Onay;
                    entity.Anasayfa = blog.Anasayfa;
                    entity.CategoryId = blog.CategoryId;

                    db.SaveChanges();
                    TempData["Blog"] = entity;//bilgi tasimak icin
                    return RedirectToAction("List");
                }

            }
            ViewBag.CategoryId = new SelectList(db.Kategoriler, "Id", "KategoriAdi", blog.CategoryId);
            return View(blog);
        }
        
        
        // GET: Blog/Create
        [Authorize]
        public ActionResult Create()
        {
            TempData["username"] = adm;
            ViewBag.CategoryId = new SelectList(db.Kategoriler, "Id", "KategoriAdi");
            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Baslik,Aciklama,Resim,Icerik,CategoryId")] Blog blog)
        {
            
            if (ModelState.IsValid)
            {
                blog.EklenmeTarihi = DateTime.Now;

                db.Bloglar.Add(blog);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            ViewBag.CategoryId = new SelectList(db.Kategoriler, "Id", "KategoriAdi", blog.CategoryId);
            return View(blog);
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            TempData["username"] = adm;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Bloglar.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Bloglar.Find(id);
            db.Bloglar.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string username, string password)
        {
            return RedirectToAction("Index"); 
        }
    }
}