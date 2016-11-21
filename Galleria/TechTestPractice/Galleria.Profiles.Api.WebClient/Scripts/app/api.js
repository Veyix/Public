/// <reference path="../lib/jQuery-1.10.2.js" />
/// <reference path="errorHandler.jsx" />

window.API = (function () {
    var _errorHandler;
    var _baseAddress;
    var _accessToken = null;

    function API(errorHandler, baseAddress) {
        _errorHandler = errorHandler;
        _baseAddress = baseAddress;
    }

    API.prototype.getIsLoggedIn = function () {
        return _accessToken !== null;
    };

    API.prototype.login = function (username, password, successCallback) {
        _errorHandler.reset();

        try {
            var payload = {
                contentType: 'application/x-www-form-urlencoded',
                dataType: 'json',
                url: getUrl('api/login'),
                type: 'POST',
                data: 'grant_type=password&username=' + username + '&password=' + password
            };

            $.ajax(payload)
                .done(function (response) {
                    storeAccessToken(response);
                    successCallback(response);
                })
                .error(function (response) {
                    handleError(response, "Login Failed");
                });
        }
        catch (error) {
            _errorHandler.handle(error, "Login Failed");
        }
    }

    function storeAccessToken(response) {
        _accessToken = response.access_token;

        // Store the auth token as a global AJAX header
        $.ajaxSetup({
            beforeSend: function (request) {
                request.setRequestHeader('Authorization', 'Bearer ' + _accessToken);
            }
        });
    }

    API.prototype.getUsers = function (successCallback) {
        _errorHandler.reset();

        try {
            var payload = {
                contentType: 'application/json',
                dataType: 'json',
                url: getUrl('api/users'),
                type: 'GET'
            };

            $.ajax(payload)
                .done(function (response) {
                    successCallback(response);
                })
                .error(function (response) {
                    handleError(response, "Failed to retrieve users");
                });
        }
        catch (error) {
            _errorHandler.handle(error, "Failed to retrieve users");
        }
    };

    API.prototype.getUser = function (userId, successCallback) {
        _errorHandler.reset();

        try {
            var payload = {
                contentType: 'application/json',
                dataType: 'json',
                url: getUrl('api/users/' + userId),
                type: 'GET'
            };

            $.ajax(payload)
            .done(function (response) {
                successCallback(response);
            })
            .error(function (response) {
                handleError(response, "Failed to retrieve user " + userId);
            });
        }
        catch (error) {
            _errorHandler.handle(error, "Failed to retrieve user " + userId);
        }
    };

    API.prototype.deleteUser = function (userId, successCallback) {
        _errorHandler.reset();

        try {
            var payload = {
                contentType: 'application/json',
                dataType: 'json',
                url: getUrl('api/users/' + userId),
                type: 'DELETE'
            };

            $.ajax(payload)
            .done(function (response) {
                successCallback(response);
            })
            .error(function (response) {
                handleError(response, "Failed to delete user " + userId);
            });
        }
        catch (error) {
            _errorHandler.handle(error, "Failed to delete user " + userId);
        }
    };

    API.prototype.saveUser = function (user, successCallback) {
        _errorHandler.reset();

        try {
            var payload = {
                contentType: 'application/json',
                dataType: 'json',
                url: getUrl('api/users'),
                type: user.id == 0 ? 'POST' : 'PUT',
                data: JSON.stringify(user)
            };

            $.ajax(payload)
                .done(function (response) {
                    successCallback(response);
                })
                .error(function (response) {
                    handleError(response, "Failed to save user: " + user.title + " " + user.forename + " " + user.surname);
                });
        }
        catch (error) {
            _errorHandler.handle(error, "Failed to save user: " + user.title + " " + user.forename + " " + user.surname);
        }
    };

    function handleError(response, header) {

        // Build the description of the error
        var description = response.status + ": " + response.statusText;
        if (response.responseText) {

            // We have a response object, so try to read more error information from it
            var object = JSON.parse(response.responseText);
            description = object.error_description || description;
        }

        _errorHandler.handle(description, header);
    }

    function getUrl(address) {
        return _baseAddress + address;
    }

    return API;
})();