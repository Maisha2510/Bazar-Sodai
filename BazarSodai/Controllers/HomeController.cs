using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BazarSodai.Models;
using System.Web.Security;
using System.Data.Entity.Validation;
using System.Xml;

namespace BazarSodai.Controllers
{
    public class HomeController : Controller
    {
        ShopDatabaseEntities db = new ShopDatabaseEntities();


        //******************************************  User part  *********************************************
        public ActionResult Index()
        {
            dynamic newModel = new ExpandoObject();
            newModel.categories = db.Categories.ToList();
            newModel.subcategories = db.SubCategories.ToList();
            return View(newModel);
        }


        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
      
        public ActionResult OrderHistory()
        {
            var sqlquery = "select * from [order] where UserEmail= '" + User.Identity.Name + "'";

            List<Order> orders = db.Orders.SqlQuery(sqlquery).ToList();

            return View(orders);


        }


        [HttpPost]
        public ActionResult Checkout(Order ordt)
        {
            if (ModelState.IsValid)
            {
                Order rt = new Order();

                rt.DeliveryAddress = ordt.DeliveryAddress;
                rt.TotalPrice = ordt.TotalPrice;
                rt.UserEmail = User.Identity.Name;

                string DateTime = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                Console.WriteLine(DateTime);
                rt.OrderDate = DateTime;

                db.Orders.Add(rt);
                db.SaveChanges();

                db.Carts.RemoveRange(db.Carts.Where(c => c.UsersEmail == User.Identity.Name));
                db.SaveChanges();

                return RedirectToAction("Index");
            }
         
            return View();

        }

