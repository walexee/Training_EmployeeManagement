var app = app || {};

app.viewManager = app.viewManager || {};

(function (viewManager) {
    var baseTemplateUrl = '/templates/';

    viewManager.showView = function (template, data, $location) {
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

