(function () {
    'use strict';

    var listeners = [];
    var rotatorTimer = null;

    function applyBackground(el) {
        var src = el.dataset.imageSource;
        if (!src) { return; }
        el.style.backgroundImage = "url('" + src + "')";
    }

    function fadeOutLoader() {
        var loader = document.getElementById('loader');
        if (!loader) { return; }
        loader.style.transition = 'opacity 600ms ease-out';
        loader.style.opacity = '0';
        window.setTimeout(function () { loader.style.display = 'none'; }, 650);
    }

    function wireNavbar() {
        var navbar = document.querySelector('.navbar');
        if (!navbar) { return; }
        var collapseEl = navbar.querySelector('.navbar-collapse');

        var onClick = function (e) {
            var link = e.target && e.target.closest ? e.target.closest('.nav-link') : null;
            if (!link || link.classList.contains('dropdown-toggle')) { return; }
            if (collapseEl && collapseEl.classList.contains('show') && window.bootstrap) {
                window.bootstrap.Collapse.getOrCreateInstance(collapseEl).hide();
            }
        };
        var onScroll = function () {
            navbar.classList.toggle('navbar-color', window.scrollY >= navbar.offsetHeight);
        };
        var onResize = function () {
            navbar.classList.toggle('custom-collapse', window.innerWidth <= 991);
        };

        navbar.addEventListener('click', onClick);
        window.addEventListener('scroll', onScroll, { passive: true });
        window.addEventListener('resize', onResize);
        onScroll();
        onResize();

        listeners.push(
            function () { navbar.removeEventListener('click', onClick); },
            function () { window.removeEventListener('scroll', onScroll); },
            function () { window.removeEventListener('resize', onResize); }
        );
    }

    function wireSmoothScroll() {
        var anchors = document.querySelectorAll("a[href*='#']");
        anchors.forEach(function (anchor) {
            var handler = function (e) {
                var href = anchor.getAttribute('href');
                if (!href || href === '#' || href.indexOf('#') === -1) { return; }
                var hash = href.substring(href.indexOf('#'));
                if (hash.length < 2) { return; }
                var target = document.querySelector(hash);
                if (!target) { return; }
                e.preventDefault();
                target.scrollIntoView({ behavior: 'smooth', block: 'start' });
            };
            anchor.addEventListener('click', handler);
            listeners.push(function () { anchor.removeEventListener('click', handler); });
        });
    }

    function wireTextRotator() {
        var el = document.getElementById('textrotator');
        if (!el) { return; }
        var raw = (el.textContent || '').trim();
        var items = raw.split('|').map(function (s) { return s.trim(); }).filter(function (s) { return s.length > 0; });
        if (items.length === 0) { return; }

        el.classList.add('text-rotator');
        el.textContent = items[0];
        if (items.length === 1) { return; }

        var index = 0;
        rotatorTimer = window.setInterval(function () {
            el.classList.add('text-rotator-fade-out');
            window.setTimeout(function () {
                index = (index + 1) % items.length;
                el.textContent = items[index];
                el.classList.remove('text-rotator-fade-out');
            }, 300);
        }, 3000);
    }

    window.ifesenkoShell = {
        init: function () {
            var home = document.getElementById('home');
            if (home) { applyBackground(home); }
            wireNavbar();
            wireSmoothScroll();
            wireTextRotator();
            fadeOutLoader();
        },
        dispose: function () {
            if (rotatorTimer) { window.clearInterval(rotatorTimer); rotatorTimer = null; }
            listeners.forEach(function (off) { try { off(); } catch (_) { /* ignore */ } });
            listeners = [];
        }
    };
})();
