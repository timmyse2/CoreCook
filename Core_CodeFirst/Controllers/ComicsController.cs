using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core_CodeFirst.Models;

using System.Diagnostics;
using Microsoft.Extensions.Configuration; //for IConfiguration

namespace Core_CodeFirst.Controllers
{
    public class ComicsController : Controller
    {
        private readonly ComicDbContext _context;

        //::try c14
        //private readonly IConfiguration _config;

        public ComicsController(ComicDbContext context)
        {
            _context = context;
        }
        //public ComicsController(ComicDbContext context, IConfiguration config)
        //{
        //    _context = context; //:: for DI
        //    _config = config;   //:: for configuration (study for book c14)
        //}

        //<timmy><><study for session>
        //private string sessionKey = "session_key";

        // GET: Comics
        //public async Task<IActionResult> Index()
        public async Task<IActionResult> Index(int? page, int? makerid)
        {
            //ViewData["config"] = _config["Company:NewName"];
            //ViewData["config"] = _config["ConnectionStrings:BlogDbContext"];

            if (makerid <= -1)
            {
                //:: Reset search condition from cookie or session
                HttpContext.Response.Cookies.Delete("makerid");
                HttpContext.Response.Cookies.Delete("page");

                TempData["td_serverMessage"] = "已清除篩選條件了";
                return RedirectToAction("Index");
            }

            int ipp = 5; // item per page = pageSize
            var ctx0 = _context.Comics
                    .OrderByDescending(x => x.ComicId)
                    .Include(x => x.Maker)
                    .Where(x => x.ComicName != null)
                    ;

            int _makerid = 0;
            if (makerid != null) //:: condition
            {
                ctx0 = _context.Comics
                    .OrderByDescending(x => x.ComicId)
                    .Include(x => x.Maker)                    
                    .Where(x => x.MakerId == makerid)
                    ;
                HttpContext.Response.Cookies.Append("makerid", makerid.ToString());
                HttpContext.Response.Cookies.Append("page_comic", "0"); //reset page id                
                //HttpContext.Response.Cookies.Delete("page");
                ViewData["maker"] = "依創作者";
            }
            else
            {
                var _cookeId = HttpContext.Request.Cookies["makerid"];
                try
                {
                    if (_cookeId != null)                    
                    {
                        _makerid = int.Parse(_cookeId);
                        ctx0 = _context.Comics
                            .OrderByDescending(x => x.ComicId)
                            .Include(c => c.Maker)
                            .Where(c => c.MakerId == _makerid)
                            ;
                        ViewData["maker"] = "依創作者";
                        //HttpContext.Response.Cookies.Append("page", "0"); //reset page id
                        HttpContext.Response.Cookies.Delete("page_comic");
                    }
                }
                catch
                {                    
                }
            }


            //::@timmy: set page
            int _page = 0; //pageNum
            if (page != null)
                _page = (int)page - 1;
            else
            {
                //::get page from cookie
                var _cookepage = HttpContext.Request.Cookies["page_comic"];
                try
                {
                    if (_cookepage == null)
                    {
                        _page = 0;
                    }
                    else
                    {
                        _page = int.Parse(_cookepage);
                    }
                }
                catch
                {
                    _page = 0;
                }
            }

            HttpContext.Response.Cookies.Append("page_comic", _page.ToString());


            //~~~~~~~~~~~
            int totalC 
                //= _context.Comics.Count();            
                = ctx0.Count();

            int totalPages = totalC / ipp;
            if (_page >= totalPages)
                _page = totalPages; //::debug
            int skipc = _page * ipp; //(totalPages- _page) * ipp;
            if (skipc <= 0) skipc = 0;
            //~~~~~~~~~~~


            var ctx = ctx0.Skip(skipc).Take(ipp);

            //var ctx = _context.Comics
            //    .OrderByDescending(x => x.ComicId)
            //    .Include(c => c.Maker)
            //    .Where(c => c.Description != null)
            //    .Skip(skipc).Take(ipp)
            //    ;
            ViewData["page"] = _page;
            ViewData["rsCount"] = totalC;

            //if(HttpContext.Request.Cookies["IsAdmin"] == "YES")
            if(GetMySession("IsAdmin") == "YES")
            {
                ViewData["IsAdmin"] = "YES";
                ViewData["UserAccount"] = HttpContext.Request.Cookies["UserAccount"];
            }

            TempData.Keep();//[timmy][][study]

            return View(await ctx.ToListAsync());

            //var comicDbContext = _context.Comics.Include(c => c.Maker);
            //return View(await comicDbContext.ToListAsync());
        }

