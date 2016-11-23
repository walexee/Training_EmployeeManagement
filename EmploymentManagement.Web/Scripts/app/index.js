var app = app || {};

app.index = app.index || {};


(function (index, sessionManager, viewManager) {
    index.init = function () {
        if (!sessionManager.isLoggedIn()) {
            showLogin();
        }
    };

    function showLogin() {
        viewManager.showView('login.html', null, $('#modal-section'))
            .done(function (d) {
                var $modalDiv = $('#login-modal');
                $modalDiv.modal('show');

                $modalDiv.find('#register-btn').click(function () {
                    $modalDiv.modal('hide');
                    showRegistration();
                });

            });
    }

    function showRegistration() {
        viewManager.showView('register.html', null, $('#modal-section'))
            .done(function (d) {
                var $modalDiv = $('#register-modal')

                $modalDiv.modal('show');

                $modalDiv.find('#register-btn').click(function () {
                    var email = $('[name=email]').val();
                    var password = $('[name=password]').val();
                    var confirmPassword = $('[name=confirmPassword]').val();

                    sessionManager.register(email, password, confirmPassword);
                });
            });
    }

})(app.index, app.sessionManager, app.viewManager);

