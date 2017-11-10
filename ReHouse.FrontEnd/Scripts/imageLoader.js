var previewWidth = 300, // ширина превью
    previewHeight = 150, // высота превью
    maxFileSize = 10 * 1024 * 1024, // (байт) Максимальный размер файла (2мб)
    selectedFiles = {},// объект, в котором будут храниться выбранные файлы
    queue = [],
    queue_plan = [],
    image = new Image(),
    imgLoadHandler,
    isProcessing = false,
    isProcessing_plan = false,
    errorMsg, // сообщение об ошибке при валидации файла
    previewPhotoContainer = document.querySelector('#list'); // контейнер, в котором будут отображаться превью
    previewPhotoContainer_plan = document.querySelector('#list_plan'); // контейнер, в котором будут отображаться превью

// Когда пользователь выбрал файлы, обрабатываем их
$('input[type=file][id=files]').on('change', function () {
    var newFiles = $(this)[0].files; // массив с выбранными файлами

    for (var i = 0; i < newFiles.length; i++) {

        var file = newFiles[i];

        // В качестве "ключей" в объекте selectedFiles используем названия файлов
        // чтобы пользователь не мог добавлять один и тот же файл
        // Если файл с текущим названием уже существует в массиве, переходим к следующему файлу
        if (selectedFiles[file.name] != undefined) continue;

        // Валидация файлов (проверяем формат и размер)
        if (errorMsg = validateFile(file)) {
            alert(errorMsg);
            return;
        }

        // Добавляем файл в объект selectedFiles
        selectedFiles[file.name] = file;
        queue.push(file);

    }

    $(this).val('');
    processQueue(); // запускаем процесс создания миниатюр
});

// Валидация выбранного файла (формат, размер)
var validateFile = function (file) {
    if (!file.type.match(/image\/(jpeg|jpg|png|gif)/)) {
        return 'Фотография должна быть в формате jpg, png или gif';
    }

    if (file.size > maxFileSize) {
        return 'Размер фотографии не должен превышать 10 Мб';
    }
};

var listen = function (element, event, fn) {
    return element.addEventListener(event, fn, false);
};

// Создание миниатюры
var processQueue = function () {
    // Миниатюры будут создаваться поочередно
    // чтобы в один момент времени не происходило создание нескольких миниатюр
    // проверяем запущен ли процесс
    if (isProcessing) { return; }

    // Если файлы в очереди закончились, завершаем процесс
    if (queue.length == 0) {
        isProcessing = false;
        return;
    }

    isProcessing = true;

    var file = queue.pop(); // Берем один файл из очереди

    var li = document.createElement('DIV');
    var spanDel = document.createElement('A');
    var canvas = document.createElement('CANVAS');
    var ctx = canvas.getContext('2d');

    li.setAttribute('class', 'photo_');
    spanDel.innerHTML = 'X';

    li.setAttribute('data-id', file.name);

    image.removeEventListener('load', imgLoadHandler, false);

    // создаем миниатюру
    imgLoadHandler = function () {
        ctx.drawImage(image, 0, 0, previewWidth, previewHeight);
        URL.revokeObjectURL(image.src);
        li.appendChild(canvas);
        isProcessing = false;
        setTimeout(processQueue, 200); // запускаем процесс создания миниатюры для следующего изображения
    };
    li.appendChild(spanDel);

    // Выводим миниатюру в контейнере previewPhotoContainer
    previewPhotoContainer.appendChild(li);
    listen(image, 'load', imgLoadHandler);
    image.src = URL.createObjectURL(file);

    // Сохраняем содержимое оригинального файла в base64 в отдельном поле формы
    // чтобы при отправке формы файл был передан на сервер
    var fr = new FileReader();
    fr.readAsDataURL(file);
    fr.onload = (function (file) {
        return function (e) {
            $('#list').append(
                    '<input type="hidden" name="image" value="' + e.target.result + '" data-id="' + file.name + '">'
            );
        }
    })(file);
};

// Удаление фотографии
$(document).on('click', '#list div a', function () {
    var fileId = $(this).parents('div').attr('data-id');

    if (selectedFiles[fileId] != undefined) delete selectedFiles[fileId]; // Удаляем файл из объекта selectedFiles
    $(this).parents('div.photo_').remove(); // Удаляем превью
    $('input[name^=image][data-id="' + fileId + '"]').remove(); // Удаляем поле с содержимым файла
});


// Когда пользователь выбрал файлы, обрабатываем их
$('input[type=file][id=files_plan]').on('change', function () {
    var newFiles = $(this)[0].files; // массив с выбранными файлами

    for (var i = 0; i < newFiles.length; i++) {

        var file = newFiles[i];

        // В качестве "ключей" в объекте selectedFiles используем названия файлов
        // чтобы пользователь не мог добавлять один и тот же файл
        // Если файл с текущим названием уже существует в массиве, переходим к следующему файлу
        if (selectedFiles[file.name] != undefined) continue;

        // Валидация файлов (проверяем формат и размер)
        if (errorMsg = validateFile(file)) {
            alert(errorMsg);
            return;
        }

        // Добавляем файл в объект selectedFiles
        selectedFiles[file.name] = file;
        queue_plan.push(file);

    }

    $(this).val('');
    processQueue_plan(); // запускаем процесс создания миниатюр
});

// Создание миниатюры
var processQueue_plan = function () {
    // Миниатюры будут создаваться поочередно
    // чтобы в один момент времени не происходило создание нескольких миниатюр
    // проверяем запущен ли процесс
    if (isProcessing_plan) { return; }

    // Если файлы в очереди закончились, завершаем процесс
    if (queue_plan.length == 0) {
        isProcessing_plan = false;
        return;
    }

    isProcessing_plan = true;

    var file = queue_plan.pop(); // Берем один файл из очереди

    var li = document.createElement('DIV');
    var spanDel = document.createElement('A');
    var canvas = document.createElement('CANVAS');
    var ctx = canvas.getContext('2d');

    li.setAttribute('class', 'photo_');
    spanDel.innerHTML = 'X';

    li.setAttribute('data-id', file.name);

    image.removeEventListener('load', imgLoadHandler, false);

    // создаем миниатюру
    imgLoadHandler = function () {
        ctx.drawImage(image, 0, 0, previewWidth, previewHeight);
        URL.revokeObjectURL(image.src);
        li.appendChild(canvas);
        isProcessing_plan = false;
        setTimeout(processQueue_plan, 200); // запускаем процесс создания миниатюры для следующего изображения
    };
    li.appendChild(spanDel);

    // Выводим миниатюру в контейнере previewPhotoContainer
    previewPhotoContainer_plan.appendChild(li);
    listen(image, 'load', imgLoadHandler);
    image.src = URL.createObjectURL(file);

    // Сохраняем содержимое оригинального файла в base64 в отдельном поле формы
    // чтобы при отправке формы файл был передан на сервер
    var fr = new FileReader();
    fr.readAsDataURL(file);
    fr.onload = (function (file) {
        return function (e) {
            $('#list').append(
                    '<input type="hidden" name="planimage" value="' + e.target.result + '" data-id="' + file.name + '">'
            );
        }
    })(file);
};

$(document).on('click', '#list_plan div a', function () {
    var fileId = $(this).parents('div').attr('data-id');

    if (selectedFiles[fileId] != undefined) delete selectedFiles[fileId]; // Удаляем файл из объекта selectedFiles
    $(this).parents('div.photo_').remove(); // Удаляем превью
    $('input[name^=image_plan][data-id="' + fileId + '"]').remove(); // Удаляем поле с содержимым файла
});

