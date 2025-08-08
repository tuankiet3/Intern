using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Register.Data;
using Register.Models;

namespace Register.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly RegisterContext _context;

    public HomeController(ILogger<HomeController> logger, RegisterContext context)
    {
        _context = context;
        _logger = logger;
    }

    

    [HttpGet]
    public IActionResult Index()
    {
        loadViewBag();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(
        [Bind("StudentName, Email, Phone, DateOfBirth, Address, CCCD, Prioritize, Gender, ProvinceId, HighSchoolId, AreaId, MajorId, AspirationId")]
        Student student)
    {
        ModelState.Clear();
        bool isValid = true;
        var studentExists = await _context.Students
            .AnyAsync(s => s.Email == student.Email || s.Phone == student.Phone || s.CCCD == student.CCCD);
        var stringFields = new Dictionary<string, string?>
            {
                { "StudentName", student.StudentName },
                { "Email", student.Email },
                { "Phone", student.Phone },
                { "Address", student.Address },
                { "CCCD", student.CCCD }
            };

        foreach (var field in stringFields)
        {
            if (string.IsNullOrWhiteSpace(field.Value))
            {
                ModelState.AddModelError(field.Key, $"{GetFieldName(field.Key)} không được để trống.");
                isValid = false;
            }
            if (studentExists)
            {
                ModelState.AddModelError(field.Key, $"{ GetFieldName(field.Key)} đã tồn tại");
                isValid = false;
            }
        }

        if (student.DateOfBirth == null)
        {
            ModelState.AddModelError("DateOfBirth", "Ngày sinh không được để trống.");
            isValid = false;
        }

        if (student.DateOfBirth != null && student.DateOfBirth > DateTime.Now)
        {
            ModelState.AddModelError("DateOfBirth", "Ngày sinh không được lớn hơn ngày hiện tại.");
            isValid = false;
        }

        var intFields = new Dictionary<string, int?>
            {
                { "ProvinceId", student.ProvinceId },
                { "HighSchoolId", student.HighSchoolId },
                { "AreaId", student.AreaId },
                { "MajorId", student.MajorId },
            };

        foreach (var field in intFields)
        {
            if (field.Value == null)
            {
                ModelState.AddModelError(field.Key, $"{GetFieldName(field.Key)} không được để trống.");
                isValid = false;
            }
        }
        if (isValid)
            {
            try
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đăng ký thông tin thành công!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                _logger.LogError(ex, "Error saving student data");
            }
        }

        if (student.ProvinceId != null)
        {
            ViewBag.highSchools = _context.HighSchools
                .Where(h => h.ProvinceId == student.ProvinceId)
                .OrderBy(h => h.HighSchoolName)
                .ToList();
        }
        loadViewBag();

        return View(student);
    }

    private void loadViewBag()
    {
        ViewBag.provinces = _context.Provinces.OrderBy(p => p.ProvinceName).ToList();
        ViewBag.majors = _context.Majors.OrderBy(m => m.MajorName).ToList();
        ViewBag.areas = _context.Areas.OrderBy(a => a.AreaName).ToList();
        ViewBag.aspirations = _context.Aspirations.OrderBy(a => a.AspirationName).ToList();
    }

    private string GetFieldName(string key)
    {
        return key switch
        {
            "StudentName" => "Tên học sinh",
            "Email" => "Email",
            "Phone" => "Số điện thoại",
            "Address" => "Địa chỉ",
            "CCCD" => "CCCD",
            "ProvinceId" => "Tỉnh/Thành phố",
            "HighSchoolId" => "Trường THPT",
            "AreaId" => "Khu vực",
            "MajorId" => "Ngành học",
            "AspirationId" => "Nguyện vọng",
            _ => key
        };
    }

    [HttpGet]
    public IActionResult GetHighSchoolsByProvince(int provinceId)
    {
        var highSchools = _context.HighSchools
            .Where(h => h.ProvinceId == provinceId)
            .OrderBy(h => h.HighSchoolName)
            .Select(h => new { id = h.HighSchoolId, school = h.HighSchoolName })
            .ToList();

        return Json(highSchools);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
