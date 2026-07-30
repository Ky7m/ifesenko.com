(function () {
    'use strict';

    const THEME_KEY = 'theme-preference';
    let listeners = [];

    function isReducedMotion() {
        return !!(window.matchMedia && window.matchMedia('(prefers-reduced-motion: reduce)').matches);
    }

    function getTheme() {
        try {
            const stored = localStorage.getItem(THEME_KEY);
            if (stored === 'dark' || stored === 'light') return stored;
        } catch (e) {
            // localStorage may be blocked
        }
        return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
    }

    function getThemeChromeColor() {
        return window.getComputedStyle(document.documentElement).getPropertyValue('--theme-color-browser').trim();
    }

    function applyTheme(theme) {
        document.documentElement.setAttribute('data-theme', theme);
        const meta = document.querySelector('meta[name="theme-color"]');
        const themeColor = getThemeChromeColor();
        if (meta && themeColor) { meta.setAttribute('content', themeColor); }
        const themeButtons = document.querySelectorAll('.theme-toggle');
        themeButtons.forEach(function (button) {
            button.setAttribute('aria-checked', theme === 'dark' ? 'true' : 'false');
            button.setAttribute('aria-label', theme === 'dark' ? 'Switch to light theme' : 'Switch to dark theme');
            button.setAttribute('title', theme === 'dark' ? 'Switch to light theme' : 'Switch to dark theme');
        });
    }

    function setTheme(theme) {
        applyTheme(theme);
        try {
            localStorage.setItem(THEME_KEY, theme);
        } catch (e) {
            // localStorage may be blocked; theme will still apply for this session
        }
    }

    function toggleTheme() {
        const current = getTheme();
        const next = current === 'dark' ? 'light' : 'dark';
        setTheme(next);
    }

    function fadeOutLoader() {
        const loader = document.getElementById('loader');
        if (!loader) { return; }
        loader.setAttribute('aria-hidden', 'true');
        loader.classList.add('fade-out');
        window.setTimeout(function () {
            loader.style.display = 'none';
        }, 300);
    }

    function wireNavbar() {
        const navbar = document.querySelector('.navbar');
        if (!navbar) { return; }
        
        const toggle = navbar.querySelector('.navbar-toggle');
        const menu = navbar.querySelector('.navbar-menu');
        if (!toggle || !menu) { return; }

        const closeMenu = function () {
            menu.classList.remove('open');
            document.body.classList.remove('nav-open');
            toggle.setAttribute('aria-expanded', 'false');
            toggle.setAttribute('aria-label', 'Open menu');
        };

        const onClick = function (e) {
            const link = e.target && e.target.closest ? e.target.closest('.nav-link') : null;
            if (link) {
                closeMenu();
            }
        };

        const onToggle = function () {
            const isOpen = !menu.classList.contains('open');
            if (isOpen) {
                menu.classList.add('open');
                document.body.classList.add('nav-open');
                toggle.setAttribute('aria-expanded', 'true');
                toggle.setAttribute('aria-label', 'Close menu');
                const firstLink = menu.querySelector('.nav-link, .theme-toggle');
                if (firstLink) {
                    firstLink.focus();
                }
                return;
            }

            closeMenu();
        };

        const onDocumentClick = function (e) {
            if (!menu.classList.contains('open')) { return; }
            if (navbar.contains(e.target)) { return; }
            closeMenu();
        };

        const onDocumentKeydown = function (e) {
            if (!menu.classList.contains('open')) { return; }
            if (e.key !== 'Escape') { return; }
            closeMenu();
            toggle.focus();
        };

        toggle.addEventListener('click', onToggle);
        menu.addEventListener('click', onClick);
        document.addEventListener('click', onDocumentClick);
        document.addEventListener('keydown', onDocumentKeydown);

        listeners.push(
            function () { toggle.removeEventListener('click', onToggle); },
            function () { menu.removeEventListener('click', onClick); },
            function () { document.removeEventListener('click', onDocumentClick); },
            function () { document.removeEventListener('keydown', onDocumentKeydown); }
        );
    }

    function wireThemeToggle() {
        const buttons = document.querySelectorAll('.theme-toggle');
        if (buttons.length === 0) { return; }

        const onClick = function () {
            toggleTheme();
        };

        buttons.forEach(function (button) {
            button.addEventListener('click', onClick);
            listeners.push(function () { button.removeEventListener('click', onClick); });
        });

        applyTheme(getTheme());
    }

    function wireSystemThemeSync() {
        if (!window.matchMedia) { return; }
        const mq = window.matchMedia('(prefers-color-scheme: dark)');
        const onChange = function (e) {
            // Follow the OS only while the user hasn't made an explicit choice.
            try {
                if (localStorage.getItem(THEME_KEY)) { return; }
            } catch (e) {
                // localStorage may be blocked; proceed with OS sync
            }
            applyTheme(e.matches ? 'dark' : 'light');
        };
        mq.addEventListener('change', onChange);
        listeners.push(function () { mq.removeEventListener('change', onChange); });
    }

    function wireSmoothScroll() {
        const onClick = function (e) {
            const anchor = e.target && e.target.closest ? e.target.closest("a[href*='#']") : null;
            if (!anchor) { return; }

            const href = anchor.getAttribute('href');
            if (!href || href === '#' || href.indexOf('#') === -1) { return; }

            const url = new URL(href, window.location.href);
            const samePage = url.origin === window.location.origin &&
                url.pathname === window.location.pathname &&
                url.search === window.location.search;
            if (!samePage) { return; }

            const hash = url.hash;
            if (hash.length < 2) { return; }

            const target = document.querySelector(hash);
            if (!target) { return; }

            e.preventDefault();
            target.scrollIntoView({
                behavior: isReducedMotion() ? 'auto' : 'smooth',
                block: 'start'
            });
        };

        document.addEventListener('click', onClick);
        listeners.push(function () { document.removeEventListener('click', onClick); });
    }

    function wireCopyrightYear() {
        const yearNode = document.getElementById('copyright-year');
        if (!yearNode) { return; }
        yearNode.textContent = new Date().getFullYear().toString();
    }

    function wireScrollState() {
        const navbar = document.querySelector('.navbar');
        const sections = Array.prototype.slice.call(document.querySelectorAll('.section'));
        const navLinks = Array.prototype.slice.call(document.querySelectorAll('.nav-link'));
        if (!navbar || sections.length === 0 || navLinks.length === 0) { return; }

        let frameId = 0;
        let sectionOffsets = [];

        function measureSections() {
            sectionOffsets = sections.map(function (section) {
                return {
                    id: section.getAttribute('id'),
                    top: section.offsetTop
                };
            });
        }

        function update() {
            frameId = 0;
            const scrollY = window.scrollY || window.pageYOffset || 0;
            if (navbar) {
                navbar.classList.toggle('scrolled', scrollY >= 50);
            }
            if (sections.length === 0 || navLinks.length === 0) { return; }
            const pos = scrollY + 120;
            let currentId = sectionOffsets[0] && sectionOffsets[0].id;
            sectionOffsets.forEach(function (section) {
                if (section.top <= pos) { currentId = section.id; }
            });

            const docHeight = document.documentElement.scrollHeight;
            const scrollable = docHeight > window.innerHeight + 4;
            const atBottom = (window.innerHeight + scrollY) >= (docHeight - 2);
            if (scrollable && atBottom) {
                currentId = sectionOffsets[sectionOffsets.length - 1] && sectionOffsets[sectionOffsets.length - 1].id;
            }

            navLinks.forEach(function (link) {
                link.classList.toggle('active', link.getAttribute('href') === '#' + currentId);
            });
        }

        function requestUpdate() {
            if (frameId !== 0) { return; }
            frameId = window.requestAnimationFrame(update);
        }

        function refreshMeasurements() {
            measureSections();
            requestUpdate();
        }

        measureSections();
        window.addEventListener('scroll', requestUpdate, { passive: true });
        window.addEventListener('resize', refreshMeasurements);
        window.addEventListener('load', refreshMeasurements);
        requestUpdate();

        listeners.push(function () {
            window.removeEventListener('scroll', requestUpdate);
            window.removeEventListener('resize', refreshMeasurements);
            window.removeEventListener('load', refreshMeasurements);
            if (frameId !== 0) {
                window.cancelAnimationFrame(frameId);
                frameId = 0;
            }
        });
    }

    window.ifesenkoShell = {
        init: function () {
            wireNavbar();
            wireThemeToggle();
            wireSystemThemeSync();
            wireSmoothScroll();
            wireScrollState();
            wireCopyrightYear();
            fadeOutLoader();
        },
        dispose: function () {
            listeners.forEach(function (off) { try { off(); } catch (_) { /* ignore */ } });
            listeners = [];
            document.body.classList.remove('nav-open');
        }
    };
})();