        public async Task<IActionResult> Card(int? page, int? makerid)
        {
            
            if (makerid <= -1)
            {
                //:: Reset search condition from cookie or session
                HttpContext.Response.Cookies.Delete("makerid");
                HttpContext.Response.Cookies.Delete("page");

                TempData["td_serverMessage"] = "已清除篩選條件了";
                return RedirectToAction("Index");
            }

            int ipp = 20; // item per page = pageSize
            var ctx0 = _context.Comics
                    .OrderByDescending(x => x.ComicId)
                    .Include(c => c.Maker)
                    .Where(c => c.ComicName != null)
                    ;

            int _makerid = 0;
            if (makerid != null) //:: condition
            {
                ctx0 = _context.Comics
                    .OrderByDescending(x => x.ComicId)
                    .Include(c => c.Maker)
                    .Where(c => c.MakerId == makerid)
                    ;
                HttpContext.Response.Cookies.Append("makerid", makerid.ToString());
                HttpContext.Response.Cookies.Append("page_comic", "0"); //reset page id                
                //HttpContext.Response.Cookies.Delete("page");
                ViewData["maker"] = "依創作者";
            }
            else
            {
                var _cookeId = HttpContext.Request.Cookies["makerid"];
                try
                {
                    if (_cookeId != null)
                    {
                        _makerid = int.Parse(_cookeId);
                        ctx0 = _context.Comics
                            .OrderByDescending(x => x.ComicId)
                            .Include(c => c.Maker)
                            .Where(c => c.MakerId == _makerid)
                            ;
                        ViewData["maker"] = "依創作者";
                        //HttpContext.Response.Cookies.Append("page", "0"); //reset page id
                        HttpContext.Response.Cookies.Delete("page_comic");
                    }
                }
                catch
                {
                }
            }


            //::@timmy: set page
            int _page = 0; //pageNum
            if (page != null)
                _page = (int)page - 1;
            else
            {
                //::get page from cookie
                var _cookepage = HttpContext.Request.Cookies["page_comic"];
                try
                {
                    if (_cookepage == null)
                    {
                        _page = 0;
                    }
                    else
                    {
                        _page = int.Parse(_cookepage);
                    }
                }
                catch
                {
                    _page = 0;
                }
            }

            HttpContext.Response.Cookies.Append("page_comic", _page.ToString());


            //~~~~~~~~~~~
            int totalC
                //= _context.Comics.Count();            
                = ctx0.Count();

            int totalPages = totalC / ipp;
            if (_page >= totalPages)
                _page = totalPages; //::debug
            int skipc = _page * ipp; //(totalPages- _page) * ipp;
            if (skipc <= 0) skipc = 0;
            //~~~~~~~~~~~


            var ctx = ctx0.Skip(skipc).Take(ipp);
            ViewData["page"] = _page;
            ViewData["rsCount"] = totalC;

            //if(HttpContext.Request.Cookies["IsAdmin"] == "YES")
            if (GetMySession("IsAdmin") == "YES")
            {
                ViewData["IsAdmin"] = "YES";
                ViewData["UserAccount"] = HttpContext.Request.Cookies["UserAccount"];
            }

            TempData.Keep();//[timmy][][study]

            return View(await ctx.ToListAsync());
        }

