function onScroll(elem, interval) {
    elem.addEventListener('scroll', function (e) {
        // https://developer.mozilla.org/en-US/docs/Web/API/Element/scrollTop
        if (window.scrollInfoService != null)
            window.scrollInfoService.invokeMethodAsync('OnScroll', parseInt(e.target.scrollTop, 10));
    }, interval);
}

window.RegisterScrollInfoService = (scrollInfoService) => {
    window.scrollInfoService = scrollInfoService;
}