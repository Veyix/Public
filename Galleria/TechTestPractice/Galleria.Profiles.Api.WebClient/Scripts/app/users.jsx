/// <reference path="errorHandler.jsx" />
/// <reference path="dialog.js" />
/// <reference path="api.js" />

class UsersView extends React.Component {
    constructor(props) {
        super(props);

        this.errorHandler = props.errorHandler;
        this.api = props.api;

        this.state = {
            users: []
        };

        this.viewUser = this.viewUser.bind(this);
        this.editUser = this.editUser.bind(this);
        this.deleteUser = this.deleteUser.bind(this);
    }

    componentWillMount() {
        this.refresh();
    }

    refresh() {

        // Use the API to get the users
        this.api.getUsers(
            (response) => this.setState({ users: response })
        );
    }

    viewUser(userId) {

        // Get the user for the dialog
        this.api.getUser(
            userId,
            (response) => this.showUser(response)
        );
    }

    editUser(userId) {
    }

    deleteUser(userId) {
    }

    showUser(user) {

        var dialog = new Dialog('dialog');
        var title = user.title + " " + user.forename + " " + user.surname;

        dialog.show(
            title,
            (
                <div className="text-left">
                    <div className="row">
                        <div className="col-sm-6">
                            <div><strong>User Id</strong></div>
                            <div className="text-display">{user.id}</div>
                        </div>
                        <div className="col-sm-6">
                            <div><strong>Company Id</strong></div>
                            <div className="text-display">{user.companyId}</div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-sm-6">
                            <div><strong>Title</strong></div>
                            <div className="text-display">{user.title}</div>
                        </div>
                        <div className="col-sm-6">
                            <div><strong>Forename</strong></div>
                            <div className="text-display">{user.forename}</div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-sm-6">
                            <div><strong>Surname</strong></div>
                            <div className="text-display">{user.surname}</div>
                        </div>
                        <div className="col-sm-6">
                            <div><strong>Date of Birth</strong></div>
                            <div className="text-display">{user.dateOfBirth}</div>
                        </div>
                    </div>
                    <div className="row text-muted">
                        <div className="col-sm-6">
                            <div><strong>Created</strong></div>
                            <div className="text-display">{user.createdDate}</div>
                        </div>
                        <div className="col-sm-6">
                            <div><strong>Last Changed</strong></div>
                            <div className="text-display">{user.lastChangedDate}</div>
                        </div>
                    </div>
                </div>
            )
        );
    }

    render() {
        const users = this.state.users.map(
            (user) => <User key={user.id} user={user} onViewUser={this.viewUser} onEditUser={this.editUser} onDeleteUser={this.deleteUser} />
        );

        return (
            <div className="panel panel-default">
                <div className="panel-heading">
                    <h3>Users</h3>
                </div>

                <div className="panel-body">
                    {users}
                </div>
            </div>
        );
    }
}