(function () {
    'use strict';

    const THEME_KEY = 'theme-preference';
    let listeners = [];
    let scrollTimeout = null;
    let lastScrollY = 0;

    function getTheme() {
        try {
            const stored = localStorage.getItem(THEME_KEY);
            if (stored === 'dark' || stored === 'light') return stored;
        } catch (e) {
            // localStorage may be blocked
        }
        return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
    }

    function applyTheme(theme) {
        document.documentElement.setAttribute('data-theme', theme);
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
                toggle.setAttribute('aria-expanded', 'false');
            }
        };

        const onToggle = function () {
            menu.classList.toggle('open');
            toggle.setAttribute('aria-expanded', menu.classList.contains('open') ? 'true' : 'false');
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
        const sections = Array.prototype.slice.call(document.querySelectorAll('.section'));
        const navLinks = Array.prototype.slice.call(document.querySelectorAll('.nav-link'));
        if (sections.length === 0 || navLinks.length === 0) { return; }

        let ticking = false;

        function update() {
            ticking = false;
            const scrollY = window.scrollY || window.pageYOffset || 0;
            const pos = scrollY + 120;

            let currentId = sections[0].getAttribute('id');
            sections.forEach(function (section) {
                const top = section.getBoundingClientRect().top + scrollY;
                if (top <= pos) { currentId = section.getAttribute('id'); }
            });

            const docHeight = document.documentElement.scrollHeight;
            const scrollable = docHeight > window.innerHeight + 4;
            const atBottom = (window.innerHeight + scrollY) >= (docHeight - 2);
            if (scrollable && atBottom) {
                currentId = sections[sections.length - 1].getAttribute('id');
            }

            navLinks.forEach(function (link) {
                link.classList.toggle('active', link.getAttribute('href') === '#' + currentId);
            });
        }

        function onScroll() {
            if (!ticking) { window.requestAnimationFrame(update); ticking = true; }
        }

        window.addEventListener('scroll', onScroll, { passive: true });
        window.addEventListener('resize', onScroll);
        update();

        listeners.push(function () {
            window.removeEventListener('scroll', onScroll);
            window.removeEventListener('resize', onScroll);
        });
    }

    window.ifesenkoShell = {
        init: function () {
            wireNavbar();
            wireThemeToggle();
            wireSystemThemeSync();
            wireSmoothScroll();
            wireScrollReveal();
            wireActiveLink();
            fadeOutLoader();
        },
        dispose: function () {
            if (scrollTimeout) { window.clearTimeout(scrollTimeout); scrollTimeout = null; }
            listeners.forEach(function (off) { try { off(); } catch (_) { /* ignore */ } });
            listeners = [];
        }
    };
})();
