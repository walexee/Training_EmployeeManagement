var app = app || {};

app.sessionManager = app.sessionManager || {};


(function (sessionManager) {
    sessionManager.isLoggedIn = function () {
        return false; //TODO: implement
    };

    sessionManager.register = function (email, password, confirmPassword) {
        $.post('/api/account/register', {
            email: email,
            password: password,
            confirmPassword: confirmPassword
        }).done(function (result) {
            console.log(result);
        });
    }


})(app.sessionManager);