        //::for LINQ studying <Timmy 2023 Dec>
        public async Task<IActionResult> List(int? page)
        {
            //::List Compact
            //::@t: set page
            int _page = 0;
            if (page != null)
                _page = (int)page;
            else
                _page = 0;

            //int ipp = 5; // item per page
            //int totalC = _context.Comics.Count();
            //int totalPages = totalC / ipp;
            //int skipc = (totalPages - _page) * ipp;
            //if (skipc <= 0) skipc = 0;

            //var ctx = _context.Comics
            //    .OrderByDescending(x => x.ComicId)
            //    .Include(c => c.Maker)
            //    .Skip(skipc).Take(ipp)
            //    ;
            //ViewData["page"] = _page;
            //ViewData["rsCount"] = totalC;

            //var ctx = _context.Comics;


            //::use query syntax
            //var ctx = 
            //    from cmc in _context.Comics
            //        select new Comic
            //        {
            //            ComicId = cmc.ComicId,                    
            //            ComicName = cmc.ComicName
            //        };

            //var ctx =
            //    from c in _context.Comics
            //    where c.ComicId >= 1 && c.ComicId <= 20
            //    orderby c.MakerId descending, c.ComicId descending
            //    select c
            //    ;

            //join
            var jctx0 =
                from c in _context.Comics
                join m in _context.Makers
                on c.MakerId equals m.MakerId                
                where c.ComicId > 10
                //orderby c.ComicId descending               
                orderby m.MakerId
                //select new {c,m}
                select c                
                //select new
                //{
                //    c.ComicId,
                //    c.ComicName,
                //    c.Image,
                //    c.Description,
                //    c.MakerId,                    
                //    c.Maker
                //    //m.UserName
                //}
                //select new
                //{
                //    c,
                //    m
                //}
                ;


            //var jctx1 = _context.Comics
            //        .OrderByDescending(x => x.ComicId)
            //        .Include(x => x.Maker)
            //        .Where(x => x.ComicName != null)
            //        ;

            //var jctx2 =
            //    from c in _context.Comics
            //    .Include(x=>x.Maker)
            //    //join m in _context.Makers
            //    //on c.MakerId equals m.MakerId
            //    //where c.ComicId > 10                
            //    orderby c.ComicId descending                 
            //    select c              
            //    ;

            //var jctx =
            //    from c in _context.Comics
            //    join m in _context.Makers
            //    on c.MakerId equals m.MakerId
            //    orderby c.ComicId descending
            //    //select new
            //    //{ 
            //    //    ComicId = c.ComicId,
            //    //    ComicName = c.ComicName,
            //    //    Image = c.Image,
            //    //    Description = c.Description,
            //    //    MakerId = c.MakerId,
            //    //    Maker = c.Maker
            //    //};
            //    select new
            //    {
            //        c.ComicId,
            //        c.ComicName,
            //        c.Image,
            //        c.Description,
            //        c.MakerId,
            //        c.Maker
            //    };


            //::p15-16 <Query Syntax>
            //var ctx = await (from c in _context.Comics
            //                 select new Comic
            //                 {
            //                     ComicId = c.ComicId,
            //                     ComicName = c.ComicName,
            //                     Description = c.Description,
            //                     Image = c.Image,
            //                     Maker = c.Maker
            //                 }).ToListAsync();

            //::p15-16 <Method Syntax>
            //var ctx = await _context.Comics
            //    .Select(c => new Comic
            //    {
            //        ComicId = c.ComicId,
            //        ComicName = c.ComicName,
            //        Description = c.Description,
            //        Image = c.Image,
            //        Maker = c.Maker
            //    }).ToListAsync();

            ////::p15-16 <SQL>
            //var ctx = await _context.Comics
            //    .FromSql($"select * from dbo.Comics")
            //    .ToListAsync();

            //::p15-22
            var ctx = await _context.Comics
                .OrderByDescending (p=>p.MakerId).ThenBy(p2=>p2.ComicName)
                .Select(c => new Comic
                {
                    ComicId = c.ComicId,
                    ComicName = c.ComicName,
                    Description = c.Description,
                    Image = c.Image,
                    MakerId = c.MakerId,
                    Maker = c.Maker
                }).ToListAsync();


            //var result = await jctx0.ToListAsync();           

            //List<Comic> lc = result
            //    .Select(x => new Comic
            //    {
            //        ComicId = x.ComicId,
            //        ComicName = x.ComicName,
            //        Image = x.Image,
            //        Description = x.Description,
            //        MakerId = x.MakerId,
            //        Maker = x.Maker
            //    }).ToList();

            //::use method
            //var ctx = _context.Comics.
            //        Select(x => new Comic
            //        {
            //            ComicId = x.ComicId,
            //            Description = x.Description,
            //            ComicName = x.ComicName
            //        }
            //        );

            //::use sql
            //var ctx = _context.Comics.FromSql("select * from dbo.Comics where ComicId > 10");            
            //var ctx = _context.Comics.FromSql(
            //    $"SELECT TOP 10 * FROM dbo.Comics"
            //    +" WHERE MakerId > 3"
            //    +" ORDER BY ComicId DESC");

            //return Content(result.ToString());
            //return View(Content(result));

            //return Json(result);            
            //return View(lc);
            //return View(result);



            return View(ctx);
        }

