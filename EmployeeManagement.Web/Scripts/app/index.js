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
                $modal.find('button#cancel-btn').click(function () {
                    $modal.modal('hide');
                });

                $modal.find('button#request-time-off-btn').click(function () {
                    var data = common.getFormData('#time-off-form');

                    data.employeeId = sessionManager.getUserId();

                    $.post('/api/timeOff', data)
                        .done(function () {
                            viewManager.showDialog('Request time off done!', 'Time Off Added');
                        })
                        .fail(function () {
                            viewManager.showDialog('Request time off failed!', 'Error');
                        });

                    $modal.modal('hide');
                });
            });
    }

})(app.index, app.sessionManager, app.viewManager, app.common);

