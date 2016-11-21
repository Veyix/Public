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
            <User user={user} />
        );
    }

    render() {
        const users = this.state.users.map(
            (user) => <UserSummary key={user.id} user={user} onViewUser={this.viewUser} onEditUser={this.editUser} onDeleteUser={this.deleteUser} />
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