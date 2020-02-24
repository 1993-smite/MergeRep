$(document).ready(function () {

    let zones = $("[data-id='dropZone']"),
        maxFileSize = 1000000; // максимальный размер файла - 1 мб.

    if (zones.length < 1) return;

    zones.each(function (el) {
        let dropZone = $(this);
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

        dropZone[0].ondrop = function (event) {
            event.preventDefault();
            dropZone.removeClass('hover');
            dropZone.addClass('drop');
            var files = event.dataTransfer.files;
            uploadFiles.call(this, files, dropZone);
        };

        $("input[type='file']", dropZone).change(function (event) {
            dropZone.removeClass('hover');
            dropZone.addClass('drop');
            var files = event.target.files;
            uploadFiles.call(this, files, dropZone);
        });
    });

    function uploadFiles(files, dropZone) {
        var loadfiles = [];
        for (let file of files) {
            if (file.size > maxFileSize) {
                dropZone.text(`Файл (${file.name}) слишком большой!`);
                dropZone.addClass('error');
                return false;
            }

            let form = $(this).parents(`form[type='files']`)[0];
            let formData = new FormData(form);
            formData.append("id", model.id);
            formData.append("uploadedFile", file);
            loadfiles.push(fetch($(form).attr("action"), { method: "POST", body: formData }));
        }

        Promise.all(loadfiles).then(results => {
            console.log("all files uploaded");
            dropZone.attr('class', "");
        });
    }
});