window.chatScroll = {
    scrollToBottom: function (element) {
        if (!element) {
            return;
        }

        requestAnimationFrame(function () {
            element.scrollTo({
                top: element.scrollHeight,
                behavior: "smooth"
            });
        });
    }
};
