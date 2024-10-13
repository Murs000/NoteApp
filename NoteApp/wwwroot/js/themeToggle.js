// themeToggle.js

// Function to apply the dark/light mode based on preference
function applyTheme() {
    const theme = localStorage.getItem('theme');
    const themeIcon = document.getElementById('themeIcon');
    
    if (theme === 'dark') {
        document.body.classList.add('bg-dark', 'text-white');
        document.querySelector('.navbar').classList.add('bg-dark');
        document.querySelector('.navbar').classList.remove('bg-white');
        themeIcon.textContent = '☀️'; // Change to sun icon
    } else {
        document.body.classList.remove('bg-dark', 'text-white');
        document.querySelector('.navbar').classList.remove('bg-dark');
        document.querySelector('.navbar').classList.add('bg-white');
        themeIcon.textContent = '🌙'; // Change to moon icon
    }
}

// Call applyTheme on page load to set the theme
applyTheme();

// Dark/Light Mode Toggle Script
document.getElementById('themeToggle').addEventListener('click', function() {
    const currentTheme = localStorage.getItem('theme');
    if (currentTheme === 'dark') {
        localStorage.setItem('theme', 'light');
    } else {
        localStorage.setItem('theme', 'dark');
    }
    applyTheme();
});