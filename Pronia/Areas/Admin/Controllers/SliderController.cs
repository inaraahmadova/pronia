using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DataAccesLayer;
using Pronia.Models;
using Pronia.ViewModels.Sliders;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    public class SliderController(ProniaContext _context) : Controller
    {
        public async Task <IActionResult> Index()
        {
            var data = await _context.Sliders
                .Where(x=>!x.IsDeleted)
                .Select(s => new GetSliderVM
            {
                Discount = s.Discount,
                Id = s.Id,
                ImageUrl = s.ImageUrl,
                Subtitle = s.Subtitle,
                Title = s.Title,
            }
            ).ToListAsync();
            return View(data ?? new List<GetSliderVM>());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSliderVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            Slider slider = new Slider
            {
                Discount = vm.Discount,
                CreatedTime = DateTime.Now,
            ImageUrl = vm.ImageUrl,
            IsDeleted=false,
            Subtitle = vm.Subtitle,
            Title = vm.Title,
            };
            await _context.Sliders.AddAsync(slider);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id==null|| id<1) BadRequest();
            
            Slider slider=await _context.Sliders.FirstOrDefaultAsync(s=>s.Id==id);

            if (slider==null) return NotFound();

            return View();
        }

        public async Task<IActionResult> Update(int? id, Slider slider)
        {
            if (id == null || id < 1) BadRequest();

            Slider existed=await _context.Sliders.FirstOrDefaultAsync(s=> s.Id==id);    

            if (existed==null) return NotFound();

            existed.Title = slider.Title;
            existed.Subtitle = slider.Subtitle;
            existed.Discount = slider.Discount;
            existed.ImageUrl = slider.ImageUrl;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

            //return View(Index);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            var item = await _context.Sliders.FindAsync(id);
            if (item == null) return NotFound();
            _context.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
           
        }
    }
}
