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

        this.addUser = this.addUser.bind(this);
        this.viewUser = this.viewUser.bind(this);
        this.editUser = this.editUser.bind(this);
        this.deleteUser = this.deleteUser.bind(this);
        this.saveUser = this.saveUser.bind(this);
        this.logout = this.logout.bind(this);
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

    addUser() {

        // Show an empty user in edit mode
        this.showUser({ id: 0 }, true);
    }

    viewUser(userId) {

        // Get the user for the dialog
        this.api.getUser(
            userId,
            (response) => this.showUser(response, false)
        );
    }

    editUser(userId) {

        // Get the user for the dialog and show in edit mode
        this.api.getUser(
            userId,
            (response) => this.showUser(response, true)
        );
    }

    deleteUser(userId) {

        // Delete the user then refresh the data
        this.api.deleteUser(
            userId,
            (response) => this.refresh()
        );
    }

    saveUser(user) {

        // Update the user then refresh the view
        this.api.saveUser(
            user,
            (response) => this.refresh()
        );
    }

    showUser(user, isEditMode) {

        var dialog = new Dialog('dialog');
        var title = user.title + " " + user.forename + " " + user.surname;

        if (user.id === 0) {
            title = "Add User";
        }

        dialog.show(
            title,
            <User user={user} isEditMode={isEditMode} addButton={dialog.addButton} onSave={(user) => { dialog.close(); this.saveUser(user); }} />
        );
    }

    logout() {
        this.api.logout();

        if (this.props.onLoggedOut) {
            this.props.onLoggedOut();
        }
    }

    render() {
        const users = this.state.users.map(
            (user) => <UserSummary key={user.id} user={user} onViewUser={this.viewUser} onEditUser={this.editUser} onDeleteUser={this.deleteUser} />
        );

        return (
            <div className="panel panel-default">
                <div className="panel-heading">
                    <div className="row">
                        <h3 className="col-xs-10">Users</h3>
                        <div className="col-xs-2 vertical-align buttons">
                            <a className="btn btn-default" onClick={this.addUser}>Add User</a>
                            <a className="btn btn-default" onClick={this.logout}>Logout</a>
                        </div>
                    </div>
                </div>

                <div className="panel-body">
                    {users}
                </div>
            </div>
        );
    }
}