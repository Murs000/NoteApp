using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NoteApp.DataAccess;
using NoteApp.Models;

namespace NoteApp.Services;

public class UrlService
{
    private readonly NoteAppDB _context;

    public UrlService(NoteAppDB context)
    {
        _context = context;
    }

    public async Task<string> CreateURL(int noteId)
    {
        var shortUrl = GenerateShortUrl();

        if(_context.URLs.Select(u=>u.NoteId).Contains(noteId))
        {
            var returnUrl =  await _context.URLs.FirstAsync(u => u.NoteId == noteId);

            return returnUrl.Short;
        }

        var uRL = new URL
        {
            NoteId = noteId,
            Short = shortUrl
        };

        await _context.AddAsync(uRL);
        await _context.SaveChangesAsync();

        return shortUrl;
    }

    public async Task<int> GetURL(string shortURL)
    {

        var uRL = await _context.URLs.FirstAsync(u => u.Short == shortURL);

        return uRL.NoteId;
    }

    private string GenerateShortUrl()
    {
        var guid = Guid.NewGuid().ToString().Substring(0, 6);
        return guid;
    }
}