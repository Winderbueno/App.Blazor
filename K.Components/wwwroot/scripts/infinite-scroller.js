// source : https://www.meziantou.net/infinite-scrolling-in-blazor.html

export function initialize(lastItemIndicator, componentInstance) {
    const observer = new IntersectionObserver(async (entries) => {
       // When the lastItemIndicator element is visible => invoke the C# method `LoadMore`
        for (const entry of entries) {
            if (entry.isIntersecting) {
                observer.unobserve(lastItemIndicator);
                await componentInstance.invokeMethodAsync("LoadMore");
           }    
        }
    });

    observer.observe(lastItemIndicator);

    // Allow to cleanup resources when the Razor component is removed from the page
    return {
        onNewItems: () => {
            observer.unobserve(lastItemIndicator);
            observer.observe(lastItemIndicator);
        },
    };
}