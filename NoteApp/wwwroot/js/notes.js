document.getElementById("createNote").addEventListener("click", function () {
    fetch('/Home/CreateNote', { method: 'POST' })
        .then(() => location.reload());
});

document.querySelectorAll(".saveNote").forEach(button => {
    button.addEventListener("click", function () {
        const id = button.getAttribute("data-id");
        const content = document.getElementById(`content-${id}`).value; 
        fetch(`/Home/SaveNote?id=${id}&content=${encodeURIComponent(content)}`, { method: 'POST' })
            .then(() => location.reload());
    });
});

document.querySelectorAll(".shareNote").forEach(button => {
    button.addEventListener("click", function () {
        const id = button.getAttribute("data-id");
        fetch(`/Home/ShareNote?id=${id}`)
            .then(response => response.text())
            .then(shortUrl => {
                alert(`Shortened URL: ${shortUrl}`);
                navigator.clipboard.writeText(shortUrl).then(() => alert("Link copied to clipboard!"));
            });
    });
});

document.querySelectorAll(".deleteNote").forEach(button => {
    button.addEventListener("click", function () {
        const id = button.getAttribute("data-id");
        fetch(`/Home/DeleteNote?id=${id}`, { method: 'POST' })
            .then(() => location.reload());
    });
});