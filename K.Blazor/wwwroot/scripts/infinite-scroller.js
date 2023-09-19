// See. https://www.meziantou.net/infinite-scrolling-in-blazor.htm
export function initialize(lastItemIndicator, componentInstance) {

    const options = {
        root: findClosestScrollContainer(lastItemIndicator),
        rootMargin: '0px',
        threshold: 0,
    };

    const observer = new IntersectionObserver(async (entries) => {
       // When the lastItemIndicator element is visible => invoke the C# method `LoadMore`
        for (const entry of entries) {
            if (entry.isIntersecting) {
                observer.unobserve(lastItemIndicator);
                await componentInstance.invokeMethodAsync("LoadMore");
           }    
        }
    }, options);

    observer.observe(lastItemIndicator);

    // Allow to cleanup resources when the Razor component is removed from the page
    return {
        dispose: () => dispose(observer),
        onNewItems: () => {
            observer.unobserve(lastItemIndicator);
            observer.observe(lastItemIndicator);
        },
    };
}

// Cleanup resources
function dispose(observer) {
    observer.disconnect();
}

// Find the parent element with a vertical scrollbar
// This container should be use as the root for the IntersectionObserver
function findClosestScrollContainer(element) {
    while (element) {
        const style = getComputedStyle(element);
        if (style.overflowY !== 'visible') {
            return element;
        }
        element = element.parentElement;
    }
    return null;
}