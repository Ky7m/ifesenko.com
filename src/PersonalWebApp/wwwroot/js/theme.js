(function() {
  const THEME_KEY = 'theme-preference';

  function getTheme() {
    try {
      const stored = localStorage.getItem(THEME_KEY);
      if (stored === 'dark' || stored === 'light') return stored;
    } catch (e) {
      // localStorage may be blocked (privacy mode, storage quota exceeded, etc.)
    }
    return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
  }

  // Apply theme immediately to avoid a flash of the wrong theme.
  // Do NOT persist here: writing localStorage on every load would pin the
  // choice and stop the site from following later OS theme changes.
  const theme = getTheme();
  document.documentElement.setAttribute('data-theme', theme);
  const meta = document.querySelector('meta[name="theme-color"]');
  const themeColor = window.getComputedStyle(document.documentElement).getPropertyValue('--theme-color-browser').trim();
  if (meta && themeColor) { meta.setAttribute('content', themeColor); }
})();
