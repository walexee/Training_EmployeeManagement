var app = app || {};

app.index = app.index || {};

(function (index, sessionManager, viewManager, common) {
    index.init = function () {
        showEmployeePage();
    };

    function showEmployeePage() {
        $.get('/api/employee/' + sessionManager.getUserId()).done(function (data) {
            viewManager.showView('employee.html', data);
        });
    };

})(app.index, app.sessionManager, app.viewManager, app.common);

