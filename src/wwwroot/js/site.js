const ASSET_VERSION = '1.2.0';
let demoStylesLoaded = false;
let isDemoLoading = false;

const loadScript = (src) => {
    return new Promise((resolve, reject) => {
        const script = document.createElement('script');
        script.src = src;
        script.onload = resolve;
        script.onerror = reject;
        document.body.appendChild(script);
    });
};

const loadDemoStyles = () => {
    if (demoStylesLoaded || isDemoLoading) return Promise.resolve();
    isDemoLoading = true;

    return new Promise(async (resolve, reject) => {
        const progress = document.createElement('div');
        progress.id = 'demo-load-progress';
        // Thinner progress bar (2px) and better styling
        progress.style.cssText = 'position:fixed;top:0;left:0;height:2px;background:var(--color-accent);z-index:9999;transition:width 0.4s cubic-bezier(0.1, 0, 0, 1);width:0;box-shadow: 0 0 12px var(--color-accent);';
        document.body.appendChild(progress);

        // Add loading state to all demo bodies to show skeleton
        const demoBodies = document.querySelectorAll('.style-demo-body');
        demoBodies.forEach(body => body.classList.add('is-loading'));

        // Slow progress to 80%
        setTimeout(() => progress.style.width = '80%', 10);

        try {
            // Load CSS with static version
            const link = document.createElement('link');
            link.rel = 'stylesheet';
            link.href = `/css/style-demos.css?v=${ASSET_VERSION}`;
            document.head.appendChild(link);

            // Load decorative fonts (Consolidated for parallel loading)
            const fontsLink = document.createElement('link');
            fontsLink.rel = 'stylesheet';
            const fonts = [
                'UnifrakturMaguntia',
                'Playfair+Display:ital,wght@0,400..900;1,400..900',
                'Cormorant+Garamond:ital,wght@0,300..700;1,300..700',
                'Crimson+Text:ital,wght@0,400;0,600;0,700;1,400;1,600;1,700',
                'Oswald:wght@400;500;600;700',
                'Special+Elite',
                'IM+Fell+English:ital@0;1',
                'Share+Tech+Mono',
                'Fredoka+One',
                'Varela+Round',
                'Caveat:wght@400;700',
                'Outfit:wght@300;600'
            ].join('&family=');

            fontsLink.href = `https://fonts.googleapis.com/css2?family=${fonts}&display=swap`;
            document.head.appendChild(fontsLink);

            // Load GSAP and interactions with static version
            await Promise.all([
                loadScript(`/js/vendor/gsap.min.js?v=${ASSET_VERSION}`),
                loadScript(`/js/vendor/ScrollTrigger.min.js?v=${ASSET_VERSION}`),
                loadScript(`/js/style-interactions.js?v=${ASSET_VERSION}`)
            ]);

            progress.style.width = '100%';
            demoStylesLoaded = true;
            isDemoLoading = false;

            // Remove loading state from containers and items
            demoBodies.forEach(body => body.classList.remove('is-loading'));
            document.querySelectorAll('.item-loading').forEach(item => item.classList.remove('item-loading'));

            setTimeout(() => {
                progress.style.opacity = '0';
                setTimeout(() => {
                    progress.remove();
                    resolve();
                }, 400);
            }, 200);
        } catch (err) {
            progress.remove();
            isDemoLoading = false;
            demoBodies.forEach(body => body.classList.remove('is-loading'));
            document.querySelectorAll('.item-loading').forEach(item => item.classList.remove('item-loading'));
            console.error('Failed to load demo resources', err);
            resolve();
        }
    });
};

