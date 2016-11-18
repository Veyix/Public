/// <reference path="errorHandler.js" />
/// <reference path="api.js" />

window.ClientApplication = (function () {
    var _errorHandler;
    var _api;
    var _elementId;
    var _isLoggedIn;

    function ClientApplication(errorHandler, api, elementId) {
        _errorHandler = errorHandler;
        _api = api;
		_elementId = elementId;
		_isLoggedIn = false;
	}

    ClientApplication.prototype.run = function () {
        updateDisplay();
	};

	function updateDisplay() {

	    var control = null;
	    if (_isLoggedIn) {
	        // TODO: Render the application.
            control = <p className="alert alert-success">Login Successful!</p>
	    }
	    else {

	        // Render the login form
	        control = <LoginForm submit={onLoginSubmitted} />
	    }

	    var container = document.getElementById(_elementId);
	    ReactDOM.render(
            control,
            container
        );
    }

    function onLoginSubmitted(username, password) {
        try {
            _api.login(username, password, handleLoginSuccess);
        }
        catch (error) {
            _errorHandler.handle(error, "Login Failed");
        }
    };

    function handleLoginSuccess(response) {
        _isLoggedIn = true;

        updateDisplay();
    }

	return ClientApplication;
})();