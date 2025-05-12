using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QLKS.Data;

public class HoaDonRepository : IHoaDonRepository
{
    private readonly DataQlks112Nhom3Context _context;

    public HoaDonRepository(DataQlks112Nhom3Context context)
    {
        _context = context;
    }

    // ...existing methods...

    public async Task<decimal> GetRevenue(DateTime startDate, DateTime endDate)
    {
        var revenue = await _context.HoaDons
            .Where(h => h.NgayThanhToan >= startDate && h.NgayThanhToan <= endDate)
            .SumAsync(h => h.TongTien);
        
        return revenue;
    }
}