const initAccordion = () => {
    document.querySelectorAll('.style-trigger').forEach(trigger => {
        trigger.addEventListener('click', async () => {
            const item = trigger.closest('.style-item')
            const id = item.dataset.styleId
            const isExpanded = item.dataset.expanded === 'true'

            // If opening and not loaded yet, start the loading process
            if (!isExpanded && !demoStylesLoaded) {
                loadDemoStyles(); // Don't await here to allow the accordion to open immediately
            }

            // Close other items
            document.querySelectorAll('.style-item[data-expanded="true"]').forEach(open => {
                if (open !== item) open.dataset.expanded = 'false'
            })

            // Toggle current item
            const newExpandedState = !isExpanded;
            item.dataset.expanded = newExpandedState ? 'true' : 'false'
            trigger.setAttribute('aria-expanded', newExpandedState ? 'true' : 'false')

            if (newExpandedState) {
                // Add temporary loading class to the item for extra feedback
                if (!demoStylesLoaded) {
                    item.classList.add('item-loading');
                }

                const url = new URL(window.location)
                url.searchParams.set('style', id)
                history.pushState(null, '', url)

                setTimeout(() => {
                    requestAnimationFrame(() => {
                        item.scrollIntoView({ behavior: 'smooth', block: 'start' })
                    })
                }, 100)
            } else {
                item.classList.remove('item-loading');
                const url = new URL(window.location)
                url.searchParams.delete('style')
                history.pushState(null, '', url)
            }
        })
    })

    // Check if initial URL has a style, if so load styles immediately
    if (new URLSearchParams(window.location.search).has('style')) {
        loadDemoStyles();
    }
}

const initAnimations = () => {
    const observer = new IntersectionObserver(entries => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('visible')
                observer.unobserve(entry.target)
            }
        })
    }, { threshold: 0.1 })

    document.querySelectorAll('.animate-in').forEach(el => observer.observe(el))
}

const initShareModal = () => {
    const modal = document.getElementById('share-modal')
    const trigger = document.getElementById('btn-share-trigger')
    const closeBtn = document.getElementById('modal-close')
    const copyBtn = document.getElementById('btn-copy-link')
    const copyLabel = document.getElementById('copy-label')
    const shareOptions = document.querySelectorAll('.share-option[data-platform]:not(#btn-copy-link)')

    if (!modal || !trigger) return

    const openModal = () => {
        modal.showModal()
        modal.classList.add('active')
        document.body.classList.add('modal-open')
    }

    const closeModal = () => {
        modal.classList.remove('active')
        document.body.classList.remove('modal-open')
        setTimeout(() => {
            if (!modal.classList.contains('active')) {
                modal.close()
            }
        }, 300) // Match transition-normal
    }

    trigger.addEventListener('click', openModal)
    closeBtn?.addEventListener('click', closeModal)

    modal.addEventListener('click', (e) => {
        const dialogDimensions = modal.getBoundingClientRect()
        if (
            e.clientX < dialogDimensions.left ||
            e.clientX > dialogDimensions.right ||
            e.clientY < dialogDimensions.top ||
            e.clientY > dialogDimensions.bottom
        ) {
            closeModal()
        }
    })

    document.addEventListener('keydown', (e) => {
        if (e.key === 'Escape' && modal.classList.contains('active')) closeModal()
    })

    const currentUrl = window.location.href
    const shareText = "¡Mira este catálogo visual de estilos CSS! 54+ diseños increíbles en Stilos."

    shareOptions.forEach(option => {
        option.addEventListener('click', (e) => {
            const platform = option.dataset.platform
            let url = ''

            switch (platform) {
                case 'whatsapp':
                    url = `https://wa.me/?text=${encodeURIComponent(shareText + ' ' + currentUrl)}`
                    break
                case 'twitter':
                    url = `https://twitter.com/intent/tweet?text=${encodeURIComponent(shareText)}&url=${encodeURIComponent(currentUrl)}`
                    break
                case 'linkedin':
                    url = `https://www.linkedin.com/sharing/share-offsite/?url=${encodeURIComponent(currentUrl)}`
                    break
            }

            if (url) {
                e.preventDefault()
                window.open(url, '_blank', 'noopener,noreferrer')
            }
        })
    })

    copyBtn?.addEventListener('click', () => {
        navigator.clipboard.writeText(currentUrl).then(() => {
            const originalText = copyLabel.textContent
            copyBtn.classList.add('success')
            copyLabel.textContent = '¡Copiado!'

            setTimeout(() => {
                copyBtn.classList.remove('success')
                copyLabel.textContent = originalText
            }, 2000)
        })
    })
}

document.addEventListener('DOMContentLoaded', () => {
    initAccordion()
    initAnimations()
    initShareModal()
})
