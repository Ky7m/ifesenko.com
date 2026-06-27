(function() {
  const THEME_KEY = 'theme-preference';
  const THEME_COLORS = { light: '#fafcfe', dark: '#0f1318' };

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
  if (meta) { meta.setAttribute('content', THEME_COLORS[theme]); }
})();
