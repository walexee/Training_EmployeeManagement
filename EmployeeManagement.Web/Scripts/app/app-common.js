var app = app || {};

app.common = app.common || {};

(function (common) {
    common.getFormData = function (formSelector) {
        var formData = {};

        $(formSelector)
            .find('input:not([type=submit],[type=button]), select, textarea')
            .each(function (index, element) {
                if (!element.name)
                    return;

                formData[element.name] = element.value;
            });

        return formData;
    };


})(app.common);
