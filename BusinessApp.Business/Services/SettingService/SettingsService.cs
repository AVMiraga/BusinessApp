using BusinessApp.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace BusinessApp.Business.Services.SettingService
{
    public class SettingsService
    {
        private readonly AppDbContext _context;

        public SettingsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, string>> GetSettings() 
        {
            return await _context.Settings.ToDictionaryAsync(s => s.Key, s => s.Value);
        }
    }
}
