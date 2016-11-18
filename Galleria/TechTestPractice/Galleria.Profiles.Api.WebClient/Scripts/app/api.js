/// <reference path="../lib/jQuery-1.10.2.js" />
/// <reference path="errorHandler.jsx" />

window.API = (function () {
    var _errorHandler;
    var _baseAddress;
    var _accessToken;

    function API(errorHandler, baseAddress) {
        _errorHandler = errorHandler;
        _baseAddress = baseAddress;
    }

    API.prototype.login = function (username, password, successCallback) {

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
                .error(function (response, status, error) {

                    // Build the description of the error
                    var description = response.status + ": " + response.statusText;
                    if (response.responseText) {

                        // We have a response object, so try to read more error information from it
                        var object = JSON.parse(response.responseText);
                        description = object.error_description || description;
                    }

                    _errorHandler.handle(description, "Login Failed");
                });
        }
        catch (error) {
            _errorHandler.handle(error, "Login Failed");
        }
    }

    function storeAccessToken(response) {
        _accessToken = response.access_token;
    }

    function getUrl(address) {
        return _baseAddress + address;
    }

    return API;
})();