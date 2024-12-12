window.screenHelper = {
    getScreenWidth: () => window.innerWidth,
    onResize: (dotNetHelper) => {
        window.addEventListener("resize", () => {
            dotNetHelper.invokeMethodAsync("UpdateScreenWidth", window.innerWidth);
        });
    }
}

window.scrollTo({
    top: 0,
    behavior: 'smooth'
});