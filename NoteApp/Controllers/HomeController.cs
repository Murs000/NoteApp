using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;
using NoteApp.Services;
using System;
using System.Collections.Generic;

namespace NoteApp.Controllers;

public class HomeController(NoteService noteService, UrlService urlService) : Controller
{

    public async Task<IActionResult> Index()
    {
        return View(await noteService.GetAll());
    }

    [HttpPost]
    public async Task<IActionResult> CreateNode()
    {
        var newNode = new Note
        {
            Content = "New Node"
        };

        await noteService.Add(newNode);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> SaveNode(int id, string content)
    {
        var node = await noteService.Get(id);
        if (node != null)
        {
            node.Content = content;
        }
        await noteService.Update(node);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteNode(int id)
    {
        await noteService.Delete(id);
        return RedirectToAction("Index");
    }

    // Action to create and share the shortened URL
    public IActionResult ShareNode(int id)
    {
        var shortenedUrl = urlService.CreateURL(id);
        var fullUrl = Url.Action("RedirectToOriginal", "Home", new { shortUrl = shortenedUrl }, Request.Scheme);

        return Json(new { shortUri = fullUrl });
    }

    // Action to handle the redirection when a shortened URL is accessed
    public IActionResult RedirectToOriginal(string shortUrl)
    {
        var nodeId = urlService.GetURL(shortUrl);
        if (nodeId != null)
        {
            return RedirectToAction("NodeDetail", new { id = nodeId });
        }
        return NotFound();
    }

    public async Task<IActionResult> NodeDetail(int id)
    {
        var node = await noteService.Get(id);
        if (node != null)
        {
            return View("NodeDetail", node);
        }
        return NotFound();
    }
}
