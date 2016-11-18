/// <reference path="../lib/jquery-1.10.2.js" />

class ErrorMessage extends React.Component {
    render() {
        if (!this.props.message) {
            return null;
        }

        return (
            <div className="alert alert-danger">
                <strong>{this.props.header}:&nbsp;</strong>
                <span>{this.props.message}</span>
            </div>
        );
    }
}

window.ErrorHandler = (function () {
    function ErrorHandler(errorElementId) {
        this.errorElementId = errorElementId;
    }

    ErrorHandler.prototype.handle = function (error, description) {

        var message = error.message || error.toString();
        var header = description || "Error";

        render(this.errorElementId, header, message);
    };

    ErrorHandler.prototype.reset = function () {
        render(this.errorElementId, null, null);
    };

    function render(containerId, header, message) {

        var container = document.getElementById(containerId);
        ReactDOM.render(
            <ErrorMessage header={header} message={message} />,
            container
        );
    }

    return ErrorHandler;
})();