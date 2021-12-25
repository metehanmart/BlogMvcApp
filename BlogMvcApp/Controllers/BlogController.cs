using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogMvcApp.Models;

namespace BlogMvcApp.Controllers
{
    public class BlogController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Blog
        public ActionResult List(int? id, String q)
        {
            var bloglar = db.Bloglar.Where(i => i.Onay == true)
                .Select(i => new BlogModel()
                {
                    Id = i.Id,
                    Baslik = i.Baslik.Length > 100 ? i.Baslik.Substring(0, 100) + "..." : i.Baslik,
                    Aciklama = i.Aciklama,
                    EklenmeTarihi = i.EklenmeTarihi,
                    Anasayfa = i.Anasayfa,
                    Onay = i.Onay,
                    Resim = i.Resim,
                    CategoryId = i.CategoryId
                }).AsQueryable();

            if (string.IsNullOrEmpty(q) == false)
            {
                bloglar = bloglar.Where(i => i.Aciklama.Contains(q) || i.Baslik.Contains(q));
            }
            if(id != null)
            {
                bloglar = bloglar.Where(i => i.CategoryId == id);
            }
                
            return View(bloglar.ToList());
        }

        

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
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
