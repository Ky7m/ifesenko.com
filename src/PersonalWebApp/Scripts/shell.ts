declare const bootstrap: {
    Collapse: {
        getOrCreateInstance(el: Element, options?: { toggle?: boolean }): { hide(): void; show(): void; };
    };
    ScrollSpy: new (el: Element, options?: { target?: string; rootMargin?: string }) => unknown;
};

namespace ifesenko.com.Shell {
    function applyBackground(el: HTMLElement): void {
        const src = el.dataset.imageSource;
        if (!src) {
            return;
        }
        el.style.backgroundImage = `url('${src}')`;
    }

    function fadeOutLoader(): void {
        const loader = document.getElementById('loader');
        if (!loader) {
            return;
        }
        loader.style.transition = 'opacity 600ms ease-out';
        loader.style.opacity = '0';
        window.setTimeout(() => { loader.style.display = 'none'; }, 650);
    }

    function wireNavbar(): void {
        const navbar = document.querySelector<HTMLElement>('.navbar');
        if (!navbar) {
            return;
        }

        const collapseEl = navbar.querySelector<HTMLElement>('.navbar-collapse');

        navbar.addEventListener('click', (e) => {
            const target = e.target as HTMLElement | null;
            if (!target) {
                return;
            }
            const link = target.closest('.nav-link') as HTMLElement | null;
            if (!link || link.classList.contains('dropdown-toggle')) {
                return;
            }
            if (collapseEl && collapseEl.classList.contains('show')) {
                bootstrap.Collapse.getOrCreateInstance(collapseEl).hide();
            }
        });

        const applyScrollState = () => {
            const scrolled = window.scrollY >= navbar.offsetHeight;
            navbar.classList.toggle('navbar-color', scrolled);
        };
        const applyCollapseState = () => {
            navbar.classList.toggle('custom-collapse', window.innerWidth <= 991);
        };

        applyScrollState();
        applyCollapseState();
        window.addEventListener('scroll', applyScrollState, { passive: true });
        window.addEventListener('resize', applyCollapseState);
    }

    function wireSmoothScroll(): void {
        document.querySelectorAll<HTMLAnchorElement>("a[href*='#']").forEach(anchor => {
            anchor.addEventListener('click', (e) => {
                const href = anchor.getAttribute('href');
                if (!href || href === '#' || !href.includes('#')) {
                    return;
                }
                const hash = href.substring(href.indexOf('#'));
                if (hash.length < 2) {
                    return;
                }
                const target = document.querySelector(hash);
                if (!target) {
                    return;
                }
                e.preventDefault();
                target.scrollIntoView({ behavior: 'smooth', block: 'start' });
            });
        });
    }

    function wireTextRotator(): void {
        const el = document.getElementById('textrotator');
        if (el) {
            ifesenko.com.TextRotator.init(el, { separator: '|', speed: 3000 });
        }
    }

    document.addEventListener('DOMContentLoaded', () => {
        const home = document.getElementById('home');
        if (home) {
            applyBackground(home);
        }
        wireNavbar();
        wireSmoothScroll();
        wireTextRotator();
    });

    window.addEventListener('load', fadeOutLoader);
}
