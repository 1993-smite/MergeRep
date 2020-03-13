var lazy = [];

$(function () {
    let vue = new Vue({
        el: "#files",
        data: {
            files: model
        },
        computed: {
            fullName: function () {
                // `this` указывает на экземпляр vm
                return this.files.map(function (item) {
                    return `${pathRelativeServer}/${item.name}`
                });
            }
        }
    });

    setLazy();
    lazyLoad();

    $("#images").scroll(function () {
        console.info("test");
        lazyLoad();
    });
    $(window).resize(function () {
        lazyLoad();
    });
});

//registerListener('load', setLazy);
//registerListener('load', lazyLoad);
//registerListener('scroll', lazyLoad);
//registerListener('resize', lazyLoad);

function setLazy() {
    //document.getElementById('tbl-points').removeChild(document.getElementById('viewMore'));
    //document.getElementById('nextPage').removeAttribute('class');

    lazy = document.getElementsByClassName('lazy');
    console.log('Found ' + lazy.length + ' lazy images');
}

function lazyLoad() {
    for (var i = 0; i < lazy.length; i++) {
        if (isInViewport(lazy[i])) {
            if (lazy[i].getAttribute('data-src')) {
                lazy[i].src = lazy[i].getAttribute('data-src');
                lazy[i].removeAttribute('data-src');
            }
        }
    }

    cleanLazy();
}

function cleanLazy() {
    lazy = Array.prototype.filter.call(lazy, function (l) { return l.getAttribute('data-src'); });
}

function isInViewport(el) {
    var rect = el.getBoundingClientRect();

    return (
        rect.bottom >= 0 &&
        rect.right >= 0 &&
        rect.top <= (window.innerHeight || document.documentElement.clientHeight) &&
        rect.left <= (window.innerWidth || document.documentElement.clientWidth)
    );
}

function registerListener(event, func) {
    if (window.addEventListener) {
        window.addEventListener(event, func)
    } else {
        window.attachEvent('on' + event, func)
    }
}