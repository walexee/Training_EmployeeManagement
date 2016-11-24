var app = app || {};

app.index = app.index || {};

(function (index, sessionManager, viewManager, common) {
    index.init = function () {
        showEmployeePage();
        $('div#main-view').on('click', '#timeoff-request', requestTimeOff);
    };

    function showEmployeePage() {
        $.get('/api/employee/' + sessionManager.getUserId()).done(function (data) {
            viewManager.showView('employee.html', data);
        });
    }

    function requestTimeOff() {
        viewManager
            .showModal('timeoff-request.html')
            .done(function ($modal) {
                console.log($modal);
            });
    }

})(app.index, app.sessionManager, app.viewManager, app.common);