        /*
        [HttpPost]
        public async Task<IActionResult> List()
        {
            var ctx =
                from c in _context.Comics
                where c.ComicId >= 15 && c.ComicId <= 20
                orderby c.MakerId descending, c.ComicId descending
                select c
                ;

            //join
            var jctx =
                from c in _context.Comics
                join m in _context.Makers
                on c.MakerId equals m.MakerId
                select new
                {
                    cname = c.ComicName,
                    mname = m.UserName
                }
                ;

            var result = await ctx.ToListAsync();

            //::use method
            //var ctx = _context.Comics.
            //        Select(x => new Comic
            //        {
            //            ComicId = x.ComicId,
            //            Description = x.Description,
            //            ComicName = x.ComicName
            //        }
            //        );

            //::use sql
            //var ctx = _context.Comics.FromSql("select * from dbo.Comics where ComicId > 10");            
            //var ctx = _context.Comics.FromSql(
            //    $"SELECT TOP 10 * FROM dbo.Comics"
            //    +" WHERE MakerId > 3"
            //    +" ORDER BY ComicId DESC");
            return View(result);
        }
        */
        // GET: Comics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //::session test
            //byte[] bv = null;
            //HttpContext.Session.TryGetValue(sessionKey, out vb2);
            //HttpContext.Session.TryGetValue("sk_temp", out bv);
            //bv = System.Text.Encoding.Default.GetBytes("9527");
             //Debug.WriteLine(" " + GetMySession("sk_temp"));

            if (id == null)
            {
                return NotFound();
            }

            var comic = await _context.Comics
                //.Include(c => c.Maker)
                .Include(x => x.Maker)
                //.FirstOrDefaultAsync(m => m.ComicId == id);
                .FirstOrDefaultAsync(x => x.ComicId == id);
            if (comic == null)
            {
                return NotFound();
            }

            //::
            if(GetMySession("IsAdmin") == "YES")
            //if (HttpContext.Request.Cookies["IsAdmin"] == "YES")
            {
                ViewData["IsAdmin"] = "YES";
            }
            return View(comic);
        }

        // GET: Comics/Create
        public IActionResult Create()
        {
            //::login check
            if (GetMySession("IsAdmin") != "YES")            
            {
                TempData["td_serverMessage"] = "權限不足";
                return RedirectToAction("Index");
            }
            //ViewData["MakerId"] = new SelectList(_context.Makers, "MakerId", "MakerId");
            ViewData["MakerId"] = new SelectList(_context.Makers, "MakerId", "UserName");

            return View();
        }