        [Authorize]
        public ActionResult Checkout()
        {

            var sqlquery = "select * from cart where UsersEmail= '" + User.Identity.Name + "'";

            List<Cart> carts = db.Carts.SqlQuery(sqlquery).ToList();

            return View(carts);

        }
        public ActionResult Cart()
        {

            var sqlquery = "select * from cart where UsersEmail= '" + User.Identity.Name + "'";

            List<Cart> carts = db.Carts.SqlQuery(sqlquery).ToList();

            return View(carts);

        }


        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "UsersEmail, UsersPassword")] User newUser)
        {
            List<User> userAccounts = db.Users.Where(temp => temp.UsersEmail == newUser.UsersEmail &&
            temp.UsersPassword == newUser.UsersPassword).ToList();
            while (userAccounts.Count > 0)
            {
                FormsAuthentication.SetAuthCookie(newUser.UsersEmail, false);

                return RedirectToAction("Index", "Home");

            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount([Bind(Include = "UsersEmail, UsersPhone, UsersPassword")] User newUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(newUser);
                db.SaveChanges();
                Response.Redirect("https://localhost:44375/Home/Login");
                return View();
            }
            return View();
        }

        public ActionResult Products(int? id)
        {
            dynamic newModel = new ExpandoObject();
            var sqlquery = "select * from products where SubCategoryID=" + id;
            newModel.catgegoryWiseProduct = db.Products.SqlQuery(sqlquery).ToList();



            return View(newModel);
        }


        public ActionResult Subcategory(int? id)
        {
            dynamic newModel = new ExpandoObject();
            var sqlquery = "select * from SubCategory where CategoryID=" + id;
            newModel.subcategoryResult = db.SubCategories.SqlQuery(sqlquery).ToList();

            return View(newModel);
        }

        public ActionResult ProductDetails(int? id)
        {
            dynamic newModel = new ExpandoObject();
            var sqlquery = "select * from products where ProductsID=" + id;
            newModel.specificProduct = db.Products.SqlQuery(sqlquery).ToList();

            var sqlquery1 = "select * from products";
            newModel.allProducts = db.Products.SqlQuery(sqlquery1).ToList();

            var sqlquery2 = "select * from review";
            newModel.specificReview = db.Reviews.SqlQuery(sqlquery2).ToList();

            return View(newModel);

        }

        [HttpPost]
        public ActionResult ProductDetails(Cart newcart)
        {
            List<Cart> list = db.Carts.ToList();
            ViewBag.catlist = new SelectList(list, "CartID", "UserEmail");

            if (ModelState.IsValid)
            {
                Cart cart = new Cart();

                cart.ProductsID = Convert.ToInt16(newcart.ProductsID);
                cart.ProductName = Convert.ToString(newcart.ProductName);
                cart.ProductsPrice = Convert.ToInt16(newcart.ProductsPrice);
                cart.ProductsImage = newcart.ProductsImage;
                cart.Quantity = Convert.ToInt16(newcart.Quantity);
                cart.UsersEmail = User.Identity.Name;

                db.Carts.Add(cart);

                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }

                Response.Redirect("https://localhost:44375/Home/Index");
            }
            return View();
        }


        //[HttpPost, ActionName("Review")]
    
        //public ActionResult ProductDetails([Bind(Include = "ProductID, UserName, UserEmail, Comment")] Review newReview)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Reviews.Add(newReview);
        //        db.SaveChanges();
        //        return View();
        //    }
        //    return View();
        //}
    
        //******************************************  Admin part  *********************************************
        public ActionResult Authloginbasic()
        {
            return View();

        }
        public ActionResult AddProduct()
        {
            dynamic newModel = new ExpandoObject();
            var sqlquery = "select * from Category";
            newModel.catlist = db.Categories.SqlQuery(sqlquery).ToList();
            var sqlquery1 = "select * from SubCategory";
            newModel.Subcatlist = db.SubCategories.SqlQuery(sqlquery1).ToList();
            return View(newModel);

        }
        [HttpGet]
        public ActionResult ViewProduct()
        {
            dynamic newModel = new ExpandoObject();
            var sqlquery = "select * from products ";
            newModel.specificProduct = db.Products.SqlQuery(sqlquery).ToList();


            return View(newModel);

        }

        [HttpPost]
        public ActionResult AddProduct(Product newpr)
        {
            if (ModelState.IsValid)
            {
                Product prt = new Product();


                prt.ProducsName = newpr.ProducsName;
                prt.ProductsPrice = newpr.ProductsPrice;
                prt.ProductsStock = newpr.ProductsStock;
                prt.ProductsImage = newpr.ProductsImage.ToString();
                prt.SubCategoryID = newpr.SubCategoryID;
                prt.CategoryID = newpr.CategoryID;
                prt.ProductsWeight = newpr.ProductsWeight;





                db.Products.Add(prt);
                db.SaveChanges();
                return View();
            }

            return View();
        }

        public ActionResult ViewCategory()
        {
            CategoryModel category = new CategoryModel();
            category.cats = new List<Category>();

            var data = db.Categories.ToList();
            foreach (var item in data)
            {
                category.cats.Add(new Category
                {
                    CategoryID = item.CategoryID,
                   CategoryName = item.CategoryName,
                   CategoryThumb=item.CategoryThumb

                });
            }
            return View(category);

        }
        

        public ActionResult AddCategory()
        {

            List<Category> list = db.Categories.ToList();
            ViewBag.catlist =new SelectList(list,"CategoryID","CategoryName");

            return View(ViewBag.catlist);
        }

        [HttpPost]
        public ActionResult AddCategory(Category newcat)
        {
            List<Category> list = db.Categories.ToList();
            ViewBag.catlist = new SelectList(list, "CategoryID", "CategoryName");

            if (ModelState.IsValid)
            {
                Category pr = new Category();


                pr.CategoryName = newcat.CategoryName;
                pr.CategoryThumb = newcat.CategoryThumb.ToString();
               
                    

                    var folder = Server.MapPath("~/assets/img/");

                db.Categories.Add(pr);
                db.SaveChanges();
                return View();
            }
               
          
            return View();
    }

        public ActionResult AddSubCategory()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddSubCategory(SubCategory newsubcat )
        {
            if (ModelState.IsValid)
            {
                SubCategory sc = new SubCategory();


                sc.SubCategoryName = newsubcat.SubCategoryName;
                sc.SubCategoryImage = newsubcat.SubCategoryImage;
                sc.CategoryID = newsubcat.CategoryID;

                db.SubCategories.Add(sc);
                db.SaveChanges();
                return View();
            }

            return View();
        }
        public ActionResult ViewSubCategory()
        {

            SubCategoryModel scategory = new SubCategoryModel();
            scategory.scats = new List<SubCategory>();

            var data = db.SubCategories.ToList();
            foreach (var item in data)
            {
                scategory.scats.Add(new SubCategory
                {
                    CategoryID = item.CategoryID,
                    SubCategoryID = item.SubCategoryID,
                    SubCategoryName = item.SubCategoryName,
                    SubCategoryImage = item.SubCategoryImage,
                    

                });
            }
            return View(scategory);

        
        }


        static int pid;
        [HttpGet]
         public ActionResult remove(int id)
           {
            pid = id;
              var obj = db.Products.Find(id);
            return View(obj);

           }
           [HttpPost]
           public ActionResult remove(Product prd)
           {
           
            var obj = db.Products.Where(temp => temp.ProductsID == pid).FirstOrDefault();
            db.Products.Remove(obj);
            db.SaveChanges();
            return RedirectToAction("ViewProduct");
           
           

        }

           

        [HttpPost]
        public ActionResult Authloginbasic([Bind(Include = "UsersEmail, UsersPassword")] User newUser)
        {
            List<User> userAccounts = db.Users.Where(temp => temp.UsersEmail == newUser.UsersEmail &&
            temp.UsersPassword == newUser.UsersPassword).ToList();
            while (userAccounts.Count > 0)
            {

                return RedirectToAction("AddCategory", "Home");

                // Response.Redirect("https://localhost:44375/Home/AddProduct");

            }
            ViewBag.Message = "Username or password wrong";
            return View();
        }

        [HttpGet]
        public ActionResult Authregisterbasic()
        {

            return View();

        }
        [HttpPost]
        public ActionResult Authregisterbasic([Bind(Include = "UsersEmail, UsersPhone, UsersPassword")] User newUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(newUser);
                db.SaveChanges();
                Response.Redirect("https://localhost:44375/Home/Authloginbasic");
                return View();
            }
            return View();
        }

    }
}