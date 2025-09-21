using Microsoft.AspNetCore.Mvc;
using ShopTARge24.ApplicationServices.Services;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using ShopTARge24.Models.Kindergartens;

namespace ShopTARge24.Controllers
{
    public class KindergartenController : Controller
    {
        private readonly ShopTARge24Context _context;
        private readonly IKindergartenService _kindergartenService;

        public KindergartenController(
            ShopTARge24Context context,
            IKindergartenService kindergartenService
        )
        {
            _context = context;
            _kindergartenService = kindergartenService;
        }

        public IActionResult Index()
        {
            var result = _context.Kindergartens
                .Select(x => new KindergartenIndexViewModel
                {
                    Id = x.Id,
                    GroupName = x.GroupName,
                    ChildrenCount = x.ChildrenCount,
                    KindergartenName = x.KindergartenName,
                    TeacherName = x.TeacherName,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).ToList();

            return View(result);
        }

        //create
        [HttpGet]
        public IActionResult Create()
        {
            var vm = new KindergartenCreateUpdateViewModel();
            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(KindergartenCreateUpdateViewModel vm)
        {
            var dto = new KindergartenDto
            {
                Id = vm.Id ?? 0,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergartenName = vm.KindergartenName,
                TeacherName = vm.TeacherName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _kindergartenService.Create(dto);

            return RedirectToAction(nameof(Index));
        }

        //update
        public async Task<IActionResult> Update(int id)
        {
            var kg = await _kindergartenService.DetailAsync(id);

            if (kg == null) return NotFound();

            var vm = new KindergartenCreateUpdateViewModel
            {
                Id = kg.Id,
                GroupName = kg.GroupName,
                ChildrenCount = kg.ChildrenCount,
                KindergartenName = kg.KindergartenName,
                TeacherName = kg.TeacherName,
                CreatedAt = kg.CreatedAt,
                UpdatedAt = kg.UpdatedAt
            };

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(KindergartenCreateUpdateViewModel vm)
        {
            var dto = new KindergartenDto
            {
                Id = vm.Id ?? 0,
                GroupName = vm.GroupName,
                ChildrenCount = vm.ChildrenCount,
                KindergartenName = vm.KindergartenName,
                TeacherName = vm.TeacherName,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = DateTime.Now
            };

            await _kindergartenService.Update(dto);

            return RedirectToAction(nameof(Index));
        }

        //details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var kg = await _kindergartenService.DetailAsync(id);

            if (kg == null) return NotFound();

            var vm = new KindergartenDetailsViewModel
            {
                Id = kg.Id,
                GroupName = kg.GroupName,
                ChildrenCount = kg.ChildrenCount,
                KindergartenName = kg.KindergartenName,
                TeacherName = kg.TeacherName,
                CreatedAt = kg.CreatedAt,
                UpdatedAt = kg.UpdatedAt
            };

            return View(vm);
        }

        //delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var kg = await _kindergartenService.DetailAsync(id);

            if (kg == null)
            {
                return NotFound();
            }

            var vm = new KindergartenDeleteViewModel
            {
                Id = kg.Id,
                GroupName = kg.GroupName,
                ChildrenCount = kg.ChildrenCount,
                KindergartenName = kg.KindergartenName,
                TeacherName = kg.TeacherName,
                CreatedAt = kg.CreatedAt,
                UpdatedAt = kg.UpdatedAt
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            bool deleted = await _kindergartenService.Delete(id);

            if (!deleted)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
