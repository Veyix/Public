window.Dialog = (function () {
    function Dialog(elementId) {
        this.elementId = elementId;

        close = close.bind(this);
    }

    // Instance Methods
    Dialog.prototype.show = function (title, content) {

        var element = document.getElementById(this.elementId);
        if (!element) {
            throw new Error("Unable to find the " + this.elementId + " element");
        }

        ReactDOM.render(
            (
            <div className="row">
                <div className="col-md-6 col-md-offset-3 vertical-align">
                    <div className="dialog">
                        <div className="panel panel-default text-center">
                            <div className="panel-heading">
                                <h3>{title}</h3>
                            </div>
                            <div className="panel-body">
                                <div>
                                    {content}
                                </div>
                                <div className="buttons">
                                    <button className="btn btn-default" onClick={close}>
                                        Close
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            ),
            element
        );

        // Show the dialog
        element.className = element.className.replace(" hide", "");
    };

    function close(event) {
        event.preventDefault();

        var element = document.getElementById(this.elementId);
        element.className += " hide";
    };

    return Dialog;
})();