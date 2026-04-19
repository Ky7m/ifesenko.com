namespace ifesenko.com.TextRotator {
    interface TextRotatorOptions {
        separator?: string;
        speed?: number;
    }

    export function init(element: HTMLElement, options: TextRotatorOptions = {}): void {
        const separator = options.separator ?? '|';
        const speed = options.speed ?? 3000;

        const raw = (element.textContent ?? '').trim();
        const items = raw.split(separator).map(s => s.trim()).filter(s => s.length > 0);
        if (items.length === 0) {
            return;
        }

        element.classList.add('text-rotator');
        element.textContent = items[0];

        if (items.length === 1) {
            return;
        }

        let index = 0;
        window.setInterval(() => {
            element.classList.add('text-rotator-fade-out');
            window.setTimeout(() => {
                index = (index + 1) % items.length;
                element.textContent = items[index];
                element.classList.remove('text-rotator-fade-out');
            }, 300);
        }, speed);
    }
}
