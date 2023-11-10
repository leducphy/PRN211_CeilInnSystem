using CeilInnHotelSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.ComponentModel;

namespace CeilInnHotelSystem.Pages.RoomPage
{
    public class ImportRoomModel : PageModel
    {
        private readonly CeilInnHotelDbContext _context;

        public ImportRoomModel(CeilInnHotelDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("file", "Please select a file.");
                return Page();
            }

            // Step 3: Parse the Excel file and create a list of objects
            var listRoom = new List<Room>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.Commercial;
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++) // Assuming the first row contains headers
                    {
                        var obj = new Room
                        {
                            RoomType = worksheet.Cells[row, 1].Value?.ToString(),
                            BedType = worksheet.Cells[row, 2].Value?.ToString(),
                            Rate = float.Parse(worksheet.Cells[row, 2].Value?.ToString()),
                        };

                        listRoom.Add(obj);
                    }
                }
            }

            await Import(listRoom);
            return RedirectToPage("Room");
        }

        public async Task Import(List<Room> listDept)
        {
            foreach (var item in listDept)
            {
                var dept = await _context.Rooms.FirstOrDefaultAsync(i => i.RoomType == item.RoomType);
                if (dept == null)
                {
                    var newDept = new Room()
                    {
                        Id = Guid.NewGuid(),
                        RoomType = item.RoomType,
                        BedType = item.BedType,
                        RoomStatus = true,
                        Rate = item.Rate,
                    };
                    await _context.Rooms.AddAsync(newDept);
                    await _context.SaveChangesAsync();
                }

            }
        }
    }
}
