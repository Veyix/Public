/// <reference path=http://ajax.aspnetcdn.com/ajax/jquery/jquery-2.0.3.min.js" />

var App = (function ($, undefined) {

    function App() {
    };

    App.prototype.init = function () {
        if ($ === undefined) {
            alert("jQuery not initialized");

            return;
        }

        $(document).ready(start);
    };

    function start() {

        $("#error").hide();

        // Download all values from the API
        $.getJSON('api/products')
            .error(
                function (data) {
                    $("#error").text("Code: " + data.status + " - Error: " + data.statusText);
                    $("#error").show();
                }
            )
            .done(
                function (data) {
                    displayProducts(data);
                }
            );
    };

    function displayProducts(products) {
        $.each(products, function (key, item) {
            $('<li>', { text: item }).appendTo('#values');
        });
    }

    return App;
})(window.jQuery);