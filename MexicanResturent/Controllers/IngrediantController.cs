using MexicanResturent.Data;
using MexicanResturent.Models;
using Microsoft.AspNetCore.Mvc;

namespace MexicanResturent.Controllers
{
    public class IngrediantController : Controller
    {
        private Repository<Ingrediant> ingrediants;
        public IngrediantController(ApplicationDbContext context)
        {
            ingrediants =new Repository<Ingrediant>(context);
        }
        public async Task<IActionResult> Index()
        {
            return View(await ingrediants.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await ingrediants.GetByIdAsync(id, new QueryOptions<Ingrediant>() { Includes="ProductIngrediants.Product" }));
        }

        //Ingrediant/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IngrediantId,Name")]Ingrediant ingrediant)
        {
            if (ModelState.IsValid)
            {
                await ingrediants.AddAsync(ingrediant);
                return RedirectToAction("Index");
            }
            return View(ingrediant);
        }

        //Delete

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await ingrediants.GetByIdAsync(id, new QueryOptions<Ingrediant>{ Includes="ProductIngrediants.Product"}));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Ingrediant ingrediant)
        {
            await ingrediants.DeleteAsync(ingrediant.IngrediantId);
            return RedirectToAction("Index");
        }

        //Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await ingrediants.GetByIdAsync(id, new QueryOptions<Ingrediant> { Includes = "ProductIngrediants.Product" }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Ingrediant ingrediant)
        {
            if (!ModelState.IsValid)
            {
                await ingrediants.UpdateAsync(ingrediant);
                return RedirectToAction("Index");
            }
            return View(ingrediant);
        }


    }
}
