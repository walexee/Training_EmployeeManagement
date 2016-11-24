var app = app || {};

app.viewManager = app.viewManager || {};

(function (viewManager) {
    var baseTemplateUrl = '/templates/';

    viewManager.showView = function (template, data, $location) {
        return showTemplate(template, data, $('div#main-view'));
    };

    viewManager.showModal = function (template, data, $location) {
        var defer = jQuery.Deferred();
        var $location = $location || $('div#modal-view');

        showTemplate(template, data, $location)
            .done(function () {
                var $modal = $location.find('div.modal');

                $modal.modal('show');
                defer.resolve($modal);
            })
            .fail(function () {
                defer.reject(false);
            });

        return defer.promise();
    };

    viewManager.showDialog = function (message, title, isConfirm) {
        var defer = jQuery.Deferred();
        var $location = $('div#dialog-view');
        var data = {
            title: title || 'Alert',
            message: message,
            isConfirm: isConfirm || false
        };

        viewManager
            .showModal('dialog.html', data, $location)
            .done(function ($modal) {
                $location.find('button#ok-btn').click(function () {
                    $modal.modal('hide');
                    defer.resolve(true);
                });

                if (!isConfirm)
                    return;

                $location.find('button#cancel-btn').click(function () {
                    $modal.modal('hide');
                    defer.resolve(false);
                });
            });

        return defer.promise();
    }

    function showTemplate(template, data, $location) {
        var defer = jQuery.Deferred();

        $.get(baseTemplateUrl + template).done(function (returnedTemplate) {
            if (!data) {
                $location.html(returnedTemplate);
                defer.resolve(true);
                return;
            }

            var compiledTemplate = Handlebars.compile(returnedTemplate);
            $location.html(compiledTemplate(data));
            defer.resolve(true);
        });

        return defer.promise();
    };

})(app.viewManager);

