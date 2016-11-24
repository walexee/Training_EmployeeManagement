var app = app || {};

app.sessionManager = app.sessionManager || {};

(function (sessionManager, viewManager, common) {
    $.ajaxSetup({ beforeSend: ajaxBeforeSend, error: ajaxErrorHandler });

    sessionManager.isLoggedIn = function () {
        var token = sessionStorage.getItem('token');
        var expires = sessionStorage.getItem('expires');

        return (token && new Date(expires) > new Date());
    };

    sessionManager.getToken = function () {
        if (!sessionManager.isLoggedIn())
            return null;

        return sessionStorage.getItem('token');
    };

    sessionManager.getUserId = function () {
        return sessionStorage.getItem('userid');
    };

    sessionManager.login = function (loginInfo) {
        var defer = jQuery.Deferred();

        loginInfo.grant_type = 'password';

        $.post('/token', loginInfo)
            .done(function (token) {
                sessionStorage.setItem('token', token.access_token);
                sessionStorage.setItem('expires', token['.expires']);
                sessionStorage.setItem('username', token.userName);
                sessionStorage.setItem('userid', token.userId);

                defer.resolve(true);
            })
            .fail(function (err) {
                console.log(err);
                defer.reject(false);
            });

        return defer.promise();
    };

    sessionManager.logout = function () {
        sessionStorage.removeItem('token');
        sessionStorage.removeItem('expires');
        sessionStorage.removeItem('username');
        sessionStorage.removeItem('userid');
    };

    sessionManager.register = function (data) {
        $.post('/api/account/register', data).done(function (result) {
            console.log(result);
        });
    };

    function showLogin() {
        viewManager.showModal('login.html')
            .done(function (d) {
                var $modalDiv = $('#modalView');

                $modalDiv.find('#register-btn').click(function () {
                    $modalDiv.modal('hide');
                    showRegistration();
                });

                $('#login-btn').click(function () {
                    var loginInfo = common.getFormData('#login-form');

                    sessionManager.login(loginInfo)
                    .done(function () {
                        $modalDiv.modal('hide');
                        document.location.reload();
                    });
                });

            });
    }

    function showRegistration() {
        viewManager.showModal('register.html')
            .done(function (d) {
                var $modalDiv = $('#modalView')

                $modalDiv.modal('show');

                $modalDiv.find('#register-btn').click(function () {
                    var data = common.getFormData('#register-form');

                    sessionManager.register(data);
                });
            });
    }

    function ajaxBeforeSend(e, data) {
        if (sessionManager.isLoggedIn()) {
            e.setRequestHeader('Authorization', 'Bearer ' + sessionManager.getToken());
        }

        return true;
    };

    function ajaxErrorHandler(xhr, status, error) {
        if (xhr.status == 401) {
            showLogin();
        }
        else {
            viewManager.showDialog(error, 'Error!');
        }
    };

})(app.sessionManager, app.viewManager, app.common);