        // POST: Comics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComicId,ComicName,Image,Description,MakerId")] Comic comic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comic);
                await _context.SaveChangesAsync();
                TempData["td_serverMessage"] = "已建立資料"; //::
                return RedirectToAction(nameof(Index));
            }
            ViewData["MakerId"] = new SelectList(_context.Makers, "MakerId", "MakerId", comic.MakerId);
            return View(comic);
        }

        // GET: Comics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //::login check
            if(GetMySession("IsAdmin") != "YES")
            //if(HttpContext.Request.Cookies["IsAdmin"] != "YES")
            {
                TempData["td_serverMessage"] = "權限不足";
                return RedirectToAction("Index");
            }

            var comic = await _context.Comics.FindAsync(id);
            if (comic == null)
            {
                return NotFound();
            }
            //ViewData["MakerId"] = new SelectList(_context.Makers, "MakerId", "MakerId", comic.MakerId);
            ViewData["MakerId"] = new SelectList(_context.Makers, "MakerId", "UserName", comic.MakerId);
            return View(comic);
        }

        // POST: Comics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComicId,ComicName,Image,Description,MakerId")] Comic comic)
        {
            if (id != comic.ComicId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comic);
                    TempData["td_serverMessage"] = "已更新資料"; //::
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComicExists(comic.ComicId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MakerId"] = new SelectList(_context.Makers, "MakerId", "MakerId", comic.MakerId);
            return View(comic);
        }

        // GET: Comics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //::login check
            //if (HttpContext.Request.Cookies["IsAdmin"] != "YES")
            if (GetMySession("IsAdmin") != "YES")
            {
                TempData["td_serverMessage"] = "權限不足"; //::
                return RedirectToAction("Index");        
                //return Content("You have no power here!");
            }

            var comic = await _context.Comics
                .Include(c => c.Maker)
                .FirstOrDefaultAsync(m => m.ComicId == id);
            if (comic == null)
            {
                return NotFound();
            }
            
            return View(comic);
        }

        // POST: Comics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comic = await _context.Comics.FindAsync(id);
            _context.Comics.Remove(comic);
            await _context.SaveChangesAsync();
            TempData["td_serverMessage"] = "已刪除"; //::
            return RedirectToAction(nameof(Index));
        }

        private bool ComicExists(int id)
        {
            return _context.Comics.Any(e => e.ComicId == id);
        }


        //::add - login dummy
        //public IActionResult Login(string IsAdmin)
        //{
        //    if (IsAdmin == "yes")
        //    {
        //        ViewData["IsAdmin"] = "YES";
        //        HttpContext.Response.Cookies.Append("IsAdmin", "YES");
        //        return RedirectToAction("Index");
        //    }
        //    return Content("Warning! Not match");
        //}

        public IActionResult Login()
        {
            //::remember last user
            ViewData["UserAccount"] = HttpContext.Request.Cookies["UserAccount"];

            return View();
        }
            
        //public IActionResult LoginO(string account, string pwd)
        //{
        //    //account = "jimmy@hp.co.jp";
        //    //pwd = "1999";
        //    if (account == null || account == "")
        //    {
        //        return Content("Error!");
        //    }
        //    //len check or ...
        //    if (pwd == null || pwd == "")
        //    {
        //        return Content("Error!");
        //    }
        //    //::pin

        //    var ctx = _context.Admins
        //        .Where(a => a.Pwd == pwd && a.Account == account);            

        //    if (ctx == null)
        //    {
        //        System.Diagnostics.Debug.WriteLine("not found");
        //        return Content("Warning! Not match");
        //    }
        //    else
        //    {
        //        int count = ctx.Count();

        //        if (count == 1) //only one - pass
        //        {
        //            ViewData["IsAdmin"] = "YES";
        //            HttpContext.Response.Cookies.Append("IsAdmin", "YES");
        //            HttpContext.Response.Cookies.Append("UserAccount", account);
        //            return RedirectToAction("Index");
        //        }
        //        else if (count > 1) //fail case - 
        //        {
        //            System.Diagnostics.Debug.WriteLine("found " + ctx.Count());
        //            return Content("Warning! found " + ctx.Count() + " account(s)");
        //        }
        //        else //fail case
        //        {
        //            return Content("Error! No matched account");
        //        }                
        //    }


        //    //var admin = _context.Admins
        //    //    //.FirstOrDefaultAsync(m => m.Account == account);
        //    //    .FirstAsync(m => m.Account == account && m.Pwd == pwd);
        //    //if (admin == null)
        //    //{
        //    //    //not found
        //    //    System.Diagnostics.Debug.WriteLine("not found");
        //    //}
        //    //else
        //    //{
        //    //    //System.Diagnostics.Debug.WriteLine("found " + admin.Id);
        //    //}
        //}

        [HttpPost]
        public IActionResult LoginPost(string account, string pwd, string pin)
        {
            if (account == null || account == "")
            {
                //return Content("{res:Error}");
                return Json(new { result = "fail", detail = "lost account" });
            }
            //len check or ...
            if (pwd == null || pwd == "")
            {
                return Json(new { result = "fail", detail = "lost pwd" });
            }
            //::pin
            if (pin == null || pin == "")
            {
                return Json(new { result = "fail", detail = "lost pin" });
            }
            else
            {
                if (pin != "94:<") //9527
                {
                    //return Content(Json("error"));
                    return Json(new { result = "fail", detail = "pin mismatch" });
                }            
            }

            var ctx = _context.Admins
                .Where(a => a.Pwd == pwd && a.Account == account)
                //.FirstOrDefault()
                ;


            //::check failCount <= 3

            if (ctx == null)
            {
                System.Diagnostics.Debug.WriteLine("not found");
                //return Content("Warning! Not match");
                return Json(new { result = "error", detail = "not match" });
            }
            else
            {
                int count = ctx.Count();

                if (count == 1) //only one - pass
                {
                    ViewData["IsAdmin"] = "YES";                    
                    HttpContext.Response.Cookies.Append("UserAccount", account);
                    //::<Timmy><Dec 18,2023><PS. use session is better!>
                    //HttpContext.Response.Cookies.Append("IsAdmin", "YES");                    
                    SetMySession("IsAdmin", "YES");
                    //<><><>
                    //return RedirectToAction("Index");
                    return Json(new { result = "PASS", detail = "matched" });
                }
                else if (count > 1) //fail case - 
                {
                    System.Diagnostics.Debug.WriteLine("found " + ctx.Count());
                    //return Content("Warning! found " + ctx.Count() + " account(s)");
                    return Json(new { result = "fail", detail = "overload" });
                }
                else //fail case
                {
                    //return Content("Error! No matched account");
                    return Json(new { result = "fail", detail = "no matched data" });
                }
            }
        }
        //::add - login dummy
        public IActionResult Logout()
        {
            try
            {
                ViewData["IsAdmin"] = null;
                //HttpContext.Response.Cookies.Delete("IsAdmin");                
                HttpContext.Session.Remove("IsAdmin");

                //HttpContext.Response.Cookies.Delete("UserAccount");
                TempData["td_serverMessage"] = "已登出"; //::
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Warning! Not match");
            }
        }

        //::<Timmy><Dec 18,2023><my api for session>
        public bool SetMySession(string key, string val)
        {
            try
            {
                //::string to byte[]
                byte[] bv = System.Text.Encoding.Default.GetBytes(val); ;
                HttpContext.Session.Set(key, bv);
            }
            catch
            {
                return false;
            }
            return true;
        }

        //::<Timmy><Dec 18,2023><my api for session>
        public string GetMySession(string key)
        {
            string _str = null;
            try
            {
                byte[] bv = null;
                //Microsoft.AspNetCore.Http.HttpContext hp = new Microsoft.AspNetCore.Http.HttpContext();s
                HttpContext.Session.TryGetValue(key, out bv);
                //::byte[] to string
                _str = System.Text.Encoding.Default.GetString(bv);
            }
            catch
            {
            }            
            return _str;
        }
    }

    
}
