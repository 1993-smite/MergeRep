$(document).ready(function () {
    var dropZone = $('#dropZone'),
        maxFileSize = 1000000; // максимальный размер файла - 1 мб.

    if (dropZone.length < 1) return;

    if (typeof (window.FileReader) == 'undefined') {
        dropZone.text('Не поддерживается браузером!');
        dropZone.addClass('error');
    }
    dropZone[0].ondragover = function () {
        dropZone.addClass('hover');
        return false;
    };

    dropZone[0].ondragleave = function () {
        dropZone.removeClass('hover');
        return false;
    };

    function uploadProgress(event) {
        var percent = parseInt(event.loaded / event.total * 100);
        dropZone.text('Загрузка: ' + percent + '%');
    }

    function stateChange(event) {
        if (event.target.readyState == 4) {
            if (event.target.status == 200) {
                dropZone.text('Загрузка успешно завершена!');
            } else {
                dropZone.text('Произошла ошибка!');
                dropZone.addClass('error');
            }
        }
    }

    dropZone[0].ondrop = function (event) {
        event.preventDefault();
        dropZone.removeClass('hover');
        dropZone.addClass('drop');
        var files = event.dataTransfer.files;
        var loadfiles = [];
        for (let file of files) {
            if (file.size > maxFileSize) {
                dropZone.text(`Файл (${file.name}) слишком большой!`);
                dropZone.addClass('error');
                return false;
            }

            let form = $(this).parents("form")[0];
            let formData = new FormData(form);
            formData.append("id", model.id);
            formData.append("uploadedFile", file);
            loadfiles.push(fetch($(form).attr("action"), { method: "POST", body: formData }));
        }

        Promise.all(loadfiles).then(results => {
            console.log("all files uploaded");
            dropZone.attr('class',"");
        });
        //dropZone.removeClass('drop');

        /*var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener('progress', uploadProgress, false);
        xhr.onreadystatechange = stateChange;
        xhr.open('POST', $(this).attr("action"));
        xhr.setRequestHeader('X-FILE-NAME', file.name);
        xhr.send(file);*/
    };
});