(function () {
    'use strict';

    const THEME_KEY = 'theme-preference';
    let listeners = [];
    let rotatorTimer = null;
    let scrollTimeout = null;
    let lastScrollY = 0;

    function getTheme() {
        const stored = localStorage.getItem(THEME_KEY);
        if (stored) return stored;
        return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
    }

    function applyTheme(theme) {
        document.documentElement.setAttribute('data-theme', theme);
    }

    function setTheme(theme) {
        applyTheme(theme);
        localStorage.setItem(THEME_KEY, theme);
    }

    function toggleTheme() {
        const current = getTheme();
        const next = current === 'dark' ? 'light' : 'dark';
        setTheme(next);
    }

    function fadeOutLoader() {
        const loader = document.getElementById('loader');
        if (!loader) { return; }
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

        const onClick = function (e) {
            const link = e.target && e.target.closest ? e.target.closest('.nav-link') : null;
            if (link) {
                menu.classList.remove('open');
            }
        };

        const onToggle = function () {
            menu.classList.toggle('open');
        };

        const onScroll = function () {
            lastScrollY = window.scrollY;
            if (scrollTimeout) clearTimeout(scrollTimeout);
            scrollTimeout = window.setTimeout(function () {
                navbar.classList.toggle('scrolled', lastScrollY >= 50);
            }, 10);
        };

        toggle.addEventListener('click', onToggle);
        menu.addEventListener('click', onClick);
        window.addEventListener('scroll', onScroll, { passive: true });
        onScroll();

        listeners.push(
            function () { toggle.removeEventListener('click', onToggle); },
            function () { menu.removeEventListener('click', onClick); },
            function () { window.removeEventListener('scroll', onScroll); }
        );
    }

    function wireThemeToggle() {
        const button = document.querySelector('.theme-toggle');
        if (!button) { return; }

        const onClick = function () {
            toggleTheme();
        };

        button.addEventListener('click', onClick);
        listeners.push(function () { button.removeEventListener('click', onClick); });
    }

    function wireSystemThemeSync() {
        if (!window.matchMedia) { return; }
        const mq = window.matchMedia('(prefers-color-scheme: dark)');
        const onChange = function (e) {
            // Follow the OS only while the user hasn't made an explicit choice.
            if (localStorage.getItem(THEME_KEY)) { return; }
            applyTheme(e.matches ? 'dark' : 'light');
        };
        mq.addEventListener('change', onChange);
        listeners.push(function () { mq.removeEventListener('change', onChange); });
    }

    function wireSmoothScroll() {
        const anchors = document.querySelectorAll("a[href*='#']");
        anchors.forEach(function (anchor) {
            const handler = function (e) {
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
                target.scrollIntoView({ behavior: 'smooth', block: 'start' });
            };
            anchor.addEventListener('click', handler);
            listeners.push(function () { anchor.removeEventListener('click', handler); });
        });
    }

    function wireScrollReveal() {
        const sections = document.querySelectorAll('.section');
        if (!window.IntersectionObserver) { return; }

        const observer = new IntersectionObserver(function(entries) {
            entries.forEach(function(entry) {
                if (entry.isIntersecting) {
                    entry.target.classList.add('reveal');
                }
            });
        }, {
            threshold: 0.1,
            rootMargin: '0px 0px -100px 0px'
        });

        sections.forEach(function(section) {
            observer.observe(section);
        });

        listeners.push(function () { observer.disconnect(); });
    }

    function wireActiveLink() {
        const sections = document.querySelectorAll('.section');
        const navLinks = document.querySelectorAll('.nav-link');
        if (!window.IntersectionObserver || navLinks.length === 0) { return; }

        const observer = new IntersectionObserver(function(entries) {
            entries.forEach(function(entry) {
                if (entry.isIntersecting) {
                    const id = entry.target.getAttribute('id');
                    navLinks.forEach(function(link) {
                        link.classList.remove('active');
                        const href = link.getAttribute('href');
                        if (href === '#' + id) {
                            link.classList.add('active');
                        }
                    });
                }
            });
        }, {
            threshold: 0.3
        });

        sections.forEach(function(section) {
            observer.observe(section);
        });

        listeners.push(function () { observer.disconnect(); });
    }

    function wireTextRotator() {
        const el = document.getElementById('textrotator');
        if (!el) { return; }
        const raw = (el.textContent || '').trim();
        const items = raw.split('|').map(function (s) { return s.trim(); }).filter(function (s) { return s.length > 0; });
        if (items.length === 0) { return; }

        el.classList.add('text-rotator');
        el.textContent = items[0];
        if (items.length === 1) { return; }

        let index = 0;
        rotatorTimer = window.setInterval(function () {
            el.classList.add('text-rotator-fade-out');
            window.setTimeout(function () {
                index = (index + 1) % items.length;
                el.textContent = items[index];
                el.classList.remove('text-rotator-fade-out');
            }, 300);
        }, 3000);
    }

    function wireScrollCue() {
        const cue = document.querySelector('.scroll-cue');
        if (!cue) { return; }

        const handler = function () {
            const target = document.getElementById('profile-contact') || document.getElementById('profile');
            if (target) {
                target.scrollIntoView({ behavior: 'smooth', block: 'start' });
            }
        };

        cue.addEventListener('click', handler);
        listeners.push(function () { cue.removeEventListener('click', handler); });
    }

    window.ifesenkoShell = {
        init: function () {
            wireNavbar();
            wireThemeToggle();
            wireSystemThemeSync();
            wireSmoothScroll();
            wireScrollReveal();
            wireActiveLink();
            wireTextRotator();
            wireScrollCue();
            fadeOutLoader();
        },
        dispose: function () {
            if (rotatorTimer) { window.clearInterval(rotatorTimer); rotatorTimer = null; }
            if (scrollTimeout) { window.clearTimeout(scrollTimeout); scrollTimeout = null; }
            listeners.forEach(function (off) { try { off(); } catch (_) { /* ignore */ } });
            listeners = [];
        }
    };
})();
