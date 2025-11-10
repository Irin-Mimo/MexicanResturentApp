using MexicanResturent.Data;
using MexicanResturent.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MexicanResturent.Controllers
{
    public class ProductController : Controller
    {
        private Repository<Product> products;
        private Repository<Ingrediant> ingrediants;
        private Repository<Category> categories;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            products =new Repository<Product>(context);
            ingrediants =new Repository<Ingrediant>(context);
            categories =new Repository<Category>(context);
            this.webHostEnvironment=webHostEnvironment;
        }

        // Search Filter
        public async Task<IActionResult> Index(string search)
        {
            var productList = await products.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                productList = productList
                    .Where(p => p.Name.ToLower().Contains(search) ||
                                p.Description.ToLower().Contains(search))
                    .ToList();
            }

            ViewBag.Search = search; 

            return View(productList);
        }
        //public async Task<IActionResult> Index()
        //{
        //    return View(await products.GetAllAsync());
        //}

        //Create
        public async Task<IActionResult> AddEdit(int id)
        {
            ViewBag.Ingrediants =await ingrediants.GetAllAsync();
            ViewBag.Categories =await categories.GetAllAsync();

            if (id == 0)
            {
                ViewBag.Operation = "Add";
                return View(new Product());
            }
            else
            {
                Product product = await products.GetByIdAsync(id, new QueryOptions<Product>
                {
                    Includes =  "ProductIngrediants.Ingrediant, Category", Where = p => p.ProductId == id
                });
                ViewBag.Operation = "Edit";              
                return View(product);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(Product product, int[] ingrediantIds,int catId)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Ingrediants=await ingrediants.GetAllAsync();
                ViewBag.Categories=await categories.GetAllAsync();

                if (product.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    string uniqeFileName = Guid.NewGuid().ToString()+ "_" + product.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqeFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(fileStream);
                    }
                    product.ImageUrl = uniqeFileName;
                }
                if (product.ProductId ==0)
                {
                  
                    product.CategoryId = catId;

                    //Add
                    foreach(int id in ingrediantIds)
                    {
                        product.ProductIngrediants?.Add(new ProductIngrediant {IngrediantId = id, ProductId = product.ProductId});
                    }

                    await products.AddAsync(product);
                    return RedirectToAction("Index","Product");
                }
                else
                {
                    var existingProduct = await products.GetByIdAsync(product.ProductId, new QueryOptions<Product>
                    {
                        Includes = "ProductIngrediants"
                    });
                    if (existingProduct == null)
                    {
                        ModelState.AddModelError("", "Product not found.");
                        ViewBag.Ingrediants = await ingrediants.GetAllAsync();
                        ViewBag.Categories = await categories.GetAllAsync();

                        return View(product);
                    }

                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.Stock = product.Stock;
                    existingProduct.CategoryId = catId;

                    //update

                    existingProduct.ProductIngrediants?.Clear();
                    foreach (int id in ingrediantIds)
                    {
                        existingProduct.ProductIngrediants?.Add(new ProductIngrediant { IngrediantId = id, ProductId = product.ProductId });
                    }
                    try
                    {
                        await products.UpdateAsync(existingProduct);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Unable to update product:Error{ex.GetBaseException().Message}");
                        ViewBag.Ingrediants = await ingrediants.GetAllAsync();
                        ViewBag.Categories = await categories.GetAllAsync();
                        return View(product);
                    }
                   
                }
            }
            return RedirectToAction("Index", "Product");
            //else
            //{
            //    return View(product);
            //}
        }
        //Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await products.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch 
            {
                ModelState.AddModelError("", "Product Not Found.");
                return RedirectToAction("Index");
            }
        }
            
    }
}
