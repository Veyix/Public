window.Dialog = (function () {
    function Dialog(elementId) {
        this.elementId = elementId;
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
                                {content}
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

    return Dialog;
})();