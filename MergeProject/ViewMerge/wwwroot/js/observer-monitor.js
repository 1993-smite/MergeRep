class Observer {

    constructor(target, config, callback) {
        // Выбираем целевой элемент
        this.target = target;

        // Конфигурация observer (за какими изменениями наблюдать)
        this.config = config || {
            attributes: true,
            childList: true,
            subtree: true
        };

        // Функция обратного вызова при срабатывании мутации
        this.callback = callback || function (mutationsList, observerEl) {
            for (let mutation of mutationsList) {
                if (mutation.type === 'childList') {
                    console.log('A child node has been added or removed.');
                } else if (mutation.type === 'attributes') {
                    console.log('The ' + mutation.attributeName + ' attribute was modified.');
                }
            }
        };

        // Создаем экземпляр наблюдателя с указанной функцией обратного вызова
        this.observer = new MutationObserver(this.callback);

        // Начинаем наблюдение за настроенными изменениями целевого элемента
        this.observer.observe(this.target, this.config);

        return this;
    }

    stopObserver() {
        // Позже можно остановить наблюдение
        this.observer.disconnect();
    }
}