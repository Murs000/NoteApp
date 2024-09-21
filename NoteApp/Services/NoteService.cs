using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NoteApp.DataAccess;
using NoteApp.Models;

namespace NoteApp.Services;

public class NoteService
{
    private readonly NoteAppDB _context;

    public NoteService(NoteAppDB context)
    {
        _context = context;
    }

    public async Task<List<Note>> GetAll()
    {
        return await _context.Notes.OrderBy(n => n.Id).ToListAsync();
    }

    public async Task<Note> Get(int id)
    {
        return await _context.Notes.FirstAsync(n => n.Id == id);
    }

    public async Task<bool> Add(Note model)
    {
        await _context.Notes.AddAsync(model);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(Note model)
    {
         _context.Notes.Update(model);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        _context.Notes.Remove(await _context.Notes.FirstAsync(n => n.Id == id));
        await _context.SaveChangesAsync();
        return true;
    }
}
