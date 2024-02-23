function onScroll(elem, interval) {
    elem.addEventListener('scroll', function (e) {
        // https://developer.mozilla.org/en-US/docs/Web/API/Element/scrollTop
        if (window.navMenuService != null)
            window.navMenuService.invokeMethodAsync('OnScroll', parseInt(e.target.scrollTop, 10));
    }, interval);
}

window.RegisterNavMenuService = (navMenuService) => {
    window.navMenuService = navMenuService;
}