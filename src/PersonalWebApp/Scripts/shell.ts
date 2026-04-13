namespace ifesenko.com.Shell {
    // Set the background image on the #home section from its data attribute
    const homeSection = document.getElementById('home');
    if (homeSection) {
        const imageSource = homeSection.dataset['imageSource'];
        if (imageSource) {
            homeSection.style.backgroundImage = `url('${imageSource}')`;
            homeSection.style.backgroundSize = 'cover';
            homeSection.style.backgroundPosition = 'center center';
            homeSection.style.backgroundRepeat = 'no-repeat';
            homeSection.style.backgroundAttachment = 'fixed';
        }
    }

    window.addEventListener('load', () => {
        const loader = document.getElementById('loader');
        if (loader) {
            loader.style.transition = 'opacity 0.5s ease';
            loader.style.opacity = '0';
            setTimeout(() => { loader.style.display = 'none'; }, 500);
        }
    });

    document.addEventListener('DOMContentLoaded', () => {
        // Close the navbar collapse when a nav link is clicked (mobile)
        const navbarCollapse = document.querySelector('.navbar-collapse');
        if (navbarCollapse) {
            navbarCollapse.addEventListener('click', (e: Event) => {
                const target = e.target as HTMLElement;
                if (target.tagName === 'A' && !target.classList.contains('dropdown-toggle')) {
                    const bsCollapse = (window as any).bootstrap?.Collapse.getInstance(navbarCollapse);
                    bsCollapse?.hide();
                }
            });
        }

        // Smooth scroll only for same-page pure hash anchors (e.g. href="#profile")
        document.querySelectorAll<HTMLAnchorElement>("a[href^='#']").forEach(anchor => {
            anchor.addEventListener('click', (e: Event) => {
                const hash = anchor.getAttribute('href') ?? '';
                if (!hash || hash === '#') return;
                const target = document.querySelector(hash);
                if (target) {
                    e.preventDefault();
                    target.scrollIntoView({ behavior: 'smooth' });
                }
            });
        });

        // Navbar scroll behavior
        const navbar = document.querySelector<HTMLElement>('.navbar');
        if (navbar) {
            const navHeight = navbar.offsetHeight;

            const updateNavbar = () => {
                const scrolled = window.scrollY >= navHeight;
                navbar.classList.toggle('navbar-scrolled', scrolled);
            };

            const updateMobile = () => {
                const isMobile = window.innerWidth <= 767;
                navbar.classList.toggle('navbar-mobile', isMobile);
            };

            window.addEventListener('scroll', updateNavbar, { passive: true });
            window.addEventListener('resize', updateMobile, { passive: true });

            updateNavbar();
            updateMobile();
        }

        // Text rotator
        const rotatorEl = document.getElementById('textrotator');
        if (rotatorEl) {
            const items = rotatorEl.textContent?.split('|').map(s => s.trim()).filter(Boolean) ?? [];
            if (items.length > 1) {
                let current = 0;
                rotatorEl.textContent = items[0];
                setInterval(() => {
                    rotatorEl.style.transition = 'opacity 0.5s ease';
                    rotatorEl.style.opacity = '0';
                    setTimeout(() => {
                        current = (current + 1) % items.length;
                        rotatorEl.textContent = items[current];
                        rotatorEl.style.opacity = '1';
                    }, 500);
                }, 3000);
            }
        }
    });
}
