using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogMvcApp.Models
{
    public class BlogInitializer: DropCreateDatabaseIfModelChanges<BlogContext>
    {
        protected override void Seed(BlogContext context)
        {
            List<Category> kategoriler = new List<Category>()
            {
                new Category(){KategoriAdi ="C#"},
                new Category(){KategoriAdi ="Asp.net MVC"},
                new Category(){KategoriAdi ="Asp.net WebForm"},
                new Category(){KategoriAdi ="Windows Form"},

            };
            foreach (var item in kategoriler)
            {
                context.Kategoriler.Add(item);
            }
            context.SaveChanges();
            List<Blog> bloglar = new List<Blog>()
            {
                new Blog(){Baslik = "C# Delagates Hakkında ", Aciklama="hadeee", EklenmeTarihi = DateTime.Now.AddDays(-10), Anasayfa = true, Onay = false, Resim = "1.jpg",CategoryId = 1 },
                new Blog(){Baslik = "C# Delagates Hakkında ", Aciklama="Çok uykum var benim", EklenmeTarihi = DateTime.Now.AddDays(-10), Anasayfa = false, Onay = true, Resim = "1.jpg",CategoryId = 1 },
                new Blog(){Baslik = "C# Delagates Hakkında ", Aciklama="Çok uykum var benim", EklenmeTarihi = DateTime.Now.AddDays(-10), Anasayfa = true, Onay = false, Resim = "1.jpg",CategoryId = 1 },
                new Blog(){Baslik = "C# Delagates Hakkında ", Aciklama="Çok uykum var benim", EklenmeTarihi = DateTime.Now.AddDays(-10), Anasayfa = false, Onay = true, Resim = "1.jpg",CategoryId = 2 },
                new Blog(){Baslik = "C# Delagates Hakkında ", Aciklama="Çok uykum var benim", EklenmeTarihi = DateTime.Now.AddDays(-10), Anasayfa = true, Onay = true, Resim = "1.jpg",CategoryId = 2 },
                new Blog(){Baslik = "C# Delagates Hakkında ", Aciklama="Çok uykum var benim", EklenmeTarihi = DateTime.Now.AddDays(-10), Anasayfa = false, Onay = false, Resim = "1.jpg",CategoryId = 2 },
                new Blog(){Baslik = "C# Delagates Hakkında ", Aciklama="Çok uykum var benim", EklenmeTarihi = DateTime.Now.AddDays(-10), Anasayfa = true, Onay = true, Resim = "1.jpg",CategoryId = 3 },
                new Blog(){Baslik = "C# Delagates Hakkında ", Aciklama="Çok uykum var benim", EklenmeTarihi = DateTime.Now.AddDays(-10), Anasayfa = true, Onay = true, Resim = "1.jpg",CategoryId = 3 },
                new Blog(){Baslik = "C# Delagates Hakkında ", Aciklama="Çok uykum var benim", EklenmeTarihi = DateTime.Now.AddDays(-10), Anasayfa = true, Onay = false, Resim = "1.jpg",CategoryId = 3 },
                new Blog(){Baslik = "C# Delagates Hakkında ", Aciklama="Çok uykum var benim", EklenmeTarihi = DateTime.Now.AddDays(-10), Anasayfa = true, Onay = true, Resim = "1.jpg",CategoryId = 1 },
                new Blog(){Baslik = "C# Delagates Hakkında ", Aciklama="Çok uykum var benim", EklenmeTarihi = DateTime.Now.AddDays(-10), Anasayfa = true, Onay = true, Resim = "1.jpg",CategoryId = 4 },
                new Blog(){Baslik = "C# Delagates Hakkında ", Aciklama="Çok uykum var benim", EklenmeTarihi = DateTime.Now.AddDays(-10), Anasayfa = true, Onay = true, Resim = "1.jpg",CategoryId = 4 },
                new Blog(){Baslik = "C# Delagates Hakkında ", Aciklama="Çok uykum var benim", EklenmeTarihi = DateTime.Now.AddDays(-10), Anasayfa = true, Onay = false, Resim = "1.jpg",CategoryId = 4 },

            };

            foreach (var item in bloglar)
            {
                context.Bloglar.Add(item);
            }
            context.SaveChanges();
            List<Admin> adminler = new List<Admin>()
            {
                new Admin(){userName="Admin", password="123456"}
            };
            foreach (var item in adminler)
            {
                context.Adminler.Add(item);
            }
            context.SaveChanges();
            
            base.Seed(context);
        }
    }
}