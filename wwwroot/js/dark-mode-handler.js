const themeSwitch = document.getElementById("theme-switch");
const themeIndicator = document.getElementById("theme-indicator");
const themeStates = ["light", "dark"];
const indicators = ["fa-moon", "fa-sun"];
// Initialize theme from localStorage
let currentTheme = localStorage.getItem("theme") || themeStates[0];
// Function to set the theme and store it in localStorage
function setTheme(theme) {
    localStorage.setItem("theme", themeStates[theme]);
    currentTheme = themeStates[theme];
    document.body.setAttribute("data-theme", themeStates[theme]); // Set data-theme attribute
}
// Function to update the theme indicator icon
function setIndicator(theme) {
    themeIndicator.classList.remove(...indicators); // Removes all indicator classes
    themeIndicator.classList.add(indicators[theme]);
}
// Apply the initial theme based on localStorage
if (currentTheme === themeStates[0]) {
    setIndicator(0);
    setTheme(0);
    themeSwitch.checked = true;
} else if (currentTheme === themeStates[1]) {
    setIndicator(1);
    setTheme(1);
    themeSwitch.checked = false;
}
// Listen for theme toggle and apply changes
themeSwitch.addEventListener('change', function () {
    if (this.checked) {
        setTheme(0);
        setIndicator(0);
        
    } else {
        setTheme(1);
        setIndicator(1);
    }
});

