(function() {
  const THEME_KEY = 'theme-preference';
  
  function getTheme() {
    const stored = localStorage.getItem(THEME_KEY);
    if (stored) return stored;
    return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
  }
  
  // Apply theme immediately to avoid a flash of the wrong theme.
  // Do NOT persist here: writing localStorage on every load would pin the
  // choice and stop the site from following later OS theme changes.
  document.documentElement.setAttribute('data-theme', getTheme());
})();
