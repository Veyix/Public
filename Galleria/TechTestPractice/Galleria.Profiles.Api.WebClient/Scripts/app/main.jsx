/// <reference path="errorHandler.js" />
/// <reference path="api.js" />

window.ClientApplication = (function () {
    var _errorHandler;
    var _api;

    function ClientApplication(errorHandler, api, elementId) {
        _errorHandler = errorHandler;
        _api = api;

		this.elementId = elementId;
		this.isLoggedIn = false;
	}

	ClientApplication.prototype.run = function () {

		var control = null;
		if (this.isLoggedIn) {
			// TODO: Render the application.
		}
		else {

		    // Render the login form
            control = <LoginForm submit={onLoginSubmitted} />
		}

		var container = document.getElementById(this.elementId);
		ReactDOM.render(
            control,
            container
        );
	};

    function onLoginSubmitted(username, password) {
        try {
            _api.login(username, password);
        }
        catch (error) {
            _errorHandler.handle(error, "Login Failed");
        }
    };

	return ClientApplication;
})();