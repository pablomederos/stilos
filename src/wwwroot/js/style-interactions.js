document.addEventListener('DOMContentLoaded', () => {
    if (typeof gsap === 'undefined') return

    gsap.registerPlugin(ScrollTrigger)

    // ScrollTrigger configurations to improve performance and avoid forced reflows
    ScrollTrigger.config({
        ignoreMobileResize: true,
        autoRefreshEvents: "visibilitychange,DOMContentLoaded,load" // Avoid frequent refreshes
    })

    const styleTriggers = document.querySelectorAll('.style-trigger')

    styleTriggers.forEach(trigger => {
        trigger.addEventListener('click', () => {
            // Wait for the accordion transition (managed by CSS/site.js) to settle
            requestAnimationFrame(() => {
                setTimeout(() => {
                    const isExpanded = trigger.getAttribute('aria-expanded') === 'true'
                    if (isExpanded) {
                        const panelId = trigger.getAttribute('aria-controls')
                        const panel = document.getElementById(panelId)

                        const styleItem = trigger.closest('.style-item')
                        const styleId = styleItem.dataset.styleId

                        initAnimationsForStyle(styleId, panel)
                    }
                }, 50)
            })
        })
    })

    function initAnimationsForStyle(styleId, panel) {
        const demoContainer = panel.querySelector('.immersive-demo-container')
        if (!demoContainer) return

        // Targeted cleanup of previous animations to avoid expensive querySelectorAll('*')
        // We only target elements that were actually animated in the known styles
        const animatableElements = demoContainer.querySelectorAll('.landing-floating-element, .saas-stat-card, .portfolio-project')
        gsap.killTweensOf([demoContainer, ...animatableElements])

        // Generic entrance animation
        gsap.fromTo(demoContainer,
            { opacity: 0, y: 15 },
            { opacity: 1, y: 0, duration: 0.5, ease: "power2.out" }
        )

        // Style-specific animations (Maximalist, Cyberpunk, Psychedelic)
        if (['maximalism', 'cyberpunk', 'psychedelic'].includes(styleId)) {
            const floatingEls = demoContainer.querySelectorAll('.landing-floating-element, .saas-stat-card, .portfolio-project')
            if (floatingEls.length > 0) {
                gsap.fromTo(floatingEls,
                    { rotation: -2, y: 10 },
                    { rotation: 2, y: -10, duration: 2, yoyo: true, repeat: -1, ease: "sine.inOut", stagger: 0.2 }
                )
            }
        }
    }
})
